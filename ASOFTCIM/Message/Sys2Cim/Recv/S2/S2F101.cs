using ASOFTCIM.Helper;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Data;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS2F101(SysPacket sysPacket)
        {
            try
            {
                string HACK = "0"; string jobid = "";

                string RCMD = sysPacket.GetItemString(1);
                if (RCMD == "4" || RCMD == "5" || RCMD == "6" || RCMD == "7" || RCMD == "8" || RCMD == "9")
                {
                    TrsProcess jobProcess = new TrsProcess();
                    jobProcess.RCMD = RCMD;
                    jobProcess.PORTID = sysPacket.GetItemString(5);
                    jobProcess.TRSNAME = sysPacket.GetItemString();
                    jobProcess.JOBID = sysPacket.GetItemString();
                    jobProcess.JOBTYPE = sysPacket.GetItemString();
                    jobProcess.PRODUCTID = sysPacket.GetItemString();
                    jobProcess.STEPID = sysPacket.GetItemString();
                    jobProcess.SOURCELOC = sysPacket.GetItemString();
                    jobProcess.SOURCEPORTID = sysPacket.GetItemString();
                    jobProcess.FINALLOC = sysPacket.GetItemString();
                    jobProcess.FINALPORTID = sysPacket.GetItemString();
                    jobProcess.MIDLOC = sysPacket.GetItemString();
                    jobProcess.MIDPORTID = sysPacket.GetItemString();
                    jobProcess.ORIGINLOC = sysPacket.GetItemString();
                    jobProcess.PRIORITY = sysPacket.GetItemString();
                    jobProcess.DESCRIPTION = sysPacket.GetItemString();
                }
                else if (RCMD == "1" || RCMD == "2" || RCMD == "3")
                {

                }
                else
                {
                    HACK = "1";
                }
                SendS2F102( HACK);
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
