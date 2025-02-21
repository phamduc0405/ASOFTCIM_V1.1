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
        public void SendS8F2(INQUIRY inquiry, string ACK)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 8;
                packet.Function = 2;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, ACK);
                packet.addItem(DataType.Ascii, inquiry.PRODUCTID);
                packet.addItem(DataType.List, inquiry.INQUIRYDATA.Count);
                foreach (var item in inquiry.INQUIRYDATA)
                {
                    packet.addItem(DataType.List, 6);
                    {
                        packet.addItem(DataType.Ascii, item.DATA_TYPE);
                        packet.addItem(DataType.Ascii, item.ITEMNAME);
                        packet.addItem(DataType.Ascii, item.ITEMVALUE);
                        packet.addItem(DataType.Ascii, item.CHECKSUM);
                        packet.addItem(DataType.Ascii, item.REFERENCE);
                        packet.addItem(DataType.Ascii, item.EAC);
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
