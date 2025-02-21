using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    public class FUNCTIONSTATE
    {
        public string CELLTRACKING { get; set; }
        public string TRACKINGCONTROL { get; set; }
        public string MATERIALTRACKING { get; set; }
        public string CELLMCRMODE { get; set; }
        public string MATERIALMCRMODE { get; set; }
        public string LOTASSIGNINFORMATION { get; set; }
        public string AGVACCESSMODE { get; set; }
        public string AREASENSORMODE { get; set; }
        public string SORTMODE { get; set; }
        public string INTERLOCKCONTROL { get; set; }
        public string LOADERLI01 { get; set; }
        public string LOADERLS01 { get; set; }
        public string UNLOADER { get; set; }
        public string APCMODE { get; set; }
        public string MULTIPASSMODE { get; set; }
    }
    [Serializable()]
    public class FUNCTION
    {

        public string UNITID { get; set; }
        [System.Xml.Serialization.XmlElement("BYWHO")]
        public string BYWHO { get; set; }
        [System.Xml.Serialization.XmlElement("EFID")]
        public string EFID { get; set; }
        [System.Xml.Serialization.XmlElement("EFST")]
        public string EFST { get; set; }
        [System.Xml.Serialization.XmlElement("NEWEFST")]
        public string NEWEFST { get; set; }
        [System.Xml.Serialization.XmlElement("OLDEFST")]
        public string OLDEFST { get; set; }


        public string EFNAME { get; set; }
        public string MESSAGE { get; set; }
        public override string ToString()
        {
            return EFID;
        }
    }
}
