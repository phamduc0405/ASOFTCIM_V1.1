using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using ASOFTCIM.Data;
using ASOFTCIM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static A_SOFT.PLC.MelsecIF;
using System.Windows.Shapes;
using ASOFTCIM.Config;
using A_SOFT.Ctl.SecGem;
using Type = System.Type;
using FontAwesome.Sharp;
using ASOFTCIM.Message.PLC2Cim.Recv;
using ASOFTCIM.MainControl.Device.PC;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        #region Field
        private CimHelper _cim;
        private PlcComm _plc;
        private PLCHelper _plcH;
        public EQPDATA EqpData { get; set; }
        public string EQPID { get; set; } = "EQPTEST";
        public EquipmentConfig _eqpConfig;


        private bool _isEqStatusUpdate = false;
        private bool _isSvidUpdate = false;
        private List<string> _unitUpdate = new List<string>();
        #endregion



        #region Property
        public EquipmentConfig EqpConfig
        {
            get { return _eqpConfig; }
            set { _eqpConfig = value; }
        }
        public CimHelper Cim
        {
            get { return _cim; }
            set { _cim = value; }
        }
        public PlcComm PLC
        {
            get { return _plc; }
            set { _plc = value; }
        }
        public PLCHelper PLCH
        {
            get { return _plcH; }
            set { _plcH = value; }
        }
        #endregion

        public ACIM()
        {
            Initial();
            _cim = new CimHelper(EQPID);
            _cim.Init(ATCPIP.ConnectMode.Passive, "127.0.0.1", 8000);
            _cim.SysPacketEvent += _cim_SysPacketEvent;
            _plc = new PlcComm();

            LoadExcelConfig(@"D:\DucPH\Project\PCControl\SAMSUNG\CIM\Document\SDCCIM_ASOFT_Portal_Online_Map_SDC_Basic_V1.21_v0.1.xlsx");
            InitialPlc();
        }
        public void Stop()
        {
            _cim.Close();
        }
        private void Initial()
        {
            EqpData = new EQPDATA();
        }
        
        private void _cim_SysPacketEvent(SysPacket sysPacket)
        {
            EqpData.TransactionSys = sysPacket.SystemByte;
            EqpData.DeviceId = sysPacket.DeviceId;
            MethodInfo method = this.GetType().GetMethod($"RecvS{sysPacket.Stream}F{sysPacket.Function}");
            if (method != null)
            {
                object result = method.Invoke(this,null);
                sysPacket.MakeCimLog();
                return;
            }
            else
            {
                SendS9F3(sysPacket);
                return;
            }

        }
        public Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }

        public void AddTrans(uint trans)
        {
            _cim.AddTrans(trans);
        }
    }


   
}
