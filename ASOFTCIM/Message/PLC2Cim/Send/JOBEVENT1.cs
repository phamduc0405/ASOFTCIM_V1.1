using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using ASOFTCIM.Data;
using ASOFTCIM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASOFTCIM.Message.PLC2Cim.Send
{
    public class JOBEVENT1
    {
        public JOBEVENT1(PLCHelper plcdata, JobProcess job)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "RCMD").SetValue = job.RCMD;
                word.FirstOrDefault(x => x.Item == "PARENTLOT").SetValue = job.PARENTLOT;
                word.FirstOrDefault(x => x.Item == "RFID").SetValue = job.RFID;
                word.FirstOrDefault(x => x.Item == "PORTNO").SetValue = job.PORTNO;
                word.FirstOrDefault(x => x.Item == "PPID").SetValue = job.PPID;
                word.FirstOrDefault(x => x.Item == "CELLCNT").SetValue = job.CELLCNT;
                word.FirstOrDefault(x => x.Item == "MESSAGE").SetValue = job.MESSAGE;

                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name);
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }
}
