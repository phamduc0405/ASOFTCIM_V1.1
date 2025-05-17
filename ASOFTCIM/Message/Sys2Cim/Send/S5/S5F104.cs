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
        public void SendS5F104(IConnect conn, EQPDATA eqp)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 5;
                packet.Function = 104;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.addItem(DataType.List, 1);
                packet.addItem(DataType.List, 2);
                packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                if(eqp == null)
                {
                    packet.addItem(DataType.List, 0);
                    packet.Send2Sys();Host2CimEventHandle($"CIM -> HOST :SEND S{packet.Stream}F{packet.Function}");
                    return;
                }    
                packet.addItem(DataType.List, eqp.CurrAlarm.Count());
                foreach (var item in eqp.CurrAlarm)
                {
                    packet.addItem(DataType.List, 3);
                    {
                        packet.addItem(DataType.Ascii, item.ALCD);
                        packet.addItem(DataType.Ascii, item.ALID);
                        packet.addItem(DataType.Ascii, item.ALTEXT);
                    }

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
