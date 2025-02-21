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
    public class GETATTRIBUTEREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                

List<IWordModel> word = bit.LstWord;
                ATTRREQUEST att = new ATTRREQUEST();

                att.OBJTYPE = word.FirstOrDefault(x => x.Item == "OBJTYPE").GetValue(eq.PLC);
                att.OBJID = word.FirstOrDefault(x => x.Item == "OBJID").GetValue(eq.PLC);
                att.COMMENT = word.FirstOrDefault(x => x.Item == "COMMENT").GetValue(eq.PLC);
                eq.SendS14F1( att);
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
