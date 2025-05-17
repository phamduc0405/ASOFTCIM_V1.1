using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using ASOFTCIM.Config;
using ASOFTCIM.MainControl;
using System;
using System.Collections.Generic;
using System.Linq;
using ASOFTCIM.Init;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using System.Threading;
using ASOFTCIM.MVVM.View.Popup;
using ASOFTCIM.MVVM.ViewModel;

namespace ASOFTCIM.MVVM.View.Config
{
    /// <summary>
    /// Interaction logic for ConfigView.xaml
    /// </summary>
    public partial class ConfigView : System.Windows.Controls.UserControl
    {
        #region Field

        private Controller _controller;
        private EquipmentConfig _equipmentConfig;
        #endregion
        #region Property

        #endregion
        #region Event

        #endregion
        #region Constructor
        public ConfigView()
        {
            InitializeComponent();
            CreaterEven();
        }
        #endregion
        #region Private Void
        private void CreaterEven()
        {
            Unloaded += (s, e) =>
            {
                if (DataContext is ConfigViewModel vm)
                {
                    vm.Dispose();
                }
            };
        }
        #endregion
    }
}
