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
    public delegate void UpdateLyapEventHandler(object sender, LyapGainsEventArgs e);
    public partial class LyapunovGains : DockContent
    {
        Mav3DSim parent;
        public event UpdateLyapEventHandler UpdateButton;
        private double kDelta;
        private double ks;
        private double kw;
        private double kphi;
        private double psi_a;
        public LyapunovGains()
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

        private void txtLyapKs_TextChanged(object sender, EventArgs e)
        {
            ks = Convert.ToDouble(txtLyapKs.Text);
            tbLyapKs.Value = Convert.ToInt32(ks * 1000); 
        }

        private void txtLyapKsMin_TextChanged(object sender, EventArgs e)
        {
            tbLyapKs.Minimum = Convert.ToInt32(Convert.ToDouble(txtLyapKsMin.Text) * 1000);
        }

        private void txtLyapKsMax_TextChanged(object sender, EventArgs e)
        {
            tbLyapKs.Maximum = Convert.ToInt32(Convert.ToDouble(txtLyapKsMax.Text) * 1000);
        }

        private void tbLyapKs_ValueChanged(object sender, EventArgs e)
        {
            txtLyapKs.Text = (Convert.ToDouble(tbLyapKs.Value) / 1000).ToString();
        }

        private void txtLyapKw_TextChanged(object sender, EventArgs e)
        {
            kw = Convert.ToDouble(txtLyapKw.Text);
            tbLyapKw.Value = Convert.ToInt32(kw * 1000); 
        }

        private void txtLyapKwMin_TextChanged(object sender, EventArgs e)
        {
            tbLyapKs.Minimum = Convert.ToInt32(Convert.ToDouble(txtLyapKsMin.Text) * 1000);
        }

        private void txtLyapKwMax_TextChanged(object sender, EventArgs e)
        {
            tbLyapKw.Maximum = Convert.ToInt32(Convert.ToDouble(txtLyapKwMax.Text) * 1000);
        }

        private void tbLyapKw_ValueChanged(object sender, EventArgs e)
        {
            txtLyapKw.Text = (Convert.ToDouble(tbLyapKw.Value) / 1000).ToString();
        }

        private void txtLyapKphi_TextChanged(object sender, EventArgs e)
        {
            kphi = Convert.ToDouble(txtLyapKphi.Text);
            tbLyapKphi.Value = Convert.ToInt32(kphi * 1000); 
        }

        private void txtLyapKphiMin_TextChanged(object sender, EventArgs e)
        {
            
            tbLyapKphi.Minimum = Convert.ToInt32(Convert.ToDouble(txtLyapKphiMin.Text) * 1000);
        }

        private void txtLyapKphiMax_TextChanged(object sender, EventArgs e)
        {
            tbLyapKphi.Maximum = Convert.ToInt32(Convert.ToDouble(txtLyapKphiMax.Text) * 1000);
        }

        private void tbLyapKphi_ValueChanged(object sender, EventArgs e)
        {
            txtLyapKphi.Text = (Convert.ToDouble(tbLyapKphi.Value) / 1000).ToString();
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

        private void btnUpdateLyapGains_Click(object sender, EventArgs e)
        {
            if (UpdateButton != null)
                UpdateButton(this, new LyapGainsEventArgs(kDelta,ks,kw,kphi,psi_a));
        }

        public double Kdelta
        {
            get { return kDelta; }
            set { kDelta = value; txtLyapKd.Text = value.ToString(); }
        }
        public double Kw1
        {
            get { return kw; }
            set { kw = value; txtLyapKw.Text = value.ToString(); }
        }
        public double Ks
        {
            get { return ks; }
            set { ks = value; txtLyapKs.Text = value.ToString(); }
        }
        public double Kphi
        {
            get { return kphi; }
            set { kphi = value; txtLyapKphi.Text = value.ToString(); }
        }
        public double Psi_a
        {
            get { return psi_a; }
            set { psi_a = value; txtLyapPsia.Text = value.ToString(); }
        }

        private void LyapunovGains_Shown(object sender, EventArgs e)
        {
            parent = (Mav3DSim)this.ParentForm;
        }

    }

    public class LyapGainsEventArgs
    {
        private double kdelta;
        private double ks;
        private double kw;
        private double kphi;
        private double psi_a;
        public LyapGainsEventArgs(double KDelta, double Ks, double Kw1, double Kphi, double Psi_a)
        {
            this.kdelta = KDelta;
            this.ks = Ks;
            this.kw = Kw1;
            this.kphi = Kphi;
            this.psi_a = Psi_a;
        } // eo ctor

        public double Kdelta { get { return this.kdelta; } }
        public double Ks { get { return this.ks; } }
        public double Kw1 { get { return this.kw; } }
        public double Kphi { get { return this.kphi; } }
        public double Psi_a { get { return this.psi_a; } }
        
    }
}
