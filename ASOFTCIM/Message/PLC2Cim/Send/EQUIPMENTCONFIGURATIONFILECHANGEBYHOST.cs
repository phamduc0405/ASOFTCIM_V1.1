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
    public class EQUIPMENTCONFIGURATIONFILECHANGEBYHOST
    {
        public EQUIPMENTCONFIGURATIONFILECHANGEBYHOST(PLCHelper plcdata, ConfigFileChange config)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "RCMD").SetValue = "17";
                word.FirstOrDefault(x => x.Item == "PRODUCTID").SetValue = config.PRODUCTID;
                word.FirstOrDefault(x => x.Item == "ACTIONTYPE").SetValue = config.ACTIONTYPE;
                word.FirstOrDefault(x => x.Item == "ACTIONRESULT").SetValue = config.ACTIONRESULT;
                int count = config.FILES.Count;
                for (int i = 0; i < count; i++)
                {
                    word.FirstOrDefault(x => x.Item == "FILETYPE" + (i + 1).ToString()).SetValue = config.FILES[i].FILETYPE;
                    word.FirstOrDefault(x => x.Item == "FILENAME" + (i + 1).ToString()).SetValue = config.FILES[i].FILENAME;
                    word.FirstOrDefault(x => x.Item == "FILEPATH" + (i + 1).ToString()).SetValue = config.FILES[i].FILEPATH;
                    word.FirstOrDefault(x => x.Item == "LOCALCHECKSUM" + (i + 1).ToString()).SetValue = config.FILES[i].LOCALCHECKSUM;
                    word.FirstOrDefault(x => x.Item == "CURRENTCHECKSUM" + (i + 1).ToString()).SetValue = config.FILES[i].CURRENTCHECKSUM;
                }


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
