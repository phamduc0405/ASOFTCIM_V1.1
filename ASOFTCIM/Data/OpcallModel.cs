using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ASOFTCIM.Data
{
    /// <summary>
    /// T:RPTID 700/OPCALL
    /// </summary>
    [XmlRoot("OPCALL")]
    public class OPCALL
    {
        [System.Xml.Serialization.XmlElement("OPCALLID")]
        public string OPCALLID { get; set; }

        [System.Xml.Serialization.XmlElement("MESSAGE")]
        public string MESSAGE { get; set; }
    }
    public class OPCALLCONFIRM
    {
        [XmlArray("OPCALLS")]
        [XmlArrayItem("OPCALL", typeof(OPCALL))]
        public List<OPCALL> OPCALLs { get; set; }
        [XmlIgnore]
        internal CELLINFOR CELL { get; set; }
    }

    public class UNITOPCALLCONFIRM
    {
        public EQPSTATE UNIT { get; set; }
        public List<OPCALL> OPCALLs { get; set; }
        public CELLINFOR CELL { get; set; }
    }
    public class OPCALLCONFIRMEVENT
    {
        [XmlArray("OPCALLS")]
        [XmlArrayItem("OPCALL", typeof(OPCALL))]
        public List<OPCALL> OPCALLs { get; set; }


    }
}
