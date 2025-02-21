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
    public class PROCESSCONTROLINFORMATIONSEND
    {
        public PROCESSCONTROLINFORMATIONSEND(PLCHelper plcdata, PROCESSDATACONTROL process)
        {

            try
            {
                if (process.CELLs.Count < 1) return;

                List<WordModel> word = plcdata.Words.Where(x => x.Area == (this.GetType().Name + "1")).ToList();
                word.FirstOrDefault(x => x.Item == "MODE").SetValue = process.MODE;
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = process.CELLs[0].CELLID;
                word.FirstOrDefault(x => x.Item == "SEQ_NO").SetValue = process.CELLs[0].SEQ_NO;
                word.FirstOrDefault(x => x.Item == "MODULEID").SetValue = process.CELLs[0].MODULEs[0].MODULEID;
                word.FirstOrDefault(x => x.Item == "PPID").SetValue = process.CELLs[0].MODULEs[0].PPID;
                word.FirstOrDefault(x => x.Item == "PPID").SetValue = process.CELLs[0].MODULEs[0].PPID_TYPE;

                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name);
                bit.SetPCValue = true;

                if (process.CELLs.Count < 2) return;
                word = plcdata.Words.Where(x => x.Area == (this.GetType().Name + "2")).ToList();
                word.FirstOrDefault(x => x.Item == "MODE").SetValue = process.MODE;
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = process.CELLs[1].CELLID;
                word.FirstOrDefault(x => x.Item == "SEQ_NO").SetValue = process.CELLs[1].SEQ_NO;
                word.FirstOrDefault(x => x.Item == "MODULEID").SetValue = process.CELLs[1].MODULEs[0].MODULEID;
                word.FirstOrDefault(x => x.Item == "PPID").SetValue = process.CELLs[1].MODULEs[0].PPID;
                word.FirstOrDefault(x => x.Item == "PPID").SetValue = process.CELLs[1].MODULEs[0].PPID_TYPE;
                bit = plcdata.Bits.First(x => x.Comment == (this.GetType().Name + "2"));
                bit.SetPCValue = true;

                if (process.CELLs.Count < 3) return;
                word = plcdata.Words.Where(x => x.Area == (this.GetType().Name + "3")).ToList();
                word.FirstOrDefault(x => x.Item == "MODE").SetValue = process.MODE;
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = process.CELLs[2].CELLID;
                word.FirstOrDefault(x => x.Item == "SEQ_NO").SetValue = process.CELLs[2].SEQ_NO;
                word.FirstOrDefault(x => x.Item == "MODULEID").SetValue = process.CELLs[2].MODULEs[0].MODULEID;
                word.FirstOrDefault(x => x.Item == "PPID").SetValue = process.CELLs[2].MODULEs[0].PPID;
                word.FirstOrDefault(x => x.Item == "PPID").SetValue = process.CELLs[2].MODULEs[0].PPID_TYPE;
                bit = plcdata.Bits.First(x => x.Comment == (this.GetType().Name + "3"));
                bit.SetPCValue = true;

                if (process.CELLs.Count < 4) return;
                word = plcdata.Words.Where(x => x.Area == (this.GetType().Name + "4")).ToList();
                word.FirstOrDefault(x => x.Item == "MODE").SetValue = process.MODE;
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = process.CELLs[3].CELLID;
                word.FirstOrDefault(x => x.Item == "SEQ_NO").SetValue = process.CELLs[3].SEQ_NO;
                word.FirstOrDefault(x => x.Item == "MODULEID").SetValue = process.CELLs[3].MODULEs[0].MODULEID;
                word.FirstOrDefault(x => x.Item == "PPID").SetValue = process.CELLs[3].MODULEs[0].PPID;
                word.FirstOrDefault(x => x.Item == "PPID").SetValue = process.CELLs[3].MODULEs[0].PPID_TYPE;
                bit = plcdata.Bits.First(x => x.Comment == (this.GetType().Name + "4"));
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
