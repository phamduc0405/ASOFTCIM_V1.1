using A_SOFT.CMM.INIT;
using A_SOFT.PLC;
using ASOFTCIM.Data;
using ASOFTCIM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASOFTCIM.Message.PLC2Cim.Send
{
    public class DATETIMESET
    {
        public DATETIMESET(PLCHelper plcdata, string datetime)
        {

            try
            {
                List<WordModel> word = plcdata.Words.Where(x => x.Area == this.GetType().Name).ToList();
                word.FirstOrDefault(x => x.Item == "DATETIMESET").SetValue = datetime;
                BitModel bit = plcdata.Bits.First(x => x.Comment == this.GetType().Name);
                bit.SetPCValue = true;
                SetTimePC(datetime);
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }

        }
        public void SetTimePC(string datetime)
        {
            DateTime dateTime = DateTime.ParseExact(datetime, "yyyyMMddHHmmss", null);
            SYSTEMTIME st = new SYSTEMTIME();
            st.Year = ushort.Parse(dateTime.Year.ToString());    // Năm
            st.Month = ushort.Parse(dateTime.Month.ToString());      // Tháng
            st.Day = ushort.Parse(dateTime.Day.ToString());        // Ngày
            st.Hour = ushort.Parse(dateTime.Hour.ToString());      // Giờ
            st.Minute = ushort.Parse(dateTime.Minute.ToString());    // Phút
            st.Second = ushort.Parse(dateTime.Second.ToString());     // Giây
            st.Milliseconds = ushort.Parse(dateTime.Millisecond.ToString()); // Millisecond
            bool result = SetSystemTime(ref st);
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME st);
        public struct SYSTEMTIME
        {
            public ushort Year;
            public ushort Month;
            public ushort DayOfWeek;
            public ushort Day;
            public ushort Hour;
            public ushort Minute;
            public ushort Second;
            public ushort Milliseconds;
        }
    }
}
