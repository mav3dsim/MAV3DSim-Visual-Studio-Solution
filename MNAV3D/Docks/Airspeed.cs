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
    public partial class Airspeed : DockContent
    {
        ThreadSafe threadSafe = new ThreadSafe();
        public Airspeed()
        {
            InitializeComponent();
        }

        public int AirSpeed
        {
            set { threadSafe.InvokeControlMethodThreadSafe(airSpeedIndicatorInstrumentControl1, "SetAirSpeedIndicatorParameters", value); }
        }

    }
}
