using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("DataLayer")]
    public class PORTSTATECHANGE
    {
        public PORTSTATE PORTSTATE { get; set; }

    }
    public class PORTSTATE : ACopy
    {
        public string PORTID { get; set; }
        public string PORTNO { get; set; }
        /// <summary>
        /// PORTAVAILABILITYSTATE     1:DOWN  2:UP
        /// </summary>
        public string PORTAVAILABILITYSTATE { get; set; } = string.Empty;
        /// <summary>
        /// PORTACCESSMODE            1:Manual  2:AUTO
        /// </summary>
        public string PORTACCESSMODE { get; set; } = string.Empty;
        /// <summary>
        /// PORTTRANSFERSTATE         0: EMPTY, 1: READYTOLOAD, 2: LOADED, 3: READYTOUNLOAD
        /// </summary>
        public string PORTTRANSFERSTATE { get; set; } = string.Empty;
        /// <summary>
        /// PORTPROCESSINGSTATE      0: NONE, 1: READYTOPROCESS, 2: READYTOSTART, 3: CANCELED 
        ///                             4: PROCESSING, 5: PAUSED, 6: PROCESSED, 7: ABORTED
        /// </summary>
        public string PORTPROCESSINGSTATE { get; set; } = string.Empty;
        public string REASONCODE { get; set; }
        public string DESCRIPTION { get; set; }

       
    }
}
