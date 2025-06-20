using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using ASOFTCIM.Data;
using ASOFTCIM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static A_SOFT.PLC.MelsecIF;
using System.Windows.Shapes;
using ASOFTCIM.Config;
using A_SOFT.Ctl.SecGem;
using Type = System.Type;
using FontAwesome.Sharp;
using ASOFTCIM.Message.PLC2Cim.Recv;
using ASOFTCIM.MainControl.Device.PC;
using ASOFTCIM.MainControl;
using A_SOFT.CMM.INIT;
using System.Diagnostics;
using System.Xml.Linq;
using System.Net.Sockets;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        #region Event
        public delegate void PLC2CIMEventDelegate(string bit);
        public event PLC2CIMEventDelegate Plc2CimChangeEvent;
        public delegate void CIM2PLCEventDelegate(string bit);
        public event CIM2PLCEventDelegate Cim2PlcChangeEvent;
        public delegate void Host2CimEventDelegate(string SnFm);
        public event Host2CimEventDelegate Host2CimChangeEvent;
        public delegate void Cim2HostChangeEventDelegate(string SnFm);
        public event Cim2HostChangeEventDelegate Cim2HostChangeEvent;
        #endregion



        #region Field
        private CimHelper _cim;
        private PlcComm _plc;
        private PLCHelper _plcH;
        private List<TRACESV> _tracesvs = new List<TRACESV>();


        public EQPDATA EqpData { get; set; }
        public string EQPID { get; set; } = "";
        public EquipmentConfig _eqpConfig;

        private Thread _simulatorAlarm;
        private bool _issimulatorAlarm;
        private bool _isEqStatusUpdate = false;
        private bool _isSvidUpdate = false;
        #endregion



        #region Property
        public EquipmentConfig EqpConfig
        {
            get { return _eqpConfig; }
            set { _eqpConfig = value; }
        }
        public CimHelper Cim
        {
            get { return _cim; }
            set { _cim = value; }
        }
        public PlcComm PLC
        {
            get { return _plc; }
            set { _plc = value; }
        }
        public PLCHelper PLCH
        {
            get { return _plcH; }
            set { _plcH = value; }
        }
        public List<TRACESV> Tracesvs
        {
            get { return _tracesvs; }
            set { _tracesvs = value; }
        }
        public bool IsSimulatorAlarm
        {
            get { return _issimulatorAlarm; }
            set { _issimulatorAlarm = value; }
        }
        #endregion

        #region Constructor

        public ACIM(EquipmentConfig equipmentConfig)
        {
            Initial();
            EQPID = equipmentConfig.EQPID;  
            _cim = new CimHelper(EQPID);
            ATCPIP.ConnectMode connectMode = (ATCPIP.ConnectMode)Enum.Parse(typeof(ATCPIP.ConnectMode), equipmentConfig.CimConfig.ConnectMode);
            string Ip = equipmentConfig.CimConfig.IP;
            ushort Port = ushort.Parse(equipmentConfig.CimConfig.Port);
            //0:Passive ; 1:Active
            _cim.Init(connectMode, Ip, Port);
            _cim.SysPacketEvent += _cim_SysPacketEvent;
            _cim.TransTimeOutEvent += _cim_TransTimeOutEvent;
            _plc = new PlcComm();
            _eqpConfig = equipmentConfig;
            InitialPlc();
        }
        #endregion

        #region Private
        private void Initial()
        {
            EqpData = new EQPDATA();
            
            EqpData.EQINFORMATION.EQPVER = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        private void _cim_TransTimeOutEvent(TransactionWait trans)
        {
            SysPacket sysPacket = new SysPacket(_cim.Conn);
            sysPacket.DeviceId = 1;
            sysPacket.SystemByte = trans.TransactionSys;
            SendS9F9(sysPacket);

        }
        private void _cim_SysPacketEvent(SysPacket sysPacket)
        {
            try
            {
                
                EqpData.DeviceId = sysPacket.DeviceId;
               
                if (sysPacket.DeviceId != 1)
                {
                    SendS9F1(sysPacket);
                    return;
                }

                MethodInfo method = this.GetType().GetMethod($"RecvS{sysPacket.Stream}F{sysPacket.Function}");
                if (method != null)
                {
                    if (sysPacket.Function%2 == 0)
                    {
                      //  EqpData.TransactionSys = sysPacket.SystemByte + 1;
                        RemoveTrans(sysPacket.SystemByte);
                    }
                    else
                    {
                        EqpData.TransactionSys = sysPacket.SystemByte;
                    }
                    string snfm = $"S{sysPacket.Stream}F{sysPacket.Function}";

                    int ceid = 0;
                    if (sysPacket.Items.Count > 1 && int.TryParse(sysPacket.Items[1]?.ToString(), out int tmp))
                    {
                        ceid = tmp;
                    }
                    string messageName = GetMessageName(snfm, ceid);
                    Host2CimEventHandle($"HOST -> CIM :Recv{snfm} {messageName}");
                    object result = method.Invoke(this, new object[] { sysPacket });

                    return;
                }
                else
                {
                    bool hasStream = this.GetType().GetMethods().Any(m => m.Name.StartsWith($"RecvS{sysPacket.Stream}"));
                    if (!hasStream) SendS9F3(sysPacket);
                    else SendS9F5(sysPacket);

                    return;
                }
            }
            catch (Exception ex)
            {
                SendS9F7(sysPacket);
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
                ex.Data.Clear();
            }


        }
        private static readonly Dictionary<(string SnFm, int Ceid), string> _messageNameMap = new Dictionary<(string, int), string>
        {
            { ("S1F1", 0), "Are You There?" },
            { ("S1F2", 0), "CIM CONNECT" },
            { ("S6F11", 106), "EQP STATE" },
            { ("S6F11", 101), "EQP STATE CHANGE" },
            { ("S2F17", 0), "DATETIMEREQUEST" },
            { ("S2F18", 0), "DATETIME" },
            { ("S1F5", 1), "EQP STATE REQUEST" },
            { ("S1F6", 1), "EQP STATE" },
            { ("S1F5", 2), "UNIT STATE REQUEST" },
            { ("S1F6", 2), "UNIT STATE" },
            { ("S1F5", 3), "MATERIAL STATE REQUEST" },
            { ("S1F6", 3), "MATERIAL STATE" },
            { ("S1F5", 4), "PORT STATE REQUEST" },
            { ("S1F6", 4), "PORT STATE" },
            { ("S1F5", 5), "FUNCTION STATE REQUEST" },
            { ("S1F6", 5), "FUNCTION STATE" },

            { ("S2F13", 0), "LIST ECM REQUEST" },
            { ("S2F14", 0), "LIST ECM" },
            { ("S2F29", 0), "CONSTANT NAME LIST ECM REQUEST" },
            { ("S2F30", 0), "CONSTANT NAME LIST ECM" },

            { ("S1F3", 0), "LIST NAME FDC REQUEST" },
            { ("S1F4", 0), "LIST NAME FDC" },
            { ("S1F11", 0), "VARIABLE NAME LIST FDC REQUEST" },
            { ("S1F12", 0), "VARIABLE NAME LIST FDC" },
            { ("S2F23", 0), "TRACE FDC REQUEST" },
            { ("S2F24", 0), "TRACE FDC ACK" },
            { ("S6F1", 0), "TRACE FDC" },

            { ("S2F41", 1), "OPCALL REQUEST" },
            { ("S2F42", 1), "OPCALL ACK" },
            { ("S6F11", 501), "OPCALL COMFIRM" },
            { ("S10F5", 0), "TERMINAL REQUEST" },
            { ("S10F6", 0), "TERMINAL ACK" },

            { ("S2F41", 2), "INTERLOCK REQUEST" },
            { ("S2F42", 2), "INTERLOCK ACK" },
            { ("S6F11", 502), "INTERLOCK COMFIRM" },

            { ("S5F1", 0), "ALARM REPORT" },
            { ("S5F2", 0), "ALARM ACK" },

            { ("S6F11", 606), "TPM" },
            { ("S6F203", 257), "SPECIFIC VALIDATION REQUEST" },
            { ("S3F103", 257), "SPECIFIC VALIDATION " },
            { ("S6F11", 257), "CARRIER RELEASE COMPLETE" },

        };

        private string GetMessageName(string snfm, int ceid)
        {
            if (_messageNameMap.TryGetValue((snfm, ceid), out string name))
            {
                if(ceid != 0)
                {
                    return $"{ceid} " + name;
                }
                return name;
            }
            return "";
        }

        private void AlarmReader()
        {
            while (_issimulatorAlarm)
            {
                try
                {
                    Data.Alarm alarm = new Data.Alarm();

                    for(int i =1; i<=5;i++)
                    {
                        alarm = EqpData.ALS[i];
                        alarm.ALST = "2";
                        SendS5F1(alarm);
                    }
                }
                catch (Exception ex)
                {
                    LogTxt.Add(LogTxt.Type.Exception, $"Class:{this.GetType().Name} Method:{MethodBase.GetCurrentMethod().Name} exception: {ex.Message}");
                }

                Thread.Sleep(100);
            }
        }
        #endregion

        #region Public
        public void Stop()
        {
            SysPacket sysPacket = new SysPacket(_cim.Conn);
            sysPacket.DeviceId = 1;
            sysPacket.SystemByte = EqpData.TransactionSys ++;
            sysPacket.Command = Command.SeparateReq;
            sysPacket.Send2Sys();
            Thread.Sleep(1000);
            _cim.Close();
        }


        public Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }

        public void AddTrans(uint trans)
        {
            _cim.AddTrans(trans);
        }
        public void RemoveTrans(uint trans)
        {
            _cim.RemoveTrans(trans);
        }
        public void SendMessage2PLC(string classname, object obj)
        {
            string namespaces = "ASOFTCIM.Message.PLC2Cim.Send";
            Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), namespaces);
            Type t = Assembly.GetExecutingAssembly().GetType($"{namespaces}.{classname}");

            if (t != null && typelist.Contains(t))
            {
                try
                {
                    object instance = Activator.CreateInstance(t, new object[] { _plcH, obj });

                    if (instance != null)
                    {
                        Cim2PlcEventHandle($"CIM -> EQP :Recv {classname}");
                    }
                }
                catch (Exception ex)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.",
                        MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            }
        }
        public void SendMessage2PLC(string classname, object obj, PlcComm plcComm)
        {
            string namespaces = "ASOFTCIM.Message.PLC2Cim.Send";
            Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), namespaces);
            Type t = Assembly.GetExecutingAssembly().GetType($"{namespaces}.{classname}");

            if (t != null && typelist.Contains(t))
            {
                try
                {
                    object instance = Activator.CreateInstance(t, new object[] { _plcH, _plc, obj });

                    if (instance != null)
                    {
                        Cim2PlcEventHandle($"CIM -> EQP :Recv {classname}");
                    }
                }
                catch (Exception ex)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.",
                        MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            }
        }
        public void SendMessage2PLC(string classname, object obj, string Unitid)
        {
            string namespaces = "ASOFTCIM.Message.PLC2Cim.Send";
            Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), namespaces);
            Type t = Assembly.GetExecutingAssembly().GetType($"{namespaces}.{classname}");

            if (t != null && typelist.Contains(t))
            {
                try
                {
                    object instance = Activator.CreateInstance(t, new object[] { _plcH, obj, Unitid });

                    if (instance != null)
                    {
                        Cim2PlcEventHandle($"CIM -> EQP :Recv {classname}");
                    }
                }
                catch (Exception ex)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.",
                        MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            }
        }
        public void StartSiMulatorAlarm()
        {
            if (_simulatorAlarm != null && _simulatorAlarm.IsAlive)
                return;
            _issimulatorAlarm = true;
            _simulatorAlarm = new Thread(() => AlarmReader());
            _simulatorAlarm.IsBackground = true;
            _simulatorAlarm.Start();
        }
        public void StopSiMulatorAlarm()
        {
            //_controller.CIM.PLCH.TestAlarm = false;
            if (_simulatorAlarm != null && _simulatorAlarm.IsAlive)
            {
                _issimulatorAlarm = false;
                _simulatorAlarm.Join();
                _simulatorAlarm = null;
            }
        }
        public void GetNameofMessage( int Stream, int Function,List<Item> Items)
        {
            string snfm = $"S{Stream}F{Function}";
            int ceid = 0;
            if (Items.Count > 1 && int.TryParse(Items[1]?.ToString(), out int tmp))
            {
                ceid = tmp;
            }
            if(Items.Count > 1 && int.TryParse(Items[2]?.ToString(), out int tmpS6f11) && Stream == 6 && Function == 11)
            {
                ceid = tmpS6f11;
            }
            string messageName = GetMessageName(snfm, ceid);
            Host2CimEventHandle($"CIM -> HOST :SEND S{Stream}F{Function} {messageName}");
        }
        #endregion

        #region Eventhandle
        private void Plc2CimEventHandle(string bit)
        {
            var handle = Plc2CimChangeEvent;
            if (handle != null)
            {
                handle(bit);
            }
        }
        private void Cim2PlcEventHandle(string bit)
        {
            var handle = Cim2PlcChangeEvent;
            if (handle != null)
            {
                handle(bit);
            }
        }
        private void Host2CimEventHandle(string SnFm)
        {
            var handle = Host2CimChangeEvent;
            if (handle != null)
            {
                handle(SnFm);
            }
        }
        private void Cim2HostEventHandle(string SnFm)
        {
            var handle = Cim2HostChangeEvent;
            if (handle != null)
            {
                handle(SnFm);
            }
        }
        #endregion

    }
}
