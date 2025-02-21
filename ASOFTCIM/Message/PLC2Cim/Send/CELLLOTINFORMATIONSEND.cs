using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using ASOFTCIM.Data;
using ASOFTCIM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Message.PLC2Cim.Send
{
    public class CELLLOTINFORMATIONSEND11
    {
        public CELLLOTINFORMATIONSEND11(PLCHelper plcdata, CELLLOTINFODOWNLOAD cellLot)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = cellLot.CELLID;
                word.FirstOrDefault(x => x.Item == "CASSETTEID").SetValue = cellLot.CASSETTEID;
                word.FirstOrDefault(x => x.Item == "BATCHLOT").SetValue = cellLot.BATCHLOT;
                word.FirstOrDefault(x => x.Item == "PRODUCTID").SetValue = cellLot.PRODUCTID;
                word.FirstOrDefault(x => x.Item == "PRODUCT_TYPE").SetValue = cellLot.PRODUCT_TYPE;
                word.FirstOrDefault(x => x.Item == "PRODUCT_KIND").SetValue = cellLot.PRODUCT_KIND;
                word.FirstOrDefault(x => x.Item == "PRODUCTSPEC").SetValue = cellLot.PRODUCTSPEC;
                word.FirstOrDefault(x => x.Item == "STEPID").SetValue = cellLot.STEPID;
                word.FirstOrDefault(x => x.Item == "CELL_SIZE").SetValue = cellLot.CELL_SIZE;
                word.FirstOrDefault(x => x.Item == "CELL_THICKNESS").SetValue = cellLot.CELL_THICKNESS;
                word.FirstOrDefault(x => x.Item == "COMMENT").SetValue = cellLot.COMMENT;


                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name);
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }
    public class CELLLOTINFORMATIONSEND12
    {
        public CELLLOTINFORMATIONSEND12(PLCHelper plcdata, CELLLOTINFODOWNLOAD cellLot)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = cellLot.CELLID;
                word.FirstOrDefault(x => x.Item == "CASSETTEID").SetValue = cellLot.CASSETTEID;
                word.FirstOrDefault(x => x.Item == "BATCHLOT").SetValue = cellLot.BATCHLOT;
                word.FirstOrDefault(x => x.Item == "PRODUCTID").SetValue = cellLot.PRODUCTID;
                word.FirstOrDefault(x => x.Item == "PRODUCT_TYPE").SetValue = cellLot.PRODUCT_TYPE;
                word.FirstOrDefault(x => x.Item == "PRODUCT_KIND").SetValue = cellLot.PRODUCT_KIND;
                word.FirstOrDefault(x => x.Item == "PRODUCTSPEC").SetValue = cellLot.PRODUCTSPEC;
                word.FirstOrDefault(x => x.Item == "STEPID").SetValue = cellLot.STEPID;
                word.FirstOrDefault(x => x.Item == "CELL_SIZE").SetValue = cellLot.CELL_SIZE;
                word.FirstOrDefault(x => x.Item == "CELL_THICKNESS").SetValue = cellLot.CELL_THICKNESS;
                word.FirstOrDefault(x => x.Item == "COMMENT").SetValue = cellLot.COMMENT;


                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name);
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }
    public class CELLLOTINFORMATIONSEND21
    {
        public CELLLOTINFORMATIONSEND21(PLCHelper plcdata, CELLLOTINFODOWNLOAD cellLot)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = cellLot.CELLID;
                word.FirstOrDefault(x => x.Item == "CASSETTEID").SetValue = cellLot.CASSETTEID;
                word.FirstOrDefault(x => x.Item == "BATCHLOT").SetValue = cellLot.BATCHLOT;
                word.FirstOrDefault(x => x.Item == "PRODUCTID").SetValue = cellLot.PRODUCTID;
                word.FirstOrDefault(x => x.Item == "PRODUCT_TYPE").SetValue = cellLot.PRODUCT_TYPE;
                word.FirstOrDefault(x => x.Item == "PRODUCT_KIND").SetValue = cellLot.PRODUCT_KIND;
                word.FirstOrDefault(x => x.Item == "PRODUCTSPEC").SetValue = cellLot.PRODUCTSPEC;
                word.FirstOrDefault(x => x.Item == "STEPID").SetValue = cellLot.STEPID;
                word.FirstOrDefault(x => x.Item == "CELL_SIZE").SetValue = cellLot.CELL_SIZE;
                word.FirstOrDefault(x => x.Item == "CELL_THICKNESS").SetValue = cellLot.CELL_THICKNESS;
                word.FirstOrDefault(x => x.Item == "COMMENT").SetValue = cellLot.COMMENT;


                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name);
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }
    public class CELLLOTINFORMATIONSEND22
    {
        public CELLLOTINFORMATIONSEND22(PLCHelper plcdata, CELLLOTINFODOWNLOAD cellLot)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = cellLot.CELLID;
                word.FirstOrDefault(x => x.Item == "CASSETTEID").SetValue = cellLot.CASSETTEID;
                word.FirstOrDefault(x => x.Item == "BATCHLOT").SetValue = cellLot.BATCHLOT;
                word.FirstOrDefault(x => x.Item == "PRODUCTID").SetValue = cellLot.PRODUCTID;
                word.FirstOrDefault(x => x.Item == "PRODUCT_TYPE").SetValue = cellLot.PRODUCT_TYPE;
                word.FirstOrDefault(x => x.Item == "PRODUCT_KIND").SetValue = cellLot.PRODUCT_KIND;
                word.FirstOrDefault(x => x.Item == "PRODUCTSPEC").SetValue = cellLot.PRODUCTSPEC;
                word.FirstOrDefault(x => x.Item == "STEPID").SetValue = cellLot.STEPID;
                word.FirstOrDefault(x => x.Item == "CELL_SIZE").SetValue = cellLot.CELL_SIZE;
                word.FirstOrDefault(x => x.Item == "CELL_THICKNESS").SetValue = cellLot.CELL_THICKNESS;
                word.FirstOrDefault(x => x.Item == "COMMENT").SetValue = cellLot.COMMENT;


                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name);
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }
    public class CELLLOTINFORMATIONSEND31
    {
        public CELLLOTINFORMATIONSEND31(PLCHelper plcdata, CELLLOTINFODOWNLOAD cellLot)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = cellLot.CELLID;
                word.FirstOrDefault(x => x.Item == "CASSETTEID").SetValue = cellLot.CASSETTEID;
                word.FirstOrDefault(x => x.Item == "BATCHLOT").SetValue = cellLot.BATCHLOT;
                word.FirstOrDefault(x => x.Item == "PRODUCTID").SetValue = cellLot.PRODUCTID;
                word.FirstOrDefault(x => x.Item == "PRODUCT_TYPE").SetValue = cellLot.PRODUCT_TYPE;
                word.FirstOrDefault(x => x.Item == "PRODUCT_KIND").SetValue = cellLot.PRODUCT_KIND;
                word.FirstOrDefault(x => x.Item == "PRODUCTSPEC").SetValue = cellLot.PRODUCTSPEC;
                word.FirstOrDefault(x => x.Item == "STEPID").SetValue = cellLot.STEPID;
                word.FirstOrDefault(x => x.Item == "CELL_SIZE").SetValue = cellLot.CELL_SIZE;
                word.FirstOrDefault(x => x.Item == "CELL_THICKNESS").SetValue = cellLot.CELL_THICKNESS;
                word.FirstOrDefault(x => x.Item == "COMMENT").SetValue = cellLot.COMMENT;


                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name);
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }
    public class CELLLOTINFORMATIONSEND32
    {
        public CELLLOTINFORMATIONSEND32(PLCHelper plcdata, CELLLOTINFODOWNLOAD cellLot)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "CELLID").SetValue = cellLot.CELLID;
                word.FirstOrDefault(x => x.Item == "CASSETTEID").SetValue = cellLot.CASSETTEID;
                word.FirstOrDefault(x => x.Item == "BATCHLOT").SetValue = cellLot.BATCHLOT;
                word.FirstOrDefault(x => x.Item == "PRODUCTID").SetValue = cellLot.PRODUCTID;
                word.FirstOrDefault(x => x.Item == "PRODUCT_TYPE").SetValue = cellLot.PRODUCT_TYPE;
                word.FirstOrDefault(x => x.Item == "PRODUCT_KIND").SetValue = cellLot.PRODUCT_KIND;
                word.FirstOrDefault(x => x.Item == "PRODUCTSPEC").SetValue = cellLot.PRODUCTSPEC;
                word.FirstOrDefault(x => x.Item == "STEPID").SetValue = cellLot.STEPID;
                word.FirstOrDefault(x => x.Item == "CELL_SIZE").SetValue = cellLot.CELL_SIZE;
                word.FirstOrDefault(x => x.Item == "CELL_THICKNESS").SetValue = cellLot.CELL_THICKNESS;
                word.FirstOrDefault(x => x.Item == "COMMENT").SetValue = cellLot.COMMENT;


                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name);
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }

}
