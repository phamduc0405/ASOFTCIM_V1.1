﻿

using A_SOFT.CMM.INIT;
using A_SOFT.Ctl.SecGem;
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
        public void RecvS1F17(SysPacket sysPacket)
        {
            try
            {
                EqpData.TransactionSys = sysPacket.SystemByte;
                string onlack= string.Empty ;
                if (sysPacket.Items[2].Value.ToString()!= _cim.EQPID)
                {
                    onlack = "4";
                }
                else if (sysPacket.Items[3].Value.ToString() == EqpData.EQINFORMATION.CRST)
                {
                    onlack = "0";
                }
                else if (sysPacket.Items[3].Value.ToString() != "1" && sysPacket.Items[3].Value.ToString() != "2")
                {
                    onlack = "6";
                }
                else
                {
                    //if (_cim.EQHelper.IsPlc)
                    //    new CRSTCHANGEPLC(_cim.EQHelper.PLCData, "1");
                    //new CRSTCHANGEREQUEST(EqpData, cim.EQHelper.Conn, cim.SysPacket.Items[3].Value.ToString());
                }
                if (onlack!= string.Empty)
                SendS1F18(onlack);
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
