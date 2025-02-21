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
    public class PROCESSCONTROLRESULTREPORT1
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                

List<IWordModel> word = bit.LstWord;
                PROCESSDATACONTROL pro = new PROCESSDATACONTROL();

                pro.RESULT = word.FirstOrDefault(x => x.Item == "RESULT").GetValue(eq.PLC);
                PROCESS_CELL cell = new PROCESS_CELL();
                cell.CELLID = word.FirstOrDefault(x => x.Item == "CELLID").GetValue(eq.PLC);
                cell.SEQ_NO = word.FirstOrDefault(x => x.Item == "SEQ_NO").GetValue(eq.PLC);
                PROCESS_MODULE module = new PROCESS_MODULE();
                module.MODULEID = word.FirstOrDefault(x => x.Item == "MODULEID").GetValue(eq.PLC);
                cell.MODULEs.Add(module);
                pro.CELLs.Add(cell);
                eq.SendS16F107( pro);
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }
    public class PROCESSCONTROLRESULTREPORT2
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                

List<IWordModel> word = bit.LstWord;
                PROCESSDATACONTROL pro = new PROCESSDATACONTROL();

                pro.RESULT = word.FirstOrDefault(x => x.Item == "RESULT").GetValue(eq.PLC);
                PROCESS_CELL cell = new PROCESS_CELL();
                cell.CELLID = word.FirstOrDefault(x => x.Item == "CELLID").GetValue(eq.PLC);
                cell.SEQ_NO = word.FirstOrDefault(x => x.Item == "SEQ_NO").GetValue(eq.PLC);
                PROCESS_MODULE module = new PROCESS_MODULE();
                module.MODULEID = word.FirstOrDefault(x => x.Item == "MODULEID").GetValue(eq.PLC);
                cell.MODULEs.Add(module);
                pro.CELLs.Add(cell);
                eq.SendS16F107( pro);
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
