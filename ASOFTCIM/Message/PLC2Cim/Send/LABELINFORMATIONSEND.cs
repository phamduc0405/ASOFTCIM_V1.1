using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using ASOFTCIM.Data;
using ASOFTCIM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace ASOFTCIM.Message.PLC2Cim.Send
{
    public class LABELINFORMATIONSEND
    {
        public LABELINFORMATIONSEND(PLCHelper plcdata, LABELINFODOWNLOAD labelinfodownload)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "OPTIONCODE").SetValue = labelinfodownload.OPTIONCODE;
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = labelinfodownload.CELLID;
                word.FirstOrDefault(x => x.Item == "PRODUCTID").SetValue = labelinfodownload.PRODUCTID;
                word.FirstOrDefault(x => x.Item == "LABELID").SetValue = labelinfodownload.LABELID;
                word.FirstOrDefault(x => x.Item == "REPLYSTATUS").SetValue = labelinfodownload.REPLYSTATUS;
                word.FirstOrDefault(x => x.Item == "REPLYCODE").SetValue = labelinfodownload.REPLYCODE;
                word.FirstOrDefault(x => x.Item == "REPLYTEXT").SetValue = labelinfodownload.REPLYTEXT;
                

                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name);
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
