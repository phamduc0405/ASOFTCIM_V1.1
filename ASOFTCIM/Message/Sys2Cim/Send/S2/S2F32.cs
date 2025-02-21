using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;
using AComm.TCPIP;
using A_SOFT.Ctl.SecGem;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void SendS2F32(string datetime)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 2;
                packet.Function = 32;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                string TIACK = "0";
                try
                {
                    DateTime dt = CheckString.CheckDateTime(datetime);
                }
                catch (Exception ex)
                {
                    TIACK = "1";
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }

                packet.addItem(DataType.Ascii, TIACK);
                packet.Send2Sys();
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }
    public static class CheckString
    {
        /// <summary>
        /// YYYYMMDDHHMMSS
        /// </summary>
        /// <param name="dt"></param>
        public static DateTime CheckDateTime(string dt)
        {
            
            int year = int.Parse(dt.Substring(0, 4));
            int month = int.Parse(dt.Substring(4, 2)); 
            int day = int.Parse(dt.Substring(6, 2));
            int hour = int.Parse(dt.Substring(8, 2));
            int minute = int.Parse(dt.Substring(10, 2));
            int second = int.Parse(dt.Substring(12, 2));
            DateTime datetime = new DateTime(year,month,day,hour,minute,second);
            return datetime;
        }
    }
}
