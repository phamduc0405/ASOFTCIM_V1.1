using ASOFTCIM.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.MVVM.NavigationService
{
    public class NavigationService : INavigationService
    {
        private readonly Action<BaseViewModels> _setViewModel;

        public NavigationService(Action<BaseViewModels> setViewModel)
        {
            _setViewModel = setViewModel;
        }

        public void NavigateTo<TViewModel>() where TViewModel : BaseViewModels, new()
        {
            var viewModel = new TViewModel();
            _setViewModel(viewModel);
        }
    }
}
