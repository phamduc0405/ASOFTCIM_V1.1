using A_SOFT.CMM.INIT;
using A_SOFT.Ctl.Mitsu.Model;
using A_SOFT.PLC;
using ASOFTCIM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Message.PLC2Cim.Recv
{
    public class PARAMETERCHANGE
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                PPIDINFOR ppidinfor = new PPIDINFOR();
                COMMANDCODE commandcode = new COMMANDCODE();
                PPPARAMS ppparam = new PPPARAMS();
                ppidinfor.MODE = eq.PLCH.PPIDParams[0].GetValue(eq.PLC);
                ppidinfor.PPID = eq.PLCH.PPIDParams[1].GetValue(eq.PLC);
                foreach (var ppidparam in eq.PLCH.PPIDParams.Skip(2))
                {
                    if(ppidparam.Item != "RESERVED")
                    {
                        PARAM param = new PARAM();
                        param.PARAMVALUE = ppidparam.GetValue(eq.PLC);
                        param.PARAMNAME = ppidparam.Item;
                        ppparam.PARAMS.Add(param);
                        commandcode.PARAMs.Add(param);
                        commandcode.CCODE = ppidinfor.MODE;
                    }    
                }
                ppidinfor.COMMANDCODEs.Add(commandcode);
                //sau khi thay đổi cần đọc lại RMS
                eq.ReadRMS();
                eq.SendS7F107(ppidinfor);
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
