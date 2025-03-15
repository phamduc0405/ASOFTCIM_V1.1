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
        private DispatcherTimer _updateTimer;
        private Thread _update;
        private ResourceDictionary res;
        public FDCView()
        {
            InitializeComponent();
            _controller = MainWindow.Controller;
            res = (ResourceDictionary)Application.LoadComponent(new Uri("/A_SOFT.CMM.UI;component/STYLE/ButtonStyle.xaml", UriKind.Relative));
            Initial();
            _update = new Thread(Update);
        }
        private void Initial()
        {
            Loaded += (sender, args) =>
            {
                _update.Start();
            };
            Unloaded += (sender, args) =>
            {
                if (_update.IsAlive || _update != null)
                {
                    _update.Abort();
                }
            };

            int count = _controller.CIM.EqpData.SVID.Count();
            tblIndex = new TextBlock[count];
            tblSvid = new TextBlock[count];
            tblSvValue = new TextBlock[count];
            tblSvName = new TextBlock[count];
            tblSvUnit = new TextBlock[count];
            #region define Text Block
            for (int i = 0; i < count; i++)
            {
                if (i < 25)
                {
                    tblIndex[i] = new TextBlock();
                    Grid.SetRow(tblIndex[i], i + 1);
                    Grid.SetColumn(tblIndex[i], 0);
                    tblIndex[i].Style = (System.Windows.Style)res["menuButtonText"];
                    tblIndex[i].TextAlignment = TextAlignment.Center;
                    mainGrid1.Children.Add(tblIndex[i]);

                    tblSvid[i] = new TextBlock();
                    Grid.SetRow(tblSvid[i], i + 1);
                    Grid.SetColumn(tblSvid[i], 1);
                    tblSvid[i].Style = (System.Windows.Style)res["menuButtonText"];
                    tblSvid[i].TextAlignment = TextAlignment.Center;
                    mainGrid1.Children.Add(tblSvid[i]);

                    tblSvName[i] = new TextBlock();
                    Grid.SetRow(tblSvName[i], i + 1);
                    Grid.SetColumn(tblSvName[i], 2);
                    tblSvName[i].Style = (System.Windows.Style)res["menuButtonText"];
                    tblSvName[i].TextAlignment = TextAlignment.Left;
                    tblSvName[i].Margin = new Thickness(10, 0, 0, 0);

                    mainGrid1.Children.Add(tblSvName[i]);

                    tblSvValue[i] = new TextBlock();
                    Grid.SetRow(tblSvValue[i], i + 1);
                    Grid.SetColumn(tblSvValue[i], 3);
                    tblSvValue[i].Style = (System.Windows.Style)res["menuButtonText"];
                    tblSvValue[i].TextAlignment = TextAlignment.Center;
                    mainGrid1.Children.Add(tblSvValue[i]);
                }
                else
                {
                    tblIndex[i] = new TextBlock();
                    Grid.SetRow(tblIndex[i], i + 1 - 25);
                    Grid.SetColumn(tblIndex[i], 0);
                    tblIndex[i].Style = (System.Windows.Style)res["menuButtonText"];
                    tblIndex[i].TextAlignment = TextAlignment.Center;
                    mainGrid2.Children.Add(tblIndex[i]);

                    tblSvid[i] = new TextBlock();
                    Grid.SetRow(tblSvid[i], i + 1 - 25);
                    Grid.SetColumn(tblSvid[i], 1);
                    tblSvid[i].Style = (System.Windows.Style)res["menuButtonText"];
                    tblSvid[i].TextAlignment = TextAlignment.Center;
                    mainGrid2.Children.Add(tblSvid[i]);

                    tblSvName[i] = new TextBlock();
                    Grid.SetRow(tblSvName[i], i + 1 - 25);
                    Grid.SetColumn(tblSvName[i], 2);
                    tblSvName[i].Style = (System.Windows.Style)res["menuButtonText"];
                    tblSvName[i].TextAlignment = TextAlignment.Left;
                    tblSvName[i].Margin = new Thickness(10, 0, 0, 0);
                    mainGrid2.Children.Add(tblSvName[i]);

                    tblSvValue[i] = new TextBlock();
                    Grid.SetRow(tblSvValue[i], i + 1 - 25);
                    Grid.SetColumn(tblSvValue[i], 3);
                    tblSvValue[i].Style = (System.Windows.Style)res["menuButtonText"];
                    tblSvValue[i].TextAlignment = TextAlignment.Center;
                    mainGrid2.Children.Add(tblSvValue[i]);
                }
            }

            #endregion
        }
        private async void Update()
        {
            bool isInTargetRange = false;
            int numberofLstSvData = _controller.CIM.EqpData.SVID.Count;
            while (true)
            {

                await Dispatcher.Invoke(async () =>
                {
                    for (int i = 0; i < numberofLstSvData; i++)
                    {
                        if (i < _controller.CIM.EqpData.SVID.Count)
                        {
                            string strDe = "";
                            tblIndex[i].Text = (i + 1).ToString();
                            tblSvid[i].Text = string.IsNullOrEmpty(_controller.CIM.EqpData.SVID[i].SVID) ? "" : _controller.CIM.EqpData.SVID[i].SVID;
                            tblSvName[i].Text = string.IsNullOrEmpty(_controller.CIM.EqpData.SVID[i].SVNAME) ? "" : _controller.CIM.EqpData.SVID[i].SVNAME;
                            string str = (_controller.CIM.EqpData.SVID[i].SVVALUE);
                            tblSvValue[i].Text = string.IsNullOrEmpty(str) ? "" : str;
                            var Dec = float.TryParse(_controller.CIM.EqpData.SVID[i].SVVALUE, out float result);
                            if (Dec)
                            {
                                switch (_controller.CIM.EqpData.SVID[i].SVID)
                                {
                                    case "10003":

                                        strDe = (result / 100).ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "10004":
                                        strDe = (result / 100).ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "10010":
                                        strDe = (result / 10).ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "10025":
                                        strDe = (result / 100).ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "10026":
                                        strDe = result.ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "10030":
                                        strDe = result.ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "10031":
                                        strDe = result.ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "10032":
                                        strDe = result.ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "10034":
                                        strDe = result.ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "60000":
                                        strDe = (result / 10).ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "60001":
                                        strDe = (result / 1000).ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "60002":
                                        strDe = (result / 10).ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "60003":
                                        strDe = result.ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "60006":
                                        strDe = (result / 10).ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "60007":
                                        strDe = (result / 1000).ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "60008":
                                        strDe = (result / 10).ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "60009":
                                        strDe = result.ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "10038":
                                        strDe = (result / 100).ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "10037":
                                        strDe = result.ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "10039":
                                        strDe = result.ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                    case "10040":
                                        strDe = result.ToString();
                                        tblSvValue[i].Text = string.IsNullOrEmpty(strDe) ? "" : strDe;

                                        break;
                                }
                            }

                        }


                    }
                });
                Thread.Sleep(1000);
            }

        }
    }
}
