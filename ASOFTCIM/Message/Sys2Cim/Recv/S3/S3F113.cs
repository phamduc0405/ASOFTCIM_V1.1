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
        public void RecvS3F113(SysPacket sysPacket)
        {
            try
            {
                string ACK = "0";
                string eqp = sysPacket.GetItemString(1);
                FPOINFODOWNLOAD fpoInfor = new FPOINFODOWNLOAD();
                fpoInfor.EQPID = sysPacket.GetItemString(1);
                fpoInfor.CELLID = sysPacket.GetItemString();
                fpoInfor.PRODUCTID = sysPacket.GetItemString();
                fpoInfor.FPOID = sysPacket.GetItemString();
                fpoInfor.SFPOID = sysPacket.GetItemString();
                fpoInfor.FPO_SIZE = sysPacket.GetItemString();
                fpoInfor.FPO_QTY = sysPacket.GetItemString();
                fpoInfor.SFPO_SIZE = sysPacket.GetItemString();
                fpoInfor.SFPO_QTY = sysPacket.GetItemString();
                fpoInfor.SAMPLE_SIZE = sysPacket.GetItemString();
                fpoInfor.SAMPLE_QTY = sysPacket.GetItemString();
                fpoInfor.REPLYSTATUS = sysPacket.GetItemString();
                fpoInfor.REPLYTEXT = sysPacket.GetItemString();
                int count = int.Parse(sysPacket.GetItemString());
                for (int i = 0; i < count; i++)
                {
                    string lst = sysPacket.GetItemString();
                    ITEM item = new ITEM();
                    item.ITEMNAME= sysPacket.GetItemString();
                    item.ITEMVALUE = sysPacket.GetItemString();
                    fpoInfor.ITEMS.Add(item);
                }

                SendS3F114( ACK);
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
