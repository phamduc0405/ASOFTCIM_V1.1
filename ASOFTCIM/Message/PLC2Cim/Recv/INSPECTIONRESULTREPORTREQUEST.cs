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
    public class INSPECTIONRESULTREPORTREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                

List<IWordModel> word = bit.LstWord;
                INSPECTION ins = new INSPECTION();
                ins.PROCESSNAME = word.FirstOrDefault(x => x.Item == "PROCESSNAME").GetValue(eq.PLC);
                ins.CELLID = word.FirstOrDefault(x => x.Item == "CELLID").GetValue(eq.PLC);
                ins.PROCESSFLAG = word.FirstOrDefault(x => x.Item == "PROCESSFLAG").GetValue(eq.PLC);
                ins.JUDGE = word.FirstOrDefault(x => x.Item == "JUDGE").GetValue(eq.PLC);
                ins.REASONCODE = word.FirstOrDefault(x => x.Item == "REASONCODE").GetValue(eq.PLC);
                ins.OPERID = word.FirstOrDefault(x => x.Item == "OPERID").GetValue(eq.PLC);
                ins.SENDUNIQUEINFO = word.FirstOrDefault(x => x.Item == "SENDUNIQUEINFO").GetValue(eq.PLC);
                ins.REVUNIQUEINFO = word.FirstOrDefault(x => x.Item == "RECVUNIQUEINFO").GetValue(eq.PLC);
                eq.SendS6F11_609( ins);
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
