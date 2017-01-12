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
    public partial class Lyapunov3DGains : DockContent
    {
        Mav3DSim parent;
        private double kDelta;
        private double kx;
        private double ky;
        private double kz;
        private double psi_a;
        public Lyapunov3DGains()
        {
            InitializeComponent();
        }

        private void txtLyapKd_TextChanged(object sender, EventArgs e)
        {
            kDelta = Convert.ToDouble(txtLyapKd.Text);
            tbLyapKd.Value = Convert.ToInt32( kDelta * 1000);
            
        }

        private void txtLyapKdMin_TextChanged(object sender, EventArgs e)
        {
            tbLyapKd.Minimum = Convert.ToInt32(Convert.ToDouble(txtLyapKdMin.Text) * 1000);
        }

        private void txtLyapKdMax_TextChanged(object sender, EventArgs e)
        {
            tbLyapKd.Maximum = Convert.ToInt32(Convert.ToDouble(txtLyapKdMax.Text) * 1000);
        }

        private void tbLyapKd_ValueChanged(object sender, EventArgs e)
        {
            txtLyapKd.Text = (Convert.ToDouble(tbLyapKd.Value) / 1000).ToString();
        }

        private void txtLyapKx_TextChanged(object sender, EventArgs e)
        {
            kx = Convert.ToDouble(txtLyapKx.Text);
            tbLyapKx.Value = Convert.ToInt32(Convert.ToDouble(txtLyapKx.Text) * 1000);
        }

        private void txtLyapKxMin_TextChanged(object sender, EventArgs e)
        {
            tbLyapKx.Minimum = Convert.ToInt32(Convert.ToDouble(txtLyapKxMin.Text) * 1000);
        }

        private void txtLyapKxMax_TextChanged(object sender, EventArgs e)
        {
            tbLyapKx.Maximum = Convert.ToInt32(Convert.ToDouble(txtLyapKxMax.Text) * 1000);
        }

        private void tbLyapKx_ValueChanged(object sender, EventArgs e)
        {
            txtLyapKx.Text = (Convert.ToDouble(tbLyapKx.Value) / 1000).ToString();
        }

        private void txtLyapKy_TextChanged(object sender, EventArgs e)
        {
            ky = Convert.ToDouble(txtLyapKy.Text);
            tbLyapKy.Value = Convert.ToInt32( ky * 1000);
        }

        private void txtLyapKyMin_TextChanged(object sender, EventArgs e)
        {
            tbLyapKx.Minimum = Convert.ToInt32(Convert.ToDouble(txtLyapKxMin.Text) * 1000);
        }

        private void txtLyapKyMax_TextChanged(object sender, EventArgs e)
        {
            tbLyapKy.Maximum = Convert.ToInt32(Convert.ToDouble(txtLyapKyMax.Text) * 1000);
        }

        private void tbLyapKy_ValueChanged(object sender, EventArgs e)
        {
            txtLyapKy.Text = (Convert.ToDouble(tbLyapKy.Value) / 1000).ToString();
        }

        private void txtLyapKz_TextChanged(object sender, EventArgs e)
        {
            kz = Convert.ToDouble(txtLyapKz.Text);
            tbLyapKz.Value = Convert.ToInt32( kz * 1000);
        }

        private void txtLyapKzMin_TextChanged(object sender, EventArgs e)
        {
            tbLyapKz.Minimum = Convert.ToInt32(Convert.ToDouble(txtLyapKzMin.Text) * 1000);
        }

        private void txtLyapKzMax_TextChanged(object sender, EventArgs e)
        {
            tbLyapKz.Maximum = Convert.ToInt32(Convert.ToDouble(txtLyapKzMax.Text) * 1000);
        }

        private void tbLyapKz_ValueChanged(object sender, EventArgs e)
        {
            txtLyapKz.Text = (Convert.ToDouble(tbLyapKz.Value) / 1000).ToString();
        }

        private void txtLyapPsia_TextChanged(object sender, EventArgs e)
        {
            psi_a = Convert.ToDouble(txtLyapPsia.Text);
            tbLyapPsia.Value = Convert.ToInt32( psi_a * 1000);
        }

        private void txtLyapPsiaMin_TextChanged(object sender, EventArgs e)
        {
            tbLyapPsia.Minimum = Convert.ToInt32(Convert.ToDouble(txtLyapPsiaMin.Text) * 1000);
        }

        private void txtLyapPsiaMax_TextChanged(object sender, EventArgs e)
        {
            tbLyapPsia.Maximum = Convert.ToInt32(Convert.ToDouble(txtLyapPsiaMax.Text) * 1000);
        }

        private void tbLyapPsia_ValueChanged(object sender, EventArgs e)
        {
            txtLyapPsia.Text = (Convert.ToDouble(tbLyapPsia.Value) / 1000).ToString();
        }

        private void btnUpdateLyap3DGains_Click(object sender, EventArgs e)
        {
            parent.GetControllerLyap3D.UpdateGains(kx, ky, kz, kDelta, psi_a);
        }

        public double Kdelta
        {
            get { return kDelta; }
            set { kDelta = value; txtLyapKd.Text = value.ToString(); }
        }
        public double Ky
        {
            get { return ky; }
            set { ky = value; txtLyapKy.Text = value.ToString(); }
        }
        public double Kx
        {
            get { return kx; }
            set { kx = value; txtLyapKx.Text = value.ToString(); }
        }
        public double Kz
        {
            get { return kz; }
            set { kz = value; txtLyapKz.Text = value.ToString(); }
        }
        public double Psi_a
        {
            get { return psi_a; }
            set { psi_a = value; txtLyapPsia.Text = value.ToString(); }
        }

        private void Lyapunov3DGains_Shown(object sender, EventArgs e)
        {
            parent = (Mav3DSim)this.ParentForm;
        }

    }
}
