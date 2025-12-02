using A_SOFT.CMM.INIT;
using ASOFTCIM.MainControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ASOFTCIM.MVVM.ViewModels
{
    public class BaseViewModels : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _disposed = false;
        protected Thread _backgroundThread;
        protected static bool _running = true;
        protected DispatcherTimer _timer;
        private EventHandler _timerTickHandler;
        /// <summary>
        /// Bắt đầu một luồng nền chạy hành động liên tục nếu _running = true.
        /// </summary>
        /// <param name="action">Hành động cần thực thi trong thread.</param>
        /// <param name="loop">Nếu true, hành động sẽ lặp vô hạn đến khi gọi StopThread.</param>
        /// <param name="intervalMs">Khoảng cách giữa các vòng lặp (nếu loop=true)</param>
        protected void StartThread(Action action, bool loop = false, int intervalMs = 1000)
        {
            //StopThread(); // Đảm bảo không có thread cũ đang chạy

            _running = true;
            _backgroundThread = new Thread(() =>
            {
                try
                {
                    if (loop)
                    {
                        while (_running)
                        {
                            action?.Invoke();
                            Thread.Sleep(intervalMs);
                        }
                    }
                    else
                    {
                        action?.Invoke();
                    }
                }

                catch (Exception ex)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            });

            _backgroundThread.IsBackground = true;
            _backgroundThread.Start();
        }
        protected void StartDispatcherTimer(Action action, int intervalMs = 1000)
        {
            StopDispatcherTimer(); // tránh chạy trùng

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(intervalMs);
            _timerTickHandler = (s, e) => action?.Invoke();
            _timer.Tick += _timerTickHandler;
            _timer.Start();
        }
        protected void StopDispatcherTimer()
        {
            if (_timer != null)
            {
                if (_timerTickHandler != null)
                {
                    _timer.Tick -= _timerTickHandler;
                    _timerTickHandler = null;
                }

                _timer.Stop();
                _timer = null;
            }
        }
        /// <summary>
        /// Dừng luồng đang chạy (nếu có).
        /// </summary>
        public void StopThread()
        {
            _running = false;
            if (_backgroundThread != null && _backgroundThread.IsAlive)
            {
                _backgroundThread.Join();
                _backgroundThread = null;
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
                    StopDispatcherTimer();
                }
                _disposed = true;
            }
        }

        ~BaseViewModels()
        {
            Dispose(false);
        }
    }
}
