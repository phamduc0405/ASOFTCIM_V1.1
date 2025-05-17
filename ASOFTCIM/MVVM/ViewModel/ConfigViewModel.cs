using A_SOFT.CMM.INIT;
using ASOFTCIM.Config;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ASOFTCIM.Init;
using static ASOFTCIM.Init.LibMethod;
using System.Windows.Forms;
using A_SOFT.PLC;
using ASOFTCIM.MVVM.View.Popup;
using System.Windows;
using System.IO;

namespace ASOFTCIM.MVVM.ViewModel
{
    public class ConfigViewModel : INotifyPropertyChanged, IDisposable
    {
        private Controller _controller;
        private ConfigModel _config;
        private EquipmentConfig _equipmentConfig;
        private Thread _updateData;
        private static bool _running = true;
        private bool _disposed = false;
        public ICommand btnSavePLCConfig { get; }
        public ICommand btnSaveEQPConfig { get; }
        public ICommand btnSaveCimConfig { get; }
        public ICommand btnDirPLCExcel { get; }
        public ICommand btnDirLog { get; }
        public ICommand btnDirLogFDC { get; }
        public ConfigModel Configs
        {
            get { return _config; }
            set
            {
                _config = value;
                OnPropertyChanged(nameof(Configs));
            }
        }
        public ConfigViewModel()
        {
            _controller = MainWindow.Controller;
            _config = new ConfigModel();
            _equipmentConfig = _controller.EquipmentConfig;

            if (_equipmentConfig == null)
            {
                _equipmentConfig = new EquipmentConfig();
            }
            Loaded();
            btnDirPLCExcel = new RelayCommand(_ =>
            {
                var path = LibMethod.SelectFile(extension.excel);
                if (!string.IsNullOrEmpty(path))
                {
                    Configs.PathMapInterface = path;
                }
            });
            btnDirLog = new RelayCommand(_ =>
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                folderBrowser.Description = "Chọn thư mục";

                // Hiển thị cửa sổ dialog và lấy đường dẫn thư mục nếu người dùng chọn
                DialogResult result = folderBrowser.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string folderPath = folderBrowser.SelectedPath;
                    // Sử dụng đường dẫn thư mục ở đây
                    _config.LogFolder = folderPath;
                }
            });
            btnDirLogFDC = new RelayCommand(_ =>
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                folderBrowser.Description = "Chọn thư mục";

                // Hiển thị cửa sổ dialog và lấy đường dẫn thư mục nếu người dùng chọn
                DialogResult result = folderBrowser.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string folderPath = folderBrowser.SelectedPath;
                    // Sử dụng đường dẫn thư mục ở đây
                    _config.LogFodlerFDC = folderPath;
                }
            });
            btnSavePLCConfig = new AsyncRelayCommand(async () =>
            {

                try
                {
                    var debug = $"Class:{this.GetType().Name} Method:{MethodBase.GetCurrentMethod().Name} >.";
                    LogTxt.Add(LogTxt.Type.UI, debug);
                    _equipmentConfig.PLCConfig = new PLCConfig()
                    {
                        Channel = short.Parse(_config.PlcChannel),
                        NetworkNo = short.Parse(_config.PlcNetWork),
                        Path = int.Parse(_config.plcPath),
                        StationNo = int.Parse(_config.PlcStation),
                        IsCCLinkIe = _config.UserCCLinkIE,

                        ReadStartBitAddress = _config.PlcStartInputBit,
                        SizeReadBit = int.Parse(_config.PlcStartInputBitLength),
                        ReadStartWordAddress = _config.PlcStartInputWord,
                        SizeReadWord = int.Parse(_config.PlcStartInputWordLength),

                        WriteStartBitAddress = _config.PlcStartOutputBit,
                        SizeWriteBit = int.Parse(_config.PlcStartOutputBitLength),
                        WriteStartWordAddress = _config.PlcStartOutputWord,
                        SizeWriteWord = int.Parse(_config.PlcStartOutputWordLength),
                        PlcConnectType = (PlcConnectType)_config.PlcConnectType,
                    };
                    if (File.Exists(_config.PathMapInterface))
                    {
                        SavePLCConfigDisplay display = new SavePLCConfigDisplay(_config.PathMapInterface);
                        display.ShowDialog();
                    }
                    await SaveConfig();
                    await LoadConfig();
                }
                catch (Exception ex)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            });
            btnSaveEQPConfig = new AsyncRelayCommand(async () =>
            {
                try
                {
                    _equipmentConfig.EQPID = _config.EquipMent;
                    _equipmentConfig.LogFolder = _config.LogFolder;
                    _equipmentConfig.AliveTime = _config.AliveTime;
                    _equipmentConfig.UseLogFDC = _config.UseLogFDC;
                    _equipmentConfig.LogFDC = _config.LogFodlerFDC;

                    var debug = $"Class:{this.GetType().Name} Method:{MethodBase.GetCurrentMethod().Name} >.";
                    LogTxt.Add(LogTxt.Type.UI, debug);
                    await SaveConfig();
                    await LoadConfig();
                }
                catch (Exception ex)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                    LogTxt.ChangePathLog();
                }
            });
            btnSaveCimConfig = new AsyncRelayCommand(async () =>
            {
                try
                {
                    _equipmentConfig.CimConfig = new CimConfig()
                    {
                        IP = _config.Ip,
                        ConnectMode = _config.ConnectMode,
                        Port = _config.Port,
                    };
                    var debug = $"Class:{this.GetType().Name} Method:{MethodBase.GetCurrentMethod().Name} >.";
                    LogTxt.Add(LogTxt.Type.UI, debug);
                    await SaveConfig();
                    await LoadConfig();
                }
                catch (Exception ex)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            });
        }
        private async Task SaveConfig()
        {
            await Task.Run(async () =>
            {
                _controller.SaveControllerConfig();
            });

        }
        public async void Loaded()
        {
            await LoadConfig();
        }
        private async Task LoadConfig()
        {
            await Task.Run(() =>
            {
                try
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        // Thực hiện cập nhật giao diện người dùng ở đây
                        //EqpConfig
                        {
                            _config.EquipMent = _equipmentConfig.EQPID;
                            _config.LogFolder = _equipmentConfig.LogFolder;
                            _config.AliveTime = _equipmentConfig.AliveTime;
                            _config.LogFodlerFDC = _equipmentConfig.LogFDC;
                            _config.UseLogFDC = _equipmentConfig.UseLogFDC;

                        }

                        //PlcConfig
                        {
                            _config.PlcChannel = _equipmentConfig.PLCConfig.Channel.ToString();
                            _config.PlcNetWork = _equipmentConfig.PLCConfig.NetworkNo.ToString();
                            _config.plcPath = _equipmentConfig.PLCConfig.Path.ToString();
                            _config.PlcStation = _equipmentConfig.PLCConfig.StationNo.ToString();
                            _config.UserCCLinkIE = _equipmentConfig.PLCConfig.IsCCLinkIe;


                            _config.PlcStartInputBit = _equipmentConfig.PLCConfig.ReadStartBitAddress.ToString();
                            _config.PlcStartInputBitLength = _equipmentConfig.PLCConfig.SizeReadBit.ToString();
                            _config.PlcStartInputWord = _equipmentConfig.PLCConfig.ReadStartWordAddress.ToString();
                            _config.PlcStartInputWordLength = _equipmentConfig.PLCConfig.SizeReadWord.ToString();

                            _config.PlcStartOutputBit = _equipmentConfig.PLCConfig.WriteStartBitAddress.ToString();
                            _config.PlcStartOutputBitLength = _equipmentConfig.PLCConfig.SizeWriteBit.ToString();
                            _config.PlcStartOutputWord = _equipmentConfig.PLCConfig.WriteStartWordAddress.ToString();
                            _config.PlcStartOutputWordLength = _equipmentConfig.PLCConfig.SizeWriteWord.ToString();

                            _config.PlcConnectType = _equipmentConfig.PLCConfig.PlcConnectType;
                            var ipPlcSegments = _equipmentConfig.PLCConfig.IpPlc.Split('.');

                        }
                        //cimConfig
                        {
                            _config.Ip = _equipmentConfig.CimConfig.IP.ToString();
                            _config.ConnectMode = _equipmentConfig.CimConfig.ConnectMode.ToString();
                            _config.Port = _equipmentConfig.CimConfig.Port.ToString();

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
        public void StopThread()
        {
            _running = false;
            if (_updateData != null && _updateData.IsAlive)
            {
                _updateData.Join();
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
                    //_controller.CIM.PlcConnectChangeEvent -= Controller_PlcConnectChangeEvent;
                    //_controller.CIM.Cim.Conn.OnConnectEvent -= Controller_CimConnectChangeEvent;
                    //_controller.CIM.ResetEvent -= UpdateAlarm;
                    //_controller.CIM.Cim2HostChangeEvent -= UpdateHostcimMessage;
                    //_controller.CIM.Host2CimChangeEvent -= UpdateHostcimMessage;
                    //_controller.CIM.Plc2CimChangeEvent -= UpdatePlccimMessage;
                    //_controller.CIM.Cim2PlcChangeEvent -= UpdatePlccimMessage;
                }
                _disposed = true;
            }
        }
        ~ConfigViewModel()
        {
            Dispose(false);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
