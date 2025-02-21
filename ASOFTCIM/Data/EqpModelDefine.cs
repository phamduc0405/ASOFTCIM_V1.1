using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ASOFTCIM.Data
{

    public class SVDEFINE : ACopy
    {
        public string SVID { get; set; }
        public string SVNAME { get; set; }
        public string SVVALUE { get; set; }
        public string DEFAULTSV { get; set; }
        public string DESCRIPTION { get; set; }
        public string UNIT { get; set; }
        public string MIN { get; set; }
        public string MAX { get; set; }
        public string MODULEID { get; set; }
        public string EQPID { get; set; }
        public int DOT { get; set; }
    }
    public class ALARMDEFINE : ACopy
    {
        public string EQPID { get; set; }
        public uint ALID { get; set; }
        public string ALTEXT { get; set; }
        public string DESCRIPTION { get; set; }
        public string ALCD { get; set; }
        [Browsable(false)]
        public string ALED { get; set; }     // Alarm Enable Disable 0 Enable, 1 Disable
    }
    public class TERMINAL
    {
        public string EQPID { get; set; }
        public string TID { get; set; }
        [XmlArray("TEXTS")]
        [XmlArrayItem("TEXT")]
        public List<string> TEXT { get; set; } = new List<string>();
    }

    public class ATTRREQUEST
    {
        public string EQPID { get; set; }
        public string OBJTYPE { get; set; }
        public string OBJID { get; set; }
        public string COMMENT { get; set; }
        public string REPLYCODE { get; set; }
        public string REPLYTEXT { get; set; }
        public List<ATTR> ATTRs { get; set; }
    }
    public class ATTR
    {
        public string ATTRID { get; set; }
        public string ATTRDATA { get; set; }
    }


    public class PROCESSDATACONTROL
    {
        public string TMACK { get; set; }

        [XmlArrayItem("CELL")]
        public List<PROCESS_CELL> CELLs { get; set; }

        public string EQPID { get; set; }
        public string MODE { get; set; }
        public string BYWHO { get; set; }
        public string RESULT { get; set; }
    }
    public class PROCESS_CELL
    {
        public string CELLID { get; set; }
        public string SEQ_NO { get; set; }
        [XmlArrayItem("MODULE")]
        public List<PROCESS_MODULE> MODULEs { get; set; }
    }
    public class PROCESS_MODULE
    {
        public string MODULEID { get; set; }
        public string PPID { get; set; }
        public string PPID_TYPE { get; set; }
        public List<PARAM> PARAMs { get; set; }
        public List<ITEM> ITEMs { get; set; }
    }
    public class CELLEVENTDATA
    {
        [System.Xml.Serialization.XmlElement("EVENT")]
        public string EVENT { get; set; }
        [System.Xml.Serialization.XmlElement("UNITID")]
        public string UNITID { get; set; }
        [System.Xml.Serialization.XmlElement("UNITST")]
        public string UNITST { get; set; }
        [System.Xml.Serialization.XmlElement("EQST")]
        public EQPSTATE EQST { get; set; }
        [System.Xml.Serialization.XmlElement("CELL")]
        public CELLINFOR CELL { get; set; }
        [System.Xml.Serialization.XmlElement("WORKORDER")]
        public WORKORDER WORKORDER { get; set; }
        [System.Xml.Serialization.XmlElement("READER")]
        public READER READER { get; set; }
        [XmlArray("DVs")]
        [XmlArrayItem("DV", typeof(DV))]
        public List<DV> DVs { get; set; }

        [XmlArray("MATERIALS")]
        [XmlArrayItem("MATERIAL", typeof(TRACKING_MATERIAL))]
        public List<TRACKING_MATERIAL> MATERIALs { get; set; }

        [System.Xml.Serialization.XmlElement("JUDGEMENT")]
        public JUDGEMENT JUDGEMENT { get; set; }
        public string EQPID { get; set; }
    }
   

    /// <summary>
    /// T:RPTID 600/DV
    /// </summary>
    [Serializable()]
    public class DV
    {
        [System.Xml.Serialization.XmlElement("DVNAME")]
        public string DVNAME { get; set; }

        [System.Xml.Serialization.XmlElement("DVVAL")]
        public string DVVAL { get; set; }
    }
    /// <summary>
    /// T:RPTID 500/JUDGEMENT
    /// </summary>
    [Serializable()]
    public class JUDGEMENT
    {
        [System.Xml.Serialization.XmlElement("OPERATORID1")]
        public string OPERATORID1 { get; set; }

        [System.Xml.Serialization.XmlElement("OPERATORID2")]
        public string OPERATORID2 { get; set; }

        [System.Xml.Serialization.XmlElement("OPERATORID3")]
        public string OPERATORID3 { get; set; }
        [System.Xml.Serialization.XmlElement("JUDGE")]
        public string JUDGE { get; set; }
        [System.Xml.Serialization.XmlElement("REASONCODE")]
        public string REASONCODE { get; set; }
        [System.Xml.Serialization.XmlElement("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
    }

    /// <summary>
    /// T:RPTID 300 /CELLINFOR
    /// </summary>
    [Serializable()]
    public class CELLINFOR
    {
        [System.Xml.Serialization.XmlElement("CELLID")]
        public string CELLID { get; set; }

        [System.Xml.Serialization.XmlElement("PPID")]
        public string PPID { get; set; }

        [System.Xml.Serialization.XmlElement("PRODUCTID")]
        public string PRODUCTID { get; set; }
        [System.Xml.Serialization.XmlElement("PRODUCTTYPE")]
        public string PRODUCTTYPE { get; set; }
        [System.Xml.Serialization.XmlElement("STEPID")]
        public string STEPID { get; set; }
    }
    /// <summary>
    /// T:RPTID 301 /WORKORDER
    /// </summary>
    [Serializable()]
    public class WORKORDER
    {
        [System.Xml.Serialization.XmlElement("PROCESSJOB")]
        public string PROCESSJOB { get; set; }

        [System.Xml.Serialization.XmlElement("PLANQTY")]
        public string PLANQTY { get; set; }

        [System.Xml.Serialization.XmlElement("PROCESSEDQTY")]
        public string PROCESSEDQTY { get; set; }
    }

    
    /// <summary>
    /// T:RPTID 308/PAIRCELL
    /// </summary>
    [Serializable()]
    public class PAIRCELL
    {
        [System.Xml.Serialization.XmlElement("PAIRCELLID")]
        public string PAIRCELLID { get; set; }

        [System.Xml.Serialization.XmlElement("PAIRTYPE")]
        public string PAIRTYPE { get; set; }
        [System.Xml.Serialization.XmlElement("PPID")]
        public string PPID { get; set; }
        [System.Xml.Serialization.XmlElement("PRODUCTID")]
        public string PRODUCTID { get; set; }
        [System.Xml.Serialization.XmlElement("STEPID")]
        public string STEPID { get; set; }
    }

    
    /// <summary>
    /// T:RPTID 400/READER
    /// </summary>
    [Serializable()]
    public class READER
    {
        [System.Xml.Serialization.XmlElement("READERID")]
        public string READERID { get; set; }

        [System.Xml.Serialization.XmlElement("READERRESULTCODE")]
        public string READERRESULTCODE { get; set; }
    }
}
