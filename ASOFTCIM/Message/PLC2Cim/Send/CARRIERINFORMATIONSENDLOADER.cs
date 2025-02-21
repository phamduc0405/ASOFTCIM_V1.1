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
    public class CARRIERINFORMATIONSENDLOADER1
    {
        public CARRIERINFORMATIONSENDLOADER1(PLCHelper plcdata, CARRIERINFODOWNLOAD carrier)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CARRIERID").SetValue = carrier.CARRIERID;
                word.FirstOrDefault(x => x.Item == "CARRIERTYPE").SetValue = carrier.CARRIERTYPE;
                word.FirstOrDefault(x => x.Item == "CARRIERPRODUCT").SetValue = carrier.CARRIERPRODUCT;
                word.FirstOrDefault(x => x.Item == "CARRIERSTEPID").SetValue = carrier.CARRIERSTEPID;
                word.FirstOrDefault(x => x.Item == "CARRIER_S_COUNT").SetValue = carrier.CARRIER_S_COUNT;
                int sub = carrier.SUBCARRIERS.Count;
                if (sub < 1) return;
                word.FirstOrDefault(x => x.Item == "SUBCARRIERID").SetValue = carrier.SUBCARRIERS[0].SUBCARRIERID;
                word.FirstOrDefault(x => x.Item == "CELLQTY").SetValue = carrier.SUBCARRIERS[0].CELLQTY;
                int cellCount = carrier.SUBCARRIERS[0].CELLSINFOR.Count;
                for (int i = 0; i < cellCount; i++)
                {
                    word.FirstOrDefault(x => x.Item == "CELLID" + (i + 1).ToString()).SetValue = carrier.SUBCARRIERS[0].CELLSINFOR[i].CELLID;
                    word.FirstOrDefault(x => x.Item == "LOCATIONNO" + (i + 1).ToString()).SetValue = carrier.SUBCARRIERS[0].CELLSINFOR[i].LOCATIONNO;
                    word.FirstOrDefault(x => x.Item == "JUDGE" + (i + 1).ToString()).SetValue = carrier.SUBCARRIERS[0].CELLSINFOR[i].JUDGE;
                    word.FirstOrDefault(x => x.Item == "REASONCODE" + (i + 1).ToString()).SetValue = carrier.SUBCARRIERS[0].CELLSINFOR[i].REASONCODE;
                }

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
    public class CARRIERINFORMATIONSENDLOADER2
    {
        public CARRIERINFORMATIONSENDLOADER2(PLCHelper plcdata, CARRIERINFODOWNLOAD carrier)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CARRIERID").SetValue = carrier.CARRIERID;
                word.FirstOrDefault(x => x.Item == "CARRIERTYPE").SetValue = carrier.CARRIERTYPE;
                word.FirstOrDefault(x => x.Item == "CARRIERPRODUCT").SetValue = carrier.CARRIERPRODUCT;
                word.FirstOrDefault(x => x.Item == "CARRIERSTEPID").SetValue = carrier.CARRIERSTEPID;
                word.FirstOrDefault(x => x.Item == "CARRIER_S_COUNT").SetValue = carrier.CARRIER_S_COUNT;
                int sub = carrier.SUBCARRIERS.Count;
                if (sub < 1) return;
                word.FirstOrDefault(x => x.Item == "SUBCARRIERID").SetValue = carrier.SUBCARRIERS[0].SUBCARRIERID;
                word.FirstOrDefault(x => x.Item == "CELLQTY").SetValue = carrier.SUBCARRIERS[0].CELLQTY;
                int cellCount = carrier.SUBCARRIERS[0].CELLSINFOR.Count;
                for (int i = 0; i < cellCount; i++)
                {
                    word.FirstOrDefault(x => x.Item == "CELLID" + (i + 1).ToString()).SetValue = carrier.SUBCARRIERS[0].CELLSINFOR[i].CELLID;
                    word.FirstOrDefault(x => x.Item == "LOCATIONNO" + (i + 1).ToString()).SetValue = carrier.SUBCARRIERS[0].CELLSINFOR[i].LOCATIONNO;
                    word.FirstOrDefault(x => x.Item == "JUDGE" + (i + 1).ToString()).SetValue = carrier.SUBCARRIERS[0].CELLSINFOR[i].JUDGE;
                    word.FirstOrDefault(x => x.Item == "REASONCODE" + (i + 1).ToString()).SetValue = carrier.SUBCARRIERS[0].CELLSINFOR[i].REASONCODE;
                }

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
    public class CARRIERINFORMATIONSENDLOADER3
    {
        public CARRIERINFORMATIONSENDLOADER3(PLCHelper plcdata, CARRIERINFODOWNLOAD carrier)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CARRIERID").SetValue = carrier.CARRIERID;
                word.FirstOrDefault(x => x.Item == "CARRIERTYPE").SetValue = carrier.CARRIERTYPE;
                word.FirstOrDefault(x => x.Item == "CARRIERPRODUCT").SetValue = carrier.CARRIERPRODUCT;
                word.FirstOrDefault(x => x.Item == "CARRIERSTEPID").SetValue = carrier.CARRIERSTEPID;
                word.FirstOrDefault(x => x.Item == "CARRIER_S_COUNT").SetValue = carrier.CARRIER_S_COUNT;
                int sub = carrier.SUBCARRIERS.Count;
                if (sub < 1) return;
                word.FirstOrDefault(x => x.Item == "SUBCARRIERID").SetValue = carrier.SUBCARRIERS[0].SUBCARRIERID;
                word.FirstOrDefault(x => x.Item == "CELLQTY").SetValue = carrier.SUBCARRIERS[0].CELLQTY;
                int cellCount = carrier.SUBCARRIERS[0].CELLSINFOR.Count;
                for (int i = 0; i < cellCount; i++)
                {
                    word.FirstOrDefault(x => x.Item == "CELLID" + (i + 1).ToString()).SetValue = carrier.SUBCARRIERS[0].CELLSINFOR[i].CELLID;
                    word.FirstOrDefault(x => x.Item == "LOCATIONNO" + (i + 1).ToString()).SetValue = carrier.SUBCARRIERS[0].CELLSINFOR[i].LOCATIONNO;
                    word.FirstOrDefault(x => x.Item == "JUDGE" + (i + 1).ToString()).SetValue = carrier.SUBCARRIERS[0].CELLSINFOR[i].JUDGE;
                    word.FirstOrDefault(x => x.Item == "REASONCODE" + (i + 1).ToString()).SetValue = carrier.SUBCARRIERS[0].CELLSINFOR[i].REASONCODE;
                }

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
