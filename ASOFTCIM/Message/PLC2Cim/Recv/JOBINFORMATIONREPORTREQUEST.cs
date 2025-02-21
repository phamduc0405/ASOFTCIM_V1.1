using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Message.PLC2Cim.Recv
{
    public class JOBINFORMATIONREPORTREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                

List<IWordModel> word = bit.LstWord;
                JOB job = new JOB();
                job.FINALEQPID = word.FirstOrDefault(x => x.Item == "FINALEQPID").GetValue(eq.PLC);
                job.JOBID = word.FirstOrDefault(x => x.Item == "JOBID").GetValue(eq.PLC);
                job.JOBTYPE = word.FirstOrDefault(x => x.Item == "JOBTYPE").GetValue(eq.PLC);
                job.READERID = word.FirstOrDefault(x => x.Item == "READERID").GetValue(eq.PLC);
                job.READERRESULT = word.FirstOrDefault(x => x.Item == "READERRESULT").GetValue(eq.PLC);
                job.OPERID = word.FirstOrDefault(x => x.Item == "OPERID").GetValue(eq.PLC);
                eq.SendS6F11_608( job);
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
