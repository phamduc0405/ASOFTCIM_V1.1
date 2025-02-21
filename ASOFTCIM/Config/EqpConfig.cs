using A_SOFT.CMM.INIT;
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
        public string EqpName { get; set; } = string.Empty;
        public PLCHelper PLCHelper { get; set; } = new PLCHelper();
        public PLCConfig PLCConfig { get; set; } = new PLCConfig();
        public string LineName { get; set; }
    }
    public class DefineConst
    {
        public const int ShortBits = sizeof(short) * 8;

    }
}
