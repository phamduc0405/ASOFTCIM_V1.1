﻿using A_SOFT.PLC;
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

namespace ASOFTCIM
{
    public partial class ACIM
    {
        #region Field
        private CimHelper _cim;
        private PlcComm _plc;
        private PLCHelper _plcH;
        private List<TRACESV> _tracesvs = new List<TRACESV>();


        public EQPDATA EqpData { get; set; }
        public string EQPID { get; set; } = "EQPTEST";
        public EquipmentConfig _eqpConfig;


        private bool _isEqStatusUpdate = false;
        private bool _isSvidUpdate = false;
        private List<string> _unitUpdate = new List<string>();
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
        #endregion

        public ACIM(EquipmentConfig equipmentConfig)
        {
            Initial();
            _cim = new CimHelper(EQPID);
            _cim.Init(ATCPIP.ConnectMode.Passive, "127.0.0.1", 8000);
            _cim.SysPacketEvent += _cim_SysPacketEvent;
            _cim.TransTimeOutEvent += _cim_TransTimeOutEvent;
            _plc = new PlcComm();
            _eqpConfig = equipmentConfig;
            //LoadExcelConfig(@"D:\Project_New\ACIM\SDCCIM_ASOFT_Portal_Online_Map_SDC_Basic_V1.21_v0.1.xlsx");
            InitialPlc();
            
        }

       

        public void Stop()
        {
            SysPacket sysPacket = new SysPacket(_cim.Conn);
            sysPacket.DeviceId = 1;
            sysPacket.SystemByte = EqpData.TransactionSys+1 ;
            sysPacket.Command = Command.SeparateReq;
            sysPacket.Send2Sys();
            Thread.Sleep(1000);
            _cim.Close();
        }
        private void Initial()
        {
            EqpData = new EQPDATA();
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
                EqpData.TransactionSys = sysPacket.SystemByte;
                EqpData.DeviceId = sysPacket.DeviceId;
                sysPacket.MakeCimLog();
                if (sysPacket.DeviceId != 1)
                {
                    SendS9F1(sysPacket);
                    return;
                }

                MethodInfo method = this.GetType().GetMethod($"RecvS{sysPacket.Stream}F{sysPacket.Function}");
                if (method != null)
                {
                    object result = method.Invoke(this, null);

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
        public void SendMessage2PLC(string classname,object obj)
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
                        Console.WriteLine($"Tạo instance của {classname} thành công!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi tạo instance: {ex.Message}");
                }
            }
        }
    }
}
