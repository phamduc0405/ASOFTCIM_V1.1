using ASOFTCIM.Helper;
using ASOFTCIM.Data;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Message.PLC2Cim.Send;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS2F43(SysPacket sysPacket)
        {
            try
            {
                string HACK = "0";
                string RCMD = sysPacket.GetItemString(1);
                if (RCMD == "21" || RCMD == "22" || RCMD == "23" || RCMD == "24")
                {
                    CellJobProcess jobProcess = new CellJobProcess();
                    jobProcess.RCMD = RCMD;
                    jobProcess.JOBID = sysPacket.GetItemString(5);
                    jobProcess.CELLID = sysPacket.GetItemString(8);
                    jobProcess.PRODUCTID = sysPacket.GetItemString(11);
                    jobProcess.STEPID = sysPacket.GetItemString(14);
                    jobProcess.ACTIONTYPE = sysPacket.GetItemString(17);
                    jobProcess.EQPID = _cim.EQPID;
                    if(jobProcess.JOBID == "1")
                    {
                        SendMessage2PLC("CELLJOBPROCESS1", jobProcess);//DUNG CHO PORT 1

                    }
                    if (jobProcess.JOBID == "2")
                    {
                        SendMessage2PLC("CELLJOBPROCESS2", jobProcess);// DUNG CHO PORT 2

                    }

                }
                else if (RCMD == "31" || RCMD == "32" )
                {
                    ApproveProcess jobProcess = new ApproveProcess();
                    jobProcess.RCMD = RCMD;
                    jobProcess.APPROVECODE = sysPacket.GetItemString(6);
                    jobProcess.APPROVEINFO = sysPacket.GetItemString(9);
                    jobProcess.APPROVEID = sysPacket.GetItemString(12);
                    jobProcess.BYWHO = sysPacket.GetItemString(15);
                    jobProcess.APPROVETEXT = sysPacket.GetItemString(18);
                    SendMessage2PLC("EQUIPMENTAPPROVEPROCESS", jobProcess);
                }
                else
                {
                    HACK = "1";
                }
                

                SendS2F44( RCMD, HACK);
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
