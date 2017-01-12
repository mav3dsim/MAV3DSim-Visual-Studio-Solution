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
    public partial class RawData : DockContent
    {
        ThreadSafe threadSafe;
        public RawData()
        {
            InitializeComponent();
            threadSafe = new ThreadSafe();
        }

        public double P
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtP,"Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtP, "Text", value.ToString()); }
        }

        public double Q
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtQ,"Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtQ, "Text", value.ToString()); }
        }

        public double R
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtR,"Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtR, "Text", value.ToString()); }
        }

        public double Ax
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtAx,"Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtAx, "Text", value.ToString()); }
        }

        public double Ay
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtAy,"Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtAy, "Text", value.ToString()); }
        }

        public double Az
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtAz,"Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtAz, "Text", value.ToString()); }
        }

        public double Mx
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtMx,"Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtMx, "Text", value.ToString()); }
        }

        public double My
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtMy,"Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtMy, "Text", value.ToString()); }
        }

        public double Mz
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtMz,"Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtMz, "Text", value.ToString()); }
        }

        public double Phi
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtPhi,"Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtPhi, "Text", value.ToString()); }
        }

        public double Theta
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtTheta,"Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtTheta, "Text", value.ToString()); }
        }

        public double Psi
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtPsi,"Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtPsi, "Text", value.ToString()); }
        }

        public double U
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtU,"Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtU, "Text", value.ToString()); }
        }

        public double V
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtV,"Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtV, "Text", value.ToString()); }
        }

        public double W
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtW, "Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtW, "Text", value.ToString()); }
        }

        public double Lidar
        {
            get { return Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtLidar, "Text")); }
            set { threadSafe.SetControlPropertyThreadSafe(txtLidar, "Text", value.ToString()); }
        }

    }
}
