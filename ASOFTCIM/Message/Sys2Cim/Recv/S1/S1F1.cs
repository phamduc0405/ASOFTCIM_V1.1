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
        public void RecvS1F1()
        {
            if (_cim.SysPacket.Items.Count > 0)
            {
                SendS9F7(_cim.SysPacket);
                return;
                var d = 1;
            }
            SendS1F2(_cim.Conn);
        }
    }
}
