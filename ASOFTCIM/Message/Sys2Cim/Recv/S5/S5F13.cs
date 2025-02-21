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
        public void RecvS5F13()
        {
            try
            {
                string ACK = "0";
                string aled = _cim.SysPacket.GetItemString(1);
                string eqpId = _cim.SysPacket.GetItemString();
                string unitId = _cim.SysPacket.GetItemString();
                int count = int.Parse(_cim.SysPacket.GetItemString());
                List<string> listAlid = new List<string>();
                for (int i = 0; i < count; i++)
                {
                    listAlid.Add(_cim.SysPacket.GetItemString());
                }
                if (aled != "0" || aled != "1" || eqpId != _cim.EQPID)
                {
                    ACK = "1";
                    SendS5F14( ACK);
                    return;
                }

                // Send to Eqp Change Enable or Disable Alarm

                SendS5F4( ACK);
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
