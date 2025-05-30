﻿using ASOFTCIM.Helper;

using ASOFTCIM.Data;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Message.PLC2Cim.Send;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS2F103(SysPacket sysPacket)
        {
            try
            {
                string HACK = "0";
                BODYCellInfor cellInfor = new BODYCellInfor();
                cellInfor.CELLID = sysPacket.GetItemString(2);
                cellInfor.PRODUCTID = sysPacket.GetItemString();
                cellInfor.CELLINFORESULT = sysPacket.GetItemString();

                SendMessage2PLC("CELLINFORMATIONDOWNLOAD1",cellInfor);
                SendS2F104( HACK);
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
