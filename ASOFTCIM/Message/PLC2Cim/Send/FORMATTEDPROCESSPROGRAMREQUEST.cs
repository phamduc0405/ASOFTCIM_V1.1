﻿using A_SOFT.CMM.INIT;
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
    public class FORMATTEDPROCESSPROGRAMREQUEST
    {
        public FORMATTEDPROCESSPROGRAMREQUEST(PLCHelper plcdata, PPIDINFOR ppid)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "ReqPPID").SetValue = ppid.PPID;
                word.FirstOrDefault(x => x.Item == "ReqPPIDTYPE").SetValue = ppid.PPID_TYPE;
                word.FirstOrDefault(x => x.Item == "ReqPPIDINDEX").SetValue = ppid.PPID_NUMBER;


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
