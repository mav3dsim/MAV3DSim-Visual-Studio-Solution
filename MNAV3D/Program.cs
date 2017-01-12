using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAV3DSim
{
    static class Program
    {
        private static Docks.SplashScreen splashScreen;
        private static Mav3DSim mnav3dsim;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(ShowAssemblies);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            splashScreen = new Docks.SplashScreen();
            splashScreen.Show();
            //AppDomain.CurrentDomain.AssemblyLoad -= ShowAssemblies;
            Mav3DSim mnav3dsim = new Mav3DSim();
            mnav3dsim.Load +=mnav3dsim_Load;
            Application.Run(mnav3dsim);
        }

        static void mnav3dsim_Load(object sender, EventArgs e)
        {
            splashScreen.Close();
        }

        static void Application_Idle(object sender, EventArgs e)
        {
            splashScreen.Close();
        }

        private static void ShowAssemblies(object sender, AssemblyLoadEventArgs e)
        {
            // Store name of assembly in the queue
            
            Docks.SplashScreen.AsmLoads.Enqueue(e.LoadedAssembly.GetName().Name);
        }
    }
}
