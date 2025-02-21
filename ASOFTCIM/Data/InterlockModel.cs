using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ASOFTCIM.Data
{
    public class INTERLOCKCONFIRM
    {
        public List<INTERLOCK> INTERLOCKs { get; set; }
        public CELLINFOR CELL { get; set; }
    }
    public class UNITINTERLOCKCONFIRM
    {
        public EQPSTATE UNIT { get; set; }
        public List<INTERLOCK> INTERLOCKs { get; set; }
        public CELLINFOR CELL { get; set; }
    }
    /// <summary>
    /// T:RPTID 701/INTERLOCK
    /// </summary>
    [Serializable()]
    public class INTERLOCK
    {
        [System.Xml.Serialization.XmlElement("INTERLOCKID")]
        public string INTERLOCKID { get; set; }

        [System.Xml.Serialization.XmlElement("MESSAGE")]
        public string MESSAGE { get; set; }
    }
    public class INTERLOCKMESS
    {
        public string TIME { get; set; }
        [Browsable(false)]
        public string INTERLOCK { get; set; }
        [Browsable(false)]
        public string EQPID { get; set; }

        [Browsable(false)]
        public string UNITID { get; set; }

        [Browsable(false)]
        public string RCMD { get; set; } // *11 Transfer Stop *12 Loading Stop *13 StepStop *14 OWN Stop
                                         // *41 Force Transfer Stop *42 Force Loading Stop *43 Force Step Stop *44 Force Own Stop

        [Browsable(false)]
        public string INTERLOCKID { get; set; }
        [XmlElement("MESSAGE")]
        public string MESSAGE { get; set; }
    }
}
