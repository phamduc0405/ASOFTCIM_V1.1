using A_SOFT.CMM.INIT;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.ViewModels;
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
using System.Windows.Shapes;

namespace ASOFTCIM.MVVM.Views.Popup
{
    /// <summary>
    /// Interaction logic for LogInDisplay.xaml
    /// </summary>
    public partial class LogInDisplay : Window
    {
        
        public LogInDisplay()
        {
            InitializeComponent();
            cbb.ItemsSource = new List<string>() { "Admin","Engineer","Operator" };
            cbb.SelectedIndex = 0;
            CreateEvents();
        }
        private void CreateEvents()
        {
            Loaded += (sender, args) =>
            {
                
            };
            btnLogIn.Click += (sender, args) =>
            {
                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)sender).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
                MainWindowViewModel.User = cbb.Text;
                MainWindowViewModel.Pass = txtPass.Text;
                CloseView();            
            };
            Closing += (sender, args) =>
            {
                MainWindowViewModel.User = cbb.Text;
                MainWindowViewModel.Pass = txtPass.Text;
            };
        }
        private void CloseView()
        {
            MainWindowViewModel.User = cbb.Text;
            MainWindowViewModel.Pass = txtPass.Text;
            this.Close();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            CloseView();
        }
    }
}
