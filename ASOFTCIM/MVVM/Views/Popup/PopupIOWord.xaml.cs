using A_SOFT.CMM.STYLE;
using A_SOFT.Ctl.Mitsu.Model;
using A_SOFT.PLC;
using ASOFTCIM.MainControl;
using ASOFTCIM.MVVM.ViewModels;
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
using System.Windows.Shapes;

namespace ASOFTCIM.MVVM.Views.Popup
{
    /// <summary>
    /// Interaction logic for PopupIOWord.xaml
    /// </summary>
    public partial class PopupIOWord : Window
    {
        private List<IWordModel> _wordModels;
        private Controller _controller;

        public PopupIOWord(List<IWordModel> wordModels)
        {
            InitializeComponent();
            _controller = MainWindowViewModel.Controller;
            _wordModels = wordModels;
            Initial();
        }
        #region Private Method
        private async void Initial()
        {
            var wordViewModels = new List<(IWordModel Word, string Address, string Comment, string Length, string Value, bool IsInput)>();

            // 1. Xử lý dữ liệu ở background thread
            await Task.Run(() =>
            {
                foreach (var word in _wordModels)
                {
                    string address = "W" + word.Start.ToString();
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

               

                
                    wrpInput.Children.Add(wordCmt);
                
            }
        }
        #endregion
    }
}
