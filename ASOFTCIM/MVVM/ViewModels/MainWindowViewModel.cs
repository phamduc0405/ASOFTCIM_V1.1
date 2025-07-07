using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;
using ASOFTCIM.Init;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.Models;
using ASOFTCIM.MVVM.Views.Alarm;
using ASOFTCIM.MVVM.Views.Config;
using ASOFTCIM.MVVM.Views.ECM;
using ASOFTCIM.MVVM.Views.FDC;
using ASOFTCIM.MVVM.Views.Home;
using ASOFTCIM.MVVM.Views.Material;
using ASOFTCIM.MVVM.Views.Monitor;
using ASOFTCIM.MVVM.Views.Popup;
using ASOFTCIM.MVVM.Views.RMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Input;
using ASOFTCIM.MVVM.Behaviors;
using System.Windows.Threading;
using ECMView = ASOFTCIM.MVVM.Views.ECM.ECMView;
using ASOFTCIM.MVVM.NavigationService;
using Microsoft.Extensions.DependencyInjection;

namespace ASOFTCIM.MVVM.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Events

        #endregion

        #region Costants
        #endregion

        #region Fields
        private static MainWindowModel _mainWindowModel;
        private readonly Controller _controller;
        public static string User = "User";
        public static string Pass = "2";
        public static int LeveLogin = 0;
        private ExitDisplay _displayPopupCode;
        private LogInDisplay _displayPopupLogIn;
        private DateTime _datetime;
        private Thread _updateTime;
        private static INavigationService _navigationService;
        private static InactivityMonitor _inactivityMonitor;
        #endregion

        #region Properties
        public ICommand HomeViewCommand { get; set; }
        public ICommand ConfigViewCommand { get; set; }
        public ICommand MonitorIOViewCommand { get; set; }
        public ICommand FDCViewCommand { get; set; }
        public ICommand RMSViewCommand { get; set; }
        public ICommand ECMViewCommand { get; set; }
        public ICommand MaterialViewCommand { get; set; }
        public ICommand ALARMViewCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand ResizeCommand { get; set; }
        public ICommand HideMenuCommand { get; set; }
        public ICommand LogInCommand { get; set; }
        public ICommand grdTopMain { get; set; }

        private readonly Window _window;
       
        public static ASOFTCIM.MVVM.Models.MainWindowModel MainWindowModel
        {
            get => _mainWindowModel;
            set { _mainWindowModel = value;  }
        }
        public static INavigationService NavigationService
        {
            get => _navigationService;
            set { _navigationService = value; }
        }
        #endregion

        #region Constructors
        public MainWindowViewModel(Window window, IServiceProvider serviceProvider)
        {
            _mainWindowModel = new MainWindowModel();
            _mainWindowModel.CancellationTokenSource = new CancellationTokenSource();
            _mainWindowModel.CancellationToken = _mainWindowModel.CancellationTokenSource.Token;
            _navigationService = new ASOFTCIM.MVVM.NavigationService.NavigationService(view =>
            {
                MainWindowModel.Currentview = view;
            }, serviceProvider); 
            _window = window;
            
            _navigationService.NavigateTo<HomeViewModel>();
            _mainWindowModel.VersionInfo = $"Version : {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()}       {LibMethod.GetBuildTime()}";
            _updateTime = new Thread(UpdateTime)
            {
                IsBackground = true,
            };
            _updateTime.Start();
            HomeViewCommand = new RelayCommand(o => _navigationService.NavigateTo<HomeViewModel>());
            ALARMViewCommand = new RelayCommand(o => _navigationService.NavigateTo<AlarmViewModel>());
            FDCViewCommand = new RelayCommand(o => _navigationService.NavigateTo<FDCViewModel>());
            ConfigViewCommand = new RelayCommand(excute =>
            {
                _navigationService.NavigateTo<ConfigMainViewModel>();
            },
            canExecute =>
            {
                if (LeveLogin == 1)
                    return true;
                return false;
            }
            );
            ECMViewCommand = new RelayCommand(o => _navigationService.NavigateTo<ECMViewModel>());
            MaterialViewCommand = new RelayCommand(o =>
            {
                _navigationService.NavigateTo<MaterialViewModel>();
            },
            canExecute =>
            {
                if (LeveLogin == 1)
                    return true;
                return false;
            }
            );
            MonitorIOViewCommand = new RelayCommand(o =>
            {
                var controller = serviceProvider.GetRequiredService<Controller>();
                MainWindowModel.Currentview = new MonitorIOView(controller);
            },
            canExecute =>
            {
                if (LeveLogin == 1)
                    return true;
                return false;
            }
            );
            CloseCommand = new AsyncRelayCommand(async () =>
            {
                if (await PopupMessage("DO YOU WANT EXIT ?"))
                {
                    Thread.Sleep(1000);
                    _mainWindowModel.CancellationTokenSource.Cancel();
                    if (_updateTime != null && _updateTime.IsAlive)
                    {
                        if (!_updateTime.Join(3000))
                        {
                            LogTxt.Add(LogTxt.Type.Exception, "Thread UpdateTime không kết thúc đúng hạn.");
                        }
                    }
                    _window.Close();
                    LogTxt.Stop();
                }
            });
            ResizeCommand = new RelayCommand(o =>
            {
                if (_window.WindowState == (WindowState)FormWindowState.Normal)
                {
                    _window.WindowState = (WindowState)FormWindowState.Maximized;
                }
                else
                {
                    _window.WindowState = (WindowState)FormWindowState.Normal;
                }
            });
            HideMenuCommand = new RelayCommand(o =>
            {
                _window.WindowState = (WindowState)FormWindowState.Minimized;
            });
            LogInCommand = new RelayCommand(o =>
            {
                PopupLogIn();
            });
        }
        #endregion

        #region private method
        private async Task<bool> PopupMessage(string message)
        {
            bool result = false;
            try
            {

                if (_displayPopupCode == null)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() => {
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
                    System.Windows.Application.Current.Dispatcher.Invoke(() => {
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
        private void UpdateTime()
        {
            while (!_mainWindowModel.CancellationToken.IsCancellationRequested)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    _mainWindowModel.DateTime = "DateTime: " + DateTime.Now.ToString();
                }));
                Thread.Sleep(100);
            }

        }
        private void StartInactivityMonitor()
        {
            _inactivityMonitor?.Stop();
            _inactivityMonitor = new InactivityMonitor(TimeSpan.FromMinutes(2));
            //_inactivityMonitor = new InactivityMonitor(TimeSpan.FromSeconds(10));
            _inactivityMonitor.TimeoutReached += () =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    if(LeveLogin==1)
                    {
                        System.Windows.MessageBox.Show("Tài khoản Admin đã hết thời gian hoạt động. Tự động đăng xuất.");
                        LogOut();
                    }
                });
            };
            _inactivityMonitor.Start();

        }

        #endregion

        #region public method
        public void LogIn()
        {
            if (User == "Admin" && Pass == "1")
            {
                MainWindowViewModel.User = User;
                _mainWindowModel.User = User;
                _mainWindowModel.LogInOut = "LogOut";
                LeveLogin = 1;

                StartInactivityMonitor();
                return;
            }
            if (User == "Engineer" && Pass == "2")
            {
                MainWindowViewModel.User = User;
                _mainWindowModel.LogInOut = "LogOut";
                _mainWindowModel.User = User;

                LeveLogin = 2;
                return;
            }
            if (User == "Operator" && Pass == "3")
            {
                MainWindowViewModel.User = User;
                _mainWindowModel.LogInOut = "LogOut";
                _mainWindowModel.User = User;
                LeveLogin = 3;
            }
            else
            {
                MainWindowViewModel.User = "User";
                _mainWindowModel.User = User;
                _mainWindowModel.LogInOut = "LogIn";

                LeveLogin = 2;
            }
        }
        public void LogOut()
        {
            LeveLogin = 0;
            User = "User";
            Pass = "2";
            _mainWindowModel.User = User;
            _mainWindowModel.LogInOut = "LogIn";
            _navigationService.NavigateTo<HomeViewModel>();
            //MainWindowModel.Currentview = new HomeView(); //quay ve HOME
        }
        #endregion

    }
}
