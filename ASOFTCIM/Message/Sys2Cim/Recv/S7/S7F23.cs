using A_SOFT.CMM.INIT;
using ASOFTCIM.Helper;
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
        public void RecvS7F23()
        {
            try
            {
                string ACK = "0";
                // nếu thiết bị cho phép Recipe Delete By Host => false , nếu không cho phép => true
                if (false)
                {
                    ACK = "1";
                    SendS7F24(ACK);
                    return;
                }
                PPIDINFOR ppid = new PPIDINFOR();
                ppid.EQPID = _cim.SysPacket.GetItemString(1);
                ppid.PPID = _cim.SysPacket.GetItemString();
                ppid.PPID_TYPE = _cim.SysPacket.GetItemString();
                //Abnormal 
                if (ppid.PPID_TYPE == "2" || string.IsNullOrEmpty(ppid.PPID_TYPE))
                {
                    ACK = "9";
                    SendS7F24(ACK);
                    return;
                }
                if (EqpData.CurrPPID.PPID == ppid.PPID)
                {
                    ACK = "3";
                    SendS7F24(ACK);
                    return;
                }
                if (ppid.EQPID != EqpData.EQINFORMATION.EQPID)
                {
                    ACK = "7";
                    SendS7F24(ACK);
                    return;
                }
                if(EqpData.EQPSTATE.RUNSTATE == "2")
                {
                    ACK = "5";
                    SendS7F24(ACK);
                    return;
                }    
                //Abnormal 
                int count = int.Parse(_cim.SysPacket.GetItemString());
                for (int i = 0; i < count; i++)
                {
                    string lst = _cim.SysPacket.GetItemString();
                    COMMANDCODE cmd = new COMMANDCODE();
                    cmd.CCODE = _cim.SysPacket.GetItemString();
                    //Abnormal 
                    if (cmd.CCODE == "4" || string.IsNullOrEmpty(cmd.CCODE))
                    {
                        ACK = "9";
                        SendS7F24(ACK);
                        return;
                    }
                    //Abnormal
                    int countParams = int.Parse(_cim.SysPacket.GetItemString());
                    for (int j = 0; j < countParams; j++)
                    {
                        string lst2 = _cim.SysPacket.GetItemString();
                        PARAM param = new PARAM();
                        param.PARAMNAME = _cim.SysPacket.GetItemString();
                        param.PARAMVALUE = _cim.SysPacket.GetItemString();
                        cmd.PARAMs.Add(param);
                    }
                    ppid.COMMANDCODEs.Add(cmd);
                }
                EQPDATA data = EqpData;
               
                SendMessage2PLC("FORMATTEDPROCESSPROGRAMSEND", ppid,_plc);
                SendS7F24(ACK);
            }
            catch (Exception ex)
            {
                SendS9F7(_cim.SysPacket);
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
        }
    }
}
