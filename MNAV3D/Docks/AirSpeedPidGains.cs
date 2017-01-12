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
    public partial class AirSpeedPidGains : DockContent
    {
        public event Docks.UpdatePidFFEventHandler UpdateButton;
        public AirSpeedPidGains()
        {
            InitializeComponent();
            txtSpeedFF.Text = "0";
            txtMinSpeedFF.Text = "0";
            txtMaxSpeedFF.Text = "1";
        }

        private void txtSpeedKp_TextChanged(object sender, EventArgs e)
        {
            tbSpeedKp.Value = Convert.ToInt32(Convert.ToDouble(txtSpeedKp.Text) * 1000);
        }

        private void txtMinSpeedKp_TextChanged(object sender, EventArgs e)
        {
            tbSpeedKp.Minimum = Convert.ToInt32(Convert.ToDouble(txtMinSpeedKp.Text) * 1000);
        }

        private void txtMaxSpeedKp_TextChanged(object sender, EventArgs e)
        {
            tbSpeedKp.Maximum = Convert.ToInt32(Convert.ToDouble(txtMaxSpeedKp.Text) * 1000);
        }

        private void tbSpeedKp_ValueChanged(object sender, EventArgs e)
        {
            txtSpeedKp.Text = (Convert.ToDouble(tbSpeedKp.Value) / 1000).ToString();
        }

        private void txtSpeedKi_TextChanged(object sender, EventArgs e)
        {
            tbSpeedKi.Value = Convert.ToInt32(Convert.ToDouble(txtSpeedKi.Text) * 1000);
        }

        private void txtMinSpeedKi_TextChanged(object sender, EventArgs e)
        {
            tbSpeedKi.Minimum = Convert.ToInt32(Convert.ToDouble(txtMinSpeedKi.Text) * 1000);
        }

        private void txtMaxSpeedKi_TextChanged(object sender, EventArgs e)
        {
            tbSpeedKi.Maximum = Convert.ToInt32(Convert.ToDouble(txtMaxSpeedKi.Text) * 1000);
        }

        private void tbSpeedKi_ValueChanged(object sender, EventArgs e)
        {
            txtSpeedKi.Text = (Convert.ToDouble(tbSpeedKi.Value) / 1000).ToString();
        }

        private void txtSpeedKd_TextChanged(object sender, EventArgs e)
        {
            tbSpeedKd.Value = Convert.ToInt32(Convert.ToDouble(txtSpeedKd.Text) * 1000);
        }

        private void txtMinSpeedKd_TextChanged(object sender, EventArgs e)
        {
            tbSpeedKd.Minimum = Convert.ToInt32(Convert.ToDouble(txtMinSpeedKd.Text) * 1000);
        }

        private void txtMaxSpeedKd_TextChanged(object sender, EventArgs e)
        {
            tbSpeedKd.Maximum = Convert.ToInt32(Convert.ToDouble(txtMaxSpeedKd.Text) * 1000);
        }

        private void tbSpeedKd_ValueChanged(object sender, EventArgs e)
        {
            txtSpeedKd.Text = (Convert.ToDouble(tbSpeedKd.Value) / 1000).ToString();
        }

        private void btnUpdateSpeedGains_Click(object sender, EventArgs e)
        {
            OnUpdateButton(EventArgs.Empty);
        }

        protected virtual void OnUpdateButton(EventArgs e)
        {
            if (UpdateButton != null)
                UpdateButton(this, new PidFFGainsEventArgs(Convert.ToDouble(txtSpeedKp.Text), Convert.ToDouble(txtSpeedKi.Text), Convert.ToDouble(txtSpeedKd.Text), Convert.ToDouble(txtSpeedFF.Text)));
        }

        public double Kp
        {
            get { return Convert.ToDouble(tbSpeedKp.Value) / 1000; }
            set { tbSpeedKp.Value = Convert.ToInt32(value * 1000); }
        }

        public double Ki
        {
            get { return Convert.ToDouble(tbSpeedKi.Value) / 1000; }
            set { tbSpeedKi.Value = Convert.ToInt32(value * 1000); txtSpeedKi.Text = value.ToString(); }
        }

        public double Kd
        {
            get { return Convert.ToDouble(tbSpeedKd.Value) / 1000; }
            set { tbSpeedKd.Value = Convert.ToInt32(value * 1000); }
        }

        public double FF
        {
            get { return Convert.ToDouble(tbSpeedFF.Value) / 1000; }
            set { tbSpeedFF.Value = Convert.ToInt32(value * 1000); }
        }

        private void txtSpeedFF_TextChanged(object sender, EventArgs e)
        {
            tbSpeedFF.Value = Convert.ToInt32(Convert.ToDouble(txtSpeedFF.Text) * 1000);
        }

        private void txtMinSpeedFF_Load(object sender, EventArgs e)
        {
            tbSpeedFF.Minimum = Convert.ToInt32(Convert.ToDouble(txtMinSpeedFF.Text) * 1000);
        }

        private void txtMaxSpeedFF_Load(object sender, EventArgs e)
        {
            tbSpeedFF.Maximum = Convert.ToInt32(Convert.ToDouble(txtMaxSpeedFF.Text) * 1000);
        }

        private void tbSpeedFF_ValueChanged(object sender, EventArgs e)
        {
            txtSpeedFF.Text = (Convert.ToDouble(tbSpeedFF.Value) / 1000).ToString();
        }

    }

    public class PidFFGainsEventArgs : EventArgs
    {
        private double _Kp;
        private double _Ki;
        private double _Kd;
        private double _FF;
        public PidFFGainsEventArgs(double Kp, double Ki, double Kd, double FF)
        {
            this._Kp = Kp;
            this._Ki = Ki;
            this._Kd = Kd;
            this._FF = FF;
        } // eo ctor

        public double Kp { get { return this._Kp; } }
        public double Ki { get { return this._Ki; } }
        public double Kd { get { return this._Kd; } }
        public double FF { get { return this._FF; } }
    }
}
