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
            _controller = MainWindowViewModel.Controller;
            _alarmView = new AlarmModel();
            UpdateAlarm();
            _controller.CIM.ResetEvent -= UpdateAlarm;
            _controller.CIM.ResetEvent += UpdateAlarm;
        }
        private void UpdateAlarm()
        {
            try
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    try
                    {
                        _alarmView.AlarmList.Clear();
                        var tempList = _controller.CIM.EqpData.AlarmHistory.ToList();
                        foreach (var item in tempList)
                        {
                            _alarmView.AlarmList.Add(item);
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
