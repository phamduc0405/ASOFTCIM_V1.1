using ASOFTCIM.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                
                services.AddTransient<MainWindowViewModel>();
                services.AddTransient<HomeViewModel>();
                services.AddTransient<FDCViewModel>();
                services.AddTransient<ECMViewModel>();
                services.AddTransient<AlarmViewModel>();
                services.AddTransient<MaterialViewModel>();
                services.AddTransient<ConfigViewModel>();
                services.AddTransient<ConfigMainViewModel>();
            });
            return host;
        }
    }
}
