using A_SOFT.CMM.INIT;
using A_SOFT.Ctl.SecGem;
using ASOFTCIM.Data;
using ASOFTCIM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS7F109(SysPacket sysPacket)
        {
            try
            {
                string eqpid = sysPacket.GetItemString(1);
                string ppid_type = sysPacket.GetItemString();
                //EqpData.CurrPPID.PPID_TYPE = sysPacket.GetItemString(2);
                string ACK = "0";
                if(eqpid != EqpData.EQINFORMATION.EQPID)
                {
                     ACK = "7";
                    SendS7F110(EqpData.CurrPPID, ACK);
                    return;
                }    
                if(ppid_type!= "1")
                {
                     ACK = "9";
                    SendS7F110(EqpData.CurrPPID, ACK);
                    return;
                }
                ReadRMS();
                EqpData.CurrPPID.PPID_TYPE = ppid_type;
                SendS7F110(EqpData.CurrPPID,ACK);
            }
            catch (Exception ex)
            {
                SendS9F7(sysPacket);
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    }
}
