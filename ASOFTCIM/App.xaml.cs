using ASOFTCIM.Helper;
using ASOFTCIM.Init;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.ViewModels;
using ASOFTCIM.MVVM.Views.Popup;
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
            ShowMainWindowWithWaitingPopup();
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

            Application.Current.Dispatcher.Invoke(() =>
            {
                LogHelper.Error(e.Exception, "Unhandled exception in WPF UI thread");
                MessageBox.Show($"Ứng dụng gặp sự cố:\n\n{e.Exception.GetType().Name}: {e.Exception.Message}",
                     "Lỗi chưa xử lý (UI Thread)",
                     MessageBoxButton.OK,
                     MessageBoxImage.Error
                    );

            e.Handled = true; // Đánh dấu là đã xử lý, ứng dụng không crash
            });
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
            LogHelper.SetBaseFolder(@"D:\LOGCIM");

            LogHelper.StatStop("Start App");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            LogHelper.StatStop("Stop App.");
            LogHelper.Stop();
            base.OnExit(e);
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

            // Kiểm tra các file cần thiết trước khi khởi động 
            var fileChecker = new FileCheckerService();
            if (!fileChecker.CheckAllRequiredFiles(out string errorMessage))
            {
                MessageBox.Show(
                    errorMessage,
                    "Lỗi khởi động",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                Shutdown();
                return;
            }
            await waitingPopup.SmoothProgressToAsync(30 * 3.6);

            // khởi tạo Controller trước 
            var controller = Controller.Instange;
            if (!controller.IsReadConfig)
            {
                MessageBox.Show($"Cần kiểm tra File Config",
                    "Ứng dụng gặp sự cố : Lỗi file Config",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                   );
                Application.Current.Shutdown();
                return;
            }
            await waitingPopup.SmoothProgressToAsync(50 * 3.6);

            // sau khi có controller thì khởi tạo UI
            var mainWindow = new MainWindow();
            await waitingPopup.SmoothProgressToAsync(80 * 3.6);

            mainWindow.Loaded += (s, a) =>
            {
                waitingPopup.Close();
            };
            mainWindow.DataContext = new MainWindowViewModel(mainWindow);
            await waitingPopup.SmoothProgressToAsync(100 * 3.6);
            mainWindow.Show();
        }
        
    }
}
