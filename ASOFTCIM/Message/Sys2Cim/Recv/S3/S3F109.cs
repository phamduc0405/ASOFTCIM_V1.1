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
        public void RecvS3F109(SysPacket sysPacket)
        {
            try
            {
                string ACK = "0";
                string eqp = sysPacket.GetItemString(1);
                CELLLOTINFODOWNLOAD cellLotInfor = new CELLLOTINFODOWNLOAD();
                cellLotInfor.CELLID = sysPacket.GetItemString(4);
                cellLotInfor.CASSETTEID = sysPacket.GetItemString();
                cellLotInfor.BATCHLOT = sysPacket.GetItemString();
                cellLotInfor.PRODUCTID = sysPacket.GetItemString();
                cellLotInfor.PRODUCT_TYPE = sysPacket.GetItemString();
                cellLotInfor.PRODUCT_KIND = sysPacket.GetItemString();
                cellLotInfor.PRODUCTSPEC = sysPacket.GetItemString();
                cellLotInfor.STEPID = sysPacket.GetItemString();
                cellLotInfor.PPID= sysPacket.GetItemString();
                cellLotInfor.CELL_SIZE = sysPacket.GetItemString();
                cellLotInfor.CELL_THICKNESS = sysPacket.GetItemString();
                cellLotInfor.COMMENT = sysPacket.GetItemString();
                int count = int.Parse(sysPacket.GetItemString());
                
                    List<ITEM> items = new List<ITEM>();
                for (int i = 0; i < count; i++)
                {
                    ITEM item = new ITEM();
                    item.ITEMNAME = sysPacket.GetItemString();
                    item.ITEMVALUE = sysPacket.GetItemString();
                    items.Add(item);
                }
                cellLotInfor.ITEMS = items;
                SendMessage2PLC("CELLLOTINFORMATIONSEND11", cellLotInfor);
                SendS3F110( ACK);
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
