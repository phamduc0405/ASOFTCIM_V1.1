using A_SOFT.CMM.HELPER;
using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using ASOFTCIM.Config;
using ASOFTCIM.Data;
using ASOFTCIM.Helper;
using LiveCharts.Maps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASOFTCIM.MainControl
{
    public class Controller
    {
        private ACIM _cim;
        private EquipmentConfig _equipmentConfig = new EquipmentConfig();
        private bool _plcConnect = false;
        private int _countconnectPLC = 0;
        private bool _cimConnect = false;
        public EquipmentConfig EquipmentConfig
        {
            get { return _equipmentConfig; }
            set { _equipmentConfig = value; }
        }
        public ACIM CIM
        {
            get { return _cim; }
            set { _cim = value;}
        }
        public bool CimConnect
        {
            get { return _cimConnect; }
            set { _cimConnect = value; }
        }

        public Controller()
        {
            DefaultData.AppPath = @"C:\CimConfig";
            ReadControllerConfig();
            DefaultData.LogPath = $"{_equipmentConfig.LogFolder}";
            _cim = new ACIM(_equipmentConfig);
            _cim.PlcConnectChangeEvent += PlcConnectEvent;
            _cim.Cim.Conn.OnConnectEvent += OnConnectEvent;
            LogTxt.FileSize = int.Parse(_equipmentConfig.SizeFile);
        }
        public void Stop()
        {
            _cim.Stop();
        }
        private void ReadControllerConfig()
        {
            try
            {
                if (File.Exists(DefaultData.AppPath + @"\Setting\SystemConfig.setting"))
                {
                    string readText = File.ReadAllText(DefaultData.AppPath + @"\Setting\SystemConfig.setting");
                    _equipmentConfig = XmlHelper<EquipmentConfig>.DeserializeFromString(readText);
                    foreach (var b in _equipmentConfig.PLCHelper.Bits)
                    {
                        List<WordModel> wm = _equipmentConfig.PLCHelper.Words.Where(x => x.BitEvent==($"{b.PLCDevice}{b.PLCHexAdd}") || x.BitEvent == ($"{b.PLCDevice}{b.PCHexAdd}")).ToList();
                        b.LstWord.AddRange(wm);
                        List<MaterialModel> material = _equipmentConfig.PLCHelper.Materrials.Where(x => x.BitEvent.Contains($"{b.PLCDevice}{b.PLCHexAdd}")).ToList();
                        b.LstWord.AddRange(material);

                    }
                }
                else
                {
                    //_equipmentConfig.PathLog = DefaultData.LogPath;
                    //_equipmentConfig.DelLog = 30;
                }
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
        }
        public void SaveControllerConfig()
        {
            
            string str = XmlHelper<EquipmentConfig>.SerializeToString(_equipmentConfig);
            try
            {
                Task.Run(() =>
                {
                    string path = DefaultData.AppPath + @"\Setting";
                    DefaultData.CheckFolder(path);
                    path += @"\SystemConfig.setting";
                    File.WriteAllText(path, str);
                });
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
        }
        private async void SendWhenStart() 
        {
            await Task.Delay(1000);
            _cim.SendS1F1(_cim.Cim.Conn);
            await Task.Delay(1000);
            _cim.EqpData.EQINFORMATION.CRST = "1";
            WordModel crst = _cim.PLCH.Words.FirstOrDefault(x => x.Item == "CRST");
            if (crst.GetValue() != _cim.EqpData.EQINFORMATION.CRST)
            {
                _cim.EqpData.EQINFORMATION.CRST = crst.GetValue();
                string ceiID = "106";
                    _cim.SendS6F11_104_106(ceiID);
            }
            await Task.Delay(10);
            SendAlarmWhenStart();
        }
        private async void OnConnectEvent(bool isConnect)
        {
            if (isConnect)
            {
                _cimConnect = true;
                if (_plcConnect)
                {
                    SendWhenStart();
                    _countconnectPLC++;
                }
                return;
            }
            _countconnectPLC = 0;
            _cimConnect = false;
        }
        private async void PlcConnectEvent(bool isConnect)
        {
            if (isConnect)
            {
                _plcConnect = true;
                if(_cimConnect && _countconnectPLC == 0 )
                {
                    SendWhenStart();
                    _countconnectPLC++;
                }
                return;
            }
            _countconnectPLC = 0;
            _plcConnect = false;
        }
        private void SendAlarmWhenStart()
        {
            try
            {
                List<Alarm> Alarmlst = new List<Alarm>();
                Alarmlst = _cim.EqpData.CurrAlarm;
                foreach (var item in Alarmlst)
                {
                    _cim.SendS5F1(item);
                }
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
             
        }
    }
}
