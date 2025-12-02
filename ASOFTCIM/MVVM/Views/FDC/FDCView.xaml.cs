using ASOFTCIM.MainControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using ASOFTCIM.Data;
using ASOFTCIM.MVVM.ViewModels;

namespace ASOFTCIM.MVVM.Views.FDC
{
    /// <summary>
    /// Interaction logic for FDCView.xaml
    /// </summary>
    public partial class FDCView : UserControl
    {
        
        private Controller _controller;
        private Thread _update;
        private ResourceDictionary res;
        private int _index = 0;
        private int _totalsvid = 0;

        private DispatcherTimer _timer;
        public FDCView()
        {
            InitializeComponent();
            res = Application.Current.Resources;
            
            CreateEvent();
        }
        private void CreateEvent()
        {
            Unloaded += (s, e) =>
            {
                if (DataContext is FDCViewModel vm)
                {
                    vm.Dispose();
                }
            };
        }
    }
}
