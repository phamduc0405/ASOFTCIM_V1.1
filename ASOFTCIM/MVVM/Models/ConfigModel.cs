using A_SOFT.PLC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ASOFTCIM.MVVM.Models
{
    public class ConfigModel : INotifyPropertyChanged
    {
        #region Fields
        #region PLC
        private bool _userCCLinkIE;
        private PlcConnectType _plcConnectType;
        public List<PlcConnectType> PlcConnectTypes => Enum.GetValues(typeof(PlcConnectType)).Cast<PlcConnectType>().ToList();
        private string _plcIP;
        private string _plcPort;
        private string _plcStation;
        private string _plcPath;
        private string _plcNetWork;
        private string _plcChannel;
        private string _plcTimeAlive;
        private string _plcStartInputBit;
        private string _plcStartInputBitLength;
        private string _plcStartInputWord;
        private string _plcStartInputWordLength;
        private string _plcStartOutputBit;
        private string _plcStartOutputBitLength;
        private string _plcStartOutputWord;
        private string _plcStartOutputWordLength;
        #endregion
        #region EQP
        private string _equipMent;
        private string _logFolder;
        private string _sizeFile = "10"; // MB
        private string _autoDeleteLog;
        private string _aliveTime;
        private bool _useLogFDC;
        private string _logFodlerFDC;
        #endregion
        #region CIM
        private string _ip;
        private string _connectMode;
        private string _port;
        #endregion
        private string _pathMapInterface;

        #endregion
        #region Properties
        #region PLC
        public bool UserCCLinkIE
        {
            get { return _userCCLinkIE; }
            set
            {
                _userCCLinkIE = value;
                OnPropertyChanged(nameof(UserCCLinkIE));
            }
        }
        public PlcConnectType PlcConnectType
        {
            get { return _plcConnectType; }
            set
            {
                _plcConnectType = value;
                OnPropertyChanged(nameof(PlcConnectType));
            }
        }

        public string PlcIP
        {
            get { return _plcIP; }
            set
            {
                _plcIP = value;
                OnPropertyChanged(nameof(PlcIP));
            }
        }
        public string PlcPort
        {
            get { return _plcPort; }
            set
            {
                _plcPort = value;
                OnPropertyChanged(nameof(PlcPort));
            }
        }
        public string PlcStation
        {
            get { return _plcStation; }
            set
            {
                _plcStation = value;
                OnPropertyChanged(nameof(PlcStation));
            }
        }
        public string plcPath
        {
            get { return _plcPath; }
            set
            {
                _plcPath = value;
                OnPropertyChanged(nameof(plcPath));
            }
        }
        public string PlcNetWork
        {
            get { return _plcNetWork; }
            set
            {
                _plcNetWork = value;
                OnPropertyChanged(nameof(PlcNetWork));
            }
        }
        public string PlcChannel
        {
            get { return _plcChannel; }
            set
            {
                _plcChannel = value;
                OnPropertyChanged(nameof(PlcChannel));
            }
        }
        public string PlcTimeAlive
        {
            get { return _plcTimeAlive; }
            set
            {
                _plcTimeAlive = value;
                OnPropertyChanged(nameof(PlcTimeAlive));
            }
        }
        public string PlcStartInputBit
        {
            get { return _plcStartInputBit; }
            set
            {
                _plcStartInputBit = value;
                OnPropertyChanged(nameof(PlcStartInputBit));
            }
        }
        public string PlcStartInputBitLength
        {
            get { return _plcStartInputBitLength; }
            set
            {
                _plcStartInputBitLength = value;
                OnPropertyChanged(nameof(PlcStartInputBitLength));
            }
        }
        public string PlcStartInputWord
        {
            get { return _plcStartInputWord; }
            set
            {
                _plcStartInputWord = value;
                OnPropertyChanged(nameof(PlcStartInputWord));
            }
        }
        public string PlcStartInputWordLength
        {
            get { return _plcStartInputWordLength; }
            set
            {
                _plcStartInputWordLength = value;
                OnPropertyChanged(nameof(PlcStartInputWordLength));
            }
        }
        public string PlcStartOutputBit
        {
            get { return _plcStartOutputBit; }
            set
            {
                _plcStartOutputBit = value;
                OnPropertyChanged(nameof(PlcStartOutputBit));
            }
        }
        public string PlcStartOutputBitLength
        {
            get { return _plcStartOutputBitLength; }
            set
            {
                _plcStartOutputBitLength = value;
                OnPropertyChanged(nameof(PlcStartOutputBitLength));
            }
        }
        public string PlcStartOutputWord
        {
            get { return _plcStartOutputWord; }
            set
            {
                _plcStartOutputWord = value;
                OnPropertyChanged(nameof(PlcStartOutputWord));
            }
        }
        public string PlcStartOutputWordLength
        {
            get { return _plcStartOutputWordLength; }
            set
            {
                _plcStartOutputWordLength = value;
                OnPropertyChanged(nameof(PlcStartOutputWordLength));
            }
        }
        #endregion
        #region EQP
        public string EquipMent
        {
            get { return _equipMent; }
            set
            {
                _equipMent = value;
                OnPropertyChanged(nameof(EquipMent));
            }
        }
        public string LogFolder
        {
            get { return _logFolder; }
            set
            {
                _logFolder = value;
                OnPropertyChanged(nameof(LogFolder));
            }
        }
        public string SizeFile
        {
            get { return _sizeFile; }
            set
            {
                _sizeFile = value;
                OnPropertyChanged(nameof(SizeFile));
            }
        }
        public string AutoDeleteLog
        {
            get { return _autoDeleteLog; }
            set
            {
                _autoDeleteLog = value;
                OnPropertyChanged(nameof(AutoDeleteLog));
            }
        }
        public string AliveTime
        {
            get { return _aliveTime; }
            set
            {
                _aliveTime = value;
                OnPropertyChanged(nameof(AliveTime));
            }
        }
        public bool UseLogFDC
        {
            get { return _useLogFDC; }
            set
            {
                _useLogFDC = value;
                OnPropertyChanged(nameof(UseLogFDC));
            }
        }
        public string LogFodlerFDC
        {
            get { return _logFodlerFDC; }
            set
            {
                _logFodlerFDC = value;
                OnPropertyChanged(nameof(LogFodlerFDC));
            }
        }
        #endregion
        #region CIM
        public string Ip
        {
            get { return _ip; }
            set
            {
                _ip = value;
                OnPropertyChanged(nameof(Ip));
            }
        }
        public string ConnectMode
        {
            get { return _connectMode; }
            set
            {
                _connectMode = value;
                OnPropertyChanged(nameof(ConnectMode));
            }
        }
        public string Port
        {
            get { return _port; }
            set
            {
                _port = value;
                OnPropertyChanged(nameof(Port));
            }
        }



        #endregion
        public string PathMapInterface
        {
            get { return _pathMapInterface; }
            set
            {
                _pathMapInterface = value;
                OnPropertyChanged(nameof(PathMapInterface));
            }
        }
        public ICommand SavePLCConfig { get; }
        public ICommand SaveEQPConfig { get; }
        public ICommand SaveCimConfig { get; }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
