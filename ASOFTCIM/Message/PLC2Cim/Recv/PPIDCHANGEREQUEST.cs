using A_SOFT.CMM.INIT;
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
    public class PPIDCHANGEREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                List<WordModel> word = eq.PLCH.Words.Where(x => x.Area == bit.Comment).ToList();
                PPIDINFOR pPIDModel = new PPIDINFOR();
                pPIDModel.MODE = word.FirstOrDefault(x => x.Item == "PPID").GetValue(eq.PLC);


                eq.SendS7F107(pPIDModel);
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
