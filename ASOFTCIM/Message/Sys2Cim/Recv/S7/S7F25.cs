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
using System.Threading;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS7F25(SysPacket sysPacket)
        {
            try
            {
                ReadRMS();
                PPIDINFOR ppidInfor = new PPIDINFOR();
                ppidInfor.EQPID = sysPacket.GetItemString(1);
                ppidInfor.PPID = sysPacket.GetItemString();
                ppidInfor.PPID_TYPE = sysPacket.GetItemString();
                if (ppidInfor.EQPID != EqpData.EQINFORMATION.EQPID)
                {
                    SendS7F26(null);
                    return;
                }
                if (ppidInfor.PPID_TYPE != "1")
                {
                    SendS7F26(null);
                    return;
                }
                SendMessage2PLC("FORMATTEDPROCESSPROGRAMREQUEST", ppidInfor);
                Thread.Sleep(500);
                //var v = _plcH.PPIDParams.FirstOrDefault(x => x.Item == "PPID").GetValue(_plc);
                var v2 = _plcH.PPIDParams.FirstOrDefault(x => x.Item == "TRAY_TYPE").GetValue(_plc);

                PPIDINFOR ppidInfornew = new PPIDINFOR();
                ppidInfornew = EqpData.CurrPPID;
                ppidInfornew.COMMANDCODEs[0].PARAMs[2].PARAMNAME = "TRAY_TYPE";//đang fix cứng theo dự án. Cần sửa lại cho trường hợp tổng quát 
                ppidInfornew.COMMANDCODEs[0].PARAMs[2].PARAMVALUE = v2;
                ppidInfornew.PPID = ppidInfor.PPID;
                ppidInfornew.PPID_TYPE = ppidInfor.PPID_TYPE;

                SendS7F26(ppidInfornew);
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
