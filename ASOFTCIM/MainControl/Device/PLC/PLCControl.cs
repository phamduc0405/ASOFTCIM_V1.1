﻿using A_SOFT.CMM.INIT;
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

namespace ASOFTCIM
{
    public partial class ACIM
    {
        private Thread _aliveBit;
        private bool _isPlcConnected;

        public delegate void PlcConnectChangeEventDelegate(bool isConnected);
        public event PlcConnectChangeEventDelegate PlcConnectChangeEvent;


        public void InitialPlc()
{
    if (_eqpConfig.PLCConfig != null)
    {
        Task.Run(async () =>
        {
            try
            {
                // Khởi tạo đối tượng PLC
                _plc = new PlcComm();
                _eqpConfig.PLCConfig.PlcConnectType = PlcConnectType.Component;
                _eqpConfig.PLCConfig.StationNo = 255;
                _plc.ConfigComm(_eqpConfig.PLCConfig);
                _plc.Start();
                
                _plcH = new PLCHelper();
                _plcH = _eqpConfig.PLCHelper;
                _plcH.Start(_plc, _eqpConfig.EQPID);
                
                _aliveBit = new Thread(Alive)
                {
                    IsBackground = true
                };
                _aliveBit.Start();
                
                // Đọc dữ liệu từ PLC
                ReadEqpState();
                ReadRMS();
                ReadECM();
                ReadAPC();
                
                // Gán sự kiện thay đổi trạng thái bit
                _plcH.BitChangedEvent += (bit) =>
                {
                    PLCBitChange(bit.Comment, bit);
                };
                
                _plcH.WordChangedEvent += _plcH_WordChangedEvent;
                
                // Kiểm tra trạng thái các từ đầu vào của PLC
                foreach (var item in _plc.InputWordStatuses)
                {
                    WordStatus w = item;
                    WordModel word = _plcH.Words.FirstOrDefault(x => x.Item.ToUpper() == "ALARM" && x.IsPlc);
                    
                    if (word != null && w.Address >= word.Address && w.Address < word.Address + word.Length)
                    {
                        if (!w.IsOn)
                        {
                            var alid = w.Index - word.Address * 16 + 2;
                        }
                    }
                }
                
                // Gán danh sách báo động
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
        public void LoadExcelConfig(string path)
        {
            try
            {
                if (_eqpConfig == null)
                {
                    _eqpConfig = new Config.EquipmentConfig();
                }
                if (File.Exists(path))
                {
                    _eqpConfig.PLCHelper.LoadExcel(path);
                }
                if (_eqpConfig.PLCHelper.PlcMemms?.Count > 0)
                {
                    if (_eqpConfig.PLCHelper.Bits.Any(x => x.Item.ToUpper().Contains("ALIVE")))
                    {
                        BitModel bAlive = _eqpConfig.PLCHelper.Bits.FirstOrDefault(x => x.Item.ToUpper().Contains("ALIVE"));
                        if (_eqpConfig.PLCHelper.PlcMemms.Any(x => x.BPLCStart == bAlive.PLCHexAdd))
                        {
                            PlcMemmory plcmem = _eqpConfig.PLCHelper.PlcMemms.FirstOrDefault(x => x.BPLCStart == bAlive.PLCHexAdd);

                            _eqpConfig.PLCConfig.ReadStartBitAddress = plcmem.BPLCStart;
                            _eqpConfig.PLCConfig.SizeReadBit = int.Parse(plcmem.BPLCPoints);
                            _eqpConfig.PLCConfig.ReadStartWordAddress = plcmem.WPLCStart;
                            _eqpConfig.PLCConfig.SizeReadWord = int.Parse(plcmem.WPLCPoints);

                            _eqpConfig.PLCConfig.WriteStartBitAddress = plcmem.BPCStart;
                            _eqpConfig.PLCConfig.SizeWriteBit = int.Parse(plcmem.BPCPoints);
                            _eqpConfig.PLCConfig.WriteStartWordAddress = plcmem.WPCStart;
                            _eqpConfig.PLCConfig.SizeWriteWord = int.Parse(plcmem.WPCPoints);

                            _eqpConfig.PLCConfig.BitDevice = plcmem.BitDevice;
                            _eqpConfig.PLCConfig.WordDevice = plcmem.WordDevice;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
        }
        private void _plcH_WordChangedEvent(string Method, object data)
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
                new ALARMREPORT().Excute(this, data);
            }
            if (new[] { "UNITSTATUS", "MATERIALPORTSTATE", "PORTSTATUS" }.Any(Method.Contains))
            {
                List<WordModel> word = (List<WordModel>)data;
                string unit = word[0].Area;
                if (_unitUpdate.Contains(unit)) return;
                _unitUpdate.Add(unit);
                Thread.Sleep(100);
                PLCWordChange(Method, data);
                _unitUpdate.Remove(unit);
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
                    sv.SVVALUE = item.GetValue(_plc);
                }
                else
                {
                    SV sv = new SV();
                    sv.SVID = item.SVID;
                    sv.SVNAME = item.NAME;
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
            foreach (var ppid in _eqpConfig.PLCHelper.ListPPID)
            {
                if(!string.IsNullOrEmpty(ppid.GetValue(this.PLC)))
                {
                    this.EqpData.PPIDList.PPID.Add(ppid.Item);
                }    
            }
            List<PPIDModel> word = this.PLCH.PPIDParams.ToList();
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
            this.EqpData.CurrPPID.COMMANDCODEs.Add(commandcode);
        }
        public void ReadECM()
        {
            foreach(var ecm in _eqpConfig.PLCHelper.ECMS)
            {
                if (ecm.ECNAME != "RESERVED")
                {
                    EC ec = new EC();
                    ec.ECNAME = ecm.ECNAME;
                    ec.ECID = ecm.ECID;
                    ec.ECDEF = ecm.GetValue(this.PLC);
                    this.EqpData.ECS.Add(ec);
                }
            }
        }
        public void ReadAPC()
        {
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
        public void ReadEqpState()
        {
            this.EqpData.ALS = _eqpConfig.PLCHelper.Alarms;
            EqpData.EQINFORMATION.EQPID = _eqpConfig.EQPID;
            WordModel crst = _eqpConfig.PLCHelper.Words.FirstOrDefault(x => x.Item == "CRST");

            EqpData.EQINFORMATION.CRST = _eqpConfig.CRST;
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

            while (true)
            {
                if (_plc != null)
                {
                    if (_plc.IsOpen)
                    {
                        try
                        {
                            isOn = !isOn;
                            if (_plcH.Bits.Any(x => x.Item.ToUpper() == "ALIVE"))
                            {
                                BitModel bitAlive = _plcH.Bits.FirstOrDefault(x => x.Item.ToUpper() == "ALIVE");
                                bitAlive.SetPCValue = isOn;
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
