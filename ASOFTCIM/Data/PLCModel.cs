using A_SOFT.Ctl.Mitsu.Model;
using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    public class PLCModel
    {
    }
    public class FDCModel
    {
        public string SVID { get; set; }
        public string NAME { get; set; }
        public string Area { get; set; }
        public string PLCSTART { get; set; }
        public string BitEvent { get; set; }

        public int PLCAddress
        {
            get
            {
                return Convert.ToInt32(PLCSTART, 16);
            }
        }
        public int Length { get; set; }
        public string Type { get; set; }
        public string GetValue(PlcComm plc)
        {
            string val = "";
            switch (Type.ToUpper())
            {
                case "ASCII": val = plc.GetWord(PLCAddress, Length); break;
                case "DEC": val = plc.InputWordBuffer[PLCAddress].ToString(); break;
                default:
                    break;
            }
            return val;
        }
    }
    public class PPIDModel
    {
        //   public string Object { get; set; }
        public string PPID_NUMBER { get; set; }
        public string Item { get; set; }
        public string Area { get; set; }
        public string PLCSTART { get; set; }
        public string CIMSTART { get; set; }
        public string BitEvent { get; set; }
        public int PLCAddress
        {
            get
            {
                return Convert.ToInt32(PLCSTART, 16);
            }
        }
        public int CIMAddress
        {
            get
            {
                return Convert.ToInt32(CIMSTART, 16);
            }
        }
        public int Length { get; set; }
        public string Type { get; set; }
        public string GetValue (PlcComm plc) 
        {
           
                string val = "";
                switch (Type.ToUpper())
                {
                    case "ASCII": val = plc.GetWord(PLCAddress, Length); break;
                    case "DEC":
                        if (Length == 1)
                        {
                            val = plc.InputWordBuffer[PLCAddress].ToString();
                        }
                        if (Length == 2)
                        {
                            val = plc.GetInt32(PLCAddress).ToString();
                        }

                        break;
                    default:
                        break;
                }
                return val;
            

        }


        public void SetValue(PlcComm plc, string value)
        {
           
                if (CIMAddress < plc.WriteStartWordAddress) return;
                if (Type.ToUpper() == "ASCII")
                {
                    plc.SetWordFromString(CIMAddress - plc.WriteStartWordAddress, value);
                }
                if (Type.ToUpper() == "DEC")
                {
                    if (Length == 1)
                    {
                        short val = short.Parse(value);
                        plc.SetWord(CIMAddress - plc.WriteStartWordAddress, val);
                    }
                    if (Length == 2)
                    {
                        plc.SetInt32(CIMAddress - plc.WriteStartWordAddress, value);
                    }
                }
            
        }
    }
    public class ECMModel
    {
        public string ECID { get; set; }
        public string ECNAME { get; set; }
        public string Comment { get; set; }
        public string PLCSTART { get; set; }
        public int Length { get; set; }
        public string Type { get; set; }
        public string BitEvent { get; set; }
        public int PLCAddress
        {
            get
            {
                return Convert.ToInt32(PLCSTART, 16);
            }
        }
        public string GetValue(PlcComm plc)
        {
                string val = "";
                switch (Type.ToUpper())
                {
                    case "ASCII": val = plc.GetWord(PLCAddress, Length); break;
                    case "DEC":
                        if (Length == 1)
                        {
                            val = plc.InputWordBuffer[PLCAddress].ToString();
                        }
                        if (Length == 2)
                        {
                            val = plc.GetInt32(PLCAddress).ToString();
                        }

                        break;
                    default:
                        break;
                }
                return val;
            

        }
    }
    public class MaterialModel:IWordModel
    {
        public string Area { get; set; }
        public string Item { get; set; }
        public string Comment { get; set; }
        public string Start { get; set; }
        public int Length { get; set; }
        public string Type { get; set; }
        public string BitEvent { get; set; }
        public int Address
        {
            get
            {
                return Convert.ToInt32(Start, 16);
            }
        }
        public string GetValue(PlcComm plc)
        {
            string val = "";
            switch (Type.ToUpper())
            {
                case "ASCII": val = plc.GetWord(Address, Length); break;
                case "DEC":
                    if (Length == 1)
                    {
                        val = plc.InputWordBuffer[Address].ToString();
                    }
                    if (Length == 2)
                    {
                        val = plc.GetInt32(Address).ToString();
                    }

                    break;
                default:
                    break;
            }
            return val;


        }
    }
}
