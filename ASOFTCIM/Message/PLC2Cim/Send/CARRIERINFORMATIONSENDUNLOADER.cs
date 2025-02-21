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
    public class CARRIERINFORMATIONSENDUNLOADER1
    {
        public CARRIERINFORMATIONSENDUNLOADER1(PLCHelper plcdata, CARRIERINFODOWNLOAD carrier)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CARRIERID").SetValue = carrier.CARRIERID;
                int sub = carrier.SUBCARRIERS.Count;
                if (sub < 1) return;
                word.FirstOrDefault(x => x.Item == "SUBCARRIERID").SetValue = carrier.SUBCARRIERS[0].SUBCARRIERID;
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
    public class CARRIERINFORMATIONSENDUNLOADER2
    {
        public CARRIERINFORMATIONSENDUNLOADER2(PLCHelper plcdata, CARRIERINFODOWNLOAD carrier)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CARRIERID").SetValue = carrier.CARRIERID;
                int sub = carrier.SUBCARRIERS.Count;
                if (sub < 1) return;
                word.FirstOrDefault(x => x.Item == "SUBCARRIERID").SetValue = carrier.SUBCARRIERS[0].SUBCARRIERID;
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
    public class CARRIERINFORMATIONSENDUNLOADER3
    {
        public CARRIERINFORMATIONSENDUNLOADER3(PLCHelper plcdata, CARRIERINFODOWNLOAD carrier)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CARRIERID").SetValue = carrier.CARRIERID;
                int sub = carrier.SUBCARRIERS.Count;
                if (sub < 1) return;
                word.FirstOrDefault(x => x.Item == "SUBCARRIERID").SetValue = carrier.SUBCARRIERS[0].SUBCARRIERID;
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
