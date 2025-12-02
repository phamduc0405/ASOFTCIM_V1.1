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
        public void SendS7F102(PPIDList ppids)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 7;
                packet.Function = 102;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                if (ppids == null)
                {
                    packet.addItem(DataType.List, 0);
                    packet.Send2Sys(); Host2CimEventHandle($"CIM -> HOST :SEND S{packet.Stream}F{packet.Function}");
                    return;
                }
                packet.addItem(DataType.List, 3);
                {
                    packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                    packet.addItem(DataType.Ascii, ppids.PPID_TYPE);
                    int cout = 0;
                    foreach (var item in ppids.PPID)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            cout++;

                        }
                    }
                    packet.addItem(DataType.List, cout);
                    foreach (var item in ppids.PPID)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            packet.addItem(DataType.Ascii, item);

                        }
                    }
                }
                packet.Send2Sys(); Host2CimEventHandle($"CIM -> HOST :SEND S{packet.Stream}F{packet.Function}");
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    }
}
