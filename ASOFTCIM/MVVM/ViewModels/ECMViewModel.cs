using ASOFTCIM.Data;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ASOFTCIM.MVVM.ViewModels
{
    public class ECMViewModel : BaseViewModels
    {
        #region Fields
        private Controller _controller;
        private ASOFTCIM.MVVM.Models.ECMModel _eCMMOdel;
        private Thread _updateData;
        private static bool _running = true;
        private bool _disposed = false;
        private int _index = 0;
        private int _totalecm = 0;
        private DispatcherTimer _timer;
        #endregion
        #region Properties
        public ASOFTCIM.MVVM.Models.ECMModel ECMModel
        {
            get => _eCMMOdel;
            set { _eCMMOdel = value; OnPropertyChanged(nameof(ECMModel)); }
        }
        #endregion
        public ECMViewModel(Controller controller, ASOFTCIM.MVVM.Models.ECMModel eCMModel)
        {
            _controller = controller;
            _eCMMOdel = eCMModel;
            _eCMMOdel.AllECs = new ObservableCollection<EC>(_controller.CIM.EqpData.ECS);
            _totalecm = _eCMMOdel.AllECs.Count;
            _index = 0;

            _eCMMOdel.CurrentECsL = new ObservableCollection<EC>();
            _eCMMOdel.CurrentECsR = new ObservableCollection<EC>();
            _totalecm = _controller.CIM.EqpData.ECS.Count();
            ShowECM(_index);

            //NextCommand = new RelayCommand(_ => OnNext(), _ => CanNext());
            //BackCommand = new RelayCommand(_ => OnBack(), _ => CanBack());
            StartDispatcherTimer(UpdateECMData, 100);

        }
        private void UpdateECMData()
        {
            ShowECM(_index);
        }
        private void ShowECM(int index)
        {
            _eCMMOdel.CurrentECsL.Clear();
            _eCMMOdel.CurrentECsR.Clear();

            int baseIndex = index * 50;
            int leftStart = baseIndex;
            int leftEnd = Math.Min(leftStart + 25, _totalecm);
            int rightStart = baseIndex + 25;
            int rightEnd = Math.Min(rightStart + 25, _totalecm);
            var svidList = _controller.CIM.EqpData.ECS;
            for (int i = leftStart; i < leftEnd; i++)
            {
                var item = svidList[i];
                item.ECDEF = FormatSVValue(item);
                _eCMMOdel.CurrentECsL.Add(item);
            }


            for (int i = rightStart; i < rightEnd; i++)
            {
                var item = svidList[i];
                item.ECDEF = FormatSVValue(item);
                _eCMMOdel.CurrentECsR.Add(item);
            }
        }
        private string FormatSVValue(EC item)
        {
            if (!float.TryParse(item.ECDEF, out float result))
                return item.ECDEF;

            switch (item.ECID)
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
                    return item.ECDEF;
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
        ~ECMViewModel()
        {
            Dispose(false);
        }
        
    }
}
