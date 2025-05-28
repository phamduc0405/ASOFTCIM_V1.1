using A_SOFT.CMM.INIT;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASOFTCIM.Helper
{
    public static class LogHelper
    {
        private static string _baseFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        private static readonly BlockingCollection<string> _logQueue = new BlockingCollection<string>();
        private static readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private static Task _logTask;
        private static Thread _monitorThread;
        private static bool _isAppRunning = true;
        private static Thread _cleanupThread;
        private static int _timeToKeep;
        private static int _countdelete = 0;
        static LogHelper()
        {
            _logTask = Task.Factory.StartNew(ProcessLogQueue, _cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            _monitorThread = new Thread(MonitorAppStatus);
            _monitorThread.Start();
            //_cleanupThread = new Thread(DailyCleanupTask);
            //_cleanupThread.Start();
        }

        // Method to monitor the app status
        private static void MonitorAppStatus()
        {
            while (_isAppRunning)
            {
                Thread.Sleep(1000);
                try
                {
                    if (AppDomain.CurrentDomain.FriendlyName == "ASOFTCIM.exe")
                    {
                        StatStop("Alive");
                    }
                    if (DateTime.Now.Hour == 23 && DateTime.Now.Minute == 50 && _countdelete == 0)
                    {
                        
                        DeleteOldLogFolders();
                        _countdelete++;
                    }
                    if(DateTime.Now.Hour == 23 && DateTime.Now.Minute == 51 && _countdelete != 0)
                    {
                        _countdelete = 0;
                    }
                }
                catch (Exception ex) 
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
                
            }
        }

        public static void StopMonitoring()
        {
            _isAppRunning = false;
            _monitorThread?.Join();
            _cleanupThread?.Abort();
        }

        /// <summary>
        /// Đặt lại thư mục lưu log
        /// </summary>
        public static void SetBaseFolder(string folderPath)
        {
            if (!string.IsNullOrWhiteSpace(folderPath))
            {
                _baseFolder = folderPath;
                Directory.CreateDirectory(_baseFolder);
            }
        }
        public static void SetDaysToKeep(int times)
        {
            _timeToKeep = times;
        }

        /// <summary>
        /// Ghi log thông thường (Info)
        /// </summary>
        public static void Info(string message)
        {
            AddLog("INFO", message);
        }

        /// <summary>
        /// Ghi log cảnh báo (Warning)
        /// </summary>
        public static void Warn(string message)
        {
            AddLog("WARN", message);
        }

        /// <summary>
        /// Ghi log lỗi (Error)
        /// </summary>
        public static void Error(string message)
        {
            AddLog("ERROR", message);
        }
        public static void StatStop(string message)
        {
            AddLog("STARTSTOP", message);
        }

        /// <summary>
        /// Ghi log lỗi kèm Exception
        /// </summary>
        public static void Error(Exception ex, string message = "")
        {
            string fullMessage = $"{message} | Exception: {ex.Message} | StackTrace: {ex.StackTrace}";
            AddLog("ERROR", fullMessage);
        }

        private static void AddLog(string level, string content)
        {
            if (!_logQueue.IsCompleted)
            {
                string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{level}] {content}";
                _logQueue.Add(logLine);
            }
        }

        private static void ProcessLogQueue()
        {
            try
            {
                foreach (var log in _logQueue.GetConsumingEnumerable(_cts.Token))
                {
                    try
                    {
                        DateTime now = DateTime.Now;
                        string logLevel = ExtractLogLevel(log);
                        string folderPath = Path.Combine(_baseFolder, logLevel, now.ToString("yyyy"), now.ToString("MM"));
                        Directory.CreateDirectory(folderPath);
                        string filePath = Path.Combine(folderPath, $"{now:dd}.log");
                        File.AppendAllText(filePath, log + Environment.NewLine, Encoding.UTF8);
                    }
                    catch (Exception ex)
                    {
                        // Log any errors during file writing
                        try
                        {
                            string fallbackPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogHelperError.log");
                            string errorMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [FATAL] Error writing log: {ex.Message}";
                            File.AppendAllText(fallbackPath, errorMessage + Environment.NewLine, Encoding.UTF8);
                        }
                        catch
                        {
                            var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                            LogTxt.Add(LogTxt.Type.Exception, debug);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // This exception is expected when cancellation is requested.
                // No need to log it; just exit the method.
            }
            catch (Exception ex)
            {
                // Catch any other unexpected exceptions and log them.
                try
                {
                    string fallbackPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogHelperError.log");
                    string errorMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [FATAL] Unexpected error: {ex.Message}";
                    File.AppendAllText(fallbackPath, errorMessage + Environment.NewLine, Encoding.UTF8);
                }
                catch (Exception e)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, e.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            }
        }

        // Phương thức trích xuất level từ log (INFO, WARN, ERROR)
        private static string ExtractLogLevel(string log)
        {
            if (log.Contains("[INFO]"))
                return "INFO";
            else if (log.Contains("[WARN]"))
                return "WARN";
            else if (log.Contains("[ERROR]"))
                return "ERROR";
            else if (log.Contains("[STARTSTOP]"))
                return "STARTSTOP";
            else
                return "UNKNOWN"; // Nếu không xác định được level thì lưu vào thư mục UNKNOWN
        }

        /// <summary>
        /// Gọi khi app shutdown để đảm bảo ghi hết log
        /// </summary>
        public static void Stop()
        {
            _logQueue.CompleteAdding();
            
            _cts.Cancel();
            _logTask?.Wait();  
            StopMonitoring(); 
            //_isAppRunning = false;
            //_cleanupThread?.Join();

        }
        private static void DeleteOldLogFolders(int monthsToKeep = 2)
        {
            try
            {
                var now = DateTime.Now;
                var baseDir = new DirectoryInfo(_baseFolder);

                foreach (var levelDir in baseDir.GetDirectories()) 
                {
                    foreach (var yearDir in levelDir.GetDirectories()) 
                    {
                        foreach (var monthDir in yearDir.GetDirectories()) 
                        {
                            if (int.TryParse(yearDir.Name, out int year) && int.TryParse(monthDir.Name, out int month))
                            {
                                DateTime folderDate;
                                try
                                {
                                    folderDate = new DateTime(year, month, 1);
                                }
                                catch
                                {
                                    continue; 
                                }

                                var compareDate = new DateTime(now.Year, now.Month, 1).AddMonths(-monthsToKeep);
                                if (folderDate < now.AddMonths(-monthsToKeep))
                                {
                                    try
                                    {
                                        Directory.Delete(monthDir.FullName, true);
                                        Info($"Deleted old log folder: {monthDir.FullName}");
                                    }
                                    catch (Exception ex)
                                    {
                                        var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                                        LogTxt.Add(LogTxt.Type.Exception, debug);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error($"Error while deleting old log folders: {ex.Message}");
            }
        }
        private static void DailyCleanupTask()
        {
            while (_isAppRunning)
            {
                try
                {
                    //DeleteOldLogFolders(_timeToKeep);
                    DeleteOldLogFolders(2);
                }
                catch (Exception ex)
                {
                    Error($"Cleanup task error: {ex.Message}");
                }

                Thread.Sleep(TimeSpan.FromDays(1));
            }
        }
    }
}
