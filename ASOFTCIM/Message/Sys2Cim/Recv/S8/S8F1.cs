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
        public void RecvS8F1()
        {
            try
            {
                INQUIRY inquiry = new INQUIRY();
                inquiry.EQPID = _cim.SysPacket.GetItemString(1);
                inquiry.PRODUCTID = _cim.SysPacket.GetItemString();
                inquiry.ACTIONFLAG = _cim.SysPacket.GetItemString();
                int count = int.Parse(_cim.SysPacket.GetItemString());
                for (int i = 0; i < count; i++)
                {
                    string lst = _cim.SysPacket.GetItemString();
                    INQUIRYDATA data = new INQUIRYDATA();
                    data.DATA_TYPE = _cim.SysPacket.GetItemString();
                    data.ITEMNAME = _cim.SysPacket.GetItemString();
                    data.ITEMVALUE = _cim.SysPacket.GetItemString();
                    data.CHECKSUM = _cim.SysPacket.GetItemString();
                    data.REFERENCE = _cim.SysPacket.GetItemString();
                    inquiry.INQUIRYDATA.Add(data);
                }

              //  new MULTIUSEDATASETREQUEST(EqpData,cim.EQHelper.Conn,inquiry);
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
