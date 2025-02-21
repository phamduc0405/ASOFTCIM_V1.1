using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    public class LOSSCODEREPORT
    {
        public LOSS LOSSCODE { get; set; }
    }
    /// <summary> 
    /// T:RPTID 806/Loss Code
    /// Loss Code
    /// </summary>
    [Serializable()]
    public class LOSS
    {
        [System.Xml.Serialization.XmlElement("LOSSCODE")]
        public string LOSSCODE { get; set; }

        [System.Xml.Serialization.XmlElement("LOSSDESCRIPTION")]
        public string LOSSDESCRIPTION { get; set; }
    }
}
