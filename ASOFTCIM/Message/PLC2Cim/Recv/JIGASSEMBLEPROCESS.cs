using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Data;
using System.Windows.Media.Media3D;

namespace ASOFTCIM.Message.PLC2Cim.Recv
{
    public class JIGASSEMBLEPROCESS1_1
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                List<IWordModel> word = bit.LstWord;
                MATERIALPROCESSCHANGEDATA material = new MATERIALPROCESSCHANGEDATA();

                material.CELL.CELLID = word.FirstOrDefault(x => x.Item == "CELLID").GetValue(eq.PLC);
                //material.CELL.PPID = word.FirstOrDefault(x => x.Item == "PPID").GetValue(eq.PLC);
                //material.CELL.PRODUCTID = word.FirstOrDefault(x => x.Item == "PRODUCTID").GetValue(eq.PLC);
                //material.CELL.STEPID = word.FirstOrDefault(x => x.Item == "STEPID").GetValue(eq.PLC);
                TRACKING_MATERIAL tracking = new TRACKING_MATERIAL();
                tracking.EQPMATERIALID = word.FirstOrDefault(x => x.Item == "JIGID").GetValue(eq.PLC);
                tracking.EQPMATERIALTYPE = word.FirstOrDefault(x => x.Item == "JIGTYPE").GetValue(eq.PLC);
                tracking.EQPMATERIALST = word.FirstOrDefault(x => x.Item == "JIGST").GetValue(eq.PLC);
                tracking.EQPMATERIALPORTID = word.FirstOrDefault(x => x.Item == "JIGPORTID").GetValue(eq.PLC);
                tracking.EQPMATERIALSTATE = word.FirstOrDefault(x => x.Item == "JIGSTATE").GetValue(eq.PLC);
                tracking.EQPMATERIALTOTALQTY = word.FirstOrDefault(x => x.Item == "JIGTOTALQTY").GetValue(eq.PLC);
                tracking.EQPMATERIALUSEQTY = word.FirstOrDefault(x => x.Item == "JIGUSEQTY").GetValue(eq.PLC);
                tracking.EQPMATERIALREMAINQTY = word.FirstOrDefault(x => x.Item == "JIGREMAINQTY").GetValue(eq.PLC);
                material.MATERIALs.Add(tracking);
                //string ceid = word.FirstOrDefault(x => x.Item == "CEID").GetValue(eq.PLC);
                eq.SendS6F11_211_227(material, "215");
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        
        }
    }
}
