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

        public Thread _thread;
        private bool isRunning = false;
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
            if (isRunning) return; // Tránh Start nhiều lần
            isRunning = true;
            _thread.Start();
        }
        public void Stop()
        {
            isRunning = false;  
            if (_thread != null && _thread.IsAlive)
            {
                _thread.Join(); 
            }
        }
        public void Trace()
        {
            while ((isRunning && SMPLN < TOTSMP) || (isRunning && TOTSMP == 0))
            {
                SMPLN++;
                TranceEventHandle(SVs, SMPLN == TOTSMP);
                if(SMPLN == TOTSMP && TOTSMP != 0)
                {
                    return;
                }    
                if (!isRunning) return;
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
