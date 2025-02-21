using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Message.PLC2Cim.Recv
{
    public class MATERIALSUPPLEMENTREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                

List<IWordModel> word = bit.LstWord;
                MATERIALPROCESSCHANGEDATA material = new MATERIALPROCESSCHANGEDATA();

                TRACKING_MATERIAL tracking = new TRACKING_MATERIAL();
                tracking.EQPMATERIALBATCHID = word.FirstOrDefault(x => x.Item == "MATERIALBATCHID").GetValue(eq.PLC);
                tracking.EQPMATERIALBATCHNAME = word.FirstOrDefault(x => x.Item == "MATERIALBATCHNAME").GetValue(eq.PLC);
                tracking.EQPMATERIALID = word.FirstOrDefault(x => x.Item == "MATERIALID").GetValue(eq.PLC);
                tracking.EQPMATERIALTYPE = word.FirstOrDefault(x => x.Item == "MATERIALTYPE").GetValue(eq.PLC);
                tracking.EQPMATERIALST = word.FirstOrDefault(x => x.Item == "MATERIALST").GetValue(eq.PLC);
                tracking.EQPMATERIALPORTID = word.FirstOrDefault(x => x.Item == "MATERIALPORTID").GetValue(eq.PLC);
                tracking.EQPMATERIALSTATE = word.FirstOrDefault(x => x.Item == "MATERIALSTATE").GetValue(eq.PLC);
                tracking.EQPMATERIALTOTALQTY = word.FirstOrDefault(x => x.Item == "MATERIALTOTALQTY").GetValue(eq.PLC);
                tracking.EQPMATERIALUSEQTY = word.FirstOrDefault(x => x.Item == "MATERIALUSEQTY").GetValue(eq.PLC);
                tracking.EQPMATERIALASSEMQTY = word.FirstOrDefault(x => x.Item == "MATERIALASSEMQTY").GetValue(eq.PLC);
                tracking.EQPMATERIALNGQTY = word.FirstOrDefault(x => x.Item == "MATERIALNGQTY").GetValue(eq.PLC);
                tracking.EQPMATERIALREMAINQTY = word.FirstOrDefault(x => x.Item == "MATERIALREMAINQTY").GetValue(eq.PLC);
                tracking.EQPMATERIALPRODUCTQTY = word.FirstOrDefault(x => x.Item == "MATERIALPRODUCTQTY").GetValue(eq.PLC);
                tracking.EQPMATERIALPROCUSEQTY = word.FirstOrDefault(x => x.Item == "MATERIALPROCESSUSEQTY").GetValue(eq.PLC);
                tracking.EQPMATERIALPROCASSEMQTY = word.FirstOrDefault(x => x.Item == "MATERIALPROCESSASSEMQTY").GetValue(eq.PLC);
                tracking.EQPMATERIALPROCNGQTY = word.FirstOrDefault(x => x.Item == "MATERIALPROCESSNGQTY").GetValue(eq.PLC);
                tracking.EQPMATERIALSUPPLYREQUESTQTY = word.FirstOrDefault(x => x.Item == "MATERIALSUPPLYREQUESTQTY").GetValue(eq.PLC);
                material.MATERIALs.Add(tracking);

                string ceid = word.FirstOrDefault(x => x.Item == "CEID").GetValue(eq.PLC);
                eq.SendS6F11_211_227( material, ceid);
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
