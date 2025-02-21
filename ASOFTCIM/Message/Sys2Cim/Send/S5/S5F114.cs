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
        public void SendS5F114( List<string> lstUnit)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 5;
                packet.Function = 114;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.addItem(DataType.List, 2);
                packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                packet.addItem(DataType.List, lstUnit.Count());
                for (int i = 0; i < lstUnit.Count(); i++)
                {
                    packet.addItem(DataType.List, 2);
                    packet.addItem(DataType.Ascii, lstUnit[i]);

                }

                // Unit khong send Alarm  ??

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
