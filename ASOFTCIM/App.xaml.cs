using ASOFTCIM.Init;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ASOFTCIM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            WpfSingleInstance.Make("ASOFTCIM");
            var mainview = new MainWindow();
            mainview.Show();
        }
    }
}
