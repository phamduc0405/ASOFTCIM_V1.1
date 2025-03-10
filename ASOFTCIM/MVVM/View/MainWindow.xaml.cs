using A_SOFT.CMM.HELPER;
using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.Model;
using ASOFTCIM.MVVM.View.Config;
using ASOFTCIM.MVVM.View.Home;
using ASOFTCIM.MVVM.View.Material;
using ASOFTCIM.MVVM.View.Monitor;
using ASOFTCIM.MVVM.View.Popup;
using ASOFTCIM.MVVM.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ASOFTCIM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Controller Controller;
        private ExitDisplay _displayPopupCode;
        private PerformanceCounter memoryCounter;
        private Thread memoryUsageThread;
        private bool isMonitoringMemory = true;
        
        private MainViewModel viewModel;
        private Thread _updateTime;
        private PartialCpuChart _cpuChart;

        private static bool _running = true;
        public static bool Running
        {
            get
            {
                return _running;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            _cpuChart = new PartialCpuChart();
            grdCpu.Children.Add(_cpuChart);
            StartMemoryMonitoring();
            DataContext = new MainViewModel();
            Initial();
            maincontent.Content = new HomeView();
            Controller.CIM.PlcConnectChangeEvent -= Controller_PlcConnectChangeEvent;
            Controller.CIM.PlcConnectChangeEvent += Controller_PlcConnectChangeEvent;
            Controller.CIM.Cim.Conn.OnConnectEvent -= Controller_CimConnectChangeEvent;
            Controller.CIM.Cim.Conn.OnConnectEvent += Controller_CimConnectChangeEvent;
            CreateEvent();
        }
        private void Initial()
        {
            Controller = new Controller();
        }
        private void CreateEvent()
        {
            this.Closing += (s, e) =>
            {
                Controller.Stop();
                LogTxt.Stop();

            };
            btnClose.Click += async (sender, e) =>
            {
                if(await PopupMessage("DO YOU WANT EXIT ?"))
                {
                    Thread.Sleep(1000);
                    this.Close();
                    LogTxt.Stop();
                }    
            };
            btnResize.Click += (sender, e) =>
            {
                if (this.WindowState == (WindowState)FormWindowState.Normal)
                {
                    this.WindowState = (WindowState)FormWindowState.Maximized;
                }
                else { this.WindowState = (WindowState)FormWindowState.Normal; }


            };
            btnHideMenu.Click += (sender, e) =>
            {
                this.WindowState = (WindowState)FormWindowState.Minimized;

            };
            grdTopMain.MouseDown += (sender, e) =>
            {

                DragMove();
            };

            grdPanel.MouseEnter += (sender, e) =>
            {
                //if (tgMenu.IsChecked == true)
                //{
                //    ttT5s.Visibility = Visibility.Collapsed;
                //    ttConfig.Visibility = Visibility.Collapsed;
                //}
                //else
                //{
                //    ttT5s.Visibility = Visibility.Visible;
                //    ttConfig.Visibility = Visibility.Visible;
                //}
            };
            btnHome.Click += (sender, e) =>
            {
                maincontent.Content = new HomeView();
            };
            btnMonitor.Click += (sender, e) =>
            {
                maincontent.Content = new MonitorIOView();
            };
            btnConfig.Click += (sender, e) =>
            {
                maincontent.Content = new ConfigMainView();
            };
            btnSvid.Click += (sender, e) =>
            {
                maincontent.Content = new FDCView();
            };
            btnRecipes.Click += (sender, e) =>
            {
                //maincontent.Content = new RMSView();
            };
            btnEcm.Click += (sender, e) =>
            {
                //maincontent.Content = new ECM.ECM();
            };
            btnAlarm.Click += (sender, e) =>
            {
                maincontent.Content = new Alarm();
            };
            btnMaterial.Click += (sender, e) =>
            {
                maincontent.Content = new MaterialView();
            };

        }
        private async Task<bool> PopupMessage(string message)
        {
            bool result = false;
            try
            {

                if (_displayPopupCode == null)
                {
                    Dispatcher.Invoke(() => {
                        _displayPopupCode = new ExitDisplay(message);
                        result = (bool)_displayPopupCode.ShowDialog();
                        // Check the DialogResult
                        _displayPopupCode.Closing += (sender, a) =>
                        {
                            _displayPopupCode = null;
                        };
                        _displayPopupCode.Topmost = true;
                        _displayPopupCode.Close();
                        _displayPopupCode = null;
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
                return false;
            }
        }
        private void Controller_PlcConnectChangeEvent(bool isConnected)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                bdrPlcConnect.Background = isConnected ? Brushes.Green : Brushes.Red;
                txtPlcConnect.Text = isConnected ? "Plc Connected" : "Plc Disconnected";
            }));
        }
        private void Controller_CimConnectChangeEvent(bool isConnected)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                bdrCimConnect.Background = isConnected ? Brushes.Green : Brushes.Red;
                txtCimConnect.Text = isConnected ? "Cim Connected" : "Cim Disconnected";
            }));
        }
        private void StartMemoryMonitoring()
        {
            memoryCounter = new PerformanceCounter("Memory", "Available MBytes");
            memoryUsageThread = new Thread(() =>
            {
                while (isMonitoringMemory)
                {
                    Thread.Sleep(1000);
                    UpdateMemoryUsage();
                }
            });
            memoryUsageThread.IsBackground = true;
            memoryUsageThread.Start();
        }

        private void UpdateMemoryUsage()
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    //double totalMemory = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / (1024.0 * 1024.0);
                    //double availableMemory = memoryCounter.NextValue();
                    //double usedMemory = totalMemory - availableMemory;
                    //double usagePercentage = (usedMemory / totalMemory) * 100;
                    //txtMemoryUsage.Text = $"Memory Usage: {usedMemory:F1} MB / {totalMemory:F1} MB ({usagePercentage:F1}%)";
                }
                catch (Exception ex)
                {
                    txtMemoryUsage.Text = "Error retrieving memory info.";
                    Debug.WriteLine($"Error: {ex.Message}");
                }
            });
        }

    }
}
