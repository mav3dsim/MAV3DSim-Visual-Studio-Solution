using MAV3DSim.Utils;
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
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace MAV3DSim.Docks
{
    public delegate void ReturnFromPOIEventHandler(object sender, EventArgs e);
    public partial class MissionControl : DockContent
    {
        Mav3DSim parent;
        ThreadSafe threadSafe;
        public event ReturnFromPOIEventHandler ReturnFromPOI;
        
        public MissionControl()
        {
            InitializeComponent();
            threadSafe = new ThreadSafe();
        }

        private void PathGeneration_Shown(object sender, EventArgs e)
        {
            parent = (Mav3DSim)this.ParentForm;
        }
        public bool GenerateInitialPath
        {
            get { return (bool)threadSafe.GetControlPropertyThreadSafe(chkGenerateInitialPath,"Checked"); }
            set
            {
                threadSafe.SetControlPropertyThreadSafe(chkGenerateInitialPath, "Checked", value);
            }
        }

        private void btnAddNewPOI_Click(object sender, EventArgs e)
        {
            ((Map)parent.GetDocks[typeof(Map).ToString()]).NewPointOfInterest();
        }

        private void MissionControl_Shown(object sender, EventArgs e)
        {
            parent = (Mav3DSim)this.ParentForm;
        }

        private void btnRemovePOI_Click(object sender, EventArgs e)
        {
            ((Map)parent.GetDocks[typeof(Map).ToString()]).RemovePointOfInterest(Convert.ToInt32(txtRemovePOI.Text));
        }

        private void btnReturnFromPOI_Click(object sender, EventArgs e)
        {
            chkIgnorePOI.Checked = true;
            if (ReturnFromPOI != null)
                ReturnFromPOI(sender, e);
        }

        private void btnLandingRoute_Click(object sender, EventArgs e)
        {
            ((Map)parent.GetDocks[typeof(Map).ToString()]).NewLandingTrack();
        }

        private void btnSavePoints_Click(object sender, EventArgs e)
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
                ((Log)parent.GetLog).SaveLog(saveFileDialog.FileName);
            }
            
        }

        private void btnSaveMissionPoints_Click(object sender, EventArgs e)
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
                for (int i = 0; i < ((MAV3DSim.Navigation.Navigator4D)parent.GetNavigator4D).PointListObjectives.Count; i++)
                {
                    writer.WriteStartElement("POINT");
                    writer.WriteAttributeString("LAT", ((MAV3DSim.Navigation.Navigator4D)parent.GetNavigator4D).PointListObjectives[i].Lat.ToString());
                    writer.WriteAttributeString("LON", ((MAV3DSim.Navigation.Navigator4D)parent.GetNavigator4D).PointListObjectives[i].Lng.ToString());
                    writer.WriteAttributeString("ALT", ((MAV3DSim.Navigation.Navigator4D)parent.GetNavigator4D).PointListObjectives[i].Alt.ToString());
                    writer.WriteAttributeString("CC", ((MAV3DSim.Navigation.Navigator4D)parent.GetNavigator4D).CCurvature[i].ToString());
                    writer.WriteAttributeString("PSI", ((MAV3DSim.Navigation.Navigator4D)parent.GetNavigator4D).Psi_F[i].ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Close();
            }
        }

        private void chkIgnorePOI_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
