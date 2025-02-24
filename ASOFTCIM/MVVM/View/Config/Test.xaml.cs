using ASOFTCIM.MainControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Xml.Linq;

namespace ASOFTCIM.MVVM.View.Config
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : UserControl
    {
        private Controller _controller;
        public Test()
        {
            InitializeComponent();
            Initial();
            CreaterEvent();
        }
        private void Initial()
        {
            _controller = MainWindow.Controller;
        }
        private void CreaterEvent()
        {
            btnTest.Click += (s, e) =>
            {
                //Controller.ACIM.SendS1F1();
                string namespaces = "ASOFTCIM.Message.PLC2Cim.Send";
                Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), namespaces);
                Type t = Assembly.GetExecutingAssembly().GetType($"{namespaces}.{txtNameClass.Text}");

                if (t != null && typelist.Contains(t))
                {
                    // Tạo instance của class với constructor có tham số
                    //PLCHelper plcHelper = new PLCHelper();
                    string Name = $"{txtNameClass.Text}";
                    string classss = $"{txtNameObj.Text}";
                    object batchlotInstance = CreateInstanceFromName(Name, classss);

                    //BATCHLOT batchlot = new BATCHLOT();

                    try
                    {
                        string m = "";
                        object instance = Activator.CreateInstance(t, new object[] { _controller.CIM.PLCH, batchlotInstance });

                        if (instance != null)
                        {
                            Console.WriteLine($"Tạo instance của {txtNameClass.Text} thành công!");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi tạo instance: {ex.Message}");
                    }
                }
            };
        }
        public Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }
        public static object CreateInstanceFromName(string Name, string classs)
        {
            string fullClassName = $"ASOFTCIM.Data.{classs}"; // Định dạng namespace + class

            // Lấy Type từ Assembly
            Type t = Assembly.GetExecutingAssembly().GetType(fullClassName);
            if (t == null) return null; // Trả về null nếu không tìm thấy

            try
            {
                return Activator.CreateInstance(t); // Tạo instance
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tạo instance: {ex.Message}");
                return null;
            }
        }
    }
}
