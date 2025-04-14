using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;

using HPSocket.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LiveCharts.Maps;

namespace ASOFTCIM.Message.PLC2Cim.Recv
{
    public class UNITSTATUS
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                bool isSend = false;
                eq.EqpData.TransactionSys += 1;
                List<EQPSTATE> unitstates = new List<EQPSTATE>();
                List<EQPSTATE> unitstatesbufer = new List<EQPSTATE>();
                List<WordModel> word = new List<WordModel>();
                word = eq.PLCH.Words.Where(x => x.Area == "UnitStatus").ToList();
                for (var i = 1; i < 9; i++)
                {
                    EQPSTATE unitstate = new EQPSTATE();
                    unitstate.AVAILABILITYSTATE = word.FirstOrDefault(x => x.Item == $"AVAILABILITYSTATE{i}").GetValue();
                    unitstate.INTERLOCKSTATE = word.FirstOrDefault(x => x.Item == $"INTERLOCKSTATE{i}").GetValue();
                    unitstate.MOVESTATE = word.FirstOrDefault(x => x.Item == $"MOVESTATE{i}").GetValue();
                    unitstate.RUNSTATE = word.FirstOrDefault(x => x.Item == $"RUNSTATE{i}").GetValue();
                    unitstate.FRONTSTATE = word.FirstOrDefault(x => x.Item == $"FRONTSTATE{i}").GetValue();
                    unitstate.REARSTATE = word.FirstOrDefault(x => x.Item == $"REARSTATE{i}").GetValue();
                    unitstate.PPSPLSTATE = word.FirstOrDefault(x => x.Item == $"PPSPLSTATE{i}").GetValue();
                    unitstates.Add(unitstate);
                }
                List<int> id = new List<int>();
                int unitId =0;
                int count = unitstates.Count;
                int e = 0;
                foreach (var unitstate in unitstates)
                {
                    if(unitstate.AVAILABILITYSTATE != eq.EqpData.UNITSTATES[e].AVAILABILITYSTATE ||
                        unitstate.INTERLOCKSTATE != eq.EqpData.UNITSTATES[e].INTERLOCKSTATE ||
                        unitstate.RUNSTATE != eq.EqpData.UNITSTATES[e].RUNSTATE ||
                        unitstate.FRONTSTATE != eq.EqpData.UNITSTATES[e].FRONTSTATE ||
                        unitstate.REARSTATE != eq.EqpData.UNITSTATES[e].REARSTATE ||
                        unitstate.MOVESTATE != eq.EqpData.UNITSTATES[e].MOVESTATE )
                    {
                        EQPSTATE unitstated = unitstates[e];
                        unitId = e;
                        id.Add(e);
                        unitstatesbufer.Add(unitstated);

                    }
                    e++;
                }
                int u = 0;
                foreach(var item in unitstatesbufer)
                {
                    
                    EQPSTATE oldstate = eq.EqpData.UNITSTATES[id[u]].Copy<EQPSTATE>();

                    eq.SendS6F11_102(oldstate,item);
                    u++;
                }
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
   
        }
       
    }
}
