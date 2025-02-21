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
    public class CELLSTARTEVENTPORTREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;

                

List<IWordModel> word = bit.LstWord;
                CELLEVENTDATA cell = new CELLEVENTDATA();
                cell.CELL.CELLID = word.FirstOrDefault(x => x.Item == "CELLID").GetValue(eq.PLC);
                cell.CELL.PRODUCTID = word.FirstOrDefault(x => x.Item == "PRODUCTID").GetValue(eq.PLC);
                cell.CELL.STEPID = word.FirstOrDefault(x => x.Item == "STEPID").GetValue(eq.PLC);
                cell.WORKORDER.PROCESSJOB = word.FirstOrDefault(x => x.Item == "PROCESSJOBID").GetValue(eq.PLC);
                cell.WORKORDER.PLANQTY = word.FirstOrDefault(x => x.Item == "PLANQTY").GetValue(eq.PLC);
                cell.WORKORDER.PROCESSEDQTY = word.FirstOrDefault(x => x.Item == "PROCESSEDQTY").GetValue(eq.PLC);
                cell.READER.READERID = word.FirstOrDefault(x => x.Item == "READERID").GetValue(eq.PLC);
                cell.READER.READERRESULTCODE = word.FirstOrDefault(x => x.Item == "READERRESULTCODE").GetValue(eq.PLC);
                cell.JUDGEMENT.REASONCODE = word.FirstOrDefault(x => x.Item == "REASONCODE").GetValue(eq.PLC);

                eq.SendS6F11_401( cell, "401");
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
