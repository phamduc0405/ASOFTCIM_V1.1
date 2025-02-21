using A_SOFT.CMM.INIT;
using A_SOFT.Ctl.Mitsu.Model;
using A_SOFT.PLC;
using ASOFTCIM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Message.PLC2Cim.Recv
{
    public class CARRIERLOADEREVENTREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                
                
                List<IWordModel> word = bit.LstWord;
                CARRIERCHANGE carrier = new CARRIERCHANGE();
                carrier.PARENTLOT = word.FirstOrDefault(x => x.Item == "PARENTLOT").GetValue(eq.PLC);
                carrier.RFID = word.FirstOrDefault(x => x.Item == "RFID").GetValue(eq.PLC);
                carrier.PORTNO_1 = word.FirstOrDefault(x => x.Item == "PORTNO").GetValue(eq.PLC);
                carrier.PPID = word.FirstOrDefault(x => x.Item == "PPID").GetValue(eq.PLC);
                string ceid = word.FirstOrDefault(x => x.Item == "EVENTTYPE").GetValue(eq.PLC);
                eq.SendS6F11_250_253( carrier, ceid);
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
