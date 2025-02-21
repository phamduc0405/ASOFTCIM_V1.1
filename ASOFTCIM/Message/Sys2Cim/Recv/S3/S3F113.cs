using ASOFTCIM.Helper;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Data;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS3F113()
        {
            try
            {
                string ACK = "0";
                string eqp = _cim.SysPacket.GetItemString(1);
                FPOINFODOWNLOAD fpoInfor = new FPOINFODOWNLOAD();
                fpoInfor.EQPID = _cim.SysPacket.GetItemString(1);
                fpoInfor.CELLID = _cim.SysPacket.GetItemString();
                fpoInfor.PRODUCTID = _cim.SysPacket.GetItemString();
                fpoInfor.FPOID = _cim.SysPacket.GetItemString();
                fpoInfor.SFPOID = _cim.SysPacket.GetItemString();
                fpoInfor.FPO_SIZE = _cim.SysPacket.GetItemString();
                fpoInfor.FPO_QTY = _cim.SysPacket.GetItemString();
                fpoInfor.SFPO_SIZE = _cim.SysPacket.GetItemString();
                fpoInfor.SFPO_QTY = _cim.SysPacket.GetItemString();
                fpoInfor.SAMPLE_SIZE = _cim.SysPacket.GetItemString();
                fpoInfor.SAMPLE_QTY = _cim.SysPacket.GetItemString();
                fpoInfor.REPLYSTATUS = _cim.SysPacket.GetItemString();
                fpoInfor.REPLYTEXT = _cim.SysPacket.GetItemString();
                int count = int.Parse(_cim.SysPacket.GetItemString());
                for (int i = 0; i < count; i++)
                {
                    string lst = _cim.SysPacket.GetItemString();
                    ITEM item = new ITEM();
                    item.ITEMNAME= _cim.SysPacket.GetItemString();
                    item.ITEMVALUE = _cim.SysPacket.GetItemString();
                    fpoInfor.ITEMS.Add(item);
                }

                SendS3F114( ACK);
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
