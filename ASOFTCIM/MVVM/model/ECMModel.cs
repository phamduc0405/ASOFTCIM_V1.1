using ASOFTCIM.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.MVVM.Model
{
    public class ECMModel : INotifyPropertyChanged
    {
        private ObservableCollection<EC> _allECs;
        private ObservableCollection<EC> _currentECsL;
        private ObservableCollection<EC> _currentECsR;

        public ObservableCollection<EC> AllECs
        {
            get => _allECs;
            set
            {
                _allECs = value;
                OnPropertyChanged(nameof(AllECs));
            }
        }
        public ObservableCollection<EC> CurrentECsL
        {
            get => _currentECsL;
            set
            {
                _currentECsL = value;
                OnPropertyChanged(nameof(CurrentECsL));
            }
        }
        public ObservableCollection<EC> CurrentECsR
        {
            get => _currentECsR;
            set
            {
                _currentECsR = value;
                OnPropertyChanged(nameof(CurrentECsR));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
