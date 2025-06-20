using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;
using ASOFTCIM.MainControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ASOFTCIM.MVVM.Models;
using ASOFTCIM.MVVM.ViewModels;
using System.Collections.ObjectModel;

namespace ASOFTCIM.MVVM.ViewModels
{
    public class AlarmViewModel : BaseViewModels
    {
        private Controller _controller;
        private AlarmModel _alarmView;
        public AlarmModel AlarmView
        {
            get => _alarmView;
            set { _alarmView = value; OnPropertyChanged(nameof(AlarmView)); }
        }
        public AlarmViewModel()
        {
            _controller = Controller.Instange;
            _alarmView = new AlarmModel();
            UpdateAlarm();
            _controller.CIM.ResetEvent -= UpdateAlarm;
            _controller.CIM.ResetEvent += UpdateAlarm;
        }
        private void UpdateAlarm()
        {
            Task.Run(() =>
            {
                try
                {
                    // Tạo danh sách mới ngoài UI thread
                    var tempList = _controller.CIM.EqpData.AlarmHistory.ToList();
                    var newCollection = new ObservableCollection<Data.Alarm>(tempList);

                    // Gán vào AlarmList trên UI thread
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        try
                        {
                            AlarmView.AlarmList = newCollection;
                        }
                        catch (Exception ex)
                        {
                            var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.",
                                MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                            LogTxt.Add(LogTxt.Type.Exception, debug);
                        }
                    });
                }
                catch (Exception ex)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.",
                        this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.",
                        this.GetType().Name, MethodBase.GetCurrentMethod().Name, t.Exception?.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopDispatcherTimer();
                _controller.CIM.ResetEvent -= UpdateAlarm;
            }
            base.Dispose(disposing);
        }
        ~AlarmViewModel()
        {
            Dispose(false);
        }
        
    }
}
