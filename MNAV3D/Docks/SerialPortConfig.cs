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

    public enum SerialEvents
    {
        Event1
    }

    // A delegate type for Serial button.
    public delegate void SerialConnectEventHandler(object sender, EventArgs e);
    public delegate void SerialDisconnectEventHandler(object sender, EventArgs e);
    public partial class SerialPortConfig : DockContent
    {

        // An event that clients can use to be notified
        public event SerialConnectEventHandler SerialConnect;
        public event SerialDisconnectEventHandler SerialDisconnect;

        public SerialPortConfig()
        {
            InitializeComponent();

            // Enumerate Serial Ports
            cbPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            System.IO.Ports.SerialPort a = new System.IO.Ports.SerialPort();

            cbBaudRate.Items.AddRange(new string[] { "110", "300", "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200", "230400", "460800", "921600" });
            cbBaudRate.SelectedItem = "57600";
            cbDataBits.Items.AddRange(new string[] { "5", "6", "7", "8" });
            cbDataBits.SelectedItem = "8";
            cbParity.Items.AddRange(new string[] { "Odd", "Even", "None", "Mark", "Space" });
            cbParity.SelectedItem = "None";
            cbStopBits.Items.AddRange(new string[] { "1", "1.5", "2" });
            cbStopBits.SelectedItem = "1";
            cbFlowControl.Items.AddRange(new string[] { "Xon/Xoff", "Hardware", "None" });
            cbFlowControl.SelectedItem = "None";
            cbProtocol.Items.AddRange(new string[] { "MNAV", "MAVLink" });
            cbProtocol.SelectedItem = "MAVLink";

        }

        public String Port
        {
            get { return (string)cbPort.SelectedItem; }
        }

        public String BaudRate
        {
            get { return (string)cbBaudRate.SelectedItem; }
        }

        public String DataBits
        {
            get { return (string)cbDataBits.SelectedItem; }
        }

        public String Parity
        {
            get { return (string)cbParity.SelectedItem; }
        }

        public String StopBits
        {
            get { return (string)cbStopBits.SelectedItem; }
        }
        public String Protocol
        {
            get { return (string)cbProtocol.SelectedItem; }
        }

        protected virtual void OnSerialDisconnect(EventArgs e)
        {
            if (SerialDisconnect != null)
                SerialDisconnect(this, e);
            
        }
        protected virtual void OnSerialConnect(EventArgs e)
        {
            if (SerialConnect != null)
                SerialConnect(this, e);
        }

        private void btnOpenSerialPort_Click(object sender, EventArgs e)
        {
            OnSerialConnect(EventArgs.Empty);
        }

        private void btnClosePort_Click(object sender, EventArgs e)
        {
            OnSerialDisconnect(EventArgs.Empty);
        }

    }
}
