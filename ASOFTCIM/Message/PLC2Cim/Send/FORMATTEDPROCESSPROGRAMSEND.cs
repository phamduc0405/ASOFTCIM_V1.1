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
    public class FORMATTEDPROCESSPROGRAMSEND2
    {

        public FORMATTEDPROCESSPROGRAMSEND2(PLCHelper plcdata,PlcComm plcComm , PPIDINFOR ppid)
        {

            try
            {
                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name);
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "PPIDTYPE").SetValue = ppid.PPID_TYPE;
                word.FirstOrDefault(x => x.Item == "CCODE").SetValue = ppid.COMMANDCODEs[0].CCODE;
                word.FirstOrDefault(x => x.Item == "PPID_NUMBER").SetValue = ppid.PPID_NUMBER;
                
                if(ppid.COMMANDCODEs[0].CCODE =="2")
                {
                    List<PPIDModel> param2 = plcdata.PPIDParams.ToList();
                    param2[1].SetValue(plcComm,ppid.PPID);
                    bit.SetPCValue = true;
                    return;

                }
                else
                {
                    List<PPIDModel> param2 = plcdata.PPIDParams.ToList();
                    param2[1].SetValue(plcComm, ppid.PPID);
                }    
                
                List<PPIDModel> param = plcdata.PPIDParams.ToList();
                int paramcount = ppid.COMMANDCODEs[0].PARAMs.Count;
                for(int i = 0;i<paramcount; i++)
                {
                    param[i+2].SetValue(plcComm, ppid.COMMANDCODEs[0].PARAMs[i].PARAMVALUE);
                }
               
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
