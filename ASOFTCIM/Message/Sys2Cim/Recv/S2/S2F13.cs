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
        public void RecvS2F13()
        {
            try
            {
                string eqp = _cim.SysPacket.GetItemString(1);
                if (eqp!= _cim.EQPID)
                {
                    SendS9F1(_cim.SysPacket);
                    return;
                }
                if (_cim.SysPacket.Items.Count <= 0)
                {
                    SendS9F7(_cim.SysPacket);
                    return;
                }
                
                List<string> lst = new List<string>();
                int count = int.Parse(_cim.SysPacket.Items[2].ToString());
                for (int i = 0; i < count; i++)
                {
                    lst.Add(_cim.SysPacket.Items[3 + i].ToString());
                }

                SendS2F14(lst);
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
