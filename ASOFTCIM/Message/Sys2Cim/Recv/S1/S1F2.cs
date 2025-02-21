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
        public void RecvS1F2()
        {
            try
            {
                EqpData.TransactionSys = _cim.SysPacket.SystemByte;
                if (_cim.SysPacket.Items[1].Value.ToString().Trim() != _cim.EQPID)
                {
                    SendS2F17(_cim.SysPacket);
                }
                else
                {
                    SendS9F1(_cim.SysPacket);
                }
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
