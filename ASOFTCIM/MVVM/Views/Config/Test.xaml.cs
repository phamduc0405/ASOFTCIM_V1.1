using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.Models;
using ASOFTCIM.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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
using System.Xml.Linq;

namespace ASOFTCIM.MVVM.Views.Config
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : UserControl
    {
        private Controller _controller;
        private Thread _alarmReaderThread;
        private bool _isReadingAlarm = false;
        public Test()
        {
            InitializeComponent();
            Initial();
            CreaterEvent();
        }
        private void Initial()
        {
            _controller = MainWindowViewModel.Controller;
        }
        private void CreaterEvent()
        {
            btnTest.Click += (s, e) =>
            {
                //Controller.ACIM.SendS1F1();
                string namespaces = "ASOFTCIM.Message.PLC2Cim.Send";
                Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), namespaces);
                Type t = Assembly.GetExecutingAssembly().GetType($"{namespaces}.{txtNameClass.Text}");

                if (t != null && typelist.Contains(t))
                {
                    // Tạo instance của class với constructor có tham số
                    //PLCHelper plcHelper = new PLCHelper();
                    string Name = $"{txtNameClass.Text}";
                    string classss = $"{txtNameObj.Text}";
                    object batchlotInstance = CreateInstanceFromName(Name, classss);

                    //BATCHLOT batchlot = new BATCHLOT();

                    try
                    {
                        string m = "";
                        object instance = Activator.CreateInstance(t, new object[] { _controller.CIM.PLCH, batchlotInstance });

                    }
                    catch (Exception ex)
                    {
                        var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.",
                        MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                        LogTxt.Add(LogTxt.Type.Exception, debug);
                    }
                }
            };
            btnS2F17.Click += (s, e) => 
            {
                _controller.CIM.SendS2F17(_controller.CIM.Cim.SysPacket);
            };
            btnS1F1.Click += (s, e) =>
            {
                _controller.CIM.SendS1F1(_controller.CIM.Cim.Conn);

            };
            btnS6F1Start.Click += (s, e) =>
            {
                List<SV> svs = new List<SV>();
                svs = _controller.CIM.EqpData.SVID;
                TRACESV tRACESV = new TRACESV();
                List<string> lstSvid = new List<string>();
                foreach( var Item in svs)
                {
                    lstSvid.Add(Item.SVID);
                }
                var trid = txtTrid.Text;
                var dsper = int.Parse(txtDsper.Text);
                var totsmp = int.Parse(txtTotsmp.Text);
                var repgsz = txtRepgsz.Text;
                tRACESV.Init(lstSvid, trid, dsper, totsmp, repgsz);
                tRACESV.SMPLN = int.Parse(txtTotsmp.Text);
                tRACESV.Start();
                _controller.CIM.SendS6F1(svs, tRACESV);
            };
            btnS6F1Stop.Click += (s, e) =>
            {
                List<SV> svs = new List<SV>();
                svs = _controller.CIM.EqpData.SVID;
                TRACESV tRACESV = new TRACESV();
                List<string> lstSvid = new List<string>();
                foreach (var Item in svs)
                {
                    lstSvid.Add(Item.SVID);
                }
                var trid = txtTrid.Text;
                var dsper = int.Parse(txtDsper.Text);
                var totsmp = int.Parse(txtTotsmp.Text);
                var repgsz = txtRepgsz.Text;
                tRACESV.Init(lstSvid, trid, dsper, totsmp, repgsz);
                tRACESV.SMPLN = int.Parse(txtTotsmp.Text);
                tRACESV.Stop();
                tRACESV.TraceSvEvent += (lstSv, isEnd) =>
                {
                    _controller.CIM.SendS6F1(svs, tRACESV);
                    if (tRACESV.SMPLN == tRACESV.TOTSMP)
                    {
                        //tRACESV.Remove(tRACESV.First(x => x.TRID == tRACESV.TRID));
                        tRACESV.Stop();
                    }
                };
                
            };
            btnS5F1Set.Click += (s, e) =>
            {

                Data.Alarm alarm = new Data.Alarm();
                //if (!int.TryParse(txtAlarmID.Text, out int al))
                //{
                //    return;
                //}
                alarm = _controller.CIM.EqpData.ALS[int.Parse(txtAlarmID.Text)];
                alarm.ALST = "1";
                _controller.CIM.SendS5F1(alarm);


            };
            btnS5F1Reset.Click += (s, e) =>
            {
                Data.Alarm alarm = new Data.Alarm();
                if (!int.TryParse(txtAlarmID.Text, out int al))
                {
                    return;
                }
                alarm = _controller.CIM.EqpData.ALS[int.Parse(txtAlarmID.Text)];
                alarm.ALST = "2";
                _controller.CIM.SendS5F1(alarm);
            };
            btnS5F1Start.Click += (s, e) =>
            {
                //_controller.CIM.PLCH.TestAlarm = true;
                // StartAlarmReader();
                _controller.CIM.StartSiMulatorAlarm();

            };
            btnS5F1Stop.Click += (s, e) =>
            {
                //_controller.CIM.PLCH.TestAlarm = false;
                //if (_alarmReaderThread != null && _alarmReaderThread.IsAlive)
                //{
                //    _isReadingAlarm = false;
                //    _alarmReaderThread.Join();
                //    _alarmReaderThread = null;
                //}
                _controller.CIM.StopSiMulatorAlarm();

            };
        }
        private void StartAlarmReader()
        {
            if (_alarmReaderThread != null && _alarmReaderThread.IsAlive)
                return;

            _isReadingAlarm = true;
            _alarmReaderThread = new Thread(() => AlarmReader());
            _alarmReaderThread.IsBackground = true;
            _alarmReaderThread.Start();
        }
        private void AlarmReader()
        {

            while (_isReadingAlarm)
            {
                try
                {
                    Data.Alarm alarm = new Data.Alarm();
                    alarm = _controller.CIM.EqpData.ALS[1];
                    alarm.ALST = "2";
                    _controller.CIM.SendS5F1(alarm);
                    alarm = _controller.CIM.EqpData.ALS[2];
                    alarm.ALST = "2";
                    _controller.CIM.SendS5F1(alarm);
                    alarm = _controller.CIM.EqpData.ALS[3];
                    alarm.ALST = "2";
                    _controller.CIM.SendS5F1(alarm);
                    alarm = _controller.CIM.EqpData.ALS[4];
                    alarm.ALST = "2";
                    _controller.CIM.SendS5F1(alarm);
                    alarm = _controller.CIM.EqpData.ALS[5];
                    alarm.ALST = "2";
                    _controller.CIM.SendS5F1(alarm);
                    alarm = _controller.CIM.EqpData.ALS[6];
                    alarm.ALST = "2";
                    _controller.CIM.SendS5F1(alarm);
                    alarm = _controller.CIM.EqpData.ALS[7];
                    alarm.ALST = "2";
                    _controller.CIM.SendS5F1(alarm);
                    alarm = _controller.CIM.EqpData.ALS[8];
                    alarm.ALST = "2";
                    _controller.CIM.SendS5F1(alarm);
                    alarm = _controller.CIM.EqpData.ALS[9];
                    alarm.ALST = "2";
                    _controller.CIM.SendS5F1(alarm);
                    alarm = _controller.CIM.EqpData.ALS[10];
                    alarm.ALST = "2";
                    _controller.CIM.SendS5F1(alarm);
                    alarm = _controller.CIM.EqpData.ALS[11];
                    alarm.ALST = "2";
                    _controller.CIM.SendS5F1(alarm);
                    alarm = _controller.CIM.EqpData.ALS[12];
                    alarm.ALST = "2";
                    _controller.CIM.SendS5F1(alarm);
                    alarm = _controller.CIM.EqpData.ALS[13];
                    alarm.ALST = "2";
                    _controller.CIM.SendS5F1(alarm);
                    alarm = _controller.CIM.EqpData.ALS[14];
                    alarm.ALST = "2";
                    _controller.CIM.SendS5F1(alarm);
                    alarm = _controller.CIM.EqpData.ALS[15];
                    alarm.ALST = "2";
                    _controller.CIM.SendS5F1(alarm);
                    alarm = _controller.CIM.EqpData.ALS[16];
                    alarm.ALST = "2";
                    _controller.CIM.SendS5F1(alarm);

                }
                catch (Exception ex)
                {
                    LogTxt.Add(LogTxt.Type.Exception, $"Class:{this.GetType().Name} Method:{MethodBase.GetCurrentMethod().Name} exception: {ex.Message}");
                }

                Thread.Sleep(100);
            }
        }
        public Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }
        public static object CreateInstanceFromName(string Name, string classs)
        {
            string fullClassName = $"ASOFTCIM.Data.{classs}"; // Định dạng namespace + class

            // Lấy Type từ Assembly
            Type t = Assembly.GetExecutingAssembly().GetType(fullClassName);
            if (t == null) return null; // Trả về null nếu không tìm thấy

            try
            {
                return Activator.CreateInstance(t); // Tạo instance
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.",
                        MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
                return null;
            }
        }
    }
}
