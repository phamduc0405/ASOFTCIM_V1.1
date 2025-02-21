using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    public class CASSETTLE
    {
        public string CASSETTLEID { get; set; } = String.Empty;
        public string CASSETTETYPE { get; set; } = String.Empty;
        public string BATCHLOT { get; set; } = String.Empty;
        public string BATCHLOTQTY { get; set; } = String.Empty;
        public string PRODUCTID { get; set; } = String.Empty;
        public string PRODUCT_TYPE { get; set; } = String.Empty;
        public string PRODUCT_KIND { get; set; } = String.Empty;
        public string PRODUCTSPEC { get; set; } = String.Empty;
        public string PPID { get; set; } = String.Empty;
        public string STEPID { get; set; } = String.Empty;
        public string COMMENT { get; set; } = String.Empty;
        public List<string> CELLIDS { get; set; } = new List<string>();
    }
    public class CASSETTESTATECHANGE
    {
        [System.Xml.Serialization.XmlElement("PORTID")]
        public string PORTID { get; set; }
        [System.Xml.Serialization.XmlElement("CEID")]
        public string CEID { get; set; }

        [System.Xml.Serialization.XmlElement("PORTAVAILABLESTATE")]
        public string PORTAVAILABLESTATE { get; set; }
        [System.Xml.Serialization.XmlElement("PORTACCESSMODE")]
        public string PORTACCESSMODE { get; set; }

        [System.Xml.Serialization.XmlElement("PORTTRANSFERSTATE")]
        public string PORTTRANSFERSTATE { get; set; }
        [System.Xml.Serialization.XmlElement("PORTPROCESSINGSTATE")]
        public string PORTPROCESSINGSTATE { get; set; }

        [System.Xml.Serialization.XmlElement("JOBID")]
        public string JOBID { get; set; }
        [System.Xml.Serialization.XmlElement("JOBTYPE")]
        public string JOBTYPE { get; set; }

    }

    public class CASSETTEUNITPROCESS
    {
        public CASSETTEUNIT CASSETTEUNIT { get; set; }
    }
    /// <summary>
    /// T:RPTID 303 /CASSETTEUNIT
    /// </summary>
    [Serializable()]
    public class CASSETTEUNIT
    {
        [System.Xml.Serialization.XmlElement("PRODUCTID")]
        public string PRODUCTID { get; set; }

        [System.Xml.Serialization.XmlElement("PPID")]
        public string PPID { get; set; }

        [System.Xml.Serialization.XmlElement("CASSETTEID")]
        public string CASSETTEID { get; set; }
        [System.Xml.Serialization.XmlElement("CASSETTETYPE")]
        public string CASSETTETYPE { get; set; }

        [System.Xml.Serialization.XmlElement("ITEMCOUNT1")]
        public string ITEMCOUNT1 { get; set; }

        [System.Xml.Serialization.XmlElement("ITEMCOUNT2")]
        public string ITEMCOUNT2 { get; set; }
        [System.Xml.Serialization.XmlElement("FROMUNITID")]
        public string FROMUNITID { get; set; }

        [System.Xml.Serialization.XmlElement("TOUNITID")]
        public string TOUNITID { get; set; }
    }
}
