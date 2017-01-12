using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mitov.PlotLab;
using WeifenLuo.WinFormsUI.Docking;

namespace MAV3DSim.Docks
{
    public partial class Plot : DockContent
    {
        Dictionary<string, double> plotVariables = new Dictionary<string,double>();
        int scaleValue;
        int pointCounter;
        private static readonly object lockChannels = new object();
        public Plot()
        {
            InitializeComponent();
            //Change colors
            scope1.XAxis.DataView.Lines.Pen.Color = Color.FromArgb(0,122,204);
            scope1.YAxis.DataView.Lines.Pen.Color = Color.FromArgb(0, 122, 204);
            scope1.DataView.Border.Pen.Color = Color.FromArgb(0, 122, 204);
            scope1.BackColor = Color.FromArgb(45, 45, 48);
            //label1.BackColor = Color.FromArgb(45, 45, 48);
            //label1.ForeColor = Color.FromArgb(45, 45, 48);
            scope1.ToolBar.ButtonColor = Color.FromArgb(45, 45, 48);
            scope1.ToolBar.BorderColor = Color.FromArgb(45, 45, 48);
            scope1.ToolBar.GlyphColor = Color.FromArgb(199, 199, 199);
            scope1.ToolBar.MouseOverButtonColor = Color.FromArgb(62,62,64);
            scope1.ToolBar.MouseOverBorderColor = Color.FromArgb(62, 62, 64);
            scope1.ToolBar.MouseDownBorderColor = Color.FromArgb(0, 122, 204);
            scope1.ToolBar.MouseDownButtonColor = Color.FromArgb(0, 122, 204);
            scope1.ToolBar.DisabledColor = Color.FromArgb(78, 78, 80);


            scope1.XAxis.Button.MouseOverColor = Color.FromArgb(62, 62, 64);
            scope1.XAxis.Button.MouseOverBorderColor = Color.FromArgb(62, 62, 64);
            scope1.XAxis.Button.MouseDownBorderColor = Color.FromArgb(0, 122, 204);
            scope1.XAxis.Button.MouseDownColor = Color.FromArgb(0, 122, 204);
            

            scaleValue = 50;
            pointCounter = 0;
            
        }

        private void Plot_Paint(object sender, PaintEventArgs e)
        {
            //scope1.Refresh();
            //scope1.RefreshView();
            //scope1.Show();
            //scope1.Update();
            //scope1.Invalidate();
            
        }

        public Dictionary<string, double> PlotVariables
        {
            get{ return plotVariables;}
            set { plotVariables = value;  }
        }

        public void PopulateMenu()
        {
            tvPlotVariables.Nodes.Clear();
            //contextMenu.Items.Clear();
            foreach (KeyValuePair<string, double> variable in plotVariables)
            {
                tvPlotVariables.Nodes.Add(variable.Key);
                //contextMenu.Items.Add(menuItem);
                
            }

        }

        private void addChannel()
        {
            lock (lockChannels)
            {

                foreach (TreeNode node in tvPlotVariables.Nodes)
                {
                    if (node.Checked)
                    {
                        bool dontAdd = false;
                        foreach (ScopeChannel scopeChannel in scope1.Channels)
                        {
                            if (node.Text == scopeChannel.Name)
                                dontAdd = true;
                        }
                        if (!dontAdd)
                        {
                            ScopeChannel a = new ScopeChannel();
                            a.Name = node.Text;
                            scope1.Channels.Add(a);
                            return;
                        }

                    }
                    else
                    {
                        foreach (ScopeChannel scopeChannel in scope1.Channels)
                        {
                            if (node.Text == scopeChannel.Name)
                            {
                                scope1.Channels.Remove(scopeChannel);
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void contextMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
                e.Cancel = true;
        }

        public void addPoint()
        {
            lock (lockChannels)
            {
                foreach (ScopeChannel scopeChannel in scope1.Channels)
                {
                    scopeChannel.Data.AddXYPoint(pointCounter, plotVariables[scopeChannel.Name]);

                }
                if (scope1.Channels.Count > 0)
                {

                    if (pointCounter > scaleValue)
                    {
                        scope1.XAxis.Min.Value = pointCounter - scaleValue;
                        scope1.XAxis.Min.DataValue = pointCounter - scaleValue;

                    }
                }
            }

            pointCounter++;
            
        }

        private void tvPlotVariables_Click(object sender, EventArgs e)
        {
            addChannel();
        }

        private void tbScale_ValueChanged(object sender, EventArgs e)
        {
            scaleValue = tbScale.Value;
        }

        public void Clear()
        {
            pointCounter = 0;
            foreach (ScopeChannel scopeChannel in scope1.Channels)
            {
                scopeChannel.Data.Clear();
                

            }
            PopulateMenu();

        }

    
         
    }
}
