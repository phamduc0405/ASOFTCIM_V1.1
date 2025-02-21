using A_SOFT.CMM.HELPER;
using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using ASOFTCIM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASOFTCIM.Helper
{
    public class PLCHelper
    {
        #region Enum

        #endregion
        #region Field
        private List<BitModel> _bit;
        private List<WordModel> _words;
        private List<PlcMemmory> _plcMemms;
        private PlcComm _plc;
        private List<Alarm> _alarms = new List<Alarm>();
        private List<FDCModel> _svids;
        private List<PPIDModel> _ppidParams;
        private List<PPIDModel> _lstPPID;
        private List<ECMModel> _ecms;
        private List<MaterialModel> _materials;
        private Thread _update;
        #endregion
        #region Property
        public List<BitModel> Bits
        {
            get { return _bit; }
            set { _bit = value; }
        }
        public List<WordModel> Words
        {
            get { return _words; }
            set { _words = value; }
        }
        public List<PlcMemmory> PlcMemms
        {
            get { return _plcMemms; }
            set { _plcMemms = value; }
        }
        public List<Alarm> Alarms
        {
            get { return _alarms; }
            set { _alarms = value; }
        }
        public string EqpId { get; set; } = null;
        #endregion
        #region Event
        public delegate void BitChangedEventDelegate(BitModel bit);
        public event BitChangedEventDelegate BitChangedEvent;
        public delegate void WordChangedEventDelegate(string Method, object data);
        public event WordChangedEventDelegate WordChangedEvent;
        #endregion
        #region Constructor

        public PLCHelper()
        {

        }
        #endregion

        #region Public Method
        public void LoadExcel(string ExcelPath)
        {
            Task.Run( async () =>
            {
                _bit = await ExcelHelper.ReadExcel<BitModel>(ExcelPath, "Local PLC Bit");
                _words = await ExcelHelper.ReadExcel<WordModel>(ExcelPath, "CIM->PLC Word(V1.21)");
                var wplc = await ExcelHelper.ReadExcel<WordModel>(ExcelPath, "PLC->CIM Word(V1.21)");
                _alarms = await ExcelHelper.ReadExcel<Alarm>(ExcelPath, "ALARM");
                _svids = await ExcelHelper.ReadExcel<FDCModel>(ExcelPath, "FDC");
                _ppidParams = await ExcelHelper.ReadExcel<PPIDModel>(ExcelPath, "RMS");
                _lstPPID = await ExcelHelper.ReadExcel<PPIDModel>(ExcelPath, "PPID");
                _ecms = await ExcelHelper.ReadExcel<ECMModel>(ExcelPath, "ECM");
                _words.AddRange(wplc);
                _plcMemms =await ExcelHelper.ReadExcel<PlcMemmory>(ExcelPath, "MemoryConfig");
                _materials = await ExcelHelper.ReadExcel<MaterialModel>(ExcelPath, "Material");

                foreach (var b in _bit)
                {
                    List<WordModel> wm= _words.Where(x=>x.BitEvent.Contains($"{b.PLCDevice}{b.PLCHexAdd}")).ToList();
                    b.LstWord.AddRange(wm);
                    List<MaterialModel> material = _materials.Where(x => x.BitEvent.Contains($"{b.PLCDevice}{b.PLCHexAdd}")).ToList();
                    b.LstWord.AddRange(material);
                  
                }
            }).GetAwaiter().GetResult();
           
        }


        public void Start(PlcComm plc, string eqp)
        {
            _plc = plc;
            EqpId = eqp;
            foreach (var b in _bit)
            {
                b.PLCs = _plc;
            }
            foreach (var w in Words)
            {
                w.PLCs = _plc;
            }

            _plc.BitChangedEvent -= PlcComm_BitChangedEvent;
            _plc.BitChangedEvent += PlcComm_BitChangedEvent;
            _plc.WordChangedEvent -= PlcComm_WordChangedEvent;
            _plc.WordChangedEvent += PlcComm_WordChangedEvent;
        }


        private void PlcComm_WordChangedEvent(MelsecIF.WordStatus status)
        {
            try
            {
                Task.Run(() =>
                {
                    foreach (var w in _words)
                    {
                        if (status.Address <= (w.Address + w.Length - 1) && status.Address >= w.Address)
                        {
                            switch (w.Area.ToUpper())
                            {
                                case string a when a.Contains("ALARM"):
                                    WordChangedEventHandle("ALARMREPORT", status);
                                    break;
                                case string a when a.Contains("EQSTATUS"):
                                    WordChangedEventHandle(w.Area, _words.Where(x => x.Area == w.Area).ToList());
                                    break;
                                case string a when a.Contains("FDC"):
                                    WordChangedEventHandle(w.Area, _svids);
                                    break;
                                case string a when a.Contains("UNITSTATUS"):
                                    WordChangedEventHandle("UNITSTATUS", _words.Where(x => x.Area == w.Area).ToList());
                                    break;
                                case string a when a.Contains("MATERIALPORTSTATE"):
                                    WordChangedEventHandle("MATERIALPORTSTATE", _words.Where(x => x.Area == w.Area).ToList());
                                    break;
                                case string a when a.Contains("PORTSTATUS"):
                                    WordChangedEventHandle("PORTSTATUS", _words.Where(x => x.Area == w.Area).ToList());
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }

        public BitModel GetBitName(MelsecIF.BitStatus bitstatus)
        {
            BitModel bit = new BitModel(_plc);
            if (_bit.Any(x => x.PLCAddress == bitstatus.Address))
            {
                bit = _bit.FirstOrDefault(x => x.PLCAddress == bitstatus.Address);
            }
            return bit;
        }
        public void Close()
        {
            _update?.Abort();
        }
        #endregion

        #region Private Method
        private void PlcComm_BitChangedEvent(MelsecIF.BitStatus status)
        {

            BitModel bit = new BitModel(_plc);

            if (_bit.Any(x => x.PLCAddress == status.Address))
            {
                bit = _bit.FirstOrDefault(x => x.PLCAddress == status.Address);
                bit.BitChangeByPlc();
                if (bit.Type == "Event")
                {
                    if (!status.IsOn)
                    {
                        bit.SetPCValue = false;
                        return;
                    }
                    MakeLogBit(false, bit, status.IsOn);
                    BitChangedEventHandle(bit);
                }
                if (!status.IsOn) return;
                if (bit.Type == "Command")
                {
                    string name = bit.Item.Trim() + "CONFIRM";
                    MakeLogBit(true, bit, status.IsOn);
                    bit.SetPCValue = false;
                }
                else return;
            }
        }

        private void MakeLogBit(bool isSend, BitModel bit, bool value)
        {
            StringBuilder strlog = new StringBuilder();
            string str = string.Format("[{0}] [Bit]: Name:{1} Address:{2} Value:{3}", isSend ? "SEND" : "RECV", bit.Item, bit.PLCHexAdd, value.ToString());
            strlog.Append(str);
           // List<WordModel> words = Words.Where(x => x.Area == bit.LinkWord).ToList();
            foreach (var item in bit.LstWord)
            {
                strlog.Append("\n\t");
                str = string.Format("Address:{0}{1} Value:{2}",_plc.WordDevice, item.Start, item.GetValue(_plc));
                strlog.Append(str);
            }
            LogTxt.Add(LogTxt.Type.PLCMess, strlog.ToString(), EqpId);
        }
        #endregion
        #region EventHandle
        private void BitChangedEventHandle(BitModel bit)
        {
            var handle = BitChangedEvent;
            if (handle != null)
            {
                handle(bit);
            }
        }
        private void WordChangedEventHandle(string FuncName, object data)
        {
            var handle = WordChangedEvent;
            if (handle != null)
            {
                handle(FuncName, data);
            }
        }

        public PLCHelper Copy()
        {
            // Tạo đối tượng PLCHelper mới
            PLCHelper copy = new PLCHelper();

            // Sao chép các thuộc tính
            copy.EqpId = this.EqpId;

            if (this.Bits != null)
            {
                copy.Bits = new List<BitModel>();
                foreach (var bit in this.Bits)
                {
                    copy.Bits.Add(bit.Copy());
                }
            }

            if (this.Words != null)
            {
                copy.Words = new List<WordModel>();
                foreach (var word in this.Words)
                {
                    copy.Words.Add(word.Copy());
                }
            }

            if (this.PlcMemms != null)
            {
                copy.PlcMemms = new List<PlcMemmory>();
                foreach (var plcMem in this.PlcMemms)
                {
                    copy.PlcMemms.Add(plcMem.Copy<PlcMemmory>());
                }
            }

           

            return copy;
        }
        #endregion
    }
}
