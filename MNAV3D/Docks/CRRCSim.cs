using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.Runtime.InteropServices;
using WeifenLuo.WinFormsUI.Docking;

namespace MAV3DSim.Docks
{
    public partial class CRRCSim : DockContent
    {
        private Process pDocked;
        private IntPtr hWndOriginalParent;
        private IntPtr hWndDocked;
        private IntPtr hWndParent;

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public CRRCSim()
        {
            InitializeComponent();
            dockIt();
            
        }
        private void dockIt()
        {
            if (hWndDocked != IntPtr.Zero) //don't do anything if there's already a window docked.
                return;
            hWndParent = IntPtr.Zero;

            foreach (var process in Process.GetProcessesByName("crrcsim"))
            {
                process.Kill();
            }

            pDocked = Process.Start(@"C:\MinGW3\msys\1.0\home\1064N\crrcsim-0.9.11\crrcsim-0.9.11\crrcsim.exe");
            while (hWndDocked == IntPtr.Zero)
            {
                pDocked.WaitForInputIdle(1000); //wait for the window to be ready for input;
                pDocked.Refresh();              //update process info
                if (pDocked.HasExited)
                {
                    return; //abort if the process finished before we got a handle.
                }
                hWndDocked = pDocked.MainWindowHandle;  //cache the window handle
            }
            //Windows API call to change the parent of the target window.
            //It returns the hWnd of the window's parent prior to this call.
            hWndOriginalParent = SetParent(hWndDocked, Panel1.Handle);

            //Wire up the event to keep the window sized to match the control
            Panel1.SizeChanged += new EventHandler(Panel1_Resize);
            //Perform an initial call to set the size.
            Panel1_Resize(new Object(), new EventArgs());
        }

        private void undockIt()
        {
            //Restores the application to it's original parent.
            SetParent(hWndDocked, hWndOriginalParent);
        }

        private void Panel1_Resize(object sender, EventArgs e)
        {
            //Change the docked windows size to match its parent's size. 
            MoveWindow(hWndDocked, 0, 0, Panel1.Width, Panel1.Height, true);
        }

        private void CRRCSim_FormClosing(object sender, FormClosingEventArgs e)
        {
            pDocked.Kill();
        } 

    }
}
