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
        public void RecvS2F105()
        {
            try
            {
                string HACK = "0";
                string eqpId = _cim.SysPacket.GetItemString(1);
                string pairCellId = _cim.SysPacket.GetItemString();
                string codeType = _cim.SysPacket.GetItemString();
                string replyStatus = _cim.SysPacket.GetItemString();
                string replyText = _cim.SysPacket.GetItemString();
                int count= int.Parse(_cim.SysPacket.GetItemString());
                List<Tuple<string, string, string>> lst = new List<Tuple<string, string, string>>();
                for (int i = 0; i < count; i++)
                {
                    string list = _cim.SysPacket.GetItemString();
                    string judge = _cim.SysPacket.GetItemString();
                    string reasoncode = _cim.SysPacket.GetItemString();
                    string reasonDescription = _cim.SysPacket.GetItemString();
                    lst.Add(new Tuple<string, string, string>(judge, reasoncode, reasonDescription ));
                }

                SendS2F106( HACK);
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
