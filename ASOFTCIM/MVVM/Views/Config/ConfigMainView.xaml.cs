
using A_SOFT.CMM.INIT;
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

namespace ASOFTCIM.MVVM.Views.Config
{
    /// <summary>
    /// Interaction logic for ConfigMainView.xaml
    /// </summary>
    public partial class ConfigMainView : UserControl
    {
        public ConfigMainView()
        {
            InitializeComponent();
            CreateEvent();
        }
        private void CreateEvent()
        {
            btnEquipSetting.Click += (s, e) =>
            {
                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)s).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
                this.Content = new ConfigView();
            };

            btnTest.Click += (s, e) =>
            {
                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)s).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
                this.Content = new Test();
            };
        }
    }
}

