using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ASOFTCIM.Init
{
    public class FileCheckerService
    {
        public bool CheckAllRequiredFiles(out string errorMessage)
        {
            errorMessage = "";

            // Check config file
            var configFilePath = Path.Combine("C:\\CimConfig\\Setting", "SystemConfig.setting");
            if (!File.Exists(configFilePath))
            {
                errorMessage = $"Không tìm thấy file cấu hình: {configFilePath}";
                return false;
            }

            // Check required DLLs
            var requiredFiles = new[]
            {
                "A_SOFT.CMM.dll",
                "A_SOFT.CMM.UI.dll",
                "A_SOFT.Ctl.Mitsu.dll",
                "A_SOFT.Ctl.SecGem.dll",
                "A_SOFT.Ctl.Tcp.dll",
                "ASOFTCIM.exe",
                "ASOFTCIM.exe.config",
                "AsoftLicense.dll",
                "EPPlus.dll",
                "EPPlus.Interfaces.dll",
                "EPPlus.System.Drawing.dll",
                "EPPlus.xml",
                "FontAwesome.Sharp.dll",
                "HPSocket.Net.dll",
                "HPSocket.Net.pdb",
                "HPSocket.Net.xml",
                "HPSocket4C.dll",
                "LiveCharts.dll",
                "LiveCharts.pdb",
                "LiveCharts.Wpf.dll",
                "LiveCharts.Wpf.pdb",
                "LiveCharts.Wpf.xml",
                "LiveCharts.xml",
                "Microsoft.IO.RecyclableMemoryStream.dll",
                "Microsoft.IO.RecyclableMemoryStream.xml",
                "Microsoft.Xaml.Behaviors.dll",
                "Microsoft.Xaml.Behaviors.pdb",
                "Microsoft.Xaml.Behaviors.xml",
                "Mitsu3E.dll",
                "System.ComponentModel.Annotations.dll"
            };

            foreach (var file in requiredFiles)
            {
                var dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
                if (!File.Exists(dllPath))
                {
                    errorMessage = $"Không tìm thấy thư viện cần thiết: {file}";
                    return false;
                }
            }

            // Check executable files or other resources nếu cần
            // Ví dụ:
            var appExecutable = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ASOFTCIM.exe");
            if (!File.Exists(appExecutable))
            {
                errorMessage = $"Không tìm thấy file chạy chính: {appExecutable}";
                return false;
            }

            return true;
        }
    }
}
