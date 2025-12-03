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
        public void RecvS14F2(SysPacket sysPacket)
        {
            try
            {
                string ACK = "0";
                ATTRREQUEST attrequest = new ATTRREQUEST();
                attrequest.EQPID = sysPacket.GetItemString(1);
                attrequest.OBJTYPE = sysPacket.GetItemString();
                attrequest.OBJID = sysPacket.GetItemString();
                attrequest.COMMENT = sysPacket.GetItemString();
                int count = int.Parse(sysPacket.GetItemString());
                for (int i = 0; i < count; i++)
                {
                    string lst = sysPacket.GetItemString();
                    ATTR attr = new ATTR();
                    attr.ATTRID = sysPacket.GetItemString();
                    attr.ATTRDATA = sysPacket.GetItemString();
                    attrequest.ATTRs.Add(attr);
                }
                attrequest.REPLYCODE = sysPacket.GetItemString();
                attrequest.REPLYCODE = sysPacket.GetItemString();
                for (int i = 1; i <= count; i++) 
                {
                    SendMessage2PLC($"GETATTRIBUTEREQUEST{i}", attrequest,PLC);
                }
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
