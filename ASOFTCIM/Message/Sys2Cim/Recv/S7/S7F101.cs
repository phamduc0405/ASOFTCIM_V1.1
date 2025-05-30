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
        public void RecvS7F101(SysPacket sysPacket)
        {
            try
            {
                string eqpid = sysPacket.GetItemString(1);
                string ppid_type = sysPacket.GetItemString();
                PPIDList ppidlist = new PPIDList();
                if(eqpid != EqpData.EQINFORMATION.EQPID)
                {
                    SendS7F102(null);
                    return;
                }
                if(ppid_type != "1")
                {
                    SendS7F102(null);
                    return;
                }
                //this.EqpData.PPIDList.PPID_TYPE = sysPacket.GetItemString(2);
                ReadRMS();
                SendS7F102(this.EqpData.PPIDList);
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
