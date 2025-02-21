using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using A_SOFT.Ctl.SecGem;
using AComm.TCPIP;

namespace ASOFTCIM
{
    public partial class ACIM
    {

        /// <summary>
        /// OFF-LINE Acknowledge
        /// OFLACK : 0 ACCEPTED,1 EQPID doesn't Exist, 2 MODULEID doesn't Exist,3 Mode Not Support,4 Equipment to Busy
        /// </summary>
        /// <param name="con"></param>
        /// <param name="eqp"></param>
        /// <param name="oflack"></param>
        public void SendS1F16( string oflack)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 1;
                packet.Function = 16;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.WaitBit = true;
				AddTrans(EqpData.TransactionSys);
                packet.addItem(DataType.List, 1);
                {
                    packet.addItem(DataType.List, 3);
                    {

                        packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                        packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.CRST);
                        packet.addItem(DataType.Ascii, oflack);

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
