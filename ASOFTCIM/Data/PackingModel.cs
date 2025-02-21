using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    /// <summary> 
    /// T:RPTID 820/Inspection Result
    /// </summary>
    [Serializable()]
    public class PACKING
    {
        [System.Xml.Serialization.XmlElement("SBPID")]
        public string SBPID { get; set; }

        [System.Xml.Serialization.XmlElement("SBPREALWEIGHT")]
        public string SBPREALWEIGHT { get; set; }
        [System.Xml.Serialization.XmlElement("CARTONID")]
        public string CARTONID { get; set; }
        [System.Xml.Serialization.XmlElement("CARTONREALWEIGHT")]
        public string CARTONREALWEIGHT { get; set; }

        [System.Xml.Serialization.XmlElement("CHECKERNAME")]
        public string CHECKERNAME { get; set; }
        [System.Xml.Serialization.XmlElement("ERRORMESSAGE")]
        public string ERRORMESSAGE { get; set; }

    }

    public class TRAYPACKING
    {
        public string PACKEQPID { get; set; }
        public string PRODUCTID { get; set; }
        public string PPID { get; set; }
        public string PACKLABELID { get; set; }
        public string BYWHO { get; set; }
        public string OPERATORID { get; set; }
        public string ITEM_1COUNT { get; set; }
        public string ITEM_2COUNT { get; set; }
        public List<ITEM> ITEM_1 { get; set; }
        public List<ITEM> ITEM_2 { get; set; }
    }

    public class PACKINGINFOR
    {
        public string ERRORMESSAGE { get; set; }
        public string SBPID { get; set; }
        public string CHECKERNAME { get; set; }
        public string SHIPMENTTYPE { get; set; }
        public string CUSTOMERID { get; set; }
        public string CELLSIZE { get; set; }
        public string ASSY_CODE { get; set; }
        public string PRODUCTQUANTITY { get; set; }
    }
}
