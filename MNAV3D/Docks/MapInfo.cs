using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using WeifenLuo.WinFormsUI.Docking;

namespace MAV3DSim.Docks
{
    public partial class MapInfo : DockContent
    {
        private PointLatLng position;
        ThreadSafe threadSafe = new ThreadSafe();
        public MapInfo()
        {
            InitializeComponent();
        }


        public PointLatLng Position
        {
            get { return position; }
            set { position = value; }
        }

        public double Lat
        {
            get { return position.Lat; }
            set
            {
                position.Lat = value;
                threadSafe.SetControlPropertyThreadSafe(txtLat, "Text", value.ToString());
            }
        }

        public double Lon
        {
            get { return position.Lng; }
            set
            {
                position.Lng = value;
                threadSafe.SetControlPropertyThreadSafe(txtLon, "Text", value.ToString());
            }
        }

        public double Alt
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtAlt, "Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtAlt, "Text", value.ToString()); }
        }

        public int NumSV
        {
            get { return Convert.ToInt32(threadSafe.GetControlPropertyThreadSafe(txtNumSV, "Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtNumSV, "Text", value.ToString()); }
        }

        public double Fix
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtFix, "Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtFix, "Text", value.ToString()); }
        }

        public int PosAccurancy
        {
            get { return Convert.ToInt32(threadSafe.GetControlPropertyThreadSafe(txtPosAcc, "Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtPosAcc, "Text", value.ToString()); }
        }

        public int VelAccurancy
        {
            get { return Convert.ToInt32(threadSafe.GetControlPropertyThreadSafe(txtVelAcc, "Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtVelAcc, "Text", value.ToString()); }
        }

    }
}
