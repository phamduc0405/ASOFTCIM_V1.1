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
        public void RecvS8F1(SysPacket sysPacket)
        {
            try
            {
                INQUIRY inquiry = new INQUIRY();
                inquiry.EQPID = sysPacket.GetItemString(1);
                inquiry.PRODUCTID = sysPacket.GetItemString();
                inquiry.ACTIONFLAG = sysPacket.GetItemString();
                int count = int.Parse(sysPacket.GetItemString());
                for (int i = 0; i < count; i++)
                {
                    string lst = sysPacket.GetItemString();
                    INQUIRYDATA data = new INQUIRYDATA();
                    data.DATA_TYPE = sysPacket.GetItemString();
                    data.ITEMNAME = sysPacket.GetItemString();
                    data.ITEMVALUE = sysPacket.GetItemString();
                    data.CHECKSUM = sysPacket.GetItemString();
                    data.REFERENCE = sysPacket.GetItemString();
                    inquiry.INQUIRYDATA.Add(data);
                }

              //  new MULTIUSEDATASETREQUEST(EqpData,cim.EQHelper.Conn,inquiry);
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
