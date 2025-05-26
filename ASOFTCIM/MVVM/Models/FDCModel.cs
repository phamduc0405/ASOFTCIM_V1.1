using ASOFTCIM.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.MVVM.Models
{
    public class FDCModel : INotifyPropertyChanged
    {
        private ObservableCollection<SV> _allSVIDs;
        private ObservableCollection<SV> _currentSVIDsL;
        private ObservableCollection<SV> _currentSVIDsR;

        public ObservableCollection<SV> AllSVIDs
        {
            get => _allSVIDs;
            set
            {
                _allSVIDs = value;
                OnPropertyChanged(nameof(AllSVIDs));
            }
        }
        public ObservableCollection<SV> CurrentSVIDsL
        {
            get => _currentSVIDsL;
            set
            {
                _currentSVIDsL = value;
                OnPropertyChanged(nameof(CurrentSVIDsL));
            }
        }
        public ObservableCollection<SV> CurrentSVIDsR
        {
            get => _currentSVIDsR;
            set
            {
                _currentSVIDsR = value;
                OnPropertyChanged(nameof(CurrentSVIDsR));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
