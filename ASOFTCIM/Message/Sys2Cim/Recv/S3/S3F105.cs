using ASOFTCIM.Helper;

using ASOFTCIM.Data;
using A_SOFT.CMM.INIT;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS3F105(SysPacket sysPacket)
        {
            try
            {
                string ACK = "0";
                string eqp = sysPacket.GetItemString(1);
                MaterialEqp materialEq = new MaterialEqp();
                materialEq.MATERIALEQPID = sysPacket.GetItemString(5);
                materialEq.MATERIALBATCHID = sysPacket.GetItemString();
                materialEq.MATERIALCODE = sysPacket.GetItemString();
                materialEq.MATERIALUSEDATE = sysPacket.GetItemString();
                materialEq.MATERIALDISEASEDATE = sysPacket.GetItemString();
                materialEq.MATERIALMAKER = sysPacket.GetItemString();
                materialEq.MATERIALVALIDATIONFLAGE = sysPacket.GetItemString();
                materialEq.MATERIALCODE = sysPacket.GetItemString();
                materialEq.COMMENT = sysPacket.GetItemString();

                string lst = sysPacket.GetItemString();
                MaterialInfor materialInfor = new MaterialInfor();
                materialInfor.MATERIALID = sysPacket.GetItemString();
                materialInfor.MATERIALTYPE = sysPacket.GetItemString();
                materialInfor.MATERIALST = sysPacket.GetItemString();
                materialInfor.MATERIALPORTID = sysPacket.GetItemString();
                materialInfor.MATERIALSTATE = sysPacket.GetItemString();
                materialInfor.MATERIALTOTALQTY = sysPacket.GetItemString();
                materialInfor.MATERIALUSEQTY = sysPacket.GetItemString();
                materialInfor.MATERIALASSEMQTY = sysPacket.GetItemString();
                materialInfor.MATERIALNGQTY = sysPacket.GetItemString();
                materialInfor.MATERIALREMAINQTY = sysPacket.GetItemString();
                materialInfor.MATERIALPROCUSEQTY = sysPacket.GetItemString();

                string lsts = sysPacket.GetItemString();
                REPLY reply = new REPLY();
                reply.REPLYSTATUS = sysPacket.GetItemString();
                reply.REPLYCODE = sysPacket.GetItemString();
                reply.REPLYTEXT = sysPacket.GetItemString();

                MATERIALINFOMATIONSEND materialInfomationSend = new MATERIALINFOMATIONSEND();
                materialInfomationSend.MATERIALUSEINFO = materialInfor;
                materialInfomationSend.MATERIALSTANDARD = materialEq;
                materialInfomationSend.REPLY=reply;
                
                if(materialInfor.MATERIALPORTID == "1")//port1 dung cho material
                {
                    SendMessage2PLC("MATERIALINFORMATIONSEND1", materialInfomationSend);
                }
                if (materialInfor.MATERIALPORTID == "2")//port2 dung cho jig
                {
                    SendMessage2PLC("MATERIALINFORMATIONSEND2", materialInfomationSend);
                }
                SendS3F106( ACK);
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
