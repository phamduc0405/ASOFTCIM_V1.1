using ASOFTCIM.Helper;


using ASOFTCIM.Data;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Message.PLC2Cim.Send;
using A_SOFT.PLC;
using System.Threading;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS2F41()
        {
            try
            {
                string HACK = "0";
                string RCMD = _cim.SysPacket.GetItemString(1);
                if (RCMD == "4"|| RCMD == "5"||RCMD == "6"||RCMD == "7"|| RCMD == "8")
                {
                    JobProcess jobProcess = new JobProcess();
                    jobProcess.RCMD = RCMD;
                    jobProcess.EQPID = _cim.SysPacket.GetItemString(1);
                    jobProcess.PARENTLOT = _cim.SysPacket.GetItemString(4);
                    jobProcess.RFID = _cim.SysPacket.GetItemString();
                    jobProcess.EQPID = _cim.SysPacket.GetItemString();
                    jobProcess.PORTNO = _cim.SysPacket.GetItemString();
                    jobProcess.PPID = _cim.SysPacket.GetItemString();
                    jobProcess.CELLCNT = _cim.SysPacket.GetItemString();
                    jobProcess.MESSAGE = _cim.SysPacket.GetItemString();
                    SendMessage2PLC("JOBEVENT1", jobProcess);
                }
                if (RCMD == "11" || RCMD == "12" || RCMD == "13" || RCMD == "14" || RCMD == "41" || RCMD == "42" || RCMD == "43" || RCMD == "44" || RCMD == "45")
                {
                    INTERLOCKMESS interlock = new INTERLOCKMESS();
                    interlock.RCMD = _cim.SysPacket.GetItemString(1);
                    interlock.INTERLOCK = _cim.SysPacket.GetItemString(4);
                    interlock.UNITID = _cim.SysPacket.GetItemString(6);
                    interlock.INTERLOCKID = _cim.SysPacket.GetItemString(7);
                    interlock.MESSAGE = _cim.SysPacket.GetItemString(8);
                    SendMessage2PLC("EQUIPMENTMACHINECONTROL", interlock, "1");//unitid = 1
                    
                }
                switch (RCMD)
                {
                    case "1":   //Equipment Command (Eqp Op-call Send)
                        OPCALLMESS opcall = new OPCALLMESS();
                        opcall.OPCALL = _cim.SysPacket.GetItemString(4);
                        opcall.EQPID = _cim.SysPacket.GetItemString();
                        opcall.OPCALLID = _cim.SysPacket.GetItemString();
                        opcall.MESSAGE = _cim.SysPacket.GetItemString();
                        EqpData.OPCALLS.Add(opcall);
                        SendMessage2PLC("OPERATORCALL", opcall);
                        break;
                    case "2":   //Equipment Command (Eqp Interlock Send)
                        INTERLOCKMESS interlock = new INTERLOCKMESS();
                        interlock.RCMD = _cim.SysPacket.GetItemString(1);
                        interlock.INTERLOCK = _cim.SysPacket.GetItemString(4);
                        interlock.EQPID = _cim.SysPacket.GetItemString();
                        interlock.INTERLOCKID = _cim.SysPacket.GetItemString();
                        interlock.MESSAGE = _cim.SysPacket.GetItemString();
                        
                        SendMessage2PLC("INTERLOCK", interlock);
                        break;
                    case "3":   //Equipment Job Command (Job(=PPID) Select))
                        JobProcess jobSelect = new JobProcess();
                        jobSelect.RCMD = "3";
                        jobSelect.PPID = _cim.SysPacket.GetItemString(4);
                        jobSelect.EQPID = _cim.SysPacket.GetItemString();
                        jobSelect.PORTNO = _cim.SysPacket.GetItemString();
                        jobSelect.MESSAGE = _cim.SysPacket.GetItemString();
                        break;
                    case "4":   //(Job Process Start)

                        break;
                    case "5":   //(Job Process Abort)

                        break;
                    case "6":   //(Job Process Pause)

                        break;
                    case "7":   //(Job Process Resume)
                        break;
                    case "8":   //(Job Process Cancel)
                        break;
                     case "9":   //Equipment Job Command (Job(=PPID) Change Reserve))
                        break;
                    case "10":  //(Function Change)
                        
                        FUNCTION func = new FUNCTION();
                        func.UNITID = _cim.SysPacket.GetItemString(5);
                        func.EFID = _cim.SysPacket.GetItemString();
                        func.EFST = _cim.SysPacket.GetItemString(); 
                        func.MESSAGE = _cim.SysPacket.GetItemString();
                        if(EqpData.EQINFORMATION.EQPID != _cim.SysPacket.GetItemString(2))
                        {
                            HACK = "1";
                            SendS2F42(RCMD, HACK);
                            return;
                        }
                        if (_cim.SysPacket.GetItemString(7) == "")
                        {
                            HACK = "3";
                            SendS2F42(RCMD, HACK);
                            return;
                        }
                        SendMessage2PLC("EQUIPMENTFUNCTIONCHANGECOMMAND", func);
                        Thread.Sleep(500);
                        WordModel word = _plcH.Words.FirstOrDefault(x => x.Area == "EquipFunctionChangeCommand");
                        HACK = word.GetValue(PLC);
                        break;
                    case "11":  //(Transfer Stop)
                        break;
                    case "12":
                        INTERLOCKMESS interlock12 = new INTERLOCKMESS();
                        interlock12.RCMD = _cim.SysPacket.GetItemString(1);
                        interlock12.INTERLOCK = _cim.SysPacket.GetItemString(4);
                        interlock12.EQPID = _cim.SysPacket.GetItemString();
                        interlock12.INTERLOCKID = _cim.SysPacket.GetItemString();
                        interlock12.MESSAGE = _cim.SysPacket.GetItemString();
                        if(interlock12.INTERLOCKID == EqpData.EQINFORMATION.EQPID || interlock12.INTERLOCKID == "")
                        {
                            SendMessage2PLC("INTERLOCK", interlock12);
                            break;
                        }


                        break;
                    case "13":  // (Step Stop)
                        break;
                    case "14":  //(OWN Stop)
                        break;
                    case "15":  //Equipment Command (Control Information)
                        ControlInfoMation controlInfoMation =new ControlInfoMation();
                        controlInfoMation.ACTIONTYPE = _cim.SysPacket.GetItemString(6);
                        controlInfoMation.ACTIONDETAIL = _cim.SysPacket.GetItemString(9);
                        controlInfoMation.ACTION = _cim.SysPacket.GetItemString(12);
                        controlInfoMation.DESCRIPTION = _cim.SysPacket.GetItemString(15);
                        SendMessage2PLC("EQUIPMENTCONTROLINFORMATION", controlInfoMation);

                        break;
                    case "16":  //(Unit Op-call Send)
                        OPCALLMESS opcallUnit = new OPCALLMESS();
                        opcallUnit.OPCALL = _cim.SysPacket.GetItemString(4);
                        opcallUnit.EQPID = _cim.SysPacket.GetItemString();
                        opcallUnit.OPCALLID = _cim.SysPacket.GetItemString();
                        opcallUnit.MESSAGE = _cim.SysPacket.GetItemString();
                        break;
                    case "17":  //(Equipment Configuration File Change By Host)
                        ConfigFileChange config = new ConfigFileChange();
                        config.PRODUCTID = _cim.SysPacket.GetItemString(4);
                        config.ACTIONTYPE = _cim.SysPacket.GetItemString();
                        config.ACTIONRESULT = _cim.SysPacket.GetItemString();
                        int lst = int.Parse(_cim.SysPacket.GetItemString());
                        for (int i = 0; i < lst; i++)
                        {
                            FILE file = new FILE();
                            string count = _cim.SysPacket.GetItemString();
                            file.FILETYPE = _cim.SysPacket.GetItemString();
                            file.FILENAME = _cim.SysPacket.GetItemString();
                            file.FILEPATH = _cim.SysPacket.GetItemString();
                            file.LOCALCHECKSUM= _cim.SysPacket.GetItemString();
                            file.CURRENTCHECKSUM= _cim.SysPacket.GetItemString();
                            config.FILES.Add(file);
                        }
                        
                        
                       
                        break;
                    case "41":  //(Force Transfer Stop)
                        break;
                    case "42":  //(Force Loading Stop)
                        break;
                    case "43":  //(Force Step Stop)
                        break;
                    case "44":  //(Force OWN Stop)
                        break;
                    case "45":  //(Force Interlock Release)
                        break;

                    default:
                        break;
                }
                 SendS2F42(RCMD, HACK);
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
