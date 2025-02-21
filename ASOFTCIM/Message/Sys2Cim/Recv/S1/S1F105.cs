using ASOFTCIM;
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
        public void RecvS1F105()
        {
            try
            {
                EqpData.TransactionSys = _cim.SysPacket.SystemByte;
                if (_cim.SysPacket.Items.Count > 0)
                {
                    List<string> list = new List<string>();
                    if (_cim.SysPacket.Items[1].ToString() == "2")
                    {
                        int count = int.Parse(_cim.SysPacket.Items[5].ToString());
                        for (int i = 0; i < count; i++)
                        {
                            list.Add(_cim.SysPacket.Items[6+i].ToString());
                        }
                        SendS1F106_2(list);
                    }
                    if (_cim.SysPacket.Items[1].ToString() == "3")
                    {
                        int count = int.Parse(_cim.SysPacket.Items[5].ToString());
                        for (int i = 0; i < count; i++)
                        {
                            list.Add(_cim.SysPacket.Items[6 + i].ToString());
                        }
                        SendS1F106_3( list);
                    }
                    if (_cim.SysPacket.Items[1].ToString() == "4")
                    {
                        int count = int.Parse(_cim.SysPacket.Items[5].ToString());
                        for (int i = 0; i < count; i++)
                        {
                            list.Add(_cim.SysPacket.Items[6 + i].ToString());
                        }
                        SendS1F106_4( list);
                    }
                }
                else
                    SendS9F7(_cim.SysPacket);
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
