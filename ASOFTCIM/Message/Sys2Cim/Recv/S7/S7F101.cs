﻿using A_SOFT.CMM.INIT;
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
        public void RecvS7F101()
        {
            try
            {
                string eqpid = _cim.SysPacket.GetItemString(1);
                string ppid_type = _cim.SysPacket.GetItemString();
                this.EqpData.PPIDList.PPID_TYPE = _cim.SysPacket.GetItemString(2);
                // new S7F102().SendMessage(mes);
                SendS7F102(this.EqpData.PPIDList);
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
