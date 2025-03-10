using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using ASOFTCIM.Config;
using ASOFTCIM.MainControl;
using System;
using System.Collections.Generic;
using System.Linq;
using ASOFTCIM.Init;
using System.Reflection;
using System.Text;
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
using System.IO;
using System.Diagnostics;

namespace ASOFTCIM.MVVM.View.Config
{
    /// <summary>
    /// Interaction logic for ConfigView.xaml
    /// </summary>
    public partial class ConfigView : System.Windows.Controls.UserControl
    {
        #region Field

        private Controller _controller;
        private EquipmentConfig _equipmentConfig;
        #endregion


        #region Property

        #endregion
        #region Event

        #endregion
        #region Constructor
        public ConfigView()
        {
            InitializeComponent();
            _controller = MainWindow.Controller;

            Initial();
            CreaterEven();

        }
        #endregion
        #region Private Void
        private void Initial()
        {
        }
        private void CreaterEven()
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
                    cbbplcConnectType.ItemsSource = Enum.GetValues(typeof(PlcConnectType));
                    cbbplcConnectType.SelectedIndex = 0;
                    await LoadConfig();
                }
                catch (Exception ex)
                {
                    // _controller.DisplayMessage(false, "Check Input Type!");
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }

            };
            btnDirPLCExcel.Click += (s, e) =>
            {

                LibMethod.SelectFile(LibMethod.extension.excel, txtPathPlcExcel);

                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)s).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
            };
            btnDirLog.Click += (s, e) =>
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                folderBrowser.Description = "Chọn thư mục";

                // Hiển thị cửa sổ dialog và lấy đường dẫn thư mục nếu người dùng chọn
                DialogResult result = folderBrowser.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string folderPath = folderBrowser.SelectedPath;
                    // Sử dụng đường dẫn thư mục ở đây
                    txtPathLog.Text = folderPath;
                }
            };
            btnSaveEqpConfig.Click += async (s, e) =>
            {
               
            };

            btnSavePlcConfig.Click += async (s, e) =>
            {
                try
                {
                    // LoadingPlcImage.Visibility = Visibility.Visible;

                    _equipmentConfig.PLCConfig = new PLCConfig()
                    {
                        Channel = short.Parse(txtPLCChannel.Text),
                        NetworkNo = short.Parse(txtPLCNetWork.Text),
                        Path = int.Parse(txtPLCPath.Text),
                        StationNo = int.Parse(txtPLCStation.Text),
                        IsCCLinkIe = tglPlcUseCCLinkIe.IsChecked == true,

                        ReadStartBitAddress = txtPLCStartInBAdd.Text,
                        SizeReadBit = int.Parse(txtPLCLengthInB.Text),
                        ReadStartWordAddress = txtPLCStartInWAdd.Text,
                        SizeReadWord = int.Parse(txtPLCLengthInW.Text),

                        WriteStartBitAddress = txtPLCStartOutB.Text,
                        SizeWriteBit = int.Parse(txtPLCLengthOutB.Text),
                        WriteStartWordAddress = txtPLCStartOutW.Text,
                        SizeWriteWord = int.Parse(txtPLCLengthOutW.Text),

                        //PlcConnectType = (PlcConnectType)cbbplcConnectType.SelectedItem,

                        //PortPlc = int.Parse(txtPLCPort.Text),
                        
                    };
                    if (File.Exists(txtPathPlcExcel.Text))
                    {
                        _controller.CIM.LoadExcelConfig(@"D:\Project_New\ACIM\SDCCIM_ASOFT_Portal_Online_Map_SDC_Basic_V1.21_v0.1.xlsx");
                    }
                    var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)s).Name);
                    LogTxt.Add(LogTxt.Type.UI, debug);
                    await SaveConfig();
                    await LoadConfig();
                }
                catch (Exception ex)
                {
                    //  _controller.DisplayMessage(false, "Check Input Type!");
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
                //LoadingPlcImage.Visibility = Visibility.Hidden;
            };
            btnSaveCimConfig.Click += async (s, e) =>
            {
                try
                {
                    _equipmentConfig.CimConfig = new CimConfig()
                    {
                        IP = txtIp.Text,
                        ConnectMode = txtConectMode.Text,
                        Port = txtPort.Text,
                    };
                    var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)s).Name);
                    LogTxt.Add(LogTxt.Type.UI, debug);
                    await SaveConfig();
                    await LoadConfig();
                }
                catch (Exception ex)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            };

        }
        private async Task SaveConfig()
        {
            await Task.Run(async () =>
            {
                _controller.SaveControllerConfig();
            });

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
                            txtEqpId.Text = _equipmentConfig.EQPID;
                        }

                        //PlcConfig
                        {
                            txtPLCChannel.Text = _equipmentConfig.PLCConfig.Channel.ToString();
                            txtPLCNetWork.Text = _equipmentConfig.PLCConfig.NetworkNo.ToString();
                            txtPLCPath.Text = _equipmentConfig.PLCConfig.Path.ToString();
                            txtPLCStation.Text = _equipmentConfig.PLCConfig.StationNo.ToString();
                            tglPlcUseCCLinkIe.IsChecked = _equipmentConfig.PLCConfig.IsCCLinkIe;


                            txtPLCStartInBAdd.Text = _equipmentConfig.PLCConfig.ReadStartBitAddress.ToString();
                            txtPLCLengthInB.Text = _equipmentConfig.PLCConfig.SizeReadBit.ToString();
                            txtPLCStartInWAdd.Text = _equipmentConfig.PLCConfig.ReadStartWordAddress.ToString();
                            txtPLCLengthInW.Text = _equipmentConfig.PLCConfig.SizeReadWord.ToString();

                            txtPLCStartOutB.Text = _equipmentConfig.PLCConfig.WriteStartBitAddress.ToString();
                            txtPLCLengthOutB.Text = _equipmentConfig.PLCConfig.SizeWriteBit.ToString();
                            txtPLCStartOutW.Text = _equipmentConfig.PLCConfig.WriteStartWordAddress.ToString();
                            txtPLCLengthOutW.Text = _equipmentConfig.PLCConfig.SizeWriteWord.ToString();

                            cbbplcConnectType.SelectedItem = _equipmentConfig.PLCConfig.PlcConnectType;
                            var ipPlcSegments = _equipmentConfig.PLCConfig.IpPlc.Split('.');
                           
                        }
                        //cimConfig
                        {
                            txtIp.Text = _equipmentConfig.CimConfig.IP.ToString();
                            txtConectMode.Text = _equipmentConfig.CimConfig.ConnectMode.ToString();
                            txtPort.Text = _equipmentConfig.CimConfig.Port.ToString();

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

        #endregion
    }
}
