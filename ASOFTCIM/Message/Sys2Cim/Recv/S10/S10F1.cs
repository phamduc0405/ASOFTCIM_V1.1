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
        public void RecvS10F1()
        {
            try
            {
                string ACK = "0";
                TERMINAL terminal = new TERMINAL();
                terminal.EQPID = _cim.SysPacket.GetItemString(1);
                terminal.TID = _cim.SysPacket.GetItemString();
                terminal.TEXT.Add(_cim.SysPacket.GetItemString());
               // new TERMINALDISPLAYREQUEST(EqpData, cim.EQHelper.Conn, terminal);
                SendS10F2(ACK);
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
