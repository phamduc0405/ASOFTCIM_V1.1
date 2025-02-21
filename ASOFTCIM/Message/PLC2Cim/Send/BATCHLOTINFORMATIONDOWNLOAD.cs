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
using System.Threading;
using System.Threading.Tasks;

namespace ASOFTCIM.Message.PLC2Cim.Send
{
    public class BATCHLOTINFORMATIONDOWNLOAD
    {
        public BATCHLOTINFORMATIONDOWNLOAD(PLCHelper plcdata, BATCHLOT batchlot)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "PRODUCTID").SetValue = batchlot.PRODUCTID;
                word.FirstOrDefault(x => x.Item == "BATCHLOT").SetValue = batchlot.BATCHLOTID;
                word.FirstOrDefault(x => x.Item == "BATCHLOTQTY").SetValue = batchlot.BATCHLOTQTY;
                word.FirstOrDefault(x => x.Item == "REASONCODE").SetValue = batchlot.REASONCODE;
                word.FirstOrDefault(x => x.Item == "DESCRIPTION").SetValue = batchlot.DESCRIPTION;

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
