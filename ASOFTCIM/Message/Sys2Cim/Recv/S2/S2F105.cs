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
        public void RecvS2F105(SysPacket sysPacket)
        {
            try
            {
                string HACK = "0";
                string eqpId = sysPacket.GetItemString(1);
                string pairCellId = sysPacket.GetItemString();
                string codeType = sysPacket.GetItemString();
                string replyStatus = sysPacket.GetItemString();
                string replyText = sysPacket.GetItemString();
                int count= int.Parse(sysPacket.GetItemString());
                List<Tuple<string, string, string>> lst = new List<Tuple<string, string, string>>();
                for (int i = 0; i < count; i++)
                {
                    string list = sysPacket.GetItemString();
                    string judge = sysPacket.GetItemString();
                    string reasoncode = sysPacket.GetItemString();
                    string reasonDescription = sysPacket.GetItemString();
                    lst.Add(new Tuple<string, string, string>(judge, reasoncode, reasonDescription ));
                }

                SendS2F106( HACK);
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
