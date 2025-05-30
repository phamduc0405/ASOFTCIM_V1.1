using ASOFTCIM.Helper;
using ASOFTCIM.Data;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ASOFTCIM.Message.PLC2Cim.Send;
using A_SOFT.PLC;
using System.Threading;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
     public partial class ACIM 
    {
        public void RecvS16F103(SysPacket sysPacket)
        {
            try
            {
                string ACK = "0";
                PROCESSDATACONTROL process = new PROCESSDATACONTROL();

                process.EQPID = sysPacket.GetItemString(1);
                process.MODE = sysPacket.GetItemString();
                if (process.MODE =="OFF")
                {
                    ACK = "26";
                }
                int countCell = int.Parse(sysPacket.GetItemString());
                for (int i = 0; i < countCell; i++)
                {
                    string lstCell = sysPacket.GetItemString();
                    PROCESS_CELL cell = new PROCESS_CELL();
                    cell.CELLID = sysPacket.GetItemString();
                    cell.SEQ_NO = sysPacket.GetItemString();
                    int countModule = int.Parse(sysPacket.GetItemString());
                    for (int j = 0; j < countModule; j++)
                    {
                        string lstModule = sysPacket.GetItemString();
                        if (process.MODE == "1" && lstModule =="0")
                        {
                            ACK = "22";
                        }
                        PROCESS_MODULE module = new PROCESS_MODULE();
                        module.MODULEID = sysPacket.GetItemString();
                        if (module.MODULEID != EqpData.PROCESSDATACONTROL.CELLs[0].MODULEs[0].MODULEID)
                        {
                            //ACK = "23";
                        }
                        module.PPID = sysPacket.GetItemString();
                        module.PPID_TYPE = sysPacket.GetItemString();
                        //if (module.PPID_TYPE != EqpData.PROCESSDATACONTROL.CELLs[0].MODULEs[0].PPID_TYPE)
                        //{
                        //    ACK = "27";
                        //}
                        int countParam = int.Parse(sysPacket.GetItemString());
                        for (int x = 0; x < countParam; x++)
                        {
                            int list = int.Parse(sysPacket.GetItemString());
                            PARAM param = new PARAM();
                            param.PARAMNAME = sysPacket.GetItemString();
                            param.PARAMVALUE = sysPacket.GetItemString();
                            module.PARAMs.Add(param);
                        }
                        int countItem = int.Parse(sysPacket.GetItemString());
                        for (int y = 0; y < countItem; y++)
                        {
                            int list = int.Parse(sysPacket.GetItemString());
                            ITEM item = new ITEM();
                            item.ITEMNAME = sysPacket.GetItemString();
                            item.ITEMVALUE = sysPacket.GetItemString();
                            module.ITEMs.Add(item);
                        }
                        cell.MODULEs.Add(module);
                    }
                    process.CELLs.Add(cell);
                }

                //if (_cim.EQHelper.IsPlc)
                //    new ASOFTCIM.EQ.PLC.PLCMessage.Send.PROCESSCONTROLINFORMATIONSEND(_cim.EQHelper.PLCData, process);
                //new PROCESSCONTROLINFORMATIONSEND(EqpData, cim.EQHelper.Conn, process);
                
                

                if (process.EQPID != EqpData.EQINFORMATION.EQPID)
                {
                    ACK = "22";
                }
                


                SendMessage2PLC("PROCESSCONTROLINFORMATIONSEND", process, PLC);
                Thread.Sleep(500);
                WordModel word = _plcH.Words.FirstOrDefault(x => x.Area == "ProcessControlInformSend1");
                ACK = word.GetValue(PLC);
                SendS16F104(ACK);
            }
            catch (Exception ex)
            {
                SendS9F7(sysPacket);
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
        }
    }
}
