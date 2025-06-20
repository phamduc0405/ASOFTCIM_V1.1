using ASOFTCIM.Data;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ASOFTCIM.MVVM.Views.ECM
{
    /// <summary>
    /// Interaction logic for ECMView.xaml
    /// </summary>
    public partial class ECMView : UserControl
    {
        private Controller _controller;
        public ECMView()
        {
            InitializeComponent();
            _controller = Controller.Instange;
            CreaterEvent();
        }
        private void CreaterEvent()
        {
            Unloaded += (s, e) =>
            {
                if (DataContext is ECMViewModel vm)
                {
                    vm.Dispose();
                }
            };
        }
    }
}
