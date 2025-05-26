using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Input;

namespace ASOFTCIM.MVVM.Behaviors
{
    public class InactivityMonitor
    {
        private readonly DispatcherTimer _timer;
        private DateTime _lastActivity;
        private readonly TimeSpan _timeout;

        public event Action TimeoutReached;

        public InactivityMonitor(TimeSpan timeout)
        {
            _timeout = timeout;
            _lastActivity = DateTime.Now;

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            _timer.Tick += CheckInactivity;

            EventManager.RegisterClassHandler(typeof(Window),
                UIElement.PreviewMouseMoveEvent,
                new MouseEventHandler(OnActivity));
            EventManager.RegisterClassHandler(typeof(Window),
                UIElement.PreviewKeyDownEvent,
                new KeyEventHandler(OnActivity));
        }

        public void Start() => _timer.Start();
        public void Stop() => _timer.Stop();

        private void OnActivity(object sender, EventArgs e)
        {
            _lastActivity = DateTime.Now;
        }

        private void CheckInactivity(object sender, EventArgs e)
        {
            if (DateTime.Now - _lastActivity > _timeout)
            {
                Stop();
                TimeoutReached?.Invoke();
            }
        }
    }
}
