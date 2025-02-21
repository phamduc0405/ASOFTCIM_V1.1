using A_SOFT.CMM.INIT;
using ASOFTCIM.Helper;
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
        public void RecvS1F11()
        {
            try
            {
                string eqpId = _cim.SysPacket.GetItemString(1);
                int count = int.Parse(_cim.SysPacket.GetItemString());
                if (eqpId != _cim.EQPID)
                {
                    SendS9F1(_cim.SysPacket);
                    return;
                }
                List<string> lstSvid = new List<string>();
                for (int i = 0; i < count; i++)
                {
                    lstSvid.Add(_cim.SysPacket.GetItemString());
                }
                SendS1F12( lstSvid);
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
