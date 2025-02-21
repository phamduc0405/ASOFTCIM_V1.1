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

namespace ASOFTCIM
{
     public partial class ACIM 
    {
        public void RecvS14F2()
        {
            try
            {
                string ACK = "0";
                ATTRREQUEST attrequest = new ATTRREQUEST();
                attrequest.EQPID = _cim.SysPacket.GetItemString(1);
                attrequest.OBJTYPE = _cim.SysPacket.GetItemString();
                attrequest.OBJID = _cim.SysPacket.GetItemString();
                attrequest.COMMENT = _cim.SysPacket.GetItemString();
                int count = int.Parse(_cim.SysPacket.GetItemString());
                for (int i = 0; i < count; i++)
                {
                    string lst = _cim.SysPacket.GetItemString();
                    ATTR attr = new ATTR();
                    attr.ATTRID = _cim.SysPacket.GetItemString();
                    attr.ATTRDATA = _cim.SysPacket.GetItemString();
                    attrequest.ATTRs.Add(attr);
                }
                attrequest.REPLYCODE = _cim.SysPacket.GetItemString();
                attrequest.REPLYCODE = _cim.SysPacket.GetItemString();

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
