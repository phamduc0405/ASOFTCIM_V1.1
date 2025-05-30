﻿using A_SOFT.CMM.INIT;
using A_SOFT.Ctl.Mitsu.Model;
using A_SOFT.PLC;
using ASOFTCIM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Message.PLC2Cim.Recv
{
    public class MATERIALIDREADINGRESULTREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                
                List<IWordModel> word = bit.LstWord;
                MATERIALSTATE materialstate = new MATERIALSTATE();
                materialstate.MATERIALID = word.FirstOrDefault(x => x.Item == "MATERIALID").GetValue(eq.PLC);
                materialstate.MATERIALPORTID = word.FirstOrDefault(x => x.Item == "MATERIALPORTID").GetValue(eq.PLC);
                string  unitId = word.FirstOrDefault(x => x.Item == "MODULEID").GetValue(eq.PLC);
                eq.SendS6F11_615(materialstate, unitId);
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
