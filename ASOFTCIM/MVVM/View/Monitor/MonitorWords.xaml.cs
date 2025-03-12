using A_SOFT.CMM.STYLE;
using A_SOFT.PLC;
using ASOFTCIM.MainControl;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ASOFTCIM.MVVM.View.Monitor
{
    /// <summary>
    /// Interaction logic for MonitorWords.xaml
    /// </summary>
    public partial class MonitorWords : UserControl
    {
        private Controller _controller;
        public MonitorWords()
        {
            InitializeComponent();
            _controller = MainWindow.Controller;
            Initial();
        }
        #region Private Method
        private async void Initial()
        {
            var wordViewModels = new List<(WordModel Word, string Address, string Comment, string Length, string Value, bool IsInput)>();

            // 1. Xử lý dữ liệu ở background thread
            await Task.Run(() =>
            {
                foreach (var word in _controller.CIM.PLCH.Words)
                {
                    string address = word.Device + word.Start.ToString();
                    string comment = string.IsNullOrEmpty(word.Comment) ? "" : word.Comment;
                    string length = word.Length.ToString();
                    string value = word.GetValue(_controller.CIM.PLC).ToString();
                    bool isInput = word.Address < _controller.CIM.PLC.WriteStartWordAddress;

                    wordViewModels.Add((word, address, comment, length, value, isInput));
                }
            });

            // 2. Tạo control UI và cập nhật UI thread
            foreach (var vm in wordViewModels)
            {
                var wordCmt = new WordComment
                {
                    Address = vm.Address,
                    Comment = vm.Comment,
                    Length = vm.Length,
                    Value = vm.Value
                };

                vm.Word.WordChangedEvent += () =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        wordCmt.Value = vm.Word.GetValue(_controller.CIM.PLC).ToString();
                    });
                };

                if (vm.IsInput)
                    wrpInput.Children.Add(wordCmt);
                else
                    wrpOutput.Children.Add(wordCmt);
            }
        }
        #endregion
    }
}
