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
    public class MATERIALPORTSTATE
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                bool isSend = false;
                eq.EqpData.TransactionSys += 1;
                List<WordModel> word = (List<WordModel>)body;
                int materialId = int.Parse(word[0].Area[word[0].Area.Length - 1].ToString()) - 1;
                MATERIALSTATE oldstate = eq.EqpData.MATERIALSTATES[materialId].Copy<MATERIALSTATE>();
                foreach (var item in eq.EqpData.MATERIALSTATES[materialId].GetType().GetProperties())
                {
                    if (word.Any(x => x.Item == item.Name))
                    {
                        WordModel w = word.FirstOrDefault(x => x.Item == item.Name);
                        var a = item.GetValue(eq.EqpData.UNITSTATES[materialId], null) == null ? "" : item.GetValue(eq.EqpData.UNITSTATES[materialId], null).ToString();
                        if (w.GetValue() != a)
                        {
                            isSend = true;
                            item.SetValue(eq.EqpData.UNITSTATES[materialId], w.GetValue());
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
