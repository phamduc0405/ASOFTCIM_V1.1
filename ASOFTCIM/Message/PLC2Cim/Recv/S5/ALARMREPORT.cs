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
using System.Threading.Tasks;
using static A_SOFT.PLC.MelsecIF;

namespace ASOFTCIM.Message.PLC2Cim.Recv
{
    public class ALARMREPORT
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                eq.EqpData.TransactionSys += 1;
                WordStatus word = (WordStatus)body;
                int alid = word.Index % (eq.PLCH.Words.FirstOrDefault(x => x.Area.Contains("ALARM")).Address * DefineConst.ShortBits) + 1;
                Alarm alarm;
                if (eq.EqpData.ALS.Any(x => x.ALID == alid))
                {
                    alarm = eq.EqpData.ALS.FirstOrDefault(x => x.ALID == alid);
                    alarm.ALST = word.IsOn ? "1" : "2";
                    alarm.EQPID = eq.EQPID.ToString();
                    alarm.TIME = DateTime.Now.ToString("hh:mm:ss.fff");
                }
                else
                {
                    alarm = new Alarm();
                    alarm.ALID = (uint)alid;
                    alarm.ALST = word.IsOn ? "1" : "2";
                    alarm.EQPID = eq.EQPID.ToString();
                    alarm.ALCD = "2";
                    alarm.ALTEXT = "Not Registrated";
                }
                if (alarm == null)
                    return;
                eq.SendS5F1( alarm);
                //new S5F1().SendMessage( alarm);
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    }
}
