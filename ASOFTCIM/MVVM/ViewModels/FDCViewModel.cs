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
using ASOFTCIM.MVVM.Models;
using ASOFTCIM.MVVM.Views.Alarm;
using ASOFTCIM.MVVM.ViewModels;

namespace ASOFTCIM.MVVM.ViewModels
{
    public class FDCViewModel : BaseViewModels
    {
        #region Fields
        private Controller _controller;
        private ASOFTCIM.MVVM.Models.FDCModel _fDCModel;
        private Thread _updateData;
        private static bool _running = true;
        private bool _disposed = false;

        private int _index = 0;
        private int _totalsvid = 0;
        private DispatcherTimer _timer;

        private bool _isUpdating = false;
        public ICommand NextCommand { get; }
        public ICommand BackCommand { get; }


        #endregion

        #region Properties
        public ASOFTCIM.MVVM.Models.FDCModel FDCModel
        {
            get => _fDCModel;
            set { _fDCModel = value; OnPropertyChanged(nameof(FDCModel)); }
        }
        #endregion

        #region Constructor
        public FDCViewModel(Controller controller, ASOFTCIM.MVVM.Models.FDCModel fDCModel )
        {
            _controller = controller;
            _fDCModel = fDCModel;
            _fDCModel.AllSVIDs = new ObservableCollection<SV>(_controller.CIM.EqpData.SVID);
            _totalsvid = _fDCModel.AllSVIDs.Count;
            _index = 0;

            _fDCModel.CurrentSVIDsL = new ObservableCollection<SV>();
            _fDCModel.CurrentSVIDsR = new ObservableCollection<SV>();
            _totalsvid = _controller.CIM.EqpData.SVID.Count();
            ShowSvid(_index);

            NextCommand = new RelayCommand(_ => OnNext(), _ => CanNext());
            BackCommand = new RelayCommand(_ => OnBack(), _ => CanBack());
            StartDispatcherTimer(UpdateSvidData,500);

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
                //item.SVVALUE = FormatSVValue(item);
                _fDCModel.CurrentSVIDsL.Add(item);
            }


            for (int i = rightStart; i < rightEnd; i++)
            {
                var item = svidList[i];
                //item.SVVALUE = FormatSVValue(item);
                _fDCModel.CurrentSVIDsR.Add(item);
            }
        }

        private void ShowSvid1()
        {
            if (_isUpdating) return;
            _isUpdating = true;
            try
            {
                // Chỉ update SVVALUE, không clear/add:
                var svidList = _controller.CIM.EqpData.SVID;
                int baseIndex = _index * 50;

                for (int i = 0; i < _fDCModel.CurrentSVIDsL.Count; i++)
                {
                    int index = baseIndex + i;
                    if (index < svidList.Count)
                    {
                        var item = svidList[index];
                        _fDCModel.CurrentSVIDsL[i].SVVALUE = FormatSVValue(item);
                    }
                }

                for (int i = 0; i < _fDCModel.CurrentSVIDsR.Count; i++)
                {
                    int index = baseIndex + 25 + i;
                    if (index < svidList.Count)
                    {
                        var item = svidList[index];
                        _fDCModel.CurrentSVIDsR[i].SVVALUE = FormatSVValue(item);
                    }
                }
            }
            finally
            {
                _isUpdating = false;
            }
        }


        private void UpdateSvidData()
        {
            ShowSvid(_index);
            //TEST FDC 
            //ShowSvid1();
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopDispatcherTimer();
                
            }
            base.Dispose(disposing);
        }
        
        ~FDCViewModel()
        {
            Dispose(false);
        }
       
    }
}
