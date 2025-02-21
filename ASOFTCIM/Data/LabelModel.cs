using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    public class LABELINFODOWNLOAD
    {
        public string CELLID { get; set; }
        public string PRODUCTID { get; set; }
        public string LABELID { get; set; }
        public string REPLYSTATUS { get; set; }
        public string REPLYCODE { get; set; }
        public string REPLYTEXT { get; set; }
    }
    public class LABELINFORMATIONREQUEST
    {
        public string OPTIONCODE { get; set; }
        public string CELLID { get; set; }
        public string PRODUCTID { get; set; }
        public string OPTIONINFO { get; set; }
        public string LABELID { get; set; }
    }
}
