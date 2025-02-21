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
        public void SendS2F16(IConnect con, EQPDATA eqp)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 2;
                packet.Function = 16;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.addItem(DataType.List, 2);
                packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                packet.addItem(DataType.List, eqp.ECS.Count());
                {
                    foreach (var item in eqp.ECS)
                    {
                        packet.addItem(DataType.List, 8);
                        packet.addItem(DataType.Ascii, CheckAck(item) ? "0" : "3");
                        string[] str = new string[2];
                        packet.addItem(DataType.List, 2);
                        {
                            str = item.ECID.Split(',');
                            packet.addItem(DataType.Ascii, str[0]);
                            packet.addItem(DataType.Ascii, str[1]);
                        }
                        packet.addItem(DataType.List, 2);
                        {
                            str = item.ECNAME.Split(',');
                            packet.addItem(DataType.Ascii, str[0]);
                            packet.addItem(DataType.Ascii, str[1]);
                        }
                        packet.addItem(DataType.List, 2);
                        {
                            str = item.ECDEF.Split(',');
                            packet.addItem(DataType.Ascii, str[0]);
                            packet.addItem(DataType.Ascii, str[1]);
                        }
                        packet.addItem(DataType.List, 2);
                        {
                            str = item.ECSLL.Split(',');
                            packet.addItem(DataType.Ascii, str[0]);
                            packet.addItem(DataType.Ascii, str[1]);
                        }
                        packet.addItem(DataType.List, 2);
                        {
                            str = item.ECSUL.Split(',');
                            packet.addItem(DataType.Ascii, str[0]);
                            packet.addItem(DataType.Ascii, str[1]);
                        }
                        packet.addItem(DataType.List, 2);
                        {
                            str = item.ECWLL.Split(',');
                            packet.addItem(DataType.Ascii, str[0]);
                            packet.addItem(DataType.Ascii, str[1]);
                        }
                        packet.addItem(DataType.List, 2);
                        {
                            str = item.ECWUL.Split(',');
                            packet.addItem(DataType.Ascii, str[0]);
                            packet.addItem(DataType.Ascii, str[1]);
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
        private bool CheckAck(EC ec)
        {
            string[] str = new string[2];
            str = ec.ECDEF.Split(','); if (str[1] == "1") return false;
            str = new string[2];
            str = ec.ECSLL.Split(','); if (str[1] == "1") return false;
            str = new string[2];
            str = ec.ECSUL.Split(','); if (str[1] == "1") return false;
            str = new string[2];
            str = ec.ECWLL.Split(','); if (str[1] == "1") return false;
            str = new string[2];
            str = ec.ECSUL.Split(','); if (str[1] == "1") return false;

            return true;
        }
        public void SendS2F16( List<EC> ecs, string TEAC)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 2;
                packet.Function = 16;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.addItem(DataType.List, 2);
                packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                packet.addItem(DataType.List, ecs.Count());
                {
                    foreach (var item in ecs)
                    {
                        packet.addItem(DataType.List, 8);
                        packet.addItem(DataType.Ascii, TEAC);
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, item.ECID);
                            packet.addItem(DataType.Ascii, "1");
                        }
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, item.ECNAME);
                            packet.addItem(DataType.Ascii, "1");
                        }
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, item.ECDEF);
                            packet.addItem(DataType.Ascii, "1");
                        }
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, item.ECSLL);
                            packet.addItem(DataType.Ascii, "1");
                        }
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, item.ECSUL);
                            packet.addItem(DataType.Ascii, "1");
                        }
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, item.ECWLL);
                            packet.addItem(DataType.Ascii, "1");
                        }
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, item.ECWUL);
                            packet.addItem(DataType.Ascii, "1");
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
