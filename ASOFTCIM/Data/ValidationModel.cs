using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    public class Validation
    {
        public string CARRIERID { get; set; } = string.Empty;
        public string UNIQUEID { get; set; } = string.Empty;
        public string UNIQUETYPE { get; set; } = string.Empty;
        public string PRODUCTID { get; set; } = string.Empty;
        public string PRODUCTSPEC { get; set; } = string.Empty;
        public string PRODUCT_TYPE { get; set; } = string.Empty;
        public string PRODUCT_KIND { get; set; } = string.Empty;
        public string PPID { get; set; } = string.Empty;
        public string STEPID { get; set; } = string.Empty;
        public string CELL_SIZE { get; set; } = string.Empty;
        public string CELL_THICKNESS { get; set; } = string.Empty;
        public string CELLINFORESULT { get; set; } = string.Empty;
        public string INS_COUNT { get; set; } = string.Empty;
        public string COMMENT { get; set; } = string.Empty;
        public List<Tuple<string, string>> ITEMS { get; set; } = new List<Tuple<string, string>>();
        public REPLY REPLY { get; set; } = new REPLY();
    }
    public class SPECIFICVALIDATIONREQUEST
    {
        public string OPTIONCODE { get; set; } = string.Empty;
        public string UNIQUEID { get; set; } = string.Empty;
        public string OPTIONINFO { get; set; } = string.Empty;
    }
}
