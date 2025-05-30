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
        public void RecvS2F13(SysPacket sysPacket)
        {
            try
            {
                List<string> lst = new List<string>();
                string eqp = sysPacket.GetItemString(1);
                if (eqp!= _cim.EQPID)
                {
                    //SendS9F1(sysPacket);
                    lst.Add("EQPID");
                    SendS2F14(lst);
                    return;
                }
                if (sysPacket.Items.Count <= 0)
                {
                    SendS9F7(sysPacket);
                    return;
                }
                
                
                int count = int.Parse(sysPacket.Items[2].ToString());
                for (int i = 0; i < count; i++)
                {
                    lst.Add(sysPacket.Items[3 + i].ToString());
                }

                SendS2F14(lst);
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
