using ASOFTCIM.Helper;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Data;
using ASOFTCIM.Message.PLC2Cim.Send;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
     public partial class ACIM 
    {
        public void RecvS10F5(SysPacket sysPacket)
        {
            try
            {
                string ACK = "0";
                TERMINAL terminal = new TERMINAL();
                terminal.EQPID = sysPacket.GetItemString(1);
                terminal.TID = sysPacket.GetItemString();
                int count = int.Parse(sysPacket.GetItemString());
                for (int i = 0; i < count; i++)
                {
                    terminal.TEXT.Add(sysPacket.GetItemString());
                }
                if (terminal.EQPID != EQPID)
                {
                    ACK = "1";
                    SendS10F6(ACK);
                    return;
                }
                SendMessage2PLC("TERMINALTEXT", terminal);
                SendS10F6( ACK);
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
