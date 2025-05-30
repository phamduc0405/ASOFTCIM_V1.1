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
        public void RecvS5F3(SysPacket sysPacket)
        {
            try
            {
                string ACK = "0";
                string aled = sysPacket.GetItemString(1);
                string eqpid = sysPacket.GetItemString();
                int count = int.Parse(sysPacket.GetItemString());
                List<string> listAlid = new List<string>();
                for (int i = 0; i < count; i++)
                {
                    listAlid.Add(sysPacket.GetItemString());
                }
                if (aled != "0"|| aled !="1" || eqpid != _cim.EQPID)
                {
                    ACK = "1";
                    SendS5F4( ACK);
                    return;
                }
                
                // Send to Eqp Change Enable or Disable Alarm

                SendS5F4(ACK);
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
