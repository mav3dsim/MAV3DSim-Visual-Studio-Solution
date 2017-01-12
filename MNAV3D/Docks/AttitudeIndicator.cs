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
    public partial class AttitudeIndicator : DockContent
    {
        ThreadSafe threadSafe = new ThreadSafe();
        public AttitudeIndicator()
        {
            InitializeComponent();
        }



        public double Roll
        {
            set { threadSafe.InvokeControlMethodThreadSafe(attitudeIndicatorInstrumentControl1, "SetRollAngle", value); }
        }

        public double Pitch
        {
            set { threadSafe.InvokeControlMethodThreadSafe(attitudeIndicatorInstrumentControl1, "SetPitchAngle", value); }
        }

    }
}
