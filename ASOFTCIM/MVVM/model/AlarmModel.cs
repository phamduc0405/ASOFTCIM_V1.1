using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.MVVM.Model
{
    public class AlarmModel : INotifyPropertyChanged
    {
        private ObservableCollection<Data.Alarm> _alarmList = new ObservableCollection<Data.Alarm>();

        public ObservableCollection<Data.Alarm> AlarmList
        {
            get => _alarmList;
            set
            {
                _alarmList = value;
                OnPropertyChanged(nameof(AlarmList));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
