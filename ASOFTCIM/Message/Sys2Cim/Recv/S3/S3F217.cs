using A_SOFT.CMM.INIT;
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
        public void RecvS3F217()
        {
            try
            {
                string ACK = "0";
                string eqp = _cim.SysPacket.GetItemString(1);
                string packingId = _cim.SysPacket.GetItemString();
                string checkername = _cim.SysPacket.GetItemString();
                string chipmenttype = _cim.SysPacket.GetItemString();

                SendS3F218(_cim.Conn, EqpData);
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
