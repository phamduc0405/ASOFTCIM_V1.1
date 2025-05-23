﻿using A_SOFT.CMM.INIT;
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
        public void SendS14F1( ATTRREQUEST attr)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 14;
                packet.Function = 1;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.WaitBit = true;
                AddTrans(packet.SystemByte);
                packet.addItem(DataType.List, 4);
                {
                    packet.addItem(DataType.Ascii, attr.EQPID);
                    packet.addItem(DataType.Ascii, attr.OBJTYPE);
                    packet.addItem(DataType.Ascii, attr.OBJID);
                    packet.addItem(DataType.Ascii, attr.COMMENT);
                }

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
