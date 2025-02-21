using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    /// <summary>
    /// T:RPTID 308/APN
    /// </summary>
    [Serializable()]
    public class APN
    {
        [System.Xml.Serialization.XmlElement("CELLID")]
        public string CELLID { get; set; }
        [System.Xml.Serialization.XmlElement("PAIRCELLID")]
        public string PAIRCELLID { get; set; }

        [System.Xml.Serialization.XmlElement("CELLTYPE")]
        public string CELLTYPE { get; set; }
        [System.Xml.Serialization.XmlElement("INDEX")]
        public string INDEX { get; set; }
        [System.Xml.Serialization.XmlElement("OPTIONINFO")]
        public string OPTIONINFO { get; set; }
        [System.Xml.Serialization.XmlElement("PPID")]
        public string PPID { get; set; }
        [System.Xml.Serialization.XmlElement("PRODUCTID")]
        public string PRODUCTID { get; set; }
        [System.Xml.Serialization.XmlElement("STEPID")]
        public string STEPID { get; set; }
        [System.Xml.Serialization.XmlElement("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
    }
}
