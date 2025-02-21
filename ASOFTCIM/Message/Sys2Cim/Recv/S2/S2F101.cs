using ASOFTCIM.Helper;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Data;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS2F101()
        {
            try
            {
                string HACK = "0"; string jobid = "";

                string RCMD = _cim.SysPacket.GetItemString(1);
                if (RCMD == "4" || RCMD == "5" || RCMD == "6" || RCMD == "7" || RCMD == "8" || RCMD == "9")
                {
                    TrsProcess jobProcess = new TrsProcess();
                    jobProcess.RCMD = RCMD;
                    jobProcess.PORTID = _cim.SysPacket.GetItemString(5);
                    jobProcess.TRSNAME = _cim.SysPacket.GetItemString();
                    jobProcess.JOBID = _cim.SysPacket.GetItemString();
                    jobProcess.JOBTYPE = _cim.SysPacket.GetItemString();
                    jobProcess.PRODUCTID = _cim.SysPacket.GetItemString();
                    jobProcess.STEPID = _cim.SysPacket.GetItemString();
                    jobProcess.SOURCELOC = _cim.SysPacket.GetItemString();
                    jobProcess.SOURCEPORTID = _cim.SysPacket.GetItemString();
                    jobProcess.FINALLOC = _cim.SysPacket.GetItemString();
                    jobProcess.FINALPORTID = _cim.SysPacket.GetItemString();
                    jobProcess.MIDLOC = _cim.SysPacket.GetItemString();
                    jobProcess.MIDPORTID = _cim.SysPacket.GetItemString();
                    jobProcess.ORIGINLOC = _cim.SysPacket.GetItemString();
                    jobProcess.PRIORITY = _cim.SysPacket.GetItemString();
                    jobProcess.DESCRIPTION = _cim.SysPacket.GetItemString();
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
                SendS9F7(_cim.SysPacket);
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    }
}
