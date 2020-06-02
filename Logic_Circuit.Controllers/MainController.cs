using Logic_Circuit.Models;
using Logic_Circuit.Models.Circuits;
using Logic_Circuit.Models.Factories;
using Microsoft.Win32;
using System;

namespace Logic_Circuit.Controllers
{
    /// <summary>
    /// Allows user to select a file to open in a ResultWindow.
    /// </summary>
    public class MainController
    {
        public void SelectFile(IMainWin win)
        {
            Cache.IncUserActionCounter();

            OpenFileDialog dlg = new OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "TXT Files (*.txt)|*.txt"
            };

            bool? success = dlg.ShowDialog();

            if (success == true)
            {
                var circuit = CircuitFactory.GetFromFile(dlg.FileName);

                if (circuit.success)
                {
                    win.SpawnResultWindow(dlg.FileName, circuit.circuit);
                }
                else
                {
                    Console.WriteLine(circuit.error);
                    win.SetErrorText(circuit.error);
                }
            }
        }
    }

    public interface IMainWin
    {
        void SpawnResultWindow(string name, Circuit circuit);

        void SetErrorText(string text);
    }
}
