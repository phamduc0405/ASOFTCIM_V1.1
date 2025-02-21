using A_SOFT.CMM.INIT;
using ASOFTCIM.Helper;
using ASOFTCIM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AComm.TCPIP;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void SendS6F217( FPOCREATEREQUEST fpo)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 217;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.addItem(DataType.List, 14);
                packet.addItem(DataType.Ascii, fpo.EQPID);
                packet.addItem(DataType.Ascii, fpo.CELLID);
                packet.addItem(DataType.Ascii, fpo.PRODUCTID);
                packet.addItem(DataType.Ascii, fpo.FPOID);
                packet.addItem(DataType.Ascii, fpo.SFPOID);
                packet.addItem(DataType.Ascii, fpo.SAMPLEQTY);
                packet.addItem(DataType.Ascii, fpo.FPO_TYPE);
                packet.addItem(DataType.Ascii, fpo.SHIFTINFO);
                packet.addItem(DataType.Ascii, fpo.OPERATORID1);
                packet.addItem(DataType.Ascii, fpo.OPERATORID2);
                packet.addItem(DataType.Ascii, fpo.FPO_COMP);
                packet.addItem(DataType.Ascii, fpo.JUDGE);
                packet.addItem(DataType.Ascii, fpo.REASONCODE);
                packet.addItem(DataType.Ascii, fpo.ITEMS.Count);
                foreach (var item in fpo.ITEMS)
                {
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, item.ITEMNAME);
                        packet.addItem(DataType.Ascii, item.ITEMVALUE);
                    }
                }

                packet.Send2Sys();
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }
}
