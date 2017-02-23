using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zahrada
{
    static class Program
    {
        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6) // Toto vlozeno aby nebyly ikony tak rozmazane
                SetProcessDPIAware();
            // verse 6.0 = Windows Vista
            // verse 6.1 = Windows 7
            // 6.3 = Windows 8.1
            // 10.0 = Windows 10

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HlavniForm());
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")] // proti rozmazani ikon - import knihovny user32.dll abz mohla byt volana funkce SetProcessDPIaware
        private static extern bool SetProcessDPIAware(); // toto je funkce, ktera nastavi cely proces jako DPI aware (dots per inch]

    }
}
