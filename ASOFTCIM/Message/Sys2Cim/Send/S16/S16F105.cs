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
        public void SendS16F105( PROCESSDATACONTROL process)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 16;
                packet.Function = 105;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.addItem(DataType.List, 4);
                {
                    packet.addItem(DataType.Ascii, process.EQPID);
                    packet.addItem(DataType.Ascii, process.MODE);
                    packet.addItem(DataType.Ascii, process.BYWHO);
                    packet.addItem(DataType.List, process.CELLs.Count);
                    foreach (var cell in process.CELLs)
                    {
                        packet.addItem(DataType.List, 3);
                        {
                            packet.addItem(DataType.Ascii, cell.CELLID);
                            packet.addItem(DataType.Ascii, cell.SEQ_NO);
                            packet.addItem(DataType.List, cell.MODULEs.Count);
                            foreach (var module in cell.MODULEs)
                            {
                                packet.addItem(DataType.List, 5);
                                {
                                    packet.addItem(DataType.Ascii, module.MODULEID);
                                    packet.addItem(DataType.Ascii, module.PPID);
                                    packet.addItem(DataType.Ascii, module.PPID_TYPE);
                                    packet.addItem(DataType.List, module.PARAMs.Count);
                                    foreach (var param in module.PARAMs)
                                    {
                                        packet.addItem(DataType.List, 2);
                                        {
                                            packet.addItem(DataType.Ascii, param.PARAMNAME);
                                            packet.addItem(DataType.Ascii, param.PARAMVALUE);
                                        }
                                    }
                                    packet.addItem(DataType.List, module.ITEMs.Count);
                                    foreach (var item in module.ITEMs)
                                    {
                                        packet.addItem(DataType.List, 2);
                                        {
                                            packet.addItem(DataType.Ascii, item.ITEMNAME);
                                            packet.addItem(DataType.Ascii, item.ITEMVALUE);
                                        }
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
