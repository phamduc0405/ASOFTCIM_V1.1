using ASOFTCIM.Helper;
using A_SOFT.CMM.INIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASOFTCIM.Data;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void RecvS3F115()
        {
            try
            {
                string ACK = "0";
                string eqp = _cim.SysPacket.GetItemString(1);
                string lst = _cim.SysPacket.GetItemString();
                CARRIERINFODOWNLOAD carrierinfor = new CARRIERINFODOWNLOAD();
                carrierinfor.CARRIERID = _cim.SysPacket.GetItemString();
                carrierinfor.CARRIERTYPE = _cim.SysPacket.GetItemString();
                carrierinfor.CARRIERPPID = _cim.SysPacket.GetItemString();
                carrierinfor.CARRIERPRODUCT = _cim.SysPacket.GetItemString();
                carrierinfor.CARRIERSTEPID = _cim.SysPacket.GetItemString();
                carrierinfor.CARRIER_S_COUNT = _cim.SysPacket.GetItemString();
                carrierinfor.CARRIER_C_COUNT = _cim.SysPacket.GetItemString();
                carrierinfor.PORTNO = _cim.SysPacket.GetItemString();
                int count1 = int.Parse(_cim.SysPacket.GetItemString());
                
                for (int i = 0; i < count1; i++)
                {
                    string lst1 = _cim.SysPacket.GetItemString();
                    SUBCARRIER sub = new SUBCARRIER();
                    sub.SUBCARRIERID = _cim.SysPacket.GetItemString();
                    sub.CELLQTY = _cim.SysPacket.GetItemString();
                    int count2 = int.Parse(_cim.SysPacket.GetItemString());
                    for (int j = 0; j < count2; j++)
                    {
                        string lst2 = _cim.SysPacket.GetItemString();
                        CELLINFO cell = new CELLINFO();
                        cell.CELLID = _cim.SysPacket.GetItemString();
                        cell.LOCATIONNO = _cim.SysPacket.GetItemString();
                        cell.JUDGE = _cim.SysPacket.GetItemString();
                        cell.REASONCODE = _cim.SysPacket.GetItemString();
                        sub.CELLSINFOR.Add(cell);
                    }
                    carrierinfor.SUBCARRIERS.Add(sub);
                }
                lst = _cim.SysPacket.GetItemString();
                carrierinfor.REPLY.REPLYCODE = _cim.SysPacket.GetItemString();
                carrierinfor.REPLY.REPLYTEXT = _cim.SysPacket.GetItemString();

                int cellpallet = int.Parse(_cim.SysPacket.GetItemString(15));

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
                SendS9F7(_cim.SysPacket);
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
        }
    }
}
