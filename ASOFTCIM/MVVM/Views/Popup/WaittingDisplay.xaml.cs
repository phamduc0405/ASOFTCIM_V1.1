using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace ASOFTCIM.MVVM.Views.Popup
{
    /// <summary>
    /// Interaction logic for WaittingDisplay.xaml
    /// </summary>
    public partial class WaittingDisplay : Window, INotifyPropertyChanged
    {
        private double _progress;
        public double Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }
        public WaittingDisplay()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public async Task LoadResourcesAsync()
        {
            for (int i = 0; i <= 100; i++)
            {
                //Progress = i * 3.6;
                await Task.Delay(30);
            }
        }
        public async Task SmoothProgressToAsync( double target, int durationMs = 300)
        {
            double start = Progress;
            double steps = 30;
            double stepSize = (target - start) / steps;
            int stepDelay = durationMs / (int)steps;

            for (int i = 0; i < steps; i++)
            {
                Progress += stepSize;
                await Task.Delay(stepDelay);
            }

            Progress = target;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
