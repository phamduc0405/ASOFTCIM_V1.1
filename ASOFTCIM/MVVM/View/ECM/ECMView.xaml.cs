using ASOFTCIM.Data;
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
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ASOFTCIM.MVVM.View.ECM
{
    /// <summary>
    /// Interaction logic for ECMView.xaml
    /// </summary>
    public partial class ECMView : UserControl
    {
        private Controller _controller;
        private ResourceDictionary res;
        private int _index = 0;
        private int _totalECM = 0;
        private DispatcherTimer _timer;
        public ECMView()
        {
            InitializeComponent();
            _controller = MainWindow.Controller;
            res = (ResourceDictionary)Application.LoadComponent(new Uri("/A_SOFT.CMM.UI;component/STYLE/ButtonStyle.xaml", UriKind.Relative));
            _totalECM = _controller.CIM.EqpData.ECS.Count();
            
            Initial();
        }
        private void Initial()
        {
            Loaded += (sender, args) =>
            {
                StartTimer();
            };
            Unloaded += (sender, args) =>
            {
                _timer?.Stop();
            };
        }

        private void StartTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += (s, e) => UpdateECMData();
            _timer.Start();
        }
        private void UpdateECMData()
        {
            
            ShowECM(_index);
            GridHL.Background = Brushes.LightGray;
        }
        private void ShowECM(int index)
        {
            mainGrid1.Children.Clear();
            mainGrid2.Children.Clear();

            int start = index * 50;
            int end = Math.Min(start + 50, _totalECM);
            var ecmList = _controller.CIM.EqpData.ECS;

            for (int i = start; i < end; i++)
            {
                var targetGrid = i - start < 25 ? mainGrid1 : mainGrid2;
                int rowIndex = i - start < 25 ? i - start + 1 : i - start - 25 + 1;

                //AddTextBlock(targetGrid, rowIndex, 0, (i + 1).ToString());
                AddTextBlock(targetGrid, rowIndex, 0, ecmList[i].ECID);
                AddTextBlock(targetGrid, rowIndex, 1, ecmList[i].ECNAME, TextAlignment.Left, new Thickness(10, 0, 0, 0));
                AddTextBlock(targetGrid, rowIndex, 2, FormatSVValue(ecmList[i]), background: Brushes.LightGray);
                AddTextBlock(targetGrid, rowIndex, 3, "DEC");
            }
        }
        private void AddTextBlock(Grid grid, int row, int col, string text, TextAlignment align = TextAlignment.Center, Thickness? margin = null, SolidColorBrush background = null)
        {
            var tb = new TextBlock
            {
                Style = (Style)res["menuButtonText"],
                TextAlignment = align,
                Background = background ?? Brushes.Transparent,
                Margin = margin ?? new Thickness(0),
                Text = text
            };
            Grid.SetRow(tb, row);
            Grid.SetColumn(tb, col);
            grid.Children.Add(tb);
        }
        private string FormatSVValue(EC item)
        {
            if (!float.TryParse(item.ECDEF, out float result))
                return item.ECDEF;

            switch (item.ECID)
            {
                case "10003":
                case "10004":
                case "10025":
                case "10038":
                    return (result / 100).ToString();
                case "10010":
                case "60000":
                case "60002":
                case "60006":
                case "60008":
                    return (result / 10).ToString();
                case "60001":
                case "60007":
                    return (result / 1000).ToString();
                case "10026":
                case "10030":
                case "10031":
                case "10032":
                case "10034":
                case "60003":
                case "60009":
                case "10037":
                case "10039":
                case "10040":
                    return result.ToString();
                default:
                    return item.ECDEF;
            }
        }
    }
}
