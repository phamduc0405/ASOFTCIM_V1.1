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
        public void RecvS7F125()
        {
            try
            {
                PPIDINFOR ppidInfor = new PPIDINFOR();
                ppidInfor.EQPID = _cim.SysPacket.GetItemString(1);
                ppidInfor.UNITID = _cim.SysPacket.GetItemString();
                ppidInfor.PPID = _cim.SysPacket.GetItemString();
                ppidInfor.PPID_TYPE = _cim.SysPacket.GetItemString();

                //    new S7F26().SendMessage(ppidInfor);
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
