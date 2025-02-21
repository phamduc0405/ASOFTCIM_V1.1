using A_SOFT.CMM.INIT;
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
        /// <summary>
        /// ON-LINE Acknowledge
        /// </summary>
        /// <param name="con"></param>
        /// <param name="eqp"></param>
        /// <param name="onlack">0 Accepted,1 NOTACCEPTED,2 Already connected in LOCAL MODE,3 Already connected in REMODE MODE
        /// 4 = EQPID doesn't Exist,5 = MODULEID doesn't Exist,6 = Mode Not Support,7 = Equipment to Busy
        /// </param>
        public void SendS1F18(string onlack)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 1;
                packet.Function = 18;
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
                        packet.addItem(DataType.Ascii, onlack);

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
