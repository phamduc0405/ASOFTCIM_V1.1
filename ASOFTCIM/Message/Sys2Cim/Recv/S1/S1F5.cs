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
        public void RecvS1F5()
        {
            if (_cim.SysPacket.Items.Count > 0)
            {
                EqpData.TransactionSys = _cim.SysPacket.SystemByte;
                try
                {
                    if (_cim.SysPacket.Items[3].Value.ToString() == _cim.EQPID)
                    {

                        switch (_cim.SysPacket.Items[1].Value.ToString())
                        {
                            case "1":
                                SendS1F6_1(_cim.Conn,EqpData);
                                break;
                            case "2":
                                SendS1F6_2(_cim.Conn, EqpData);
                                break;
                            case "3":
                                SendS1F6_3(_cim.Conn, EqpData);
                                break;
                            case "4":
                                SendS1F6_4(_cim.Conn, EqpData);
                                break;
                            case "5":
                                SendS1F6_5(_cim.Conn, EqpData);
                                break;
                            case "6":
                              //  new S1F6_6().SendMessage(_cim.Conn, EqpData);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                        SendS9F1(_cim.SysPacket);
                }
                catch (Exception ex)
                {
                    SendS9F7(_cim.SysPacket);
                    var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                    LogTxt.Add(LogTxt.Type.Exception, debug);
                }
                return;
            }
                SendS9F7(_cim.SysPacket);
        }
    }
}
