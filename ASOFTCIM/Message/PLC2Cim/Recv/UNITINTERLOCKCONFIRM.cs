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
    public class UNITINTERLOCKCONFIRM
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                

List<IWordModel> word = bit.LstWord;
                Data.UNITINTERLOCKCONFIRM unit = new Data.UNITINTERLOCKCONFIRM();
                INTERLOCK interlock = new INTERLOCK();
                interlock.INTERLOCKID = word.FirstOrDefault(x => x.Item == "INTERLOCKID").GetValue(eq.PLC);
                interlock.MESSAGE = word.FirstOrDefault(x => x.Item == "INTERLOCKMESSAGE").GetValue(eq.PLC);
                unit.INTERLOCKs.Add(interlock);
                eq.SendS6F11_514( unit, "514");
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
