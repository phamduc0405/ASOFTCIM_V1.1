
using ASOFTCIM.Data;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ASOFTCIM.Helper;
using ASOFTCIM.MainControl;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS2F23()
        {
            try
            {
                string TIAACK = "";
                string trid, repgsz = "";
                int dsper, totsmp = 0;
                string eqpID = _cim.SysPacket.GetItemString(1);
                trid = _cim.SysPacket.GetItemString();
                dsper = int.Parse(_cim.SysPacket.GetItemString());
                totsmp = int.Parse(_cim.SysPacket.GetItemString());
                repgsz = _cim.SysPacket.GetItemString();

                int countSvid = int.Parse(_cim.SysPacket.GetItemString());
                List<string> lstSvid = new List<string>();
                for (int i = 0; i < countSvid; i++)
                {
                    lstSvid.Add(_cim.SysPacket.GetItemString());
                }

                if (_cim.EQPID != eqpID) TIAACK = "4";
                if (EqpData.SVID.Count < countSvid) TIAACK = "1";
                if (EqpData.EQINFORMATION.CRST == "0") TIAACK = "5";
                foreach (var item in lstSvid)
                {
                    if (!EqpData.SVID.Any(x=>x.SVID == item))
                    {
                        TIAACK = "2";
                        break;
                    }
                }
                SendS2F24( TIAACK);
                if (TIAACK != "") return;
                //if (_cim.EQHelper.LstTraceSv.Any(x => x.TRID == trid))
                //{
                //    cim.EQHelper.LstTraceSv.First(x => x.TRID == trid).Init(lstSvid, trid, dsper, totsmp, repgsz);
                //}
                //else
                //{
                //    ProcessThreadCollection currentThreads = Process.GetCurrentProcess().Threads;
                //    TRACESV trace = new TRACESV();
                //    trace.Init(lstSvid, trid, dsper, totsmp, repgsz);
                    
                //    trace.Start();
                //    trace.TraceSvEvent += (lstSv, isEnd) =>
                //    {
                //        if (isEnd)
                //        {
                //            cim.EQHelper.LstTraceSv.RemoveAll(x => x.TRID == trace.TRID);

                //        }
                //        List<SV> svs = new List<SV>();
                //        foreach (var item in lstSv)
                //        {
                //            svs.Add(EqpData.SVID.First(x => x.SVID == item));
                //        }
                        
                //          SendS6F1(svs,trace);
                //    };
                //    cim.EQHelper.LstTraceSv.Add(trace);
                //}
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
