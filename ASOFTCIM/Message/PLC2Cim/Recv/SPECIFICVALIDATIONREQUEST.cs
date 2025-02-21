using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Message.PLC2Cim.Recv
{
    public class SPECIFICVALIDATIONREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                

List<IWordModel> word = bit.LstWord;
                Data.SPECIFICVALIDATIONREQUEST spec = new Data.SPECIFICVALIDATIONREQUEST();
                spec.OPTIONCODE = word.FirstOrDefault(x => x.Item == "OPTIONCODE").GetValue(eq.PLC);
                spec.UNIQUEID = word.FirstOrDefault(x => x.Item == "CELLID").GetValue(eq.PLC);
                spec.OPTIONINFO = word.FirstOrDefault(x => x.Item == "OPTIONINFO").GetValue(eq.PLC);
                eq.SendS6F203( spec);
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
