using ASOFTCIM.Helper;

using ASOFTCIM.Data;

using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    /// <summary>
    /// Formatted Process Program Send
    /// </summary>
    public partial class ACIM
    {
        public void RecvS7F123(SysPacket sysPacket)
        {
            try
            {
                string ACK = "0";
                PPIDINFOR ppid = new PPIDINFOR();
                ppid.EQPID = sysPacket.GetItemString(1);
                string unitId = sysPacket.GetItemString();
                ppid.PPID = sysPacket.GetItemString();
                ppid.PPID_TYPE = sysPacket.GetItemString();
                int count = int.Parse(sysPacket.GetItemString());
                for (int i = 0; i < count; i++)
                {
                    string lst = sysPacket.GetItemString();
                    COMMANDCODE cmd = new COMMANDCODE();
                    cmd.CCODE = sysPacket.GetItemString();
                    int countParams = int.Parse(sysPacket.GetItemString());
                    for (int j = 0; j < countParams; j++)
                    {
                        string lst2 = sysPacket.GetItemString();
                        PARAM param = new PARAM();
                        param.PARAMNAME = sysPacket.GetItemString();
                        param.PARAMVALUE = sysPacket.GetItemString();
                        cmd.PARAMs.Add(param);
                    }
                    ppid.COMMANDCODEs.Add(cmd);
                }
              //  new RECIPECONTROLREQUEST(EqpData,cim.EQHelper.Conn,ppid);
              //  new S7F124().SendMessage( ACK);
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
