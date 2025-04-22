using A_SOFT.CMM.INIT;
using ASOFTCIM.Config;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.View.Home;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ASOFTCIM.MVVM.View.Alarm
{
    /// <summary>
    /// Interaction logic for AlarmView.xaml
    /// </summary>
    public partial class AlarmView : UserControl, INotifyPropertyChanged
    {
        private Controller _controller;
        private ObservableCollection<Data.Alarm> _alarmList = new ObservableCollection<Data.Alarm>();
        public ObservableCollection<Data.Alarm> AlarmList
        {
            get => _alarmList;
            set
            {
                _alarmList = value;
                OnPropertyChanged(nameof(AlarmList));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AlarmView()
        {
            InitializeComponent();
            this.DataContext = this;
            _controller = MainWindow.Controller;
            CreaterEvent();
            _controller.CIM.ResetEvent -= UpdateAlarm;
            _controller.CIM.ResetEvent += UpdateAlarm;
        }
        private void CreaterEvent()
        {
            Loaded += async (s, e) =>
            {
                try
                {
                    UpdateAlarm();
                }
                catch (Exception ex)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.",
                        MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            };

            Unloaded += (s, e) =>
            {
                _controller.CIM.ResetEvent -= UpdateAlarm;
            };
        }
        private void UpdateAlarm()
        {
            try
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    try
                    {
                        AlarmList.Clear(); 
                        var tempList = _controller.CIM.EqpData.AlarmHistory.ToList();
                        foreach (var item in tempList)
                        {
                            AlarmList.Add(item); 
                        }
                    }
                    catch (Exception ex)
                    {
                        var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.",
                        MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                        LogTxt.Add(LogTxt.Type.Exception, debug);
                    }
                }));
            }
            catch (Exception ex)
            {

                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);

            }
        }
    }
}
