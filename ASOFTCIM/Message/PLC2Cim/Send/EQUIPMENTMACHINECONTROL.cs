﻿using A_SOFT.CMM.INIT;
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
    public class EQUIPMENTMACHINECONTROL
    {
        public EQUIPMENTMACHINECONTROL(PLCHelper plcdata, INTERLOCKMESS interlock,string UnitID)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name + UnitID).ToList();

                word.FirstOrDefault(x => x.Item == "RCMD").SetValue = interlock.RCMD;
                word.FirstOrDefault(x => x.Item == "INTERLOCK").SetValue = interlock.INTERLOCK;
                word.FirstOrDefault(x => x.Item == "INTERLOCKID").SetValue = interlock.INTERLOCKID;
                word.FirstOrDefault(x => x.Item == "INTERLOCKMESSAGE").SetValue = interlock.MESSAGE;

                BitModel bit = plcdata.Bits.First(x =>x.Comment == this.GetType().Name + UnitID);
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }

    public class EQUIPMENTMACHINECONTROL2
    {
        public EQUIPMENTMACHINECONTROL2(PLCHelper plcdata, INTERLOCKMESS interlock, string UnitID)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name + UnitID).ToList();

                word.FirstOrDefault(x => x.Item == "RCMD").SetValue = interlock.RCMD;
                word.FirstOrDefault(x => x.Item == "INTERLOCK").SetValue = interlock.INTERLOCK;
                word.FirstOrDefault(x => x.Item == "INTERLOCKID").SetValue = interlock.INTERLOCKID;
                word.FirstOrDefault(x => x.Item == "INTERLOCKMESSAGE").SetValue = interlock.MESSAGE;

                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name + UnitID);
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    }

    public class EQUIPMENTMACHINECONTROL3
    {
        public EQUIPMENTMACHINECONTROL3(PLCHelper plcdata, INTERLOCKMESS interlock, string UnitID)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name + UnitID).ToList();

                word.FirstOrDefault(x => x.Item == "RCMD").SetValue = interlock.RCMD;
                word.FirstOrDefault(x => x.Item == "INTERLOCK").SetValue = interlock.INTERLOCK;
                word.FirstOrDefault(x => x.Item == "INTERLOCKID").SetValue = interlock.INTERLOCKID;
                word.FirstOrDefault(x => x.Item == "INTERLOCKMESSAGE").SetValue = interlock.MESSAGE;

                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name + UnitID);
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    }
    public class EQUIPMENTMACHINECONTROL4
    {
        public EQUIPMENTMACHINECONTROL4(PLCHelper plcdata, INTERLOCKMESS interlock, string UnitID)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name + UnitID).ToList();

                word.FirstOrDefault(x => x.Item == "RCMD").SetValue = interlock.RCMD;
                word.FirstOrDefault(x => x.Item == "INTERLOCK").SetValue = interlock.INTERLOCK;
                word.FirstOrDefault(x => x.Item == "INTERLOCKID").SetValue = interlock.INTERLOCKID;
                word.FirstOrDefault(x => x.Item == "INTERLOCKMESSAGE").SetValue = interlock.MESSAGE;

                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name + UnitID);
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    }
    public class EQUIPMENTMACHINECONTROL5
    {
        public EQUIPMENTMACHINECONTROL5(PLCHelper plcdata, INTERLOCKMESS interlock, string UnitID)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name + UnitID).ToList();

                word.FirstOrDefault(x => x.Item == "RCMD").SetValue = interlock.RCMD;
                word.FirstOrDefault(x => x.Item == "INTERLOCK").SetValue = interlock.INTERLOCK;
                word.FirstOrDefault(x => x.Item == "INTERLOCKID").SetValue = interlock.INTERLOCKID;
                word.FirstOrDefault(x => x.Item == "INTERLOCKMESSAGE").SetValue = interlock.MESSAGE;

                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name + UnitID);
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    }
    public class EQUIPMENTMACHINECONTROL6
    {
        public EQUIPMENTMACHINECONTROL6(PLCHelper plcdata, INTERLOCKMESS interlock, string UnitID)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name + UnitID).ToList();

                word.FirstOrDefault(x => x.Item == "RCMD").SetValue = interlock.RCMD;
                word.FirstOrDefault(x => x.Item == "INTERLOCK").SetValue = interlock.INTERLOCK;
                word.FirstOrDefault(x => x.Item == "INTERLOCKID").SetValue = interlock.INTERLOCKID;
                word.FirstOrDefault(x => x.Item == "INTERLOCKMESSAGE").SetValue = interlock.MESSAGE;

                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name + UnitID);
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    }
    public class EQUIPMENTMACHINECONTROL7
    {
        public EQUIPMENTMACHINECONTROL7(PLCHelper plcdata, INTERLOCKMESS interlock, string UnitID)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name + UnitID).ToList();

                word.FirstOrDefault(x => x.Item == "RCMD").SetValue = interlock.RCMD;
                word.FirstOrDefault(x => x.Item == "INTERLOCK").SetValue = interlock.INTERLOCK;
                word.FirstOrDefault(x => x.Item == "INTERLOCKID").SetValue = interlock.INTERLOCKID;
                word.FirstOrDefault(x => x.Item == "INTERLOCKMESSAGE").SetValue = interlock.MESSAGE;

                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name + UnitID);
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    }
    public class EQUIPMENTMACHINECONTROL8
    {
        public EQUIPMENTMACHINECONTROL8(PLCHelper plcdata, INTERLOCKMESS interlock, string UnitID)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name + UnitID).ToList();

                word.FirstOrDefault(x => x.Item == "RCMD").SetValue = interlock.RCMD;
                word.FirstOrDefault(x => x.Item == "INTERLOCK").SetValue = interlock.INTERLOCK;
                word.FirstOrDefault(x => x.Item == "INTERLOCKID").SetValue = interlock.INTERLOCKID;
                word.FirstOrDefault(x => x.Item == "INTERLOCKMESSAGE").SetValue = interlock.MESSAGE;

                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name + UnitID);
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
