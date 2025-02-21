using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    /// <summary> 
    /// T:RPTID 807/Job Information
    /// Loss Code
    /// </summary>
    [Serializable()]
    public class JOB
    {
        [System.Xml.Serialization.XmlElement("FINALEQPID")]
        public string FINALEQPID { get; set; }

        [System.Xml.Serialization.XmlElement("JOBID")]
        public string JOBID { get; set; }
        [System.Xml.Serialization.XmlElement("JOBTYPE")]
        public string JOBTYPE { get; set; }
        [System.Xml.Serialization.XmlElement("READERID")]
        public string READERID { get; set; }

        [System.Xml.Serialization.XmlElement("READERRESULT")]
        public string READERRESULT { get; set; }
        [System.Xml.Serialization.XmlElement("OPERID")]
        public string OPERID { get; set; }
    }

    /// <summary> 
    /// T:RPTID 808/Inspection Result
    /// Loss Code
    /// </summary>
    [Serializable()]
    public class INSPECTION
    {
        [System.Xml.Serialization.XmlElement("PROCESSNAME")]
        public string PROCESSNAME { get; set; }

        [System.Xml.Serialization.XmlElement("CELLID")]
        public string CELLID { get; set; }
        [System.Xml.Serialization.XmlElement("PROCESSFLAG")]
        public string PROCESSFLAG { get; set; }
        [System.Xml.Serialization.XmlElement("JUDGE")]
        public string JUDGE { get; set; }

        [System.Xml.Serialization.XmlElement("REASONCODE")]
        public string REASONCODE { get; set; }
        [System.Xml.Serialization.XmlElement("OPERID")]
        public string OPERID { get; set; }
        [System.Xml.Serialization.XmlElement("SENDUNIQUEINFO")]
        public string SENDUNIQUEINFO { get; set; }
        [System.Xml.Serialization.XmlElement("REVUNIQUEINFO")]
        public string REVUNIQUEINFO { get; set; }
        [System.Xml.Serialization.XmlElement("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
    }

    public class TRACKING
    {
        public APN APN { get; set; }
        public READER READER { get; set; }
        public List<DV> DVs { get; set; }
        public JUDGEMENT JUDGEMENT { get; set; }
    }

    public class CELLLOTSTART
    {
        public string READERID { get; set; }
        public string READERRESULTCODE { get; set; }

        public List<CELLLOT> CELLLOTs = new List<CELLLOT>();
    }

    public class CELLLOT
    {
        public string CELLID { get; set; }
        public string PARENTLOT { get; set; }
    }
}
