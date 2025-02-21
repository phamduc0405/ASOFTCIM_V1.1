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
    public class UNITOPCALLCONFIRMREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                

List<IWordModel> word = bit.LstWord;
                UNITOPCALLCONFIRM unit = new UNITOPCALLCONFIRM();
                unit.UNIT.UNITID = word.FirstOrDefault(x => x.Item == "UNITNUMBER").GetValue(eq.PLC);
                OPCALL op = new OPCALL();
                op.OPCALLID = word.FirstOrDefault(x => x.Item == "OPCALLID").GetValue(eq.PLC);
                op.MESSAGE = word.FirstOrDefault(x => x.Item == "OPCALLMESSAGE").GetValue(eq.PLC);
                unit.OPCALLs.Add(op);
                eq.SendS6F11_513( unit, "513");
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
