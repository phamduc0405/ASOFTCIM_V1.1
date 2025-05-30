﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ASOFTCIM.Init
{
    public static class WpfSingleInstance
    {
        private static bool AlreadyProcessedOnThisInstance;

        internal static void Make(string appName, bool uniquePerUser = true)
        {
            if (AlreadyProcessedOnThisInstance)
            {
                return;
            }
            AlreadyProcessedOnThisInstance = true;

            Application app = Application.Current;

            string eventName = uniquePerUser
                ? $"{appName}-{Environment.MachineName}-{Environment.UserDomainName}-{Environment.UserName}"
                : $"{appName}-{Environment.MachineName}";

            bool isSecondaryInstance = true;

            EventWaitHandle eventWaitHandle = null;
            try
            {
                eventWaitHandle = EventWaitHandle.OpenExisting(eventName);
            }
            catch
            {
                // This code only runs on the first instance.
                isSecondaryInstance = false;
            }

            if (isSecondaryInstance)
            {
                ActivateFirstInstanceWindow(eventWaitHandle);

                // Let's produce a non-interceptable exit (2009 year approach).
                Environment.Exit(0);
            }

            RegisterFirstInstanceWindowActivation(app, eventName);
        }

        private static void ActivateFirstInstanceWindow(EventWaitHandle eventWaitHandle)
        {
            // Let's notify the first instance to activate its main window.
            eventWaitHandle.Set();
        }

        private static void RegisterFirstInstanceWindowActivation(Application app, string eventName)
        {
            EventWaitHandle eventWaitHandle = new EventWaitHandle(
                false,
                EventResetMode.AutoReset,
                eventName);

            ThreadPool.RegisterWaitForSingleObject(eventWaitHandle, WaitOrTimerCallback, app, Timeout.Infinite, false);

            eventWaitHandle.Close();
        }

        private static void WaitOrTimerCallback(object state, bool timedOut)
        {
            Application app = (Application)state;
            app.Dispatcher.BeginInvoke(new Action(() =>
            {
                Application.Current.MainWindow.Activate();
            }));
        }
    }
}
