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

namespace ASOFTCIM
{
     public partial class ACIM 
    {
        public void RecvS16F103()
        {
            try
            {
                PROCESSDATACONTROL process = new PROCESSDATACONTROL();

                process.EQPID = _cim.SysPacket.GetItemString(1);
                process.MODE = _cim.SysPacket.GetItemString();
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
                        PROCESS_MODULE module = new PROCESS_MODULE();
                        module.MODULEID = _cim.SysPacket.GetItemString();
                        module.PPID = _cim.SysPacket.GetItemString();
                        module.PPID_TYPE = _cim.SysPacket.GetItemString();
                        int countParam = int.Parse(_cim.SysPacket.GetItemString());
                        for (int x = 0; x < countParam; x++)
                        {
                            PARAM param = new PARAM();
                            param.PARAMNAME = _cim.SysPacket.GetItemString();
                            param.PARAMVALUE = _cim.SysPacket.GetItemString();
                            module.PARAMs.Add(param);
                        }
                        int countItem = int.Parse(_cim.SysPacket.GetItemString());
                        for (int y = 0; y < countItem; y++)
                        {
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
