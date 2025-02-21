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
    //TODO: SDFCD 1 EQUIPMENT STATE
    /// <summary>
    /// EQUIPMENT STATE   SDFCD:1
    /// </summary>
    public partial class ACIM 
    {
        public void SendS1F6_1(IConnect con, EQPDATA eqp)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 1;
                packet.Function = 6;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.WaitBit = true;
				AddTrans(EqpData.TransactionSys);
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "1"); // SFCD :1
                    packet.addItem(DataType.List, 1);

                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                            packet.addItem(DataType.Ascii, "1");
                        }
                        packet.addItem(DataType.List, 9);
                        {
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.AVAILABILITYSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.INTERLOCKSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.MOVESTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.RUNSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.FRONTSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.REARSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.PPSPLSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.REASONCODE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.DESCRIPTION);
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
    
    //TODO: SDFCD 2 UNIT STATE
    /// <summary>
    /// UNIT STATE   SDFCD:2
    /// </summary>
 
        public void SendS1F6_2(IConnect con, EQPDATA eqp)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 1;
                packet.Function = 6;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.WaitBit = true;
				AddTrans(EqpData.TransactionSys);
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "2"); // SFCD :2
                    packet.addItem(DataType.List, 1);

                    packet.addItem(DataType.List, 3);
                    {
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.EQPINFOR.EQPID);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.EQPINFOR.CRST);
                        }
                        packet.addItem(DataType.List, 9);
                        {
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.AVAILABILITYSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.INTERLOCKSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.MOVESTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.RUNSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.FRONTSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.REARSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.PPSPLSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.REASONCODE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.DESCRIPTION);
                        }
                        packet.addItem(DataType.List, EqpData.UNITSTATES.Count);
                        {
                            foreach (var item in EqpData.UNITSTATES)
                            {
                                packet.addItem(DataType.List, 2);
                                {
                                    packet.addItem(DataType.Ascii, item.UNITID);
                                    packet.addItem(DataType.List, 9);
                                    {
                                        packet.addItem(DataType.Ascii, item.AVAILABILITYSTATE);
                                        packet.addItem(DataType.Ascii, item.INTERLOCKSTATE);
                                        packet.addItem(DataType.Ascii, item.MOVESTATE);
                                        packet.addItem(DataType.Ascii, item.RUNSTATE);
                                        packet.addItem(DataType.Ascii, item.FRONTSTATE);
                                        packet.addItem(DataType.Ascii, item.REARSTATE);
                                        packet.addItem(DataType.Ascii, item.PPSPLSTATE);
                                        packet.addItem(DataType.Ascii, item.REASONCODE);
                                        packet.addItem(DataType.Ascii, item.DESCRIPTION);
                                    }
                                }
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
    

    //TODO: SDFCD 3 MATERIAL STATE
    /// <summary>
    /// MATERIAL STATE   SDFCD:3
    /// </summary>

        public void SendS1F6_3(IConnect con, EQPDATA eqp)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 1;
                packet.Function = 6;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId; // Cần sửa lại
                packet.SystemByte = EqpData.TransactionSys;
                packet.WaitBit = true;
				AddTrans(EqpData.TransactionSys);
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "3"); // SFCD :3
                    packet.addItem(DataType.List, 1);
                    packet.addItem(DataType.List, 3);
                    {
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.EQPINFOR.EQPID);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.EQPINFOR.CRST);
                        }
                        packet.addItem(DataType.List, 9);
                        {
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.AVAILABILITYSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.INTERLOCKSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.MOVESTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.RUNSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.FRONTSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.REARSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.PPSPLSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.REASONCODE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.DESCRIPTION);
                        }
                        packet.addItem(DataType.List, eqp.MATERIALSTATES.Count);
                        {
                            foreach (var item in eqp.MATERIALSTATES)
                            {

                                packet.addItem(DataType.List, 5);
                                {
                                    packet.addItem(DataType.Ascii, item.MATERIALID);
                                    packet.addItem(DataType.Ascii, item.MATERIALTYPE);
                                    packet.addItem(DataType.Ascii, item.MATERIALST);
                                    packet.addItem(DataType.Ascii, item.MATERIALPORTID);
                                    packet.addItem(DataType.Ascii, item.MATERIALUSAGE);
                                }

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
    

    //TODO: SDFCD 4 EQUIPMENT PORT STATE
    /// <summary>
    /// EQUIPMENT PORT STATE   SDFCD:4
    /// </summary>

        public void SendS1F6_4(IConnect con, EQPDATA eqp)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 1;
                packet.Function = 6;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId; // Cần sửa lại
                packet.SystemByte = EqpData.TransactionSys;
                packet.WaitBit = true;
				AddTrans(EqpData.TransactionSys);
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "4"); // SFCD :4
                    packet.addItem(DataType.List, 1);
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.EQPINFOR.EQPID);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.EQPINFOR.CRST);
                        }
                        packet.addItem(DataType.List, eqp.PORTSTATES.Count);
                        foreach (var item in eqp.PORTSTATES)
                        {
                            packet.addItem(DataType.List, 7);
                            {
                                packet.addItem(DataType.Ascii, item.PORTNO);
                                packet.addItem(DataType.Ascii, item.PORTAVAILABILITYSTATE);
                                packet.addItem(DataType.Ascii, item.PORTACCESSMODE);
                                packet.addItem(DataType.Ascii, item.PORTTRANSFERSTATE);
                                packet.addItem(DataType.Ascii, item.PORTPROCESSINGSTATE);
                                packet.addItem(DataType.Ascii, item.REASONCODE);
                                packet.addItem(DataType.Ascii, item.DESCRIPTION);
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
    

    //TODO: SDFCD 5 FUNCTION STATE
    /// <summary>
    /// FUNCTION STATE   SDFCD:5
    /// </summary>

        public void SendS1F6_5(IConnect con, EQPDATA eqp)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 5;
                packet.Function = 1;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.WaitBit = true;
				AddTrans(EqpData.TransactionSys);
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "5"); // SFCD :5
                    packet.addItem(DataType.List, 1);
                    packet.addItem(DataType.List, 3);
                    {
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.EQPINFOR.EQPID);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.EQPINFOR.CRST);
                        }
                        packet.addItem(DataType.List, 9);
                        {
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.AVAILABILITYSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.INTERLOCKSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.MOVESTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.RUNSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.FRONTSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.REARSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.PPSPLSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.REASONCODE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.DESCRIPTION);
                        }
                        packet.addItem(DataType.List, eqp.FUNCTION.Count);
                        foreach (var item in eqp.FUNCTION)
                        {
                            packet.addItem(DataType.List, 2);
                            {
                                packet.addItem(DataType.Ascii, item.EFID);
                                packet.addItem(DataType.Ascii, item.EFST);
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
