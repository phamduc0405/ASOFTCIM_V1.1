using ASOFTCIM.Helper;
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
        public void RecvS16F101()
        {
            try
            {
                ReadAPC();
                string eqpid = _cim.SysPacket.GetItemString();
                EqpData.PROCESSDATACONTROL.TMACK = "0";
                if (eqpid != EqpData.EQINFORMATION.EQPID)
                {
                    EqpData.PROCESSDATACONTROL.TMACK = "13";
                }    
                SendS16F102(EqpData.PROCESSDATACONTROL);
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
