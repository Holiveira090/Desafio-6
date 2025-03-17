using System;
using System.Windows.Forms;
using FarmacySystem.view;

namespace FarmacySystem.view.Farmaceutico
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CadastroUForm());
        }
    }
}
