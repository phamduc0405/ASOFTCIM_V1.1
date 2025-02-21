using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ASOFTCIM.Data
{
    public class CELLLOTINFODOWNLOAD
    {
        public string CELLID { get; set; }
        public string CASSETTEID { get; set; }
        public string BATCHLOT { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCT_TYPE { get; set; }
        public string PRODUCT_KIND { get; set; }
        public string PRODUCTSPEC { get; set; }
        public string STEPID { get; set; }
        public string PPID { get; set; }
        public string CELL_SIZE { get; set; }
        public string CELL_THICKNESS { get; set; }
        public string COMMENT { get; set; }

        [Description("ITEMS Array")]
        [XmlArray("ITEMS")]
        [XmlArrayItem("ITEM", typeof(ITEM))]
        public List<ITEM> ITEMS { get; set; }

    }
    public class BODYCellInfor
    {
        public string CELLID { get; set; }
        public string PRODUCTID { get; set; }
        public string CELLINFORESULT { get; set; }
    }
}
