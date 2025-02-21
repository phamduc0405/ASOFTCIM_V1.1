using A_SOFT.CMM.INIT;
using ASOFTCIM.Helper;
using ASOFTCIM.Data;
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
        public void RecvS7F23()
        {
            try
            {
                string ACK = "0";
                PPIDINFOR ppid = new PPIDINFOR();
                ppid.EQPID = _cim.SysPacket.GetItemString(1);
                ppid.PPID = _cim.SysPacket.GetItemString();
                ppid.PPID_TYPE = _cim.SysPacket.GetItemString();
                int count = int.Parse(_cim.SysPacket.GetItemString());
                for (int i = 0; i < count; i++)
                {
                    string lst = _cim.SysPacket.GetItemString();
                    COMMANDCODE cmd = new COMMANDCODE();
                    cmd.CCODE = _cim.SysPacket.GetItemString();
                    int countParams = int.Parse(_cim.SysPacket.GetItemString());
                    for (int j = 0; j < countParams; j++)
                    {
                        string lst2 = _cim.SysPacket.GetItemString();
                        PARAM param = new PARAM();
                        param.PARAMNAME = _cim.SysPacket.GetItemString();
                        param.PARAMVALUE = _cim.SysPacket.GetItemString();
                        cmd.PARAMs.Add(param);
                    }
                    ppid.COMMANDCODEs.Add(cmd);
                }
                EQPDATA data = EqpData;
               


                SendS7F24(ACK);
              //  new RECIPECONTROLREQUEST(EqpData, cim.EQHelper.Conn, ppid);
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
