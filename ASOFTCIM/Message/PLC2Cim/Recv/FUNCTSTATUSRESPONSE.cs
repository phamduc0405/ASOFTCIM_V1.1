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
    public class FUNCTSTATUSRESPONSE
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                bool isSend = false;
                //eq.EqpData.TransactionSys += 1;
                List<WordModel> word = (List<WordModel>)body;
                foreach (var item in eq.EqpData.FUNCTIONSTATE.GetType().GetProperties())
                {
                    if (word.Any(x => x.Item == item.Name))
                    {
                        WordModel w = word.FirstOrDefault(x => x.Item == item.Name);
                        var a = item.GetValue(eq.EqpData.FUNCTIONSTATE, null) == null ? "" : item.GetValue(eq.EqpData.FUNCTIONSTATE, null).ToString();
                        if (w.GetValue() != a)
                        {
                            FUNCTION func = new FUNCTION();

                            item.SetValue(eq.EqpData.FUNCTIONSTATE, w.GetValue());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }
}
