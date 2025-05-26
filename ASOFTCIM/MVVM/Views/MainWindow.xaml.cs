using A_SOFT.CMM.HELPER;
using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;
using ASOFTCIM.Helper;
using ASOFTCIM.Init;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.Models;
using ASOFTCIM.MVVM.Views.Alarm;
using ASOFTCIM.MVVM.Views.Config;
using ASOFTCIM.MVVM.Views.ECM;
using ASOFTCIM.MVVM.Views.Home;
using ASOFTCIM.MVVM.Views.Material;
using ASOFTCIM.MVVM.Views.Monitor;
using ASOFTCIM.MVVM.Views.Popup;
using ASOFTCIM.MVVM.ViewModels;
using OfficeOpenXml.FormulaParsing.Excel.Operators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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

namespace ASOFTCIM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Controller Controller;
        public static string User = "User";
        public static string Pass = "2";
        public static int LeveLogin = 0;
       

        private static bool _running = true;
        public static bool Running
        {
            get
            {
                return _running;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(this);
            
        }
    }
}
