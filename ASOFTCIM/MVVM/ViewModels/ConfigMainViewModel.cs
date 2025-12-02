using ASOFTCIM.Config;
using ASOFTCIM.Init;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ASOFTCIM.MVVM.Views.Config;

namespace ASOFTCIM.MVVM.ViewModels
{
    public class ConfigMainViewModel : BaseViewModels
    {
        public ICommand ConfigEqp { get; }
        public ICommand Test { get; }
        public ConfigMainViewModel(Controller controller)
        {
            ConfigEqp = new RelayCommand(excute =>
            {
                MainWindowViewModel.NavigationService.NavigateTo<ConfigViewModel>();
            });
            Test = new RelayCommand(excute =>
            {
                MainWindowViewModel.MainWindowModel.Currentview = new Test(controller);
            });


        }
    }
}
