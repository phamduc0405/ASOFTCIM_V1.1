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

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS2F29()
        {
            try
            {
                int count = 0;
                List<string> ecs = new List<string>();
                string eqpID = _cim.SysPacket.GetItemString(1);
                if (eqpID != _cim.EQPID)
                {
                    SendS9F1(_cim.SysPacket);
                    return;
                }
                count = int.Parse( _cim.SysPacket.GetItemString());
                for (int i = 0; i < count; i++)
                {
                    ecs.Add( _cim.SysPacket.GetItemString());
                }
                SendS2F30(ecs);
            }
            catch (Exception ex)
            {
                SendS9F7(_cim.SysPacket);
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    }
}
