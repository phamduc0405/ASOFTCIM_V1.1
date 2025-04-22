using ASOFTCIM.Data;
using ASOFTCIM.MainControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ASOFTCIM.MVVM.View.Material
{
    /// <summary>
    /// Interaction logic for MaterialView.xaml
    /// </summary>
    public partial class MaterialView : UserControl
    {
        private Controller _controller;
        private DispatcherTimer _timer;
        public MaterialView()
        {
            InitializeComponent();
            _controller = MainWindow.Controller;
            Initial();
        }
        private void Initial()
        {
            Loaded += (sender, args) =>
            {
                StartTimer();
            };
            Unloaded += (sender, args) =>
            {
                _timer?.Stop();
            };
        }
        private void StartTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += (s, e) => UpdateMaterialData();
            _timer.Start();
        }
        private void UpdateMaterialData()
        {
            if(_controller.CIM.EqpData.CELLEVENTDATA.MATERIALs.Count == 0)
            {
                return;
            }    
            // Port 1
            txtMaterialBatchID_P1.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALBATCHID;
            txtMaterialPortID_P1.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALPORTID;
            txtMaterialState_P1.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALST;
            txtAssembleQTY_P1.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALPROCASSEMQTY;
            txtTotalQTY_P1.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALTOTALQTY;
            txtRemainQTY_P1.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALREMAINQTY;
            txtNGQTY_P1.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALNGQTY;
            txtUsedQTY_P1.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALUSEQTY;
            txtMaterialBatchID_kitting_P1.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALBATCHID;
            txtMaterialPortID_kitting_P1.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALPORTID;
            txtReplyCode_kitting_P1.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALSUPPLYREQUESTQTY;
            txtReplyText_kitting_P1.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[0].EQPMATERIALSUPPLYREQUESTQTY;
            if (_controller.CIM.EqpData.CELLEVENTDATA.MATERIALs.Count == 1)
            {
                return;
            }
            // Port 2
            txtMaterialBatchID_P2.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALBATCHID;
            txtMaterialPortID_P2.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALPORTID;
            txtMaterialState_P2.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALST;
            txtAssembleQTY_P2.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALPROCASSEMQTY;
            txtTotalQTY_P2.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALTOTALQTY;
            txtRemainQTY_P2.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALREMAINQTY;
            txtNGQTY_P2.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALNGQTY;
            txtUsedQTY_P2.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALUSEQTY;
            txtMaterialBatchID_kitting_P2.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALBATCHID;
            txtMaterialPortID_kitting_P2.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALPORTID;
            txtReplyCode_kitting_P2.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALSUPPLYREQUESTQTY;
            txtReplyText_kitting_P2.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[1].EQPMATERIALSUPPLYREQUESTQTY;
            if (_controller.CIM.EqpData.CELLEVENTDATA.MATERIALs.Count == 2)
            {
                return;
            }
            // Port 3
            txtMaterialBatchID_P3.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALBATCHID;
            txtMaterialPortID_P3.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALPORTID;
            txtMaterialState_P3.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALST;
            txtAssembleQTY_P3.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALPROCASSEMQTY;
            txtTotalQTY_P3.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALTOTALQTY;
            txtRemainQTY_P3.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALREMAINQTY;
            txtNGQTY_P3.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALNGQTY;
            txtUsedQTY_P3.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALUSEQTY;
            txtMaterialBatchID_kitting_P3.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALBATCHID;
            txtMaterialPortID_kitting_P3.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALPORTID;
            txtReplyCode_kitting_P3.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALSUPPLYREQUESTQTY;
            txtReplyText_kitting_P3.Text = _controller.CIM.EqpData.CELLEVENTDATA.MATERIALs[2].EQPMATERIALSUPPLYREQUESTQTY;
        }
    }
}
