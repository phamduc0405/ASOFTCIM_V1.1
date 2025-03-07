
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
using HPSocket.Sdk;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS2F23()
        {
            try
            {
                TRACESV tracesv = new TRACESV();
                string TIAACK = "";
                string trid, repgsz = "";
                int dsper, totsmp = 0;
                string eqpID = _cim.SysPacket.GetItemString(1);
                trid = _cim.SysPacket.GetItemString(2);
                dsper = int.Parse(_cim.SysPacket.GetItemString(3));
                totsmp = int.Parse(_cim.SysPacket.GetItemString(4));
                repgsz = _cim.SysPacket.GetItemString(5);
                int countSvid = int.Parse(_cim.SysPacket.GetItemString(6));
                List<string> lstSvid = new List<string>();
                for (int i = 0; i < countSvid; i++)
                {
                    lstSvid.Add(_cim.SysPacket.GetItemString());
                }
                tracesv.SVs = lstSvid;
                tracesv.Init(lstSvid, trid, dsper, totsmp, repgsz);
                if(tracesv.DSPER<=900)
                {
                    TIAACK = "3";
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
                if(tracesv.TRID == "0")
                {
                    for (int i = Tracesvs.Count - 1; i >= 0; i--)
                    {
                        Tracesvs[i].Stop();
                        Tracesvs.RemoveAt(i);
                    }
                }    
                if (Tracesvs.Any(x =>x.TRID == tracesv.TRID))
                {
                    if(lstSvid.Count==0)
                    {
                        var Tracesvd = Tracesvs.First(x => x.TRID == tracesv.TRID);
                        Tracesvd.Stop();
                        Tracesvs.Remove(Tracesvs.First(x => x.TRID == tracesv.TRID));
                        return;
                    }
                }
                else
                {
                    tracesv.TraceSvEvent -= (lstSv, isEnd) =>
                    {
                        //S6F1(lstSv, tracesv);
                    };
                    tracesv.TraceSvEvent += (lstSv, isEnd) =>
                    {
                        List<SV> svs = new List<SV>();
                        foreach(var svid in lstSv)
                        {
                            SV sv = new SV();
                            sv = EqpData.SVID.Find(x => x.SVID == svid);
                            svs.Add(sv);
                        }
                        SendS6F1(svs, tracesv);
                        if (tracesv.SMPLN == tracesv.TOTSMP)
                        {
                            Tracesvs.Remove(Tracesvs.First(x => x.TRID == tracesv.TRID));
                            tracesv.Stop();
                        }    
                    };
                    Tracesvs.Add(tracesv);
                    tracesv.Start();
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
