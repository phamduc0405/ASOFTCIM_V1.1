using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ASOFTCIM.Data
{
    public class CARRIERCHANGE
    {
        public string PARENTLOT { get; set; } = string.Empty;
        public string RFID { get; set; } = string.Empty;
        public string PORTNO_1 { get; set; } = string.Empty;
        public string PPID { get; set; } = string.Empty;
        public string PLANQTY { get; set; } = string.Empty;
        public string PROCESSEDQTY { get; set; } = string.Empty;
        public PORTSTATE PORTSTATE = new PORTSTATE();
    }

    public class CARRIERPROCESSCHANGE
    {
        [System.Xml.Serialization.XmlElement("CEID")]
        public string CEID { get; set; } = "";
        [System.Xml.Serialization.XmlElement("CARRIERID")]
        public string CARRIERID { get; set; } = "";
        [System.Xml.Serialization.XmlElement("CARRIERTYPE")]
        public string CARRIERTYPE { get; set; } = "";
        [System.Xml.Serialization.XmlElement("CARRIERPPID")]
        public string CARRIERPPID { get; set; } = "";
        [System.Xml.Serialization.XmlElement("CARRIERPRODUCT")]
        public string CARRIERPRODUCT { get; set; } = "";

        [System.Xml.Serialization.XmlElement("CARRIERSTEPID")]
        public string CARRIERSTEPID { get; set; } = "";
        [System.Xml.Serialization.XmlElement("CARRIER_S_COUNT")]
        public string CARRIER_S_COUNT { get; set; } = "";

        [System.Xml.Serialization.XmlElement("CARRIER_C_COUNT")]
        public string CARRIER_C_COUNT { get; set; } = "";
        [System.Xml.Serialization.XmlElement("PORTNO")]
        public string PORTNO { get; set; } = "";

        [Description("SUBCARRIERINFOSET Array")]
        [XmlArray("SUBCARRIERINFOSETS")]
        [XmlArrayItem("ITEM")]
        public List<SUBCARRIER> SUBCARRIERS { get; set; }
    }
    public class CARRIERINFODOWNLOAD
    {
        public string CARRIERID { get; set; }
        public string CARRIERTYPE { get; set; }
        public string CARRIERPPID { get; set; }
        public string CARRIERPRODUCT { get; set; }
        public string CARRIERSTEPID { get; set; }
        public string CARRIER_S_COUNT { get; set; }
        public string CARRIER_C_COUNT { get; set; }
        public string PORTNO { get; set; }
        public List<SUBCARRIER> SUBCARRIERS { get; set; }
        public REPLY REPLY { get; set; }
    }
    public class SUBCARRIER
    {
        public string SUBCARRIERID { get; set; }
        public string CELLQTY { get; set; }
        public List<CELLINFO> CELLSINFOR { get; set; }
    }
    public class CELLINFO
    {
        public string CELLID { get; set; }
        public string LOCATIONNO { get; set; }
        public string JUDGE { get; set; }
        public string REASONCODE { get; set; }

    }
}
