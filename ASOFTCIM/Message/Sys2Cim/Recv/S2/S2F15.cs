
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
        public void RecvS2F15(SysPacket sysPacket)
        {
            try
            {
                string TEAC = "";
                if (sysPacket.Items.Count <= 0)
                {
                    SendS9F7(sysPacket);
                    return;
                }
                List<EC> lst = new List<EC>();
                int count = int.Parse(sysPacket.Items[2].ToString());
                for (int i = 0; i < count; i++)
                {
                    EC ecm = new EC();
                    ecm.ECID = sysPacket.Items[4  + i * 7].ToString();
                    ecm.ECDEF = sysPacket.Items[5 + i * 7].ToString();
                    ecm.ECSLL = sysPacket.Items[6 + i * 7].ToString();
                    ecm.ECSUL = sysPacket.Items[7 + i * 7].ToString();
                    ecm.ECWLL = sysPacket.Items[8 + i * 7].ToString();
                    ecm.ECWUL = sysPacket.Items[9 + i * 7].ToString();
                    
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
                
                if (sysPacket.Items[1].ToString()!= _cim.EQPID) TEAC = "5";
                if (TEAC!="")
                {
                    SendS2F16( lst, TEAC);
                }
                else
                {
                    //   new ECSETREQUEST(EqpData,cim.EQHelper.Conn,lst);
                    SendMessage2PLC("", lst);// trong map không có => không cho phép thay đổi EC by Host
                }
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
