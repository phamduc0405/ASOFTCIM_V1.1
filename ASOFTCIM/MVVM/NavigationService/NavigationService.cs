using ASOFTCIM.MVVM.ViewModels;
using ASOFTCIM.MVVM.Views.FDC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ASOFTCIM.MVVM.NavigationService
{
    public class NavigationService : INavigationService
    {
        private readonly Action<UserControl> _setView;
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(Action<UserControl> setView, IServiceProvider serviceProvider)
        {
            _setView = setView;
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TViewModel>() where TViewModel : BaseViewModels
        {
            // Resolve ViewModel từ DI
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();

            // Tìm View tương ứng dựa theo quy ước đặt tên: FDCViewModel => FDCView
            var viewTypeName = typeof(TViewModel).Name.Replace("ViewModel", "View");
            var viewType = Assembly.GetExecutingAssembly()
                                   .GetTypes()
                                   .FirstOrDefault(t => t.Name == viewTypeName && typeof(UserControl).IsAssignableFrom(t));

            if (viewType == null)
            {
                throw new InvalidOperationException($"Không tìm thấy View tương ứng cho {typeof(TViewModel).Name}");
            }

            var viewInstance = Activator.CreateInstance(viewType) as UserControl;
            if (viewInstance != null)
            {
                viewInstance.DataContext = viewModel;
                _setView(viewInstance);
            }
        }

    }

}
