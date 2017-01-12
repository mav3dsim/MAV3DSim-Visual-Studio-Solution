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
    public partial class Compass : DockContent
    {
        ThreadSafe threadSafe = new ThreadSafe();
        public Compass()
        {
            InitializeComponent();
        }

        public int Heading
        {
            set { threadSafe.InvokeControlMethodThreadSafe(headingIndicatorInstrumentControl1, "SetHeadingIndicatorParameters", value); }
        }


    }
}
