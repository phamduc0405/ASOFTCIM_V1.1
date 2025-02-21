using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using ASOFTCIM.Data;
using ASOFTCIM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Message.PLC2Cim.Send
{
    public class CELLJOBPROCESS1
    {
        public CELLJOBPROCESS1(PLCHelper plcdata, CellJobProcess cell)
        {
            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "RCMD").SetValue = cell.RCMD;
                word.FirstOrDefault(x => x.Item == "JOBID").SetValue = cell.JOBID;
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = cell.CELLID;
                word.FirstOrDefault(x => x.Item == "PRODUCTID").SetValue = cell.PRODUCTID;
                word.FirstOrDefault(x => x.Item == "EQPID").SetValue = cell.EQPID;
                word.FirstOrDefault(x => x.Item == "ACTIONTYPE").SetValue = cell.ACTIONTYPE;
                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name);
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
