using ASOFTCIM.Helper;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Data;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS8F3()
        {
            try
            {
                List<INQUIRYFORM> inquiry = new List<INQUIRYFORM>();
                string eqpID = _cim.SysPacket.GetItemString(1);
                int count = int.Parse(_cim.SysPacket.GetItemString());

                for (int i = 0; i < count; i++)
                {
                    string lst = _cim.SysPacket.GetItemString();
                    INQUIRYFORM form = new INQUIRYFORM();
                    form.DATA_TYPE = _cim.SysPacket.GetItemString();
                    int items = int.Parse(_cim.SysPacket.GetItemString());
                    for (int j = 0; j < items; j++)
                    {
                        ITEM item = new ITEM();
                        item.ITEMNAME = _cim.SysPacket.GetItemString();
                        form.ITEMs.Add(item);
                    }
                    inquiry.Add(form);
                }

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
