using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ASOFTCIM.Data
{
    public class PPIDChange
    {
        public string PPID { get; set; }
        public string PPID_TYPE { get; set; }
        public string OLD_PPID { get; set; }
        public string EQPID { get; set; }
    }

    public class PPIDChangeParameter
    {
        [System.Xml.Serialization.XmlElement("PPST")]
        public PPST PPST { get; set; }

        [XmlArray("PARAMS")]
        [XmlArrayItem("PARAM", typeof(PARAM))]
        public PARAM[] PARAMs { get; set; }

        public string EQPID { get; set; }
    }
    public class PPIDList
    {
        public string PPID_TYPE { get; set; }
        [XmlArray("PPIDS")]
        [XmlArrayItem("PPID")]
        public List<string> PPID { get; set; }
    }
    public class PPPARAMS
    {
        public string PPID { get; set; }
        public string PPID_TYPE { get; set; }
        public string SOFTREV { get; set; }
        public string CCODE { get; set; }
        public List<PARAM> PARAMS { get; set; }
    }
    public class PPST
    {
        [System.Xml.Serialization.XmlElement("PPID")]
        public string PPID { get; set; }
        [System.Xml.Serialization.XmlElement("PPIDST")]
        public string PPIDST { get; set; }
        public override string ToString()
        {
            return PPID;
        }
    }
    public class PARAM
    {
        [System.Xml.Serialization.XmlElement("PARAMNAME")]
        public string PARAMNAME { get; set; }
        [System.Xml.Serialization.XmlElement("PARAMVALUE")]
        public string PARAMVALUE { get; set; }
        public override string ToString()
        {
            return PARAMNAME;
        }
    }

    public class PPIDINFOR
    {
        public string MODE { get; set; }
        public string EQPID { get; set; }
        public string UNITID { get; set; }
        public string PPID { get; set; }
        public string PPID_TYPE { get; set; }
        public string PPID_NUMBER { get; set; }
        public List<COMMANDCODE> COMMANDCODEs { get; set; }
    }
}
