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
    public class CASSETTESTATECHANGEEVENT
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                

List<IWordModel> word = bit.LstWord;
                CASSETTESTATECHANGE castle = new CASSETTESTATECHANGE();
                castle.PORTID = word.FirstOrDefault(x => x.Item == "PORTID").GetValue(eq.PLC);
                castle.PORTAVAILABLESTATE = word.FirstOrDefault(x => x.Item == "PORTAVAILABLESTATE").GetValue(eq.PLC);
                castle.PORTACCESSMODE = word.FirstOrDefault(x => x.Item == "PORTACCESSMODE").GetValue(eq.PLC);
                castle.PORTTRANSFERSTATE = word.FirstOrDefault(x => x.Item == "PORTTRANSFERSTATE").GetValue(eq.PLC);
                castle.PORTPROCESSINGSTATE = word.FirstOrDefault(x => x.Item == "PORTPROCESSINGSTATE").GetValue(eq.PLC);
                castle.JOBID = word.FirstOrDefault(x => x.Item == "JOBID").GetValue(eq.PLC);
                castle.JOBTYPE = word.FirstOrDefault(x => x.Item == "JOBTYPE").GetValue(eq.PLC);

                string ceid = word.FirstOrDefault(x => x.Item == "CEID").GetValue(eq.PLC);
                eq.SendS6F11_350_356( castle, ceid);
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
