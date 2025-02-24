using ASOFTCIM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.MainControl
{
    public class Controller
    {
        private ACIM _cim;

        public ACIM CIM
        {
            get { return _cim; }
            set { _cim = value;}
        }


        public Controller()
        {
            _cim = new ACIM();
            
        }
        public void Stop()
        {
            _cim.Stop();
        }
    }
}
