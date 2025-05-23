﻿using A_SOFT.CMM.INIT;
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
        public void SendS1F4(List<string> lstSv)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 1;
                packet.Function = 4;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.WaitBit = false;
				            
                List<SV> svid = new List<SV>();
                svid = EqpData.SVID;
                if (lstSv.Count == 0)
                {
                    packet.addItem(DataType.List, 2);
                    packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                    packet.addItem(DataType.List, svid.Count);
                    for (int i = 0; i < svid.Count; i++)
                    {
                        packet.addItem(DataType.List, 2);
                        packet.addItem(DataType.Ascii, svid[i].SVID);
                        packet.addItem(DataType.Ascii, svid[i].SVVALUE);
                    }
                }
                else
                {
                    if (lstSv[0] == "EQPID")
                    {
                        packet.addItem(DataType.List, 0);
                        packet.Send2Sys();
                        Host2CimEventHandle($"CIM -> HOST :SEND S{packet.Stream}F{packet.Function}");
                        return;
                    }
                    foreach (var item in lstSv)
                    {
                        if (!EqpData.SVID.Any(x => x.SVID == item))
                        {
                            packet.addItem(DataType.List, 0);
                            packet.Send2Sys();
                            Host2CimEventHandle($"CIM -> HOST :SEND S{packet.Stream}F{packet.Function}");
                            return;
                        }

                    }
                    packet.addItem(DataType.List, 2);
                    packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                    packet.addItem(DataType.List, lstSv.Count);
                    foreach (var item in lstSv)
                    {
                        packet.addItem(DataType.List, 2);
                        SV sv = svid.First(x => x.SVID == item);
                        packet.addItem(DataType.Ascii, sv.SVID);
                        packet.addItem(DataType.Ascii, sv.SVVALUE);
                    }
                }

                packet.Send2Sys();
                Host2CimEventHandle($"CIM -> HOST :SEND S{packet.Stream}F{packet.Function}");
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
        }
    }
}
