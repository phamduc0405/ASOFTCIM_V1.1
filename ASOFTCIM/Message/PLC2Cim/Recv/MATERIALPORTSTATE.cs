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
using System.Windows.Media.Media3D;

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
                //int materialId = int.Parse(word[0].Area[word[0].Area.Length - 1].ToString()) - 1;
                int materialId = 1;
                List<string> port = new List<string>();

                var numberMaterial = word.Count(x => x.Item.Contains("MaterialPortStsType"));
                for (var i = 1; i <= numberMaterial; i++)
                {
                    MATERIALSTATE material = new MATERIALSTATE();
                    material.MATERIALTYPE = word.FirstOrDefault(x => x.Item == $"MaterialPortStsType{i}").GetValue(eq.PLC);
                    material.MATERIALST = word.FirstOrDefault(x => x.Item == $"MaterialPortStsLST{i}").GetValue(eq.PLC);
                    material.MATERIALPORTID = word.FirstOrDefault(x => x.Item == $"MaterialPortStsID{i}").GetValue(eq.PLC);
                    material.MATERIALPORTLOADNO = word.FirstOrDefault(x => x.Item == $"MaterialPortStsLoaderNo{i}").GetValue(eq.PLC);
                    material.MATERIALUSAGE = word.FirstOrDefault(x => x.Item == $"MaterialPortStsUsage{i}").GetValue(eq.PLC);
                    eq.EqpData.MATERIALSTATES[i - 1] = material;
                }
                MATERIALSTATE oldstate = eq.EqpData.MATERIALSTATES[materialId].Copy<MATERIALSTATE>();
                //foreach (var item in eq.EqpData.MATERIALSTATES[materialId].GetType().GetProperties())
                //{
                //    if (word.Any(x => x.Item == item.Name))
                //    {
                //        WordModel w = word.FirstOrDefault(x => x.Item == item.Name);
                //        var a = item.GetValue(eq.EqpData.UNITSTATES[materialId], null) == null ? "" : item.GetValue(eq.EqpData.UNITSTATES[materialId], null).ToString();
                //        if (w.GetValue() != a)
                //        {
                //            isSend = true;
                //            item.SetValue(eq.EqpData.UNITSTATES[materialId], w.GetValue());
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
        }
    }
}
