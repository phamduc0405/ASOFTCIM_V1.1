using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using A_SOFT.CMM.INIT;
using System.Reflection;
using ASOFTCIM.Helper;
using ASOFTCIM.MVVM.ViewModels;

namespace ASOFTCIM.MVVM.Views.Home
{
    /// <summary>
    /// Interaction logic for PartialCpuChart.xaml
    /// </summary>
    public partial class PartialCpuChart : UserControl
    {
        private PerformanceCounter cpuCounter;
        private Process currentProcess;
        public SeriesCollection LastHourSeries { get; set; }
        private CancellationTokenSource cancellationTokenSource;

        public PartialCpuChart()
        {

            InitializeComponent();
            currentProcess = Process.GetCurrentProcess();
            cpuCounter = new PerformanceCounter("Process", "% Processor Time", currentProcess.ProcessName);
            LastHourSeries = new SeriesCollection
        {
            new LineSeries
            {
                AreaLimit = -10,
                Values = new ChartValues<ObservableValue>(),
                LineSmoothness = 0.8, // 0: đường thẳng, 1: đường rất mượt
                PointGeometry = null, // Ẩn các điểm để làm cho đường mượt hơn
            }
        };
            cancellationTokenSource = new CancellationTokenSource();
            UpdateCpu(cancellationTokenSource.Token);
            DataContext = this;
        }

        private void UpdateCpu(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                var r = new Random();
                while (!cancellationToken.IsCancellationRequested && HomeViewModel.Running)
                {
                    Thread.Sleep(1000);
                    float cpuUsage = cpuCounter.NextValue() / Environment.ProcessorCount;
                    try
                    {
                        Application.Current?.Dispatcher.Invoke(() =>
                        {
                            LastHourSeries[0].Values.Add(new ObservableValue(cpuUsage));
                            if (LastHourSeries[0].Values.Count > 60)
                                LastHourSeries[0].Values.RemoveAt(0);
                            txtTarget.Text = $"CPU Usage: {cpuUsage:F2}%";
                            if(cpuUsage >20)
                            {
                                LogHelper.Warn(txtTarget.Text);
                            }
                        });
                    }
                    catch (Exception ex )
                    {
                        var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.",
                        MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                        LogTxt.Add(LogTxt.Type.Exception, debug);
                        return;
                    }

                }
            }, cancellationToken);
        }

        public void OnUnloaded()
        {
            // Cancel the task when the control is unloaded
            cancellationTokenSource.Cancel();
        }

    }
}
