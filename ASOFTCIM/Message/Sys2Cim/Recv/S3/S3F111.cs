using ASOFTCIM.Helper;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Data;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS3F111(SysPacket sysPacket)
        {
            try
            {
                string ACK = "0";
                string eqp = sysPacket.GetItemString(1);
                string optionCode = sysPacket.GetItemString();
                string lst = sysPacket.GetItemString();
                LABELINFODOWNLOAD labelInfor = new LABELINFODOWNLOAD();

                labelInfor.CELLID = sysPacket.GetItemString();
                labelInfor.PRODUCTID = sysPacket.GetItemString();
                labelInfor.LABELID = sysPacket.GetItemString();
                labelInfor.REPLYSTATUS = sysPacket.GetItemString();
                labelInfor.REPLYCODE = sysPacket.GetItemString();
                labelInfor.REPLYTEXT = sysPacket.GetItemString();
                SendMessage2PLC("LABELINFORMATIONSEND", labelInfor);
                SendS3F112( ACK);
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
