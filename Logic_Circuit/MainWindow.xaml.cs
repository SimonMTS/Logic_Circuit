using Logic_Circuit.Parser;
using Logic_Circuit.Models.Circuits;
using Logic_Circuit.Models.Factories;
using System;
using System.Windows;
using Microsoft.Win32;
using Logic_Circuit.Controllers;

namespace Logic_Circuit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWin
    {
        private readonly MainController controller = new MainController();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void OnSelectFile(object sender, RoutedEventArgs e)
        {
            controller.SelectFile(this);
        }

        public void SpawnResultWindow(string name, Circuit circuit)
        {
            ResultWindow res = new ResultWindow(circuit);
            res.SizeToContent = SizeToContent.WidthAndHeight;
            res.Title = System.IO.Path.GetFileName(name);
            App.Current.MainWindow = res;
            res.Show();
        }

        public void SetErrorText(string text)
        {
            textBlock.Text = text;
        }
    }
}
