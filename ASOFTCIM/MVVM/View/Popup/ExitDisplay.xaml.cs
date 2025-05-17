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
using System.Windows.Shapes;

namespace ASOFTCIM.MVVM.View.Popup
{
    /// <summary>
    /// Interaction logic for ExitDisplay.xaml
    /// </summary>
    public partial class ExitDisplay : Window
    {
        private string _header;
        private string _commandRequest;
        public ExitDisplay(string Command)
        {
            InitializeComponent();
            _commandRequest = Command;
            _header = "";
            //DefineCode(Command);
            CreaterEvents();
        }

        private void CreaterEvents()
        {
            Loaded += (sender, args) =>
            {
                txtCommand.Text = _header;
                txtCommandRequest.Text = $"{_commandRequest}";
            };
            btnYes.Click += (sender, args) =>
            {

                this.DialogResult = true;
                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)sender).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
            };
            btnNo.Click += (sender, args) =>
            {

                this.DialogResult = false;
                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((System.Windows.Controls.Control)sender).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
            };

        }
        private void CloseView()
        {

            this.Close();
        }
    }
}
