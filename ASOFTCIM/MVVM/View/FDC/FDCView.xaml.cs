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

namespace ASOFTCIM.MVVM.View.FDC
{
    /// <summary>
    /// Interaction logic for FDCView.xaml
    /// </summary>
    public partial class FDCView : UserControl
    {
        private TextBlock[] tblIndex;
        private TextBlock[] tblSvid;
        private TextBlock[] tblSvValue;
        private TextBlock[] tblSvName;
        private TextBlock[] tblSvUnit;
        private Controller _controller;
        private Thread _update;
        private ResourceDictionary res;
        private int _index = 0;
        private int _totalsvid = 0; 
        //public FDCView()
        //{
        //    InitializeComponent();
        //    _controller = MainWindow.Controller;
        //    res = (ResourceDictionary)Application.LoadComponent(new Uri("/A_SOFT.CMM.UI;component/STYLE/ButtonStyle.xaml", UriKind.Relative));
        //    Initial();
        //    _totalsvid = _controller.CIM.EqpData.SVID.Count();
        //    CreateEvent();
        //    _update = new Thread(Update);
        //}
        private DispatcherTimer _timer;

        public FDCView()
        {
            InitializeComponent();
            _controller = MainWindow.Controller;
            //res = (ResourceDictionary)Application.LoadComponent(new Uri("/A_SOFT.CMM.UI;component/STYLE/ButtonStyle.xaml", UriKind.Relative));
            res = Application.Current.Resources;
            _totalsvid = _controller.CIM.EqpData.SVID.Count();
            CreateEvent();
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
            _timer.Tick += (s, e) => UpdateSvidData();
            _timer.Start();
        }

        private void CreateEvent()
        {
            btnNext.Click += (s, e) =>
            {
                if ((_index + 1) * 50 < _totalsvid)
                {
                    _index++;
                    ShowSvid(_index);
                }
            };

            btnBack.Click += (s, e) =>
            {
                if (_index > 0)
                {
                    _index--;
                    ShowSvid(_index);
                }
            };
        }

        private void ShowSvid(int index)
        {
            mainGrid1.Children.Clear();
            mainGrid2.Children.Clear();

            int start = index * 50;
            int end = Math.Min(start + 50, _totalsvid);
            var svidList = _controller.CIM.EqpData.SVID;

            for (int i = start; i < end; i++)
            {
                var targetGrid = i - start < 25 ? mainGrid1 : mainGrid2;
                int rowIndex = i - start < 25 ? i - start + 1 : i - start - 25 + 1;

                AddTextBlock(targetGrid, rowIndex, 0, (i + 1).ToString());
                AddTextBlock(targetGrid, rowIndex, 1, svidList[i].SVID);
                AddTextBlock(targetGrid, rowIndex, 2, svidList[i].SVNAME, TextAlignment.Left, new Thickness(10, 0, 0, 0));
                AddTextBlock(targetGrid, rowIndex, 3, FormatSVValue(svidList[i]));
            }
        }

        private void AddTextBlock(Grid grid, int row, int col, string text, TextAlignment align = TextAlignment.Center, Thickness? margin = null)
        {
            var tb = new TextBlock
            {
                Style = (Style)res["menuButtonText"],
                TextAlignment = align,
                Margin = margin ?? new Thickness(0),
                Text = text
            };
            Grid.SetRow(tb, row);
            Grid.SetColumn(tb, col);
            grid.Children.Add(tb);
        }

        private void UpdateSvidData()
        {
            // Refresh current 50 SVs only
            ShowSvid(_index);
        }

        private string FormatSVValue(SV item)
        {
            if (!float.TryParse(item.SVVALUE, out float result))
                return item.SVVALUE;

            switch (item.SVID)
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
                    return item.SVVALUE;
            }
        }
    }
}
