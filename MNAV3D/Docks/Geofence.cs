using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MAV3DSim.Docks
{
    public partial class Geofence : DockContent
    {
        Mav3DSim parent;
        ThreadSafe threadSafe;
        List<PointLatLngAlt> geofencePoints;
        public Geofence()
        {
            InitializeComponent();
            geofencePoints = new List<PointLatLngAlt>();
            threadSafe = new ThreadSafe();
        }
        public void NewGeofencepoint(PointLatLngAlt geofencepoint, int count)
        {
            ToolStrip ts = new ToolStrip();
            ToolStripLabel l0 = new ToolStripLabel();
            l0.Text = "WP " + count.ToString();
            ToolStripLabel l1 = new ToolStripLabel();
            l1.Text = "Lat:";
            ToolStripTextBox tstb1 = new ToolStripTextBox();
            tstb1.Text = geofencepoint.Lat.ToString();
            ToolStripLabel l2 = new ToolStripLabel();
            l2.Text = "Lon:";
            ToolStripTextBox tstb2 = new ToolStripTextBox();
            tstb2.Text = geofencepoint.Lng.ToString();
            ToolStripLabel l3 = new ToolStripLabel();
            l3.Text = "Alt:";
            ToolStripTextBox tstb3 = new ToolStripTextBox();
            tstb3.Text = geofencepoint.Alt.ToString();



            ts.BackColor = Color.FromArgb(45, 45, 48);
            ts.ForeColor = Color.White;


            ts.Items.AddRange(new ToolStripItem[]{
                l0, new ToolStripSeparator(), l1, tstb1, new ToolStripSeparator(),  l2, tstb2, new ToolStripSeparator(),  l3, tstb3, new ToolStripSeparator()
            });


            threadSafe.InvokeContainerControlMethodThreadSafe(splitContainer1.Panel1, "Add", ts);
            geofencePoints.Add(geofencepoint);
            
        }



        public void NewGeofencePoints(GMapPolygon points)
        {
            for (int i = 0; i < points.Points.Count; i++)
                NewGeofencepoint(new PointLatLngAlt(points.Points[i].Lat, points.Points[i].Lng,0), i + 1);
        }

        public void ClearGeofence()
        {
            threadSafe.InvokeContainerControlMethodThreadSafe(splitContainer1.Panel1, "Clear",null);
            geofencePoints.Clear();

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ((Map)parent.GetDocks[typeof(Map).ToString()]).NewGeofence();
        }

        private void Geofence_Shown(object sender, EventArgs e)
        {
            parent = (Mav3DSim)this.ParentForm;
        }

        private void saveGeofencePoints(String toFile)
        {
            using (StreamWriter streamWriter = new StreamWriter(@toFile))
            {
                streamWriter.WriteLine("0 900");
                streamWriter.Flush();
                for (int i = 0; i < geofencePoints.Count; i++)
                {

                    streamWriter.WriteLine(geofencePoints[i].Lat.ToString() + " " + geofencePoints[i].Lng.ToString());
                    streamWriter.Flush(); //force line to be written to disk
                }

                streamWriter.Close();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Default to the directory which contains our content files.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation);
            string contentPath = Path.GetFullPath(relativePath);

            saveFileDialog.InitialDirectory = contentPath;

            saveFileDialog.Title = "Save Log File";



            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                saveGeofencePoints(saveFileDialog.FileName);
            }

            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearGeofence();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            // Default to the directory which contains our content files.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../../../../Content");
            string contentPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = contentPath;

            fileDialog.Title = "Load workspace";

            fileDialog.Filter = "XML Files (*.txt)" +
                                "All Files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {

                var numbers = File.ReadAllLines(fileDialog.FileName).Select(l => l.Split(' ')).ToArray();
                if(int.Parse(numbers[0][0])==0 && int.Parse(numbers[0][1])==900)
                {
                    ClearGeofence();
                    ((Map)parent.GetDocks[typeof(Map).ToString()]).ClearGeofence();
                    for (int i = 1; i < numbers.Length - 1; i++)
                    {
                        NewGeofencepoint(new PointLatLngAlt(Double.Parse(numbers[i][0]), Double.Parse(numbers[i][1]), 0), i - 1);
                        ((Map)parent.GetDocks[typeof(Map).ToString()]).NewGeofence(new PointLatLngAlt(Double.Parse(numbers[i][0]), Double.Parse(numbers[i][1]), 0));
                    }
                        
                }
                
            }
        }

    }
}
