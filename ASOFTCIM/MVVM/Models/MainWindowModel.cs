using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASOFTCIM.MVVM.Models
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        #region Fields
        public static Controller Controller;
        private object _currentview = new object();
        private string _user = "User";
        private string _pass = "2";
        public static int LeveLogin = 0;
        private string _logInOut = "LogIn";

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        private MainViewModel viewModel;
        private static bool _running = true;

        private string _datetime;
        private string Version = "Version 1.0.0";
        #endregion
        #region Property
        public object Currentview
        {
            get { return _currentview; }
            set
            {
                _currentview = value;
                OnPropertyChanged(nameof(Currentview));
            }
        }
        public static bool Running
        {
            get
            {
                return _running;
            }
        }
        public CancellationTokenSource CancellationTokenSource
        {
            get { return _cancellationTokenSource; }
            set
            {
                _cancellationTokenSource = value;
                OnPropertyChanged(nameof(CancellationTokenSource));
            }
        }
        public CancellationToken CancellationToken
        {
            get { return _cancellationToken; }
            set
            {
                _cancellationToken = value;
                OnPropertyChanged(nameof(CancellationToken));
            }
        }
        public string DateTime
        {
            get { return _datetime; }
            set
            {
                _datetime = value;
                OnPropertyChanged(nameof(DateTime));
            }
        }
        public string VersionInfo
        {
            get { return Version; }
            set
            {
                Version = value;
                OnPropertyChanged(nameof(VersionInfo));
            }
        }
        public string User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public string LogInOut
        {
            get { return _logInOut; }
            set
            {
                _logInOut = value;
                OnPropertyChanged(nameof(LogInOut));
            }
        }


        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
