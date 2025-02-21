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
        public void RecvS3F111()
        {
            try
            {
                string ACK = "0";
                string eqp = _cim.SysPacket.GetItemString(1);
                string optionCode = _cim.SysPacket.GetItemString();
                string lst = _cim.SysPacket.GetItemString();
                LABELINFODOWNLOAD labelInfor = new LABELINFODOWNLOAD();

                labelInfor.CELLID = _cim.SysPacket.GetItemString();
                labelInfor.PRODUCTID = _cim.SysPacket.GetItemString();
                labelInfor.LABELID = _cim.SysPacket.GetItemString();
                labelInfor.REPLYSTATUS = _cim.SysPacket.GetItemString();
                labelInfor.REPLYCODE = _cim.SysPacket.GetItemString();
                labelInfor.REPLYTEXT = _cim.SysPacket.GetItemString();

                SendS3F112( ACK);
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
