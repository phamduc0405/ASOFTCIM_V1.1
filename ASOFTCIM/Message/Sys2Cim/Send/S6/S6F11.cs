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
        public void SendS6F11(IConnect con, EQPDATA eqp)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 2);
                packet.addItem(DataType.Ascii, "abc");
                packet.addItem(DataType.Ascii, "def");
                packet.Send2Sys();
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    
    /// <summary>
    /// T:101 Equipment Status Change
    /// </summary>
  
        public void SendS6F11_101( EQPSTATE oldState)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId; 
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");          //DATA ID ?
                packet.addItem(DataType.Ascii, "101");
                packet.addItem(DataType.List, 4);
                {
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "100");      //* RPTID
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                            packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.CRST);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "101");      //* RPTID
                        packet.addItem(DataType.List, 9);           // NEW STATE
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
                        packet.addItem(DataType.Ascii, "103");      //* RPTID
                        packet.addItem(DataType.List, 9);           // OLD STATE
                        {
                            packet.addItem(DataType.Ascii, oldState.AVAILABILITYSTATE);
                            packet.addItem(DataType.Ascii, oldState.INTERLOCKSTATE);
                            packet.addItem(DataType.Ascii, oldState.MOVESTATE);
                            packet.addItem(DataType.Ascii, oldState.RUNSTATE);
                            packet.addItem(DataType.Ascii, oldState.FRONTSTATE);
                            packet.addItem(DataType.Ascii, oldState.REARSTATE);
                            packet.addItem(DataType.Ascii, oldState.PPSPLSTATE);
                            packet.addItem(DataType.Ascii, oldState.REASONCODE);
                            packet.addItem(DataType.Ascii, oldState.DESCRIPTION);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "104");      //* RPTID
                        packet.addItem(DataType.List, EqpData.CurrAlarm.Count);   //* Alarm List
                        foreach (var item in EqpData.CurrAlarm)
                        {
                            packet.addItem(DataType.List, 4);
                            {
                                packet.addItem(DataType.Ascii, item.ALST);
                                packet.addItem(DataType.Ascii, item.ALCD);
                                packet.addItem(DataType.Ascii, item.ALID);
                                packet.addItem(DataType.Ascii, item.ALTEXT);
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
    
    /// <summary>
    /// T:102 Unit Status Change
    /// </summary>
    
        public void SendS6F11_102( EQPSTATE oldState, EQPSTATE newUnitState)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");          //DATA ID ?
                packet.addItem(DataType.Ascii, "102");
                packet.addItem(DataType.List, 4);
                {
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "102");      //* RPTID
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                            packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.CRST);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "111");      //* RPTID
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, oldState.UNITID);
                            packet.addItem(DataType.List, 9);           // NEW STATE
                            {
                                //EQPSTATE newUnitState = EqpData.UNITSTATES.First(x => x.UNITID == oldState.UNITID);
                                packet.addItem(DataType.Ascii, newUnitState.AVAILABILITYSTATE);
                                packet.addItem(DataType.Ascii, newUnitState.INTERLOCKSTATE);
                                packet.addItem(DataType.Ascii, newUnitState.MOVESTATE);
                                packet.addItem(DataType.Ascii, newUnitState.RUNSTATE);
                                packet.addItem(DataType.Ascii, newUnitState.FRONTSTATE);
                                packet.addItem(DataType.Ascii, newUnitState.REARSTATE);
                                packet.addItem(DataType.Ascii, newUnitState.PPSPLSTATE);
                                packet.addItem(DataType.Ascii, newUnitState.REASONCODE);
                                packet.addItem(DataType.Ascii, newUnitState.DESCRIPTION);
                            }
                        }

                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "103");      //* RPTID
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, oldState.UNITID);
                            packet.addItem(DataType.List, 9);           // OLD STATE
                            {
                                packet.addItem(DataType.Ascii, oldState.AVAILABILITYSTATE);
                                packet.addItem(DataType.Ascii, oldState.INTERLOCKSTATE);
                                packet.addItem(DataType.Ascii, oldState.MOVESTATE);
                                packet.addItem(DataType.Ascii, oldState.RUNSTATE);
                                packet.addItem(DataType.Ascii, oldState.FRONTSTATE);
                                packet.addItem(DataType.Ascii, oldState.REARSTATE);
                                packet.addItem(DataType.Ascii, oldState.PPSPLSTATE);
                                packet.addItem(DataType.Ascii, oldState.REASONCODE);
                                packet.addItem(DataType.Ascii, oldState.DESCRIPTION);
                            }
                        }

                    }
                    packet.addItem(DataType.List, 2);
                    {
                        var tempList = EqpData.CurrAlarm.ToList();
                        packet.addItem(DataType.Ascii, "104");      //* RPTID
                        packet.addItem(DataType.List, tempList.Count);   //* Alarm List
                        foreach (var item in tempList)
                        {
                            if (item == null) continue;
                            
                            packet.addItem(DataType.List, 4);
                            {
                                packet.addItem(DataType.Ascii, item.ALST);
                                packet.addItem(DataType.Ascii, item.ALCD);
                                packet.addItem(DataType.Ascii, item.ALID);
                                packet.addItem(DataType.Ascii, item.ALTEXT);
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
    
    /// <summary>
    /// T:104~106 Control State Change
    /// 104: Control State Change (OFF_LINE)
    /// 105: Control State Change (ON_LINE LOCAL)
    /// 106: Control State Change (ON_LINE REMOTE)
    /// </summary>
  
        public void SendS6F11_104_106( string Ceid)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");          //DATA ID ?
                packet.addItem(DataType.Ascii, Ceid);
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "100");      //* RPTID
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                            packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.CRST);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "101");      //* RPTID
                        packet.addItem(DataType.List, 9);           // NEW STATE
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
    

    /// <summary>
    /// T:107 PPID CHANGE
    /// </summary>

        public void SendS6F11_107( PPIDChange ppidChange)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, "107");

                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.List, 2);
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
                        packet.addItem(DataType.Ascii, "302");
                        packet.addItem(DataType.List, 3);
                        {
                            packet.addItem(DataType.Ascii, ppidChange.PPID);
                            packet.addItem(DataType.Ascii, ppidChange.PPID_TYPE);
                            packet.addItem(DataType.Ascii, ppidChange.OLD_PPID);
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
    

    /// <summary>
    /// T:108 Equipment PPID Parameter Change
    /// </summary>

        public void SendS6F11_108( PPIDChangeParameter ppidChange)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, "108");
                packet.addItem(DataType.List, 4);
                {
                    packet.addItem(DataType.List, 2);
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
                        packet.addItem(DataType.List, 7);
                        {
                            packet.addItem(DataType.Ascii, "");         //Khong Su dung MainState
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.AVAILABILITYSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.INTERLOCKSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.MOVESTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.RUNSTATE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.REASONCODE);
                            packet.addItem(DataType.Ascii, EqpData.EQPSTATE.DESCRIPTION);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "312");
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, ppidChange.PPST.PPID);
                            packet.addItem(DataType.Ascii, ppidChange.PPST.PPIDST);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "303");
                        packet.addItem(DataType.List, ppidChange.PARAMs.Length);
                        {
                            foreach (var item in ppidChange.PARAMs)
                            {
                                packet.addItem(DataType.List, 2);
                                {
                                    packet.addItem(DataType.Ascii, item.PARAMNAME);
                                    packet.addItem(DataType.Ascii, item.PARAMVALUE);
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
    

    /// <summary>
    /// Equipment Configuration File Change ?? 
    /// 
    /// </summary>
    /// TODO: 109  File Change 

        public void SendS6F11_109( ConfigFileChange configFile)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, "109");
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.List, 2);
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
                        packet.addItem(DataType.Ascii, "311");
                        packet.addItem(DataType.List, 4);
                        {
                            packet.addItem(DataType.Ascii, configFile.PRODUCTID);
                            packet.addItem(DataType.Ascii, configFile.ACTIONTYPE);
                            packet.addItem(DataType.Ascii, configFile.ACTIONRESULT);
                            packet.addItem(DataType.List, configFile.FILES.Count);
                            foreach (var item in configFile.FILES)
                            {
                                packet.addItem(DataType.List, 5);
                                {
                                    packet.addItem(DataType.Ascii, item.FILETYPE);
                                    packet.addItem(DataType.Ascii, item.FILENAME);
                                    packet.addItem(DataType.Ascii, item.FILEPATH);
                                    packet.addItem(DataType.Ascii, item.LOCALCHECKSUM);
                                    packet.addItem(DataType.Ascii, item.CURRENTCHECKSUM);
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
    
    /// <summary>
    /// T:111 Equipment Function Change
    /// </summary>

        public void SendS6F11_111( List<FUNCTION> funcChange)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, "111");
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.List, 2);
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
                        packet.addItem(DataType.Ascii, "314");
                        packet.addItem(DataType.List, funcChange.Count);
                        foreach (var item in funcChange)
                        {
                            packet.addItem(DataType.List, 4);
                            {
                                packet.addItem(DataType.Ascii, item.BYWHO);
                                packet.addItem(DataType.Ascii, item.EFID);
                                packet.addItem(DataType.Ascii, item.NEWEFST);
                                packet.addItem(DataType.Ascii, item.OLDEFST);
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
    
    /// <summary>
    /// PPIDI CHANGE IN UNIT ????
    /// </summary>
    /// TODO: 117 PPID CHANGE UNIT Unfinished !

        public void SendS6F11_117( PPIDChange ppidChange)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, "117");
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.List, 2);
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
                        packet.addItem(DataType.Ascii, "313");
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, "");         // UNIT ID ???
                            packet.addItem(DataType.List, 3);
                            {
                                packet.addItem(DataType.Ascii, ppidChange.PPID);
                                packet.addItem(DataType.Ascii, ppidChange.PPID_TYPE);
                                packet.addItem(DataType.Ascii, ppidChange.OLD_PPID);
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
    
    /// <summary>
    /// T:200~201 MATERIAL CHANGE
    /// </summary>

        public void SendS6F11_200_201( List<MATERIALSTATE> materialChange, string ceid)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, ceid);
                packet.addItem(DataType.List, 3);
                {
                    packet.addItem(DataType.List, 2);
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

                        packet.addItem(DataType.Ascii, "101");      //* RPTID
                        packet.addItem(DataType.List, 9);           // NEW STATE
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
                        packet.addItem(DataType.Ascii, "200");
                        packet.addItem(DataType.List, materialChange.Count);
                        foreach (var item in materialChange)
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
                packet.Send2Sys();
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    
    /// <summary>
    /// T:211~227 Material Process Change
    /// </summary>

        public void SendS6F11_211_227( MATERIALPROCESSCHANGEDATA materialChange, string ceid)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, ceid);
                packet.addItem(DataType.List, 4);
                {
                    packet.addItem(DataType.List, 2);
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
                        packet.addItem(DataType.List, 9);           // NEW STATE
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
                        packet.addItem(DataType.Ascii, "300");
                        packet.addItem(DataType.List, 4);
                        {
                            packet.addItem(DataType.Ascii, materialChange.CELL.CELLID);
                            packet.addItem(DataType.Ascii, materialChange.CELL.PPID);
                            packet.addItem(DataType.Ascii, materialChange.CELL.PRODUCTID);
                            packet.addItem(DataType.Ascii, materialChange.CELL.STEPID);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "201");

                        packet.addItem(DataType.List, materialChange.MATERIALs.Count);
                        foreach (var item in materialChange.MATERIALs)
                        {
                            packet.addItem(DataType.List, 17);
                            {
                                packet.addItem(DataType.Ascii, item.EQPMATERIALBATCHID);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALBATCHNAME);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALID);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALTYPE);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALST);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALPORTID);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALSTATE);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALTOTALQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALUSEQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALASSEMQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALNGQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALREMAINQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALPRODUCTQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALPROCUSEQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALPROCASSEMQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALPROCNGQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALSUPPLYREQUESTQTY);

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
    
    /// <summary>
    /// T:250~253 Carrier Status Change
    /// </summary>

        public void SendS6F11_250_253( CARRIERCHANGE carrierChange, string ceid)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, ceid);
                packet.addItem(DataType.List, 4);
                {
                    packet.addItem(DataType.List, 2);
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
                        packet.addItem(DataType.Ascii, "305");
                        packet.addItem(DataType.List, 5);
                        {
                            packet.addItem(DataType.Ascii, carrierChange.PORTSTATE.PORTNO);
                            packet.addItem(DataType.Ascii, carrierChange.PORTSTATE.PORTAVAILABILITYSTATE);
                            packet.addItem(DataType.Ascii, carrierChange.PORTSTATE.PORTACCESSMODE);
                            packet.addItem(DataType.Ascii, carrierChange.PORTSTATE.PORTTRANSFERSTATE);
                            packet.addItem(DataType.Ascii, carrierChange.PORTSTATE.PORTPROCESSINGSTATE);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "250");
                        packet.addItem(DataType.List, 4);
                        {
                            packet.addItem(DataType.Ascii, carrierChange.PARENTLOT);
                            packet.addItem(DataType.Ascii, carrierChange.RFID);
                            packet.addItem(DataType.Ascii, carrierChange.PORTNO_1);
                            packet.addItem(DataType.Ascii, carrierChange.PPID);
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
    
    /// <summary>
    /// T: 254~255 Port State Change
    /// </summary>

        public void SendS6F11_254_255( PORTSTATECHANGE portChange, string ceid)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, ceid);
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.List, 2);
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
                        packet.addItem(DataType.Ascii, "305");
                        packet.addItem(DataType.List, 5);
                        {
                            packet.addItem(DataType.Ascii, portChange.PORTSTATE.PORTNO);
                            packet.addItem(DataType.Ascii, portChange.PORTSTATE.PORTAVAILABILITYSTATE);
                            packet.addItem(DataType.Ascii, portChange.PORTSTATE.PORTACCESSMODE);
                            packet.addItem(DataType.Ascii, portChange.PORTSTATE.PORTTRANSFERSTATE);
                            packet.addItem(DataType.Ascii, portChange.PORTSTATE.PORTPROCESSINGSTATE);
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
    
    /// <summary>
    /// T: 256~262 Carrier Process Change
    /// CEID : 256, 257, 258, 259, 260, 261 , 262
    /// </summary>

        public void SendS6F11_256_262( CARRIERPROCESSCHANGE carrierChange, string ceid)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, ceid);
                packet.addItem(DataType.List, 3);
                {
                    packet.addItem(DataType.List, 2);
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
                        packet.addItem(DataType.Ascii, "309");
                        packet.addItem(DataType.List, 8);
                        {
                            packet.addItem(DataType.Ascii, carrierChange.CARRIERID);
                            packet.addItem(DataType.Ascii, carrierChange.CARRIERTYPE);
                            packet.addItem(DataType.Ascii, carrierChange.CARRIERPPID);
                            packet.addItem(DataType.Ascii, carrierChange.CARRIERPRODUCT);
                            packet.addItem(DataType.Ascii, carrierChange.CARRIERSTEPID);
                            packet.addItem(DataType.Ascii, carrierChange.CARRIER_S_COUNT);
                            packet.addItem(DataType.Ascii, carrierChange.CARRIER_C_COUNT);
                            packet.addItem(DataType.Ascii, carrierChange.PORTNO);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "310");
                        packet.addItem(DataType.List, carrierChange.SUBCARRIERS.Count);
                        foreach (var item in carrierChange.SUBCARRIERS)
                        {
                            packet.addItem(DataType.List, 3);
                            {
                                packet.addItem(DataType.Ascii, item.SUBCARRIERID);
                                packet.addItem(DataType.Ascii, item.CELLQTY);
                                packet.addItem(DataType.List, item.CELLSINFOR.Count);
                                foreach (var cell in item.CELLSINFOR)
                                {
                                    packet.addItem(DataType.List, 4);
                                    {
                                        packet.addItem(DataType.Ascii, cell.CELLID);
                                        packet.addItem(DataType.Ascii, cell.LOCATIONNO);
                                        packet.addItem(DataType.Ascii, cell.JUDGE);
                                        packet.addItem(DataType.Ascii, cell.REASONCODE);
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
    

    /// <summary>
    /// T:301~306 Process Job
    /// CEID : 301, 302, 303, 304, 305, 306
    /// </summary>

        public void SendS6F11_301_306( CARRIERCHANGE carrierChange, string ceid)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, ceid);
                packet.addItem(DataType.List, 5);
                {
                    packet.addItem(DataType.List, 2);
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
                        packet.addItem(DataType.Ascii, "200");
                        packet.addItem(DataType.List, EqpData.MATERIALSTATES.Count);
                        foreach (var item in EqpData.MATERIALSTATES)
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
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "305");
                        packet.addItem(DataType.List, 5);
                        {
                            packet.addItem(DataType.Ascii, carrierChange.PORTSTATE.PORTNO);
                            packet.addItem(DataType.Ascii, carrierChange.PORTSTATE.PORTAVAILABILITYSTATE);
                            packet.addItem(DataType.Ascii, carrierChange.PORTSTATE.PORTACCESSMODE);
                            packet.addItem(DataType.Ascii, carrierChange.PORTSTATE.PORTTRANSFERSTATE);
                            packet.addItem(DataType.Ascii, carrierChange.PORTSTATE.PORTPROCESSINGSTATE);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "306");
                        packet.addItem(DataType.List, 5);
                        {
                            packet.addItem(DataType.Ascii, carrierChange.PARENTLOT);
                            packet.addItem(DataType.Ascii, carrierChange.RFID);
                            packet.addItem(DataType.Ascii, carrierChange.PORTNO_1);
                            packet.addItem(DataType.Ascii, "");
                            packet.addItem(DataType.Ascii, "");
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
    

    /// <summary>
    /// T:311~320 TRS Process Job
    /// CEID : 311 , 312, 313, 314, 315, 316, 317, 318, 319, 320
    /// </summary>
    /// TODO: TRS PROCESS JOB Unfinish!

        public void SendS6F11_311_320( CARRIERCHANGE carrierChange, string ceid)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, ceid);
                packet.addItem(DataType.List, 5);
                {
                    packet.addItem(DataType.List, 2);
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

                }
                packet.Send2Sys();
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    

    /// <summary>
    /// T:350~356 Cassette Status Change
    /// CEID : 350, 351, 352, 353, 354, 355, 356
    /// </summary>

        public void SendS6F11_350_356( CASSETTESTATECHANGE cassetteChange, string ceid)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, ceid);
                packet.addItem(DataType.List, 3);
                {

                    packet.addItem(DataType.List, 2);
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
                        packet.addItem(DataType.Ascii, "350");
                        packet.addItem(DataType.List, 5);
                        {
                            packet.addItem(DataType.Ascii, cassetteChange.PORTID);
                            packet.addItem(DataType.Ascii, cassetteChange.PORTAVAILABLESTATE);
                            packet.addItem(DataType.Ascii, cassetteChange.PORTACCESSMODE);
                            packet.addItem(DataType.Ascii, cassetteChange.PORTTRANSFERSTATE);
                            packet.addItem(DataType.Ascii, cassetteChange.PORTPROCESSINGSTATE);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "251");
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, cassetteChange.JOBID);
                            packet.addItem(DataType.Ascii, cassetteChange.JOBTYPE);
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
    
    /// <summary>
    /// T:390~396 Cassette Status Change
    /// CEID : 390, 391, 392, 393, 394, 395, 396
    /// </summary>

        public void SendS6F11_390_396( CASSETTESTATECHANGE cassetteChange, string ceid)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, ceid);
                packet.addItem(DataType.List, 5);
                {

                    packet.addItem(DataType.List, 2);
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
                        packet.addItem(DataType.Ascii, "307");
                        packet.addItem(DataType.List, 5);
                        {
                            packet.addItem(DataType.Ascii, cassetteChange.PORTID);
                            packet.addItem(DataType.Ascii, cassetteChange.PORTAVAILABLESTATE);
                            packet.addItem(DataType.Ascii, cassetteChange.PORTACCESSMODE);
                            packet.addItem(DataType.Ascii, cassetteChange.PORTTRANSFERSTATE);
                            packet.addItem(DataType.Ascii, cassetteChange.PORTPROCESSINGSTATE);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "251");
                        packet.addItem(DataType.List, 4);
                        {
                            packet.addItem(DataType.Ascii, "");     //$TRSID> TRS의 ID
                            packet.addItem(DataType.Ascii, "");     //$OBJECTTYPE> * 반송 대상 (PANEL, WINDOW, ASSEMBLED 등)
                            packet.addItem(DataType.Ascii, "");     //$PRODUCTID> * 제품의 모델 정보
                            packet.addItem(DataType.Ascii, "");     //$TRAYTYPE> * Tray 종류
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
    
    /// <summary>
    /// T:401 Cell Process Start
    /// CEID : 401
    /// </summary>

        public void SendS6F11_401( CELLEVENTDATA cellEvent, string ceid)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, ceid);
                packet.addItem(DataType.List, 5);
                {
                    packet.addItem(DataType.List, 2);
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
                        packet.addItem(DataType.Ascii, "300");
                        packet.addItem(DataType.List, 4);
                        {
                            packet.addItem(DataType.Ascii, cellEvent.CELL.CELLID);
                            packet.addItem(DataType.Ascii, cellEvent.CELL.PPID);
                            packet.addItem(DataType.Ascii, cellEvent.CELL.PRODUCTID);
                            packet.addItem(DataType.Ascii, cellEvent.CELL.STEPID);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "301");
                        packet.addItem(DataType.List, 3);
                        {
                            packet.addItem(DataType.Ascii, cellEvent.WORKORDER.PROCESSJOB);
                            packet.addItem(DataType.Ascii, cellEvent.WORKORDER.PLANQTY);
                            packet.addItem(DataType.Ascii, cellEvent.WORKORDER.PROCESSEDQTY);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "400");
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, cellEvent.READER.READERID);
                            packet.addItem(DataType.Ascii, cellEvent.READER.READERRESULTCODE);
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
    

    /// <summary>
    /// T: 402 Cell Process Complete
    /// CEID : 402
    /// </summary>

        public void SendS6F11_402( CELLEVENTDATA cellEvent, string ceid)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, ceid);
                packet.addItem(DataType.List, 8);
                {
                    packet.addItem(DataType.List, 2);
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
                        packet.addItem(DataType.Ascii, "300");
                        packet.addItem(DataType.List, 4);
                        {
                            packet.addItem(DataType.Ascii, cellEvent.CELL.CELLID);
                            packet.addItem(DataType.Ascii, cellEvent.CELL.PPID);
                            packet.addItem(DataType.Ascii, cellEvent.CELL.PRODUCTID);
                            packet.addItem(DataType.Ascii, cellEvent.CELL.STEPID);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "301");
                        packet.addItem(DataType.List, 3);
                        {
                            packet.addItem(DataType.Ascii, cellEvent.WORKORDER.PROCESSJOB);
                            packet.addItem(DataType.Ascii, cellEvent.WORKORDER.PLANQTY);
                            packet.addItem(DataType.Ascii, cellEvent.WORKORDER.PROCESSEDQTY);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "400");
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, cellEvent.READER.READERID);
                            packet.addItem(DataType.Ascii, cellEvent.READER.READERRESULTCODE);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "200");
                        packet.addItem(DataType.List, cellEvent.MATERIALs.Count);
                        foreach (var item in cellEvent.MATERIALs)
                        {
                            packet.addItem(DataType.List, 5);
                            {
                                packet.addItem(DataType.Ascii, item.EQPMATERIALID);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALTYPE);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALST);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALPORTID);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALUSEQTY);
                            }
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "600");
                        packet.addItem(DataType.List, cellEvent.DVs.Count);
                        foreach (var item in cellEvent.DVs)
                        {
                            packet.addItem(DataType.List, 2);
                            {
                                packet.addItem(DataType.Ascii, item.DVNAME);
                                packet.addItem(DataType.Ascii, item.DVVAL);
                            }
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "500");
                        packet.addItem(DataType.List, 4);
                        {
                            packet.addItem(DataType.Ascii, cellEvent.JUDGEMENT.OPERATORID1);
                            packet.addItem(DataType.Ascii, cellEvent.JUDGEMENT.JUDGE);
                            packet.addItem(DataType.Ascii, cellEvent.JUDGEMENT.REASONCODE);
                            packet.addItem(DataType.Ascii, cellEvent.JUDGEMENT.DESCRIPTION);
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
    
    /// <summary>
    /// T:403 Normal Data Collection
    /// CEID : 403
    /// </summary>

        public void SendS6F11_403( CELLEVENTDATA cellEvent, string ceid)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, ceid);
                packet.addItem(DataType.List, 3);
                {
                    packet.addItem(DataType.List, 2);
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
                        packet.addItem(DataType.Ascii, "300");
                        packet.addItem(DataType.List, 4);
                        {
                            packet.addItem(DataType.Ascii, cellEvent.CELL.CELLID);
                            packet.addItem(DataType.Ascii, cellEvent.CELL.PPID);
                            packet.addItem(DataType.Ascii, cellEvent.CELL.PRODUCTID);
                            packet.addItem(DataType.Ascii, cellEvent.CELL.STEPID);
                        }
                    }
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, "600");
                        packet.addItem(DataType.List, cellEvent.DVs.Count);
                        foreach (var item in cellEvent.DVs)
                        {
                            packet.addItem(DataType.List, 2);
                            {
                                packet.addItem(DataType.Ascii, item.DVNAME);
                                packet.addItem(DataType.Ascii, item.DVVAL);
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
    
    /// <summary>
    /// T:404 Cassette Unit Process
    /// CEID : 404
    /// </summary>

        public void SendS6F11_404( CASSETTEUNITPROCESS cassetteEvent, string ceid)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 11;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys++;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, "0");
                packet.addItem(DataType.Ascii, ceid);
                packet.addItem(DataType.List, 3);
                {
                    packet.addItem(DataType.List, 2);
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
                        packet.addItem(DataType.List, 8);
                        {
                            packet.addItem(DataType.Ascii, cassetteEvent.CASSETTEUNIT.PRODUCTID);
                            packet.addItem(DataType.Ascii, cassetteEvent.CASSETTEUNIT.PPID);
                            packet.addItem(DataType.Ascii, cassetteEvent.CASSETTEUNIT.CASSETTEID);
                            packet.addItem(DataType.Ascii, cassetteEvent.CASSETTEUNIT.CASSETTETYPE);
                            packet.addItem(DataType.Ascii, cassetteEvent.CASSETTEUNIT.ITEMCOUNT1);
                            packet.addItem(DataType.Ascii, cassetteEvent.CASSETTEUNIT.ITEMCOUNT2);
                            packet.addItem(DataType.Ascii, cassetteEvent.CASSETTEUNIT.FROMUNITID);
                            packet.addItem(DataType.Ascii, cassetteEvent.CASSETTEUNIT.TOUNITID);
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
    

    /// <summary>
    /// T: 405 EQP Specific Report
    /// CEID : 405
    /// </summary>

    public void SendS6F11_405( EQPSPECIFICREPORT eqpSpecifi, string ceid)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, ceid);
            packet.addItem(DataType.List, 3);
            {
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "100");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                        packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.CRST);
                    }
                }
                packet.addItem(DataType.List, 6);
                {
                    packet.addItem(DataType.Ascii, eqpSpecifi.CELLID);
                    packet.addItem(DataType.Ascii, eqpSpecifi.PPID);
                    packet.addItem(DataType.Ascii, eqpSpecifi.PRODUCTID);
                    packet.addItem(DataType.Ascii, eqpSpecifi.STEPID);
                    packet.addItem(DataType.Ascii, eqpSpecifi.OPTIONINFO);
                    packet.addItem(DataType.Ascii, eqpSpecifi.DESCRIPTION);
                }
                packet.addItem(DataType.List, eqpSpecifi.ITEMs.Count);
                foreach (var item in eqpSpecifi.ITEMs)
                {
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, item.ITEMNAME);
                        packet.addItem(DataType.Ascii, item.ITEMVALUE);
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

    /// <summary>
    /// T:406 Cell Process End
    /// CEID : 406
    /// </summary>

    public void SendS6F11_406( CELLEVENTDATA cellEvent, string ceid)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, ceid);
            packet.addItem(DataType.List, 8);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "300");
                    packet.addItem(DataType.List, 4);
                    {
                        packet.addItem(DataType.Ascii, cellEvent.CELL.CELLID);
                        packet.addItem(DataType.Ascii, cellEvent.CELL.PPID);
                        packet.addItem(DataType.Ascii, cellEvent.CELL.PRODUCTID);
                        packet.addItem(DataType.Ascii, cellEvent.CELL.STEPID);
                    }
                }
                packet.addItem(DataType.List, 2);
                {

                    packet.addItem(DataType.List, 3);
                    {

                    }
                    packet.addItem(DataType.Ascii, "301");
                    packet.addItem(DataType.List, 3);
                    {
                        packet.addItem(DataType.Ascii, cellEvent.WORKORDER.PROCESSJOB);
                        packet.addItem(DataType.Ascii, cellEvent.WORKORDER.PLANQTY);
                        packet.addItem(DataType.Ascii, cellEvent.WORKORDER.PROCESSEDQTY);
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "400");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, cellEvent.READER.READERID);
                        packet.addItem(DataType.Ascii, cellEvent.READER.READERRESULTCODE);
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "201");
                    packet.addItem(DataType.List, cellEvent.MATERIALs.Count);
                    foreach (var item in cellEvent.MATERIALs)
                    {
                        packet.addItem(DataType.List, 17);
                        {
                            packet.addItem(DataType.Ascii, item.EQPMATERIALBATCHID);
                            packet.addItem(DataType.Ascii, item.EQPMATERIALBATCHNAME);
                            packet.addItem(DataType.Ascii, item.EQPMATERIALID);
                            packet.addItem(DataType.Ascii, item.EQPMATERIALTYPE);
                            packet.addItem(DataType.Ascii, item.EQPMATERIALST);
                            packet.addItem(DataType.Ascii, item.EQPMATERIALPORTID);
                            packet.addItem(DataType.Ascii, item.EQPMATERIALSTATE);
                            packet.addItem(DataType.Ascii, item.EQPMATERIALTOTALQTY);
                            packet.addItem(DataType.Ascii, item.EQPMATERIALUSEQTY);
                            packet.addItem(DataType.Ascii, item.EQPMATERIALASSEMQTY);
                            packet.addItem(DataType.Ascii, item.EQPMATERIALNGQTY);
                            packet.addItem(DataType.Ascii, item.EQPMATERIALREMAINQTY);
                            packet.addItem(DataType.Ascii, item.EQPMATERIALPRODUCTQTY);
                            packet.addItem(DataType.Ascii, item.EQPMATERIALPROCUSEQTY);
                            packet.addItem(DataType.Ascii, item.EQPMATERIALPROCASSEMQTY);
                            packet.addItem(DataType.Ascii, item.EQPMATERIALPROCNGQTY);
                            packet.addItem(DataType.Ascii, item.EQPMATERIALSUPPLYREQUESTQTY);
                        }
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "600");
                    packet.addItem(DataType.List, cellEvent.DVs.Count);
                    foreach (var item in cellEvent.DVs)
                    {
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, item.DVNAME);
                            packet.addItem(DataType.Ascii, item.DVVAL);
                        }
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "501");
                    packet.addItem(DataType.List, 4);// để là 4 thì test được với secom để là 6 thì lỗi Illegal Format
                    {
                        packet.addItem(DataType.Ascii, cellEvent.JUDGEMENT.OPERATORID1);
                        packet.addItem(DataType.Ascii, cellEvent.JUDGEMENT.OPERATORID2);
                        packet.addItem(DataType.Ascii, cellEvent.JUDGEMENT.OPERATORID3);
                        packet.addItem(DataType.Ascii, cellEvent.JUDGEMENT.JUDGE);
                        packet.addItem(DataType.Ascii, cellEvent.JUDGEMENT.REASONCODE);
                        packet.addItem(DataType.Ascii, cellEvent.JUDGEMENT.DESCRIPTION);
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

    /// <summary>
    /// T:411 Pair Cell Process Start
    /// CEID : 411
    /// </summary>

    public void SendS6F11_411( CELLEVENTDATA pairCell, string ceid)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, ceid);
            packet.addItem(DataType.List, 3);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "308");
                    packet.addItem(DataType.List, 5);
                    {
                        packet.addItem(DataType.Ascii, pairCell.CELL.CELLID);
                        packet.addItem(DataType.Ascii, pairCell.CELL.PRODUCTTYPE);
                        packet.addItem(DataType.Ascii, pairCell.CELL.PPID);
                        packet.addItem(DataType.Ascii, pairCell.CELL.PRODUCTID);
                        packet.addItem(DataType.Ascii, pairCell.CELL.STEPID);
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "400");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, pairCell.READER.READERID);
                        packet.addItem(DataType.Ascii, pairCell.READER.READERRESULTCODE);
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

    /// <summary>
    /// T:411 Pair Cell Process Start
    /// CEID : 411
    /// </summary>

    public void SendS6F11_412( CELLEVENTDATA pairCell, string ceid)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, ceid);
            packet.addItem(DataType.List, 5);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "308");
                    packet.addItem(DataType.List, 5);
                    {
                        packet.addItem(DataType.Ascii, pairCell.CELL.CELLID);
                        packet.addItem(DataType.Ascii, pairCell.CELL.PRODUCTTYPE);
                        packet.addItem(DataType.Ascii, pairCell.CELL.PPID);
                        packet.addItem(DataType.Ascii, pairCell.CELL.PRODUCTID);
                        packet.addItem(DataType.Ascii, pairCell.CELL.STEPID);
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "400");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, pairCell.READER.READERID);
                        packet.addItem(DataType.Ascii, pairCell.READER.READERRESULTCODE);
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "600");
                    packet.addItem(DataType.List, pairCell.DVs.Count);
                    foreach (var item in pairCell.DVs)
                    {
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, item.DVNAME);
                            packet.addItem(DataType.Ascii, item.DVVAL);
                        }
                    }

                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "500");
                    packet.addItem(DataType.List, 4);
                    {
                        packet.addItem(DataType.Ascii, pairCell.JUDGEMENT.OPERATORID1);
                        packet.addItem(DataType.Ascii, pairCell.JUDGEMENT.JUDGE);
                        packet.addItem(DataType.Ascii, pairCell.JUDGEMENT.REASONCODE);
                        packet.addItem(DataType.Ascii, pairCell.JUDGEMENT.DESCRIPTION);
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

    /// <summary>
    /// T:421 Pair Cell Process Complete
    /// CEID : 421
    /// </summary>

    public void SendS6F11_421( TRACKING tracking, string ceid)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++      ;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, ceid);
            packet.addItem(DataType.List, 5);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "310");
                    packet.addItem(DataType.List, 9);
                    {
                        packet.addItem(DataType.Ascii, tracking.APN.CELLID);
                        packet.addItem(DataType.Ascii, tracking.APN.PAIRCELLID);
                        packet.addItem(DataType.Ascii, tracking.APN.CELLTYPE);
                        packet.addItem(DataType.Ascii, tracking.APN.INDEX);
                        packet.addItem(DataType.Ascii, tracking.APN.OPTIONINFO);
                        packet.addItem(DataType.Ascii, tracking.APN.PPID);
                        packet.addItem(DataType.Ascii, tracking.APN.PRODUCTID);
                        packet.addItem(DataType.Ascii, tracking.APN.STEPID);
                        packet.addItem(DataType.Ascii, tracking.APN.DESCRIPTION);
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "400");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, tracking.READER.READERID);
                        packet.addItem(DataType.Ascii, tracking.READER.READERRESULTCODE);
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "600");
                    packet.addItem(DataType.List, tracking.DVs.Count);
                    foreach (var item in tracking.DVs)
                    {
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, item.DVNAME);
                            packet.addItem(DataType.Ascii, item.DVVAL);
                        }
                    }

                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "500");
                    packet.addItem(DataType.List, 4);
                    {
                        packet.addItem(DataType.Ascii, tracking.JUDGEMENT.OPERATORID1);
                        packet.addItem(DataType.Ascii, tracking.JUDGEMENT.JUDGE);
                        packet.addItem(DataType.Ascii, tracking.JUDGEMENT.REASONCODE);
                        packet.addItem(DataType.Ascii, tracking.JUDGEMENT.DESCRIPTION);
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


    /// <summary>
    /// T: 501 OPCALL CONFIRM
    /// </summary>

    public void SendS6F11_501( List<OPCALLMESS> opcall, string ceid)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, ceid);
            packet.addItem(DataType.List, 4);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "300");
                    packet.addItem(DataType.List, 4);
                    {
                        packet.addItem(DataType.Ascii, "");
                        packet.addItem(DataType.Ascii, "");
                        packet.addItem(DataType.Ascii, "");
                        packet.addItem(DataType.Ascii, "");
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "700");
                    packet.addItem(DataType.List, opcall.Count);
                    foreach (var item in opcall)
                    {
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, item.OPCALLID);
                            packet.addItem(DataType.Ascii, item.MESSAGE);
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

    /// <summary>
    /// T:502 INTERLOCK CONFIRM
    /// </summary>

    public void SendS6F11_502( List<INTERLOCKMESS> interlock, string ceid)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, ceid);
            packet.addItem(DataType.List, 4);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "300");
                    foreach(var item in interlock)
                    {
                        packet.addItem(DataType.List, 4);
                        {
                            packet.addItem(DataType.Ascii, item.CELLID);
                            packet.addItem(DataType.Ascii, item.PPID);
                            packet.addItem(DataType.Ascii, item.PRODUCTID);
                            packet.addItem(DataType.Ascii, item.STEPID);
                        }
                    }    
                    
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "701");
                    packet.addItem(DataType.List, interlock.Count);
                    foreach (var item in interlock)
                    {
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, item.INTERLOCKID);
                            packet.addItem(DataType.Ascii, item.MESSAGE);
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

    /// <summary>
    /// T:513 UNIT OPCALL CONFIRM
    /// </summary>

    public void SendS6F11_513( UNITOPCALLCONFIRM opcall, string ceid)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, ceid);
            packet.addItem(DataType.List, 4);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "102");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, opcall.UNIT.UNITID);
                        packet.addItem(DataType.List, 9);
                        {
                            packet.addItem(DataType.Ascii, opcall.UNIT.AVAILABILITYSTATE);
                            packet.addItem(DataType.Ascii, opcall.UNIT.INTERLOCKSTATE);
                            packet.addItem(DataType.Ascii, opcall.UNIT.MOVESTATE);
                            packet.addItem(DataType.Ascii, opcall.UNIT.RUNSTATE);
                            packet.addItem(DataType.Ascii, opcall.UNIT.FRONTSTATE);
                            packet.addItem(DataType.Ascii, opcall.UNIT.REARSTATE);
                            packet.addItem(DataType.Ascii, opcall.UNIT.PPSPLSTATE);
                            packet.addItem(DataType.Ascii, opcall.UNIT.REASONCODE);
                            packet.addItem(DataType.Ascii, opcall.UNIT.DESCRIPTION);
                        }
                    }

                }

                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "300");
                    packet.addItem(DataType.List, 4);
                    {
                        packet.addItem(DataType.Ascii, opcall.CELL.CELLID);
                        packet.addItem(DataType.Ascii, opcall.CELL.PPID);
                        packet.addItem(DataType.Ascii, opcall.CELL.PRODUCTID);
                        packet.addItem(DataType.Ascii, opcall.CELL.STEPID);
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "700");
                    packet.addItem(DataType.List, opcall.OPCALLs.Count);
                    foreach (var item in opcall.OPCALLs)
                    {
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, item.OPCALLID);
                            packet.addItem(DataType.Ascii, item.MESSAGE);
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

    /// <summary>
    /// T:514 UNIT INTERLOCK CONFIRM
    /// </summary>

    public void SendS6F11_514( UNITINTERLOCKCONFIRM interlock, string ceid)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, ceid);
            packet.addItem(DataType.List, 4);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "102");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, interlock.UNIT.UNITID);
                        packet.addItem(DataType.List, 9);
                        {
                            packet.addItem(DataType.Ascii, interlock.UNIT.AVAILABILITYSTATE);
                            packet.addItem(DataType.Ascii, interlock.UNIT.INTERLOCKSTATE);
                            packet.addItem(DataType.Ascii, interlock.UNIT.MOVESTATE);
                            packet.addItem(DataType.Ascii, interlock.UNIT.RUNSTATE);
                            packet.addItem(DataType.Ascii, interlock.UNIT.FRONTSTATE);
                            packet.addItem(DataType.Ascii, interlock.UNIT.REARSTATE);
                            packet.addItem(DataType.Ascii, interlock.UNIT.PPSPLSTATE);
                            packet.addItem(DataType.Ascii, interlock.UNIT.REASONCODE);
                            packet.addItem(DataType.Ascii, interlock.UNIT.DESCRIPTION);
                        }
                    }

                }

                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "300");
                    packet.addItem(DataType.List, 4);
                    {
                        packet.addItem(DataType.Ascii, interlock.CELL.CELLID);
                        packet.addItem(DataType.Ascii, interlock.CELL.PPID);
                        packet.addItem(DataType.Ascii, interlock.CELL.PRODUCTID);
                        packet.addItem(DataType.Ascii, interlock.CELL.STEPID);
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "701");
                    packet.addItem(DataType.List, interlock.INTERLOCKs.Count);
                    foreach (var item in interlock.INTERLOCKs)
                    {
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, item.INTERLOCKID);
                            packet.addItem(DataType.Ascii, item.MESSAGE);
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


    /// <summary>
    /// T:601 Reader Result
    /// </summary>

    public void SendS6F11_601( READER reader, string CellID)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, "601");
            packet.addItem(DataType.List, 1);
            {
                packet.addItem(DataType.List, 5);
                {
                    packet.addItem(DataType.Ascii, "800");
                    packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                    packet.addItem(DataType.Ascii, CellID);
                    packet.addItem(DataType.Ascii, reader.READERID);
                    packet.addItem(DataType.Ascii, reader.READERRESULTCODE);
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

    /// <summary>
    /// T:602 Start Cell Lot
    /// </summary>

    public void SendS6F11_602( CELLLOTSTART cellLot)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, "602");
            packet.addItem(DataType.List, 1);
            {
                packet.addItem(DataType.List, 5);
                {
                    packet.addItem(DataType.Ascii, "801");
                    packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                    packet.addItem(DataType.Ascii, cellLot.READERID);
                    packet.addItem(DataType.Ascii, cellLot.READERRESULTCODE);
                    packet.addItem(DataType.List, cellLot.CELLLOTs.Count);
                    foreach (var item in cellLot.CELLLOTs)
                    {
                        packet.addItem(DataType.List, 2);
                        {
                            packet.addItem(DataType.Ascii, item.CELLID);
                            packet.addItem(DataType.Ascii, item.PARENTLOT);
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

    /// <summary>
    /// T:603 Equipment Status Change By User
    /// </summary>

    public void SendS6F11_603( EQUIPSTATUSCHANGE statusChange)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, "603");
            packet.addItem(DataType.List, 3);
            {
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "802");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, statusChange.RPTID802.EQPID);
                        packet.addItem(DataType.Ascii, statusChange.RPTID802.DATA_TYPE);
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "803");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, statusChange.RPTID803.ADDRESS);
                        packet.addItem(DataType.Ascii, statusChange.RPTID803.VALUE);
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "804");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, statusChange.RPTID804.LOSSDISPLAY);
                        packet.addItem(DataType.Ascii, statusChange.RPTID804.LOSS);
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

    /// <summary>
    /// T:604 RFID Reader Result
    /// </summary>

    public void SendS6F11_604( READER reader)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, "604");
            packet.addItem(DataType.List, 1);
            {
                packet.addItem(DataType.List, 5);
                {
                    packet.addItem(DataType.Ascii, "800");
                    packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                    packet.addItem(DataType.Ascii, "");
                    packet.addItem(DataType.Ascii, reader.READERID);
                    packet.addItem(DataType.Ascii, reader.READERRESULTCODE);
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

    /// <summary>
    /// T:605 Material ID Reader Result
    /// </summary>

    public void SendS6F11_605( MATERIALSTATE material)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, "605");
            packet.addItem(DataType.List, 1);
            {
                packet.addItem(DataType.List, 4);
                {
                    packet.addItem(DataType.Ascii, "810");
                    packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                    packet.addItem(DataType.Ascii, material.MATERIALID);
                    packet.addItem(DataType.Ascii, material.MATERIALPORTID);
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


    /// <summary>
    ///  T:606 Equipment Loss Code Report
    /// </summary>

    public void SendS6F11_606( LOSSCODEREPORT losscode)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, "606");
            packet.addItem(DataType.List, 3);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "806");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, losscode.LOSSCODE.LOSSCODE);
                        packet.addItem(DataType.Ascii, losscode.LOSSCODE.LOSSDESCRIPTION);
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

    /// <summary>
    /// T:607 Operator Information Report
    /// </summary>

    public void SendS6F11_607( OPERATOR oper)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, "607");
            packet.addItem(DataType.List, 2);
            {
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "105");
                    packet.addItem(DataType.List, 3);
                    {
                        packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                        packet.addItem(DataType.Ascii, oper.OPTIONINFO);
                        packet.addItem(DataType.Ascii, oper.COMMENT);
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "106");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, oper.OPERATORID);
                        packet.addItem(DataType.Ascii, oper.PASSWORD);
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

    /// <summary>
    /// T:608 Job Information Report
    /// </summary>

    public void SendS6F11_608( JOB job)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, "608");
            packet.addItem(DataType.List, 2);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "807");
                    packet.addItem(DataType.List, 6);
                    {
                        packet.addItem(DataType.Ascii, job.FINALEQPID);
                        packet.addItem(DataType.Ascii, job.JOBID);
                        packet.addItem(DataType.Ascii, job.JOBTYPE);
                        packet.addItem(DataType.Ascii, job.READERID);
                        packet.addItem(DataType.Ascii, job.READERRESULT);
                        packet.addItem(DataType.Ascii, job.OPERID);
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

    /// <summary>
    /// T:609 Inspection Result Report
    /// </summary>

    public void SendS6F11_609( INSPECTION insp)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, "609");
            packet.addItem(DataType.List, 2);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "808");
                    packet.addItem(DataType.List, 8);
                    {
                        packet.addItem(DataType.Ascii, insp.PROCESSNAME);
                        packet.addItem(DataType.Ascii, insp.CELLID);
                        packet.addItem(DataType.Ascii, insp.PROCESSFLAG);
                        packet.addItem(DataType.Ascii, insp.JUDGE);
                        packet.addItem(DataType.Ascii, insp.REASONCODE);
                        packet.addItem(DataType.Ascii, insp.OPERID);
                        packet.addItem(DataType.Ascii, insp.SENDUNIQUEINFO);
                        packet.addItem(DataType.Ascii, insp.REVUNIQUEINFO);
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


    /// <summary>
    /// T:615 Material ID Reader Result
    /// </summary>

    public void SendS6F11_615( MATERIALSTATE material, string UnitId)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, "604");
            packet.addItem(DataType.List, 1);
            {
                packet.addItem(DataType.List, 5);
                {
                    packet.addItem(DataType.Ascii, "811");
                    packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                    packet.addItem(DataType.Ascii, UnitId);
                    packet.addItem(DataType.Ascii, material.MATERIALID);
                    packet.addItem(DataType.Ascii, material.MATERIALPORTID);
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

    /// <summary>
    /// T:616 Unit Loss Code Report
    /// </summary>

    public void SendS6F11_616( EQPSTATE unit, LOSSCODEREPORT losscode)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, "604");
            packet.addItem(DataType.List, 3);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "102");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, unit.UNITID);
                        packet.addItem(DataType.List, 9);
                        {
                            packet.addItem(DataType.Ascii, unit.AVAILABILITYSTATE);
                            packet.addItem(DataType.Ascii, unit.INTERLOCKSTATE);
                            packet.addItem(DataType.Ascii, unit.MOVESTATE);
                            packet.addItem(DataType.Ascii, unit.RUNSTATE);
                            packet.addItem(DataType.Ascii, unit.FRONTSTATE);
                            packet.addItem(DataType.Ascii, unit.REARSTATE);
                            packet.addItem(DataType.Ascii, unit.PPSPLSTATE);
                            packet.addItem(DataType.Ascii, unit.REASONCODE);
                            packet.addItem(DataType.Ascii, unit.DESCRIPTION);
                        }
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "806");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, losscode.LOSSCODE.LOSSCODE);
                        packet.addItem(DataType.Ascii, losscode.LOSSCODE.LOSSDESCRIPTION);
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

    /// <summary>
    /// T:701 EPT Process
    /// </summary>

    public void SendS6F11_701( CELLEVENTDATA cellEvent, string ceid)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, ceid);
            packet.addItem(DataType.List, 5);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "300");
                    packet.addItem(DataType.List, 4);
                    {
                        packet.addItem(DataType.Ascii, cellEvent.CELL.CELLID);
                        packet.addItem(DataType.Ascii, cellEvent.CELL.PPID);
                        packet.addItem(DataType.Ascii, cellEvent.CELL.PRODUCTID);
                        packet.addItem(DataType.Ascii, cellEvent.CELL.STEPID);
                    }
                }

                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "301");
                    packet.addItem(DataType.List, 3);
                    {
                        packet.addItem(DataType.Ascii, cellEvent.WORKORDER.PROCESSJOB);
                        packet.addItem(DataType.Ascii, cellEvent.WORKORDER.PLANQTY);
                        packet.addItem(DataType.Ascii, cellEvent.WORKORDER.PROCESSEDQTY);
                    }
                }

                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "400");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, cellEvent.READER.READERID);
                        packet.addItem(DataType.Ascii, cellEvent.READER.READERRESULTCODE);
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

    /// <summary>
    /// T:801~807 Packing Process
    /// </summary>

    public void SendS6F11_801( string ceid, PACKING packing)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, ceid);
            packet.addItem(DataType.List, 2);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "820");
                    packet.addItem(DataType.List, 6);
                    {
                        packet.addItem(DataType.Ascii, packing.SBPID);
                        packet.addItem(DataType.Ascii, packing.SBPREALWEIGHT);
                        packet.addItem(DataType.Ascii, packing.CARTONID);
                        packet.addItem(DataType.Ascii, packing.CARTONREALWEIGHT);
                        packet.addItem(DataType.Ascii, packing.CHECKERNAME);
                        packet.addItem(DataType.Ascii, packing.ERRORMESSAGE);
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

    /// <summary>
    /// T:811~813 Packing Job Process
    /// </summary>

    public void SendS6F11_811(CELLEVENTDATA cellEvent, string ceid)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, ceid);
            packet.addItem(DataType.List, 5);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "300");
                    packet.addItem(DataType.List, 4);
                    {
                        packet.addItem(DataType.Ascii, cellEvent.CELL.CELLID);
                        packet.addItem(DataType.Ascii, cellEvent.CELL.PPID);
                        packet.addItem(DataType.Ascii, cellEvent.CELL.PRODUCTID);
                        packet.addItem(DataType.Ascii, cellEvent.CELL.STEPID);
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "301");
                    packet.addItem(DataType.List, 3);
                    {
                        packet.addItem(DataType.Ascii, cellEvent.WORKORDER.PROCESSJOB);
                        packet.addItem(DataType.Ascii, cellEvent.WORKORDER.PLANQTY);
                        packet.addItem(DataType.Ascii, cellEvent.WORKORDER.PROCESSEDQTY);
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "400");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, cellEvent.READER.READERID);
                        packet.addItem(DataType.Ascii, cellEvent.READER.READERRESULTCODE);
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

    /// <summary>
    /// T:900~901 UNIT MATERIAL CHANGE
    /// </summary>

    public void SendS6F11_900( MATERIALCHANGE materialChange, string unitId, string ceid)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, ceid);
            packet.addItem(DataType.List, 3);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "101");      //* RPTID
                    packet.addItem(DataType.List, 9);           // NEW STATE
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
                    packet.addItem(DataType.Ascii, "200");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, unitId);
                        packet.addItem(DataType.List, 5);
                        {
                            packet.addItem(DataType.Ascii, materialChange.MATERIALID);
                            packet.addItem(DataType.Ascii, materialChange.MATERIALTYPE);
                            packet.addItem(DataType.Ascii, materialChange.MATERIALST);
                            packet.addItem(DataType.Ascii, materialChange.MATERIALPORTID);
                            packet.addItem(DataType.Ascii, materialChange.MATERIALUSAGE);
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

    /// <summary>
    /// T:911~925 Material Process Change
    /// </summary>

    public void SendS6F11_911( MATERIALPROCESSCHANGEDATA materialChange, string unitId, string ceid)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, ceid);
            packet.addItem(DataType.List, 4);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.List, 9);           // NEW STATE
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
                    packet.addItem(DataType.Ascii, "300");
                    packet.addItem(DataType.List, 4);
                    {
                        packet.addItem(DataType.Ascii, materialChange.CELL.CELLID);
                        packet.addItem(DataType.Ascii, materialChange.CELL.PPID);
                        packet.addItem(DataType.Ascii, materialChange.CELL.PRODUCTID);
                        packet.addItem(DataType.Ascii, materialChange.CELL.STEPID);
                    }
                }
                packet.addItem(DataType.List, 2);
                {
                    packet.addItem(DataType.Ascii, "201");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, unitId);
                        packet.addItem(DataType.List, materialChange.MATERIALs.Count);
                        foreach (var item in materialChange.MATERIALs)
                        {
                            packet.addItem(DataType.List, 17);
                            {
                                packet.addItem(DataType.Ascii, item.EQPMATERIALBATCHID);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALBATCHNAME);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALID);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALTYPE);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALST);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALPORTID);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALSTATE);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALTOTALQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALUSEQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALASSEMQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALNGQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALREMAINQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALPRODUCTQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALPROCUSEQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALPROCASSEMQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALPROCNGQTY);
                                packet.addItem(DataType.Ascii, item.EQPMATERIALSUPPLYREQUESTQTY);

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


    /// <summary>
    /// T: 954~955 Port State Change
    /// </summary>

    public void SendS6F11_954( PORTSTATECHANGE portChange, string unitId, string ceid)
    {
        try
        {
            SysPacket packet = new SysPacket(_cim.Conn);
            packet.Stream = 6;
            packet.Function = 11;
            packet.Command = Command.UserData;
            packet.DeviceId = EqpData.DeviceId;
            packet.SystemByte = EqpData.TransactionSys++;
            packet.addItem(DataType.List, 3);
            packet.addItem(DataType.Ascii, "0");
            packet.addItem(DataType.Ascii, ceid);
            packet.addItem(DataType.List, 2);
            {
                packet.addItem(DataType.List, 2);
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
                    packet.addItem(DataType.Ascii, "305");
                    packet.addItem(DataType.List, 2);
                    {
                        packet.addItem(DataType.Ascii, unitId);
                        packet.addItem(DataType.List, 5);
                        {
                            packet.addItem(DataType.Ascii, portChange.PORTSTATE.PORTNO);
                            packet.addItem(DataType.Ascii, portChange.PORTSTATE.PORTAVAILABILITYSTATE);
                            packet.addItem(DataType.Ascii, portChange.PORTSTATE.PORTACCESSMODE);
                            packet.addItem(DataType.Ascii, portChange.PORTSTATE.PORTTRANSFERSTATE);
                            packet.addItem(DataType.Ascii, portChange.PORTSTATE.PORTPROCESSINGSTATE);
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
