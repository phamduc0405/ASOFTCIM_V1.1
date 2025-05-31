using A_SOFT.CMM.HELPER;
using A_SOFT.CMM.INIT;
using A_SOFT.Ctl.Mitsu.Model;
using A_SOFT.PLC;
using ASOFTCIM.Config;
using ASOFTCIM.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        private List<APCModel> _apc;
        private List<CarialModel> _carrial;
        private Thread _update;
        private Thread _wordReaderThread;
        private Thread _alarmReaderThread;
        private bool _isReadingWord = false;
        private bool _isReadingAlarm = false;
        public MelsecIF.WordStatus[] als;
        private EquipmentConfig _equipmentConfig;
        private bool _isLogPLC;
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
        public List<PPIDModel> ListPPID
        {
            get { return _lstPPID; }
            set { _lstPPID = value; }
        }
        public List<PPIDModel> PPIDParams
        {
            get { return _ppidParams; }
            set { _ppidParams = value; }
        }
        public List<MaterialModel> Materrials
        {
            get { return _materials; }
            set { _materials = value; }
        }
        public List<ECMModel> ECMS
        {
            get { return _ecms; }
            set { _ecms = value; }
        }
        public List<FDCModel> SVIDS
        {
            get { return _svids; }
            set { _svids = value; }
        }
        public List<APCModel> APCS
        {
            get { return _apc; }
            set { _apc = value; }
        }
        public List<CarialModel> Carrial
        {
            get { return _carrial; }
            set { _carrial = value; }
        }
        public bool IsLogPLC
        {
            get { return _isLogPLC; }
            set { _isLogPLC = value; }
        }
        public string EqpId { get; set; } = null;
        public bool TestAlarm { get; set; } = false; 
        #endregion
        #region Event
        public delegate void BitChangedEventDelegate(BitModel bit);
        public event BitChangedEventDelegate BitChangedEvent;
        public delegate void WordChangedEventDelegate(string Method, object data);
        public event WordChangedEventDelegate WordChangedEvent;
        public delegate void AlarmEventDelegate(string Method, WordModel wordModel);
        public event AlarmEventDelegate AlarmChangedEvent;
        #endregion
        #region Constructor
        public StopWatch stopWatch;
        public StopWatch stopWatch1;
        public PLCHelper()
        {
            stopWatch = new StopWatch();
            stopWatch1 = new StopWatch();
        }
        #endregion

        #region Public Method
        public async Task LoadExcel(string ExcelPath, List<string> sheets)
        {
            if (_plc != null)
            {
                _plc.BitChangedEvent -= PlcComm_BitChangedEvent;
                //_plc.WordChangedEvent -= PlcComm_WordChangedEvent;
            }
            Task.Run(async () =>
            {
                if (sheets.Any(x => x == "Bits"))
                {
                    _bit = await ExcelHelper.ReadExcel<BitModel>(ExcelPath, "Local PLC Bit");
                }
                if (sheets.Any(x => x == "Words"))
                {
                    _bit = await ExcelHelper.ReadExcel<BitModel>(ExcelPath, "Local PLC Bit");
                    _words = await ExcelHelper.ReadExcel<WordModel>(ExcelPath, "CIM->PLC Word(V1.21)");
                    var wplc = await ExcelHelper.ReadExcel<WordModel>(ExcelPath, "PLC->CIM Word(V1.21)");
                    _words.AddRange(wplc);
                    foreach (var b in _bit)
                    {
                        List<WordModel> wm = _words.Where(x => x.BitEvent.Contains($"{b.PLCDevice}{b.PLCHexAdd}")).ToList();
                        b.LstWord.AddRange(wm);
                    }
                }
                if (sheets.Any(x => x == "PlcMemms"))
                {
                    _plcMemms = await ExcelHelper.ReadExcel<PlcMemmory>(ExcelPath, "MemoryConfig");
                }
                if (sheets.Any(x => x == "Alarms"))
                {
                    _alarms = await ExcelHelper.ReadExcel<Alarm>(ExcelPath, "ALARM");
                }
                if (sheets.Any(x => x == "ListPPID"))
                {
                    _lstPPID = await ExcelHelper.ReadExcel<PPIDModel>(ExcelPath, "PPID");
                }
                if (sheets.Any(x => x == "PPIDParams"))
                {
                    _ppidParams = await ExcelHelper.ReadExcel<PPIDModel>(ExcelPath, "RMS");
                }
                if (sheets.Any(x => x == "Materrials"))
                {
                    _bit = await ExcelHelper.ReadExcel<BitModel>(ExcelPath, "Local PLC Bit");
                    _materials = await ExcelHelper.ReadExcel<MaterialModel>(ExcelPath, "Material");
                    foreach (var b in _bit)
                    {
                        List<MaterialModel> material = _materials.Where(x => x.BitEvent.Contains($"{b.PLCDevice}{b.PLCHexAdd}")).ToList();
                        b.LstWord.AddRange(material);
                    }
                }
                if (sheets.Any(x => x == "ECMS"))
                {
                    _ecms = await ExcelHelper.ReadExcel<ECMModel>(ExcelPath, "ECM");
                }
                if (sheets.Any(x => x == "SVIDS"))
                {
                    _svids = await ExcelHelper.ReadExcel<FDCModel>(ExcelPath, "FDC");
                }
                if (sheets.Any(x => x == "APCS"))
                {
                    _apc = await ExcelHelper.ReadExcel<APCModel>(ExcelPath, "APC");
                }
                if (sheets.Any(x => x == "Carial"))
                {
                    _carrial = await ExcelHelper.ReadExcel<CarialModel>(ExcelPath, "Cassette Batch");
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


            Task.Run(() =>
            {
               Task.Delay(2000).Wait(); 
                StartWordReader();
               StartAlarmReader();
            });
           

        }

        private void Al_BitChangedEvent(MelsecIF.WordStatus status)
        {
            WordChangedEventHandle("ALARMREPORT", status);
        }

        private void StartWordReader()
        {
            if (_wordReaderThread != null && _wordReaderThread.IsAlive)
                return;

            _isReadingWord = true;
            _wordReaderThread = new Thread(() => WordReader());
            _wordReaderThread.IsBackground = true;
            _wordReaderThread.Start();
        }
        private void StartAlarmReader()
        {
            if (_alarmReaderThread != null && _alarmReaderThread.IsAlive)
                return;

            _isReadingAlarm = true;
            _alarmReaderThread = new Thread(() => AlarmReader());
            _alarmReaderThread.IsBackground = true;
            _alarmReaderThread.Start();
        }
        private void WordReader()
        {
           
            while (_isReadingWord)
            {
                try
                {

                    var groups = _words.Where(w => w.Area.Equals("EQPSTATUS", StringComparison.OrdinalIgnoreCase)
                         || w.Area.Equals("FDC", StringComparison.OrdinalIgnoreCase))
                         .GroupBy(w => w.Area.ToUpper());
                    foreach (var group in groups)
                    {
                        string area = group.Key;

                        if (area == "EQPSTATUS")
                        {
                            WordChangedEventHandle(area, group.ToList());
                        }
                        else if (area == "FDC")
                        {
                            WordChangedEventHandle(area, _svids);
                        }
                    }

                }
                catch (Exception ex)
                {
                    LogTxt.Add(LogTxt.Type.Exception, $"Class:{this.GetType().Name} Method:{MethodBase.GetCurrentMethod().Name} exception: {ex.Message}");
                }

                Thread.Sleep(100);
            }
        }
        private void AlarmReader()
        {
            WordModel wAlam = _words.FirstOrDefault(x => x.Area.IndexOf("ALARM", StringComparison.OrdinalIgnoreCase) >= 0);
            int startAddr = wAlam.Address;
            int endAddr = wAlam.Address + wAlam.Length - 1;

            var _wordStatusMap = _plc.InputWordStatuses.Where(x => x.Address >= startAddr && x.Address <= endAddr).GroupBy(x => x.Address).ToDictionary(g => g.Key, g => g.ToList());
            while (_isReadingAlarm)
            {
                try
                {
                    for (int addr = startAddr; addr <= endAddr; addr++)
                    {
                        if (_wordStatusMap.TryGetValue(addr, out var wordList))
                        {
                            foreach (var bit in wordList)
                            {
                                if (bit.IsChanged)
                                {
                                    Al_BitChangedEvent(bit);
                                    if (!TestAlarm)
                                    {
                                        bit.ReflectionCompleted();
                                    }

                                }
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    LogTxt.Add(LogTxt.Type.Exception, $"Class:{this.GetType().Name} Method:{MethodBase.GetCurrentMethod().Name} exception: {ex.Message}");
                }

                Thread.Sleep(100);
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
            try
            {
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
                        if (_isLogPLC)
                        {
                            MakeLogBit(false, bit, status.IsOn);
                        }
                        
                        BitChangedEventHandle(bit);
                    } 
                    if (!status.IsOn) return;
                    if (bit.Type == "Command")
                    {
                        string name = bit.Item.Trim() + "CONFIRM";
                        if (_isLogPLC)
                        {
                            MakeLogBit(true, bit, status.IsOn);
                        }
                        
                        bit.SetPCValue = false;
                    }
                    else return;
                }
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
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
                str = string.Format("Address:{0}{1} Value:{2}", _plc.WordDevice, item.Start, item.GetValue(_plc));
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
        private void AlarmChangedEventHandle(string FuncName, WordModel wordModel)
        {
            var handle = AlarmChangedEvent;
            if (handle != null)
            {
                handle(FuncName, wordModel);
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
