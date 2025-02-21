using A_SOFT.CMM.INIT;
using A_SOFT.Ctl.SecGem;
using AComm.TCPIP;
using ASOFTCIM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM
{
    /// <summary>
    /// Status Variable Name List Request
    /// </summary>
    public partial class ACIM
    {
        public void SendS1F12(List<string> lstSv)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 1;
                packet.Function = 12;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.WaitBit = true;
				AddTrans(EqpData.TransactionSys);
                List<SV> svid = EqpData.SVID;
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                    if (lstSv.Count == 0)
                    {
                        packet.addItem(DataType.List, svid.Count);
                        foreach (var item in svid)
                        {
                            packet.addItem(DataType.List, 2);
                            {
                                packet.addItem(DataType.Ascii, item.SVID);
                                packet.addItem(DataType.Ascii, item.SVNAME);
                            }
                        }
                    }
                    else
                    {
                        packet.addItem(DataType.List, lstSv.Count);
                        foreach (var item in lstSv)
                        {
                            packet.addItem(DataType.List, 2);
                            SV sv = svid.First(x => x.SVID == item);
                            packet.addItem(DataType.Ascii, sv.SVID);
                            packet.addItem(DataType.Ascii, sv.SVNAME);
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
