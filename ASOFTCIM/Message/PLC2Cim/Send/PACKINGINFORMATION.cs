using A_SOFT.CMM.INIT;
using A_SOFT.Ctl.Mitsu.Model;
using A_SOFT.PLC;
using ASOFTCIM.Data;
using ASOFTCIM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ASOFTCIM.Message.PLC2Cim.Send
{
    public class PACKINGINFORMATION
    {
        public PACKINGINFORMATION(PLCHelper plcdata, PACKINGINFOR paking)
        {

            try
            {
                Console.WriteLine($"{DateTime.Now.Second}kk" + $"{DateTime.Now.Millisecond}ff");
               
                Stopwatch stopWatch = new Stopwatch();
                // BitModel bit = plcdata.Bits.First(x => x.Item == this.GetType().Name);
                //List<IWordModel> word = bit.LstWord;
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                
                word.FirstOrDefault(x => x.Item == "PackingInformationErrorMessage").SetValue = "1";
                stopWatch.Start();
                word.FirstOrDefault(x => x.Item == "PackingInformationSBPID").SetValue = paking.SBPID;
                
                word.FirstOrDefault(x => x.Item == "PackingInformationCHECKERNAME").SetValue = paking.CHECKERNAME;
                word.FirstOrDefault(x => x.Item == "PackingInformationSHIPMENTTYPE").SetValue = paking.SHIPMENTTYPE;
                word.FirstOrDefault(x => x.Item == "PackingInformationCUSTOMERID").SetValue = paking.CUSTOMERID;
                
                word.FirstOrDefault(x => x.Item == "PackingInformationETCLABELURL").SetValue = paking.CELLSIZE;
                stopWatch.Stop();
                Console.WriteLine($"{stopWatch.ElapsedMilliseconds}");
                Console.WriteLine($"{DateTime.Now.Second}kk" + $"{DateTime.Now.Millisecond}qq");
                BitModel bit = plcdata.Bits.First(x => x.Item == this.GetType().Name);
                Console.WriteLine($"{DateTime.Now.Second}kk" + $"{DateTime.Now.Millisecond}gg");
                

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
