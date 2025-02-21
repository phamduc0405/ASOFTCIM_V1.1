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
    public partial class ACIM
    {
        public void SendS1F106_2( List<string> lstUnit)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 1;
                packet.Function = 106;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.WaitBit = false;
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "2");
                    packet.addItem(DataType.List, 1);

                    packet.addItem(DataType.List, 3);
                    packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                    packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.CRST);
                    packet.addItem(DataType.List, lstUnit.Count);
                    foreach (var unitstate in EqpData.UNITSTATES)
                    {
                        if (lstUnit.Contains(unitstate.UNITID))
                        {
                            packet.addItem(DataType.List, 2);
                            {
                                packet.addItem(DataType.Ascii, unitstate.UNITID);
                                packet.addItem(DataType.List, 9);
                                {
                                    packet.addItem(DataType.Ascii, unitstate.AVAILABILITYSTATE != null ? unitstate.AVAILABILITYSTATE : "");
                                    packet.addItem(DataType.Ascii, unitstate.INTERLOCKSTATE != null ? unitstate.INTERLOCKSTATE : "");
                                    packet.addItem(DataType.Ascii, unitstate.MOVESTATE != null ? unitstate.MOVESTATE : "");
                                    packet.addItem(DataType.Ascii, unitstate.RUNSTATE != null ? unitstate.RUNSTATE : "");
                                    packet.addItem(DataType.Ascii, unitstate.FRONTSTATE != null ? unitstate.FRONTSTATE : "");
                                    packet.addItem(DataType.Ascii, unitstate.REARSTATE != null ? unitstate.REARSTATE : "");
                                    packet.addItem(DataType.Ascii, unitstate.PPSPLSTATE != null ? unitstate.PPSPLSTATE : "1");
                                    packet.addItem(DataType.Ascii, unitstate.REASONCODE != null ? unitstate.REASONCODE : "1");
                                    packet.addItem(DataType.Ascii, unitstate.DESCRIPTION != null ? unitstate.DESCRIPTION : "1");
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
    

  
        public void SendS1F106_3( List<string> lstUnit)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 1;
                packet.Function = 106;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.WaitBit = true;
				AddTrans(EqpData.TransactionSys);
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "3");
                    packet.addItem(DataType.List, 1);

                    packet.addItem(DataType.List, 3);
                    packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                    packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.CRST);
                    packet.addItem(DataType.List, lstUnit.Count);
                    foreach (var unitstate in EqpData.UNITSTATES)
                    {
                        if (lstUnit.Contains(unitstate.UNITID))
                        {
                            packet.addItem(DataType.List, 2);
                            {
                                packet.addItem(DataType.Ascii, unitstate.UNITID);
                                packet.addItem(DataType.List, unitstate.MATERIALSTATES.Count);
                                foreach (var material in unitstate.MATERIALSTATES)
                                {

                                    packet.addItem(DataType.List, 5);
                                    packet.addItem(DataType.Ascii, material.MATERIALID);
                                    packet.addItem(DataType.Ascii, material.MATERIALTYPE);
                                    packet.addItem(DataType.Ascii, material.MATERIALST);
                                    packet.addItem(DataType.Ascii, material.MATERIALPORTID);
                                    packet.addItem(DataType.Ascii, material.MATERIALUSAGE);
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
    

        public void SendS1F106_4( List<string> lstUnit)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 1;
                packet.Function = 106;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.WaitBit = true;
				AddTrans(EqpData.TransactionSys);
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "4");
                    packet.addItem(DataType.List, 1);
                    {
                        packet.addItem(DataType.List, 3);
                        packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                        packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.CRST);
                        packet.addItem(DataType.List, lstUnit.Count);
                        foreach (var unitstate in EqpData.UNITSTATES)
                        {
                            if (lstUnit.Contains(unitstate.UNITID))
                            {
                                packet.addItem(DataType.List, 2);
                                {
                                    packet.addItem(DataType.Ascii, unitstate.UNITID);
                                    packet.addItem(DataType.List, unitstate.PORTSTATES.Count);
                                    foreach (var port in unitstate.PORTSTATES)
                                    {
                                        packet.addItem(DataType.List, 7);
                                        packet.addItem(DataType.Ascii, port.PORTID);
                                        packet.addItem(DataType.Ascii, port.PORTAVAILABILITYSTATE);
                                        packet.addItem(DataType.Ascii, port.PORTACCESSMODE);
                                        packet.addItem(DataType.Ascii, port.PORTTRANSFERSTATE);
                                        packet.addItem(DataType.Ascii, port.PORTPROCESSINGSTATE);
                                        packet.addItem(DataType.Ascii, port.REASONCODE);
                                        packet.addItem(DataType.Ascii, port.DESCRIPTION);
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
    }
}
