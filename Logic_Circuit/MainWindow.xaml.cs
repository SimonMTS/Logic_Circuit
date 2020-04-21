using Logic_Circuit.Models;
using Logic_Circuit.Parser;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Logic_Circuit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void SelectFile(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "TXT Files (*.txt)|*.txt";

            bool? success = dlg.ShowDialog();

            if (success == true)
            {
                string content = File.ReadAllText(dlg.FileName);

                (bool, Circuit, string) c = Parse.Try( content );

                if (c.Item1)
                {
                    ResultWindow result = new ResultWindow(c.Item2);
                    result.SizeToContent = SizeToContent.WidthAndHeight;
                    result.Title = System.IO.Path.GetFileName(dlg.FileName);
                    App.Current.MainWindow = result;
                    result.Show();
                }
                else
                {
                    Console.WriteLine(c.Item3);
                    textBlock.Text = c.Item3;
                }
            }
        }
    }
}
