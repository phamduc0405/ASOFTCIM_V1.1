using A_SOFT.CMM.STYLE;
using A_SOFT.PLC;
using ASOFTCIM.Helper;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.View.Popup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static OfficeOpenXml.ExcelErrorValue;

namespace ASOFTCIM.MVVM.View.Monitor
{
    /// <summary>
    /// Interaction logic for MonitorBits.xaml
    /// </summary>
    public partial class MonitorBits : UserControl
    {
        private PopupIOWord _popupWindow;
        private PLCHelper _plcH;
        private PlcComm _plc;
        private Controller _controller;
        private bool _disposed = false; // Theo dõi trạng thái dispose
        private List<BitModel.BitChangedEventDelegate> _bitChangedHandlers = new List<BitModel.BitChangedEventDelegate>();
        private List<BitModel.BitOutChangedEventDelegate> _bitOutChangedHandlers = new List<BitModel.BitOutChangedEventDelegate>();
        public MonitorBits()
        {
            
            _controller = MainWindow.Controller;
            _plc = _controller.CIM.PLC;
            _plcH = _controller.CIM.PLCH;
            
            InitializeComponent();
            this.DataContext = this;
            Initial();
        }
        #region Private Method
        private void Initial()
        {
            if (_plcH == null) return;
            this.MouseLeave += (s, e) =>
            {
              //  _popupWindow?.Close();
            };
            
            // Unloaded += MonitorBits_Unloaded;// Khai báo biến để lưu trữ tham chiếu đến hàm xử lý sự kiện BitOutChangedEvent
            //   EventHandler bitOutChangedHandler = null;
            foreach (var bit in _plcH.Bits)
            {
                BitModel b = new BitModel(_plc);
                b = bit;

                // Input
                IOComment io = CreateIOComment(bit, b.GetPLCValue, true);
                io.InitializeComponent();
                wrpInput.Children.Add(io);

                var bitChangedHandler = new BitModel.BitChangedEventDelegate(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        io.IsOn = b.GetPLCValue;

                        io.UpdateEffect();
                    });
                });
                b.BitChangedEvent += bitChangedHandler;
                _bitChangedHandlers.Add(bitChangedHandler);
                io.MouseEnter += (s, e) =>
                {
                    _popupWindow?.Close();
                    _popupWindow = new PopupIOWord(b.LstWord);
                    _popupWindow.Left = Mouse.GetPosition(Application.Current.MainWindow).X + Application.Current.MainWindow.Left + 10;
                    _popupWindow.Top = Mouse.GetPosition(Application.Current.MainWindow).Y + Application.Current.MainWindow.Top + 10;
                    _popupWindow.Show();
                };
                io.MouseLeave += (s, e) =>
                {
                      _popupWindow?.Close();
                };
                // Output
                IOComment ioOut = CreateIOComment(bit, b.GetPCValue, false);
                wrpOutput.Children.Add(ioOut);
                ioOut.ElipClickEvent += () =>
                {
                    b.SetPCValue = !b.GetPCValue;
                };
               
                var bitOutChangedHandler = new BitModel.BitOutChangedEventDelegate(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        ioOut.IsOn = b.GetPCValue ;
                        ioOut.UpdateEffect();
                    });
                });
                b.BitOutChangedEvent += bitOutChangedHandler;
                _bitOutChangedHandlers.Add(bitOutChangedHandler);
            }
        }
        private IOComment CreateIOComment(BitModel bit, bool value, bool isPLC)
        {
            IOComment io = new IOComment();
            if (isPLC)
            {
                io.Address = "B" + bit.PLCHexAdd.ToString();
            }
            else
            {
                io.Address = "B" + bit.PCHexAdd.ToString();
            }
            io.Comment = bit.Comment.ToString();
            io.IsOn = value ;
            io.UpdateEffect();
            return io;
        }

        #endregion
        #region IDisposable Implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Ngăn GC gọi lại Dispose lần nữa
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Giải phóng các sự kiện đã đăng ký
                    UnsubscribeAllEvents();
                    wrpInput.Children.Clear();
                    wrpOutput.Children.Clear();
                }

                _disposed = true;
            }
        }

        private void UnsubscribeAllEvents()
        {
            // Hủy tất cả các sự kiện đã đăng ký
            foreach (var handler in _bitChangedHandlers)
            {
                foreach (var bit in _plcH.Bits)
                {
                    bit.BitChangedEvent -= handler;
                }
            }
            foreach (var handler in _bitOutChangedHandlers)
            {
                foreach (var bit in _plcH.Bits)
                {
                    bit.BitOutChangedEvent -= handler;
                }
            }
            _bitChangedHandlers.Clear();
            _bitOutChangedHandlers.Clear();
        }

        ~MonitorBits()
        {
            Dispose(false); // Gọi Dispose từ finalizer nếu chưa được gọi
        }
        #endregion
    }
}
