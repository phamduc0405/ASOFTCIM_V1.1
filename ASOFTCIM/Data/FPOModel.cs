using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    public class FPOINFODOWNLOAD
    {
        public string EQPID { get; set; }
        public string CELLID { get; set; }
        public string PRODUCTID { get; set; }
        public string FPOID { get; set; }
        public string SFPOID { get; set; }
        public string FPO_SIZE { get; set; }
        public string FPO_QTY { get; set; }
        public string SFPO_SIZE { get; set; }
        public string SFPO_QTY { get; set; }
        public string SAMPLE_SIZE { get; set; }
        public string SAMPLE_QTY { get; set; }
        public string REPLYSTATUS { get; set; }
        public string REPLYTEXT { get; set; }
        public List<ITEM> ITEMS { get; set; }
    }

    public class FPOCREATEREQUEST
    {
        public string EQPID { get; set; }
        public string CELLID { get; set; }
        public string PRODUCTID { get; set; }
        public string FPOID { get; set; }
        public string SFPOID { get; set; }
        public string SAMPLEQTY { get; set; }
        public string FPO_TYPE { get; set; }
        public string SHIFTINFO { get; set; }
        public string OPERATORID1 { get; set; }
        public string OPERATORID2 { get; set; }
        public string FPO_COMP { get; set; }
        public string JUDGE { get; set; }
        public string REASONCODE { get; set; }
        public List<ITEM> ITEMS { get; set; }
    }
}
