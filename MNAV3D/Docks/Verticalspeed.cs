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
    public partial class Verticalspeed : DockContent
    {
        ThreadSafe threadSafe = new ThreadSafe();
        public Verticalspeed()
        {
            InitializeComponent();
        }

        public int VerticalSpeed
        {
            set { threadSafe.InvokeControlMethodThreadSafe(verticalSpeedIndicatorInstrumentControl1, "SetVerticalSpeedIndicatorParameters", value); }
        }

    }
}
