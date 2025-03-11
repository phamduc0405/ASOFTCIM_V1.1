using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    public class Alarm : ACopy
    {
        public string TIME { get; set; } = "0";
        [Browsable(false)]
        public uint ALNO { get; set; } = 0;   // Alarm Number

        public uint ALID { get; set; } = 0;  // Alarm ID (User Define)
        [Browsable(false)]
        public string ALST { get; set; } = "0"; // Alarm Status 1 Set, 2 Reset

        public string ALCD { get; set; } = "0"; // Alarm Code 1 Light Alarm, 2 Serious Alarm
        [Browsable(false)]
        public string ALED { get; set; } = "0";  // Alarm Enable Disable 0 Enable, 1 Disable
        [Browsable(false)]
        public string ALARMIDCALC { get; set; } = "0";
        [Browsable(false)]
        public string EQPID { get; set; } = "0";
        [Browsable(false)]
        public string UNITID { get; set; } = "0";
        public string MODULEID { get; set; } = "0";
        public string ALTEXT { get; set; } = "0";  //  Alarm Text (User Define)

    }
}
