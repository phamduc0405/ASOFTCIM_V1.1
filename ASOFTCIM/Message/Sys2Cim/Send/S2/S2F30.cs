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
        public void SendS2F30(List<string> lstEc)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 2;
                packet.Function = 30;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.addItem(DataType.List, 2);
                packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                packet.addItem(DataType.List, lstEc.Count);
                foreach (var item in lstEc)
                {
                    packet.addItem(DataType.List, 7);
                    {
                        EC ec = EqpData.ECS.First(x => x.ECID == item);
                        //foreach (var ecItem in ec.GetType().GetProperties())
                        //{
                        //    packet.addItem(DataType.Ascii, ecItem.GetValue(ec));
                        //}
                        packet.addItem(DataType.Ascii, ec.ECID);
                        packet.addItem(DataType.Ascii, ec.ECNAME);
                        packet.addItem(DataType.Ascii, ec.ECDEF);
                        packet.addItem(DataType.Ascii, ec.ECSLL);
                        packet.addItem(DataType.Ascii, ec.ECSUL);
                        packet.addItem(DataType.Ascii, ec.ECWLL);
                        packet.addItem(DataType.Ascii, ec.ECWUL);
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
