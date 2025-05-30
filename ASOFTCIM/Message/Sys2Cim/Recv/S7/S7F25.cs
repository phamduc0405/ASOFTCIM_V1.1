using ASOFTCIM.Helper;

using ASOFTCIM.Data;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS7F25(SysPacket sysPacket)
        {
            try
            {
                PPIDINFOR ppidInfor = new PPIDINFOR();
                ppidInfor.EQPID = sysPacket.GetItemString(1);
                ppidInfor.PPID = sysPacket.GetItemString();
                ppidInfor.PPID_TYPE = sysPacket.GetItemString();
                if (ppidInfor.EQPID != EqpData.EQINFORMATION.EQPID)
                {
                    SendS7F26(null);
                    return;
                }
                if (ppidInfor.PPID_TYPE != "1")
                {
                    SendS7F26(null);
                    return;
                }
                SendS7F26(EqpData.CurrPPID);
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
