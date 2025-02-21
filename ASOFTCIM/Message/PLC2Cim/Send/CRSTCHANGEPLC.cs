using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using ASOFTCIM.Data;
using ASOFTCIM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Message.PLC2Cim.Send
{
    public class CRSTCHANGEPLC
    {
        public CRSTCHANGEPLC(PLCHelper plcdata, string CRST)
        {

            try
            {
                BitModel bit = new BitModel();
                switch (CRST)
                {
                    case "0":
                        bit = plcdata.Bits.First(x => x.Item == "REQUESTOFFLINE");
                        break;
                    case "1":
                        bit = plcdata.Bits.First(x => x.Item == "REQUESTONLINELOCAL");
                        break;
                    case "2":
                        bit = plcdata.Bits.First(x => x.Item == "REQUESTONLINEREMOTE");
                        break;
                    default:
                        break;
                }
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }
}
