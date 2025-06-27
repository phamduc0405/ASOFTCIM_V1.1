using ASOFTCIM.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.MVVM.NavigationService
{
    public interface INavigationService
    {
        //void NavigateTo1<TViewModel>() where TViewModel : ViewModels.BaseViewModels, new();
        void NavigateTo<TViewModel>() where TViewModel : BaseViewModels;
    }
    
}
