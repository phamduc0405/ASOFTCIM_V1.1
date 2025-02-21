using ASOFTCIM.Helper;

using ASOFTCIM.Data;
using A_SOFT.CMM.INIT;
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
        public void RecvS2F103()
        {
            try
            {
                string HACK = "0";
                BODYCellInfor cellInfor = new BODYCellInfor();
                cellInfor.CELLID = _cim.SysPacket.GetItemString(2);
                cellInfor.PRODUCTID = _cim.SysPacket.GetItemString();
                cellInfor.CELLINFORESULT = _cim.SysPacket.GetItemString();

               // new CELLINFORMATIONDOWNLOAD(EqpData, cim.EQHelper.Conn, cellInfor);
                SendS2F104( HACK);
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
