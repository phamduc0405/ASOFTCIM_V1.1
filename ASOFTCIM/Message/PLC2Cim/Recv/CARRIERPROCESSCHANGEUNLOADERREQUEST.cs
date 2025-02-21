using A_SOFT.PLC;
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
    public class CARRIERPROCESSCHANGEUNLOADERREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                

                List<IWordModel> word = bit.LstWord;
                CARRIERPROCESSCHANGE carr = new CARRIERPROCESSCHANGE();
                carr.CARRIERID = word.FirstOrDefault(x => x.Item == "CARRIERID").GetValue(eq.PLC);
                carr.CARRIERTYPE = word.FirstOrDefault(x => x.Item == "CARRIERTYPE").GetValue(eq.PLC);
                carr.CARRIERPRODUCT = word.FirstOrDefault(x => x.Item == "CARRIERPRODUCT").GetValue(eq.PLC);
                carr.CARRIERSTEPID = word.FirstOrDefault(x => x.Item == "CARRIERSTEPID").GetValue(eq.PLC);
                carr.CARRIER_S_COUNT = word.FirstOrDefault(x => x.Item == "CARRIER_S_COUNT").GetValue(eq.PLC);
                carr.CARRIER_C_COUNT = word.FirstOrDefault(x => x.Item == "CARRIER_C_COUNT").GetValue(eq.PLC);
                carr.PORTNO = word.FirstOrDefault(x => x.Item == "CARRIER_C_COUNT").GetValue(eq.PLC);
                carr.CEID = word.FirstOrDefault(x => x.Item == "CEID").GetValue(eq.PLC);
                for (int i = 1; i <= 51; i++)
                {
                    SUBCARRIER sub = new SUBCARRIER();
                    sub.SUBCARRIERID = word.FirstOrDefault(x => x.Item == "SUBCARRIERID" + i.ToString()).GetValue(eq.PLC);
                    sub.CELLQTY = word.FirstOrDefault(x => x.Item == "CELLQTY").GetValue(eq.PLC);
                    for (int j = 1; j <= 10; j++)
                    {
                        CELLINFO cell = new CELLINFO();
                        cell.CELLID = word.FirstOrDefault(x => x.Item == "CELLID" + j.ToString()).GetValue(eq.PLC);
                        cell.LOCATIONNO = word.FirstOrDefault(x => x.Item == "LOCATIONNO" + j.ToString()).GetValue(eq.PLC);
                        cell.JUDGE = word.FirstOrDefault(x => x.Item == "JUDGE" + j.ToString()).GetValue(eq.PLC);
                        cell.REASONCODE = word.FirstOrDefault(x => x.Item == "REASONCODE" + j.ToString()).GetValue(eq.PLC);
                        sub.CELLSINFOR.Add(cell);
                    }
                    carr.SUBCARRIERS.Add(sub);
                }

                eq.SendS6F11_256_262( carr, carr.CEID);
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
