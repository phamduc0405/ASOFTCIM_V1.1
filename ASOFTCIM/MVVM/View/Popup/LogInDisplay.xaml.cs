using ASOFTCIM.MainControl;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ASOFTCIM.MVVM.View.Popup
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
                MainWindow.User = cbb.Text;
                MainWindow.Pass = txtPass.Text;
                CloseView();            
            };
            Closing += (sender, args) =>
            {
                MainWindow.User = cbb.Text;
                MainWindow.Pass = txtPass.Text;
            };
        }
        private void CloseView()
        {
            MainWindow.User = cbb.Text;
            MainWindow.Pass = txtPass.Text;
            this.Close();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            CloseView();
        }
    }
}
