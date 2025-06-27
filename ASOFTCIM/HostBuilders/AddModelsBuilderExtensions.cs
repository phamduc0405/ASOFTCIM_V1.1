using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.MVVM.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ASOFTCIM.HostBuilders
{
    public static class AddModelsBuilderExtensions
    {
        public static IHostBuilder AddModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<HomeModel>();
                services.AddSingleton<FDCModel>();
                services.AddSingleton<ASOFTCIM.MVVM.Models.ECMModel>();
                services.AddSingleton<AlarmModel>();
                services.AddSingleton<MaterialModel>();
                services.AddSingleton<ConfigModel>();
            });
            return host;
        }
    }
}
