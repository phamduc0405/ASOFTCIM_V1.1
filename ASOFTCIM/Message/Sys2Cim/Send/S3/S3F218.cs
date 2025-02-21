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
        public void SendS3F218(IConnect conn, EQPDATA eqp)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 3;
                packet.Function = 216;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.addItem(DataType.List, 4);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                packet.addItem(DataType.Ascii, "ERROR MESSAGE");
                packet.addItem(DataType.List, 8);
                {
                    foreach (var item in eqp.PACKINGINFOR.GetType().GetProperties())
                    {
                        packet.addItem(DataType.Ascii, item.GetValue(eqp.ETCINFOR));
                    }
                }
                packet.addItem(DataType.List, 5);
                {
                    foreach (var item in eqp.SPBINFOR.GetType().GetProperties())
                    {
                        packet.addItem(DataType.Ascii, item.GetValue(eqp.ETCINFOR));
                    }
                }
                packet.addItem(DataType.List, 4);
                {
                    foreach (var item in eqp.CARTONINFOR.GetType().GetProperties())
                    {
                        packet.addItem(DataType.Ascii, item.GetValue(eqp.ETCINFOR));
                    }
                }
                packet.addItem(DataType.List, 4);
                {
                    foreach (var item in eqp.ETCINFOR.GetType().GetProperties())
                    {
                        packet.addItem(DataType.Ascii, item.GetValue(eqp.ETCINFOR));
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
