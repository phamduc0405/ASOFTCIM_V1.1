using ASOFTCIM.Helper;
using ASOFTCIM.Data;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS2F29(SysPacket sysPacket)
        {
            try
            {
                int count = 0;
                List<string> ecs = new List<string>();
                string eqpID = sysPacket.GetItemString(1);
                if (eqpID != _cim.EQPID)
                {
                    ecs.Add("EQPID");
                    SendS2F14(ecs);
                    return;
                }
                count = int.Parse( sysPacket.GetItemString());
                for (int i = 0; i < count; i++)
                {
                    ecs.Add( sysPacket.GetItemString());
                }
                SendS2F30(ecs);
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
