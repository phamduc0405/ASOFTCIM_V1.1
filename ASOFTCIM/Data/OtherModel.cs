using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    
    public class SPBINFOR
    {
        public string SBPLABELTYPE { get; set; }
        public string SBPLABELURL { get; set; }
        public string SBPUPPERWEIGTH { get; set; }
        public string SBPLOWERWEIGTH { get; set; }
        public string SBPREALWEIGTH { get; set; }
    }
    public class CARTONINFOR
    {
        public string CARTONID { get; set; }
        public string CARTONLABELTYPE { get; set; }
        public string CARTONLABELURL { get; set; }
        public string CARTONREALWEIGTH { get; set; }
    }
    public class ETCINFOR
    {
        public string REPAIRFLAG { get; set; }
        public string REWORKFLAG { get; set; }
        public string ETCLABELUSEYN { get; set; }
        public string ETCLABELURL { get; set; }
    }


}
