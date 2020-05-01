using Logic_Circuit.Models.Circuits;
using Logic_Circuit.Models.Factories;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Controllers
{
    public class MainController
    {
        public void SelectFile(IMainWin win)
        {
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
