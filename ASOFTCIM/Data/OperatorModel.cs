using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    /// <summary>
    /// T:RPTID 105 106 /Operator Information
    /// </summary>
    [Serializable()]
    public class OPERATOR
    {
        [System.Xml.Serialization.XmlElement("EQPID")]
        public string EQPID { get; set; }
        [System.Xml.Serialization.XmlElement("OPTIONINFO")]
        public string OPTIONINFO { get; set; }
        [System.Xml.Serialization.XmlElement("COMMENT")]
        public string COMMENT { get; set; }
        [System.Xml.Serialization.XmlElement("OPERATORID")]
        public string OPERATORID { get; set; }
        [System.Xml.Serialization.XmlElement("PASSWORD")]
        public string PASSWORD { get; set; }
    }
}
