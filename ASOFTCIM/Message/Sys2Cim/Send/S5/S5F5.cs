using A_SOFT.CMM.INIT;
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
        public void SendS5F5(SysPacket mes)
        {
            try
            {
                SysPacket packet = new SysPacket(mes.Conn);
                packet.Stream = 5;
                packet.Function = 6;
                packet.Command = Command.UserData;
                packet.DeviceId = mes.DeviceId;
                packet.SystemByte = mes.SystemByte;
                packet.addItem(DataType.List, 2);
                packet.addItem(DataType.Ascii, "abc");
                packet.addItem(DataType.Ascii, "def");
                //TRS ALARM là gì ?
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
