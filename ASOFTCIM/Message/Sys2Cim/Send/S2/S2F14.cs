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
        public void SendS2F14( List<string> lstEC)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 2;
                packet.Function = 14;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                if (lstEC.Count > 0)
                {
                    foreach (var item in lstEC)
                    {
                        if (!EqpData.ECS.Any(x => x.ECID == item))
                        {
                            packet.addItem(DataType.List, 0);
                            packet.Send2Sys();
                            return;
                        }

                    }
                    packet.addItem(DataType.List, lstEC.Count);

                    foreach (var item in lstEC)
                    {
                        packet.addItem(DataType.Ascii, EqpData.ECS.First(x => x.ECID == item).ECDEF);
                    }
                }
                else
                {
                    packet.addItem(DataType.List, EqpData.ECS.Count);
                    foreach (var item in EqpData.ECS)
                    {
                        packet.addItem(DataType.Ascii, item.ECDEF);
                    }
                }
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
