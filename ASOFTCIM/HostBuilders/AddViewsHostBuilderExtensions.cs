using ASOFTCIM.MVVM.ViewModels;
using ASOFTCIM.MVVM.Views.Alarm;
using ASOFTCIM.MVVM.Views.Config;
using ASOFTCIM.MVVM.Views.ECM;
using ASOFTCIM.MVVM.Views.FDC;
using ASOFTCIM.MVVM.Views.Home;
using ASOFTCIM.MVVM.Views.Material;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.HostBuilders
{
    public static class AddViewsHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>(provider =>
                {
                    var window = new MainWindow();
                    var viewModel = new MainWindowViewModel(window, provider);
                    window.DataContext = viewModel;
                    return window;
                });
                services.AddSingleton<HomeView>(provider =>
                {

                    var view = provider.GetRequiredService<HomeView>();
                    view.DataContext = provider.GetRequiredService<HomeViewModel>();
                    return view;
                });
                services.AddSingleton<FDCView>(provider =>
                {
                    var view = provider.GetRequiredService<FDCView>();
                    view.DataContext = provider.GetRequiredService<FDCViewModel>();
                    return view;
                });
                services.AddSingleton<ECMView>(provider =>
                {
                    var view = provider.GetRequiredService<ECMView>();
                    view.DataContext = provider.GetRequiredService<ECMViewModel>();
                    return view;
                });
                services.AddSingleton<AlarmView>(provider =>
                {
                    var view = provider.GetRequiredService<AlarmView>();
                    view.DataContext = provider.GetRequiredService<AlarmViewModel>();
                    return view;
                });
                services.AddSingleton<MaterialView>(provider =>
                {
                    var view = provider.GetRequiredService<MaterialView>();
                    view.DataContext = provider.GetRequiredService<MaterialViewModel>();
                    return view;
                });
                services.AddSingleton<ConfigView>(provider =>
                {
                    var view = provider.GetRequiredService<ConfigView>();
                    view.DataContext = provider.GetRequiredService<ConfigViewModel>();
                    return view;
                });
                services.AddSingleton<ConfigMainView>(provider =>
                {
                    var view = provider.GetRequiredService<ConfigMainView>();
                    view.DataContext = provider.GetRequiredService<ConfigMainViewModel>();
                    return view;
                });
            });

            return host;
        }
    }
}
