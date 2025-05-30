using ASOFTCIM.Helper;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Data;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS3F115(SysPacket sysPacket)
        {
            try
            {
                string ACK = "0";
                string eqp = sysPacket.GetItemString(1);
                string lst = sysPacket.GetItemString();
                CARRIERINFODOWNLOAD carrierinfor = new CARRIERINFODOWNLOAD();
                carrierinfor.CARRIERID = sysPacket.GetItemString();
                carrierinfor.CARRIERTYPE = sysPacket.GetItemString();
                carrierinfor.CARRIERPPID = sysPacket.GetItemString();
                carrierinfor.CARRIERPRODUCT = sysPacket.GetItemString();
                carrierinfor.CARRIERSTEPID = sysPacket.GetItemString();
                carrierinfor.CARRIER_S_COUNT = sysPacket.GetItemString();
                carrierinfor.CARRIER_C_COUNT = sysPacket.GetItemString();
                carrierinfor.PORTNO = sysPacket.GetItemString();
                int count1 = int.Parse(sysPacket.GetItemString());
                
                for (int i = 0; i < count1; i++)
                {
                    string lst1 = sysPacket.GetItemString();
                    SUBCARRIER sub = new SUBCARRIER();
                    sub.SUBCARRIERID = sysPacket.GetItemString();
                    sub.CELLQTY = sysPacket.GetItemString();
                    int count2 = int.Parse(sysPacket.GetItemString());
                    for (int j = 0; j < count2; j++)
                    {
                        string lst2 = sysPacket.GetItemString();
                        CELLINFO cell = new CELLINFO();
                        cell.CELLID = sysPacket.GetItemString();
                        cell.LOCATIONNO = sysPacket.GetItemString();
                        cell.JUDGE = sysPacket.GetItemString();
                        cell.REASONCODE = sysPacket.GetItemString();
                        sub.CELLSINFOR.Add(cell);
                    }
                    carrierinfor.SUBCARRIERS.Add(sub);
                }
                lst = sysPacket.GetItemString();
                carrierinfor.REPLY.REPLYCODE = sysPacket.GetItemString();
                carrierinfor.REPLY.REPLYTEXT = sysPacket.GetItemString();

                int cellpallet = int.Parse(sysPacket.GetItemString(15));

                //loader
                //if(carrierinfor.CARRIERTYPE == "11" && carrierinfor.PORTNO == "LS01")//262
                //{
                //    SendMessage2PLC("CARRIERINFORMATIONSENDLOADER1", carrierinfor);
                //}
                //if (cellpallet == 0 && carrierinfor.CARRIERTYPE == "21")//257
                //{
                //    SendMessage2PLC("INSPECTIONCARRIERRELEASEINFOSEND1", carrierinfor);
                //}
                //if (cellpallet != 0 && carrierinfor.CARRIERTYPE == "21")//261
                //{
                //    SendMessage2PLC("INSPECTIONCARRIERASSIGNINFOSEND1", carrierinfor);
                //}
                //if (carrierinfor.PORTNO == "LI01")
                //{
                //    SendMessage2PLC("CARRIERINOFRMATIONSENDCASSETTE1", carrierinfor);
                //}
                //if (carrierinfor.CARRIERTYPE == "1" || carrierinfor.CARRIERTYPE == "11" || carrierinfor.CARRIERTYPE == "13")//256 //260
                //{
                //    SendMessage2PLC("CARRIERINFORMATIONSENDLOADER1", carrierinfor);
                //}
                //loader
                //unloader
                if (carrierinfor.CARRIERTYPE == "11" && carrierinfor.PORTNO == "LS01")//262
                {
                    SendMessage2PLC("CARRIERINFORMATIONSENDLOADER1", carrierinfor);
                }
                if (cellpallet == 0 && carrierinfor.CARRIERTYPE == "21")//257
                {
                    SendMessage2PLC("INSPECTIONCARRIERRELEASEINFOSEND1", carrierinfor);
                }
                if (cellpallet != 0 && carrierinfor.CARRIERTYPE == "21")//261
                {
                    SendMessage2PLC("INSPECTIONCARRIERASSIGNINFOSEND1", carrierinfor);
                }
                if (carrierinfor.PORTNO == "LI01")
                {
                    SendMessage2PLC("CARRIERINOFRMATIONSENDCASSETTE1", carrierinfor);
                }
                if (carrierinfor.CARRIERTYPE == "1" || carrierinfor.CARRIERTYPE == "11" || carrierinfor.CARRIERTYPE == "13" || carrierinfor.CARRIERTYPE == "3")//256 //260
                {
                    SendMessage2PLC("CARRIERINFORMATIONSENDUNLOADER1", carrierinfor);
                }
                //unloader





                SendS3F116( ACK);
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
