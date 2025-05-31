
using A_SOFT.Cim;
using A_SOFT.CMM.INIT;
using A_SOFT.Ctl.SecGem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASOFTCIM.Helper
{
    public class CimHelper : SECHelper
    {

        #region Event
        public delegate void SysPacketEventDelegate(SysPacket sysPacket);
        public event SysPacketEventDelegate SysPacketEvent;

        #endregion
        private static readonly object _transWaitsLock = new object();
        public CimHelper(string eqpid) : base(eqpid)
        {
            
        }
        

        public override void SysData_SelectFunctionEvent(SysPacket sysPacket)
        {
            try
            {
                string a = sysPacket.MakeCimLog();
                LogTxt.Add(LogTxt.Type.PCCimMess, a);
                if (sysPacket.Function % 2 == 0)
                {
                    if (IsUseTimeOut)

                    {
                        if (!SysDatas.TransWaits.ContainsKey(sysPacket.SystemByte))
                        {
                            return;
                        }
                        else
                        {
                            SysDatas.TransWaits.TryRemove(sysPacket.SystemByte, out _);
                        }
                    }
                }
                SysPacket = sysPacket;
                
                SysDatas.TransactionSys = sysPacket.SystemByte;
                SysConfig.DeviceId = sysPacket.DeviceId;
                ReciveEventHandle(SysPacket);

                if (EQPID != "System")
                {
                    Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "ASOFTCIM");
                    Type t = Assembly.GetExecutingAssembly()
                        .GetType("ASOFTCIM.ACIM");

                    if (typelist.Contains(t))
                    {
                        SysPacketEventHandle(sysPacket);
                        return;
                    }
                    if (t == null) return;
                }
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.",
                                          this.GetType().Name,
                                          MethodBase.GetCurrentMethod().Name,
                                          ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
                ex.Data.Clear();
            }
        }



        #region EventHandle
        private void SysPacketEventHandle(SysPacket sysPacket)
        {
            var handle = SysPacketEvent;
            if (handle != null)
            {
                handle(sysPacket);
            }
        }

        
        #endregion
    }
}
