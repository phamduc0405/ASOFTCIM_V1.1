using ASOFTCIM.Helper;
using ASOFTCIM.Data;
using A_SOFT.CMM.INIT;
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
        public void RecvS2F141()
        {
            try
            {
                string HACK = "0";string unitId = "";
                string RCMD = _cim.SysPacket.GetItemString(1);
                if (RCMD == "4" || RCMD == "5" || RCMD == "6" || RCMD == "7" || RCMD == "8")
                {
                    JobProcess jobProcess = new JobProcess();
                    jobProcess.RCMD = RCMD;
                    jobProcess.PARENTLOT = _cim.SysPacket.GetItemString(4);
                    jobProcess.RFID = _cim.SysPacket.GetItemString();
                    jobProcess.EQPID = _cim.SysPacket.GetItemString();
                    jobProcess.UNITID = _cim.SysPacket.GetItemString();
                    jobProcess.PORTNO = _cim.SysPacket.GetItemString();
                    jobProcess.PPID = _cim.SysPacket.GetItemString();
                    jobProcess.CELLCNT = _cim.SysPacket.GetItemString();
                    jobProcess.MESSAGE = _cim.SysPacket.GetItemString();

                    unitId = jobProcess.UNITID;
                }
                switch (RCMD)
                {
                    case "1":   //Equipment Command (Eqp Op-call Send)
                        OPCALLMESS opcall = new OPCALLMESS();
                        opcall.OPCALL = _cim.SysPacket.GetItemString(4);
                        opcall.EQPID = _cim.SysPacket.GetItemString();
                        opcall.UNITID = _cim.SysPacket.GetItemString();
                        opcall.OPCALLID = _cim.SysPacket.GetItemString();
                        opcall.MESSAGE = _cim.SysPacket.GetItemString();

                        unitId = opcall.UNITID;
                        break;
                    case "2":   //Equipment Command (Eqp Interlock Send)
                        INTERLOCKMESS interlock = new INTERLOCKMESS();
                        interlock.INTERLOCK = _cim.SysPacket.GetItemString(4);
                        interlock.EQPID = _cim.SysPacket.GetItemString();
                        interlock.UNITID = _cim.SysPacket.GetItemString();
                        interlock.INTERLOCKID = _cim.SysPacket.GetItemString();
                        interlock.MESSAGE = _cim.SysPacket.GetItemString();

                        unitId = interlock.UNITID;
                        break;
                    case "3":   //Equipment Job Command (Job(=PPID) Select))
                        JobProcess jobSelect = new JobProcess();
                        jobSelect.RCMD = "3";
                        jobSelect.PPID = _cim.SysPacket.GetItemString(4);
                        jobSelect.EQPID = _cim.SysPacket.GetItemString();
                        jobSelect.UNITID = _cim.SysPacket.GetItemString();
                        jobSelect.PORTNO = _cim.SysPacket.GetItemString();
                        jobSelect.MESSAGE = _cim.SysPacket.GetItemString();

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
                SendS9F7(_cim.SysPacket);
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    }
}
