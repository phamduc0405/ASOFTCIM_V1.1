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
    public class SPECIFICVALIDATIONDATASEND1
    {
        public SPECIFICVALIDATIONDATASEND1(PLCHelper plcdata, Validation val)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CARRIERID").SetValue = val.CARRIERID;
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = val.UNIQUEID;
                word.FirstOrDefault(x => x.Item == "UNIQUETYPE").SetValue = val.UNIQUETYPE;
                word.FirstOrDefault(x => x.Item == "PRODUCTID").SetValue = val.PRODUCTID;
                word.FirstOrDefault(x => x.Item == "STEPID").SetValue = val.STEPID;
                word.FirstOrDefault(x => x.Item == "REPLYSTATUS").SetValue = val.REPLY.REPLYSTATUS;
                word.FirstOrDefault(x => x.Item == "REPLYTEXT").SetValue = val.REPLY.REPLYTEXT;

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
    public class SPECIFICVALIDATIONDATASEND2
    {
        public SPECIFICVALIDATIONDATASEND2(PLCHelper plcdata, Validation val)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CARRIERID").SetValue = val.CARRIERID;
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = val.UNIQUEID;
                word.FirstOrDefault(x => x.Item == "UNIQUETYPE").SetValue = val.UNIQUETYPE;
                word.FirstOrDefault(x => x.Item == "PRODUCTID").SetValue = val.PRODUCTID;
                word.FirstOrDefault(x => x.Item == "STEPID").SetValue = val.STEPID;
                word.FirstOrDefault(x => x.Item == "REPLYSTATUS").SetValue = val.REPLY.REPLYSTATUS;
                word.FirstOrDefault(x => x.Item == "REPLYTEXT").SetValue = val.REPLY.REPLYTEXT;

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
    public class SPECIFICVALIDATIONDATASEND3
    {
        public SPECIFICVALIDATIONDATASEND3(PLCHelper plcdata, Validation val)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CARRIERID").SetValue = val.CARRIERID;
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = val.UNIQUEID;
                word.FirstOrDefault(x => x.Item == "UNIQUETYPE").SetValue = val.UNIQUETYPE;
                word.FirstOrDefault(x => x.Item == "PRODUCTID").SetValue = val.PRODUCTID;
                word.FirstOrDefault(x => x.Item == "STEPID").SetValue = val.STEPID;
                word.FirstOrDefault(x => x.Item == "REPLYSTATUS").SetValue = val.REPLY.REPLYSTATUS;
                word.FirstOrDefault(x => x.Item == "REPLYTEXT").SetValue = val.REPLY.REPLYTEXT;

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
    public class SPECIFICVALIDATIONDATASEND4
    {
        public SPECIFICVALIDATIONDATASEND4(PLCHelper plcdata, Validation val)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CARRIERID").SetValue = val.CARRIERID;
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = val.UNIQUEID;
                word.FirstOrDefault(x => x.Item == "UNIQUETYPE").SetValue = val.UNIQUETYPE;
                word.FirstOrDefault(x => x.Item == "PRODUCTID").SetValue = val.PRODUCTID;
                word.FirstOrDefault(x => x.Item == "STEPID").SetValue = val.STEPID;
                word.FirstOrDefault(x => x.Item == "REPLYSTATUS").SetValue = val.REPLY.REPLYSTATUS;
                word.FirstOrDefault(x => x.Item == "REPLYTEXT").SetValue = val.REPLY.REPLYTEXT;

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
    public class SPECIFICVALIDATIONDATASEND5
    {
        public SPECIFICVALIDATIONDATASEND5(PLCHelper plcdata, Validation val)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CARRIERID").SetValue = val.CARRIERID;
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = val.UNIQUEID;
                word.FirstOrDefault(x => x.Item == "UNIQUETYPE").SetValue = val.UNIQUETYPE;
                word.FirstOrDefault(x => x.Item == "PRODUCTID").SetValue = val.PRODUCTID;
                word.FirstOrDefault(x => x.Item == "STEPID").SetValue = val.STEPID;
                word.FirstOrDefault(x => x.Item == "REPLYSTATUS").SetValue = val.REPLY.REPLYSTATUS;
                word.FirstOrDefault(x => x.Item == "REPLYTEXT").SetValue = val.REPLY.REPLYTEXT;

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
