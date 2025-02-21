using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASOFTCIM.Message.PLC2Cim.Recv
{
    public class CELLIDREADINGRESULTREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                

List<IWordModel> word = bit.LstWord;
                READER read = new READER();
                read.READERRESULTCODE = word.FirstOrDefault(x => x.Item == "READERRESULTCODE").GetValue(eq.PLC);
                read.READERID = word.FirstOrDefault(x => x.Item == "READERID").GetValue(eq.PLC);
                string cellId = word.FirstOrDefault(x => x.Item == "CELLID").GetValue(eq.PLC);
                eq.SendS6F11_601( read, cellId);
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }
}
