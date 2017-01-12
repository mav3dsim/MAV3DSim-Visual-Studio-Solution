using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Collections;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using MAV3DSim.Utils;

namespace MAV3DSim.Navigation
{
    class PathGenerator4D
    {
        Log log = new Log();

        Path4D path;
        
        private PointLatLngAlt initialPosition;
        private PointLatLngAlt currentPosition;
        private double turnRadius;
        private double initialYaw;
        private double initialPitch;
        private double currentHeading;
        private double currentVelocity;
        private GMapRoute route;
        private double deltaS = .1;
        Utils.MapTools mapTools = new Utils.MapTools();
        //private List<PointLatLngAlt> pointsList = new List<PointLatLngAlt>();
        //private List<PointLatLng> _pointsList = new List<PointLatLng>();
        //private List<double> x_enu = new List<double>();
        //private List<double> y_enu = new List<double>();
        //private List<double> theta_f = new List<double>();
        //private List<double> psi_f = new List<double>();
        //private List<double> Cc = new List<double>();
        //private List<double> arcLength = new List<double>();
        //private List<double> velocity = new List<double>();
        private PointLatLngAlt initialEnuPosition = new PointLatLngAlt();
        
        

        public enum Direction {right,left};
        public enum DubinsPath { LSL, LSR, RSL, RSR, None};
        public PathGenerator4D()
        {
            path = new Path4D();
        }

        public void newPath()
        {
            path = new Path4D();

            //pointsList = new List<PointLatLngAlt>();
            //_pointsList = new List<PointLatLng>();
            //pointsList.Add(initialPosition);
            //x_enu = new List<double>();
            //x_enu.Add(0);
            //y_enu = new List<double>();
            //y_enu.Add(0);
            //psi_f = new List<double>();
            //theta_f = new List<double>();
            //psi_f.Add(0);
            //Cc = new List<double>();
            //Cc.Add(0);
            //arcLength = new List<double>();

            log.ResetLog();

            //velocity = new List<double>();


        }

        public void AddPath(List<PointLatLngAlt> Points, List<double> Velocity, List<double> Curvature, List<double> Psi_f, List<double> Theta_f)
        {
            /*
            pointsList.AddRange(Points);
            for(int i=0;i<Points.Count;i++)
                _pointsList.Add(Points[i].GetPointLatLng());
            velocity.AddRange(Velocity);
            Cc.AddRange(Curvature);
            psi_f.AddRange(Psi_f);
            theta_f.AddRange(Theta_f);
             * */
            path.AddPath(Points, Velocity, Curvature, Psi_f, Theta_f);
            initialPosition = path.PointList[path.PointList.Count - 1];

            UpdateRoute();

        }


        public void StraightLine(double meters)
        {
            
            double x = 0;
            double y = 0;
            double z = 0;
            double deltaDistance = 1;
            double totalDistance = 0;
            y = deltaDistance * Math.Cos(initialYaw);  // Lat
            x = deltaDistance * Math.Sin(initialYaw);  // Lon
            z = deltaDistance * Math.Sin(initialPitch);
            //pointsList.Add(initialPosition);
            if (meters == 0)
                return;
            while (totalDistance < meters)
            {
                totalDistance += Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

                PointLatLngAlt addPoint;

                if (path.Count == 0)
                {
                    addPoint = initialPosition;
                    //pointsList.Add(initialPosition);
                    //_pointsList.Add(pointsList[pointsList.Count - 1].GetPointLatLng());
                    //velocity.Add(currentVelocity);
                    
                   
                    
                }
                else
                {
                    addPoint = mapTools.OffsetInMeters(path.LastPoint, x, y, z);
                    //pointsList.Add(mapTools.OffsetInMeters(pointsList[pointsList.Count - 1], x, y, z));
                    //_pointsList.Add(pointsList[pointsList.Count - 1].GetPointLatLng());
                    //velocity.Add(currentVelocity);
                }

                
                    

                // Data necesary for lyapunov tracking

                Point3D p = mapTools.Geodetic2ENU(addPoint, initialEnuPosition);

                //x_enu.Add(p.X);
                //y_enu.Add(p.Y);

                double x_dot = x;
                double y_dot = y;

               /* p = mapTools.Geodetic2ENU(new PointLatLng(y_dot, x_dot), new PointLatLng( 0, 0));

                double x_dot_enu = p.X;
                double y_dot_enu = p.Y;

                double x_ddot = 0;
                double y_ddot = 0;

                p = mapTools.Geodetic2ENU(new PointLatLng(y_ddot, x_ddot), new PointLatLng(0, 0));

                double x_ddot_enu = p.X;
                double y_ddot_enu = p.Y;
                 * */

                double psi_f;
                psi_f=Math.PI/2-Math.Atan2(y_dot , x_dot);
                if (psi_f > Math.PI)
                    psi_f -= 2 * Math.PI;
                if (psi_f < -Math.PI)
                    psi_f += 2 * Math.PI;

                //theta_f.Add(initialPitch);
                
                //Cc.Add(0);
                
                //arcLength.Add(route.Distance * 1000);

                // Constant curvature Cc=0
                path.AddPoint(addPoint, currentVelocity, 0, psi_f, initialPitch);
                log.Append(path.LastPoint.ToStringCSV());


            }
            //double d = route.Distance;
            initialPosition = path.LastPoint;
            UpdateRoute();
            
        }

        public void StraightLine(double Meters, double InitialVelocity, double FinalVelocity)
        {

            double x = 0;
            double y = 0;
            double z = 0;
            double deltaDistance = 1;
            double totalDistance = 0;
            y = deltaDistance * Math.Cos(initialYaw);  // Lat
            x = deltaDistance * Math.Sin(initialYaw);  // Lon
            z = deltaDistance * Math.Sin(initialPitch);
            //pointsList.Add(initialPosition);
            if (Meters == 0)
                return;
            PointLatLngAlt addPoint;
            double addVelocity;
            while (totalDistance < Meters)
            {
                totalDistance += Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

                if (path.Count == 0)
                {
                    addPoint = initialPosition;
                    addVelocity = InitialVelocity;
                    //pointsList.Add(initialPosition);
                    //velocity.Add(InitialVelocity);
                    //_pointsList.Add(pointsList[pointsList.Count - 1].GetPointLatLng());
                }
                else
                {
                    addPoint = mapTools.OffsetInMeters(path.LastPoint, x, y, z);
                    addVelocity = InitialVelocity + (totalDistance / Meters) * (FinalVelocity - InitialVelocity);
                    //pointsList.Add(mapTools.OffsetInMeters(pointsList[pointsList.Count - 1], x, y, z));
                    //velocity.Add(InitialVelocity + (totalDistance / Meters) * (FinalVelocity - InitialVelocity));
                    //_pointsList.Add(pointsList[pointsList.Count - 1].GetPointLatLng());
                }
                

                


                // Data necesary for lyapunov tracking

                Point3D p = mapTools.Geodetic2ENU(addPoint, initialEnuPosition);

                //x_enu.Add(p.X);
                //y_enu.Add(p.Y);

                double x_dot = x;
                double y_dot = y;

                /*p = mapTools.Geodetic2ENU(new PointLatLng(y_dot, x_dot), new PointLatLng(0, 0));

                double x_dot_enu = p.X;
                double y_dot_enu = p.Y;

                double x_ddot = 0;
                double y_ddot = 0;

                p = mapTools.Geodetic2ENU(new PointLatLng(y_ddot, x_ddot), new PointLatLng(0, 0));

                double x_ddot_enu = p.X;
                double y_ddot_enu = p.Y;
                 * 
                 * */

                double psi_f = Math.PI / 2 - Math.Atan2(y_dot, x_dot);
                //psi_f.Add(Math.PI / 2 - Math.Atan2(y_dot, x_dot));
                if (psi_f > Math.PI)
                    psi_f -= 2 * Math.PI;
                if (psi_f< -Math.PI)
                    psi_f+= 2 * Math.PI;
                //theta_f.Add(initialPitch);

                //Cc.Add(0);
                UpdateRoute();
                //arcLength.Add(route.Distance * 1000);
                path.AddPoint(addPoint, addVelocity, 0, psi_f, initialPitch);
                log.Append(path.LastPoint.ToStringCSV());
            }
            //double d = route.Distance;
            currentVelocity = FinalVelocity;
            initialPosition = path.LastPoint;
            UpdateRoute();

        }

        public void Turn(Direction direction, double angle)
        {
            PointLatLngAlt addPoint;
            deltaS = 10 / (2 * turnRadius);
            if(direction == Direction.right)
            {
                
                // Calculate offsets
                double offsetX = -Math.Sin(initialYaw) * turnRadius;
                double offsetY = Math.Cos(initialYaw) * turnRadius;
                double offsetZ = 0;

                PointLatLngAlt center = mapTools.OffsetInMeters(initialPosition, offsetY, offsetX, offsetZ);
                double s1 = 0;
                double s = initialYaw+3*Math.PI/2;
                
                while (s < angle + initialYaw + 3*Math.PI / 2)
                {
                    s += deltaS;
                    s1 += deltaS;

                    addPoint = mapTools.OffsetInMeters(center, turnRadius * Math.Sin(s), turnRadius * Math.Cos(s), 2 * turnRadius * s1 * Math.Sin(initialPitch));
                    //pointsList.Add(mapTools.OffsetInMeters(center, turnRadius * Math.Sin(s), turnRadius * Math.Cos(s), 2*turnRadius*s1 * Math.Sin(initialPitch)));
                    //_pointsList.Add(pointsList[pointsList.Count - 1].GetPointLatLng());
                    //velocity.Add(currentVelocity);
                    
                    // Data necesary for lyapunov tracking

                    Point3D p = mapTools.Geodetic2ENU(addPoint, initialEnuPosition);

                    //x_enu.Add(p.X);
                    //y_enu.Add(p.Y);

                    double x_dot = turnRadius * Math.Cos(s);
                    double y_dot = -turnRadius * Math.Sin(s);

                    double psi_f;
                    psi_f=Math.PI / 2 - Math.Atan2(y_dot , x_dot);
                    if (psi_f > Math.PI)
                        psi_f -= 2 * Math.PI;
                    if (psi_f < -Math.PI)
                        psi_f += 2 * Math.PI;
                    //theta_f.Add(initialPitch);

                    //Cc.Add(1/turnRadius);

                    //UpdateRoute();
                    //arcLength.Add(route.Distance * 1000);

                    path.AddPoint(addPoint, currentVelocity, 1 / turnRadius, psi_f, initialPitch);
                    log.Append(path.LastPoint.ToStringCSV());
                }
                initialPosition = path.LastPoint;
                initialYaw = initialYaw + angle;
                if (initialYaw > Math.PI)
                    initialYaw -= 2 * Math.PI;
            }
            else
            {
                
                // Calculate offsets
                double offsetX = Math.Sin(initialYaw) * turnRadius;
                double offsetY = -Math.Cos(initialYaw) * turnRadius;
                double offsetZ = 0;

                PointLatLngAlt center = mapTools.OffsetInMeters(initialPosition, offsetY, offsetX,offsetZ);
                double s1 = 0;
                double s = -initialYaw;
                while (s < angle -initialYaw )
                {
                    s += deltaS;
                    s1 += deltaS;

                    addPoint = mapTools.OffsetInMeters(center, turnRadius * Math.Cos(s), turnRadius * Math.Sin(s), 2 * turnRadius * s1 * Math.Sin(initialPitch));
                    //pointsList.Add(mapTools.OffsetInMeters(center, turnRadius * Math.Cos(s), turnRadius * Math.Sin(s), 2 * turnRadius * s1 * Math.Sin(initialPitch)));
                    //_pointsList.Add(pointsList[pointsList.Count - 1].GetPointLatLng());
                    //velocity.Add(currentVelocity);
                    
                    // Data necesary for lyapunov tracking

                    Point3D p = mapTools.Geodetic2ENU(addPoint, initialEnuPosition);

                    //x_enu.Add(p.X);
                    //y_enu.Add(p.Y);

                    double x_dot = -turnRadius * Math.Sin(s);
                    double y_dot = turnRadius * Math.Cos(s);

                    double psi_f = Math.PI / 2 - Math.Atan2(y_dot, x_dot);
                    //psi_f.Add(Math.PI / 2 - Math.Atan2(y_dot, x_dot));
                    if (psi_f > Math.PI)
                        psi_f -= 2 * Math.PI;
                    if (psi_f < -Math.PI)
                        psi_f += 2 * Math.PI;
                    //theta_f.Add(initialPitch);

                    //Cc.Add(1 / turnRadius);

                    //UpdateRoute();
                    //arcLength.Add(route.Distance * 1000);

                    path.AddPoint(addPoint, currentVelocity, 1 / turnRadius, psi_f, initialPitch);
                    log.Append(path.LastPoint.ToStringCSV());
                }

                initialPosition = path.LastPoint;
                initialYaw = initialYaw - angle;
                if (initialYaw < -Math.PI)
                    initialYaw += 2 * Math.PI;
                

            }
            UpdateRoute();
        }

        public void TurnUntil(Direction direction, double angle)
        {
            PointLatLngAlt addPoint;
            deltaS = 10 / (2 * turnRadius);
            if (direction == Direction.right)
            {

                // Calculate offsets
                double offsetX = Math.Sin(initialYaw) * turnRadius;
                double offsetY = Math.Cos(initialYaw) * turnRadius;
                double offsetZ = 0;

                PointLatLngAlt center = mapTools.OffsetInMeters(initialPosition, offsetY, -offsetX, offsetZ);
                double s1 = 0;
                double s = initialYaw + 3 * Math.PI / 2;
                if (!(s < angle + 3 * Math.PI / 2) && Math.Abs(s - angle + 3 * Math.PI / 2) > 0.1745 ) // y mayor que 10 grados
                    angle += 2 * Math.PI;
                while (s < angle + 3 * Math.PI / 2)
                {
                    s += deltaS;
                    s1 += deltaS;
                    addPoint = mapTools.OffsetInMeters(center, turnRadius * Math.Sin(s), turnRadius * Math.Cos(s), 2 * turnRadius * s1 * Math.Sin(initialPitch));
                    //pointsList.Add(mapTools.OffsetInMeters(center, turnRadius * Math.Sin(s), turnRadius * Math.Cos(s), 2 * turnRadius * s1 * Math.Sin(initialPitch)));
                    //_pointsList.Add(pointsList[pointsList.Count - 1].GetPointLatLng());
                    //velocity.Add(currentVelocity);
                    
                    // Data necesary for lyapunov tracking

                    Point3D p = mapTools.Geodetic2ENU(addPoint, initialEnuPosition);

                    //x_enu.Add(p.X);
                    //y_enu.Add(p.Y);

                    double x_dot = turnRadius * Math.Cos(s);
                    double y_dot = -turnRadius * Math.Sin(s);

                    double psi_f = Math.PI / 2 - Math.Atan2(y_dot, x_dot);
                    //psi_f.Add(Math.PI / 2 - Math.Atan2(y_dot, x_dot));
                    if (psi_f > Math.PI)
                        psi_f -= 2 * Math.PI;
                    if (psi_f < -Math.PI)
                        psi_f += 2 * Math.PI;
                    //theta_f.Add(initialPitch);

                    //Cc.Add(1 / turnRadius);

                    //UpdateRoute();
                    //arcLength.Add(route.Distance * 1000);
                    path.AddPoint(addPoint, currentVelocity, 1 / turnRadius, psi_f, initialPitch);
                    log.Append(path.LastPoint.ToStringCSV());
                }
                initialPosition = path.LastPoint;
                initialYaw = angle;
                if (initialYaw > Math.PI)
                    initialYaw -= 2 * Math.PI;
            }
            else
            {

                // Calculate offsets
                double offsetX = Math.Sin(initialYaw) * turnRadius;
                double offsetY = Math.Cos(initialYaw) * turnRadius;
                double offsetZ = 0;

                PointLatLngAlt center = mapTools.OffsetInMeters(initialPosition, -offsetY, offsetX, offsetZ);
                double s1 = 0;
                double s = initialYaw+Math.PI/2;
                if (!(s > angle + Math.PI / 2)) // y mayor que 10 grados
                    angle -= 2 * Math.PI;
                while (s > angle + Math.PI / 2)
                {
                    s -= deltaS;
                    s1 += deltaS;

                    addPoint = mapTools.OffsetInMeters(center, turnRadius * Math.Sin(s), turnRadius * Math.Cos(s), 2 * turnRadius * s1 * Math.Sin(initialPitch));
                    //pointsList.Add(mapTools.OffsetInMeters(center, turnRadius * Math.Sin(s), turnRadius * Math.Cos(s), 2 * turnRadius * s1 * Math.Sin(initialPitch)));
                    //_pointsList.Add(pointsList[pointsList.Count - 1].GetPointLatLng());
                    //velocity.Add(currentVelocity);
                        

                    // Data necesary for lyapunov tracking

                    Point3D p = mapTools.Geodetic2ENU(addPoint, initialEnuPosition);

                    //x_enu.Add(p.X);
                    //y_enu.Add(p.Y);

                    double x_dot = turnRadius * Math.Cos(s);
                    double y_dot = -turnRadius * Math.Sin(s);

                    double psi_f = Math.PI / 2 - Math.Atan2(y_dot, x_dot) + Math.PI;
                    //psi_f.Add(Math.PI / 2 - Math.Atan2(y_dot, x_dot) +Math.PI);
                    if (psi_f > Math.PI)
                        psi_f -= 2 * Math.PI;
                    if (psi_f < -Math.PI)
                        psi_f += 2 * Math.PI;
                    //theta_f.Add(initialPitch);

                    //Cc.Add(1 / turnRadius);

                    //UpdateRoute();
                    //arcLength.Add(route.Distance * 1000);

                    path.AddPoint(addPoint, currentVelocity, 1 / turnRadius, psi_f, initialPitch);
                    log.Append(path.LastPoint.ToStringCSV());


                }
                initialYaw = angle;



                initialPosition = path.LastPoint;

                if (initialYaw < -Math.PI)
                    initialYaw += 2 * Math.PI;


            }
            UpdateRoute();
        }

        public List<PointLatLngAlt> GeneratePath(PointLatLngAlt FinalPosition, double FinalYaw)
        {
            return GeneratePath(this.initialPosition, this.initialYaw, FinalPosition, FinalYaw);
        }

        
        public List<PointLatLngAlt> GeneratePath(PointLatLngAlt InitialPosition, double InitialYaw, PointLatLngAlt FinalPosition, double FinalYaw)
        {
            //Seleccionar la distancia menor de entre las circunferencias generadas
            DubinsPath dubinsPath = DubinsPath.None;
            List<PointLatLngAlt> PathPoints = new List<PointLatLngAlt>();

            if (FinalYaw > Math.PI )
                FinalYaw -= 2 * Math.PI;
            else if (FinalYaw < -Math.PI )
                FinalYaw += 2 * Math.PI;

            PointLatLngAlt Ci1 = mapTools.OffsetInMeters(InitialPosition, Math.Cos(InitialYaw) * turnRadius, -Math.Sin(InitialYaw) * turnRadius,0); // Right
            PointLatLngAlt Ci2 = mapTools.OffsetInMeters(InitialPosition, -Math.Cos(InitialYaw) * turnRadius, Math.Sin(InitialYaw) * turnRadius,0); // Left

            PointLatLngAlt Cf1 = mapTools.OffsetInMeters(FinalPosition, Math.Cos(FinalYaw) * turnRadius, -Math.Sin(FinalYaw) * turnRadius,0); // Right
            PointLatLngAlt Cf2 = mapTools.OffsetInMeters(FinalPosition, -Math.Cos(FinalYaw) * turnRadius, Math.Sin(FinalYaw) * turnRadius,0); // Left

            PathPoints.Add(Ci1);
            PathPoints.Add(Ci2);
            PathPoints.Add(Cf1);
            PathPoints.Add(Cf2);

            List<double> listD = new List<double>();
            listD.Add(mapTools.GetDistanceMeters(Ci1, Cf1));
            listD.Add(mapTools.GetDistanceMeters(Ci2, Cf1));
            listD.Add(mapTools.GetDistanceMeters(Ci1, Cf2));
            listD.Add(mapTools.GetDistanceMeters(Ci2, Cf2));
            int index = -1;
            double minDistance = 10000000;
            int index2 = -1;
            double minDistance2 = 10000000;
            foreach (double distance in listD)
            {
                if (distance < minDistance)
                {
                    index = listD.IndexOf(distance);
                    minDistance = distance;
                }

            }

            foreach (double distance in listD)
            {
                if (distance < minDistance2 && listD.IndexOf(distance) != index)
                {
                    index2 = listD.IndexOf(distance);
                    minDistance2 = distance;
                }

            }

            
            

            switch (index)
            {
                case 0:
                    PathPoints.Add(Ci1);
                    PathPoints.Add(Cf1);
                    dubinsPath = DubinsPath.RSR;
                    break;

                case 1:
                    PathPoints.Add(Ci2);
                    PathPoints.Add(Cf1);
                    dubinsPath = DubinsPath.LSR;
                    break;

                case 2:
                    PathPoints.Add(Ci1);
                    PathPoints.Add(Cf2);
                    dubinsPath = DubinsPath.RSL;
                    break;

                case 3:
                    PathPoints.Add(Ci2);
                    PathPoints.Add(Cf2);
                    dubinsPath = DubinsPath.LSL;
                    break;

            }
            if (dubinsPath == DubinsPath.LSL)
            {
                this.initialPosition = InitialPosition;
                this.initialYaw = InitialYaw;   
                double slope = mapTools.GetAngleMeassuredFromNord(PathPoints[4], PathPoints[5]);
                double angle = Math.Acos(turnRadius * 2 / minDistance);
                double finalHeading = slope;

                this.initialPitch = FindPitch(InitialPosition, FinalPosition, InitialYaw, finalHeading, FinalYaw, mapTools.GetDistanceMeters(PathPoints[4], PathPoints[5]));
               
                TurnUntil(Direction.left, finalHeading);

                StraightLine(mapTools.GetDistanceMeters(PathPoints[4], PathPoints[5]));
               
                if (finalHeading < FinalYaw)
                    TurnUntil(Direction.right, FinalYaw);
                else
                    TurnUntil(Direction.left, FinalYaw);

            }
            else if (dubinsPath == DubinsPath.LSR)
            {
                this.initialPosition = InitialPosition;
                this.initialYaw = InitialYaw;
                
                double slope = mapTools.GetAngleMeassuredFromNord(PathPoints[4], PathPoints[5]);
                double angle = Math.Acos(turnRadius * 2 / minDistance);
                double finalHeading = slope + angle - Math.PI / 2;

                this.initialPitch = FindPitch(InitialPosition, FinalPosition, InitialYaw, finalHeading, FinalYaw, Math.Sqrt(Math.Pow(minDistance, 2) - Math.Pow(turnRadius * 2, 2)));

                TurnUntil(Direction.left, finalHeading);
                StraightLine(Math.Sqrt(Math.Pow(minDistance, 2) - Math.Pow(turnRadius * 2, 2)));
                TurnUntil(Direction.right, FinalYaw);

            }
            else if (dubinsPath == DubinsPath.RSR)
            {
                this.initialPosition = InitialPosition;
                this.initialYaw = InitialYaw;
                
                double slope = mapTools.GetAngleMeassuredFromNord(PathPoints[4], PathPoints[5]);
                double angle = Math.Acos(turnRadius * 2 / minDistance);
                double finalHeading = slope;

                this.initialPitch = FindPitch(InitialPosition, FinalPosition, InitialYaw, finalHeading, FinalYaw, mapTools.GetDistanceMeters(PathPoints[4], PathPoints[5]));

                TurnUntil(Direction.right, finalHeading);
                StraightLine(mapTools.GetDistanceMeters(PathPoints[4], PathPoints[5]));
                TurnUntil(Direction.right, FinalYaw);
            }
            else if (dubinsPath == DubinsPath.RSL)
            {
                this.initialPosition = InitialPosition;
                this.initialYaw = InitialYaw;
                
                double slope = mapTools.GetAngleMeassuredFromNord(PathPoints[4], PathPoints[5]);
                double angle = Math.Acos(turnRadius * 2 / minDistance);
                double finalHeading = slope - angle + Math.PI / 2;

                this.initialPitch = FindPitch(InitialPosition, FinalPosition, InitialYaw, finalHeading, FinalYaw, Math.Sqrt(Math.Pow(minDistance, 2) - Math.Pow(turnRadius * 2, 2)));

                TurnUntil(Direction.right, finalHeading);
                StraightLine(Math.Sqrt(Math.Pow(minDistance, 2) - Math.Pow(turnRadius * 2, 2)));
                TurnUntil(Direction.left, FinalYaw);
            }


            return PathPoints;


        }

        public double FindPitch(PointLatLngAlt Ip, PointLatLngAlt Fp, double IYaw, double FHeading, double FYaw, double d2)
        {
            double a1 = IYaw + 2 * Math.PI;
            double a2 = FHeading + 2 * Math.PI;
            double a3 = FYaw + 2 * Math.PI;
            double d1 = Math.Abs(a1 - a2) * 2 * turnRadius;
            //double d2 = mapTools.GetDistanceMeters(PathPoints[4], PathPoints[5]);
            double d3 = Math.Abs(a2 - a3) * 2 * turnRadius;
            double h = Math.Abs(Ip.Alt - Fp.Alt);
            double dTotal = d1 + d2 + d3;
            double angle = Math.Atan2(h, dTotal);
            return (Ip.Alt - Fp.Alt) > 0 ? -Math.Atan2(h, dTotal) : Math.Atan2(h, dTotal);



        }

        public void Turn180(PointLatLngAlt InitialPosition, double InitialHeading)
        {
            this.initialPosition = InitialPosition;
            this.initialYaw = InitialHeading;


            StraightLine(turnRadius * 2.2);

            GeneratePath(this.initialPosition, this.initialYaw, InitialPosition, InitialHeading - Math.PI);

            UpdateRoute();

        }

        public void Turn180()
        {


            StraightLine(turnRadius * 4);

            GeneratePath(this.initialPosition, this.initialYaw, InitialPosition, InitialYaw - Math.PI);

            UpdateRoute();

        }

        public PointLatLngAlt InitialPosition
        {
            get { return initialPosition; }
            set { initialPosition = value; }
        }

        public double InitialYaw
        {
            get { return initialYaw; }
            set { initialYaw = value; }
        }

        public double InitialPitch
        {
            get { return initialPitch; }
            set { initialPitch = value; }
        }

        public double TurnRadius
        {
            get { return turnRadius;}
            set { turnRadius = value; }
        }

        public GMapRoute Route
        {
            get { return route; }
        }

        public List<PointLatLngAlt> RoutePoints
        {
            get { return path.PointList; }
        }

        public List<double> cCurvature 
        {
            get {return path.Curvature;}
        }

        

        public List<double> Psi_f
        {
            get { return path.Psi_f; }
        }
        public List<double> Theta_f
        {
            get { return path.Theta_f; }
        }
      
        
        public List<double> Velocity
        {
            get { return path.Velocity; }
        }
        public double CurrentVelocity
        {
            get { return currentVelocity; }
            set { currentVelocity = value; }
        }

        private void UpdateRoute()
        {
            
            route = new GMapRoute(path._PointList, "Route");
        }

        public PointLatLngAlt InitialEnuPosition
        {
            set { initialEnuPosition = value; }
        }

        public void SaveLog(String toFile)
        {
            log.SaveLog(toFile);
        }

        public Path4D Path
        {
            get { return path; }
            set { path = value; }
        }

        
    }
}
