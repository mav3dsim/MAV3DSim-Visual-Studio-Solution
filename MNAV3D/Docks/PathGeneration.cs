using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using WeifenLuo.WinFormsUI.Docking;
using System.Reflection;
using System.IO;
using MAV3DSim.Navigation;

namespace MAV3DSim.Docks
{
    public partial class PathGeneration : DockContent
    {
        Navigation.PathGenerator4D pathGenerator = new Navigation.PathGenerator4D();
        Mav3DSim parent;
        Utils.MapTools mapTools = new Utils.MapTools();
        List<PointLatLng> pointListObjectives;
        // Guidance data
        List<double> cCurvature = new List<double>();     // Curvatura de curva
        List<double> arcLength = new List<double>();      // Arc-length
        List<double> psi_f = new List<double>();  // Angulo tangencial con respecto a la normal.
        public PathGeneration()
        {
        
            InitializeComponent();
            
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            pathGenerator.StraightLine(Convert.ToDouble(txtUp.Text));
            ((Map)parent.GetDocks[typeof(Map).ToString()]).AddRoute(MAV3DSim.Docks.Map.MapRoute.Path, pathGenerator.Route, Color.Black);
            ((Navigator4D)parent.GetNavigator4D).setPath(pathGenerator.Path);
            

        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            pathGenerator.Turn(Navigation.PathGenerator4D.Direction.left, Convert.ToDouble(txtLeft.Text)*Math.PI/180);
            ((Map)parent.GetDocks[typeof(Map).ToString()]).AddRoute(MAV3DSim.Docks.Map.MapRoute.Path, pathGenerator.Route, Color.Black);
            ((Navigator4D)parent.GetNavigator4D).setPath(pathGenerator.Path);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            pathGenerator.Turn(Navigation.PathGenerator4D.Direction.right, Convert.ToDouble(txtRight.Text) * Math.PI / 180);
            ((Map)parent.GetDocks[typeof(Map).ToString()]).AddRoute(MAV3DSim.Docks.Map.MapRoute.Path, pathGenerator.Route, Color.Black);
            ((Navigator4D)parent.GetNavigator4D).setPath(pathGenerator.Path);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            PointLatLng position = ((MapInfo)parent.GetDocks[typeof(MapInfo).ToString()]).Position;
            
            pathGenerator.InitialPosition = new PointLatLngAlt(position.Lat, position.Lng, parent.GetImu.alt + Convert.ToDouble(txtAltitudeOffset.Text));
            pathGenerator.InitialYaw = parent.GetImu.psi;
            pathGenerator.InitialPitch = Convert.ToDouble(txtPitchAngle.Text) * Math.PI / 180;
            pathGenerator.CurrentVelocity = 210;
            
            pathGenerator.newPath();
            pathGenerator.TurnRadius = Convert.ToDouble(txtTurnRadius.Text);

            //((Map)parent.GetDocks[typeof(Map).ToString()]).DrawRectangle();
        }

        private void PathGeneration_Shown(object sender, EventArgs e)
        {
            parent = (Mav3DSim)this.ParentForm;
        }

        /*
        public void RectangleDrawn(GMapPolygon Rectangle)
        {
            double d = 10000000;
            PointLatLng startingPoint = new PointLatLng();

            foreach (PointLatLng p in Rectangle.Points)
            {
                double d1 = mapTools.GetDistance(((MapInfo)parent.GetDocks[typeof(MapInfo).ToString()]).Position, p);
                if (d1 < d)
                {
                    d = d1;
                    startingPoint = p;
                }
            }


            double distance = 0;
            double distance1 = mapTools.GetDistance(Rectangle.Points[0], Rectangle.Points[1]);
            double distance2 = mapTools.GetDistance(Rectangle.Points[0], Rectangle.Points[3]);

            bool right = true;
            if (distance1 > distance2)
            {

                distance = distance1;

                if (startingPoint.Equals(Rectangle.Points[0]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, 1, -Convert.ToDouble(txtTurnRadius.Text));
                    pathGenerator.InitialHeading = Math.PI / 2; ;
                    right = true;
                }

                if (startingPoint.Equals(Rectangle.Points[1]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -1, -Convert.ToDouble(txtTurnRadius.Text));
                    pathGenerator.InitialHeading = -Math.PI / 2; ;
                    right = false;
                }

                if (startingPoint.Equals(Rectangle.Points[2]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -1, Convert.ToDouble(txtTurnRadius.Text));
                    pathGenerator.InitialHeading = -Math.PI / 2; ;
                    right = true;
                }

                if (startingPoint.Equals(Rectangle.Points[3]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, 1, Convert.ToDouble(txtTurnRadius.Text));
                    pathGenerator.InitialHeading = Math.PI / 2; ;
                    right = false;
                }
            }
            else
            {
                distance = distance2;
                if (startingPoint.Equals(Rectangle.Points[0]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, Convert.ToDouble(txtTurnRadius.Text), 1); ;
                    pathGenerator.InitialHeading = Math.PI; ;
                    right = false;
                }

                if (startingPoint.Equals(Rectangle.Points[1]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -Convert.ToDouble(txtTurnRadius.Text), 1); ;
                    pathGenerator.InitialHeading = Math.PI;
                    right = true;

                }

                if (startingPoint.Equals(Rectangle.Points[2]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -Convert.ToDouble(txtTurnRadius.Text), -1); ;
                    pathGenerator.InitialHeading = 0;
                    right = false;

                }

                if (startingPoint.Equals(Rectangle.Points[3]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, Convert.ToDouble(txtTurnRadius.Text), -1); ;
                    pathGenerator.InitialHeading = 0;
                    right = true;

                }
            }

            pathGenerator.newPath();
            pathGenerator.TurnRadius = Convert.ToDouble(txtTurnRadius.Text);
            pathGenerator.StraightLine(pathGenerator.TurnRadius);
            while (mapTools.IsInside(pathGenerator.InitialPosition, Rectangle.Points))
            {
                pathGenerator.StraightLine(distance * 1000 - 2 * pathGenerator.TurnRadius);
                pathGenerator.Turn(right ? Controler.PathGenerator.Direction.right : Controler.PathGenerator.Direction.left, Math.PI);
                right = !right;
            }

            ((Map)parent.GetDocks[typeof(Map).ToString()]).AddRoute(1, pathGenerator.Route, Color.Black);
        }
        
        public void PolygonDrawn(GMapPolygon Rectangle)
        {
            double d = 10000000;
            PointLatLng startingPoint = new PointLatLng();

            foreach (PointLatLng p in Rectangle.Points)
            {
                double d1 = mapTools.GetDistance(((MapInfo)parent.GetDocks[typeof(MapInfo).ToString()]).Position, p);
                if (d1 < d)
                {
                    d = d1;
                    startingPoint = p;
                }
            }


            double distance = 0;
            double distance1 = mapTools.GetDistance(Rectangle.Points[0], Rectangle.Points[1]);
            double distance2 = mapTools.GetDistance(Rectangle.Points[0], Rectangle.Points[3]);

            bool right = true;
            if (distance1 > distance2)
            {

                distance = distance1;

                if (startingPoint.Equals(Rectangle.Points[0]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, 1, -Convert.ToDouble(txtTurnRadius.Text));
                    pathGenerator.InitialHeading = Math.PI / 2; ;
                    right = true;
                }

                if (startingPoint.Equals(Rectangle.Points[1]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -1, -Convert.ToDouble(txtTurnRadius.Text));
                    pathGenerator.InitialHeading = -Math.PI / 2; ;
                    right = false;
                }

                if (startingPoint.Equals(Rectangle.Points[2]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -1, Convert.ToDouble(txtTurnRadius.Text));
                    pathGenerator.InitialHeading = -Math.PI / 2; ;
                    right = true;
                }

                if (startingPoint.Equals(Rectangle.Points[3]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, 1, Convert.ToDouble(txtTurnRadius.Text));
                    pathGenerator.InitialHeading = Math.PI / 2; ;
                    right = false;
                }
            }
            else
            {
                distance = distance2;
                if (startingPoint.Equals(Rectangle.Points[0]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, Convert.ToDouble(txtTurnRadius.Text), 1); ;
                    pathGenerator.InitialHeading = Math.PI; ;
                    right = false;
                }

                if (startingPoint.Equals(Rectangle.Points[1]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -Convert.ToDouble(txtTurnRadius.Text), 1); ;
                    pathGenerator.InitialHeading = Math.PI;
                    right = true;

                }

                if (startingPoint.Equals(Rectangle.Points[2]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -Convert.ToDouble(txtTurnRadius.Text), -1); ;
                    pathGenerator.InitialHeading = 0;
                    right = false;

                }

                if (startingPoint.Equals(Rectangle.Points[3]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, Convert.ToDouble(txtTurnRadius.Text), -1); ;
                    pathGenerator.InitialHeading = 0;
                    right = true;

                }
            }

            pathGenerator.newPath();
            pathGenerator.TurnRadius = Convert.ToDouble(txtTurnRadius.Text);
            pathGenerator.StraightLine(pathGenerator.TurnRadius);
            while (mapTools.IsInside(pathGenerator.InitialPosition, Rectangle.Points))
            {
                pathGenerator.StraightLine(distance * 1000 - 2 * pathGenerator.TurnRadius);
                pathGenerator.Turn(right ? Controler.PathGenerator.Direction.right : Controler.PathGenerator.Direction.left, Math.PI);
                right = !right;
            }

            ((Map)parent.GetDocks[typeof(Map).ToString()]).AddRoute(1, pathGenerator.Route, Color.Black);
        }
        */

        private void btnPolygon_Click(object sender, EventArgs e)
        {
            PointLatLng position = ((MapInfo)parent.GetDocks[typeof(MapInfo).ToString()]).Position;

            pathGenerator.InitialPosition = new PointLatLngAlt(position.Lat, position.Lng, parent.GetImu.alt);
            pathGenerator.InitialYaw = parent.GetImu.psi;
            pathGenerator.newPath();
            pathGenerator.TurnRadius = Convert.ToDouble(txtTurnRadius.Text);

            ((Map)parent.GetDocks[typeof(Map).ToString()]).DrawPolygon();
        }

        private void btnSaveData_Click(object sender, EventArgs e)
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
                pathGenerator.SaveLog(saveFileDialog.FileName);
            }
        }

    }
}
