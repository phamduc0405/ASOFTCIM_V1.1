using A_SOFT.CMM.INIT;
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
    public class PACKINGINFORMATIONREQUEST
    {
        public void Excute(ACIM eq, object body)
        {
            try
            {
                BitModel bit = (BitModel)body;
                List<IWordModel> word = bit.LstWord;
                PACKINGINFOR packing = new PACKINGINFOR();
                packing.SBPID = word.FirstOrDefault(x => x.Item == "SBPID").GetValue(eq.PLC);
                packing.CHECKERNAME = word.FirstOrDefault(x => x.Item == "CHECKERNAME").GetValue(eq.PLC);
                packing.SHIPMENTTYPE = word.FirstOrDefault(x => x.Item == "SHIPMENTTYPE").GetValue(eq.PLC);

                eq.SendS3F217(packing, eq.EqpData);
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
