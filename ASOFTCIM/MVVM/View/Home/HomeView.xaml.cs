using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using ASOFTCIM.Config;
using ASOFTCIM.Data;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using ASOFTCIM.Config;
using ASOFTCIM.Data;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace ASOFTCIM.MVVM.View.Home
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl, INotifyPropertyChanged
    {

        private ObservableCollection<Data.Alarm> _alarmList = new ObservableCollection<Data.Alarm>();
        public ObservableCollection<Data.Alarm> AlarmList
        {
            get => _alarmList;
            set
            {
                _alarmList = value;
                OnPropertyChanged(nameof(AlarmList));
            }
        }

        private Controller _controller;
        private EquipmentConfig _equipmentConfig;
        private Thread _updateData;
        private static bool _running = true;
        private List<Data.Alarm> _data = new List<Data.Alarm>();

        private PerformanceCounter memoryCounter;
        private Thread memoryUsageThread;
        private bool isMonitoringMemory = true;

        private MainViewModel viewModel;
        private Thread _updateTime;

        private PartialCpuChart _cpuChart;

        public static bool Running
        {
            get { return _running; }
        }

        public List<Data.Alarm> Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public HomeView()
        {
            InitializeComponent();
            this.DataContext = this;
            _cpuChart = new PartialCpuChart();
            grdCpu.Children.Add(_cpuChart);

            this.DataContext = this;
            _controller = MainWindow.Controller;

            CreaterEvent();

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

            _updateData = new Thread(UpdateData)
            {
                IsBackground = true,
            };
            _updateData.Start();
        }

        private void CreaterEvent()
        {
            Loaded += async (s, e) =>
            {
                try
                {
                    _equipmentConfig = _controller.EquipmentConfig;
                    if (_equipmentConfig == null)
                    {
                        _equipmentConfig = new EquipmentConfig();
                    }

                    await LoadConfig();
                    UpdateAlarm();
                }
                catch (Exception ex)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.",
                        MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            };

            Unloaded += (s, e) =>
            {
                _updateData?.Abort();
            };
        }

        private async Task LoadConfig()
        {
            await Task.Run(() =>
            {
                try
                {
                    Dispatcher.Invoke(() =>
                    {
                        txtEQPID.Text = ":   " + _equipmentConfig.EQPID.ToString();
                        txtRecipy.Text = ":   " + _equipmentConfig.CRST.ToString();
                        txtEQPName.Text = ":   " + _equipmentConfig.EqpName.ToString();
                        txtIp.Text = ":   " + _equipmentConfig.CimConfig.IP.ToString();
                        txtState.Text = ":   " + _equipmentConfig.CimConfig.ConnectMode.ToString();
                        txtPort.Text = ":   " + _equipmentConfig.CimConfig.Port.ToString();
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

        private void UpdateData()
        {
            while (_running)
            {
                try
                {
                    Dispatcher.Invoke(() =>
                    {
                        txtAvailabilityState.Text = ":   " + (_controller.CIM.EqpData.EQPSTATE.AVAILABILITYSTATE == "2" ? "UP" : "DOWN");
                        txtInterLockState.Text = ":   " + (_controller.CIM.EqpData.EQPSTATE.INTERLOCKSTATE == "2" ? "OFF" : "ON");
                        txtRunState.Text = ":   " + (_controller.CIM.EqpData.EQPSTATE.RUNSTATE == "2" ? "RUN" : "IDLE");
                        txtFronState.Text = ":   " + (_controller.CIM.EqpData.EQPSTATE.FRONTSTATE == "2" ? "UP" : "DOWN");
                        txtRearState.Text = ":   " + (_controller.CIM.EqpData.EQPSTATE.REARSTATE == "2" ? "UP" : "DOWN");
                        txtMoveState.Text = ":   " + (_controller.CIM.EqpData.EQPSTATE.MOVESTATE == "2" ? "RUNNING" : "PAUSE");
                    });
                    Thread.Sleep(500);
                }
                catch { }
            }
        }

        private void UpdateAlarm()
        {
            try
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    try
                    {
                        AlarmList.Clear();  // Xóa dữ liệu cũ
                        var tempList = _controller.CIM.EqpData.CurrAlarm.ToList();
                        foreach (var item in tempList)
                        {
                            AlarmList.Add(item);  // Cập nhật lại dữ liệu mới
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }));
            }
            catch (Exception ex) {

                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            
            }
        }
        private void UpdateHostcimMessage(string SnFm)
        {

            Dispatcher.Invoke(new Action(() =>
            {
                string time = DateTime.Now.ToString("hh:mm:ss.fff");
                txtCimHost.AppendText("\n" + time + ": " + SnFm);
                LimitRichTextBoxLines(txtCimHost);
            }));
        }
        private void UpdatePlccimMessage(string bit)
        {

            Dispatcher.Invoke(new Action(() =>
            {
                string time = DateTime.Now.ToString("hh:mm:ss.fff");
                txtCimEqp.AppendText("\n" + time + ": " + bit);
                LimitRichTextBoxLines(txtCimEqp);
            }));
        }
        private void LimitRichTextBoxLines(RichTextBox richTextBox)
        {
            int maxLines = 20;
            var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            string[] lines = textRange.Text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length > maxLines)
            {
                string newText = string.Join("\n", lines.Skip(lines.Length - maxLines));
                richTextBox.Document.Blocks.Clear();
                richTextBox.Document.Blocks.Add(new Paragraph(new Run(newText)));
            }
        }

        private void Controller_PlcConnectChangeEvent(bool isConnected)
        {
            Dispatcher.Invoke(() =>
            {
                txtPlcConnect.Text = isConnected ? "Plc Connected" : "Plc Disconnected";
            });
        }

        private void Controller_CimConnectChangeEvent(bool isConnected)
        {
            Dispatcher.Invoke(() =>
            {
                txtCimConnect.Text = isConnected ? "Cim Connected" : "Cim Disconnected";
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

