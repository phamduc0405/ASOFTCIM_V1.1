using ASOFTCIM.Helper;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Data;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS8F3(SysPacket sysPacket)
        {
            try
            {
                List<INQUIRYFORM> inquiry = new List<INQUIRYFORM>();
                string eqpID = sysPacket.GetItemString(1);
                int count = int.Parse(sysPacket.GetItemString());

                for (int i = 0; i < count; i++)
                {
                    string lst = sysPacket.GetItemString();
                    INQUIRYFORM form = new INQUIRYFORM();
                    form.DATA_TYPE = sysPacket.GetItemString();
                    int items = int.Parse(sysPacket.GetItemString());
                    for (int j = 0; j < items; j++)
                    {
                        ITEM item = new ITEM();
                        item.ITEMNAME = sysPacket.GetItemString();
                        form.ITEMs.Add(item);
                    }
                    inquiry.Add(form);
                }

                //    new S7F26().SendMessage(ppidInfor);
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
