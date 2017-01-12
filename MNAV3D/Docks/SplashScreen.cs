using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MAV3DSim.Docks
{
    public partial class SplashScreen : Form
    {
        // The queue that stores AssemblyLoad event info
        public static Queue<string> AsmLoads = new Queue<string>();

        public SplashScreen()
        {
            InitializeComponent();
            timer1.Start();
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            while (AsmLoads.Count > 0)
            {
                //textBox1.Text += AsmLoads.Dequeue() + "\r\n";
            }
            //textBox1.ScrollToCaret();
        }



    }
}
