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
    public partial class PlotScale : DockContent
    {
        ThreadSafe threadSafe;
        public PlotScale()
        {
            InitializeComponent();
            threadSafe = new ThreadSafe();
        }

        public int Scale
        {
            get { return Convert.ToInt32(threadSafe.GetControlPropertyThreadSafe(tbScale,"Value")); }
            set { threadSafe.SetControlPropertyThreadSafe(tbScale,"Value", value); }
        }
        


    }
}
