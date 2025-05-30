using A_SOFT.Ctl.SecGem;
using ASOFTCIM;
using ASOFTCIM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS1F1(SysPacket sysPacket)
        {
            if (sysPacket.Items.Count > 0)
            {
                SendS9F7(sysPacket);
                return;
                
            }
            SendS1F2(_cim.Conn);
        }
    }
}
