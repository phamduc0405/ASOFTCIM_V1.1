﻿using ASOFTCIM.Helper;
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

namespace ASOFTCIM
{
     public partial class ACIM 
    {
        public void RecvS16F103()
        {
            try
            {
                string ACK = "0";
                PROCESSDATACONTROL process = new PROCESSDATACONTROL();

                process.EQPID = _cim.SysPacket.GetItemString(1);
                process.MODE = _cim.SysPacket.GetItemString();
                if (process.MODE =="OFF")
                {
                    ACK = "26";
                }
                int countCell = int.Parse(_cim.SysPacket.GetItemString());
                for (int i = 0; i < countCell; i++)
                {
                    string lstCell = _cim.SysPacket.GetItemString();
                    PROCESS_CELL cell = new PROCESS_CELL();
                    cell.CELLID = _cim.SysPacket.GetItemString();
                    cell.SEQ_NO = _cim.SysPacket.GetItemString();
                    int countModule = int.Parse(_cim.SysPacket.GetItemString());
                    for (int j = 0; j < countModule; j++)
                    {
                        string lstModule = _cim.SysPacket.GetItemString();
                        if (process.MODE == "1" && lstModule =="0")
                        {
                            ACK = "22";
                        }
                        PROCESS_MODULE module = new PROCESS_MODULE();
                        module.MODULEID = _cim.SysPacket.GetItemString();
                        if (module.MODULEID != EqpData.PROCESSDATACONTROL.CELLs[0].MODULEs[0].MODULEID)
                        {
                            //ACK = "23";
                        }
                        module.PPID = _cim.SysPacket.GetItemString();
                        module.PPID_TYPE = _cim.SysPacket.GetItemString();
                        //if (module.PPID_TYPE != EqpData.PROCESSDATACONTROL.CELLs[0].MODULEs[0].PPID_TYPE)
                        //{
                        //    ACK = "27";
                        //}
                        int countParam = int.Parse(_cim.SysPacket.GetItemString());
                        for (int x = 0; x < countParam; x++)
                        {
                            int list = int.Parse(_cim.SysPacket.GetItemString());
                            PARAM param = new PARAM();
                            param.PARAMNAME = _cim.SysPacket.GetItemString();
                            param.PARAMVALUE = _cim.SysPacket.GetItemString();
                            module.PARAMs.Add(param);
                        }
                        int countItem = int.Parse(_cim.SysPacket.GetItemString());
                        for (int y = 0; y < countItem; y++)
                        {
                            int list = int.Parse(_cim.SysPacket.GetItemString());
                            ITEM item = new ITEM();
                            item.ITEMNAME = _cim.SysPacket.GetItemString();
                            item.ITEMVALUE = _cim.SysPacket.GetItemString();
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
                SendS9F7(_cim.SysPacket);
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
        }
    }
}
