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
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS2F41(SysPacket sysPacket)
        {
            try
            {
                string HACK = "0";
                string RCMD = sysPacket.GetItemString(1);
                if (RCMD == "4" || RCMD == "5" || RCMD == "6" || RCMD == "7" || RCMD == "8")
                {
                    JobProcess jobProcess = new JobProcess();
                    jobProcess.RCMD = RCMD;
                    jobProcess.EQPID = sysPacket.GetItemString(1);
                    jobProcess.PARENTLOT = sysPacket.GetItemString(4);
                    jobProcess.RFID = sysPacket.GetItemString();
                    jobProcess.EQPID = sysPacket.GetItemString();
                    jobProcess.PORTNO = sysPacket.GetItemString();
                    jobProcess.PPID = sysPacket.GetItemString();
                    jobProcess.CELLCNT = sysPacket.GetItemString();
                    jobProcess.MESSAGE = sysPacket.GetItemString();
                    SendMessage2PLC("JOBEVENT1", jobProcess);
                }
                if (RCMD == "11" || RCMD == "12" || RCMD == "13" || RCMD == "14" || RCMD == "41" || RCMD == "42" || RCMD == "43" || RCMD == "44" || RCMD == "45")
                {
                    INTERLOCKMESS interlock = new INTERLOCKMESS();
                    interlock.RCMD = sysPacket.GetItemString(1);
                    interlock.INTERLOCK = sysPacket.GetItemString(4);
                    interlock.UNITID = sysPacket.GetItemString(6);
                    interlock.INTERLOCKID = sysPacket.GetItemString(6);
                    interlock.MESSAGE = sysPacket.GetItemString(7);
                    if (interlock.UNITID == EqpData.EQINFORMATION.EQPID || interlock.INTERLOCKID == "")
                    {
                        SendMessage2PLC("INTERLOCK", interlock);
                    }
                    else
                    {
                        SendMessage2PLC("EQUIPMENTMACHINECONTROL", interlock, "1");//unitid = 1
                        Thread.Sleep(500);
                        WordModel word = _plcH.Words.FirstOrDefault(x => x.Area == "EquipControlInformation");
                        HACK = word.GetValue(PLC);
                    }

                }
                switch (RCMD)
                {
                    case "1":   //Equipment Command (Eqp Op-call Send)
                        OPCALLMESS opcall = new OPCALLMESS();
                        opcall.OPCALL = sysPacket.GetItemString(4);
                        opcall.EQPID = sysPacket.GetItemString();
                        opcall.OPCALLID = sysPacket.GetItemString();
                        opcall.MESSAGE = sysPacket.GetItemString();
                        EqpData.OPCALLS.Add(opcall);
                        SendMessage2PLC("OPERATORCALL", opcall);
                        break;
                    case "2":   //Equipment Command (Eqp Interlock Send)
                        INTERLOCKMESS interlock = new INTERLOCKMESS();
                        interlock.RCMD = sysPacket.GetItemString(1);
                        interlock.INTERLOCK = sysPacket.GetItemString(4);
                        interlock.EQPID = sysPacket.GetItemString();
                        interlock.INTERLOCKID = sysPacket.GetItemString(6);
                        interlock.MESSAGE = sysPacket.GetItemString(7);
                        EqpData.INTERLOCKS.Add(interlock);
                        SendMessage2PLC("INTERLOCK", interlock);
                        break;
                    case "3":   //Equipment Job Command (Job(=PPID) Select))
                        JobProcess jobSelect = new JobProcess();
                        jobSelect.RCMD = "3";
                        jobSelect.PPID = sysPacket.GetItemString(4);
                        jobSelect.EQPID = sysPacket.GetItemString();
                        jobSelect.PORTNO = sysPacket.GetItemString();
                        jobSelect.MESSAGE = sysPacket.GetItemString();
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
                        func.UNITID = sysPacket.GetItemString(5);
                        func.EFID = sysPacket.GetItemString();
                        func.EFST = sysPacket.GetItemString();
                        func.MESSAGE = sysPacket.GetItemString();
                        if (EqpData.EQINFORMATION.EQPID != sysPacket.GetItemString(2).Trim())
                        {
                            HACK = "1";
                            SendS2F42(RCMD, HACK);
                            return;
                        }
                        if (sysPacket.GetItemString(7) == "")
                        {
                            HACK = "3";
                            SendS2F42(RCMD, HACK);
                            return;
                        }
                        if (sysPacket.GetItemString(6) == "")
                        {
                            HACK = "2";
                            SendS2F42(RCMD, HACK);
                            return;
                        }
                        //251126 NamPham them ACK Funstion mới có 7,11,12,13 
                        if ((func.EFID == "7" && func.EFST != "MANU" && func.EFST != "AUTO") ||
                            (func.EFID == "11" && func.EFST != "ON" && func.EFST != "OFF") ||
                            (func.EFID == "12" && func.EFST != "ON" && func.EFST != "OFF") ||
                            (func.EFID == "13" && func.EFST != "ON" && func.EFST != "OFF"))
                        {
                            HACK = "2";
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
                        interlock12.RCMD = sysPacket.GetItemString(1);
                        interlock12.INTERLOCK = sysPacket.GetItemString(4);
                        interlock12.EQPID = sysPacket.GetItemString();
                        interlock12.INTERLOCKID = sysPacket.GetItemString();
                        interlock12.MESSAGE = sysPacket.GetItemString();
                        if (interlock12.INTERLOCKID == EqpData.EQINFORMATION.EQPID || interlock12.INTERLOCKID != null)
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
                        ControlInfoMation controlInfoMation = new ControlInfoMation();
                        controlInfoMation.ACTIONTYPE = sysPacket.GetItemString(6);
                        controlInfoMation.ACTIONDETAIL = sysPacket.GetItemString(9);
                        controlInfoMation.ACTION = sysPacket.GetItemString(12);
                        controlInfoMation.DESCRIPTION = sysPacket.GetItemString(15);
                        SendMessage2PLC("EQUIPMENTCONTROLINFORMATION", controlInfoMation);

                        break;
                    case "16":  //(Unit Op-call Send)
                        OPCALLMESS opcallUnit = new OPCALLMESS();
                        opcallUnit.OPCALL = sysPacket.GetItemString(4);
                        opcallUnit.EQPID = sysPacket.GetItemString();
                        opcallUnit.OPCALLID = sysPacket.GetItemString();
                        opcallUnit.MESSAGE = sysPacket.GetItemString();
                        break;
                    case "17":  //(Equipment Configuration File Change By Host)
                        ConfigFileChange config = new ConfigFileChange();
                        config.PRODUCTID = sysPacket.GetItemString(4);
                        config.ACTIONTYPE = sysPacket.GetItemString();
                        config.ACTIONRESULT = sysPacket.GetItemString();
                        int lst = int.Parse(sysPacket.GetItemString());
                        for (int i = 0; i < lst; i++)
                        {
                            FILE file = new FILE();
                            string count = sysPacket.GetItemString();
                            file.FILETYPE = sysPacket.GetItemString();
                            file.FILENAME = sysPacket.GetItemString();
                            file.FILEPATH = sysPacket.GetItemString();
                            file.LOCALCHECKSUM = sysPacket.GetItemString();
                            file.CURRENTCHECKSUM = sysPacket.GetItemString();
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
                SendS9F7(sysPacket);
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
        }
    }
}
