using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASOFTCIM.MainControl
{
    public class TRACESV
    {
        public string TRID { get; set; }
        public int DSPER { get; set; } //* Data Sample Period "hms.mmm" : 시간주기
        public int TOTSMP { get; set; } //* 데이터 보고 Count
        public string REPGSZ { get; set; } //* Reporting Group Size
        public int SMPLN { get; set; } = 0;//* Sample Number
        public List<string> SVs { get; set; } = new List<string>();

        public delegate void TraceSvEventDelegate(List<string> lstSv, bool isEnd);
        public event TraceSvEventDelegate TraceSvEvent;

        Thread _thread;
        public TRACESV()
        {
            _thread = new Thread(Trace);
        }
        ~TRACESV()
        {
            _thread.Abort();
            _thread = null;
        }
        public void Init(List<string> svs, string trid, int dsper, int count, string repgsz)
        {
            TRID = trid;
            DSPER = dsper;
            TOTSMP = count;
            REPGSZ = repgsz;
            SVs = svs;
            SMPLN = 0;
        }
        public void Start()
        {
            _thread.Start();
        }
        public void Trace()
        {
            while (SMPLN < TOTSMP)
            {
                SMPLN++;
                TranceEventHandle(SVs, SMPLN == TOTSMP);
                Thread.Sleep(DSPER);
            }
        }

        private void TranceEventHandle(List<string> lstSv, bool isEnd)
        {
            var handle = TraceSvEvent;
            if (handle != null)
                handle(lstSv, isEnd);

        }
    }
}
