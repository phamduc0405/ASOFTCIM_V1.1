﻿using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using ASOFTCIM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Config
{

    public class EquipmentConfig : ACopy
    {
        public int EqpIndex { get; set; } = 0;
        public string EQPID { get; set; } = string.Empty;
        public string CRST { get; set; } = string.Empty;
        public string EqpName { get; set; } = string.Empty;
        public string AliveTime { get; set; } = "1000";
        public bool UseLogFDC { get; set; } = false;
        public string LogFolder { get; set; } = string.Empty;
        public string LogFDC { get; set; } = string.Empty;
        public PLCHelper PLCHelper { get; set; } = new PLCHelper();
        public PLCConfig PLCConfig { get; set; } = new PLCConfig();
        public string LineName { get; set; } = "";
        public CimConfig CimConfig { get; set; } = new CimConfig();
    }
    public class DefineConst
    {
        public const int ShortBits = sizeof(short) * 8;

    }
    public class CimConfig
    {
        public string ConnectMode { get; set; } = "0";
        public string IP { get; set; } = "127.0.0.1";
        public string Port { get; set; } = "8000";

    }
}
