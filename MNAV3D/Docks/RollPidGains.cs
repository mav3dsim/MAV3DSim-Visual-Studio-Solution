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
    //public delegate void UpdateEventHandler(object sender, PidGainsEventArgs e);
    public partial class RollPidGains : DockContent
    {
        public event Docks.UpdatePidEventHandler UpdateButton;
        public RollPidGains()
        {
            InitializeComponent();
            txtMaxRollKd.Text = "2";
            txtMaxRollKi.Text = "2";
            txtMaxRollKp.Text = "2";
            txtMinRollKd.Text = "0";
            txtMinRollKi.Text = "0";
            txtMinRollKp.Text = "0";
            txtRollKd.Text = "0";
            txtRollKi.Text = "0";
            txtRollKp.Text = "0";

        }

        public double Kp
        {
            get { return Convert.ToDouble(txtRollKp.Text); }
            set { txtRollKp.Text = value.ToString(); }
        }

        public double Ki
        {
            get { return Convert.ToDouble(txtRollKi.Text); }
            set { txtRollKi.Text = value.ToString(); }
        }

        public double Kd
        {
            get { return Convert.ToDouble(txtRollKd.Text); }
            set { txtRollKd.Text = value.ToString(); }
        }

        private void tbRollKp_ValueChanged(object sender, EventArgs e)
        {
            txtRollKp.Text = (Convert.ToDouble(tbRollKp.Value) / 1000).ToString();
        }

        private void tbRollKi_ValueChanged(object sender, EventArgs e)
        {
            txtRollKi.Text = (Convert.ToDouble(tbRollKi.Value) / 1000).ToString();
        }

        private void tbRollKd_ValueChanged(object sender, EventArgs e)
        {
            txtRollKd.Text = (Convert.ToDouble(tbRollKd.Value) / 1000).ToString();
        }

        private void txtRollKp_TextChanged(object sender, EventArgs e)
        {
            tbRollKp.Value = Convert.ToInt32(Convert.ToDouble(txtRollKp.Text) * 1000);
        }

        private void txtRollKi_TextChanged(object sender, EventArgs e)
        {
            tbRollKi.Value = Convert.ToInt32(Convert.ToDouble(txtRollKi.Text) * 1000);
        }

        private void txtRollKd_TextChanged(object sender, EventArgs e)
        {
            tbRollKd.Value = Convert.ToInt32(Convert.ToDouble(txtRollKd.Text) * 1000);
        }

        private void txtMinRollKp_TextChanged(object sender, EventArgs e)
        {
            tbRollKp.Minimum = Convert.ToInt32(Convert.ToDouble(txtMinRollKp.Text) * 1000);
        }

        private void txtMaxRollKp_TextChanged(object sender, EventArgs e)
        {
            tbRollKp.Maximum = Convert.ToInt32(Convert.ToDouble(txtMaxRollKp.Text) * 1000);
        }

        private void txtMinRollKi_TextChanged(object sender, EventArgs e)
        {
            tbRollKi.Minimum = Convert.ToInt32(Convert.ToDouble(txtMinRollKi.Text) * 1000);
        }

        private void txtMaxRollKi_TextChanged(object sender, EventArgs e)
        {
            tbRollKi.Maximum = Convert.ToInt32(Convert.ToDouble(txtMaxRollKi.Text) * 1000);
        }

        private void txtMinRollKd_TextChanged(object sender, EventArgs e)
        {
            tbRollKd.Minimum = Convert.ToInt32(Convert.ToDouble(txtMinRollKd.Text) * 1000);
        }

        private void txtMaxRollKd_TextChanged(object sender, EventArgs e)
        {
            tbRollKd.Maximum = Convert.ToInt32(Convert.ToDouble(txtMaxRollKd.Text) * 1000);
        }
        protected virtual void OnUpdateButton(EventArgs e)
        {
            if (UpdateButton != null)
                UpdateButton(this, new PidGainsEventArgs(Convert.ToDouble(txtRollKp.Text),Convert.ToDouble(txtRollKi.Text), Convert.ToDouble(txtRollKd.Text)));
        }

        private void btnUpdateRollGains_Click(object sender, EventArgs e)
        {
            OnUpdateButton(EventArgs.Empty);
        }
    }

    public class PidGainsEventArgs : EventArgs
    {
        private double _Kp;
        private double _Ki;
        private double _Kd;
        public PidGainsEventArgs(double Kp, double Ki, double Kd)
        {
            this._Kp = Kp;
            this._Ki = Ki;
            this._Kd = Kd;
        } // eo ctor

        public double Kp { get { return this._Kp; } }
        public double Ki { get { return this._Ki; } }
        public double Kd { get { return this._Kd; } }
    }
}
