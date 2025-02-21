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
        public void RecvS3F109()
        {
            try
            {
                string ACK = "0";
                string eqp = _cim.SysPacket.GetItemString(1);
                CELLLOTINFODOWNLOAD cellLotInfor = new CELLLOTINFODOWNLOAD();
                cellLotInfor.CELLID = _cim.SysPacket.GetItemString(4);
                cellLotInfor.CASSETTEID = _cim.SysPacket.GetItemString();
                cellLotInfor.BATCHLOT = _cim.SysPacket.GetItemString();
                cellLotInfor.PRODUCTID = _cim.SysPacket.GetItemString();
                cellLotInfor.PRODUCT_TYPE = _cim.SysPacket.GetItemString();
                cellLotInfor.PRODUCT_KIND = _cim.SysPacket.GetItemString();
                cellLotInfor.PRODUCTSPEC = _cim.SysPacket.GetItemString();
                cellLotInfor.STEPID = _cim.SysPacket.GetItemString();
                cellLotInfor.PPID= _cim.SysPacket.GetItemString();
                cellLotInfor.CELL_SIZE = _cim.SysPacket.GetItemString();
                cellLotInfor.CELL_THICKNESS = _cim.SysPacket.GetItemString();
                cellLotInfor.COMMENT = _cim.SysPacket.GetItemString();
                int count = int.Parse(_cim.SysPacket.GetItemString());
                
                    List<ITEM> items = new List<ITEM>();
                for (int i = 0; i < count; i++)
                {
                    ITEM item = new ITEM();
                    item.ITEMNAME = _cim.SysPacket.GetItemString();
                    item.ITEMVALUE = _cim.SysPacket.GetItemString();
                    items.Add(item);
                }
                cellLotInfor.ITEMS = items;

                SendS3F110( ACK);
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
