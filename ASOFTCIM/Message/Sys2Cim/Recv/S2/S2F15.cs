
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
        public void RecvS2F15()
        {
            try
            {
                string TEAC = "";
                if (_cim.SysPacket.Items.Count <= 0)
                {
                    SendS9F7(_cim.SysPacket);
                    return;
                }
                List<EC> lst = new List<EC>();
                int count = int.Parse(_cim.SysPacket.Items[2].ToString());
                for (int i = 0; i < count; i++)
                {
                    EC ecm = new EC();
                    ecm.ECID = _cim.SysPacket.Items[4  + i * 7].ToString();
                    ecm.ECDEF = _cim.SysPacket.Items[5 + i * 7].ToString();
                    ecm.ECSLL = _cim.SysPacket.Items[6 + i * 7].ToString();
                    ecm.ECSUL = _cim.SysPacket.Items[7 + i * 7].ToString();
                    ecm.ECWLL = _cim.SysPacket.Items[8 + i * 7].ToString();
                    ecm.ECWUL = _cim.SysPacket.Items[9 + i * 7].ToString();
                    
                    lst.Add(ecm);
                }
                foreach (var item in lst)
                {
                    if (!EqpData.ECS.Any(x=>x.ECID==item.ECID))
                    {
                        TEAC = "1";
                        break;
                    }
                }
                
                if (_cim.SysPacket.Items[1].ToString()!= _cim.EQPID) TEAC = "5";
                if (TEAC!="")
                {
                    SendS2F16( lst, TEAC);
                }
                else
                {
                    //   new ECSETREQUEST(EqpData,cim.EQHelper.Conn,lst);

                }
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
