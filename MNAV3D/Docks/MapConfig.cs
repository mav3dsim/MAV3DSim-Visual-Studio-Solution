using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Xml;
using System.Reflection;
using System.IO;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Collections;
using GMap.NET.WindowsForms.Markers;

namespace MAV3DSim.Docks
{
    public delegate void LoadEventHandler(object sender, EventArgs e);
    public delegate void RefreshEventHandler(object sender, EventArgs e);
    public partial class MapConfig : DockContent
    {
        Mav3DSim parent;
        public event LoadEventHandler LoadButton;
        public event RefreshEventHandler RefreshButton;
        bool newPoints = false;
        List<PointLatLng> pointListObjectives;
        //GMapRoute route;
        //GMapOverlay overlay;
        double gpsLat = 0;
        double gpsLon = 0;
        double gpsAlt = 0;

        // Guidance data
        List<double> cCurvature = new List<double>();     // Curvatura de curva
        List<double> arcLength = new List<double>();      // Arc-length
        List<double> psi_f = new List<double>();  // Angulo tangencial con respecto a la normal.
        List<double> x_enu = new List<double>();
        List<double> y_enu = new List<double>();

        public MapConfig()
        {
            InitializeComponent();
        }

        private void btnNewPointsSet_Click(object sender, EventArgs e)
        {
            if (!newPoints)
            {
                btnNewPointsSet.Text = "Stop";
                this.dataGridView1.Rows.Clear();
                pointListObjectives = new List<PointLatLng>();
                /*route = new GMapRoute(pointListObjectives, "My Objectives");
                overlay.Routes[1] = route;
                GMapMarker marker = overlay.Markers[0];
                overlay.Markers.Clear();
                overlay.Markers.Add(marker);*/

            }
            else
            {
                btnNewPointsSet.Text = "New Points";
            }

            newPoints = !newPoints;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            // Default to the directory which contains our content files.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../../../../Config");
            string contentPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = contentPath;

            fileDialog.Title = "Save Points";

            fileDialog.Filter = "XML Files (*.xml) |*.xml|" +
                                "All Files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlTextWriter writer = new XmlTextWriter(fileDialog.FileName, Encoding.UTF8);

                writer.WriteStartDocument();
                writer.WriteStartElement("CONFIG");
                writer.WriteStartElement("POINTS");
                for (int i = 0; i < pointListObjectives.Count; i++)
                {
                    writer.WriteStartElement("POINT");
                    writer.WriteAttributeString("LAT", pointListObjectives[i].Lat.ToString());
                    writer.WriteAttributeString("LON", pointListObjectives[i].Lng.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Close();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            // Default to the directory which contains our content files.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../../../../Config");
            string contentPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = contentPath;

            fileDialog.Title = "Save Points";

            fileDialog.Filter = "XML Files (*.xml) |*.xml|" +
                                "All Files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlTextReader reader = new XmlTextReader(fileDialog.FileName);
                this.dataGridView1.Rows.Clear();
                cCurvature.Clear();
                arcLength.Clear();
                psi_f.Clear();
                x_enu.Clear();
                y_enu.Clear();


                pointListObjectives = new List<PointLatLng>();
                /*GMapMarker marker = overlay.Markers[0];
                overlay.Markers.Clear();
                overlay.Markers.Add(marker);*/
                //Boolean placeMarker = true;
                while (reader.Read())
                {
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element: // El nodo es un elemento. 
                                if (reader.Name == "POINT")
                                {

                                    reader.GetAttribute("LAT");

                                    PointLatLng point = new PointLatLng(Convert.ToDouble(reader.GetAttribute("LAT")), Convert.ToDouble(reader.GetAttribute("LON")));

                                    pointListObjectives.Add(point);
                                    /*route = new GMapRoute(pointListObjectives, "My Objectives");
                                    overlay.Routes[1] = route;
                                    if (placeMarker)
                                    {
                                        GMapMarkerLabel marker1 = new GMapMarkerLabel(point, Properties.Resources.Google_Maps_Marker, pointListObjectives.Count.ToString());
                                        overlay.Markers.Add(marker1);
                                    }*/

                                    dataGridView1.Rows.Add(pointListObjectives.Count.ToString(), point.Lat.ToString(), point.Lng.ToString(), false);

                                    // Datos agregados para el guidance
                                    cCurvature.Add(Convert.ToDouble(reader.GetAttribute("Cc")));
                                    arcLength.Add(Convert.ToDouble(reader.GetAttribute("s")));
                                    psi_f.Add(Convert.ToDouble(reader.GetAttribute("Psi_f")));
                                    x_enu.Add(Convert.ToDouble(reader.GetAttribute("x")));
                                    y_enu.Add(Convert.ToDouble(reader.GetAttribute("y")));



                                }
                                /*if (reader.Name == "POINTS")
                                {
                                    reader.GetAttribute("MARKER");
                                    if (Int32.Parse(reader.GetAttribute("MARKER")) == 0)
                                        placeMarker = false;
                                }*/
                                break;
                        }
                    }
                }
            }
            fileDialog.Dispose();

            OnLoadButton(new EventArgs());
        }

        public List<PointLatLng> PointListObjectives
        {
            get { return pointListObjectives; }
        }

        public List<double>CCurvature
        {
            get { return cCurvature; }
        }

        public List<double> ArcLength
        {
            get { return arcLength; }
        }

        public List<double> Psi_F
        {
            get { return psi_f; }
        }

        public List<double> X_enu
        {
            get { return x_enu; }
        }

        public List<double> Y_enu
        {
            get { return x_enu; }
        }

        public double InitLat
        {
            get { return Convert.ToDouble(txtInitLat.Text); }
            set { txtInitLat.Text = value.ToString(); }
        }

        public double InitLon
        {
            get { return Convert.ToDouble(txtInitLon.Text); }
            set { txtInitLon.Text = value.ToString(); }
        }
        public double InitAlt
        {
            get { return Convert.ToDouble(txtInitAlt.Text); }
            set { txtInitAlt.Text = value.ToString(); }
        }

        public bool DiffLatLon
        {
            get { return cbGPSDifferential.Checked; }
            set { cbGPSDifferential.Checked = value; }
        }

        public double GPSLat
        {
            get { return gpsLat; }
        }
        public double GPSLon
        {
            get { return gpsLon; }
        }
        public double GPSAlt
        {
            get { return gpsAlt; }
        }

        private void cbGPSDifferential_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGPSDifferential.Checked)
            {
                gpsLat = Convert.ToDouble(txtInitLat.Text);
                gpsLon = Convert.ToDouble(txtInitLon.Text);
                gpsAlt = Convert.ToDouble(txtInitAlt.Text);
                ((Utils.IMU)parent.GetImu).LatInit = gpsLat;
                ((Utils.IMU)parent.GetImu).LonInit = gpsLon;
                ((Utils.IMU)parent.GetImu).AltInit = gpsAlt;

            }
            else
            {
                gpsLat = 0;
                gpsLon = 0;
                gpsAlt = 0;
                ((Utils.IMU)parent.GetImu).LatInit = gpsLat;
                ((Utils.IMU)parent.GetImu).LonInit = gpsLon;
                ((Utils.IMU)parent.GetImu).AltInit = gpsAlt;

            }
        }

        public bool UseGpsDifferential
        {
            get { return cbGPSDifferential.Checked; }
        }
        protected virtual void OnLoadButton(EventArgs e)
        {
            if (LoadButton != null)
                LoadButton(this, e);
        }

        private void MapConfig_Shown(object sender, EventArgs e)
        {
            parent = (Mav3DSim)this.ParentForm;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            OnRefreshButton(e);
        }
        protected virtual void OnRefreshButton(EventArgs e)
        {
            if (RefreshButton != null)
                RefreshButton(this, e);
        }

        private void txtInitAlt_TextChanged(object sender, EventArgs e)
        {
            //gpsAlt = Convert.ToDouble(txtInitAlt.Text);
            //((Utils.IMU)parent.GetImu).AltInit = gpsAlt;
        }

        private void txtInitLat_TextChanged(object sender, EventArgs e)
        {
            //gpsLat = Convert.ToDouble(txtInitLat.Text);
            //((Utils.IMU)parent.GetImu).LatInit = gpsLat;
        }

        private void txtInitLon_TextChanged(object sender, EventArgs e)
        {
            
            //gpsLon = Convert.ToDouble(txtInitLon.Text);
            //((Utils.IMU)parent.GetImu).LonInit = gpsLon;
            
        }








    }
}
