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
    public class PARAMETERCHANGE2
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                eq.ReadRMS();
                BitModel bit = (BitModel)body;
                PPIDINFOR ppidinfor = new PPIDINFOR();
                COMMANDCODE commandcode = new COMMANDCODE();
                PPPARAMS ppparam = new PPPARAMS();
                ppidinfor.MODE = eq.PLCH.PPIDParams[0].GetValue(eq.PLC);
                ppidinfor.PPID = eq.PLCH.PPIDParams[1].GetValue(eq.PLC);
                foreach (var ppidparam in eq.PLCH.PPIDParams.Skip(2))
                {
                    if (ppidparam.Item != "RESERVED")
                    {
                        PARAM param = new PARAM();
                        param.PARAMVALUE = ppidparam.GetValue(eq.PLC);
                        param.PARAMNAME = ppidparam.Item;
                        ppparam.PARAMS.Add(param);
                        commandcode.PARAMs.Add(param);
                        commandcode.CCODE = ppidinfor.MODE;
                    }
                }
                ppidinfor.COMMANDCODEs.Add(commandcode);
                //sau khi thay đổi cần đọc lại RMS
                eq.ReadRMS();
                eq.SendS7F217(ppidinfor, ppidinfor.MODE);
                bit.SetPCValue = true;
                //BitModel bit = (BitModel)body;
                //List<IWordModel> word = bit.LstWord;
                //PPIDINFOR ppidinfor = new PPIDINFOR();
                //COMMANDCODE commandcode = new COMMANDCODE();
                //PPPARAMS ppparam = new PPPARAMS();
                //ppidinfor.MODE = eq.PLCH.PPIDParams[0].GetValue(eq.PLC);
                //string parameterchange = word.FirstOrDefault(x => x.Item == "ParameterChange2RecipeNumber").GetValue(eq.PLC);
                //int p = int.Parse(parameterchange[0].ToString());
                //ppidinfor.PPID = eq.EqpData.PPIDList.PPID[p-1].ToString();
                //string ccode = parameterchange[1].ToString();
                //ppidinfor.PPID_NUMBER = p.ToString();
                //if(ppidinfor.MODE =="2")
                //{

                //    eq.EqpData.PPIDList.PPID.RemoveAt(p-1);

                //    eq.SendS7F217(ppidinfor, ppidinfor.MODE);
                //    bit.SetPCValue = true;
                //    return;
                //}

                //if(ppidinfor.MODE == "1" || ppidinfor.MODE == "3")
                //{
                //    ppidinfor.PPID = eq.PLCH.PPIDParams[1].GetValue(eq.PLC);
                //    foreach (var ppidparam in eq.PLCH.PPIDParams.Skip(2))
                //    {
                //        if (ppidparam.Item != "RESERVED")
                //        {
                //            PARAM param = new PARAM();
                //            param.PARAMVALUE = ppidparam.GetValue(eq.PLC);
                //            param.PARAMNAME = ppidparam.Item;
                //            ppparam.PARAMS.Add(param);
                //            commandcode.PARAMs.Add(param);
                //            commandcode.CCODE = ppidinfor.MODE;
                //        }
                //    }
                //    ppidinfor.COMMANDCODEs.Add(commandcode);
                //    //sau khi thay đổi cần đọc lại RMS

                //    for (int i = 0; i < eq.EqpData.PPIDList.PPID.Count; i++)
                //    {
                //        if (eq.EqpData.PPIDList.PPID[i] == eq.EqpData.CurrPPID.PPID)
                //        {
                //            eq.EqpData.CurrPPID.PPID_NUMBER = i.ToString();
                //            break;
                //        }
                //    }
                //    //ppidinfor.PPID_NUMBER = eq.EqpData.CurrPPID.PPID_NUMBER;
                //    eq.SendS7F217(ppidinfor, ppidinfor.MODE);
                //    bit.SetPCValue = true;
                return;
                //}

            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    }
}
