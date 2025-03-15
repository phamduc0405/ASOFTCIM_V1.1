using A_SOFT.CMM.HELPER;
using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.Model;
using ASOFTCIM.MVVM.View.Config;
using ASOFTCIM.MVVM.View.ECM;
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

            DataContext = new MainViewModel();
            Initial();
            maincontent.Content = new HomeView();
           

            CreateEvent();
            _updateTime = new Thread(UpdateTime)
            {
                IsBackground = true,
            };
            _updateTime.Start();
            txtVersion.Text = "Version: 250311";
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
                _updateTime.Abort();
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
                maincontent.Content = new MVVM.View.FDC.FDCView();
            };
            btnRecipes.Click += (sender, e) =>
            {
                maincontent.Content = new MVVM.View.RMS.RMSView();
            };
            btnEcm.Click += (sender, e) =>
            {
                maincontent.Content = new MVVM.View.ECM.ECMView();
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

        private void UpdateTime()
        {
            while (_running)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    tblDateTime.Text = "DateTime: "+ DateTime.Now.ToString();
                }));
                Thread.Sleep(100);
            }

        }

    }
}
