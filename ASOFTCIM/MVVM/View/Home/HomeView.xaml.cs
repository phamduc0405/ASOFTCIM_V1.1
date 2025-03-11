using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using ASOFTCIM.Config;
using ASOFTCIM.Data;
using ASOFTCIM.MainControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
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

namespace ASOFTCIM.MVVM.View.Home
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private Controller _controller;
        private EquipmentConfig _equipmentConfig;
        private Thread _updateData;
        private static bool _running = true;
        private List<Data.Alarm> _data = new List<Data.Alarm>();
        public static bool Running
        {
            get
            {
                return _running;
            }
        }

        public List<Data.Alarm> Data
        {
            get { return _data; }
            set
            {
                _data = value;
            }
        }
        public HomeView()
        {
            InitializeComponent();
            this.DataContext = this;
            _controller = MainWindow.Controller;
            CreaterEvent();
            _controller.CIM.ResetEvent += UpdateAlarm;
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
                }
                catch (Exception ex)
                {
                    
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }

            };
            Unloaded += async (s ,e ) =>
            {
                _updateData.Abort();
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
                        // Thực hiện cập nhật giao diện người dùng ở đây
                        //EqpConfig
                        {
                            
                        }

                        //PlcConfig
                        {
                            txtEQPID.Text = ":   " + _equipmentConfig.EQPID.ToString();
                            txtRecipy.Text = ":   " + _equipmentConfig.CRST.ToString();
                            txtEQPName.Text = ":   " + _equipmentConfig.EqpName.ToString();
                        }
                        //cimConfig
                        {
                            txtIp.Text = ":   "+ _equipmentConfig.CimConfig.IP.ToString();
                            txtState.Text = ":   " + _equipmentConfig.CimConfig.ConnectMode.ToString();
                            txtPort.Text = ":   " + _equipmentConfig.CimConfig.Port.ToString();

                        }
                    });
                }
                catch (Exception ex)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }

            });
        }
        private void  UpdateData()
        {
            while (_running)
            {
                try
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        //EQPState
                        {
                            txtAvailabilityState.Text = ":   " + (_controller.CIM.EqpData.EQPSTATE.AVAILABILITYSTATE == "2" ? "UP" : "DOWN");
                            txtInterLockState.Text = ":   " + (_controller.CIM.EqpData.EQPSTATE.INTERLOCKSTATE == "2" ? "INTERLOCKOFF" : "INTERLOCKON");
                            txtRunState.Text = ":   " + (_controller.CIM.EqpData.EQPSTATE.RUNSTATE == "2" ? "RUNNING" : "PAUSE");
                            txtFronState.Text = ":   " + (_controller.CIM.EqpData.EQPSTATE.FRONTSTATE == "2" ? "RUN" : "IDLE");
                            txtRearState.Text = ":   " + (_controller.CIM.EqpData.EQPSTATE.REARSTATE == "2" ? "UP" : "DOWN");
                            txtMoveState.Text = ":   " + (_controller.CIM.EqpData.EQPSTATE.MOVESTATE == "2" ? "UP" : "DOWN");
                        }
                        //PlcConfig
                        {
                            //txtEQPID.Text = ":   " + _equipmentConfig.EQPID.ToString();
                            //txtRecipy.Text = ":   " + _equipmentConfig.CRST.ToString();
                            //txtEQPName.Text = ":   " + _equipmentConfig.EqpName.ToString();
                        }
                        //cimConfig
                        {
                            //txtIp.Text = ":   " + _equipmentConfig.CimConfig.IP.ToString();
                            //txtState.Text = ":   " + _equipmentConfig.CimConfig.ConnectMode.ToString();
                            //txtPort.Text = ":   " + _equipmentConfig.CimConfig.Port.ToString();
                        }
                        //ALARM
                        {
                            //_data = _controller.CIM.EqpData.CurrAlarm;
                           
                            //dtgrtView.AutoGenerateColumns = true;
                            //dtgrtView.ItemsSource = Data;
                        }
                    }));
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
                    {
                        dtgrtView.ItemsSource = null;
                        _data = _controller.CIM.EqpData.CurrAlarm;
                        dtgrtView.ItemsSource = Data;
                    }
                }));
            }
            catch { }
        }
    }
}
