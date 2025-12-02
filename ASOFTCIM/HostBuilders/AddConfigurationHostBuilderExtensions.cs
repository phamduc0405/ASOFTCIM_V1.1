using LiveCharts.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ASOFTCIM.Init;
using ASOFTCIM.MainControl;
using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using A_SOFT.CMM.HELPER;
using ASOFTCIM.Config;
using Microsoft.Extensions.Configuration;
using A_SOFT.PLC;
using ASOFTCIM.Helper;
using System.Collections.Generic;
using System.Linq;
using ASOFTCIM.MVVM.Models;
using ASOFTCIM.Data;

namespace ASOFTCIM.HostBuilders
{
    public static class AddConfigurationHostBuilderExtensions
    {
        public static IHostBuilder AddConfiguration(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                services.AddSingleton<App>();
                services.AddSingleton<FileCheckerService>();
                
                // Kiểm tra file config
                var xmlPath = Path.Combine("C:\\CimConfig\\Setting", "SystemConfig.setting");
                if (!File.Exists(xmlPath))
                {
                    //Dispatcher.CurrentDispatcher.Invoke(() =>
                    //{
                    //    MessageBox.Show("Không tìm thấy file cấu hình: SystemConfig.setting",
                    //        "Lỗi khởi động", MessageBoxButton.OK, MessageBoxImage.Error);
                    //    Application.Current.Shutdown();
                    //});
                    return;
                }

                // Đọc file config
                var content = File.ReadAllText(xmlPath);
                var config = XmlHelper<EquipmentConfig>.DeserializeFromString(content);
                services.AddSingleton(config); // Inject EquipmentConfig


                services.AddSingleton<CimHelper>(sp =>
                {
                    var equipmentConfig = sp.GetRequiredService<EquipmentConfig>();
                    return new CimHelper(equipmentConfig.EQPID);
                });
                services.AddSingleton<PlcComm>();
                services.AddSingleton<PLCHelper>(sp =>
                {
                    foreach (var b in config.PLCHelper.Bits)
                    {
                        List<WordModel> wm = config.PLCHelper.Words.Where(x => x.BitEvent == ($"{b.PLCDevice}{b.PLCHexAdd}") || x.BitEvent == ($"{b.PLCDevice}{b.PCHexAdd}")).ToList();
                        b.LstWord.AddRange(wm);
                        List<Data.MaterialModel> material = config.PLCHelper.Materrials.Where(x => x.BitEvent.Contains($"{b.PLCDevice}{b.PLCHexAdd}")).ToList();
                        b.LstWord.AddRange(material);
                    }
                    var equipmentConfig = sp.GetRequiredService<EquipmentConfig>();
                    return equipmentConfig.PLCHelper;
                });
                services.AddSingleton<ACIM>();
                services.AddSingleton<Controller>();

            });

            host.ConfigureAppConfiguration(c =>
            {
                c.AddEnvironmentVariables(); 
            });

            return host;
        }
    }
}
