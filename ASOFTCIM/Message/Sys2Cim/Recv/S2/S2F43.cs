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
        public void RecvS2F43()
        {
            try
            {
                string HACK = "0";
                string RCMD = _cim.SysPacket.GetItemString(1);
                if (RCMD == "21" || RCMD == "22" || RCMD == "23" || RCMD == "24")
                {
                    CellJobProcess jobProcess = new CellJobProcess();
                    jobProcess.RCMD = RCMD;
                    jobProcess.JOBID = _cim.SysPacket.GetItemString(5);
                    jobProcess.CELLID = _cim.SysPacket.GetItemString(8);
                    jobProcess.PRODUCTID = _cim.SysPacket.GetItemString(11);
                    jobProcess.STEPID = _cim.SysPacket.GetItemString(14);
                    jobProcess.ACTIONTYPE = _cim.SysPacket.GetItemString(17);
                    jobProcess.EQPID = _cim.EQPID;
                }
                else if (RCMD == "31" || RCMD == "32" )
                {
                    ApproveProcess jobProcess = new ApproveProcess();
                    jobProcess.RCMD = RCMD;
                    jobProcess.APPROVECODE = _cim.SysPacket.GetItemString(6);
                    jobProcess.APPROVEINFO = _cim.SysPacket.GetItemString(9);
                    jobProcess.APPROVEID = _cim.SysPacket.GetItemString(12);
                    jobProcess.BYWHO = _cim.SysPacket.GetItemString(15);
                    jobProcess.APPROVETEXT = _cim.SysPacket.GetItemString(18);
                }
                else
                {
                    HACK = "1";
                }
                SendS2F44( RCMD, HACK);
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
