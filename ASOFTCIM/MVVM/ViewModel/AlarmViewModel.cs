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
using ASOFTCIM.MVVM.Model;

namespace ASOFTCIM.MVVM.ViewModel
{
    public class AlarmViewModel : INotifyPropertyChanged, IDisposable
    {
        private Controller _controller;
        private AlarmModel _alarmView;
        private Thread _updateData;
        private static bool _running = true;
        private bool _disposed = false;
        public AlarmModel AlarmView
        {
            get => _alarmView;
            set { _alarmView = value; OnPropertyChanged(nameof(AlarmView)); }
        }
        public AlarmViewModel()
        {
            _controller = MainWindow.Controller;
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
                    _controller.CIM.ResetEvent -= UpdateAlarm;
                }
                _disposed = true;
            }
        }
        ~AlarmViewModel()
        {
            Dispose(false);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
