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
    public delegate void UpdatePidEventHandler(object sender, PidGainsEventArgs e);
    public delegate void UpdatePidFFEventHandler(object sender, PidFFGainsEventArgs e);
    public partial class PitchPidGains : DockContent
    {

        public event Docks.UpdatePidEventHandler UpdateButton;
        public PitchPidGains()
        {
            InitializeComponent();
            txtMaxPitchKd.Text = "2";
            txtMaxPitchKi.Text = "2";
            txtMaxPitchKp.Text = "2";
            txtMinPitchKd.Text = "0";
            txtMinPitchKi.Text = "0";
            txtMinPitchKp.Text = "0";
            txtPitchKd.Text = "0";
            txtPitchKi.Text = "0";
            txtPitchKp.Text = "0";
        }

        public double Kp
        {
            get { return Convert.ToDouble(txtPitchKp.Text); }
            set { txtPitchKp.Text = value.ToString(); }
        }

        public double Ki
        {
            get { return Convert.ToDouble(txtPitchKi.Text); }
            set { txtPitchKi.Text = value.ToString(); }
        }

        public double Kd
        {
            get { return Convert.ToDouble(txtPitchKd.Text); }
            set { txtPitchKd.Text = value.ToString(); }
        }


        private void tbPitchKp_ValueChanged(object sender, EventArgs e)
        {
            txtPitchKp.Text = (Convert.ToDouble(tbPitchKp.Value) / 1000).ToString();
        }

        private void tbPitchKi_ValueChanged(object sender, EventArgs e)
        {
            txtPitchKi.Text = (Convert.ToDouble(tbPitchKi.Value) / 1000).ToString();
        }

        private void tbPitchKd_ValueChanged(object sender, EventArgs e)
        {
            txtPitchKd.Text = (Convert.ToDouble(tbPitchKd.Value) / 1000).ToString();
        }

        private void txtPitchKp_TextChanged(object sender, EventArgs e)
        {
            tbPitchKp.Value = Convert.ToInt32(Convert.ToDouble(txtPitchKp.Text) * 1000);
        }

        private void txtPitchKi_TextChanged(object sender, EventArgs e)
        {
            tbPitchKi.Value = Convert.ToInt32(Convert.ToDouble(txtPitchKi.Text) * 1000);
        }

        private void txtPitchKd_TextChanged(object sender, EventArgs e)
        {
            tbPitchKd.Value = Convert.ToInt32(Convert.ToDouble(txtPitchKd.Text) * 1000);
        }

        private void txtMinPitchKp_TextChanged(object sender, EventArgs e)
        {
            tbPitchKp.Minimum = Convert.ToInt32(Convert.ToDouble(txtMinPitchKp.Text) * 1000);
        }

        private void txtMaxPitchKp_TextChanged(object sender, EventArgs e)
        {
            tbPitchKp.Maximum = Convert.ToInt32(Convert.ToDouble(txtMaxPitchKp.Text) * 1000);
        }

        private void txtMinPitchKi_TextChanged(object sender, EventArgs e)
        {
            tbPitchKi.Minimum = Convert.ToInt32(Convert.ToDouble(txtMinPitchKi.Text) * 1000);
        }

        private void txtMaxPitchKi_TextChanged(object sender, EventArgs e)
        {
            tbPitchKi.Maximum = Convert.ToInt32(Convert.ToDouble(txtMaxPitchKi.Text) * 1000);
        }

        private void txtMinPitchKd_TextChanged(object sender, EventArgs e)
        {
            tbPitchKd.Minimum = Convert.ToInt32(Convert.ToDouble(txtMinPitchKd.Text) * 1000);
        }

        private void txtMaxPitchKd_TextChanged(object sender, EventArgs e)
        {
            tbPitchKd.Maximum = Convert.ToInt32(Convert.ToDouble(txtMaxPitchKd.Text) * 1000);
        }
        protected virtual void OnUpdateButton(EventArgs e)
        {
            if (UpdateButton != null)
                UpdateButton(this, new PidGainsEventArgs(Convert.ToDouble(txtPitchKp.Text), Convert.ToDouble(txtPitchKi.Text), Convert.ToDouble(txtPitchKd.Text)));
        }

        private void btnUpdatePitchGains_Click(object sender, EventArgs e)
        {
            OnUpdateButton(EventArgs.Empty);
        }

    }
}
