using A_SOFT.Cim;
using A_SOFT.CMM.INIT;
using AComm.TCPIP;
using ASOFTCIM.Config;
using ASOFTCIM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static A_SOFT.PLC.MelsecIF;

namespace ASOFTCIM.Message.PLC2Cim.Recv
{
    public class ALARMREPORT
    {


        private static SemaphoreSlim _semaphore = new SemaphoreSlim(1); 

        public async Task Excute(ACIM eq, object body)
        {
            await _semaphore.WaitAsync();
            try
            {
                eq.EqpData.TransactionSys += 1;
                WordStatus word = (WordStatus)body;
                int alid = word.Index % (eq.PLCH.Words.FirstOrDefault(x => x.Area.Contains("ALARM")).Address * DefineConst.ShortBits) + 1;
                Alarm alarm;
                lock (eq.EqpData.ALS) 
                {
                    alarm = eq.EqpData.ALS.FirstOrDefault(x => x.ALID == alid);
                    if (alarm != null)
                    {
                        alarm.ALST = word.IsOn ? "1" : "2";
                        alarm.EQPID = eq.EQPID.ToString();
                        alarm.TIME = DateTime.Now.ToString("hh:mm:ss.fff");
                    }
                    else
                    {
                        alarm = new Alarm
                        {
                            ALID = (uint)alid,
                            ALST = word.IsOn ? "1" : "2",
                            EQPID = eq.EQPID.ToString(),
                            ALCD = "2",
                            ALTEXT = "Not Registered"
                        };
                    }
                }

                if (alarm != null)
                {
                    await AddAlarmAsync(eq, alarm);
                    eq.SendS5F1(alarm);
                }
            }
            catch(Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task AddAlarmAsync(ACIM acim, Alarm alarm)
        {
            await Task.Run(() =>
            {

                lock (acim.EqpData.AlarmHistory)
                {
                    var alarmh = new Alarm
                    {
                        ALST = alarm.ALST,
                        ALID = alarm.ALID,
                        ALCD = alarm.ALCD,
                        EQPID = alarm.EQPID,
                        TIME = alarm.TIME,
                        ALTEXT = alarm.ALTEXT
                    };
                    acim.EqpData.AlarmHistory.Add(alarmh);
                    if (acim.EqpData.AlarmHistory.Count > 100)
                    {
                        acim.EqpData.AlarmHistory.RemoveAt(0);
                    }
                }

                lock (acim.EqpData.ALS) 
                {
                    acim.EqpData.ALS.RemoveAll(x => x.ALID == alarm.ALID);
                    acim.EqpData.ALS.Add(alarm);
                }

                lock (acim.EqpData.CurrAlarm)
                {
                    acim.EqpData.CurrAlarm.RemoveAll(x => x.ALID == alarm.ALID);
                    if (alarm.ALST == "1")
                    {
                        acim.EqpData.CurrAlarm.Add(alarm);
                    }
                }
            });
        }

    }
}
