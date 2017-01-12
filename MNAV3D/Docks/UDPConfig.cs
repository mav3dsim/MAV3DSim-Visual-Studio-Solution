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
    // A delegate type for START button.
    public delegate void UDPConnectEventHandler(object sender, EventArgs e);

    public partial class UDPConfig : DockContent
    {
        // An event that clients can use to be notified
        public event UDPConnectEventHandler UDPConnect;
        ThreadSafe threadSafe = new ThreadSafe();

        public UDPConfig()
        {
            InitializeComponent();
            cbProtocol.Items.AddRange(new string[] { "MNAV" , "MAVlink"});
            cbProtocol.SelectedItem = "MNAV";
            txtUDPIPAddress.Text = "127.0.0.1";
            txtUDPPort.Text = "9002";
        }

        public string IpAddress
        {
            get { return txtUDPIPAddress.Text; }
            set { txtUDPIPAddress.Text = value; }
        }

        public int Port
        {
            get { return Convert.ToInt32(txtUDPPort.Text); }
            set { txtUDPPort.Text = value.ToString(); }
        }

        public string Protocol
        {
            get { return threadSafe.GetControlPropertyThreadSafe(cbProtocol,"SelectedItem").ToString(); }
            set { threadSafe.SetControlPropertyThreadSafe(cbProtocol, "SelectedItem", value); }

        }

        private void btnUDPConnect_Click(object sender, EventArgs e)
        {
            OnUDPConnect(EventArgs.Empty);
        }

        protected virtual void OnUDPConnect(EventArgs e)
        {
            if (UDPConnect != null)
                UDPConnect(this, e);
        }
    }
}
