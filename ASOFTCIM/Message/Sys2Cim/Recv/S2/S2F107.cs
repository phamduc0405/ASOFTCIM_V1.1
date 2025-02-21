
using ASOFTCIM.Data;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Helper;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS2F107()
        {
            try
            {
                string HACK = "0";
                string eqpId = _cim.SysPacket.GetItemString(1);
                BATCHLOT batchlot = new BATCHLOT();
                batchlot.PRODUCTID = _cim.SysPacket.GetItemString();
                batchlot.BATCHLOTID = _cim.SysPacket.GetItemString();
                batchlot.BATCHLOTQTY = _cim.SysPacket.GetItemString();
                batchlot.REASONCODE = _cim.SysPacket.GetItemString();
                batchlot.DESCRIPTION = _cim.SysPacket.GetItemString();

                //if (_cim.EQHelper.IsPlc)
               //     new BATCHLOTINFORMATIONDOWNLOAD(_cim.EQHelper.PLCData, batchlot);
                SendS2F108( HACK);
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
