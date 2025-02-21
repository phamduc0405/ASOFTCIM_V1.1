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
        public void SendS6F215( TRAYPACKING trayPacking)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 215;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, "");
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.List, 3);
                    {
                        packet.addItem(DataType.Ascii, "100");
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                            packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.CRST);
                        }
                    }

                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "101");
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

                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "303");
                        packet.addItem(DataType.List, 10);
                        {
                            packet.addItem(DataType.Ascii, trayPacking.PACKEQPID);
                            packet.addItem(DataType.Ascii, trayPacking.PRODUCTID);
                            packet.addItem(DataType.Ascii, trayPacking.PPID);
                            packet.addItem(DataType.Ascii, trayPacking.PACKLABELID);
                            packet.addItem(DataType.Ascii, trayPacking.BYWHO);
                            packet.addItem(DataType.Ascii, trayPacking.OPERATORID);
                            packet.addItem(DataType.Ascii, trayPacking.ITEM_1COUNT);
                            packet.addItem(DataType.List, trayPacking.ITEM_1.Count);
                            foreach (var item in trayPacking.ITEM_1)
                            {
                                packet.addItem(DataType.Ascii, item.ITEMVALUE);
                            }
                            packet.addItem(DataType.Ascii, trayPacking.ITEM_2COUNT);
                            packet.addItem(DataType.List, trayPacking.ITEM_2.Count);
                            foreach (var item in trayPacking.ITEM_2)
                            {
                                packet.addItem(DataType.Ascii, item.ITEMVALUE);
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
