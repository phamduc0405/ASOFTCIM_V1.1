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
    public class MATERIALINFORMATIONSEND1
    {
        public MATERIALINFORMATIONSEND1(PLCHelper plcdata, MATERIALINFOMATIONSEND material)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "MATERIALEQPID").SetValue = material.MATERIALSTANDARD.MATERIALEQPID;
                word.FirstOrDefault(x => x.Item == "MATERIALBATCHID").SetValue = material.MATERIALSTANDARD.MATERIALBATCHID;
                word.FirstOrDefault(x => x.Item == "MATERIALCODE").SetValue = material.MATERIALSTANDARD.MATERIALCODE;
                word.FirstOrDefault(x => x.Item == "MATERIALUSEDATE").SetValue = material.MATERIALSTANDARD.MATERIALUSEDATE;
                word.FirstOrDefault(x => x.Item == "MATERIALDISEASEDATE").SetValue = material.MATERIALSTANDARD.MATERIALDISEASEDATE;
                word.FirstOrDefault(x => x.Item == "MATERIALMAKER").SetValue = material.MATERIALSTANDARD.MATERIALMAKER;
                word.FirstOrDefault(x => x.Item == "MATERIALVALIDATIONFLAGE").SetValue = material.MATERIALSTANDARD.MATERIALVALIDATIONFLAGE;
                word.FirstOrDefault(x => x.Item == "MATERIALDEFECTCODE").SetValue = material.MATERIALSTANDARD.MATERIALDEFECTCODE;
                word.FirstOrDefault(x => x.Item == "COMMENT").SetValue = material.MATERIALSTANDARD.COMMENT;
                word.FirstOrDefault(x => x.Item == "MATERIALID").SetValue = material.MATERIALUSEINFO.MATERIALID;
                word.FirstOrDefault(x => x.Item == "MATERIALTYPE").SetValue = material.MATERIALUSEINFO.MATERIALTYPE;
                word.FirstOrDefault(x => x.Item == "MATERIALST").SetValue = material.MATERIALUSEINFO.MATERIALST;
                word.FirstOrDefault(x => x.Item == "MATERIALPORTID").SetValue = material.MATERIALUSEINFO.MATERIALPORTID;
                word.FirstOrDefault(x => x.Item == "MATERIALSTATE").SetValue = material.MATERIALUSEINFO.MATERIALSTATE;
                word.FirstOrDefault(x => x.Item == "MATERIALTOTLAQTY").SetValue = material.MATERIALUSEINFO.MATERIALTOTALQTY;
                word.FirstOrDefault(x => x.Item == "MATERIALUSEQTY").SetValue = material.MATERIALUSEINFO.MATERIALUSEQTY;
                word.FirstOrDefault(x => x.Item == "MATERIALASSEMQTY").SetValue = material.MATERIALUSEINFO.MATERIALASSEMQTY;
                word.FirstOrDefault(x => x.Item == "MATERIALNGQTY").SetValue = material.MATERIALUSEINFO.MATERIALNGQTY;
                word.FirstOrDefault(x => x.Item == "MATERIALREMAINQTYQTY").SetValue = material.MATERIALUSEINFO.MATERIALREMAINQTY;
                word.FirstOrDefault(x => x.Item == "MATERIALPROCEUSEQTY").SetValue = material.MATERIALUSEINFO.MATERIALPROCUSEQTY;
                word.FirstOrDefault(x => x.Item == "REPLYSTATUS").SetValue = material.REPLY.REPLYSTATUS;
                word.FirstOrDefault(x => x.Item == "REPLYCODE").SetValue = material.REPLY.REPLYCODE;
                word.FirstOrDefault(x => x.Item == "REPLYTEXT").SetValue = material.REPLY.REPLYTEXT;


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
    public class MATERIALINFORMATIONSEND2
    {
        public MATERIALINFORMATIONSEND2(PLCHelper plcdata, MATERIALINFOMATIONSEND material)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "MATERIALEQPID").SetValue = material.MATERIALSTANDARD.MATERIALEQPID;
                word.FirstOrDefault(x => x.Item == "MATERIALBATCHID").SetValue = material.MATERIALSTANDARD.MATERIALBATCHID;
                word.FirstOrDefault(x => x.Item == "MATERIALCODE").SetValue = material.MATERIALSTANDARD.MATERIALCODE;
                word.FirstOrDefault(x => x.Item == "MATERIALUSEDATE").SetValue = material.MATERIALSTANDARD.MATERIALUSEDATE;
                word.FirstOrDefault(x => x.Item == "MATERIALDISEASEDATE").SetValue = material.MATERIALSTANDARD.MATERIALDISEASEDATE;
                word.FirstOrDefault(x => x.Item == "MATERIALMAKER").SetValue = material.MATERIALSTANDARD.MATERIALMAKER;
                word.FirstOrDefault(x => x.Item == "MATERIALVALIDATIONFLAGE").SetValue = material.MATERIALSTANDARD.MATERIALVALIDATIONFLAGE;
                word.FirstOrDefault(x => x.Item == "MATERIALDEFECTCODE").SetValue = material.MATERIALSTANDARD.MATERIALDEFECTCODE;
                word.FirstOrDefault(x => x.Item == "COMMENT").SetValue = material.MATERIALSTANDARD.COMMENT;
                word.FirstOrDefault(x => x.Item == "MATERIALID").SetValue = material.MATERIALUSEINFO.MATERIALID;
                word.FirstOrDefault(x => x.Item == "MATERIALTYPE").SetValue = material.MATERIALUSEINFO.MATERIALTYPE;
                word.FirstOrDefault(x => x.Item == "MATERIALST").SetValue = material.MATERIALUSEINFO.MATERIALST;
                word.FirstOrDefault(x => x.Item == "MATERIALPORTID").SetValue = material.MATERIALUSEINFO.MATERIALPORTID;
                word.FirstOrDefault(x => x.Item == "MATERIALSTATE").SetValue = material.MATERIALUSEINFO.MATERIALSTATE;
                word.FirstOrDefault(x => x.Item == "MATERIALTOTLAQTY").SetValue = material.MATERIALUSEINFO.MATERIALTOTALQTY;
                word.FirstOrDefault(x => x.Item == "MATERIALUSEQTY").SetValue = material.MATERIALUSEINFO.MATERIALUSEQTY;
                word.FirstOrDefault(x => x.Item == "MATERIALASSEMQTY").SetValue = material.MATERIALUSEINFO.MATERIALASSEMQTY;
                word.FirstOrDefault(x => x.Item == "MATERIALNGQTY").SetValue = material.MATERIALUSEINFO.MATERIALNGQTY;
                word.FirstOrDefault(x => x.Item == "MATERIALREMAINQTYQTY").SetValue = material.MATERIALUSEINFO.MATERIALREMAINQTY;
                word.FirstOrDefault(x => x.Item == "MATERIALPROCEUSEQTY").SetValue = material.MATERIALUSEINFO.MATERIALPROCUSEQTY;
                word.FirstOrDefault(x => x.Item == "REPLYSTATUS").SetValue = material.REPLY.REPLYSTATUS;
                word.FirstOrDefault(x => x.Item == "REPLYCODE").SetValue = material.REPLY.REPLYCODE;
                word.FirstOrDefault(x => x.Item == "REPLYTEXT").SetValue = material.REPLY.REPLYTEXT;


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
    public class MATERIALINFORMATIONSEND3
    {
        public MATERIALINFORMATIONSEND3(PLCHelper plcdata, MATERIALINFOMATIONSEND material)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "MATERIALEQPID").SetValue = material.MATERIALSTANDARD.MATERIALEQPID;
                word.FirstOrDefault(x => x.Item == "MATERIALBATCHID").SetValue = material.MATERIALSTANDARD.MATERIALBATCHID;
                word.FirstOrDefault(x => x.Item == "MATERIALCODE").SetValue = material.MATERIALSTANDARD.MATERIALCODE;
                word.FirstOrDefault(x => x.Item == "MATERIALUSEDATE").SetValue = material.MATERIALSTANDARD.MATERIALUSEDATE;
                word.FirstOrDefault(x => x.Item == "MATERIALDISEASEDATE").SetValue = material.MATERIALSTANDARD.MATERIALDISEASEDATE;
                word.FirstOrDefault(x => x.Item == "MATERIALMAKER").SetValue = material.MATERIALSTANDARD.MATERIALMAKER;
                word.FirstOrDefault(x => x.Item == "MATERIALVALIDATIONFLAGE").SetValue = material.MATERIALSTANDARD.MATERIALVALIDATIONFLAGE;
                word.FirstOrDefault(x => x.Item == "MATERIALDEFECTCODE").SetValue = material.MATERIALSTANDARD.MATERIALDEFECTCODE;
                word.FirstOrDefault(x => x.Item == "COMMENT").SetValue = material.MATERIALSTANDARD.COMMENT;
                word.FirstOrDefault(x => x.Item == "MATERIALID").SetValue = material.MATERIALUSEINFO.MATERIALID;
                word.FirstOrDefault(x => x.Item == "MATERIALTYPE").SetValue = material.MATERIALUSEINFO.MATERIALTYPE;
                word.FirstOrDefault(x => x.Item == "MATERIALST").SetValue = material.MATERIALUSEINFO.MATERIALST;
                word.FirstOrDefault(x => x.Item == "MATERIALPORTID").SetValue = material.MATERIALUSEINFO.MATERIALPORTID;
                word.FirstOrDefault(x => x.Item == "MATERIALSTATE").SetValue = material.MATERIALUSEINFO.MATERIALSTATE;
                word.FirstOrDefault(x => x.Item == "MATERIALTOTLAQTY").SetValue = material.MATERIALUSEINFO.MATERIALTOTALQTY;
                word.FirstOrDefault(x => x.Item == "MATERIALUSEQTY").SetValue = material.MATERIALUSEINFO.MATERIALUSEQTY;
                word.FirstOrDefault(x => x.Item == "MATERIALASSEMQTY").SetValue = material.MATERIALUSEINFO.MATERIALASSEMQTY;
                word.FirstOrDefault(x => x.Item == "MATERIALNGQTY").SetValue = material.MATERIALUSEINFO.MATERIALNGQTY;
                word.FirstOrDefault(x => x.Item == "MATERIALREMAINQTYQTY").SetValue = material.MATERIALUSEINFO.MATERIALREMAINQTY;
                word.FirstOrDefault(x => x.Item == "MATERIALPROCEUSEQTY").SetValue = material.MATERIALUSEINFO.MATERIALPROCUSEQTY;
                word.FirstOrDefault(x => x.Item == "REPLYSTATUS").SetValue = material.REPLY.REPLYSTATUS;
                word.FirstOrDefault(x => x.Item == "REPLYCODE").SetValue = material.REPLY.REPLYCODE;
                word.FirstOrDefault(x => x.Item == "REPLYTEXT").SetValue = material.REPLY.REPLYTEXT;


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
