using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ASOFTCIM.Data
{
    public class EQPSPECIFICREPORT
    {
        [System.Xml.Serialization.XmlElement("CELLID")]
        public string CELLID { get; set; }

        [System.Xml.Serialization.XmlElement("PPID")]
        public string PPID { get; set; }

        [System.Xml.Serialization.XmlElement("PRODUCTID")]
        public string PRODUCTID { get; set; }

        [System.Xml.Serialization.XmlElement("STEPID")]
        public string STEPID { get; set; }

        [System.Xml.Serialization.XmlElement("OPTIONINFO")]
        public string OPTIONINFO { get; set; }

        [System.Xml.Serialization.XmlElement("DESCRIPTION")]
        public string DESCRIPTION { get; set; }

        [XmlArray("ITEMS")]
        [XmlArrayItem("ITEM", typeof(ITEM))]
        public List<ITEM> ITEMs { get; set; }

        public string EQPID { get; set; }
    }
    /// <summary>
    ///  Equipment Status Change By User
    ///  CEID : 603
    /// </summary>
    public class EQUIPSTATUSCHANGE
    {
        public RPTID802 RPTID802 { get; set; }
        public RPTID803 RPTID803 { get; set; }
        public RPTID804 RPTID804 { get; set; }
    }
    /// <summary>
    /// T:RPTID 802/RPTID802
    /// </summary>
    [Serializable()]
    public class RPTID802
    {
        [System.Xml.Serialization.XmlElement("EQPID")]
        public string EQPID { get; set; }

        [System.Xml.Serialization.XmlElement("DATA_TYPE")]
        public string DATA_TYPE { get; set; }
    }

    /// <summary> 
    /// T:RPTID 803/PLC Data
    /// PLC Data
    /// </summary>
    [Serializable()]
    public class RPTID803
    {
        [System.Xml.Serialization.XmlElement("ADDRESS")]
        public string ADDRESS { get; set; }

        [System.Xml.Serialization.XmlElement("VALUE")]
        public string VALUE { get; set; }
    }

    /// <summary> 
    /// T:RPTID 804/PC Data
    /// PC Data
    /// </summary>
    [Serializable()]
    public class RPTID804
    {
        [System.Xml.Serialization.XmlElement("LOSSDISPLAY")]
        public string LOSSDISPLAY { get; set; }

        [System.Xml.Serialization.XmlElement("LOSS")]
        public string LOSS { get; set; }
    }
}
