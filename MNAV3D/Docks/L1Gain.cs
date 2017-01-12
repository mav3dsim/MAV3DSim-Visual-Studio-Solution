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
    public delegate void UpdateL1GainsEventHandler(object sender, L1GainsEventArgs e);
    public partial class L1Gain : DockContent
    {
        public event Docks.UpdateL1GainsEventHandler UpdateButton;
        private double l1=0;
        private double turnRadius=0;
        public L1Gain()
        {
            InitializeComponent();
            txtL1.Text = "0";
        }

        private void txtL1_TextChanged(object sender, EventArgs e)
        {
            tbL1.Value = Convert.ToInt32(Convert.ToDouble(txtL1.Text));
        }

        private void txtL1Min_TextChanged(object sender, EventArgs e)
        {
            tbL1.Minimum = Convert.ToInt32(Convert.ToDouble(txtL1Min.Text));
        }

        private void txtL1Max_TextChanged(object sender, EventArgs e)
        {
            tbL1.Maximum = Convert.ToInt32(Convert.ToDouble(txtL1Max.Text));
        }

        private void tbL1_ValueChanged(object sender, EventArgs e)
        {
            txtL1.Text = (Convert.ToDouble(tbL1.Value)).ToString();
        }

        private void btnL1Update_Click(object sender, EventArgs e)
        {
            l1 = tbL1.Value;
            OnUpdateButton(e);
        }

        private void txtTurnRadius_TextChanged(object sender, EventArgs e)
        {
            turnRadius = Convert.ToDouble(txtTurnRadius.Text);
        }

        protected virtual void OnUpdateButton(EventArgs e)
        {
            if (UpdateButton != null)
                UpdateButton(this, new L1GainsEventArgs(Convert.ToDouble(txtL1.Text), Convert.ToDouble(txtTurnRadius.Text)));
        }

        public string sL1
        {
            get { return txtL1.Text; }
            set { txtL1.Text = value; }
        }
        public string sTurnRadius
        {
            get { return txtTurnRadius.Text; }
            set { txtTurnRadius.Text = value; }
        }

        public double L1
        { 
            get { return l1; }
        }
        public double TurnRadius
        { 
            get { return turnRadius; }
        }
    }

    public class L1GainsEventArgs : EventArgs
    {
        private double _L1;
        private double _TurnRadius;
        public L1GainsEventArgs(double L1, double TurnRadius)
        {
            this._L1 = L1;
            this._TurnRadius = TurnRadius;
            
        } // eo ctor

        public double L1 { get { return this._L1; } }
        public double TurnRadius { get { return this._TurnRadius; } }
    }
}
