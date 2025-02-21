using A_SOFT.CMM.INIT;
using ASOFTCIM.Helper;
using ASOFTCIM.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void SendS6F101(SysPacket mes)
        {
            try
            {
                SysPacket packet = new SysPacket(mes.Conn);
                packet.Stream = 6;
                packet.Function = 101;
                packet.Command = Command.UserData;
                packet.DeviceId = mes.DeviceId;
                packet.SystemByte = mes.SystemByte;
                packet.addItem(DataType.List, 2);
                packet.addItem(DataType.Ascii, "abc");
                packet.addItem(DataType.Ascii, "def");
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
