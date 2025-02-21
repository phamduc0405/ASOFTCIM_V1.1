
using A_SOFT.CMM.INIT;
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
        public void RecvS1F15()
        {
            try
            {
                EqpData.TransactionSys = _cim.SysPacket.SystemByte;
                string offlack = string.Empty;
                if (_cim.SysPacket.Items[2].Value.ToString() != _cim.EQPID)
                {
                    offlack = "1";
                }
                else if (_cim.SysPacket.Items[3].Value.ToString() == EqpData.EQINFORMATION.CRST)
                {
                    offlack = "0";
                }
                else if (_cim.SysPacket.Items[3].Value.ToString() != "0" )
                {
                    offlack = "3";
                }
                else
                {
                    //if (_cim.EQHelper.IsPlc)
                    //    new CRSTCHANGEPLC(_cim.EQHelper.PLCData, "0");
                    //new CRSTCHANGEREQUEST(EqpData, cim.EQHelper.Conn, cim.SysPacket.Items[3].Value.ToString());
                }
                if (offlack != string.Empty)
                    SendS1F16( offlack);
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
