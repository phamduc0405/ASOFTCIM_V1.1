﻿using A_SOFT.CMM.INIT;
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
        public void SendS9F1(SysPacket mes)
        {
            try
            {
                SysPacket packet = new SysPacket(mes.Conn);
                packet.Stream = 9;
                packet.Function = 1;
                packet.Command = Command.UserData;
                packet.DeviceId = mes.DeviceId;
                packet.SystemByte = mes.SystemByte;
                packet.Send2Sys();Host2CimEventHandle($"CIM -> HOST :SEND S{packet.Stream}F{packet.Function}");
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }
}
