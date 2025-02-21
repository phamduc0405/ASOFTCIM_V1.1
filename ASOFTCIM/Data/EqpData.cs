using A_SOFT.CMM.INIT;
using A_SOFT.Ctl.SecGem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ASOFTCIM.Data
{
    public class EQPDATA : ACopy
    {
        public uint TransactionSys { get; set; } = 0;
        public uint TransactionEq { get; set; } = 0;
        public ushort DeviceId { get; set; } = 1;
        public EQINFORMATION EQINFORMATION { get; set; } = new EQINFORMATION();
        public List<SV> SVID { get; set; } = new List<SV>();
        public EQPSTATE EQPSTATE { get; set; } = new EQPSTATE();
        public List<EQPSTATE> UNITSTATES { get; set; } = new List<EQPSTATE>(8);
        public List<MATERIALSTATE> MATERIALSTATES { get; set; } = new List<MATERIALSTATE>();
        public List<PORTSTATE> PORTSTATES { get; set; } = new List<PORTSTATE>();
        public List<FUNCTION> FUNCTION { get; set; } = new List<FUNCTION>();
        public List<Alarm> ALS { get; set; } = new List<Alarm>();
        public List<Alarm> CurrAlarm { get; set; } = new List<Alarm>();
        public List<EC> ECS { get; set; } = new List<EC>();
        public PPIDINFOR CurrPPID { get; set; } = new PPIDINFOR();
        public PPIDList PPIDList { get; set; } = new PPIDList();
        public List<OPCALLMESS> OPCALLS { get; set; } = new List<OPCALLMESS>();
        public List<INTERLOCKMESS> INTERLOCKS { get; set; } = new List<INTERLOCKMESS>();
        public FUNCTIONSTATE FUNCTIONSTATE { get; set; } = new FUNCTIONSTATE();
        public List<CHECKERINFOR> CHECKERINFORS { get; set; } = new List<CHECKERINFOR>();
        public PACKINGINFOR PACKINGINFOR = new PACKINGINFOR();
        public SPBINFOR SPBINFOR = new SPBINFOR();
        public CARTONINFOR CARTONINFOR = new CARTONINFOR();
        public ETCINFOR ETCINFOR = new ETCINFOR();
        public EQPDATA()
        {
            for (int i = 0; i < 8; i++)
            {
                UNITSTATES.Add(new EQPSTATE());
                MATERIALSTATES.Add(new MATERIALSTATE());
                PORTSTATES.Add(new PORTSTATE());
            }
        }

    }
    public class HEADER
    {
        public string DeviceId { get; set; }
        public string Transaction { get; set; }
    }
    //TODO: SVID
    public class SV
    {
        public string SVID { get; set; }
        public string SVNAME { get; set; }
        public string SVVALUE { get; set; }
        public string DefaultSV { get; set; }
        public string DESCRIPTION { get; set; }
        public string UNIT { get; set; }
        public string MIN { get; set; }
        public string MAX { get; set; }
        public string MODULEID { get; set; }
        public string EQPID { get; set; }
        public int DOT { get; set; }
    }


    [Serializable]
    public class OPCALLMESS
    {
        [XmlIgnore]
        public string TIME { get; set; }
        [XmlIgnore]
        public string OPCALL { get; set; }
        [XmlIgnore]
        public string EQPID { get; set; }

        [XmlIgnore]
        public string UNITID { get; set; }

        [XmlIgnore]
        public string RCMD { get; set; }
        public string OPCALLID { get; set; }
        public string MESSAGE { get; set; }
    }

    
    public class EQPSTATE : ACopy
    {
        //     public string EQPID { get; set; }                   //EQPID(User Define)
        //     public string MAINSTATE { get; set; }               //MainState (Disable)
        public string AVAILABILITYSTATE { get; set; } = "0";        //Availability (Equipment Status)
        public string INTERLOCKSTATE { get; set; } = "0";          //Interlock (Equipment Status)
        public string MOVESTATE { get; set; } = "0";               //Move Status
        public string RUNSTATE { get; set; } = "0";                // Run Status
        public string FRONTSTATE { get; set; } = "0";              //Front EQP Status (Equipment Status)
        public string REARSTATE { get; set; } = "0";               //Rear EQP Status (Equipment Status)
                                                                   //    public string UNITST { get; set; }                  //Unit Status
        public string PPSPLSTATE { get; set; } = "0";              //SAMPLE LOT STATUS
        public string REASONCODE { get; set; } = "0";
        public string DESCRIPTION { get; set; } = "0";
        public string UNITID { get; set; } = "0";
        public EQINFORMATION EQPINFOR { get; set; } = new EQINFORMATION();
        public List<MATERIALSTATE> MATERIALSTATES { get; set; } = new List<MATERIALSTATE>();
        public List<PORTSTATE> PORTSTATES { get; set; } = new List<PORTSTATE>();

       
    }

    public class MATERIALSTATE : ACopy 
    {
        public string MATERIALID { get; set; }
        public string MATERIALTYPE { get; set; }
        public string MATERIALST { get; set; }
        public string MATERIALPORTID { get; set; }
        public string MATERIALUSAGE { get; set; }

        
    }


   
    public class EQINFORMATION
    {
        public string EQPID { get; set; } = String.Empty;
        public string EQPVER { get; set; } = String.Empty;         //Equipment Version
        /// <summary>
        /// Online Control State * 0 = OFFLINE * 1 = ONLINE REMOTE * 2 = ONLINE LOCAL
        /// </summary>
        public string CRST { get; set; } = String.Empty;         //Online Control State * 0 = OFFLINE * 1 = ONLINE REMOTE * 2 = ONLINE LOCAL
        public string PPID { get; set; } = String.Empty;
    }
    public class EC
    {
        public string ECID { get; set; } = string.Empty;
        public string ECNAME { get; set; } = string.Empty;
        /// <summary>
        /// Equipment Constant Set value
        /// </summary>
        public string ECDEF { get; set; } = string.Empty;
        /// <summary>
        /// STOP LOW LIMMIT
        /// </summary>
        public string ECSLL { get; set; } = string.Empty;
        /// <summary>
        /// STOP UPPER LIMMIT
        /// </summary>
        public string ECSUL { get; set; } = string.Empty;
        /// <summary>
        /// WARNNING LOW LIMMIT
        /// </summary>
        public string ECWLL { get; set; } = string.Empty;
        /// <summary>
        /// WARNNING UPPER LIMMIT
        /// </summary>
        public string ECWUL { get; set; } = string.Empty;


    }
    
    public class CELL
    {
        [System.Xml.Serialization.XmlElement("CARRIERID")]
        public string CARRIERID { get; set; }
        [System.Xml.Serialization.XmlElement("CELLID")]
        public string CELLID { get; set; }
        [System.Xml.Serialization.XmlElement("PPID")]
        public string PPID { get; set; }
        [System.Xml.Serialization.XmlElement("PRODUCTID")]
        public string PRODUCTID { get; set; }
        [System.Xml.Serialization.XmlElement("STEPID")]
        public string STEPID { get; set; }
        [System.Xml.Serialization.XmlElement("PRODUCTTYPE")]
        public string PRODUCTTYPE { get; set; }
        [System.Xml.Serialization.XmlElement("RRODUCTKIND")]
        public string RRODUCTKIND { get; set; }
        [System.Xml.Serialization.XmlElement("PRODUCTSPE")]
        public string PRODUCTSPE { get; set; }
        [System.Xml.Serialization.XmlElement("CELLSIZE")]
        public string CELLSIZE { get; set; }
        [System.Xml.Serialization.XmlElement("CELLTHICKNESS")]
        public string CELLTHICKNESS { get; set; }
        [System.Xml.Serialization.XmlElement("COMMENT")]
        public string COMMENT { get; set; }
        [System.Xml.Serialization.XmlElement("CELLINFORESULT")]
        public string CELLINFORESULT { get; set; }
        [System.Xml.Serialization.XmlElement("RESPONSESTATUS")]
        public string RESPONSESTATUS { get; set; }
        [System.Xml.Serialization.XmlElement("OPTIONINFO")]
        public string OPTIONINFO { get; set; }
        [System.Xml.Serialization.XmlElement("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
    }

    public class ITEM
    {
        public string ITEMNAME { get; set; }
        public string ITEMVALUE { get; set; }
    }
    public static class CIMINFOR
    {
        public static string CIMVER { get; set; } = "1.0";
    }
   
    public class COMMANDCODE
    {
        public string CCODE { get; set; }
        public List<PARAM> PARAMs { get; set; }
    }
    public class REPLY
    {
        [System.Xml.Serialization.XmlElement("REPLYSTATUS")]
        public string REPLYSTATUS { get; set; }

        [System.Xml.Serialization.XmlElement("REPLYCODE")]
        public string REPLYCODE { get; set; }

        [System.Xml.Serialization.XmlElement("REPLYTEXT")]
        public string REPLYTEXT { get; set; }
    }
    public class BATCHLOT
    {
        public string PRODUCTID { get; set; }
        public string BATCHLOTID { get; set; }
        public string BATCHLOTQTY { get; set; }
        public string REASONCODE { get; set; }
        public string DESCRIPTION { get; set; }
    }
    public class CHECKERINFOR
    {
        public string CHECKERNAME { get; set; }
        public string CHECKERDESC { get; set; }
    }
}
