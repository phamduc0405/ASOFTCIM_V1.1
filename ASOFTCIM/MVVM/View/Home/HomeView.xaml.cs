using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using ASOFTCIM.Config;
using ASOFTCIM.MainControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
        public HomeView()
        {
            InitializeComponent();
            _controller = MainWindow.Controller;
            CreaterEvent();
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
    }
}
