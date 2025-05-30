﻿using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Message.PLC2Cim.Recv
{
    public class OPERATORLOGINFORMATIONREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                

                List<IWordModel> word = bit.LstWord;
                OPERATOR op = new OPERATOR();
                op.OPTIONINFO = word.FirstOrDefault(x => x.Item == "OPTIONINFO").GetValue(eq.PLC);
                op.COMMENT = word.FirstOrDefault(x => x.Item == "COMMENT").GetValue(eq.PLC);
                op.OPERATORID = word.FirstOrDefault(x => x.Item == "OPERATORID").GetValue(eq.PLC);
                op.PASSWORD = word.FirstOrDefault(x => x.Item == "PASSWORD").GetValue(eq.PLC);

                eq.SendS6F11_607( op);
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
