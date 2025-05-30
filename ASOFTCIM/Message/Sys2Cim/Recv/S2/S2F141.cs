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
        public void RecvS2F141(SysPacket sysPacket)
        {
            try
            {
                string HACK = "0";string unitId = "";
                string RCMD = sysPacket.GetItemString(1);
                if (RCMD == "4" || RCMD == "5" || RCMD == "6" || RCMD == "7" || RCMD == "8")
                {
                    JobProcess jobProcess = new JobProcess();
                    jobProcess.RCMD = RCMD;
                    jobProcess.PARENTLOT = sysPacket.GetItemString(4);
                    jobProcess.RFID = sysPacket.GetItemString();
                    jobProcess.EQPID = sysPacket.GetItemString();
                    jobProcess.UNITID = sysPacket.GetItemString();
                    jobProcess.PORTNO = sysPacket.GetItemString();
                    jobProcess.PPID = sysPacket.GetItemString();
                    jobProcess.CELLCNT = sysPacket.GetItemString();
                    jobProcess.MESSAGE = sysPacket.GetItemString();

                    unitId = jobProcess.UNITID;
                }
                switch (RCMD)
                {
                    case "1":   //Equipment Command (Eqp Op-call Send)
                        OPCALLMESS opcall = new OPCALLMESS();
                        opcall.OPCALL = sysPacket.GetItemString(4);
                        opcall.EQPID = sysPacket.GetItemString();
                        opcall.UNITID = sysPacket.GetItemString();
                        opcall.OPCALLID = sysPacket.GetItemString();
                        opcall.MESSAGE = sysPacket.GetItemString();

                        unitId = opcall.UNITID;
                        break;
                    case "2":   //Equipment Command (Eqp Interlock Send)
                        INTERLOCKMESS interlock = new INTERLOCKMESS();
                        interlock.INTERLOCK = sysPacket.GetItemString(4);
                        interlock.EQPID = sysPacket.GetItemString();
                        interlock.UNITID = sysPacket.GetItemString();
                        interlock.INTERLOCKID = sysPacket.GetItemString();
                        interlock.MESSAGE = sysPacket.GetItemString();

                        unitId = interlock.UNITID;
                        break;
                    case "3":   //Equipment Job Command (Job(=PPID) Select))
                        JobProcess jobSelect = new JobProcess();
                        jobSelect.RCMD = "3";
                        jobSelect.PPID = sysPacket.GetItemString(4);
                        jobSelect.EQPID = sysPacket.GetItemString();
                        jobSelect.UNITID = sysPacket.GetItemString();
                        jobSelect.PORTNO = sysPacket.GetItemString();
                        jobSelect.MESSAGE = sysPacket.GetItemString();

                        unitId = jobSelect.UNITID;
                        break;
                    case "4":   //(Job Process Start)

                        break;
                    case "5":   //(Job Process Abort)

                        break;
                    case "6":   //(Job Process Pause)

                        break;
                    case "7":   //(Job Process Resume)
                        break;
                    case "8":   //(Job Process Cancel)
                        break;
                    default:
                        break;
                }
                SendS2F142( RCMD,unitId, HACK);
            }
            catch (Exception ex)
            {
                SendS9F7(sysPacket);
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    }
}
