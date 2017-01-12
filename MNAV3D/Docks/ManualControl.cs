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
    public partial class ManualControl : DockContent
    {
        ThreadSafe threadSafe;
        public ManualControl()
        {
            InitializeComponent();
            threadSafe = new ThreadSafe();
        }

        private void vsAileron_Scroll(object sender, ScrollEventArgs e)
        {
            int value = vsAileron.Value;
            double dValue = (((double)value / 100));
            txtAileron.Text = dValue.ToString();
            //actuators.Aileron = dValue;

        }
        private void vsAileron_ValueChanged(object sender, EventArgs e)
        {
            int value = vsAileron.Value;
            double dValue = (((double)value / 100));
            txtAileron.Text = dValue.ToString();
            //actuators.Aileron = dValue;
        }

        private void vsRudder_Scroll(object sender, ScrollEventArgs e)
        {
            int value = vsRudder.Value;
            double dValue = (((double)value / 100));
            txtRudder.Text = dValue.ToString();
            //actuators.Rudder = dValue;
        }

        private void vsRudder_ValueChanged(object sender, EventArgs e)
        {
            int value = vsRudder.Value;
            double dValue = (((double)value / 100));
            txtRudder.Text = dValue.ToString();
            //actuators.Rudder = dValue;
        }

        private void vsElevator_Scroll(object sender, ScrollEventArgs e)
        {
            int value = vsElevator.Value;
            double dValue = ((-(double)value / 100));
            txtElevator.Text = dValue.ToString();
            //actuators.Elevator = dValue;

        }
        private void vsElevator_ValueChanged(object sender, EventArgs e)
        {
            int value = vsElevator.Value;
            double dValue = ((-(double)value / 100));
            txtElevator.Text = dValue.ToString();
            //actuators.Elevator = dValue;
        }

        private void vsThrottle_Scroll(object sender, ScrollEventArgs e)
        {
            int value = vsThrottle.Value;
            double dValue = ((-(double)value / 100));
            txtThrottle.Text = dValue.ToString();
            txtThrottle1.Text = (dValue * 350).ToString();
            //actuators.Throttle = dValue;
        }
        private void vsThrottle_ValueChanged(object sender, EventArgs e)
        {
            int value = vsThrottle.Value;
            double dValue = ((-(double)value / 100));
            txtThrottle.Text = dValue.ToString();
            txtThrottle1.Text = (dValue * 350).ToString();
            //actuators.Throttle = dValue;
        }

        private void txtElevator_TextChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(Convert.ToDouble(txtElevator.Text) * 100);
            vsElevator.Value = -value;
        }

        private void txtThrottle_TextChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(Convert.ToDouble(txtThrottle.Text) * 100);
            vsThrottle.Value = -value;
        }

        private void txtRudder_TextChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(Convert.ToDouble(txtRudder.Text) * 100);
            vsRudder.Value = value;
        }

        private void txtAileron_TextChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(Convert.ToDouble(txtAileron.Text) * 100);
            vsAileron.Value = value;
        }

        public int Elevator
        {
            get { return Convert.ToInt32(threadSafe.GetControlPropertyThreadSafe(vsElevator, "Value")); }
            set { threadSafe.SetControlPropertyThreadSafe(vsElevator, "Value", value); }
        }

        public int Throttle
        {
            get { return Convert.ToInt32(threadSafe.GetControlPropertyThreadSafe(vsThrottle, "Value")); }
            set { threadSafe.SetControlPropertyThreadSafe(vsThrottle, "Value", value); }
        }

        public int Rudder
        {
            get { return Convert.ToInt32(threadSafe.GetControlPropertyThreadSafe(vsRudder, "Value")); }
            set { threadSafe.SetControlPropertyThreadSafe(vsRudder, "Value", value); }
        }

        public int Aileron
        {
            get { return Convert.ToInt32(threadSafe.GetControlPropertyThreadSafe(vsAileron, "Value")); }
            set { threadSafe.SetControlPropertyThreadSafe(vsAileron, "Value", value); }
        }

        public string sElevator
        {
            get { return threadSafe.GetControlPropertyThreadSafe(txtElevator, "Text").ToString(); }
            set { threadSafe.SetControlPropertyThreadSafe(txtElevator, "Text", value.ToString()); }
        }

        public string sElevator1
        {
            get { return threadSafe.GetControlPropertyThreadSafe(txtElevator1, "Text").ToString(); }
            set { threadSafe.SetControlPropertyThreadSafe(txtElevator1, "Text", value.ToString()); }
        }
        public bool Elevator1Visible
        {
            get { return Convert.ToBoolean(threadSafe.GetControlPropertyThreadSafe(txtElevator1, "Visible")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtElevator1, "Visible", value); }
        }

        public string sThrottle
        {
            get { return threadSafe.GetControlPropertyThreadSafe(txtThrottle, "Text").ToString(); }
            set { threadSafe.SetControlPropertyThreadSafe(txtThrottle, "Text", value.ToString()); }
        }

        public string sThrottle1
        {
            get { return threadSafe.GetControlPropertyThreadSafe(txtThrottle1, "Text").ToString(); }
            set { threadSafe.SetControlPropertyThreadSafe(txtThrottle1, "Text", value.ToString()); }
        }
        public bool Throttle1Visible
        {
            get { return Convert.ToBoolean(threadSafe.GetControlPropertyThreadSafe(txtThrottle1, "Visible")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtThrottle1, "Visible", value); }
        }

        public string sAileron
        {
            get { return threadSafe.GetControlPropertyThreadSafe(txtAileron, "Text").ToString(); }
            set { threadSafe.SetControlPropertyThreadSafe(txtAileron, "Text", value.ToString()); }
        }

        public string sRudder
        {
            get { return threadSafe.GetControlPropertyThreadSafe(txtRudder, "Text").ToString(); }
            set { threadSafe.SetControlPropertyThreadSafe(txtRudder, "Text", value.ToString()); }
        }


    }
}
