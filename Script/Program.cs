using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Script
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MessageBox.Show("LeagueOrbwalker\nCreado por Zorbuk.\nhttps://github.com/zorbuk/LeagueOrbwalker");

            Application.Run(new Main());
        }
    }
}
