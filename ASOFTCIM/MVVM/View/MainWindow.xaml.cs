using A_SOFT.CMM.HELPER;
using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;
using ASOFTCIM.Helper;
using ASOFTCIM.Init;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.Model;
using ASOFTCIM.MVVM.View.Alarm;
using ASOFTCIM.MVVM.View.Config;
using ASOFTCIM.MVVM.View.ECM;
using ASOFTCIM.MVVM.View.Home;
using ASOFTCIM.MVVM.View.Material;
using ASOFTCIM.MVVM.View.Monitor;
using ASOFTCIM.MVVM.View.Popup;
using ASOFTCIM.MVVM.ViewModel;
using OfficeOpenXml.FormulaParsing.Excel.Operators;
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
        public static string User = "User";
        public static string Pass = "2";
        public static int LeveLogin = 0;
        private ExitDisplay _displayPopupCode;
        private LogInDisplay _displayPopupLogIn;
        private PerformanceCounter memoryCounter;
        private Thread memoryUsageThread;
        private bool isMonitoringMemory = true;
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

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

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            DataContext = new MainViewModel();
            Initial();
            maincontent.Content = new HomeView();
           

            CreateEvent();
            _updateTime = new Thread(UpdateTime)
            {
                IsBackground = true,
            };
            _updateTime.Start();
            txtVersion.Text =$"Version : {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()}       {LibMethod.GetBuildTime()}" ;
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
                _running = false;
                LogTxt.Stop();

            };
            btnClose.Click += async (sender, e) =>
            {
                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)sender).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
                if (await PopupMessage("DO YOU WANT EXIT ?"))
                {
                    Thread.Sleep(1000);
                    _cancellationTokenSource.Cancel();
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
                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)sender).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
            };
            grdTopMain.MouseDown += (sender, e) =>
            {

                DragMove();
            };

            btnHome.Click += (sender, e) =>
            {
                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)sender).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
                maincontent.Content = new HomeView();
            };
            btnMonitor.Click += (sender, e) =>
            {
                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)sender).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
                if (LeveLogin != 3)
                    maincontent.Content = new MonitorIOView();
            };
            btnConfig.Click += (sender, e) =>
            {
                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)sender).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
                if (LeveLogin == 1)
                    maincontent.Content = new ConfigMainView();
            };
            btnSvid.Click += (sender, e) =>
            {
                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)sender).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
                maincontent.Content = new MVVM.View.FDC.FDCView();
            };
            btnRecipes.Click += (sender, e) =>
            {
                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)sender).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
                maincontent.Content = new MVVM.View.RMS.RMSView();
            };
            btnEcm.Click += (sender, e) =>
            {
                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)sender).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
                maincontent.Content = new MVVM.View.ECM.ECMView();
            };
            btnAlarm.Click += (sender, e) =>
            {
                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)sender).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
                maincontent.Content = new MVVM.View.Alarm.AlarmView();
            };
            btnMaterial.Click += (sender, e) =>
            {
                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)sender).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
                maincontent.Content = new MaterialView();
            };
            btnLogIn.Click += (sender, e) =>
            {
                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)sender).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
                PopupLogIn();
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
        private async Task<bool> PopupLogIn()
        {
            bool result = false;
            try
            {
                if (_displayPopupLogIn == null)
                {
                    Dispatcher.Invoke(() => {
                        _displayPopupLogIn = new LogInDisplay();
                        result = (bool)_displayPopupLogIn.ShowDialog();
                        // Check the DialogResult
                        _displayPopupLogIn.Closing += (sender, a) =>
                        {
                            LogIn();
                            _displayPopupCode = null;
                        };
                        _displayPopupLogIn.Topmost = true;
                        LogIn();
                        _displayPopupLogIn.Close();
                        _displayPopupLogIn = null;
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
        public void LogIn()
        {
            if(User=="Admin" && Pass=="1")
            {
                txtUser.Text = User;
                LeveLogin = 1;
                return;
            }
            if (User == "Enginer" && Pass == "2")
            {
                txtUser.Text = User;
                LeveLogin = 2;
                return;
            }
            if (User == "Operator" && Pass == "3")
            {
                txtUser.Text = User;
                LeveLogin = 3;
            }
            else
            {
                txtUser.Text = "User";
                LeveLogin = 2;
            }    
        }
        private void UpdateTime()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    tblDateTime.Text = "DateTime: " + DateTime.Now.ToString();
                }));
                Thread.Sleep(100);
            }

        }

    }
}
