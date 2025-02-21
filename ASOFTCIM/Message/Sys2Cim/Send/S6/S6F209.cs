using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;
using AComm.TCPIP;
using A_SOFT.Ctl.SecGem;
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
        public void SendS6F209(IConnect conn, EQPDATA eqp)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 209;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                packet.addItem(DataType.Ascii, "");
                packet.addItem(DataType.Ascii, "");

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
