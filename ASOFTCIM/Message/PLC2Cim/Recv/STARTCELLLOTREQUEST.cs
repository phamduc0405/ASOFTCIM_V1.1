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
    public class STARTCELLLOTREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                

List<IWordModel> word = bit.LstWord;
                CELLLOTSTART cell = new CELLLOTSTART();
                cell.READERRESULTCODE = word.FirstOrDefault(x => x.Item == "READERRESULTCODE").GetValue(eq.PLC);
                cell.READERID = word.FirstOrDefault(x => x.Item == "READERID").GetValue(eq.PLC);
                CELLLOT lot = new CELLLOT();
                lot.PARENTLOT = word.FirstOrDefault(x => x.Item == "PARENTLOT").GetValue(eq.PLC);
                lot.CELLID = word.FirstOrDefault(x => x.Item == "CELLID").GetValue(eq.PLC);
                string cellid = word.FirstOrDefault(x => x.Item == "CELLID").GetValue(eq.PLC);
                cell.CELLLOTs.Add(lot);
                eq.SendS6F11_602( cell);
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }
}
