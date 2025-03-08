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

namespace ASOFTCIM.Message.PLC2Cim.Send
{
    public class GETATTRIBUTEREQUEST1
    {
        public GETATTRIBUTEREQUEST1(PLCHelper plcdata, ATTRREQUEST attrequest)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "ATTRDATA").SetValue = attrequest.ATTRs[0].ATTRDATA;
                word.FirstOrDefault(x => x.Item == "REPLYCODE").SetValue = attrequest.REPLYCODE;
                word.FirstOrDefault(x => x.Item == "REPLYTEXT").SetValue = attrequest.REPLYTEXT;


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
    public class GETATTRIBUTEREQUEST2
    {
        public GETATTRIBUTEREQUEST2(PLCHelper plcdata, ATTRREQUEST attrequest)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "ATTRDATA").SetValue = attrequest.ATTRs[1].ATTRDATA;
                word.FirstOrDefault(x => x.Item == "REPLYCODE").SetValue = attrequest.REPLYCODE;
                word.FirstOrDefault(x => x.Item == "REPLYTEXT").SetValue = attrequest.REPLYTEXT;


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
    public class GETATTRIBUTEREQUEST3
    {
        public GETATTRIBUTEREQUEST3(PLCHelper plcdata, ATTRREQUEST attrequest)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "ATTRDATA").SetValue = attrequest.ATTRs[2].ATTRDATA;
                word.FirstOrDefault(x => x.Item == "REPLYCODE").SetValue = attrequest.REPLYCODE;
                word.FirstOrDefault(x => x.Item == "REPLYTEXT").SetValue = attrequest.REPLYTEXT;


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
