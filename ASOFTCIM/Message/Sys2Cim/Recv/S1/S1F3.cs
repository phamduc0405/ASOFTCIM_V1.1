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
        public void RecvS1F3(SysPacket sysPacket)
        {
            try
            {
                
                if (sysPacket.Items.Count > 0)
                {
                    string eqpId = sysPacket.GetItemString(1);
                    int count = int.Parse(sysPacket.GetItemString());
                    List<string> lstSvid = new List<string>();
                    if (eqpId != _cim.EQPID)
                    {
                        //SendS9F1(sysPacket);
                        lstSvid.Add("EQPID");
                        SendS1F4(lstSvid);
                        return;
                    }
                    
                    for (int i = 0; i < count; i++)
                    {
                        lstSvid.Add(sysPacket.GetItemString());
                    }

                    SendS1F4( lstSvid);
                }
                else
                SendS9F7(sysPacket);
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
