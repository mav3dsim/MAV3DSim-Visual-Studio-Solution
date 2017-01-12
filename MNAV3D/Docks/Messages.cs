using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MAV3DSim.Docks
{
    public partial class Messages : DockContent
    {
        ThreadSafe threadSafe;
        public Messages()
        {
            InitializeComponent();
            threadSafe = new ThreadSafe();
        }

        private void tsmClear_Click(object sender, EventArgs e)
        {
            txtMessages.Text = "";
        }

        public void AddMessage(string message)
        {
            threadSafe.InvokeControlMethodThreadSafe(txtMessages, "AppendText", message + Environment.NewLine);
            
        }

        private void txtMessages_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                contextMenuStrip1.Show(e.Location);
        }
    }
}
