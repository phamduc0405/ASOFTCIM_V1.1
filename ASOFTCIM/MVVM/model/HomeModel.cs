using ASOFTCIM.MainControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace ASOFTCIM.MVVM.Model
{
    public class HomeModel : INotifyPropertyChanged
    {
        #region Fields
        private Controller _controller;
        private Thread _updateData;
        private static bool _running = true;
        private bool _disposed = false;
        private UIElement _cpuChart;

        #region EQP
        private string _eQPID;
        private string _eQPName;
        private string _pPID;
        private string _pLCConnect;

        #endregion
        #region HOST
        private string _iP;
        private string _port;
        private string _state;
        private string _cimConnect;
        #endregion
        #region EQPSTATE
        private string _availabilitystate;
        private string _interlockstate;
        private string _runstate;
        private string _fronstate;
        private string _rearstate;
        private string _movesstate;
        #endregion
        #region ALARM
        private ObservableCollection<Data.Alarm> _alarmList = new ObservableCollection<Data.Alarm>();

        #endregion
        #region MESSAGE
        public string _messageCim2EQP;
        public string _messageCim2Host;

        #endregion
        #endregion
        #region Properties
        public UIElement CpuChart
        {
            get => _cpuChart;
            set { _cpuChart = value; OnPropertyChanged(nameof(CpuChart)); }
        }
        #region EQP
        public string EQPID
        {
            get { return _eQPID; }
            set
            {
                _eQPID = value;
                OnPropertyChanged(nameof(EQPID));
            }
        }
        public string EQPNAME
        {
            get { return _eQPName; }
            set
            {
                _eQPName = value;
                OnPropertyChanged(nameof(EQPNAME));
            }
        }
        public string PPID
        {
            get { return _pPID; }
            set
            {
                _pPID = value;
                OnPropertyChanged(nameof(PPID));
            }
        }
        public string PLCConnect
        {
            get { return _pLCConnect; }
            set
            {
                _pLCConnect = value;
                OnPropertyChanged(nameof(PLCConnect));
            }
        }
        #endregion
        #region HOST
        public string IP
        {
            get { return _iP; }
            set
            {
                _iP = value;
                OnPropertyChanged(nameof(IP));
            }
        }
        public string PORT
        {
            get { return _port; }
            set
            {
                _port = value;
                OnPropertyChanged(nameof(PORT));
            }
        }
        public string CimConnect
        {
            get { return _cimConnect; }
            set
            {
                _cimConnect = value;
                OnPropertyChanged(nameof(CimConnect));
            }
        }
        public string STATE
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged(nameof(STATE));
            }
        }
        #endregion
        #region EQPSTATE

        public string AVAILABILITYSTATE
        {
            get { return _availabilitystate; }
            set
            {
                _availabilitystate = value;
                OnPropertyChanged(nameof(AVAILABILITYSTATE));
            }
        }
        public string INTERLOCKSTATE
        {
            get { return _interlockstate; }
            set
            {
                _interlockstate = value;
                OnPropertyChanged(nameof(INTERLOCKSTATE));
            }
        }
        public string RUNSTATE
        {
            get { return _runstate; }
            set
            {
                _runstate = value;
                OnPropertyChanged(nameof(RUNSTATE));
            }
        }
        public string FRONTSTATE
        {
            get { return _fronstate; }
            set
            {
                _fronstate = value;
                OnPropertyChanged(nameof(FRONTSTATE));
            }
        }
        public string REARSTATE
        {
            get { return _rearstate; }
            set
            {
                _rearstate = value;
                OnPropertyChanged(nameof(REARSTATE));
            }
        }
        public string MOVESTATE
        {
            get { return _movesstate; }
            set
            {
                _movesstate = value;
                OnPropertyChanged(nameof(MOVESTATE));
            }
        }
        #endregion
        #region ALARM
        public ObservableCollection<Data.Alarm> AlarmList
        {
            get => _alarmList;
            set
            {
                _alarmList = value;
                OnPropertyChanged(nameof(AlarmList));
            }
        }
        #endregion
        #region MESSAGE
        public string MessageCIM2EQP
        {
            get { return _messageCim2EQP; }
            set
            {
                _messageCim2EQP = value;
                OnPropertyChanged(nameof(MessageCIM2EQP));
            }
        }
        public string MessageCIM2HOST
        {
            get { return _messageCim2Host; }
            set
            {
                _messageCim2Host = value;
                OnPropertyChanged(nameof(MessageCIM2HOST));
            }
        }
        #endregion
        public ICommand ClearMessageCim2Host { get; }
        public ICommand ClearMessageCim2Eqp { get; }
        public ICommand Unloaded { get; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
