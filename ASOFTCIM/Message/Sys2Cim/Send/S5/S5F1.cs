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
        public void SendS5F1(Alarm alarm)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 5;
                packet.Function = 1;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.WaitBit = true;
				AddTrans(EqpData.TransactionSys);
                packet.addItem(DataType.List, 5);
                packet.addItem(DataType.Ascii, alarm.EQPID);
                packet.addItem(DataType.Ascii, alarm.ALST);
                packet.addItem(DataType.Ascii, alarm.ALCD);
                packet.addItem(DataType.Ascii, alarm.ALID);
                packet.addItem(DataType.Ascii, alarm.ALTEXT);
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
