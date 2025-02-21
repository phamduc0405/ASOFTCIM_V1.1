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

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS3F105()
        {
            try
            {
                string ACK = "0";
                string eqp = _cim.SysPacket.GetItemString(1);
                MaterialEqp materialEq = new MaterialEqp();
                materialEq.MATERIALEQPID = _cim.SysPacket.GetItemString(5);
                materialEq.MATERIALBATCHID = _cim.SysPacket.GetItemString();
                materialEq.MATERIALCODE = _cim.SysPacket.GetItemString();
                materialEq.MATERIALUSEDATE = _cim.SysPacket.GetItemString();
                materialEq.MATERIALDISEASEDATE = _cim.SysPacket.GetItemString();
                materialEq.MATERIALMAKER = _cim.SysPacket.GetItemString();
                materialEq.MATERIALVALIDATIONFLAGE = _cim.SysPacket.GetItemString();
                materialEq.MATERIALCODE = _cim.SysPacket.GetItemString();
                materialEq.COMMENT = _cim.SysPacket.GetItemString();

                string lst = _cim.SysPacket.GetItemString();
                MaterialInfor materialInfor = new MaterialInfor();
                materialInfor.MATERIALID = _cim.SysPacket.GetItemString();
                materialInfor.MATERIALTYPE = _cim.SysPacket.GetItemString();
                materialInfor.MATERIALST = _cim.SysPacket.GetItemString();
                materialInfor.MATERIALPORTID = _cim.SysPacket.GetItemString();
                materialInfor.MATERIALSTATE = _cim.SysPacket.GetItemString();
                materialInfor.MATERIALTOTALQTY = _cim.SysPacket.GetItemString();
                materialInfor.MATERIALUSEQTY = _cim.SysPacket.GetItemString();
                materialInfor.MATERIALASSEMQTY = _cim.SysPacket.GetItemString();
                materialInfor.MATERIALNGQTY = _cim.SysPacket.GetItemString();
                materialInfor.MATERIALREMAINQTY = _cim.SysPacket.GetItemString();
                materialInfor.MATERIALPROCUSEQTY = _cim.SysPacket.GetItemString();

                string lsts = _cim.SysPacket.GetItemString();
                REPLY reply = new REPLY();
                reply.REPLYSTATUS = _cim.SysPacket.GetItemString();
                reply.REPLYCODE = _cim.SysPacket.GetItemString();
                reply.REPLYTEXT = _cim.SysPacket.GetItemString();

                //MaterialInfomationSend materialInfomationSend = new MaterialInfomationSend();
                //materialInfomationSend.MATERIALUSEINFO = materialInfor;
                //materialInfomationSend.MATERIALSTANDARD = materialEq;
                //materialInfomationSend.REPLY=reply;

                //new MATERIALINFORMATIONSEND(EqpData, cim.EQHelper.Conn, materialInfomationSend);
                SendS3F106( ACK);
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
