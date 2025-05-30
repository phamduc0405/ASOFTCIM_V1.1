using ASOFTCIM.Helper;
using ASOFTCIM.Init;
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
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            WpfSingleInstance.Make("ASOFTCIM");
            var mainview = new MainWindow();
            mainview.Show();
        }
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        /// <summary>
        /// Bắt tất cả exception chưa được xử lý trong UI thread (WPF)
        /// </summary>
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            LogHelper.Error(e.Exception, "Unhandled exception in WPF UI thread");
            MessageBox.Show($"Ứng dụng gặp sự cố:\n\n{e.Exception.GetType().Name}: {e.Exception.Message}",
                             "Lỗi chưa xử lý (UI Thread)",
                             MessageBoxButton.OK,
                             MessageBoxImage.Error
                            );

            e.Handled = true; // Đánh dấu là đã xử lý, ứng dụng không crash
        }

        /// <summary>
        /// Bắt các exception chưa xử lý trong các thread khác (non-UI threads)
        /// </summary>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LogHelper.SetBaseFolder(@"C:\LOGCIM");

            LogHelper.StatStop("Start App");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            LogHelper.StatStop("Stop App.");
            LogHelper.Stop();
            base.OnExit(e);
        }
    }
}
