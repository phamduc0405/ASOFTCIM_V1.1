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

namespace ASOFTCIM
{
    public partial class ACIM
    {

        public void InitialPlc()
        {
            if (_eqpConfig.PLCConfig != null)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(1000);
                    _plc = new PlcComm();
                    _eqpConfig.PLCConfig.PlcConnectType = PlcConnectType.Component;
                    _eqpConfig.PLCConfig.StationNo = 255;
                    _plc.ConfigComm(_eqpConfig.PLCConfig);
                    _plc.Start();
                    _plcH = new PLCHelper();
                    _plcH = _eqpConfig.PLCHelper;
                    _plcH.Start(_plc, _eqpConfig.EQPID);
                    //_aliveBit = new Thread(Alive)
                    //{
                    //    IsBackground = true
                    //};
                    //_aliveBit.Start();
                    //DefineAlarm();
                    _plcH.BitChangedEvent += (bit) =>
                    {
                        PLCBitChange(bit.Item, bit);
                    };
                    _plcH.WordChangedEvent += _plcH_WordChangedEvent;
                    foreach (var item in _plc.InputWordStatuses)
                    {
                        WordStatus w = item;
                        WordModel word = _plcH.Words.FirstOrDefault(x => x.Item.ToUpper() == "ALARM" && x.IsPlc);
                        if (w.Address >= word.Address && w.Address < word.Address + word.Length)
                        {
                            if (!w.IsOn)
                            {
                                var alid = w.Index - word.Address * 16 + 2;


                            }
                        }
                        // w.BitChangedEvent += W_BitChangedEvent;
                    }
                });

                //_alarm.HistoryReset(_eqpConfig.EQPID);
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
                    //     await _controller.Eqps.FirstOrDefault(x => x.EqpID == _eqpConfig.EQPID).SavePlcData();
                    //  DisplaySavePlcConfig display = new DisplaySavePlcConfig(txtPathPlcExcel.Text);
                    //  display.ShowDialog();
                }
                if (_eqpConfig.PLCHelper.PlcMemms?.Count > 0)
                {
                    if (_eqpConfig.PLCHelper.Bits.Any(x => x.Item.Contains("ALIVE")))
                    {
                        BitModel bAlive = _eqpConfig.PLCHelper.Bits.FirstOrDefault(x => x.Item.Contains("ALIVE"));
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
                //  _controller.DisplayMessage(false, "Check Input Type!");
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
        }
        private void _plcH_WordChangedEvent(string Method, object data)
        {
            if (Method.Contains("EQSTATUS"))
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
                    MethodInfo method = this.GetType().GetMethod($"Excute");
                    if (method != null)
                    {
                        object result = method.Invoke(this, new object[] { this, w });
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
                        MethodInfo method = t.GetType().GetMethod($"Excute");
                        if (method != null)
                        {
                            object result = method.Invoke(this, new object[] { this, bit });

                        }
                        bit.SetPCValue = true;
                        return;
                    }

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

    }
}
