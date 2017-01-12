using GMap.NET;
using GMap.NET.WindowsForms;
using MAV3DSim.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAV3DSim.Navigation
{
    
    public class Navigator3D
    {
        PathGenerator3D pathGenerator;
        List<PointLatLngAlt> pointList;
        List<PointLatLngAlt> pointListObjectives;
        List<PointLatLng> _pointListObjectives;
        List<PointLatLngAlt> pointListObjectivesCircle;
        List<PointLatLng> _pointListObjectivesCircle;
        List<PointLatLngAlt> pointListObjectives_o;
        List<PointLatLngAlt> pointListObjectivesAux;
        List<PointLatLngAlt> pointListObjectivesAuxCircle;
        List<PointLatLng> pointsOfInterests;
        List<PointLatLngAlt> rectangle;
        List<PointLatLngAlt> pointListObjectivesLanding;

        List<PointLatLngAlt> waypoints;

        // Guidance data
        List<double> cCurvature = new List<double>();     // Curvatura de curva
        List<double> arcLength = new List<double>();      // Arc-length
        List<double> arcLength_test = new List<double>();      // Arc-length
        List<double> psi_f = new List<double>();  // Angulo tangencial con respecto a la normal.
        List<double> cCurvature_o = new List<double>();     // Curvatura de curva
        //List<double> arcLength_o = new List<double>();      // Arc-length
        List<double> psi_f_o = new List<double>();  // Angulo tangencial con respecto a la normal.
        //List<double> x_enu = new List<double>();
        //List<double> y_enu = new List<double>();
        List<double> cCurvatureAux = new List<double>();     // Curvatura de curva
        //List<double> arcLengthAux = new List<double>();      // Arc-length
        List<double> Psi_fAux = new List<double>();  // Angulo tangencial con respecto a la normal.
        //List<double> x_enuAux = new List<double>();
        //List<double> y_enuAux = new List<double>();
        List<double> cCurvatureAuxCircle = new List<double>();     // Curvatura de curva
        List<double> arcLengthAuxCircle = new List<double>();      // Arc-length
        List<double> Psi_fAuxCircle = new List<double>();  // Angulo tangencial con respecto a la normal.



        int currentObjective = 0;
        int currentObjectiveOld = 0;
        int currentPointOfInterest = 0;
        //int currentArcLength = 0;

        Utils.MapTools mapTools = new Utils.MapTools();

        double turnRadius = 0;
        PointLatLngAlt initialEnuPosition = new PointLatLngAlt();

        bool isInitialPath = false;
        bool isTurn180 = false;
        bool isReturnFromCircle = false;
        bool isCirclePath = false;
        bool ignorePointsOfInterest = false;

        Log log = new Log();

        public Navigator3D()
        {
            pathGenerator = new PathGenerator3D();
            pointsOfInterests = new List<PointLatLng>();
        }

        public void RestoreOriginalPath()
        {
            pointListObjectives = pointListObjectives_o;
            cCurvature = cCurvature_o;
            ResetArc();
            psi_f = psi_f_o;
            currentObjective = 0;
            isTurn180 = false;
            isInitialPath = false;
            isReturnFromCircle = false;
            isCirclePath = false;
            ignorePointsOfInterest = false;

        }

        private void ResetArc()
        {
            arcLength = new List<double>(); // arcLength_o;
            arcLength.Add(0);
            //currentArcLength = 0;
        }
        public void CreateInitialPath(double InitialYaw, double Lat, double Lon, double Alt)
        {
            isInitialPath = true;
            isTurn180 = false;
            pathGenerator.InitialYaw = InitialYaw;
            pathGenerator.InitialPosition = new PointLatLngAlt(Lat, Lon,Alt);
            pathGenerator.InitialEnuPosition = initialEnuPosition;
            pathGenerator.newPath();
            pathGenerator.StraightLine(pathGenerator.TurnRadius * 1.5);

            // Generate path to initial point of the rectangle generated path
            pathGenerator.GeneratePath(pointListObjectives[currentObjective], psi_f[currentObjective]);

            // Initial path
            pointListObjectivesAux = pointListObjectives;
            cCurvatureAux = cCurvature;     // Curvatura de curva
            //arcLengthAux = arcLength;      // Arc-length

            Psi_fAux = psi_f;  // Angulo tangencial con respecto a la normal.
            //x_enuAux = x_enu;
            //y_enuAux = y_enu;

            

            pointListObjectives = pathGenerator.RoutePoints;
            cCurvature = pathGenerator.cCurvature;     // Curvatura de curva
            //arcLength = new List<double>(); // pathGenerator.ArcLength;      // Arc-length
            //currentArcLength = 0;
            ResetArc();
            arcLength_test = pathGenerator.ArcLength;
            psi_f = pathGenerator.Psi_f;  // Angulo tangencial con respecto a la normal.
            //x_enu = pathGenerator.X_enu;
            //y_enu = pathGenerator.Y_enu;
        }

        public bool ProximityCheck(double L1, double Lat, double Lon, double Alt, double psi)
        {
            if(proximityCheck(L1,Lat,Lon,Alt))
            {
                if (isInitialPath)
                {
                    isInitialPath = false;
                    currentObjective = 0;
                    pointListObjectives = pointListObjectivesAux;
                    cCurvature = cCurvatureAux;     // Curvatura de curva
                    //arcLength = new List<double>(); // arcLengthAux;      // Arc-length
                    //currentArcLength = 0;
                    ResetArc();
                    psi_f = Psi_fAux;  // Angulo tangencial con respecto a la normal.
                    //x_enu = x_enuAux;
                    //y_enu = y_enuAux;

                    
                }
                else
                {


                    isTurn180 = !isTurn180;

                    if (isTurn180)
                    {
                        currentObjective = 0;
                        pathGenerator.InitialPosition = pointListObjectives[pointListObjectives.Count - 1];
                        pathGenerator.InitialYaw = psi;
                        pathGenerator.InitialPitch = 0; // just turn, do not change altitude
                        pathGenerator.InitialEnuPosition = initialEnuPosition;
                        pathGenerator.newPath();
                        pathGenerator.TurnRadius = turnRadius;
                        pathGenerator.Turn180(pointListObjectives[pointListObjectives.Count - 1], psi);
                        pointListObjectivesAux = pointListObjectives;
                        cCurvatureAux = cCurvature;     // Curvatura de curva
                        //arcLengthAux = arcLength;      // Arc-length
                        Psi_fAux = psi_f;  // Angulo tangencial con respecto a la normal.
                        //x_enuAux = x_enu;
                        //y_enuAux = y_enu;

                        pointListObjectives = pathGenerator.RoutePoints;
                        cCurvature = pathGenerator.cCurvature;     // Curvatura de curva
                        //arcLength = new List<double>(); // pathGenerator.ArcLength;      // Arc-length
                        //currentArcLength = 0;
                        ResetArc();
                        psi_f = pathGenerator.Psi_f;  // Angulo tangencial con respecto a la normal.
                        //x_enu = pathGenerator.X_enu;
                        //y_enu = pathGenerator.Y_enu;
                    }
                    else
                    {
                        currentObjective = 0;
                        pointListObjectives = pointListObjectivesAux;
                        cCurvature = cCurvatureAux;     // Curvatura de curva
                        //arcLength = new List<double>(); // arcLengthAux;      // Arc-length
                        //currentArcLength = 0;
                        ResetArc();

                        psi_f = Psi_fAux;  // Angulo tangencial con respecto a la normal.
                        //x_enu = x_enuAux;
                        //y_enu = y_enuAux;

                        pointListObjectives.Reverse();
                        cCurvature.Reverse();
                        //arcLength.Reverse();
                        psi_f.Reverse();
                        //x_enu.Reverse();
                        //y_enu.Reverse();
                    }
                }
                return true;
            }
            return false;
        }

        private bool proximityCheck(double L1, double lat, double lon, double alt)
        {
            double dist = 0;
            bool first = true;

            while (dist < L1)
            {
                if (!first)
                {
                    currentObjective++;
                    if (currentObjective >= pointListObjectives.Count)
                        return true;  //currentObjective = 0;  
                }
                dist = mapTools.GetDistanceMeters(new PointLatLngAlt(lat, lon,alt), pointListObjectives[currentObjective]);
                first = false;
            };
            return false;
        }

        public bool VelocityCheck(double s_dot, double dt, double psi)
        {
            if (velocityCheck(s_dot, dt)) // if true means that it reach the last point.
            {
                if (isInitialPath)
                {
                    isInitialPath = false;
                    pointListObjectives = pointListObjectivesAux;
                    cCurvature = cCurvatureAux;     // Curvatura de curva
                    //arcLength = new List<double>(); // arcLengthAux;      // Arc-length
                    //currentArcLength = 0;
                    ResetArc();
                    psi_f = Psi_fAux;  // Angulo tangencial con respecto a la normal.
                    //x_enu = x_enuAux;
                    //y_enu = y_enuAux;

                    if (!isReturnFromCircle)
                        currentObjective = 0;
                    else
                    {
                        currentObjective = currentObjectiveOld;
                        isReturnFromCircle = false;
                        isCirclePath = false;
                        arcLength = arcLengthAuxCircle;
                    }
                }
                else
                {

                    #region Old180Turn
                    //isTurn180 = !isTurn180;

                    //if (isTurn180)
                    //{
                    //    currentObjective = 0;
                    //    pathGenerator.InitialPosition = new PointLatLng(pointListObjectives[pointListObjectives.Count - 1].Lat, pointListObjectives[pointListObjectives.Count - 1].Lng);
                    //    pathGenerator.InitialYaw = imu.psi;
                    //    pathGenerator.InitialEnuPosition = new PointLatLng(gpsLat, gpsLon);
                    //    pathGenerator.newPath();
                    //    pathGenerator.TurnRadius = Convert.ToDouble(txtTurnRadius.Text);
                    //    pathGenerator.Turn180(new PointLatLng(pointListObjectives[pointListObjectives.Count - 1].Lat, pointListObjectives[pointListObjectives.Count - 1].Lng), imu.psi);
                    //    pointListObjectivesAux = pointListObjectives;
                    //    cCurvatureAux = cCurvature;     // Curvatura de curva
                    //    arcLengthAux = arcLength;      // Arc-length
                    //    Psi_fAux = Psi_f;  // Angulo tangencial con respecto a la normal.
                    //    x_enuAux = x_enu;
                    //    y_enuAux = y_enu;

                    //    pointListObjectives = pathGenerator.RoutePoints;
                    //    cCurvature = pathGenerator.cCurvature;     // Curvatura de curva
                    //    arcLength = pathGenerator.ArcLength;      // Arc-length
                    //    Psi_f = pathGenerator.Psi_f;  // Angulo tangencial con respecto a la normal.
                    //    x_enu = pathGenerator.X_enu;
                    //    y_enu = pathGenerator.Y_enu;

                    //    if (overlay.Routes.Count < 4)
                    //        overlay.Routes.Add(pathGenerator.Route);
                    //    else
                    //        overlay.Routes[3] = pathGenerator.Route;
                    //    overlay.Routes[3].Stroke.Color = System.Drawing.Color.Green;



                    //}
                    //else
                    //{
                    //    currentObjective = 0;
                    //    pointListObjectives = pointListObjectivesAux;
                    //    cCurvature = cCurvatureAux;     // Curvatura de curva
                    //    arcLength = arcLengthAux;      // Arc-length
                    //    Psi_f = Psi_fAux;  // Angulo tangencial con respecto a la normal.
                    //    x_enu = x_enuAux;
                    //    y_enu = y_enuAux;

                    //    pointListObjectives.Reverse();
                    //    cCurvature.Reverse();
                    //    arcLength.Reverse();
                    //    Psi_f.Reverse();
                    //    x_enu.Reverse();
                    //    y_enu.Reverse();

                    //    for (int i = 0; i < Psi_f.Count; i++)
                    //    {
                    //        if (Psi_f[i]<0)
                    //            Psi_f[i] = Psi_f[i] + Math.PI;
                    //        if (Psi_f[i] > 0)
                    //            Psi_f[i] = Psi_f[i] - Math.PI;

                    //    }
                    //}
                    #endregion

                    if (!isCirclePath)
                    {
                        isTurn180 = !isTurn180;

                        if (isTurn180)
                        {
                            currentObjective = 0;
                            pathGenerator.InitialPosition = pointListObjectives[pointListObjectives.Count - 1];
                            pathGenerator.InitialYaw = psi;
                            pathGenerator.InitialPitch = 0;
                            pathGenerator.InitialEnuPosition = initialEnuPosition;

                            pathGenerator.newPath();
                            pathGenerator.TurnRadius = turnRadius;
                            pathGenerator.Turn180(pointListObjectives[pointListObjectives.Count - 1], psi);
                            pointListObjectivesAux = pointListObjectives;
                            cCurvatureAux = cCurvature;     // Curvatura de curva
                            //arcLengthAux = arcLength;      // Arc-length
                            Psi_fAux = psi_f;  // Angulo tangencial con respecto a la normal.
                            //x_enuAux = x_enu;
                            //y_enuAux = y_enu;
                            pointListObjectives = pathGenerator.RoutePoints;
                            cCurvature = pathGenerator.cCurvature;     // Curvatura de curva
                            //arcLength = new List<double>(); // pathGenerator.ArcLength;      // Arc-length
                            //currentArcLength = 0;
                            ResetArc();
                            psi_f = pathGenerator.Psi_f;  // Angulo tangencial con respecto a la normal.
                            //x_enu = pathGenerator.X_enu;
                            //y_enu = pathGenerator.Y_enu;
                        }
                        else
                        {
                            currentObjective = 0;
                            pointListObjectives = pointListObjectivesAux;
                            cCurvature = cCurvatureAux;     // Curvatura de curva
                            //arcLength = new List<double>(); // arcLengthAux;      // Arc-length
                            psi_f = Psi_fAux;  // Angulo tangencial con respecto a la normal.
                            //currentArcLength = 0;
                            ResetArc();
                            //x_enu = x_enuAux;
                            //y_enu = y_enuAux;

                            pointListObjectives.Reverse();
                            cCurvature.Reverse();
                            //arcLength.Reverse();
                            psi_f.Reverse();
                            for (int i = 0; i < psi_f.Count;i++ )
                            {
                                if(psi_f[i]<=0)
                                    psi_f[i]+=Math.PI;
                                else
                                    psi_f[i]-=Math.PI;
                            }
                                
                            //x_enu.Reverse();
                            //y_enu.Reverse();
                        }

                     
                    }
                    else
                    {
                        if (!isReturnFromCircle)
                            currentObjective = 0;
                        else
                        {
                            PointLatLngAlt endCircle = pointListObjectives[currentObjective];
                            currentObjective = currentObjectiveOld;
                            pointListObjectives = pointListObjectivesAuxCircle;
                            cCurvature = cCurvatureAuxCircle;
                            psi_f = Psi_fAuxCircle;
                            //arcLength = new List<double>(); //
                            arcLength = arcLengthAuxCircle;
                            CreateInitialPath(psi, endCircle.Lat,endCircle.Lng, endCircle.Alt);
                            currentObjective = 0;
                            //currentArcLength = 0;
                            //ResetArc();
                        }

                    }


                }
                return true;

            }
            return false;
        }

        private bool velocityCheck(double s_dot, double dt)
        {
            double d = s_dot * dt;
            int cont = 0;
            try
            {
                while (Convert.ToDouble(getArcLength(currentObjective)) + d > Convert.ToDouble(getArcLength(currentObjective + cont)))
                {
                    if (currentObjective + ++cont >= pointListObjectives.Count)
                        return true;
                }
                currentObjective += cont;
                if (currentObjective < 0)
                    currentObjective = 0;
                if (currentObjective >= pointListObjectives.Count)
                    return true;
            }
            catch (Exception e)
            {

            }


            return false;
        }

        
        
        private double getArcLength(int CurrentObjective)
        {
            if (CurrentObjective > arcLength.Count-1)
            {
                
                //for(int i=currentArcLength;i<=CurrentObjective;i++)
                //{
                //if (CurrentObjective != 0)
                //    {
                        arcLength.Add(arcLength[CurrentObjective - 1] + mapTools.GetDistanceMeters(pointListObjectives[CurrentObjective - 1], pointListObjectives[CurrentObjective]));
                        //currentArcLength++;// = i;
                 //   }
                    
                //}      
            }
            return arcLength[CurrentObjective];
        }

        public bool CheckForPointsOfInterests(double psi, double lat, double lon, double alt)
        {
            if (checkForPointsOfInterests(lat, lon,alt) && !isCirclePath && !ignorePointsOfInterest)
            {
                currentObjectiveOld = currentObjective;
                double angle = mapTools.GetAngleMeassuredFromNord(new PointLatLngAlt(lat, lon, alt), new PointLatLngAlt(pointsOfInterests[currentPointOfInterest].Lat,pointsOfInterests[currentPointOfInterest].Lng,0));
                pathGenerator.InitialPosition = new PointLatLngAlt(pointsOfInterests[currentPointOfInterest].Lat, pointsOfInterests[currentPointOfInterest].Lng, alt);
                pathGenerator.InitialYaw = angle;
                pathGenerator.newPath();
                pathGenerator.StraightLine(pathGenerator.TurnRadius);
                List<PointLatLngAlt> line = pathGenerator.RoutePoints;


                PointLatLngAlt center = line[line.Count - 1];
                MAV3DSim.Navigation.PathGenerator3D.Direction direction = MAV3DSim.Navigation.PathGenerator3D.Direction.left;
                double initialHeading = 0;
                if (psi < 0)
                    initialHeading = angle + Math.PI / 2;
                else
                    initialHeading = angle - Math.PI / 2;

                if (initialHeading > Math.PI)
                    initialHeading -= 2 * Math.PI;
                else if (initialHeading < -Math.PI)
                    initialHeading += 2 * Math.PI;

                if (initialHeading > 0)
                {
                    if (initialHeading < Math.PI / 2)
                        direction = MAV3DSim.Navigation.PathGenerator3D.Direction.right;
                }
                else
                {
                    if (initialHeading < -Math.PI / 2)
                        direction = MAV3DSim.Navigation.PathGenerator3D.Direction.right;
                }

                pathGenerator.InitialPosition = center;
                pathGenerator.InitialYaw = initialHeading;
                pathGenerator.newPath();
                pathGenerator.Turn(direction, 2 * Math.PI);

                pointListObjectivesAuxCircle = pointListObjectives;
                cCurvatureAuxCircle = cCurvature;
                Psi_fAuxCircle = psi_f;
                arcLengthAuxCircle = arcLength;

                pointListObjectives = pathGenerator.RoutePoints;
                pointListObjectivesCircle = pathGenerator.RoutePoints;
                cCurvature = pathGenerator.cCurvature;
                psi_f = pathGenerator.Psi_f;
                //arcLength = new List<double>(); // pathGenerator.ArcLength;
                //currentArcLength = 0;
                ResetArc();


                // Turn
                pathGenerator.InitialPosition = new PointLatLngAlt(lat, lon,alt);
                pathGenerator.InitialYaw = psi;
                pathGenerator.InitialPitch = 0;
                pathGenerator.newPath();
                double turnAngle = initialHeading > 0 ? initialHeading - Math.PI : initialHeading + Math.PI;
                pathGenerator.TurnUntil(direction == MAV3DSim.Navigation.PathGenerator3D.Direction.right ? MAV3DSim.Navigation.PathGenerator3D.Direction.left : MAV3DSim.Navigation.PathGenerator3D.Direction.right, turnAngle);

                List<PointLatLngAlt> pointListObjectivesTurn = pathGenerator.RoutePoints;
                List<double> cCurvatureTurn = pathGenerator.cCurvature;
                List<double> Psi_fTurn = pathGenerator.Psi_f;
                //List<double> arcLengthTurn = new List<double>(); // pathGenerator.ArcLength;
                currentObjective = 0;
                //currentArcLength = 0;
                CreateInitialPath(Psi_fTurn[Psi_fTurn.Count - 1], pointListObjectivesTurn[pointListObjectivesTurn.Count - 1].Lat, pointListObjectivesTurn[pointListObjectivesTurn.Count - 1].Lng, pointListObjectivesTurn[pointListObjectivesTurn.Count - 1].Alt);
                pointListObjectives.InsertRange(0, pointListObjectivesTurn);
                cCurvature.InsertRange(0, cCurvatureTurn);
                psi_f.InsertRange(0, Psi_fTurn);
                //arcLength.InsertRange(0, arcLengthTurn);
                //arcLength = new List<double>();
                ResetArc();
                isCirclePath = true;

                return true;
            }
            return false;
        }

        private bool checkForPointsOfInterests(double lat, double lon,double alt)
        {
            
            for(int i=0;i<pointsOfInterests.Count;i++)
            {
                if(mapTools.IsInsideCircle(new PointLatLngAlt(pointsOfInterests[i].Lat,pointsOfInterests[i].Lng,0), new PointLatLngAlt(lat, lon,alt), pathGenerator.TurnRadius))
                {
                    currentPointOfInterest = i;
                    return true;
                }
            }
            currentPointOfInterest = -1;
            return false;
        }

        public void GenerateSearchPath(GMapPolygon Rectangle, PointLatLngAlt PosInitial)
        {
            double d = 10000000;
            PointLatLngAlt startingPoint = new PointLatLngAlt();

            List<PointLatLngAlt> points = new List<PointLatLngAlt>();
            points.Add(new PointLatLngAlt(Rectangle.Points[0].Lat, Rectangle.Points[0].Lng, PosInitial.Alt));
            points.Add(new PointLatLngAlt(Rectangle.Points[1].Lat, Rectangle.Points[1].Lng, PosInitial.Alt));
            points.Add(new PointLatLngAlt(Rectangle.Points[2].Lat, Rectangle.Points[2].Lng, PosInitial.Alt));
            points.Add(new PointLatLngAlt(Rectangle.Points[3].Lat, Rectangle.Points[3].Lng, PosInitial.Alt));

            foreach (PointLatLngAlt p in points)
            {
                double d1 = mapTools.GetDistance(PosInitial, p);
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
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, 1, -turnRadius,0);
                    pathGenerator.InitialYaw = Math.PI / 2; ;
                    pathGenerator.InitialPitch = 0;
                    right = true;
                }

                if (startingPoint.Equals(Rectangle.Points[1]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -1, -turnRadius,0);
                    pathGenerator.InitialYaw = -Math.PI / 2;
                    pathGenerator.InitialPitch = 0;
                    right = false;
                }

                if (startingPoint.Equals(Rectangle.Points[2]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -1, turnRadius,0);
                    pathGenerator.InitialYaw = -Math.PI / 2;
                    pathGenerator.InitialPitch = 0;
                    right = true;
                }

                if (startingPoint.Equals(Rectangle.Points[3]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, 1, turnRadius,0);
                    pathGenerator.InitialYaw = Math.PI / 2;
                    pathGenerator.InitialPitch = 0;
                    right = false;
                }
            }
            else
            {
                distance = distance2;
                if (startingPoint.Equals(Rectangle.Points[0]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, turnRadius, 1,0); ;
                    pathGenerator.InitialYaw = Math.PI;
                    pathGenerator.InitialPitch = 0;
                    right = false;
                }

                if (startingPoint.Equals(Rectangle.Points[1]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -turnRadius, 1,0);
                    pathGenerator.InitialYaw = Math.PI;
                    pathGenerator.InitialPitch = 0;
                    right = true;

                }

                if (startingPoint.Equals(Rectangle.Points[2]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -turnRadius, -1,0); ;
                    pathGenerator.InitialYaw = 0;
                    pathGenerator.InitialPitch = 0;
                    right = false;

                }

                if (startingPoint.Equals(Rectangle.Points[3]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, turnRadius, -1,0); ;
                    pathGenerator.InitialPitch = 0;
                    pathGenerator.InitialYaw = 0;
                    right = true;

                }
            }

            pathGenerator.newPath();
            pathGenerator.TurnRadius = turnRadius;
            pathGenerator.StraightLine(pathGenerator.TurnRadius);
            while (mapTools.IsInside(pathGenerator.InitialPosition.GetPointLatLng(), Rectangle.Points))
            {
                if (right)
                    pathGenerator.InitialPitch = 0.26;
                else
                    pathGenerator.InitialPitch = -0.26;
                pathGenerator.StraightLine(distance * 1000 - 2 * pathGenerator.TurnRadius);
                pathGenerator.InitialPitch = 0;
                pathGenerator.Turn(right ? Navigation.PathGenerator3D.Direction.right : Navigation.PathGenerator3D.Direction.left, Math.PI);
                right = !right;
            }

            pointListObjectives = pathGenerator.RoutePoints;
            cCurvature = pathGenerator.cCurvature;     // Curvatura de curva
            //arcLength = new List<double>(); // pathGenerator.ArcLength;      // Arc-length
            //currentArcLength = 0;
            ResetArc();
            psi_f = pathGenerator.Psi_f;  // Angulo tangencial con respecto a la normal.

            pointListObjectives_o = pathGenerator.RoutePoints;
            cCurvature_o = pathGenerator.cCurvature;     // Curvatura de curva
            //arcLength_o = pathGenerator.ArcLength;      // Arc-length
            psi_f_o = pathGenerator.Psi_f;  // Angulo tangencial con respecto a la normal.

            //((Map)parent.GetDocks[typeof(Map).ToString()]).AddRoute(1, pathGenerator.Route, Color.Black);
        }

        public void GenerateSearchPathPolygon(GMapPolygon Polygon, PointLatLngAlt PosInitial)
        {
            double d = 10000000;
            PointLatLngAlt startingPoint = new PointLatLngAlt();

            rectangle = new List<PointLatLngAlt>();
            double maxLat = -1000;
            double minLat = 1000;
            double maxLon = -1000;
            double minLon = 1000;
            double pitchAngle = .13;
            for (int i = 0; i < Polygon.Points.Count;i++ )
            {
                maxLat = Polygon.Points[i].Lat > maxLat ? Polygon.Points[i].Lat : maxLat;
                maxLon = Polygon.Points[i].Lng > maxLon ? Polygon.Points[i].Lng : maxLon;
                minLat = Polygon.Points[i].Lat < minLat ? Polygon.Points[i].Lat : minLat;
                minLon = Polygon.Points[i].Lng < minLon ? Polygon.Points[i].Lng : minLon;
                
            }

            rectangle.Add(new PointLatLngAlt(maxLat, maxLon,100));
            rectangle.Add(new PointLatLngAlt(maxLat, minLon,100));
            rectangle.Add(new PointLatLngAlt(minLat, minLon,100));
            rectangle.Add(new PointLatLngAlt(minLat, maxLon,100));

            rectangle.Sort((x, y) => x.Lng.CompareTo(y.Lng));
            rectangle.Sort((x, y) => x.Lat.CompareTo(y.Lat));

            PointLatLngAlt p_aux = Rectangle[2];
            rectangle[2] = Rectangle[3];
            rectangle[3] = p_aux;

            rectangle.Reverse();

            foreach (PointLatLngAlt p in rectangle)
            {
                double d1 = mapTools.GetDistance(PosInitial, p);
                if (d1 < d)
                {
                    d = d1;
                    startingPoint = p;
                }
            }


            double distance = 0;
            double distance1 = mapTools.GetDistance(Rectangle[0], Rectangle[1]);
            double distance2 = mapTools.GetDistance(Rectangle[0], Rectangle[3]);

            bool right = true;
            if (distance1 > distance2)
            {

                distance = distance1;

                if (startingPoint.Equals(Rectangle[0]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, 1, -turnRadius,0);
                    pathGenerator.InitialYaw = Math.PI / 2; ;
                    pathGenerator.InitialPitch = 0;
                    right = true;
                }

                if (startingPoint.Equals(Rectangle[1]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -1, -turnRadius,0);
                    pathGenerator.InitialYaw = -Math.PI / 2;
                    pathGenerator.InitialPitch = 0;
                    right = false;
                }

                if (startingPoint.Equals(Rectangle[2]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -1, turnRadius,0);
                    pathGenerator.InitialYaw = -Math.PI / 2; ;
                    pathGenerator.InitialPitch = 0;
                    right = true;
                }

                if (startingPoint.Equals(Rectangle[3]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, 1, turnRadius,0);
                    pathGenerator.InitialYaw = Math.PI / 2;
                    pathGenerator.InitialPitch = 0;
                    right = false;
                }
            }
            else
            {
                distance = distance2;
                if (startingPoint.Equals(Rectangle[0]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, turnRadius, 1,0); ;
                    pathGenerator.InitialYaw = Math.PI;
                    pathGenerator.InitialPitch = 0;
                    right = false;
                }

                if (startingPoint.Equals(Rectangle[1]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -turnRadius, 1,0);
                    pathGenerator.InitialYaw = Math.PI;
                    pathGenerator.InitialPitch = 0;
                    right = true;

                }

                if (startingPoint.Equals(Rectangle[2]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -turnRadius, -1,0);
                    pathGenerator.InitialYaw = 0;
                    pathGenerator.InitialPitch = 0;
                    right = false;

                }

                if (startingPoint.Equals(Rectangle[3]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, turnRadius, -1,0);
                    pathGenerator.InitialYaw = 0;
                    pathGenerator.InitialPitch = 0;
                    right = true;

                }
            }




            pathGenerator.newPath();
            pathGenerator.TurnRadius = turnRadius;
            pathGenerator.StraightLine(pathGenerator.TurnRadius);


            while (!mapTools.IsInside(pathGenerator.InitialPosition.GetPointLatLng(), Polygon.Points))
            {
                pathGenerator.StraightLine(pathGenerator.TurnRadius);
            }

            pathGenerator.newPath();

            while (mapTools.IsInside(pathGenerator.InitialPosition, Rectangle))
            {
                if (right)
                    pathGenerator.InitialPitch = pitchAngle;
                else
                    pathGenerator.InitialPitch = pitchAngle;
                while (mapTools.IsInside(pathGenerator.InitialPosition.GetPointLatLng(), Polygon.Points))
                {
                    
                    pathGenerator.StraightLine(pathGenerator.TurnRadius);
                    
                }
                
                //pathGenerator.StraightLine(distance * 1000 - 2 * pathGenerator.TurnRadius);
                
                pathGenerator.Turn(right ? Navigation.PathGenerator3D.Direction.right : Navigation.PathGenerator3D.Direction.left, Math.PI);

                right = !right;
                if (right)
                    pathGenerator.InitialPitch = pitchAngle;
                else
                    pathGenerator.InitialPitch = pitchAngle;
                pathGenerator.StraightLine(pathGenerator.TurnRadius);
                while (!mapTools.IsInside(pathGenerator.InitialPosition.GetPointLatLng(), Polygon.Points) && mapTools.IsInside(pathGenerator.InitialPosition, Rectangle))
                {
                    pathGenerator.StraightLine(pathGenerator.TurnRadius);
                }
            }

            pointListObjectives = pathGenerator.RoutePoints;
            cCurvature = pathGenerator.cCurvature;     // Curvatura de curva
            //arcLength = new List<double>(); // pathGenerator.ArcLength;      // Arc-length
            //currentArcLength = 0;
            ResetArc();
            psi_f = pathGenerator.Psi_f;  // Angulo tangencial con respecto a la normal.

            pointListObjectives_o = pathGenerator.RoutePoints;
            cCurvature_o = pathGenerator.cCurvature;     // Curvatura de curva
            //arcLength_o = pathGenerator.ArcLength;      // Arc-length
            psi_f_o = pathGenerator.Psi_f;  // Angulo tangencial con respecto a la normal.

            //((Map)parent.GetDocks[typeof(Map).ToString()]).AddRoute(1, pathGenerator.Route, Color.Black);
        }

        public void ClearWaypoints()
        {
            waypoints = new List<PointLatLngAlt>();
        }

        public void NewWaypoint(PointLatLngAlt waypoint)
        {
            waypoints.Add(waypoint);
        }
        public void GenerateWaypointsPath()
        {
            pathGenerator.newPath();
            pathGenerator.InitialPosition = waypoints[0];
            pathGenerator.InitialYaw = mapTools.GetSlopeFromNord(waypoints[0].GetPointLatLng(), waypoints[1].GetPointLatLng());
            pathGenerator.TurnRadius = turnRadius;
            pathGenerator.InitialPitch = pathGenerator.FindPitch(waypoints[0], waypoints[1], pathGenerator.InitialYaw, pathGenerator.InitialYaw, pathGenerator.InitialYaw,mapTools.GetDistanceMeters(waypoints[0], waypoints[1]));
            pathGenerator.StraightLine(mapTools.GetDistanceMeters(waypoints[0], waypoints[1]));
            for (int i = 1; i < waypoints.Count-1;i++ )
                pathGenerator.GeneratePath(waypoints[i+1], mapTools.GetSlopeFromNord(waypoints[i].GetPointLatLng(), waypoints[i+1].GetPointLatLng()));
            pointListObjectives = pathGenerator.RoutePoints;
            cCurvature = pathGenerator.cCurvature;     // Curvatura de curva
            //arcLength = new List<double>(); // pathGenerator.ArcLength;      // Arc-length
            //currentArcLength = 0;
            ResetArc();
            psi_f = pathGenerator.Psi_f;  // Angulo tangencial con respecto a la normal.

            pointListObjectives_o = pathGenerator.RoutePoints;
            cCurvature_o = pathGenerator.cCurvature;     // Curvatura de curva
            //arcLength_o = pathGenerator.ArcLength;      // Arc-length
            psi_f_o = pathGenerator.Psi_f;  // Angulo tangencial con respecto a la normal.
        }

        public void GenerateLandingPath(double Heading, double Lat, double Lon, double Alt)
        {
            pathGenerator.newPath();
            pathGenerator.InitialPosition = pointListObjectivesLanding[0];
            pathGenerator.InitialYaw = mapTools.GetSlopeFromNord(pointListObjectivesLanding[0].GetPointLatLng(), pointListObjectivesLanding[1].GetPointLatLng());
            pathGenerator.TurnRadius = turnRadius;
            pathGenerator.StraightLine(mapTools.GetDistanceMeters(pointListObjectivesLanding[0], pointListObjectivesLanding[1]));
            pointListObjectives = pathGenerator.RoutePoints;
            cCurvature = pathGenerator.cCurvature;
            psi_f = pathGenerator.Psi_f;
            ResetArc();
            currentObjective = 0;
            CreateInitialPath(Heading, Lat, Lon,Alt);
       } 

        private void updatePointListObjectives()
        {
            _pointListObjectives = new List<PointLatLng>();
            for(int i =0; i<pointListObjectives.Count-1;i++)
                _pointListObjectives.Add(pointListObjectives[i].GetPointLatLng());
        }

        private void updatepointListObjectivesCircle()
        {
            _pointListObjectivesCircle = new List<PointLatLng>();
            for (int i = 0; i < pointListObjectivesCircle.Count - 1; i++)
                _pointListObjectivesCircle.Add(pointListObjectivesCircle[i].GetPointLatLng());
        }

        public List<PointLatLngAlt> PointListObjectives
        {
            get { return pointListObjectives; }
            set { pointListObjectives = value; pointListObjectives_o = value; }
        }
        public List<PointLatLng> PointListObjectivesLatLng
        {
            get { updatePointListObjectives();  return _pointListObjectives; }
            set { _pointListObjectives = value; }
        }
        public List<PointLatLng> PointsOfInterests
        {
            get { return pointsOfInterests; }
            set { pointsOfInterests = value; }
        }
        public List<PointLatLngAlt> PointListObjectivesLanding
        {
            get { return pointListObjectivesLanding; }
            set { pointListObjectivesLanding = value; }
        }
        public List<PointLatLngAlt> CirclePath
        {
            get { return pointListObjectivesCircle; }
            set { pointListObjectivesCircle = value; }
        }
        public List<PointLatLng> CirclePathatLng
        {
            get { updatepointListObjectivesCircle();  return _pointListObjectivesCircle; }
            set { _pointListObjectivesCircle = value; }
        }
        public List<PointLatLngAlt> Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public  List<double> CCurvature
        {
            get { return cCurvature; }
            set { cCurvature = value; cCurvature_o = value; }
        }

        //public List<double> ArcLength
        //{
        //    get { return arcLength; }
        //    set { arcLength = value; arcLength_o = value; }
        //}

        public List<double> Psi_F
        {
            get { return psi_f; }
            set { psi_f = value; psi_f_o = value; }
        }

        public double TurnRadius
        {
            set { turnRadius = value; pathGenerator.TurnRadius = value; }
        }

        public PointLatLngAlt InitialEnuPosition
        {
            set { initialEnuPosition = value; }
        }

        public PointLatLngAlt CurrentObjective
        {
            get { return pointListObjectives[currentObjective]; }
        }
        public double CurrentPsi_F
        {
            get { return psi_f[currentObjective]; }
        }
        public double CurrentCurvature
        {
            get { return cCurvature[currentObjective]; }
        }
        public bool IsTurn180
        {
            get { return isTurn180; }
            set { isTurn180 = value; }
        }

        public bool IsCirclePath
        {
            get { return isCirclePath; }
            set { isCirclePath = value; }
        }

        public bool IgnorePointsOfInterest
        {
            get { return ignorePointsOfInterest; }
            set { ignorePointsOfInterest = value; }
        }

        public bool IsReturnFromCircle
        {
            get { return isReturnFromCircle; }
            set { isReturnFromCircle = value; }
        }

    }
}
