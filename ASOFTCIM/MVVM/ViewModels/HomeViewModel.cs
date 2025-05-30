
using System;
using ASOFTCIM.Init;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ASOFTCIM.MainControl;
using System.Collections.ObjectModel;
using ASOFTCIM.MVVM.Models;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Threading;
using A_SOFT.CMM.INIT;
using System.Reflection;
using System.Windows.Media;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;
using System.Windows.Interop;
using System.Windows.Input;
using ASOFTCIM.MVVM.Views.Home;
using ASOFTCIM.MVVM.ViewModels;

namespace ASOFTCIM.MVVM.ViewModels
{
    public class HomeViewModel : BaseViewModels
    {
        #region Fields
        private readonly Controller _controller;
        private HomeModel _home;
        private static bool _running = true;

        private PartialCpuChart _cpuChart;

        // Biến tạm lưu message
        private string _pendingHostMessage;
        private string _pendingPlcMessage;
        // DispatcherTimer for Alarm Update
        private readonly DispatcherTimer _alarmUpdateTimer;
        private bool _alarmUpdatePending = false;
        #endregion

        #region Properties
        public PartialCpuChart CpuChart
        {
            get => _cpuChart;
            set { _cpuChart = value; OnPropertyChanged(nameof(CpuChart)); }
        }

        public static bool Running => _running;

        public HomeModel Home
        {
            get => _home;
            private set { _home = value; }
        }

        public ICommand ClearMessageCim2Host { get; }
        public ICommand ClearMessageCim2Eqp { get; }
        public ICommand Unloaded { get; }
        #endregion

        #region Constructor
        public HomeViewModel()
        {
            _controller = MainWindowViewModel.Controller;
            _home = new HomeModel();
            _home.EQPID = _controller.EquipmentConfig.EQPID;
            _running = true;
            CpuChart = new PartialCpuChart();

            LoadConfig();
            Controller_CimConnectChangeEvent(_controller.CimConnect);
            

            ClearMessageCim2Host = new RelayCommand(_ =>
            {
                _home.MessageCIM2HOST = null;
                _home._messageCim2Host = null;
            });

            ClearMessageCim2Eqp = new RelayCommand(_ =>
            {
                _home.MessageCIM2EQP = null;
                _home._messageCim2EQP = null;
            });

            Unloaded = new RelayCommand(_ =>
            {
                _home.MessageCIM2EQP = null;
                _home._messageCim2EQP = null;
            });

            
            //_controller.CIM.Cim2HostChangeEvent += Controller_Cim2HostChangeEvent;
            //_controller.CIM.Plc2CimChangeEvent += Controller_Plc2CimChangeEvent;



            _controller.CIM.Cim2HostChangeEvent -= Controller_Cim2HostChangeEvent;
            _controller.CIM.Cim2HostChangeEvent += Controller_Cim2HostChangeEvent;

            _controller.CIM.Host2CimChangeEvent -= Controller_Cim2HostChangeEvent;
            _controller.CIM.Host2CimChangeEvent += Controller_Cim2HostChangeEvent;

            
            _controller.CIM.Plc2CimChangeEvent -= Controller_Plc2CimChangeEvent;
            _controller.CIM.Plc2CimChangeEvent += Controller_Plc2CimChangeEvent;

            _controller.CIM.Cim2PlcChangeEvent -= Controller_Plc2CimChangeEvent;
            _controller.CIM.Cim2PlcChangeEvent += Controller_Plc2CimChangeEvent;


            _controller.CIM.PlcConnectChangeEvent += Controller_PlcConnectChangeEvent;
            _controller.CIM.Cim.Conn.OnConnectEvent += Controller_CimConnectChangeEvent;
            

            StartDispatcherTimer(UpdateData, 1);
            _alarmUpdateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _alarmUpdateTimer.Tick += (s, e) =>
            {
                _alarmUpdateTimer.Stop();
                UpdateAlarmInternal();
            };
            UpdateAlarm();
            _controller.CIM.ResetEvent += UpdateAlarm;
        }
        #endregion

        #region Event Handlers
        private void Controller_Cim2HostChangeEvent(string SnFm)
        {

            _pendingHostMessage = SnFm;
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            AddMessage(ref _home._messageCim2Host, $"{time}: {_pendingHostMessage}");
        }

        private void Controller_Plc2CimChangeEvent(string bit)
        {
            _pendingPlcMessage = bit;
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            AddMessage(ref _home._messageCim2EQP, $"{time}: {_pendingPlcMessage}");
        }

        private void Controller_CimConnectChangeEvent(bool isConnected)
        {
            _home.CimConnect = isConnected ? ":   CimConnect" : ":   CimDisConnect";
        }

        private void Controller_PlcConnectChangeEvent(bool isConnected)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                _home.PLCConnect = isConnected ? ":   Plc Connected" : ":   Plc Disconnected";
            });
        }
        #endregion

        #region Data Update
        private void UpdateData()
        {
            try
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    _home.AVAILABILITYSTATE = ":   " + (_controller.CIM.EqpData.EQPSTATE.AVAILABILITYSTATE == "2" ? "UP" : "DOWN");
                    _home.INTERLOCKSTATE = ":   " + (_controller.CIM.EqpData.EQPSTATE.INTERLOCKSTATE == "2" ? "OFF" : "ON");
                    _home.RUNSTATE = ":   " + (_controller.CIM.EqpData.EQPSTATE.RUNSTATE == "2" ? "RUN" : "IDLE");
                    _home.FRONTSTATE = ":   " + (_controller.CIM.EqpData.EQPSTATE.FRONTSTATE == "2" ? "UP" : "DOWN");
                    _home.REARSTATE = ":   " + (_controller.CIM.EqpData.EQPSTATE.REARSTATE == "2" ? "UP" : "DOWN");
                    _home.MOVESTATE = ":   " + (_controller.CIM.EqpData.EQPSTATE.MOVESTATE == "2" ? "RUNNING" : "PAUSE");

                    // Xử lý message tạm
                    if (!string.IsNullOrWhiteSpace(_pendingHostMessage))
                    {
                        _home.MessageCIM2HOST = _home._messageCim2Host;
                    }

                    if (!string.IsNullOrWhiteSpace(_pendingPlcMessage))
                    {
                        _home.MessageCIM2EQP = _home._messageCim2EQP;
                        
                    }
                });
            }
            catch (Exception ex)
            {
                var debug = $"Class:{GetType().Name} Method:{MethodBase.GetCurrentMethod().Name} exception occurred. Message is <{ex.Message}>.";
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            //UpdateAlarm();
        }

        private void AddMessage(ref string message, string msg)
        {
            message += msg + "\n";
            var lines = message.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length > 100)
            {
                message = string.Join("\n", lines.Skip(lines.Length - 100)) + "\n";
            }
        }
        #endregion

        #region Others
        private async Task LoadConfig()
        {
            await Task.Run(() =>
            {
                try
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        _home.EQPID = ":   " + _controller.EquipmentConfig.EQPID;
                        _home.EQPNAME = ":   " + _controller.EquipmentConfig.EQPID;
                        _home.PPID = ":   " + _controller.EquipmentConfig.CRST;
                        _home.IP = ":   " + _controller.EquipmentConfig.CimConfig.IP;
                        _home.PORT = ":   " + _controller.EquipmentConfig.CimConfig.Port;
                        _home.STATE = ":   " + _controller.EquipmentConfig.CimConfig.ConnectMode;
                    });
                }
                catch (Exception ex)
                {
                    var debug = $"Class:{GetType().Name} Method:{MethodBase.GetCurrentMethod().Name} exception occurred. Message is <{ex.Message}>.";
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            });
        }

        private void UpdateAlarm()
        {
            _alarmUpdatePending = true;
            
            _alarmUpdateTimer.Start();
        }

        private void UpdateAlarmInternal()
        {
            if (!_alarmUpdatePending) return;
            _alarmUpdatePending = false;

            try
            {
                var tempList = _controller.CIM.EqpData.CurrAlarm.ToList();
                _home.AlarmList = new ObservableCollection<Data.Alarm>(tempList);
            }
            catch (Exception ex)
            {
                var debug = $"Class:{GetType().Name} Method:{MethodBase.GetCurrentMethod().Name} exception occurred. Message is <{ex.Message}>.";
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopDispatcherTimer();
                _running = false;
                CpuChart.OnUnloaded();

                _controller.CIM.Cim2HostChangeEvent -= Controller_Cim2HostChangeEvent;
                _controller.CIM.Plc2CimChangeEvent -= Controller_Plc2CimChangeEvent;
                _controller.CIM.PlcConnectChangeEvent -= Controller_PlcConnectChangeEvent;
                _controller.CIM.Cim.Conn.OnConnectEvent -= Controller_CimConnectChangeEvent;
                _controller.CIM.ResetEvent -=  UpdateAlarm;
            }
            base.Dispose(disposing);
        }

        ~HomeViewModel()
        {
            Dispose(false);
        }
        #endregion

    }
}
