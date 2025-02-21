using A_SOFT.CMM.INIT;
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
using ASOFTCIM.MainControl;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void SendS6F1(List<SV> svs,TRACESV tracesv)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 1;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = (uint)tracesv.SMPLN;
                packet.addItem(DataType.List, 5);
                packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                packet.addItem(DataType.Ascii, tracesv.TRID);
                packet.addItem(DataType.Ascii, tracesv.SMPLN);
                packet.addItem(DataType.Ascii, DateTime.Now.ToString("yyyyMMddHHmmss"));
                packet.addItem(DataType.List, svs.Count);
                foreach (var item in svs)
                {
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, item.SVID);
                        packet.addItem(DataType.Ascii, item.SVVALUE);
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
