using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    public class INQUIRY
    {
        public string EQPID { get; set; }
        public string PRODUCTID { get; set; }
        public string ACTIONFLAG { get; set; }
        public List<INQUIRYDATA> INQUIRYDATA { get; set; }

    }
    public class INQUIRYDATA
    {
        public string DATA_TYPE { get; set; }
        public string ITEMNAME { get; set; }
        public string ITEMVALUE { get; set; }
        public string CHECKSUM { get; set; }
        public string REFERENCE { get; set; }
        public string EAC { get; set; }
    }

    public class INQUIRYFORM
    {
        public string DATA_TYPE { get; set; }
        public List<ITEM> ITEMs { get; set; }
    }
}
