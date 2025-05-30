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
        public void RecvS5F113(SysPacket sysPacket)
        {
            try
            {
                string lst = sysPacket.GetItemString(1);
                string eqpId = sysPacket.GetItemString();
                int count = int.Parse(sysPacket.GetItemString());
                List<string> lstUnitId = new List<string>();
                for (int i = 0; i < count; i++)
                {
                    lstUnitId.Add(sysPacket.GetItemString());
                }
                SendS5F114(lstUnitId);
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
