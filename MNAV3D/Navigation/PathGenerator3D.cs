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
    class PathGenerator3D
    {
        Log log = new Log();
        
        private PointLatLngAlt initialPosition;
        private PointLatLngAlt currentPosition;
        private double turnRadius;
        private double initialYaw;
        private double initialPitch;
        private double currentHeading;
        private GMapRoute route;
        private double deltaS = .1;
        Utils.MapTools mapTools = new Utils.MapTools();
        private List<PointLatLngAlt> pointsList = new List<PointLatLngAlt>();
        private List<PointLatLng> _pointsList = new List<PointLatLng>();
        private List<double> x_enu = new List<double>();
        private List<double> y_enu = new List<double>();
        private List<double> psi_f = new List<double>();
        private List<double> Cc = new List<double>();
        private List<double> arcLength = new List<double>();
        private PointLatLngAlt initialEnuPosition = new PointLatLngAlt();
        
        

        public enum Direction {right,left};
        public enum DubinsPath { LSL, LSR, RSL, RSR, None};
        public PathGenerator3D()
        {
           
        }

        public void newPath()
        {
            pointsList = new List<PointLatLngAlt>();
            _pointsList = new List<PointLatLng>();
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

            log.ResetLog();


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

                if (pointsList.Count == 0)
                {
                    pointsList.Add(initialPosition);
                    _pointsList.Add(pointsList[pointsList.Count - 1].GetPointLatLng());
                   
                    
                }
                else
                {
                    pointsList.Add(mapTools.OffsetInMeters(pointsList[pointsList.Count - 1], x, y, z));
                    _pointsList.Add(pointsList[pointsList.Count - 1].GetPointLatLng());
                }

                log.Append(pointsList[pointsList.Count - 1].ToStringCSV());
                    

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
                    pointsList.Add(mapTools.OffsetInMeters(center, turnRadius * Math.Sin(s), turnRadius * Math.Cos(s), 2*turnRadius*s1 * Math.Sin(initialPitch)));
                    _pointsList.Add(pointsList[pointsList.Count - 1].GetPointLatLng());
                    log.Append(pointsList[pointsList.Count - 1].ToStringCSV());
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
                    pointsList.Add(mapTools.OffsetInMeters(center, turnRadius * Math.Cos(s), turnRadius * Math.Sin(s), 2 * turnRadius * s1 * Math.Sin(initialPitch)));
                    _pointsList.Add(pointsList[pointsList.Count - 1].GetPointLatLng());
                    log.Append(pointsList[pointsList.Count - 1].ToStringCSV());
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
                initialYaw = initialYaw - angle;
                if (initialYaw < -Math.PI)
                    initialYaw += 2 * Math.PI;
                

            }
            UpdateRoute();
        }

        public void TurnUntil(Direction direction, double angle)
        {
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
                    pointsList.Add(mapTools.OffsetInMeters(center, turnRadius * Math.Sin(s), turnRadius * Math.Cos(s), 2 * turnRadius * s1 * Math.Sin(initialPitch)));
                    _pointsList.Add(pointsList[pointsList.Count - 1].GetPointLatLng());
                    log.Append(pointsList[pointsList.Count - 1].ToStringCSV());
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
                        pointsList.Add(mapTools.OffsetInMeters(center, turnRadius * Math.Sin(s), turnRadius * Math.Cos(s), 2 * turnRadius * s1 * Math.Sin(initialPitch)));
                        _pointsList.Add(pointsList[pointsList.Count - 1].GetPointLatLng());
                        log.Append(pointsList[pointsList.Count - 1].ToStringCSV());

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
                initialYaw = angle;
                
                

                initialPosition = pointsList[pointsList.Count - 1];

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
            if (h != 0)
                h = h;
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
            
            route = new GMapRoute(_pointsList, "Route");
        }

        public PointLatLngAlt InitialEnuPosition
        {
            set { initialEnuPosition = value; }
        }

        public void SaveLog(String toFile)
        {
            log.SaveLog(toFile);
        }
    }
}
