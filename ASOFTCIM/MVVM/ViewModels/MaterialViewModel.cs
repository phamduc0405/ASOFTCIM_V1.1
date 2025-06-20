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
    public class MaterialViewModel : BaseViewModels
    {
        #region Fields
        private Controller _controller;
        private ASOFTCIM.MVVM.Models.MaterialModel _materialModel;
        #endregion
        #region Properties
        public ASOFTCIM.MVVM.Models.MaterialModel MaterialModel
        {
            get => _materialModel;
            set { _materialModel = value; OnPropertyChanged(nameof(MaterialModel)); }
        }
        #endregion
        public MaterialViewModel()
        {
            _controller = Controller.Instange;
            _materialModel = new ASOFTCIM.MVVM.Models.MaterialModel();
            StartDispatcherTimer(UpdateMaterialData, 1);
        }
        private void UpdateMaterialData()
        {
            if (_controller.CIM.EqpData.CELLEVENTDATA.MATERIALs.Count == 0)
            {
                return;
            }
            // Port 1
            _materialModel.TxtMaterialBatchID_P1 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALBATCHID;
            _materialModel.TxtMaterialPortID_P1 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALPORTID;
            _materialModel.TxtMaterialState_P1 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALST;
            _materialModel.TxtAssembleQTY_P1 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALPROCASSEMQTY;
            _materialModel.TxtTotalQTY_P1 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALTOTALQTY;
            _materialModel.TxtRemainQTY_P1 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALREMAINQTY;
            _materialModel.TxtNGQTY_P1 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALNGQTY;
            _materialModel.TxtUsedQTY_P1 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALUSEQTY;
            _materialModel.TxtMaterialBatchID_kitting_P1 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALBATCHID;
            _materialModel.TxtMaterialPortID_kitting_P1 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALPORTID;
            _materialModel.TxtReplyCode_kitting_P1 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALSUPPLYREQUESTQTY;
            _materialModel.TxtReplyText_kitting_P1 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALSUPPLYREQUESTQTY;
            if (_controller.CIM.EqpData.CELLEVENTDATA.MATERIALs.Count == 1)
            {
                return;
            }
            // Port 2
            _materialModel.TxtMaterialBatchID_P2 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALBATCHID;
            _materialModel.TxtMaterialPortID_P2 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALPORTID;
            _materialModel.TxtMaterialState_P2 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALST;
            _materialModel.TxtAssembleQTY_P2 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALPROCASSEMQTY;
            _materialModel.TxtTotalQTY_P2 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALTOTALQTY;
            _materialModel.TxtRemainQTY_P2 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALREMAINQTY;
            _materialModel.TxtNGQTY_P2 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALNGQTY;
            _materialModel.TxtUsedQTY_P2 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALUSEQTY;
            _materialModel.TxtMaterialBatchID_kitting_P2 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALBATCHID;
            _materialModel.TxtMaterialPortID_kitting_P2 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALPORTID;
            _materialModel.TxtReplyCode_kitting_P2 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALSUPPLYREQUESTQTY;
            _materialModel.TxtReplyText_kitting_P2 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALSUPPLYREQUESTQTY;
            if (_controller.CIM.EqpData.CELLEVENTDATA.MATERIALs.Count == 2)
            {
                return;
            }
            // Port 3
            _materialModel.TxtMaterialBatchID_P3 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALBATCHID;
            _materialModel.TxtMaterialPortID_P3 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALPORTID;
            _materialModel.TxtMaterialState_P3 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALST;
            _materialModel.TxtAssembleQTY_P3 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALPROCASSEMQTY;
            _materialModel.TxtTotalQTY_P3 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALTOTALQTY;
            _materialModel.TxtRemainQTY_P3 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALREMAINQTY;
            _materialModel.TxtNGQTY_P3 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALNGQTY;
            _materialModel.TxtUsedQTY_P3 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALUSEQTY;
            _materialModel.TxtMaterialBatchID_kitting_P3 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALBATCHID;
            _materialModel.TxtMaterialPortID_kitting_P3 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALPORTID;
            _materialModel.TxtReplyCode_kitting_P3 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALSUPPLYREQUESTQTY;
            _materialModel.TxtReplyText_kitting_P3 = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALSUPPLYREQUESTQTY;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopDispatcherTimer();
            }
            base.Dispose(disposing);
        }
        ~MaterialViewModel()
        {
            Dispose(false);
        }
        
    }
}
