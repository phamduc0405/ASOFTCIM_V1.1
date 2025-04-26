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

namespace ASOFTCIM.Init
{
    public static class LogFDC
    {
        private static string BaseFolder = @"D:\\Auto_Test\\FDCLog";
        private static readonly BlockingCollection<string> LogQueue = new BlockingCollection<string>();
        private static readonly CancellationTokenSource Cts = new CancellationTokenSource();

        static LogFDC()
        {
            Task.Factory.StartNew(ProcessLogQueue, TaskCreationOptions.LongRunning);
        }

        public static void SetBasePath(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                BaseFolder = path;
            }
        }

        public static void Log(string content)
        {
            string logLine = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}, {1}", DateTime.Now, content);
            LogQueue.Add(logLine);
        }

        private static void ProcessLogQueue()
        {
            foreach (var log in LogQueue.GetConsumingEnumerable(Cts.Token))
            {
                try
                {
                    DateTime now = DateTime.Now;
                    string folder = Path.Combine(BaseFolder, "FDC", now.ToString("yyyy"), now.ToString("MM"));
                    Directory.CreateDirectory(folder);

                    string filePath = Path.Combine(folder, now.ToString("dd") + ".txt");
                    File.AppendAllText(filePath, log + Environment.NewLine, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", MethodBase.GetCurrentMethod().DeclaringType.Name.ToString(), MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
            }
        }

        public static void Stop()
        {
            LogQueue.CompleteAdding();
            Cts.Cancel();
        }
    }
}
