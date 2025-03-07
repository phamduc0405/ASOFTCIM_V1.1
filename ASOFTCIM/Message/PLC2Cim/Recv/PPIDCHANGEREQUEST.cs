using A_SOFT.CMM.INIT;
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
    public class PPIDCHANGEREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                List<WordModel> word = eq.PLCH.Words.Where(x => x.Area == bit.Comment).ToList();
                List<PPIDModel> ppidModels = new List<PPIDModel>(); 
                ppidModels = eq.PLCH.PPIDParams.ToList();
                PPIDChange ppidchange = new PPIDChange();

                ppidchange.OLD_PPID = eq.EqpData.CurrPPID.PPID;
                ppidchange.PPID = ppidModels.FirstOrDefault(x => x.Item == "PPID").GetValue(eq.PLC);
                //ppidchange.PPID_TYPE = ppidModels.FirstOrDefault(x => x.Item == "PPID_TYPE").GetValue(eq.PLC);
                ppidchange.PPID_TYPE = "1";
                //sau khi đổi ppid cần đọc lại RMS
                eq.ReadRMS();
                eq.SendS6F11_107(ppidchange);
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
