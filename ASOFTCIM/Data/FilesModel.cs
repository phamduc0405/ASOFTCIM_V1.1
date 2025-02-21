using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    public class ConfigFileChange
    {
        public string PRODUCTID { get; set; }
        public string ACTIONTYPE { get; set; }
        public string ACTIONRESULT { get; set; }
        public List<FILE> FILES { get; set; }
    }
    public class FILE
    {
        public string FILETYPE { get; set; }
        public string FILENAME { get; set; }
        public string FILEPATH { get; set; }
        public string LOCALCHECKSUM { get; set; }
        public string CURRENTCHECKSUM { get; set; }
    }
}
