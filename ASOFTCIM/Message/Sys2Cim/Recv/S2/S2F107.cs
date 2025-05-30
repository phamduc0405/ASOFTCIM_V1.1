
using ASOFTCIM.Data;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Helper;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS2F107(SysPacket sysPacket)
        {
            try
            {
                string HACK = "0";
                string eqpId = sysPacket.GetItemString(1);
                BATCHLOT batchlot = new BATCHLOT();
                batchlot.PRODUCTID = sysPacket.GetItemString();
                batchlot.BATCHLOTID = sysPacket.GetItemString();
                batchlot.BATCHLOTQTY = sysPacket.GetItemString();
                batchlot.REASONCODE = sysPacket.GetItemString();
                batchlot.DESCRIPTION = sysPacket.GetItemString();

                //if (_cim.EQHelper.IsPlc)
               //     new BATCHLOTINFORMATIONDOWNLOAD(_cim.EQHelper.PLCData, batchlot);
                SendS2F108( HACK);
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
