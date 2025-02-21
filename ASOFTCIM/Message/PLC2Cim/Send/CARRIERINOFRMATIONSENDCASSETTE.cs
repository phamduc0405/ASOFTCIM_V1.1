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
    public class CARRIERINOFRMATIONSENDCASSETTE1
    {
        public CARRIERINOFRMATIONSENDCASSETTE1(PLCHelper plcdata, CARRIERINFODOWNLOAD carrier)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CARRIERID").SetValue = carrier.CARRIERID;
                word.FirstOrDefault(x => x.Item == "REPLYCODE").SetValue = carrier.REPLY.REPLYCODE;
                word.FirstOrDefault(x => x.Item == "REPLYTEXT").SetValue = carrier.REPLY.REPLYTEXT;

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
    public class CARRIERINOFRMATIONSENDCASSETTE2
    {
        public CARRIERINOFRMATIONSENDCASSETTE2(PLCHelper plcdata, CARRIERINFODOWNLOAD carrier)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CARRIERID").SetValue = carrier.CARRIERID;
                word.FirstOrDefault(x => x.Item == "REPLYCODE").SetValue = carrier.REPLY.REPLYCODE;
                word.FirstOrDefault(x => x.Item == "REPLYTEXT").SetValue = carrier.REPLY.REPLYTEXT;

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
    public class CARRIERINOFRMATIONSENDCASSETTE3
    {
        public CARRIERINOFRMATIONSENDCASSETTE3(PLCHelper plcdata, CARRIERINFODOWNLOAD carrier)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CARRIERID").SetValue = carrier.CARRIERID;
                word.FirstOrDefault(x => x.Item == "REPLYCODE").SetValue = carrier.REPLY.REPLYCODE;
                word.FirstOrDefault(x => x.Item == "REPLYTEXT").SetValue = carrier.REPLY.REPLYTEXT;

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
