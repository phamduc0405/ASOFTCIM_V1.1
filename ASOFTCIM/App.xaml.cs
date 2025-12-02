using ASOFTCIM.Helper;
using ASOFTCIM.HostBuilders;
using ASOFTCIM.Init;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.ViewModels;
using ASOFTCIM.MVVM.Views.Popup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ASOFTCIM
{
    public partial class App : Application
    {
        public readonly IHost _appHost;

        public App()
        {
            _appHost = CreateHostBuilder().Build();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }
        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfiguration()
                .AddModels()
                .AddViews()
                .AddViewModels();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LogHelper.SetBaseFolder(@"D:\LOGCIM");
            LogHelper.StatStop("Start App");

            ShowMainWindowWithWaitingPopup();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            LogHelper.StatStop("Stop App.");
            LogHelper.Stop();

            _appHost?.Dispose();
            base.OnExit(e);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            LogHelper.Error(e.Exception, "Unhandled exception in WPF UI thread");
            MessageBox.Show($"Ứng dụng gặp sự cố:\n\n{e.Exception.GetType().Name}: {e.Exception.Message}",
                "Lỗi chưa xử lý (UI Thread)", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                LogHelper.Error(ex, "Unhandled exception in non-UI thread");
                MessageBox.Show(
                    $"Ứng dụng gặp sự cố:\n\n{ex.GetType().Name}: {ex.Message}",
                    "Lỗi chưa xử lý (Thread nền)",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
            else
            {
                MessageBox.Show("Ứng dụng gặp sự cố không xác định.");
            }
        }

        private async void ShowMainWindowWithWaitingPopup()
        {
            //cửa sổ chờ chạy chương trình
            var waitingPopup = new WaittingDisplay();
            await Dispatcher.InvokeAsync(() => waitingPopup.Show());
            waitingPopup.Progress = 0 * 3.6;

            //kiểm tra xem chương trình có đang chạy không
            WpfSingleInstance.Make("ASOFTCIM");
            await waitingPopup.SmoothProgressToAsync(10 * 3.6);

            //Kiểm tra các file cần thiết trước khi khởi động
            var fileChecker = _appHost.Services.GetRequiredService<FileCheckerService>();
            if (!fileChecker.CheckAllRequiredFiles(out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Lỗi khởi động", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
                return;
            }
            await waitingPopup.SmoothProgressToAsync(30 * 3.6);

            // khởi tạo Controller trước 
            //var controller = _appHost.Services.GetRequiredService<Controller>();
            await waitingPopup.SmoothProgressToAsync(50 * 3.6);

            // sau khi có controller thì khởi tạo UI
            var mainWindow = _appHost.Services.GetRequiredService<MainWindow>();

            await waitingPopup.SmoothProgressToAsync(80 * 3.6);
            mainWindow.Loaded += (o, e) => waitingPopup.Close();
            await waitingPopup.SmoothProgressToAsync(100 * 3.6);

            mainWindow.Show();
        }

        
    }

}
