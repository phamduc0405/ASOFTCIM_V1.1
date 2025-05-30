using ASOFTCIM.Helper;
using ASOFTCIM.Data;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS7F223(SysPacket sysPacket)
        {
            try
            {
                ReadRMS();
                string ACK = "0";
                PPIDINFOR ppid = new PPIDINFOR();
                ppid.EQPID = sysPacket.GetItemString(1);
                ppid.UNITID = sysPacket.GetItemString();
                ppid.PPID = sysPacket.GetItemString();
                ppid.PPID_TYPE = sysPacket.GetItemString();
                ppid.PPID_NUMBER = sysPacket.GetItemString();
                int count = int.Parse(sysPacket.GetItemString());
                ppid.COMMANDCODEs = new List<COMMANDCODE>();
                for (int i = 0; i < count; i++)
                {
                    string lst = sysPacket.GetItemString();
                    COMMANDCODE cmd = new COMMANDCODE();
                    cmd.PARAMs = new List<PARAM>();
                    cmd.CCODE = sysPacket.GetItemString();
                    int countParams = int.Parse(sysPacket.GetItemString());
                    for (int j = 0; j < countParams; j++)
                    {
                        string lst2 = sysPacket.GetItemString();
                        PARAM param = new PARAM();
                        param.PARAMNAME = sysPacket.GetItemString(); 
                        param.PARAMVALUE = sysPacket.GetItemString();
                        if(param.PARAMVALUE== null || param.PARAMNAME == null)
                        {
                            ACK = "2";
                            SendS7F224(ACK);
                            return;
                        }    
                        cmd.PARAMs.Add(param);
                    }
                    ppid.COMMANDCODEs.Add(cmd);
                }

                ppid.MODE = sysPacket.GetItemString(8);
                if (false) // Host không có quyền thay đổi
                {
                    ACK = "1";
                }    
                if((EqpData.PPIDList.PPID.Contains(ppid.PPID) || ppid.PPID == null ) && (ppid.MODE == "1" || ppid.MODE == "1"))
                {
                    ACK = "6";
                       
                }    
                    //  if (_cim.EQHelper.PLCData.LstPPID.Any(x => x.PPID_NUMBER == ppid.PPID_NUMBER && string.IsNullOrEmpty(x.GetValue))|| string.IsNullOrEmpty(ppid.PPID_NUMBER)) ACK = "7";
                if (ppid.EQPID != _cim.EQPID) ACK = "7";
                if (ppid.MODE == "3" && ppid.PPID != EqpData.CurrPPID.PPID) ACK = "3";
                if (ppid.PPID_TYPE == "2" || string.IsNullOrEmpty(ppid.PPID_TYPE) || ppid.COMMANDCODEs[0].CCODE == "4" || string.IsNullOrEmpty(ppid.COMMANDCODEs[0].CCODE)) ACK = "9";
                if (EqpData.EQPSTATE.MOVESTATE == "2")
                {
                    ACK = "5";
                }

                
                if ((ppid.PPID_NUMBER[1].ToString() == "D" || ppid.PPID_NUMBER[1].ToString() == "D") && (ppid.MODE != "2"))
                {
                    ACK = "1";
                }
                if ((ppid.PPID_NUMBER[1].ToString() == "N" || ppid.PPID_NUMBER[1].ToString() == "O") && (ppid.MODE != "1"))
                {
                    ACK = "1";
                }
                if ((ppid.PPID_NUMBER[1].ToString() == "C") && (ppid.MODE != "3"))
                {
                    ACK = "1";
                }
                
                if (ACK!="0")
                {
                    SendS7F224( ACK);
                    return;
                }
                SendMessage2PLC("FORMATTEDPROCESSPROGRAMSEND2", ppid, _plc);
                SendS7F24(ACK);
            }
            catch (Exception ex)
            {
                SendS9F7(sysPacket);
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception,debug);
            }
        }
    }
}
