using ASOFTCIM.Data;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Helper;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS3F101(SysPacket sysPacket)
        {
            try
            {
                string ACK = "0";
                string eqp = sysPacket.GetItemString(1);
                int cassetCount =int.Parse( sysPacket.GetItemString());
                List<CASSETTLE> cassetList = new List<CASSETTLE>();
                for (int i = 0; i < cassetCount; i++)
                {
                    CASSETTLE casset = new CASSETTLE();
                    casset.CASSETTLEID = sysPacket.GetItemString(4);
                    casset.CASSETTETYPE = sysPacket.GetItemString();
                    casset.BATCHLOT = sysPacket.GetItemString();
                    casset.BATCHLOTQTY = sysPacket.GetItemString();
                    casset.PRODUCTSPEC = sysPacket.GetItemString();
                    casset.PRODUCT_TYPE = sysPacket.GetItemString();
                    casset.PRODUCT_KIND = sysPacket.GetItemString();
                    casset.PRODUCTSPEC = sysPacket.GetItemString();
                    casset.PPID = sysPacket.GetItemString();
                    casset.STEPID = sysPacket.GetItemString();
                    casset.COMMENT = sysPacket.GetItemString();
                    int cellidCount = int.Parse(sysPacket.GetItemString());
                    for (int j = 0; j < cellidCount; j++)
                    {
                        string cellid = sysPacket.GetItemString();
                        casset.CELLIDS.Add(cellid);
                    }
                    cassetList.Add(casset);
                }

                List<Tuple<string, string>> items = new List<Tuple<string, string>>();
                int itemCount = int.Parse(sysPacket.GetItemString());
                for (int i = 0; i < itemCount; i++)
                {
                    string list = sysPacket.GetItemString();
                    string itemName = sysPacket.GetItemString();
                    string itemValue = sysPacket.GetItemString();
                    items.Add(Tuple.Create(itemName, itemValue));
                }
                SendS3F102(ACK);
            }
            catch (Exception ex)
            {
                SendS9F7(sysPacket);
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
    }
}
