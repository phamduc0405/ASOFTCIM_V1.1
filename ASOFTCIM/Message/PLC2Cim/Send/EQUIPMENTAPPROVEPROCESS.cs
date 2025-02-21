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
    public class EQUIPMENTAPPROVEPROCESS
    {
        public EQUIPMENTAPPROVEPROCESS(PLCHelper plcdata, ApproveProcess app)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "RCMD").SetValue = app.RCMD;
                word.FirstOrDefault(x => x.Item == "APPROVECODE").SetValue = app.APPROVECODE;
                word.FirstOrDefault(x => x.Item == "APPROVEINFO").SetValue = app.APPROVEINFO;
                word.FirstOrDefault(x => x.Item == "APPROVEID").SetValue = app.APPROVEID;
                word.FirstOrDefault(x => x.Item == "BYWHO").SetValue = app.BYWHO;
                word.FirstOrDefault(x => x.Item == "APPROVETEXT").SetValue = app.APPROVETEXT;
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
