using A_SOFT.CMM.INIT;
using A_SOFT.Ctl.SecGem;
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
        public void RecvS2F18(SysPacket sysPacket)
        {
            try
            {
              //  new S2F102().SendMessage(mes);
                string DateTime = sysPacket.GetItemString();
                SendMessage2PLC("DATETIMESET",DateTime);
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
