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
    public class EQUIPMENTFUNCTIONCHANGEEVENT
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                bool isSend = false;
                eq.EqpData.TransactionSys += 1;
                BitModel bit = (BitModel)body;

                List<WordModel> word = (List<WordModel>)body;
                int i = 0;
                foreach (var item in eq.EqpData.FUNCTIONSTATE.GetType().GetProperties())
                {
                    i++;
                    if (word.Any(x => x.Item == item.Name))
                    {
                        WordModel w = word.FirstOrDefault(x => x.Item == item.Name);
                        var a = item.GetValue(eq.EqpData.FUNCTIONSTATE, null) == null ? "" : item.GetValue(eq.EqpData.FUNCTIONSTATE, null).ToString();
                        if (w.GetValue() != a)
                        {
                            FUNCTION func = new FUNCTION();
                            func.BYWHO = word.FirstOrDefault(x => x.Item == "BYWHO").GetValue(eq.PLC);
                            func.OLDEFST = a;
                            func.EFNAME = item.Name;
                            func.NEWEFST = w.GetValue();
                            func.EFID = i.ToString();
                            eq.EqpData.FUNCTION.Add(func);
                            isSend = true;
                            item.SetValue(eq.EqpData.FUNCTIONSTATE, w.GetValue());
                        }
                    }
                }
                if (isSend)
                {
                    eq.SendS6F11_111( eq.EqpData.FUNCTION);
                    eq.EqpData.FUNCTION = new List<FUNCTION>();
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
