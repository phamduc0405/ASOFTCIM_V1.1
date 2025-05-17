using ASOFTCIM.Data;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.ViewModel;
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
            CreaterEvent();
        }
        private void CreaterEvent()
        {
            Unloaded += (s, e) =>
            {
                if (DataContext is MaterialViewModel vm)
                {
                    vm.Dispose();
                }
            };
        }

    }
}
