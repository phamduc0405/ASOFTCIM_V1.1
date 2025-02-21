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
    public class PACKINGJOBSTARTREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                List<IWordModel> word = bit.LstWord;
                PACKING packing = new PACKING();
                packing.SBPID = word.FirstOrDefault(x => x.Item == "SBPID").GetValue(eq.PLC);
                packing.SBPREALWEIGHT = word.FirstOrDefault(x => x.Item == "SBPREALWEIGHT").GetValue(eq.PLC);
                packing.CARTONID = word.FirstOrDefault(x => x.Item == "CARTONID").GetValue(eq.PLC);
                packing.CARTONREALWEIGHT = word.FirstOrDefault(x => x.Item == "CARTONREALWEIGHT").GetValue(eq.PLC);
                packing.CHECKERNAME = word.FirstOrDefault(x => x.Item == "CHECKERNAME").GetValue(eq.PLC);
                packing.ERRORMESSAGE = word.FirstOrDefault(x => x.Item == "ERRORMESSAGE").GetValue(eq.PLC);
                eq.SendS6F11_801("802",packing);
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
