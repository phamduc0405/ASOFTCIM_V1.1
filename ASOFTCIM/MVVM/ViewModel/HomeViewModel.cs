
using System;
using ASOFTCIM.Init;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ASOFTCIM.MainControl;
using System.Collections.ObjectModel;
using ASOFTCIM.MVVM.Model;
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
using ASOFTCIM.MVVM.View.Home;

namespace ASOFTCIM.MVVM.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged, IDisposable
    {
        #region Fields
        private readonly Controller _controller;
        private HomeModel _home;
        private Thread _updateData;
        private static bool _running = true;
        private bool _disposed = false;

        private UIElement _cpuChart;


        #endregion
        #region Properties
        public UIElement CpuChart
        {
            get => _cpuChart;
            set { _cpuChart = value; OnPropertyChanged(nameof(CpuChart)); }
        }
        public HomeModel Home
        {
            get => _home;
            private set { _home = value; }
        }

        public ICommand ClearMessageCim2Host { get; }
        public ICommand ClearMessageCim2Eqp { get; }
        public ICommand Unloaded { get; }
        #endregion
        #region CONSTRUCTOR
        public HomeViewModel()
        {
            _controller = MainWindow.Controller;
            _home = new HomeModel();
            _home.EQPID = _controller.EquipmentConfig.EQPID;
            CpuChart = new PartialCpuChart();
            LoadConfig();
            Controller_CimConnectChangeEvent(_controller.CimConnect);
            UpdateAlarm();
            ClearMessageCim2Host = new RelayCommand(_ =>
            {
                if (_home.MessageCIM2HOST != null)
                {
                    _home.MessageCIM2HOST = null;
                }
            });
            ClearMessageCim2Eqp = new RelayCommand(_ =>
            {
                if (_home.MessageCIM2EQP != null)
                {
                    _home.MessageCIM2EQP = null;
                }
            });
            Unloaded = new RelayCommand(_ =>
            {
                if (_home.MessageCIM2EQP != null)
                {
                    _home.MessageCIM2EQP = null;
                }
            });
            _controller.CIM.PlcConnectChangeEvent -= Controller_PlcConnectChangeEvent;
            _controller.CIM.PlcConnectChangeEvent += Controller_PlcConnectChangeEvent;
            _controller.CIM.Cim.Conn.OnConnectEvent -= Controller_CimConnectChangeEvent;
            _controller.CIM.Cim.Conn.OnConnectEvent += Controller_CimConnectChangeEvent;
            _controller.CIM.ResetEvent -= UpdateAlarm;
            _controller.CIM.ResetEvent += UpdateAlarm;

            _controller.CIM.Cim2HostChangeEvent -= UpdateHostcimMessage;
            _controller.CIM.Cim2HostChangeEvent += UpdateHostcimMessage;

            _controller.CIM.Host2CimChangeEvent -= UpdateHostcimMessage;
            _controller.CIM.Host2CimChangeEvent += UpdateHostcimMessage;

            _controller.CIM.Plc2CimChangeEvent -= UpdatePlccimMessage;
            _controller.CIM.Plc2CimChangeEvent += UpdatePlccimMessage;

            _controller.CIM.Cim2PlcChangeEvent -= UpdatePlccimMessage;
            _controller.CIM.Cim2PlcChangeEvent += UpdatePlccimMessage;
            _running = true;
            _updateData = new Thread(UpdateData)
            {
                IsBackground = true,
            };
            _updateData.Start();
        }
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
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.",
                        MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            });
        }
        #endregion
        private void UpdateData()
        {
            while (_running)
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
                    });
                    Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.",
                        MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            }
        }
        private void UpdateAlarm()
        {
            try
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    try
                    {
                        _home.AlarmList.Clear();  // Xóa dữ liệu cũ
                        var tempList = _controller.CIM.EqpData.CurrAlarm.ToList();
                        foreach (var item in tempList)
                        {
                            _home.AlarmList.Add(item);  // Cập nhật lại dữ liệu mới
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.",
                        MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                        LogTxt.Add(LogTxt.Type.Exception, debug);
                    }
                }));
            }
            catch (Exception ex)
            {

                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);

            }
        }
        private void UpdateHostcimMessage(string SnFm)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {

                string time = DateTime.Now.ToString("HH:mm:ss.fff");
                AddMessage(ref _home._messageCim2Host, $"{time}: {SnFm}");
                _home.MessageCIM2HOST = _home._messageCim2Host; // Notify binding
            }));
        }
        private void UpdatePlccimMessage(string bit)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                string time = DateTime.Now.ToString("HH:mm:ss.fff");
                AddMessage(ref _home._messageCim2EQP, $"{time}: {bit}");
                _home.MessageCIM2EQP = _home._messageCim2EQP; // Notify binding
            }));
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
        public void StopThread()
        {
            _running = false;
            if (_updateData != null && _updateData.IsAlive)
            {
                _updateData?.Join(1000);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    StopThread();
                    _controller.CIM.PlcConnectChangeEvent -= Controller_PlcConnectChangeEvent;
                    _controller.CIM.Cim.Conn.OnConnectEvent -= Controller_CimConnectChangeEvent;
                    _controller.CIM.ResetEvent -= UpdateAlarm;
                    _controller.CIM.Cim2HostChangeEvent -= UpdateHostcimMessage;
                    _controller.CIM.Host2CimChangeEvent -= UpdateHostcimMessage;
                    _controller.CIM.Plc2CimChangeEvent -= UpdatePlccimMessage;
                    _controller.CIM.Cim2PlcChangeEvent -= UpdatePlccimMessage;
                }
                _disposed = true;
            }
        }
        ~HomeViewModel()
        {
            Dispose(false);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
