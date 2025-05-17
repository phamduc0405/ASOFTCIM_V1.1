using A_SOFT.CMM.INIT;
using ASOFTCIM.Config;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.View.Home;
using ASOFTCIM.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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

namespace ASOFTCIM.MVVM.View.Alarm
{
    /// <summary>
    /// Interaction logic for AlarmView.xaml
    /// </summary>
    public partial class AlarmView : UserControl
    {
        public AlarmView()
        {
            InitializeComponent();
            CreaterEvent();
        }
        private void CreaterEvent()
        {
            Unloaded += (s, e) =>
            {
                if (DataContext is AlarmViewModel vm)
                {
                    vm.Dispose();
                }
            };
        }
    }
}
