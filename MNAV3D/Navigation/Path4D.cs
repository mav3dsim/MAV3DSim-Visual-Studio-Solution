using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAV3DSim.Navigation
{
    public class Path4D
    {
        private List<PointLatLngAlt> pointsList = new List<PointLatLngAlt>();
        private List<PointLatLng> _pointsList = new List<PointLatLng>();
        private List<double> theta_f = new List<double>();
        private List<double> psi_f = new List<double>();
        private List<double> Cc = new List<double>();
        private List<double> velocity = new List<double>();

        public Path4D()
        {
            pointsList = new List<PointLatLngAlt>();
            _pointsList = new List<PointLatLng>();
            theta_f = new List<double>();
            psi_f = new List<double>();
            Cc = new List<double>();
            velocity = new List<double>();
        }
        public Path4D(List<PointLatLngAlt> PointsList, List<double> Psi_f, List<double> Theta_f, List<double> Curvature, List<double> Velocity)
        {
            pointsList = PointsList;
            updatePointList();
            theta_f = new List<double>();
            psi_f = new List<double>();
            Cc = new List<double>();
            velocity = new List<double>();
        }
        public void AddPath(List<PointLatLngAlt> Points, List<double> Velocity, List<double> Curvature, List<double> Psi_f, List<double> Theta_f)
        {
            pointsList.AddRange(Points);
            for(int i=0;i<Points.Count;i++)
                _pointsList.Add(Points[i].GetPointLatLng());
            velocity.AddRange(Velocity);
            Cc.AddRange(Curvature);
            psi_f.AddRange(Psi_f);
            theta_f.AddRange(Theta_f);

            //initialPosition = pointsList[pointsList.Count - 1];
        }

        public void AddPoint(PointLatLngAlt Point, double Velocity, double Curvature, double Psi_f, double Theta_f)
        {
            pointsList.Add(Point);
            _pointsList.Add(Point.GetPointLatLng());
            velocity.Add(Velocity);
            Cc.Add(Curvature);
            psi_f.Add(Psi_f);
            theta_f.Add(Theta_f);

            //initialPosition = pointsList[pointsList.Count - 1];
        }

        public void Reverse()
        {
            pointsList.Reverse();
            _pointsList.Reverse();
            velocity.Reverse();
            Cc.Reverse();
            psi_f.Reverse();
            theta_f.Reverse();
            for (int i = 0; i < psi_f.Count; i++)
            {
                if (psi_f[i] <= 0)
                    psi_f[i] += Math.PI;
                else
                    psi_f[i] -= Math.PI;
            }

            for (int i = 0; i < theta_f.Count; i++)
            {
                theta_f[i] = -theta_f[i];
            }

        }
        public void InsertRange(int Index,Path4D Path)
        {
            InsertRange(Index,Path.PointList, Path.Velocity, Path.Curvature, Path.Psi_f, Path.Theta_f);
        }
        public void InsertRange(int Index, List<PointLatLngAlt> Points, List<double> Velocity, List<double> Curvature, List<double> Psi_f, List<double> Theta_f)
        {
            pointsList.InsertRange(Index, Points);
            //_pointsList = new List<PointLatLng>();
            theta_f.InsertRange(Index, Theta_f);
            psi_f.InsertRange(Index,Psi_f);
            Cc.InsertRange(Index,Curvature);
            velocity.InsertRange(Index,Velocity);
        }

        public List<PointLatLngAlt> PointList
        {
            get { return pointsList; }
            set { pointsList = value; updatePointList(); }
        }
        public List<PointLatLng> _PointList
        {
            get { return _pointsList; }
            //set { pointsList = value; updatePointList(); }
        }

        public List<double> Theta_f
        {
            get { return theta_f; }
            set { theta_f = value; }
        }
        public List<double> Psi_f
        {
            get { return psi_f; }
            set { psi_f = value; }
        }
        public double LastPsi_f
        {
            get { return psi_f[psi_f.Count - 1]; }
        }
        public List<double> Curvature
        {
            get { return Cc; }
            set { Cc = value; }
        }
        public List<double> Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        public PointLatLngAlt LastPoint
        {
            get { return pointsList[pointsList.Count - 1]; }
        }
        public int Count
        {
            get { return pointsList.Count; }
        }

        private void updatePointList()
        {
            _pointsList = new List<PointLatLng>();
            for(int i=0; i<pointsList.Count;i++)
                _pointsList.Add(pointsList[i].GetPointLatLng());
        }

        
    }
}
