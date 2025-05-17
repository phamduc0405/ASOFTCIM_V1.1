using ASOFTCIM.Data;
using ASOFTCIM.MainControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Runtime.Remoting.Contexts;
using ASOFTCIM.Init;
using ASOFTCIM.MVVM.Model;
using ASOFTCIM.MVVM.View.Alarm;

namespace ASOFTCIM.MVVM.ViewModel
{
    public class FDCViewModel : INotifyPropertyChanged, IDisposable
    {
        #region Fields
        private Controller _controller;
        private ASOFTCIM.MVVM.Model.FDCModel _fDCModel;
        private Thread _updateData;
        private static bool _running = true;
        private bool _disposed = false;

        private int _index = 0;
        private int _totalsvid = 0;
        private DispatcherTimer _timer;
        public ICommand NextCommand { get; }
        public ICommand BackCommand { get; }


        #endregion

        #region Properties
        public ASOFTCIM.MVVM.Model.FDCModel FDCModel
        {
            get => _fDCModel;
            set { _fDCModel = value; OnPropertyChanged(nameof(FDCModel)); }
        }
        #endregion

        #region Constructor
        public FDCViewModel()
        {
            _controller = MainWindow.Controller;
            _fDCModel = new ASOFTCIM.MVVM.Model.FDCModel();
            _fDCModel.AllSVIDs = new ObservableCollection<SV>(_controller.CIM.EqpData.SVID);
            _totalsvid = _fDCModel.AllSVIDs.Count;
            _index = 0;

            _fDCModel.CurrentSVIDsL = new ObservableCollection<SV>();
            _fDCModel.CurrentSVIDsR = new ObservableCollection<SV>();
            _totalsvid = _controller.CIM.EqpData.SVID.Count();
            ShowSvid(_index);

            NextCommand = new RelayCommand(_ => OnNext(), _ => CanNext());
            BackCommand = new RelayCommand(_ => OnBack(), _ => CanBack());

            StartTimer();
            //StartTimer();
        }
        #endregion
        private void OnNext()
        {
            if ((_index + 1) * 50 < _totalsvid)
            {
                _index++;
                ShowSvid(_index);
            }
        }

        private void OnBack()
        {
            if (_index > 0)
            {
                _index--;
                ShowSvid(_index);
            }
        }

        private bool CanNext() => (_index + 1) * 50 < _totalsvid;
        private bool CanBack() => _index > 0;

        private void ShowSvid(int index)
        {
            _fDCModel.CurrentSVIDsL.Clear();
            _fDCModel.CurrentSVIDsR.Clear();

            int baseIndex = index * 50;
            int leftStart = baseIndex;
            int leftEnd = Math.Min(leftStart + 25, _totalsvid);
            int rightStart = baseIndex + 25;
            int rightEnd = Math.Min(rightStart + 25, _totalsvid);
            var svidList = _controller.CIM.EqpData.SVID;
            for (int i = leftStart; i < leftEnd; i++)
            {
                var item = svidList[i];
                item.SVVALUE = FormatSVValue(item);
                _fDCModel.CurrentSVIDsL.Add(item);
            }


            for (int i = rightStart; i < rightEnd; i++)
            {
                var item = svidList[i];
                item.SVVALUE = FormatSVValue(item);
                _fDCModel.CurrentSVIDsR.Add(item);
            }
        }

        private void StartTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += (s, e) => UpdateSvidData();
            _timer.Start();
        }

        private void UpdateSvidData()
        {
            ShowSvid(_index);
            //foreach (var sv in _fDCModel.CurrentSVIDsL)
            //{
            //    if (float.TryParse(sv.SVVALUE, out float currentValue))
            //    {
            //        sv.SVVALUE = (currentValue + 1).ToString();
            //    }
            //}
            //OnPropertyChanged(nameof(_fDCModel.CurrentSVIDsL));
            //foreach (var sv in CurrentSVIDsR)
            //{
            //    if (float.TryParse(sv.SVVALUE, out float currentValue))
            //    {
            //        sv.SVVALUE = (currentValue + 1).ToString();
            //    }
            //}

            //// Gọi PropertyChanged cho toàn bộ list
            //OnPropertyChanged(nameof(CurrentSVIDsR));
        }

        private string FormatSVValue(SV item)
        {
            if (!float.TryParse(item.SVVALUE, out float result))
                return item.SVVALUE;

            switch (item.SVID)
            {
                case "10003":
                case "10004":
                case "10025":
                case "10038":
                    return (result / 100).ToString();
                case "10010":
                case "60000":
                case "60002":
                case "60006":
                case "60008":
                    return (result / 10).ToString();
                case "60001":
                case "60007":
                    return (result / 1000).ToString();
                case "10026":
                case "10030":
                case "10031":
                case "10032":
                case "10034":
                case "60003":
                case "60009":
                case "10037":
                case "10039":
                case "10040":
                    return result.ToString();
                default:
                    return item.SVVALUE;
            }
        }
        public void StopThread()
        {
            _running = false;
            _timer?.Stop();
            if (_updateData != null && _updateData.IsAlive)
            {
                _updateData.Join(1000);
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
                }
                _disposed = true;
            }
        }
        ~FDCViewModel()
        {
            Dispose(false);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
