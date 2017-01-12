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

namespace MAV3DSim.Navigation
{
    class PathGenerator
    {
        private PointLatLng initialPosition;
        private PointLatLng currentPosition;
        private double turnRadius;
        private double initialHeading;
        private double currentHeading;
        private GMapRoute route;
        private double deltaS = .1;
        Utils.MapTools mapTools = new Utils.MapTools();
        private List<PointLatLng> pointsList = new List<PointLatLng>();
        private List<double> x_enu = new List<double>();
        private List<double> y_enu = new List<double>();
        private List<double> psi_f = new List<double>();
        private List<double> Cc = new List<double>();
        private List<double> arcLength = new List<double>();
        private PointLatLng initialEnuPosition = new PointLatLng();
        
        

        public enum Direction {right,left};
        public enum DubinsPath { LSL, LSR, RSL, RSR, None};
        public PathGenerator()
        {

        }

        public void newPath()
        {
            pointsList = new List<PointLatLng>();
            //pointsList.Add(initialPosition);
            x_enu = new List<double>();
            //x_enu.Add(0);
            y_enu = new List<double>();
            //y_enu.Add(0);
            psi_f = new List<double>();
            //psi_f.Add(0);
            Cc = new List<double>();
            //Cc.Add(0);
            arcLength = new List<double>();



        }

        public void StraightLine(double meters)
        {
            
            double x = 0;
            double y = 0;
            double deltaDistance = 1;
            double totalDistance = 0;
            y = deltaDistance * Math.Cos(InitialHeading);  // Lat
            x = deltaDistance * Math.Sin(InitialHeading);  // Lon
            //pointsList.Add(initialPosition);
            if (meters == 0)
                return;
            while (totalDistance < meters)
            {
                totalDistance += Math.Sqrt((Math.Pow(x, 2) + Math.Pow(y, 2)));

                if (pointsList.Count == 0)
                {
                    pointsList.Add(initialPosition);
                    
                }
                else
                    pointsList.Add(mapTools.OffsetInMeters(pointsList[pointsList.Count - 1], x, y));

                // Data necesary for lyapunov tracking

                Point3D p = mapTools.Geodetic2ENU(pointsList[pointsList.Count - 1], initialEnuPosition);

                x_enu.Add(p.X);
                y_enu.Add(p.Y);

                double x_dot = x;
                double y_dot = y;

                p = mapTools.Geodetic2ENU(new PointLatLng(y_dot, x_dot), new PointLatLng( 0, 0));

                double x_dot_enu = p.X;
                double y_dot_enu = p.Y;

                double x_ddot = 0;
                double y_ddot = 0;

                p = mapTools.Geodetic2ENU(new PointLatLng(y_ddot, x_ddot), new PointLatLng(0, 0));

                double x_ddot_enu = p.X;
                double y_ddot_enu = p.Y;

                psi_f.Add(Math.PI/2-Math.Atan2(y_dot , x_dot));
                if (psi_f[psi_f.Count - 1] > Math.PI)
                    psi_f[psi_f.Count - 1] -= 2 * Math.PI;
                if (psi_f[psi_f.Count - 1] < -Math.PI)
                    psi_f[psi_f.Count - 1] += 2 * Math.PI;
                
                Cc.Add(0);
               
                UpdateRoute();
                arcLength.Add(route.Distance * 1000);




            }
            //double d = route.Distance;
            initialPosition = pointsList[pointsList.Count - 1];
            UpdateRoute();
            
        }

        public void Turn(Direction direction, double angle)
        {
            if(direction == Direction.right)
            {
                
                // Calculate offsets
                double offsetX = -Math.Sin(initialHeading) * turnRadius;
                double offsetY = Math.Cos(initialHeading) * turnRadius;

                PointLatLng center = mapTools.OffsetInMeters(initialPosition, offsetY, offsetX);
                
                double s = initialHeading+3*Math.PI/2;
                while (s < angle + initialHeading + 3*Math.PI / 2)
                {
                    s += deltaS;
                    pointsList.Add(mapTools.OffsetInMeters(center, turnRadius * Math.Sin(s), turnRadius * Math.Cos(s)));

                    // Data necesary for lyapunov tracking

                    Point3D p = mapTools.Geodetic2ENU(pointsList[pointsList.Count - 1], initialEnuPosition);

                    x_enu.Add(p.X);
                    y_enu.Add(p.Y);

                    double x_dot = turnRadius * Math.Cos(s);
                    double y_dot = -turnRadius * Math.Sin(s);

                    psi_f.Add(Math.PI / 2 - Math.Atan2(y_dot , x_dot));
                    if (psi_f[psi_f.Count - 1] > Math.PI)
                        psi_f[psi_f.Count - 1] -= 2 * Math.PI;
                    if (psi_f[psi_f.Count - 1] < -Math.PI)
                        psi_f[psi_f.Count - 1] += 2 * Math.PI;

                    Cc.Add(1/turnRadius);

                    UpdateRoute();
                    arcLength.Add(route.Distance * 1000);
                }
                initialPosition = pointsList[pointsList.Count - 1];
                InitialHeading = initialHeading+angle;
                if (InitialHeading > Math.PI)
                    InitialHeading -= 2 * Math.PI;
            }
            else
            {
                
                // Calculate offsets
                double offsetX = Math.Sin(initialHeading) * turnRadius;
                double offsetY = -Math.Cos(initialHeading) * turnRadius;

                PointLatLng center = mapTools.OffsetInMeters(initialPosition, offsetY, offsetX);
                
                double s = -initialHeading;
                while (s < angle -initialHeading )
                {
                    s += deltaS;
                    pointsList.Add(mapTools.OffsetInMeters(center, turnRadius * Math.Cos(s), turnRadius * Math.Sin(s)));

                    // Data necesary for lyapunov tracking

                    Point3D p = mapTools.Geodetic2ENU(pointsList[pointsList.Count - 1], initialEnuPosition);

                    x_enu.Add(p.X);
                    y_enu.Add(p.Y);

                    double x_dot = -turnRadius * Math.Sin(s);
                    double y_dot = turnRadius * Math.Cos(s);

                    psi_f.Add(Math.PI / 2 - Math.Atan2(y_dot, x_dot));
                    if (psi_f[psi_f.Count - 1] > Math.PI)
                        psi_f[psi_f.Count - 1] -= 2 * Math.PI;
                    if (psi_f[psi_f.Count - 1] < -Math.PI)
                        psi_f[psi_f.Count - 1] += 2 * Math.PI;

                    Cc.Add(1 / turnRadius);

                    UpdateRoute();
                    arcLength.Add(route.Distance * 1000);
                }

                initialPosition = pointsList[pointsList.Count - 1];
                InitialHeading = initialHeading - angle;
                if (InitialHeading < -Math.PI)
                    InitialHeading += 2 * Math.PI;
                

            }
            UpdateRoute();
        }

        public void TurnUntil(Direction direction, double angle)
        {
            if (direction == Direction.right)
            {

                // Calculate offsets
                double offsetX = Math.Sin(initialHeading) * turnRadius;
                double offsetY = Math.Cos(initialHeading) * turnRadius;

                PointLatLng center = mapTools.OffsetInMeters(initialPosition, offsetY, -offsetX);

                double s = initialHeading + 3 * Math.PI / 2;
                if (!(s < angle + 3 * Math.PI / 2) && Math.Abs(s - angle + 3 * Math.PI / 2) > 0.1745 ) // y mayor que 10 grados
                    angle += 2 * Math.PI;
                while (s < angle + 3 * Math.PI / 2)
                {
                    s += deltaS;
                    pointsList.Add(mapTools.OffsetInMeters(center, turnRadius * Math.Sin(s), turnRadius * Math.Cos(s)));

                    // Data necesary for lyapunov tracking

                    Point3D p = mapTools.Geodetic2ENU(pointsList[pointsList.Count - 1], initialEnuPosition);

                    x_enu.Add(p.X);
                    y_enu.Add(p.Y);

                    double x_dot = turnRadius * Math.Cos(s);
                    double y_dot = -turnRadius * Math.Sin(s);

                    psi_f.Add(Math.PI / 2 - Math.Atan2(y_dot, x_dot));
                    if (psi_f[psi_f.Count - 1] > Math.PI)
                        psi_f[psi_f.Count - 1] -= 2 * Math.PI;
                    if (psi_f[psi_f.Count - 1] < -Math.PI)
                        psi_f[psi_f.Count - 1] += 2 * Math.PI;

                    Cc.Add(1 / turnRadius);

                    UpdateRoute();
                    arcLength.Add(route.Distance * 1000);
                }
                initialPosition = pointsList[pointsList.Count - 1];
                InitialHeading = angle;
                if (InitialHeading > Math.PI)
                    InitialHeading -= 2 * Math.PI;
            }
            else
            {

                // Calculate offsets
                double offsetX = Math.Sin(initialHeading) * turnRadius;
                double offsetY = Math.Cos(initialHeading) * turnRadius;

                PointLatLng center = mapTools.OffsetInMeters(initialPosition, -offsetY, offsetX);

                double s = initialHeading+Math.PI/2;
                if (!(s > angle + Math.PI / 2)) // y mayor que 10 grados
                    angle -= 2 * Math.PI;
                while (s > angle + Math.PI / 2)
                    {
                        s -= deltaS;
                        pointsList.Add(mapTools.OffsetInMeters(center, turnRadius * Math.Sin(s), turnRadius * Math.Cos(s)));

                        // Data necesary for lyapunov tracking

                        Point3D p = mapTools.Geodetic2ENU(pointsList[pointsList.Count - 1], initialEnuPosition);

                        x_enu.Add(p.X);
                        y_enu.Add(p.Y);

                        double x_dot = turnRadius * Math.Cos(s);
                        double y_dot = -turnRadius * Math.Sin(s);

                        psi_f.Add(Math.PI / 2 - Math.Atan2(y_dot, x_dot) +Math.PI);
                        if (psi_f[psi_f.Count - 1] > Math.PI)
                            psi_f[psi_f.Count - 1] -= 2 * Math.PI;
                        if (psi_f[psi_f.Count - 1] < -Math.PI)
                            psi_f[psi_f.Count - 1] += 2 * Math.PI;

                        Cc.Add(1 / turnRadius);

                        UpdateRoute();
                        arcLength.Add(route.Distance * 1000);
                    }
                InitialHeading = angle;
                
                

                initialPosition = pointsList[pointsList.Count - 1];
                
                if (InitialHeading < -Math.PI)
                    InitialHeading += 2 * Math.PI;


            }
            UpdateRoute();
        }

        public List<PointLatLng> GeneratePath(PointLatLng FinalPosition, double FinalHeading)
        {
            return GeneratePath(this.initialPosition, this.initialHeading, FinalPosition, FinalHeading);
        }

        
        public List<PointLatLng> GeneratePath(PointLatLng InitialPosition, double InitialHeading, PointLatLng FinalPosition, double FinalHeading)
        {
            //Seleccionar la distancia menor de entre las circunferencias generadas
            DubinsPath dubinsPath = DubinsPath.None;
            List<PointLatLng> PathPoints = new List<PointLatLng>();

            if (FinalHeading > Math.PI )
                FinalHeading -= 2 * Math.PI;
            else if (FinalHeading < -Math.PI )
                FinalHeading += 2 * Math.PI;

            PointLatLng Ci1 = mapTools.OffsetInMeters(InitialPosition, Math.Cos(InitialHeading) * turnRadius, -Math.Sin(InitialHeading) * turnRadius); // Right
            PointLatLng Ci2 = mapTools.OffsetInMeters(InitialPosition, -Math.Cos(InitialHeading) * turnRadius, Math.Sin(InitialHeading) * turnRadius); // Left

            PointLatLng Cf1 = mapTools.OffsetInMeters(FinalPosition, Math.Cos(FinalHeading) * turnRadius, -Math.Sin(FinalHeading) * turnRadius); // Right
            PointLatLng Cf2 = mapTools.OffsetInMeters(FinalPosition, -Math.Cos(FinalHeading) * turnRadius, Math.Sin(FinalHeading) * turnRadius); // Left

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


#region old
            //if (dubinsPath == DubinsPath.LSL)
            //{

            //    this.initialPosition = InitialPosition;
            //    this.initialHeading = InitialHeading;
            //    newPath();

            //    double slope = mapTools.GetSlopeFromNord(PathPoints[4], PathPoints[5]);

            //    double angle2turn = -InitialHeading + Math.PI / 2 - slope;
            //    double angle2turn2 = Math.Atan2(Math.Sin(slope), Math.Cos(slope)) - Math.Atan2(Math.Sin(InitialHeading), Math.Cos(InitialHeading));
            //    double angle2turn3 = slope - InitialHeading;
            //    if (Math.Sign(InitialHeading) != Math.Sign(slope))
            //    {
            //        if (Math.Sign(slope) > 0)
            //        {
            //            angle2turn = angle2turn - 2 * Math.PI;
            //            if (angle2turn > 3 * Math.PI/2)
            //                angle2turn -= 2 * Math.PI;
            //            else if (angle2turn < 3 * Math.PI / 2)
            //                angle2turn += 2 * Math.PI;
            //        }
            //        else
            //        {
            //            //double initial = InitialHeading + Math.PI;
            //            angle2turn = angle2turn - 2 * Math.PI;
            //            if (angle2turn > 2 * Math.PI)
            //                angle2turn -= 2 * Math.PI;
            //            else if (angle2turn < -2 * Math.PI)
            //                angle2turn += 2 * Math.PI;
            //        }
            //    }
            //    else
            //    {
            //        if (angle2turn > Math.PI)
            //            angle2turn -= 2 * Math.PI;
            //        else if (angle2turn < -Math.PI)
            //            angle2turn += 2 * Math.PI;
            //    }
            //    Turn(MNAV3D.Controler.PathGenerator.Direction.left, Math.Abs(angle2turn));
            //    StraightLine(mapTools.GetDistanceMeters(PathPoints[4], PathPoints[5]));
            //    //double vectorSlope = FinalHeading;
            //    //double finalTurn = this.initialHeading - vectorSlope;
            //    //Turn(MNAV3D.Controler.PathGenerator.Direction.left, finalTurn);

                
            //}
            //else if (dubinsPath == DubinsPath.LSR)
            //{
            //    this.initialPosition = InitialPosition;
            //    this.initialHeading = InitialHeading;
            //    newPath();

            //    double angle = Math.Acos(turnRadius * 2 / minDistance);
            //    double slope2 =  (mapTools.GetSlopeFromNord(PathPoints[4], PathPoints[5]));
            //    double angle2turn = InitialHeading + Math.PI / 2 + slope2 - Math.PI + Math.PI / 2 - angle;
            //    angle2turn = InitialHeading  + slope2  - angle;
            //    if (Math.Sign(InitialHeading) != Math.Sign(slope2 + angle))
            //    {
            //        if (Math.Sign(slope2 + angle) > 0)
            //        {
            //            //double initial = InitialHeading + Math.PI;
            //            angle2turn = InitialHeading + slope2 - angle + 2 * Math.PI;
            //            if (angle2turn > 2 * Math.PI)
            //                angle2turn -= 2 * Math.PI;
            //            else if (angle2turn < -2 * Math.PI)
            //                angle2turn += 2 * Math.PI;
            //        }
            //        else
            //        {
            //            //double initial = InitialHeading - Math.PI;
            //            angle2turn = InitialHeading + slope2 - angle + 2 * Math.PI;
            //            if(angle2turn > 2*Math.PI)
            //                angle2turn -= 2*Math.PI;
            //            else if(angle2turn < -2*Math.PI)
            //                angle2turn += 2*Math.PI;
            //        }
            //    }
            //    else
            //    {
            //        if (angle2turn > 3*Math.PI/2)
            //            angle2turn -= 2 * Math.PI;
            //        else if (angle2turn < 3 * Math.PI / 2)
            //            angle2turn += 2 * Math.PI;
            //    }
            //    Turn(MNAV3D.Controler.PathGenerator.Direction.left, Math.Abs(angle2turn));
            //    StraightLine(Math.Sqrt(Math.Pow(minDistance, 2) - Math.Pow(turnRadius * 2, 2)));
            //    //double vectorSlope = FinalHeading;
            //    //double finalTurn = this.initialHeading - vectorSlope;

            //    //Turn(MNAV3D.Controler.PathGenerator.Direction.right, -finalTurn);
            //}
            //else if (dubinsPath == DubinsPath.RSR)
            //{
            //    this.initialPosition = InitialPosition;
            //    this.initialHeading = InitialHeading;
            //    newPath();

            //    double slope2 = Math.PI / 2 - (mapTools.GetSlope(PathPoints[4], PathPoints[5]));
            //    if (slope2 > Math.PI)
            //        slope2 -= 2 * Math.PI;
            //    else if (slope2 < -Math.PI)
            //        slope2 += 2 * Math.PI;
            //    double angle2turn = -InitialHeading + Math.PI / 2 - slope2;
            //    if (angle2turn > Math.PI)
            //        angle2turn -= 2 * Math.PI;
            //    else if (angle2turn < -Math.PI)
            //        angle2turn += 2 * Math.PI;

            //    Turn(MNAV3D.Controler.PathGenerator.Direction.right, Math.Abs(angle2turn));
            //    StraightLine(mapTools.GetDistanceMeters(PathPoints[4], PathPoints[5]));
            //    double vectorSlope = FinalHeading;
            //    double finalTurn = this.initialHeading - vectorSlope;
            //    if (finalTurn > Math.PI || (this.initialHeading > 0 && vectorSlope > 0 && finalTurn > Math.PI))
            //        finalTurn -= 2 * Math.PI;
            //    else if (finalTurn < -Math.PI)
            //        finalTurn += 2 * Math.PI;
            //    Turn(MNAV3D.Controler.PathGenerator.Direction.right, Math.Abs(finalTurn));
            //}
            //else if (dubinsPath == DubinsPath.RSL)
            //{
            //    this.initialPosition = InitialPosition;
            //    this.initialHeading = InitialHeading;
            //    newPath();
            //    double angle = Math.Acos(turnRadius * 2 / minDistance);
            //    double slope2 = Math.PI / 2 - (mapTools.GetSlope(PathPoints[4], PathPoints[5]));
            //    if (slope2 > Math.PI)
            //        slope2 -= 2 * Math.PI;
            //    else if (slope2 < -Math.PI)
            //        slope2 += 2 * Math.PI;
            //    double angle2turn = -InitialHeading + Math.PI / 2 - slope2 + Math.PI/2 - angle;

            //    if (angle2turn > Math.PI)
            //        angle2turn -= 2 * Math.PI;
            //    else if (angle2turn < -Math.PI)
            //        angle2turn += 2 * Math.PI;

            //    Turn(MNAV3D.Controler.PathGenerator.Direction.right, Math.Abs(angle2turn));
            //    StraightLine(Math.Sqrt(Math.Pow(minDistance,2) - Math.Pow(turnRadius*2,2)));
            //    double vectorSlope = FinalHeading;
            //    double finalTurn = this.initialHeading - vectorSlope;
            //    if (finalTurn > 5 * Math.PI / 4 || (this.initialHeading < 0 && vectorSlope > 0 && finalTurn > 0) || (this.initialHeading < 0 && vectorSlope < 0 && finalTurn > Math.PI/2))
            //        finalTurn -= 2 * Math.PI;
            //    else if (finalTurn < -5 * Math.PI / 4 || (this.initialHeading < 0 && vectorSlope > 0 && finalTurn < 0) || (this.initialHeading < 0 && vectorSlope < 0 && finalTurn < -Math.PI/2))
            //        finalTurn += 2 * Math.PI;
            //    Turn(MNAV3D.Controler.PathGenerator.Direction.left, Math.Abs(finalTurn));
            //}
            #endregion

            if (dubinsPath == DubinsPath.LSL)
            {
                
                this.initialPosition = InitialPosition;
                this.initialHeading = InitialHeading;
                
                double slope = mapTools.GetAngleMeassuredFromNord(PathPoints[4], PathPoints[5]);
                double angle = Math.Acos(turnRadius * 2 / minDistance);
                double finalHeading = slope;
                TurnUntil(Direction.left, finalHeading);

                StraightLine(mapTools.GetDistanceMeters(PathPoints[4], PathPoints[5]));
                if(finalHeading<FinalHeading)
                    TurnUntil(Direction.right, FinalHeading);
                else
                    TurnUntil(Direction.left, FinalHeading);

            }
            else if (dubinsPath == DubinsPath.LSR)
            {
                this.initialPosition = InitialPosition;
                this.initialHeading = InitialHeading;
                
                double slope = mapTools.GetAngleMeassuredFromNord(PathPoints[4], PathPoints[5]);
                double angle = Math.Acos(turnRadius * 2 / minDistance);
                double finalHeading = slope + angle - Math.PI / 2;
                TurnUntil(Direction.left, finalHeading);
                StraightLine(Math.Sqrt(Math.Pow(minDistance, 2) - Math.Pow(turnRadius * 2, 2)));
                TurnUntil(Direction.right, FinalHeading);

            }
            else if (dubinsPath == DubinsPath.RSR)
            {
                this.initialPosition = InitialPosition;
                this.initialHeading = InitialHeading;
                
                double slope = mapTools.GetAngleMeassuredFromNord(PathPoints[4], PathPoints[5]);
                double angle = Math.Acos(turnRadius * 2 / minDistance);
                double finalHeading = slope;
                TurnUntil(Direction.right, finalHeading);
                StraightLine(mapTools.GetDistanceMeters(PathPoints[4], PathPoints[5]));
                TurnUntil(Direction.right, FinalHeading);
            }
            else if (dubinsPath == DubinsPath.RSL)
            {
                this.initialPosition = InitialPosition;
                this.initialHeading = InitialHeading;
                
                double slope = mapTools.GetAngleMeassuredFromNord(PathPoints[4], PathPoints[5]);
                double angle = Math.Acos(turnRadius * 2 / minDistance);
                double finalHeading = slope - angle + Math.PI / 2;
                TurnUntil(Direction.right, finalHeading);
                StraightLine(Math.Sqrt(Math.Pow(minDistance, 2) - Math.Pow(turnRadius * 2, 2)));
                TurnUntil(Direction.left, FinalHeading);
            }


            return PathPoints;


        }

        public void Turn180(PointLatLng InitialPosition, double InitialHeading)
        {
            this.initialPosition = InitialPosition;
            this.initialHeading = InitialHeading;


            StraightLine(turnRadius * 2.2);

            GeneratePath(this.initialPosition, this.initialHeading, InitialPosition, InitialHeading - Math.PI);

            UpdateRoute();

        }

        public void Turn180()
        {


            StraightLine(turnRadius * 4);

            GeneratePath(this.initialPosition, this.initialHeading, InitialPosition, InitialHeading - Math.PI);

            UpdateRoute();

        }

        

        public PointLatLng InitialPosition
        {
            get { return initialPosition; }
            set { initialPosition = value; }
        }

        public double InitialHeading
        {
            get { return initialHeading; }
            set { initialHeading = value; }
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

        public List<PointLatLng> RoutePoints
        {
            get { return pointsList; }
        }

        public List<double> cCurvature 
        {
            get {return Cc;}
        }

        public List<double> ArcLength
        {
            get{return arcLength;}
        }

        public List<double> Psi_f
        {
            get{return psi_f;}
        }
        public List<double> X_enu
        {
            get{return x_enu;}
        }
        public List<double> Y_enu 
        {
            get{return y_enu;}
        }

        private void UpdateRoute()
        {
            route = new GMapRoute(pointsList, "Route");
        }

        public PointLatLng InitialEnuPosition
        {
            set { initialEnuPosition = value; }
        }
    }
}
