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
        public void RecvS2F109()
        {
            try
            {
                string HACK = "0";
                string eqpId = _cim.SysPacket.GetItemString(1);
                string productId = _cim.SysPacket.GetItemString();
                string slipId = _cim.SysPacket.GetItemString();
                string item_1Qty = _cim.SysPacket.GetItemString();
                int countItem1 = int.Parse(_cim.SysPacket.GetItemString());
                List<string> lstItem1 = new List<string>();
                for (int i = 0; i < countItem1; i++)
                {
                    string item1Value = _cim.SysPacket.GetItemString();
                    lstItem1.Add(item1Value);
                }
                string item_2Qty = _cim.SysPacket.GetItemString();
                int countItem2 = int.Parse(_cim.SysPacket.GetItemString());
                List<string> lstItem2 = new List<string>();
                for (int i = 0; i < countItem1; i++)
                {
                    string item2Value = _cim.SysPacket.GetItemString();
                    lstItem1.Add(item2Value);
                }
                string replyStatus = _cim.SysPacket.GetItemString();
                string replyCode = _cim.SysPacket.GetItemString();
                string replyText = _cim.SysPacket.GetItemString();


                SendS2F110( HACK);
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
