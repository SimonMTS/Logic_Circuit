using Logic_Circuit.Parser;
using Logic_Circuit.Models.Circuits;
using Logic_Circuit.Models.Factories;
using System;
using System.Windows;
using Microsoft.Win32;

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
            OpenFileDialog dlg = new OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "TXT Files (*.txt)|*.txt"
            };

            bool? success = dlg.ShowDialog();

            if (success == true)
            {
                Circuit circuit = CircuitFactory.GetFromFile(dlg.FileName);

                if (circuit != null)
                {
                    ResultWindow res = new ResultWindow(circuit);
                    res.SizeToContent = SizeToContent.WidthAndHeight;
                    res.Title = System.IO.Path.GetFileName(dlg.FileName);
                    App.Current.MainWindow = res;
                    res.Show();
                }
                else
                {
                    Console.WriteLine("result.error");
                }
            }
        }
    }
}
