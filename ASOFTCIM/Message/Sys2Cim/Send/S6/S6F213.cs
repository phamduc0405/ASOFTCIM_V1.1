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
        public void SendS6F213( SLIPLOTINFORMATIONREQUEST slipLot)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 213;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.addItem(DataType.List, 6);
                packet.addItem(DataType.Ascii, slipLot.EQPID);
                packet.addItem(DataType.Ascii, slipLot.SLIPID);
                packet.addItem(DataType.Ascii, slipLot.SLIPTYPE);
                packet.addItem(DataType.Ascii, slipLot.PORTNO);
                packet.addItem(DataType.Ascii, slipLot.BYWHO);
                packet.addItem(DataType.Ascii, slipLot.OPERATORID);

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
