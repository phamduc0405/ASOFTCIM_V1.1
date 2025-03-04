using A_SOFT.CMM.HELPER;
using A_SOFT.CMM.INIT;
using ASOFTCIM.Config;
using ASOFTCIM.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.MainControl
{
    public class Controller
    {
        private ACIM _cim;
        private EquipmentConfig _equipmentConfig;
        
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


        public Controller()
        {
            ReadControllerConfig();
            _cim = new ACIM(_equipmentConfig);
            
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
                    if (_equipmentConfig == null)
                    {
                        _equipmentConfig = new EquipmentConfig();
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
    }
}
