using ASOFTCIM.Helper;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS3F107(SysPacket sysPacket)
        {
            try
            {
                string ACK = "0";
                string eqp = sysPacket.GetItemString(1);
                string productID = sysPacket.GetItemString();
                string slipId = sysPacket.GetItemString();
                string item_1Qty = sysPacket.GetItemString();
                string item_2Qty = sysPacket.GetItemString(); ;
                string descrip = sysPacket.GetItemString();

                SendS3F108( ACK);
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
