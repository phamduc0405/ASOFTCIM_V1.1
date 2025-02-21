using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using ASOFTCIM.Data;
using ASOFTCIM.Helper;

namespace ASOFTCIM.Message.PLC2Cim.Send
{
    public class TERMINALTEXT
    {
        public TERMINALTEXT(PLCHelper plcdata, TERMINAL terminal)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "TID").SetValue = terminal.TID;
                word.FirstOrDefault(x => x.Item == "TEXT").SetValue = terminal.TEXT[0];

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
