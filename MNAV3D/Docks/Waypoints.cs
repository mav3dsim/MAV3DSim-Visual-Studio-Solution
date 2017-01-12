using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MAV3DSim.Docks
{
    public partial class Waypoints : DockContent
    {
        ThreadSafe threadSafe;

        public Waypoints()
        {
            InitializeComponent();
            threadSafe = new ThreadSafe();
        }
        public void NewWaypoint(PointLatLngAlt waypoint, int count)
        {
            ToolStrip ts = new ToolStrip();
            /*ts.Items.Add("WP " + count.ToString());
            ts.Items.Add(new ToolStripSeparator());
            ts.Items.Add("Lat:");
            ToolStripTextBox tstb = new ToolStripTextBox();
            tstb.Text = waypoint.Lat.ToString();
            ts.Items.Add(tstb);
            //ts.Items.Add(new ToolStripSeparator());
            ts.Items.Add("Lon:");
            tstb = new ToolStripTextBox();
            tstb.Text = waypoint.Lng.ToString();
            ts.Items.Add(tstb);
            /*ts.Items.Add("|Alt:" + count.ToString());
            tstb.Text = waypoint.Alt.ToString();
            ts.Items.Add(tstb);
            /*ts.Items.Add(new ToolStripTextBox(waypoint.Lng.ToString()));
            ts.Items.Add(new ToolStripSeparator());
            ts.Items.Add(new ToolStripTextBox(waypoint.Alt.ToString()));
            ts.Items.Add(new ToolStripSeparator());
            */

            ToolStripLabel l0 = new ToolStripLabel();
            l0.Text = "WP " + count.ToString();
            ToolStripLabel l1 = new ToolStripLabel();
            l1.Text = "Lat:";
            ToolStripTextBox tstb1 = new ToolStripTextBox();
            tstb1.Text = waypoint.Lat.ToString();
            ToolStripLabel l2 = new ToolStripLabel();
            l2.Text = "Lon:";
            ToolStripTextBox tstb2 = new ToolStripTextBox();
            tstb2.Text = waypoint.Lng.ToString();
            ToolStripLabel l3 = new ToolStripLabel();
            l3.Text = "Alt:";
            ToolStripTextBox tstb3 = new ToolStripTextBox();
            tstb3.Text = waypoint.Alt.ToString();



            ts.BackColor = Color.FromArgb(45, 45, 48);
            ts.ForeColor = Color.White;
            

            ts.Items.AddRange(new ToolStripItem[]{
                l0, new ToolStripSeparator(), l1, tstb1, new ToolStripSeparator(),  l2, tstb2, new ToolStripSeparator(),  l3, tstb3, new ToolStripSeparator()
            });
            

            threadSafe.InvokeContainerControlMethodThreadSafe(splitContainer1.Panel1, "Add", ts);

        }

        public void ClearWaypoints()
        {

        }
    }
}
