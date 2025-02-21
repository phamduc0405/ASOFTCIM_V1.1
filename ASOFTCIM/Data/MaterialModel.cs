using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ASOFTCIM.Data
{
    public class MaterialInfor
    {
        public string MATERIALID { get; set; } = string.Empty;
        public string MATERIALTYPE { get; set; } = string.Empty;
        public string MATERIALST { get; set; } = string.Empty;
        public string MATERIALPORTID { get; set; } = string.Empty;
        public string MATERIALSTATE { get; set; } = string.Empty;
        public string MATERIALTOTALQTY { get; set; } = string.Empty;
        public string MATERIALUSEQTY { get; set; } = string.Empty;
        public string MATERIALASSEMQTY { get; set; } = string.Empty;
        public string MATERIALNGQTY { get; set; } = string.Empty;
        public string MATERIALREMAINQTY { get; set; } = string.Empty;
        public string MATERIALPROCUSEQTY { get; set; } = string.Empty;
    }
    public class MaterialEqp
    {
        public string MATERIALEQPID { get; set; } = string.Empty;
        public string MATERIALBATCHID { get; set; } = string.Empty;
        public string MATERIALCODE { get; set; } = string.Empty;
        public string MATERIALUSEDATE { get; set; } = string.Empty;
        public string MATERIALDISEASEDATE { get; set; } = string.Empty;
        public string MATERIALMAKER { get; set; } = string.Empty;
        public string MATERIALVALIDATIONFLAGE { get; set; } = string.Empty;
        public string MATERIALDEFECTCODE { get; set; } = string.Empty;
        public string COMMENT { get; set; } = string.Empty;
    }
    public class MATERIALCHANGE
    {
        public string MATERIALID { get; set; } = string.Empty;
        public string MATERIALTYPE { get; set; } = string.Empty;
        public string MATERIALST { get; set; } = string.Empty;
        public string MATERIALPORTID { get; set; } = string.Empty;
        public string MATERIALUSAGE { get; set; } = string.Empty;
    }

    public class MATERIALPROCESSCHANGEDATA
    {
        public string EQPID { get; set; }

        [System.Xml.Serialization.XmlElement("EVENT")]
        public string EVENT { get; set; }

        [System.Xml.Serialization.XmlElement("CELL")]
        public CELL CELL { get; set; }


        [XmlArray("MATERIALS")]
        [XmlArrayItem("MATERIAL")]
        public List<TRACKING_MATERIAL> MATERIALs { get; set; }
    }
    [Serializable()]
    public class TRACKING_MATERIAL
    {
        [System.Xml.Serialization.XmlElement("EQPMATERIALBATCHID")]
        public string EQPMATERIALBATCHID { get; set; } = "";

        [System.Xml.Serialization.XmlElement("EQPMATERIALBATCHNAME")]
        public string EQPMATERIALBATCHNAME { get; set; } = "";

        [System.Xml.Serialization.XmlElement("EQPMATERIALID")]
        public string EQPMATERIALID { get; set; } = "";

        [System.Xml.Serialization.XmlElement("EQPMATERIALTYPE")]
        public string EQPMATERIALTYPE { get; set; } = "";

        [System.Xml.Serialization.XmlElement("EQPMATERIALST")]
        public string EQPMATERIALST { get; set; } = "";

        [System.Xml.Serialization.XmlElement("EQPMATERIALPORTID")]
        public string EQPMATERIALPORTID { get; set; } = "";

        [System.Xml.Serialization.XmlElement("EQPMATERIALSTATE")]
        public string EQPMATERIALSTATE { get; set; } = "";

        [System.Xml.Serialization.XmlElement("EQPMATERIALTOTALQTY")]
        public string EQPMATERIALTOTALQTY { get; set; } = "";

        [System.Xml.Serialization.XmlElement("EQPMATERIALUSEQTY")]
        public string EQPMATERIALUSEQTY { get; set; } = "";

        [System.Xml.Serialization.XmlElement("EQPMATERIALASSEMQTY")]
        public string EQPMATERIALASSEMQTY { get; set; } = "";

        [System.Xml.Serialization.XmlElement("EQPMATERIALNGQTY")]
        public string EQPMATERIALNGQTY { get; set; } = "";

        [System.Xml.Serialization.XmlElement("EQPMATERIALREMAINQTY")]
        public string EQPMATERIALREMAINQTY { get; set; } = "";

        [System.Xml.Serialization.XmlElement("EQPMATERIALPRODUCTQTY")]
        public string EQPMATERIALPRODUCTQTY { get; set; } = "";

        [System.Xml.Serialization.XmlElement("EQPMATERIALPROCEUSEQTY")]
        public string EQPMATERIALPROCUSEQTY { get; set; } = "";

        [System.Xml.Serialization.XmlElement("EQPMATERIALPROCEASSEMQTY")]
        public string EQPMATERIALPROCASSEMQTY { get; set; } = "";

        [System.Xml.Serialization.XmlElement("EQPMATERIALPROCNGQTY")]
        public string EQPMATERIALPROCNGQTY { get; set; } = "";

        [System.Xml.Serialization.XmlElement("EQPMATERIALSUPPLYREQUESTQTY")]
        public string EQPMATERIALSUPPLYREQUESTQTY { get; set; } = "";

        public override string ToString()
        {
            return EQPMATERIALID;
        }
    }
    public class MATERIALINFOMATIONSEND
    {
        public string EQPID { get; set; }

        [System.Xml.Serialization.XmlElement("MATERIALSTANDARD")]
        public MaterialEqp MATERIALSTANDARD { get; set; }

        [System.Xml.Serialization.XmlElement("MATERIALUSEINFO")]
        public MaterialInfor MATERIALUSEINFO { get; set; }

        [System.Xml.Serialization.XmlElement("REPLY")]
        public REPLY REPLY { get; set; }
    }
}
