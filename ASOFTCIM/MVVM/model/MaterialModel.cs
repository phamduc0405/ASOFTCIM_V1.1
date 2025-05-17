using ASOFTCIM.MainControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ASOFTCIM.MVVM.Model
{
    public class MaterialModel : INotifyPropertyChanged
    {
        #region Fields
        #region P1
        private string _txtMaterialBatchID_P1;
        private string _txtMaterialPortID_P1;
        private string _txtMaterialState_P1;
        private string _txtAssembleQTY_P1;
        private string _txtTotalQTY_P1;
        private string _txtRemainQTY_P1;
        private string _txtNGQTY_P1;
        private string _txtUsedQTY_P1;
        private string _txtMaterialBatchID_kitting_P1;
        private string _txtMaterialPortID_kitting_P1;
        private string _txtReplyCode_kitting_P1;
        private string _txtReplyText_kitting_P1;
        #endregion
        #region P2
        private string _txtMaterialBatchID_P2;
        private string _txtMaterialPortID_P2;
        private string _txtMaterialState_P2;
        private string _txtAssembleQTY_P2;
        private string _txtTotalQTY_P2;
        private string _txtRemainQTY_P2;
        private string _txtNGQTY_P2;
        private string _txtUsedQTY_P2;
        private string _txtMaterialBatchID_kitting_P2;
        private string _txtMaterialPortID_kitting_P2;
        private string _txtReplyCode_kitting_P2;
        private string _txtReplyText_kitting_P2;
        #endregion
        #region P3
        private string _txtMaterialBatchID_P3;
        private string _txtMaterialPortID_P3;
        private string _txtMaterialState_P3;
        private string _txtAssembleQTY_P3;
        private string _txtTotalQTY_P3;
        private string _txtRemainQTY_P3;
        private string _txtNGQTY_P3;
        private string _txtUsedQTY_P3;
        private string _txtMaterialBatchID_kitting_P3;
        private string _txtMaterialPortID_kitting_P3;
        private string _txtReplyCode_kitting_P3;
        private string _txtReplyText_kitting_P3;
        #endregion
        #endregion
        #region Properties
        #region P1
        public string TxtMaterialBatchID_P1
        {
            get => _txtMaterialBatchID_P1;
            set { _txtMaterialBatchID_P1 = value; OnPropertyChanged(nameof(TxtMaterialBatchID_P1)); }
        }
        public string TxtMaterialPortID_P1
        {
            get => _txtMaterialPortID_P1;
            set { _txtMaterialPortID_P1 = value; OnPropertyChanged(nameof(TxtMaterialPortID_P1)); }
        }
        public string TxtMaterialState_P1
        {
            get => _txtMaterialState_P1;
            set { _txtMaterialState_P1 = value; OnPropertyChanged(nameof(TxtMaterialState_P1)); }
        }
        public string TxtAssembleQTY_P1
        {
            get => _txtAssembleQTY_P1;
            set { _txtAssembleQTY_P1 = value; OnPropertyChanged(nameof(TxtAssembleQTY_P1)); }
        }
        public string TxtTotalQTY_P1
        {
            get => _txtTotalQTY_P1;
            set { _txtTotalQTY_P1 = value; OnPropertyChanged(nameof(TxtTotalQTY_P1)); }
        }
        public string TxtRemainQTY_P1
        {
            get => _txtRemainQTY_P1;
            set { _txtRemainQTY_P1 = value; OnPropertyChanged(nameof(TxtRemainQTY_P1)); }
        }
        public string TxtNGQTY_P1
        {
            get => _txtNGQTY_P1;
            set { _txtNGQTY_P1 = value; OnPropertyChanged(nameof(TxtNGQTY_P1)); }
        }
        public string TxtUsedQTY_P1
        {
            get => _txtUsedQTY_P1;
            set { _txtUsedQTY_P1 = value; OnPropertyChanged(nameof(TxtUsedQTY_P1)); }
        }
        public string TxtMaterialBatchID_kitting_P1
        {
            get => _txtMaterialBatchID_kitting_P1;
            set { _txtMaterialBatchID_kitting_P1 = value; OnPropertyChanged(nameof(TxtMaterialBatchID_kitting_P1)); }
        }
        public string TxtMaterialPortID_kitting_P1
        {
            get => _txtMaterialPortID_kitting_P1;
            set { _txtMaterialPortID_kitting_P1 = value; OnPropertyChanged(nameof(TxtMaterialPortID_kitting_P1)); }
        }
        public string TxtReplyCode_kitting_P1
        {
            get => _txtReplyCode_kitting_P1;
            set { _txtReplyCode_kitting_P1 = value; OnPropertyChanged(nameof(TxtReplyCode_kitting_P1)); }
        }
        public string TxtReplyText_kitting_P1
        {
            get => _txtReplyText_kitting_P1;
            set { _txtReplyText_kitting_P1 = value; OnPropertyChanged(nameof(TxtReplyText_kitting_P1)); }
        }
        #endregion
        #region P2
        public string TxtMaterialBatchID_P2
        {
            get => _txtMaterialBatchID_P2;
            set { _txtMaterialBatchID_P2 = value; OnPropertyChanged(nameof(TxtMaterialBatchID_P2)); }
        }
        public string TxtMaterialPortID_P2
        {
            get => _txtMaterialPortID_P2;
            set { _txtMaterialPortID_P2 = value; OnPropertyChanged(nameof(TxtMaterialPortID_P2)); }
        }
        public string TxtMaterialState_P2
        {
            get => _txtMaterialState_P2;
            set { _txtMaterialState_P2 = value; OnPropertyChanged(nameof(TxtMaterialState_P2)); }
        }
        public string TxtAssembleQTY_P2
        {
            get => _txtAssembleQTY_P2;
            set { _txtAssembleQTY_P2 = value; OnPropertyChanged(nameof(TxtAssembleQTY_P2)); }
        }
        public string TxtTotalQTY_P2
        {
            get => _txtTotalQTY_P2;
            set { _txtTotalQTY_P2 = value; OnPropertyChanged(nameof(TxtTotalQTY_P2)); }
        }
        public string TxtRemainQTY_P2
        {
            get => _txtRemainQTY_P2;
            set { _txtRemainQTY_P2 = value; OnPropertyChanged(nameof(TxtRemainQTY_P2)); }
        }
        public string TxtNGQTY_P2
        {
            get => _txtNGQTY_P2;
            set { _txtNGQTY_P2 = value; OnPropertyChanged(nameof(TxtNGQTY_P2)); }
        }
        public string TxtUsedQTY_P2
        {
            get => _txtUsedQTY_P2;
            set { _txtUsedQTY_P2 = value; OnPropertyChanged(nameof(TxtUsedQTY_P2)); }
        }
        public string TxtMaterialBatchID_kitting_P2
        {
            get => _txtMaterialBatchID_kitting_P2;
            set { _txtMaterialBatchID_kitting_P2 = value; OnPropertyChanged(nameof(TxtMaterialBatchID_kitting_P2)); }
        }
        public string TxtMaterialPortID_kitting_P2
        {
            get => _txtMaterialPortID_kitting_P2;
            set { _txtMaterialPortID_kitting_P2 = value; OnPropertyChanged(nameof(TxtMaterialPortID_kitting_P2)); }
        }
        public string TxtReplyCode_kitting_P2
        {
            get => _txtReplyCode_kitting_P2;
            set { _txtReplyCode_kitting_P2 = value; OnPropertyChanged(nameof(TxtReplyCode_kitting_P2)); }
        }
        public string TxtReplyText_kitting_P2
        {
            get => _txtReplyText_kitting_P2;
            set { _txtReplyText_kitting_P2 = value; OnPropertyChanged(nameof(TxtReplyText_kitting_P2)); }
        }
        #endregion
        #region P3
        public string TxtMaterialBatchID_P3
        {
            get => _txtMaterialBatchID_P3;
            set { _txtMaterialBatchID_P3 = value; OnPropertyChanged(nameof(TxtMaterialBatchID_P3)); }
        }
        public string TxtMaterialPortID_P3
        {
            get => _txtMaterialPortID_P3;
            set { _txtMaterialPortID_P3 = value; OnPropertyChanged(nameof(TxtMaterialPortID_P3)); }
        }
        public string TxtMaterialState_P3
        {
            get => _txtMaterialState_P3;
            set { _txtMaterialState_P3 = value; OnPropertyChanged(nameof(TxtMaterialState_P3)); }
        }
        public string TxtAssembleQTY_P3
        {
            get => _txtAssembleQTY_P3;
            set { _txtAssembleQTY_P3 = value; OnPropertyChanged(nameof(TxtAssembleQTY_P3)); }
        }
        public string TxtTotalQTY_P3
        {
            get => _txtTotalQTY_P3;
            set { _txtTotalQTY_P3 = value; OnPropertyChanged(nameof(TxtTotalQTY_P3)); }
        }
        public string TxtRemainQTY_P3
        {
            get => _txtRemainQTY_P3;
            set { _txtRemainQTY_P3 = value; OnPropertyChanged(nameof(TxtRemainQTY_P3)); }
        }
        public string TxtNGQTY_P3
        {
            get => _txtNGQTY_P3;
            set { _txtNGQTY_P3 = value; OnPropertyChanged(nameof(TxtNGQTY_P3)); }
        }
        public string TxtUsedQTY_P3
        {
            get => _txtUsedQTY_P3;
            set { _txtUsedQTY_P3 = value; OnPropertyChanged(nameof(TxtUsedQTY_P3)); }
        }
        public string TxtMaterialBatchID_kitting_P3
        {
            get => _txtMaterialBatchID_kitting_P3;
            set { _txtMaterialBatchID_kitting_P3 = value; OnPropertyChanged(nameof(TxtMaterialBatchID_kitting_P3)); }
        }
        public string TxtMaterialPortID_kitting_P3
        {
            get => _txtMaterialPortID_kitting_P3;
            set { _txtMaterialPortID_kitting_P3 = value; OnPropertyChanged(nameof(TxtMaterialPortID_kitting_P3)); }
        }
        public string TxtReplyCode_kitting_P3
        {
            get => _txtReplyCode_kitting_P3;
            set { _txtReplyCode_kitting_P3 = value; OnPropertyChanged(nameof(TxtReplyCode_kitting_P3)); }
        }
        public string TxtReplyText_kitting_P3
        {
            get => _txtReplyText_kitting_P3;
            set { _txtReplyText_kitting_P3 = value; OnPropertyChanged(nameof(TxtReplyText_kitting_P3)); }
        }
        #endregion
        #endregion
        //private ObservableCollection<MATE> _allMaterials;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
