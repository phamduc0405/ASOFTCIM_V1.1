using A_SOFT.CMM.INIT;
using ASOFTCIM.Config;
using ASOFTCIM.Helper;
using ASOFTCIM.MainControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ASOFTCIM.MVVM.Views.Popup
{
    /// <summary>
    /// Interaction logic for SavePLCConfigDisplay.xaml
    /// </summary>
    public partial class SavePLCConfigDisplay : Window
    {
        
        private ResourceDictionary resBtn;
        private ResourceDictionary resTxt;
        private Controller _controller;
        private EquipmentConfig _eqpConfig;
        private string _path;
        public SavePLCConfigDisplay(string path,Controller controller)
        {
            InitializeComponent();
            _path = path;
            this.DataContext = this;
            resBtn = Application.Current.Resources;
            resTxt = Application.Current.Resources;
            _controller = controller;
            Initial();
            CreateEvent();
        }
        private void CreateEvent()
        {
            btnSavePlcConfig.Click += async (s, e) =>
            {

                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((Control)s).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
                LoadingPlcImage.Visibility = Visibility.Visible;
                PLCHelper plcTemp = new PLCHelper();
                List<string> str = stkMain.Children
    .OfType<StackPanel>()
    .SelectMany(row => row.Children.OfType<ToggleButton>())
    .Where(tglBtn => tglBtn.IsChecked == true && !string.IsNullOrEmpty(tglBtn.Name))
    .Select(tglBtn => tglBtn.Name)
    .ToList();
                await plcTemp.LoadExcel(_path,str);
                

                PropertyInfo[] tempProperties = plcTemp.GetType().GetProperties();
                foreach (var propertyName in str)
                {
                    var propertyInfo = tempProperties.FirstOrDefault(item => item.Name == propertyName);
                    if (propertyInfo != null)
                    {
                        var propertyValue = propertyInfo.GetValue(plcTemp); // Lấy giá trị của thuộc tính từ _controller.Plc
                        var plcProperty = _controller.CIM.PLCH.GetType().GetProperty(propertyName); // Lấy PropertyInfo của plcTemp

                        if (plcProperty != null)
                        {
                            plcProperty.SetValue(_controller.CIM.PLCH, propertyValue); // Thiết lập giá trị cho plcTemp từ _controller.Plc
                        }
                    }
                }
                //await _controller.SavePlcData();
                LoadingPlcImage.Visibility = Visibility.Hidden;
                this.Close();
            };
            btnCancel.Click += (s, e) =>
            {

                var debug = string.Format("Class:{0} Method:{1} Event:{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ((Control)s).Name);
                LogTxt.Add(LogTxt.Type.UI, debug);
                this.Close();
            };
        }

        private void Initial()
        {
            Type myClassType = typeof(PLCHelper);

            // Lấy danh sách các thuộc tính công khai (public properties)
            PropertyInfo[] properties = myClassType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                //Console.WriteLine($"Property Name: {property.Name}");
                //Console.WriteLine($"Property Type: {property.PropertyType}");
                AddRow(property.Name);
            }
        }
        private void AddRow(string item = null)
        {
            StackPanel newStackPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 5, 0, 0) };
            TextBlock newTbl = new TextBlock { Width = 100, Margin = new Thickness(0, 0, 10, 0), Text = item, Style = (Style)resTxt["HeaderTextBlockStyle"], };
            ToggleButton toggleButton = new ToggleButton { Background = Brushes.LawnGreen, Tag = "GreenYellow", Name = item, Style = (Style)resBtn["ToggleFlipFlop"], };
            newStackPanel.Children.Add(newTbl);
            newStackPanel.Children.Add(toggleButton);

            stkMain.Children.Add(newStackPanel);
        }
    }
}
