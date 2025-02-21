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
namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void SendS7F26( PPPARAMS ppid)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 7;
                packet.Function = 26;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.addItem(DataType.List, 7);
                {
                    packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                    packet.addItem(DataType.Ascii, ppid.PPID);
                    packet.addItem(DataType.Ascii, ppid.PPID_TYPE);
                    packet.addItem(DataType.Ascii, CIMINFOR.CIMVER);
                    packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPVER);
                    packet.addItem(DataType.Ascii, DateTime.Now.ToString());
                    packet.addItem(DataType.List, 1);

                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, ppid.CCODE);
                        packet.addItem(DataType.List, ppid.PARAMS.Count);
                        foreach (var param in ppid.PARAMS)
                        {
                            packet.addItem(DataType.List, 2);
                            {
                                packet.addItem(DataType.Ascii, param.PARAMNAME);
                                packet.addItem(DataType.Ascii, param.PARAMVALUE);
                            }
                        }
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
