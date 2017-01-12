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
    public partial class AltitudePidGains : DockContent
    {
        public event Docks.UpdatePidEventHandler UpdateButton;
        public AltitudePidGains()
        {
            InitializeComponent();
        }
        public double Kp
        {
            get { return Convert.ToDouble(txtAltitudeKp.Text); }
            set { txtAltitudeKp.Text = value.ToString(); }
        }

        public double Ki
        {
            get { return Convert.ToDouble(txtAltitudeKi.Text); }
            set { txtAltitudeKi.Text = value.ToString(); }
        }

        public double Kd
        {
            get { return Convert.ToDouble(txtAltitudeKd.Text); }
            set { txtAltitudeKd.Text = value.ToString(); }
        }

        private void txtAltitudKp_TextChanged(object sender, EventArgs e)
        {
            tbAltitudeKp.Value = Convert.ToInt32(Convert.ToDouble(txtAltitudeKp.Text) * 1000);
        }

        private void txtMinAltitudKp_TextChanged(object sender, EventArgs e)
        {
            tbAltitudeKp.Minimum = Convert.ToInt32(Convert.ToDouble(txtMinAltitudeKp.Text) * 1000);
        }

        private void txtMaxAltitudKp_TextChanged(object sender, EventArgs e)
        {
            tbAltitudeKp.Maximum = Convert.ToInt32(Convert.ToDouble(txtMaxAltitudeKp.Text) * 1000);
        }

        private void tbAltitudKp_ValueChanged(object sender, EventArgs e)
        {
            txtAltitudeKp.Text = (Convert.ToDouble(tbAltitudeKp.Value) / 1000).ToString();
        }

        private void txtAltitudKi_TextChanged(object sender, EventArgs e)
        {
            tbAltitudeKi.Value = Convert.ToInt32(Convert.ToDouble(txtAltitudeKi.Text) * 1000);
        }

        private void txtMinAltitudKi_TextChanged(object sender, EventArgs e)
        {
            tbAltitudeKi.Minimum = Convert.ToInt32(Convert.ToDouble(txtMinAltitudeKi.Text) * 1000);
        }

        private void txtMaxAltitudKi_TextChanged(object sender, EventArgs e)
        {
            tbAltitudeKi.Maximum = Convert.ToInt32(Convert.ToDouble(txtMaxAltitudeKi.Text) * 1000);
        }

        private void tbAltitudKi_ValueChanged(object sender, EventArgs e)
        {
            txtAltitudeKi.Text = (Convert.ToDouble(tbAltitudeKi.Value) / 1000).ToString();
        }

        private void txtAltitudKd_TextChanged(object sender, EventArgs e)
        {
            tbAltitudeKd.Value = Convert.ToInt32(Convert.ToDouble(txtAltitudeKd.Text) * 1000);
        }

        private void txtMinAltitudKd_TextChanged(object sender, EventArgs e)
        {
            tbAltitudeKd.Minimum = Convert.ToInt32(Convert.ToDouble(txtMinAltitudeKd.Text) * 1000);
        }

        private void txtMaxAltitudKd_TextChanged(object sender, EventArgs e)
        {
            tbAltitudeKd.Maximum = Convert.ToInt32(Convert.ToDouble(txtMaxAltitudeKd.Text) * 1000);
        }

        private void tbAltitudKd_ValueChanged(object sender, EventArgs e)
        {
            txtAltitudeKd.Text = (Convert.ToDouble(tbAltitudeKd.Value) / 1000).ToString();
        }

        private void btnUpdateAltitudGains_Click(object sender, EventArgs e)
        {
            OnUpdateButton(EventArgs.Empty);
        }
        
        protected virtual void OnUpdateButton(EventArgs e)
        {
            if (UpdateButton != null)
                UpdateButton(this, new PidGainsEventArgs(Convert.ToDouble(txtAltitudeKp.Text), Convert.ToDouble(txtAltitudeKi.Text), Convert.ToDouble(txtAltitudeKd.Text)));
        }
    }
}
