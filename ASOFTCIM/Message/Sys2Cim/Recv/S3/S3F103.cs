﻿
using A_SOFT.CMM.INIT;
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
        public void RecvS3F103()
        {
            try
            {
                string ACK = "0";
                string eqpId = _cim.SysPacket.GetItemString(1);
                Validation validation = new Validation();
                validation.CARRIERID = _cim.SysPacket.GetItemString();
                validation.UNIQUEID = _cim.SysPacket.GetItemString(4);
                validation.UNIQUETYPE = _cim.SysPacket.GetItemString();
                validation.PRODUCTID = _cim.SysPacket.GetItemString();
                validation.PRODUCTSPEC = _cim.SysPacket.GetItemString();
                validation.PRODUCT_TYPE = _cim.SysPacket.GetItemString();
                validation.PRODUCT_KIND = _cim.SysPacket.GetItemString();
                validation.PPID = _cim.SysPacket.GetItemString();
                validation.STEPID = _cim.SysPacket.GetItemString();
                validation.CELL_SIZE = _cim.SysPacket.GetItemString();
                validation.CELL_THICKNESS = _cim.SysPacket.GetItemString();
                validation.CELLINFORESULT = _cim.SysPacket.GetItemString();
                validation.INS_COUNT = _cim.SysPacket.GetItemString();
                validation.COMMENT = _cim.SysPacket.GetItemString();
                List<Tuple<string, string>> items = new List<Tuple<string, string>>();
                int itemCount = int.Parse(_cim.SysPacket.GetItemString());
                for (int i = 0; i < itemCount; i++)
                {
                    string list = _cim.SysPacket.GetItemString();
                    string itemName = _cim.SysPacket.GetItemString();
                    string itemValue = _cim.SysPacket.GetItemString();
                    items.Add(Tuple.Create(itemName, itemValue));
                }
                validation.ITEMS = items;
                string lst = _cim.SysPacket.GetItemString();
                validation.REPLY.REPLYSTATUS = _cim.SysPacket.GetItemString();
                validation.REPLY.REPLYTEXT = _cim.SysPacket.GetItemString(); 
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
                SendS9F7(_cim.SysPacket);
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
        }
    }
}
