using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MAV3DSim.Docks
{
    public partial class Altimeter : DockContent
    {
        ThreadSafe threadSafe = new ThreadSafe();
        public Altimeter()
        {
            InitializeComponent();
        }

        public int Altitud
        {
            set { threadSafe.InvokeControlMethodThreadSafe(altimeterInstrumentControl1, "SetAlimeterParameters", value); }
        }
    }
}
