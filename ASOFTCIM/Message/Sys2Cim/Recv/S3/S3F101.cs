using ASOFTCIM.Data;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Helper;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS3F101()
        {
            try
            {
                string ACK = "0";
                string eqp = _cim.SysPacket.GetItemString(1);
                int cassetCount =int.Parse( _cim.SysPacket.GetItemString());
                List<CASSETTLE> cassetList = new List<CASSETTLE>();
                for (int i = 0; i < cassetCount; i++)
                {
                    CASSETTLE casset = new CASSETTLE();
                    casset.CASSETTLEID = _cim.SysPacket.GetItemString(4);
                    casset.CASSETTETYPE = _cim.SysPacket.GetItemString();
                    casset.BATCHLOT = _cim.SysPacket.GetItemString();
                    casset.BATCHLOTQTY = _cim.SysPacket.GetItemString();
                    casset.PRODUCTSPEC = _cim.SysPacket.GetItemString();
                    casset.PRODUCT_TYPE = _cim.SysPacket.GetItemString();
                    casset.PRODUCT_KIND = _cim.SysPacket.GetItemString();
                    casset.PRODUCTSPEC = _cim.SysPacket.GetItemString();
                    casset.PPID = _cim.SysPacket.GetItemString();
                    casset.STEPID = _cim.SysPacket.GetItemString();
                    casset.COMMENT = _cim.SysPacket.GetItemString();
                    int cellidCount = int.Parse(_cim.SysPacket.GetItemString());
                    for (int j = 0; j < cellidCount; j++)
                    {
                        string cellid = _cim.SysPacket.GetItemString();
                        casset.CELLIDS.Add(cellid);
                    }
                    cassetList.Add(casset);
                }

                List<Tuple<string, string>> items = new List<Tuple<string, string>>();
                int itemCount = int.Parse(_cim.SysPacket.GetItemString());
                for (int i = 0; i < itemCount; i++)
                {
                    string list = _cim.SysPacket.GetItemString();
                    string itemName = _cim.SysPacket.GetItemString();
                    string itemValue = _cim.SysPacket.GetItemString();
                    items.Add(Tuple.Create(itemName, itemValue));
                }
                SendS3F102(ACK);
            }
            catch (Exception ex)
            {
                SendS9F7(_cim.SysPacket);
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    }
}
