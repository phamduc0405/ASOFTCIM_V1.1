using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Data
{
    public class JobProcess
    {
        public string RCMD { get; set; } = string.Empty;
        public string PARENTLOT { get; set; } = string.Empty;       // * Batch 단위의 ID
        public string RFID { get; set; } = string.Empty;            //* RF Tag에 부여 된 ID
        public string EQPID { get; set; } = string.Empty;           //* 설비 고유 ID
        public string UNITID { get; set; } = string.Empty;           //* 설비 고유 ID
        public string PORTNO { get; set; } = string.Empty;          //* Thông tin cổng được xác định trong EQ
        public string PPID { get; set; } = string.Empty;            // * Process Parameter Group ID(Recipe ID)
        public string CELLCNT { get; set; } = string.Empty;         // * Lot 종속 된 Cell 수량
        public string MESSAGE { get; set; } = string.Empty;         // * Lot 또는 Process Start명령을 설비에 전달하려는 정보
    }

    public class CellJobProcess
    {
        public string RCMD { get; set; } = string.Empty;
        public string JOBID { get; set; } = string.Empty;
        public string CELLID { get; set; } = string.Empty;
        public string PRODUCTID { get; set; } = string.Empty;
        public string STEPID { get; set; } = string.Empty;
        public string ACTIONTYPE { get; set; } = string.Empty;
        public string EQPID { get; set; } = string.Empty;
    }
    public class ApproveProcess
    {
        public string RCMD { get; set; } = string.Empty;
        public string APPROVECODE { get; set; } = string.Empty;
        public string APPROVEINFO { get; set; } = string.Empty;
        public string APPROVEID { get; set; } = string.Empty;
        public string BYWHO { get; set; } = string.Empty;
        public string APPROVETEXT { get; set; } = string.Empty;
    }
    public class TrsProcess
    {
        public string RCMD { get; set; } = string.Empty;
        public string PORTID { get; set; } = string.Empty;
        public string TRSNAME { get; set; } = string.Empty;
        public string JOBID { get; set; } = string.Empty;
        public string JOBTYPE { get; set; } = string.Empty;
        public string PRODUCTID { get; set; } = string.Empty;
        public string STEPID { get; set; } = string.Empty;
        public string SOURCELOC { get; set; } = string.Empty;
        public string SOURCEPORTID { get; set; } = string.Empty;
        public string FINALLOC { get; set; } = string.Empty;
        public string FINALPORTID { get; set; } = string.Empty;
        public string MIDLOC { get; set; } = string.Empty;
        public string MIDPORTID { get; set; } = string.Empty;
        public string ORIGINLOC { get; set; } = string.Empty;
        public string PRIORITY { get; set; } = string.Empty;
        public string DESCRIPTION { get; set; } = string.Empty;

    }
}
