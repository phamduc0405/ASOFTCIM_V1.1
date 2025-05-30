﻿using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Message.PLC2Cim.Recv
{
    public class PROCESSCONTROLDATACREATEDELETEREPORT
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                

                List<IWordModel> word = bit.LstWord;
                PROCESSDATACONTROL pro = new PROCESSDATACONTROL();
                pro = eq.EqpData.PROCESSDATACONTROL;
                pro.MODE = word.FirstOrDefault(x => x.Item == "MODE").GetValue(eq.PLC);
                pro.BYWHO = word.FirstOrDefault(x => x.Item == "BYWHO").GetValue(eq.PLC);
                pro.CELLs[0].CELLID= word.FirstOrDefault(x => x.Item == "CELLID").GetValue(eq.PLC);
                pro.CELLs[0].SEQ_NO= word.FirstOrDefault(x => x.Item == "SEQ_NO").GetValue(eq.PLC);
                pro.CELLs[0].MODULEs[0].MODULEID = word.FirstOrDefault(x => x.Item == "MODULEID").GetValue(eq.PLC);
                eq.SendS16F105( pro);
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
