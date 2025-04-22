using A_SOFT.CMM.INIT;
using A_SOFT.Ctl.SecGem;
using AComm.TCPIP;
using ASOFTCIM.Data;
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
        public void SendS3F217(PACKINGINFOR packinginfor, EQPDATA eqp)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 3;
                packet.Function = 217;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 4);
                packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                packet.addItem(DataType.Ascii, packinginfor.SBPID);
                packet.addItem(DataType.Ascii, packinginfor.CHECKERNAME);
                packet.addItem(DataType.Ascii, packinginfor.SHIPMENTTYPE);
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

