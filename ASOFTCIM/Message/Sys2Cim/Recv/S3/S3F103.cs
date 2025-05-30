
using A_SOFT.CMM.INIT;
using A_SOFT.Ctl.SecGem;
using ASOFTCIM.Data;
using ASOFTCIM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS3F103(SysPacket sysPacket)
        {
            try
            {
                string ACK = "0";
                string eqpId = sysPacket.GetItemString(1);
                Validation validation = new Validation();
                validation.CARRIERID = sysPacket.GetItemString();
                validation.UNIQUEID = sysPacket.GetItemString(4);
                validation.UNIQUETYPE = sysPacket.GetItemString();
                validation.PRODUCTID = sysPacket.GetItemString();
                validation.PRODUCTSPEC = sysPacket.GetItemString();
                validation.PRODUCT_TYPE = sysPacket.GetItemString();
                validation.PRODUCT_KIND = sysPacket.GetItemString();
                validation.PPID = sysPacket.GetItemString();
                validation.STEPID = sysPacket.GetItemString();
                validation.CELL_SIZE = sysPacket.GetItemString();
                validation.CELL_THICKNESS = sysPacket.GetItemString();
                validation.CELLINFORESULT = sysPacket.GetItemString();
                validation.INS_COUNT = sysPacket.GetItemString();
                validation.COMMENT = sysPacket.GetItemString();
                List<Tuple<string, string>> items = new List<Tuple<string, string>>();
                int itemCount = int.Parse(sysPacket.GetItemString());
                for (int i = 0; i < itemCount; i++)
                {
                    string list = sysPacket.GetItemString();
                    string itemName = sysPacket.GetItemString();
                    string itemValue = sysPacket.GetItemString();
                    items.Add(Tuple.Create(itemName, itemValue));
                }
                validation.ITEMS = items;
                string lst = sysPacket.GetItemString();
                validation.REPLY.REPLYSTATUS = sysPacket.GetItemString();
                validation.REPLY.REPLYTEXT = sysPacket.GetItemString(); 
                //ETC
                SendMessage2PLC("SPECIFICVALIDATIONDATASEND1", validation);
                //ETC
                //unloader
                //SendMessage2PLC("SPECIFICVALIDATIONDATASEND5", validation);
                //unloader
                SendS3F104( ACK);
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
