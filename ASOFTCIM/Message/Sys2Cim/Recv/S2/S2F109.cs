using A_SOFT.CMM.INIT;
using A_SOFT.Ctl.SecGem;
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
        public void RecvS2F109(SysPacket sysPacket)
        {
            try
            {
                string HACK = "0";
                string eqpId = sysPacket.GetItemString(1);
                string productId = sysPacket.GetItemString();
                string slipId = sysPacket.GetItemString();
                string item_1Qty = sysPacket.GetItemString();
                int countItem1 = int.Parse(sysPacket.GetItemString());
                List<string> lstItem1 = new List<string>();
                for (int i = 0; i < countItem1; i++)
                {
                    string item1Value = sysPacket.GetItemString();
                    lstItem1.Add(item1Value);
                }
                string item_2Qty = sysPacket.GetItemString();
                int countItem2 = int.Parse(sysPacket.GetItemString());
                List<string> lstItem2 = new List<string>();
                for (int i = 0; i < countItem1; i++)
                {
                    string item2Value = sysPacket.GetItemString();
                    lstItem1.Add(item2Value);
                }
                string replyStatus = sysPacket.GetItemString();
                string replyCode = sysPacket.GetItemString();
                string replyText = sysPacket.GetItemString();


                SendS2F110( HACK);
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
