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

            Nullable<bool> success = dlg.ShowDialog();

            if (success == true)
            {
                string content = File.ReadAllText(dlg.FileName);

                Circuit c = Parse.Try( content );

                ResultWindow result = new ResultWindow( c );
                App.Current.MainWindow = result;
                this.Close();
                result.Show();
            }
        }
    }
}
