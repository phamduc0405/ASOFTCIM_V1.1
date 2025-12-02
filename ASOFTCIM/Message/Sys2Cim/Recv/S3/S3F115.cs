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
using A_SOFT.PLC;

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
                //// 20251125: NamPham

                //WordModel Carrier = _plcH.Words.FirstOrDefault(x => x.Area == "CarrierProcessChangeUnloader1" && x.Item == "CARRIERID");
                //if (carrierinfor.CARRIERID == Carrier.GetValue())
                //{
                //    ACK = "9";
                //    SendS3F116(ACK);
                //    return;
                //}
                //251126 NamPham sua ACK carrier cho  Unload
                WordModel Carrier = _plcH.Words.FirstOrDefault(x => x.Area == "CarrierProcessChangeUnloader1" && x.Item == "CARRIERID");
                WordModel CarrierSub = _plcH.Words.FirstOrDefault(x => x.Area == "CarrierProcessChangeUnloader1" && x.Item == "SUBCARRIERID1");

                var c = Carrier.GetValue();
                var c1 = CarrierSub.GetValue();
                if ((sysPacket.GetItemString(13) != CarrierSub.GetValue()) && !string.IsNullOrEmpty(CarrierSub.GetValue()) && carrierinfor.SUBCARRIERS.Count != 0)
                {
                    ACK = "9";
                    SendS3F116(ACK);
                    return;
                }
                if ((sysPacket.GetItemString(3) != Carrier.GetValue()) && !string.IsNullOrEmpty(Carrier.GetValue()))
                {
                    ACK = "9";
                    SendS3F116(ACK);
                    return;
                }
                //251126 NamPham sua ACK carrier cho load
                //WordModel Carrier = _plcH.Words.FirstOrDefault(x => x.Area == "CarrierProcessChangeUnloader1" && x.Item == "CARRIERID");
                //WordModel CarrierSub = _plcH.Words.FirstOrDefault(x => x.Area == "CarrierProcessChangeUnloader1" && x.Item == "SUBCARRIERID1");

                //var c = Carrier.GetValue();
                //var c1 = CarrierSub.GetValue();
                //if ((sysPacket.GetItemString(13) != CarrierSub.GetValue()) && !string.IsNullOrEmpty(CarrierSub.GetValue()) && carrierinfor.SUBCARRIERS.Count != 0)
                //{
                //    ACK = "9";
                //    SendS3F116(ACK);
                //    return;
                //}
                //if ((sysPacket.GetItemString(3) != Carrier.GetValue()) && !string.IsNullOrEmpty(Carrier.GetValue()))
                //{
                //    ACK = "9";
                //    SendS3F116(ACK);
                //    return;
                //}
                int cellpallet = int.Parse(sysPacket.GetItemString(16));

                if (carrierinfor.PORTNO == "LS01")//256 //260
                {
                    SendMessage2PLC("CARRIERINFORMATIONSENDLOADER1", carrierinfor);
                }
                if (carrierinfor.PORTNO == "LH01" || carrierinfor.PORTNO == "UH01")//260
                {
                    SendMessage2PLC("CARRIERINFORMATIONSENDLOADER1", carrierinfor);
                }
                if ((carrierinfor.CARRIERTYPE == "11" || carrierinfor.CARRIERTYPE == "13") && carrierinfor.PORTNO == "UL01")//354
                {
                    SendMessage2PLC("CARRIERINOFRMATIONSENDCASSETTE1", carrierinfor);
                }
                if ((carrierinfor.CARRIERTYPE == "11" || carrierinfor.CARRIERTYPE == "13") && carrierinfor.PORTNO == "LI01" && carrierinfor.PORTNO == "UI01")//354
                {
                    SendMessage2PLC("CARRIERINOFRMATIONSENDCASSETTE1", carrierinfor);
                }
                if (carrierinfor.PORTNO == "US01" || carrierinfor.PORTNO == "UH01")//256 //260
                {
                    SendMessage2PLC("CARRIERINFORMATIONSENDUNLOADER1", carrierinfor);
                }

                //unloader





                SendS3F116(ACK);
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
