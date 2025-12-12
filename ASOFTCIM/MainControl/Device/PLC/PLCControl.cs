using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using ASOFTCIM.Data;
using ASOFTCIM.Helper;
using ASOFTCIM.Message.PLC2Cim.Recv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static A_SOFT.PLC.MelsecIF;
using Type = System.Type;
using System.Xml.Linq;
using ASOFTCIM.Message.PLC2Cim.Send;
using HPSocket.Sdk;
using System.Diagnostics;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        private Thread _aliveBit;
        private bool _isPlcConnected;

        public delegate void PlcConnectChangeEventDelegate(bool isConnected);
        public event PlcConnectChangeEventDelegate PlcConnectChangeEvent;
        public event Action ResetEvent;
        public StopWatch stopWatch;
        public void InitialPlc(PlcComm plcComm, PLCHelper pLCHelper)
        {
            stopWatch = new StopWatch();
            if (_eqpConfig.PLCConfig != null)
            {
                Task.Run(async () =>
                {
                    try
                    {
                        // Khởi tạo đối tượng PLC
                        _plc = plcComm;
                        _plc.ConfigComm(_eqpConfig.PLCConfig);
                        _plc.Start();

                        _plcH = pLCHelper;
                        _plcH = _eqpConfig.PLCHelper;
                        _plcH.IsLogPLC = _eqpConfig.UseLogPLC;
                        _plcH.Start(_plc, _eqpConfig.EQPID);
                        _aliveBit = new Thread(Alive)
                        {
                            IsBackground = true
                        };
                        _aliveBit.Start();

                        // Đọc dữ liệu từ PLC
                        ReadEqpState();
                        //ReadRMS();
                        ReadECM();
                        ReadAPC();
                        ReadFunction();
                        // Gán sự kiện thay đổi trạng thái bit
                        _plcH.BitChangedEvent += (bit) =>
                        {
                            PLCBitChange(bit.Comment, bit);
                        };

                        _plcH.WordChangedEvent += _plcH_WordChangedEvent;
                        this.EqpData.ALS = _eqpConfig.PLCHelper.Alarms;
                    }
                    catch (Exception ex)
                    {
                        var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                        LogTxt.Add(LogTxt.Type.Exception, debug);
                    }
                });
            }
        }
        
        private void _plcH_WordChangedEvent(string Method, object data)
        {

                try
                {
                    if (Method.Contains("EQPSTATUS"))
                    {
                        if (_isEqStatusUpdate) return;
                        _isEqStatusUpdate = true;
                        UpdateStatus(data);
                    }
                    if (Method.Contains("FDC"))
                    {
                        if (_isSvidUpdate) return;
                        _isSvidUpdate = true;
                        UpdateSVID(data);
                        _isSvidUpdate = false;
                    }
                    if (Method.Contains("ALARMREPORT"))
                    {
                        var resul = new ALARMREPORT().Excute(this, data);
                        Task.WaitAll(resul);
                        ResetEvent?.Invoke();
                    }
                    if (new[] { "UNITSTATUS", "MATERIALPORTSTATE", "PORTSTATUS" }.Any(Method.Contains))
                    {
                        PLCWordChange(Method, data);

                    }
                }
                catch (Exception ex)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            
           
        }
        private async void PLCWordChange(string name, object w)
        {
            await Task.Run(async () =>
            {
                string namespaces = "ASOFTCIM.Message.PLC2Cim.Recv";
                Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), namespaces);
                Type t = Assembly.GetExecutingAssembly()
                    .GetType(string.Format($"{namespaces}.{name}"));

                if (typelist.Contains(t))
                {
                    object s = Activator.CreateInstance(t);
                    MethodInfo method = t.GetMethod($"Excute");
                    if (method != null)
                    {
                        object result = method.Invoke(s, new object[] { this, w });
                    }
                    return;
                }
            });
        }


        private async void PLCBitChange(string name, BitModel bit)
        {
            await Task.Run(async () =>
            {
                if (bit.GetPLCValue) 
                {
                    string namespaces = "ASOFTCIM.Message.PLC2Cim.Recv";
                    Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), namespaces);
                    Type t = Assembly.GetExecutingAssembly()
                        .GetType(string.Format($"{namespaces}.{name}"));

                    if (typelist.Contains(t))
                    {
                        object s = Activator.CreateInstance(t);
                        MethodInfo method = t.GetMethod($"Excute");
                        if (method != null)
                        {
                            Plc2CimEventHandle("EQP -> CIM: B" + bit.PLCHexAdd + " - " + bit.Item);
                            object result = method.Invoke(s, new object[] { this, bit });

                        }
                        bit.SetPCValue = true;
                        return;
                    }
                    bit.SetPCValue = true;
                }
                else { bit.SetPCValue = false; }

            });
        }
        private void UpdateSVID(object data)
        {
            List<FDCModel> fdc = (List<FDCModel>)data;
            foreach (var item in fdc)
            {
                if (EqpData.SVID.Any(x => x.SVID == item.SVID))
                {
                    SV sv = EqpData.SVID.First(x => x.SVID == item.SVID);
                    sv.SVID = item.SVID;
                    sv.SVNAME = item.NAME;
                    sv.Remarks = item.Remarks;
                    sv.SVVALUE = item.GetValue(_plc);
                    //if (item.Type.ToUpper() == "DEC" && float.TryParse(sv.SVVALUE, out float result) && item.Remarks != 1)
                    //{
                    //    sv.SVVALUE = (result / item.Remarks).ToString("F4");
                    //}
                    if (item.Type.ToUpper() == "DEC" && float.TryParse(sv.SVVALUE, out float result) && item.Remarks != 1)
                    {
                        int decimals = 0;

                        if (item.Remarks > 1)
                        {
                            decimals = (int)Math.Log10(item.Remarks);
                        }

                        sv.SVVALUE = (result / item.Remarks).ToString($"F{decimals}");
                    }
                }
                else
                {
                    SV sv = new SV();
                    sv.SVID = item.SVID;
                    sv.SVNAME = item.NAME;
                    sv.Remarks = item.Remarks;
                    sv.SVVALUE = item.GetValue(_plc);
                    EqpData.SVID.Add(sv);
                }
            }
            Thread.Sleep(1000);
        }
        private async void UpdateStatus(object data)
        {
            await Task.Delay(100);
            bool isSend = false;
            List<WordModel> word = (List<WordModel>)data;

            EQPSTATE oldstate = EqpData.EQPSTATE.Copy<EQPSTATE>();
            foreach (var item in EqpData.EQPSTATE.GetType().GetProperties())
            {
                if (word.Any(x => x.Item == item.Name))
                {
                    WordModel w = word.FirstOrDefault(x => x.Item == item.Name);
                    var a = item.GetValue(EqpData.EQPSTATE, null) == null ? "" : item.GetValue(EqpData.EQPSTATE, null).ToString();
                    if (w.GetValue() != a)
                    {
                        isSend = true;
                        item.SetValue(EqpData.EQPSTATE, w.GetValue());
                    }
                }
            }
            if (PLCH.Bits.Find(x => x.Item == "TPMLOSSREADY").GetPLCValue)
            {
                isSend = false;
            }    
            if (isSend)
                SendS6F11_101( oldstate);

            WordModel crst = word.FirstOrDefault(x => x.Item == "CRST");
            if (crst.GetValue() != EqpData.EQINFORMATION.CRST)
            {
                EqpData.EQINFORMATION.CRST = crst.GetValue();
                string ceiID = "0";
                if (EqpData.EQINFORMATION.CRST == "0") ceiID = "104";
                if (EqpData.EQINFORMATION.CRST == "1") ceiID = "106";
                if (EqpData.EQINFORMATION.CRST == "2") ceiID = "105";
                if (ceiID != "0")
                    SendS6F11_104_106( ceiID);
            }
            _isEqStatusUpdate = false;
        }
        public void ReadRMS()
        {
            this.EqpData.PPIDList.PPID = null;
            List<string> list = new List<string>();
            foreach (var ppid in _eqpConfig.PLCHelper.ListPPID)
            {
                
                    list.Add(ppid.GetValue(this.PLC));
                    this.EqpData.PPIDList.PPID = list;
                   
            }
            List<PPIDModel> word = this.PLCH.PPIDParams.ToList();
            List<WordModel> words = _plcH.Words.Where(x => x.Area == "EQPStatus").ToList();
            this.EqpData.CurrPPID.PPID = word.FirstOrDefault(x => x.Item == "PPID").GetValue(this.PLC);
            COMMANDCODE commandcode = new COMMANDCODE();
            PPPARAMS ppparam = new PPPARAMS();
            
            foreach (var ppidparam in _eqpConfig.PLCHelper.PPIDParams)
            {
                if (ppidparam.Item != "RESERVED")
                {
                    PARAM param = new PARAM();
                    param.PARAMVALUE = ppidparam.GetValue(this.PLC);
                    param.PARAMNAME = ppidparam.Item;
                    ppparam.PARAMS.Add(param);
                    commandcode.PARAMs.Add(param);
                }    
            }

            for (int i = 0; i < this.EqpData.PPIDList.PPID.Count; i++)
            {
                if (this.EqpData.PPIDList.PPID[i] == this.EqpData.CurrPPID.PPID)
                {
                    this.EqpData.CurrPPID.PPID_NUMBER = i.ToString();
                    break;
                }
            }
            if (this.EqpData.CurrPPID.COMMANDCODEs.Any(x => x.CCODE == commandcode.CCODE))
            {
                this.EqpData.CurrPPID.COMMANDCODEs = this.EqpData.CurrPPID.COMMANDCODEs.Where(x => x.CCODE != commandcode.CCODE).ToList();
            }
            this.EqpData.CurrPPID.COMMANDCODEs.Add(commandcode);
        }
        public void ReadECM()
        {
            //var count = this.EqpData.ECS.Count;
            //for (int i = 0; i < count; i++)
            //{
            //    this.EqpData.ECS.RemoveAt(i);
            //}    
            foreach (var ecm in _eqpConfig.PLCHelper.ECMS)
            {
                if (ecm.ECNAME != "RESERVED")
                {
                    EC ec = new EC();
                    ec.ECNAME = ecm.ECNAME;
                    ec.ECID = ecm.ECID;
                    ec.ECDEF = ecm.GetValue(this.PLC);
                    var index = _eqpConfig.PLCHelper.ECMS.IndexOf(ecm);

                    if(this.EqpData.ECS.Any(x => x.ECID == ec.ECID))
                    {
                        this.EqpData.ECS = this.EqpData.ECS.Where(x => x.ECID != ec.ECID).ToList();
                    }    
                    this.EqpData.ECS.Add(ec);
                }
            }
        }
        public void ReadAPC()
        {
            EqpData.PROCESSDATACONTROL.CELLs.Clear();
            foreach (var apc in _eqpConfig.PLCHelper.APCS)
            {
                EqpData.PROCESSDATACONTROL.EQPID = EqpData.EQINFORMATION.EQPID;
                PROCESS_CELL processcell = new PROCESS_CELL();
                PROCESS_MODULE processmodule = new PROCESS_MODULE();
                PARAM param = new PARAM();
                param.PARAMNAME = apc.Item;
                param.PARAMVALUE = apc.GetValue(this.PLC);
                processmodule.PARAMs.Add(param);
                processcell.MODULEs.Add(processmodule);
                EqpData.PROCESSDATACONTROL.CELLs.Add(processcell);
            }
            
        }
        public void ReadFunction()
        {
            EqpData.FUNCTION = null;
            List<FUNCTION> functions = new List<FUNCTION>();
            List<WordModel> word = new List<WordModel>();
            word = PLCH.Words.Where(x => x.Area == "EQUIPMENTFUNCTIONCHANGEEVENT").ToList();
            int i = 0;
            foreach (var item in EqpData.FUNCTIONSTATE.GetType().GetProperties())
            {
                i++;
                if (word.Any(x => x.Item == item.Name))
                {
                    WordModel w = (WordModel)word.FirstOrDefault(x => x.Item == item.Name);
                    var a = item.GetValue(EqpData.FUNCTIONSTATE, null) == null ? "" : item.GetValue(EqpData.FUNCTIONSTATE, null).ToString();
                    var v = w.GetValue();
                    
                        FUNCTION func = new FUNCTION();
                        func.BYWHO = word.FirstOrDefault(x => x.Item == "BYWHO").GetValue(PLC);
                        func.OLDEFST = a;
                        func.EFNAME = item.Name;
                        func.NEWEFST = w.GetValue();
                        func.EFID = i.ToString();
                        functions.Add(func);
                    
                }
            }
            EqpData.FUNCTION = functions;

        }
        public void ReadMaterial()
        {
            EqpData.MATERIALSTATES.Clear();
            List<MATERIALSTATE> materialstates = new List<MATERIALSTATE>();
            List<WordModel> word = new List<WordModel>();
            word = PLCH.Words.Where(x => x.Area == "MaterialPortState").ToList();
            for (var i = 1; i < 9; i++)
            {
                MATERIALSTATE materialstate = new MATERIALSTATE();
                materialstate.MATERIALTYPE = word.FirstOrDefault(x => x.Item == $"MaterialPortStsType{i}").GetValue(PLC);
                materialstate.MATERIALST = word.FirstOrDefault(x => x.Item == $"MaterialPortStsLST{i}").GetValue(PLC);
                materialstate.MATERIALPORTID = word.FirstOrDefault(x => x.Item == $"MaterialPortStsID{i}").GetValue(PLC);
                materialstate.MATERIALPORTLOADNO = word.FirstOrDefault(x => x.Item == $"MaterialPortStsLoaderNo{i}").GetValue(PLC);
                materialstate.MATERIALUSAGE = word.FirstOrDefault(x => x.Item == $"MaterialPortStsUsage{i}").GetValue(PLC);
                EqpData.MATERIALSTATES.Add(materialstate);
            }

        }
        public void PortState()
        {
            EqpData.PORTSTATES.Clear();
            List<PORTSTATE> portstates = new List<PORTSTATE>();
            List<WordModel> word = new List<WordModel>();
            word = PLCH.Words.Where(x => x.Area == "PortStatus").ToList();
            
            for (var i=1; i <5;i++)
            {
                
                PORTSTATE portstate = new PORTSTATE();
                portstate.PORTNO = word.FirstOrDefault(x => x.Item == $"PortNo{i}").GetValue();
                portstate.PORTAVAILABILITYSTATE = word.FirstOrDefault(x => x.Item == $"PortAvailstate{i}").GetValue();
                portstate.PORTACCESSMODE = word.FirstOrDefault(x => x.Item == $"PortAccessMode{i}").GetValue();
                portstate.PORTTRANSFERSTATE = word.FirstOrDefault(x => x.Item == $"PortTransferState{i}").GetValue();
                portstate.PORTPROCESSINGSTATE = word.FirstOrDefault(x => x.Item == $"PortProcessingState{i}").GetValue();
                EqpData.PORTSTATES.Add(portstate);
            }

        }
        public void ReadEqpState()
        {
            this.EqpData.ALS = _eqpConfig.PLCHelper.Alarms;
            EqpData.EQINFORMATION.EQPID = _eqpConfig.EQPID;
            WordModel crst = _eqpConfig.PLCHelper.Words.FirstOrDefault(x => x.Item == "CRST");

            EqpData.EQINFORMATION.CRST = _eqpConfig.CRST;
        }
        public void ReadUnitstate()
        {
            EqpData.UNITSTATES.Clear();
            List<EQPSTATE> unitstates = new List<EQPSTATE>();
            List<WordModel> word = new List<WordModel>();
            word = PLCH.Words.Where(x => x.Area == "UnitStatus").ToList();

            for (var i = 1; i < 9; i++)
            {
                EQPSTATE unitstate = new EQPSTATE();
                unitstate.AVAILABILITYSTATE = word.FirstOrDefault(x => x.Item == $"AVAILABILITYSTATE{i}").GetValue();
                unitstate.INTERLOCKSTATE = word.FirstOrDefault(x => x.Item == $"INTERLOCKSTATE{i}").GetValue();
                unitstate.MOVESTATE = word.FirstOrDefault(x => x.Item == $"MOVESTATE{i}").GetValue();
                unitstate.RUNSTATE = word.FirstOrDefault(x => x.Item == $"RUNSTATE{i}").GetValue();
                unitstate.FRONTSTATE = word.FirstOrDefault(x => x.Item == $"FRONTSTATE{i}").GetValue();
                unitstate.REARSTATE = word.FirstOrDefault(x => x.Item == $"REARSTATE{i}").GetValue();
                unitstate.PPSPLSTATE = word.FirstOrDefault(x => x.Item == $"PPSPLSTATE{i}").GetValue();
                EqpData.UNITSTATES.Add(unitstate);
            }
        }
        private void PlcConnectChangeEventHandle(bool isConnected)
        {
            var handle = PlcConnectChangeEvent;
            if (handle != null)
            {
                handle(isConnected);
            }
        }
        public void Alive()
        {
            bool plcAlive = false;
            int plcCount = 0;
            bool mcrConnect = false;
            bool isOn = false;
            Stopwatch aliveWatch = new Stopwatch();
            aliveWatch.Start();
            while (true)
            {
                if (_plc != null)
                {
                    if (_plc.IsOpen)
                    {
                        try
                        {
                            if (_plcH.Bits.Any(x => x.Item.ToUpper() == "ALIVE"))
                            {
                                if (aliveWatch.Elapsed.TotalMilliseconds >= int.Parse(_eqpConfig.AliveTime))
                                {
                                    isOn = !isOn;
                                    BitModel bitAlive = _plcH.Bits.FirstOrDefault(x => x.Item.ToUpper() == "ALIVE");
                                    bitAlive.SetPCValue = isOn;
                                    aliveWatch.Restart();
                                }
                                WordModel word = _plcH.Words.FirstOrDefault(x => x.Area == "DATETIMESET");
                                word.SetValue = DateTime.Now.ToString("yyyyMMddHHmmss");


                            }
                        }
                        catch (Exception ex)
                        {
                            var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                            LogTxt.Add(LogTxt.Type.Exception, debug);
                        }
                        if (plcAlive == _plcH.Bits.FirstOrDefault(x => x.Item == "ALIVE").GetPLCValue)
                        {
                            plcCount++;
                            if (plcCount > 100)
                            {
                                PlcConnectChangeEventHandle(false);
                                _isPlcConnected = false;
                            }

                        }
                        else
                        {
                            _isPlcConnected = true;
                            plcCount = 0;
                            plcAlive = _plcH.Bits.FirstOrDefault(x => x.Item == "ALIVE").GetPLCValue;
                            PlcConnectChangeEventHandle(true);
                        }
                    }
                    else PlcConnectChangeEventHandle(false);
                }

                Thread.Sleep(500);
            }
        }
    }
}
