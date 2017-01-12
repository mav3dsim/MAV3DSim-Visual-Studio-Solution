using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAV3DSim.Navigation
{
    
    public class Navigator
    {
        
        PathGenerator pathGenerator;
        List<PointLatLng> pointList;
        List<PointLatLng> pointListObjectives;
        List<PointLatLng> pointListObjectivesCircle;
        List<PointLatLng> pointListObjectives_o;
        List<PointLatLng> pointListObjectivesAux;
        List<PointLatLng> pointListObjectivesAuxCircle;
        List<PointLatLng> pointsOfInterests;
        List<PointLatLng> rectangle;
        List<PointLatLng> pointListObjectivesLanding;
        List<PointLatLng> waypoints;

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
        PointLatLng initialEnuPosition = new PointLatLng();

        bool isInitialPath = false;
        bool isTurn180 = false;
        bool isReturnFromCircle = false;
        bool isCirclePath = false;
        bool ignorePointsOfInterest = false;

        public Navigator()
        {
            pathGenerator = new PathGenerator();
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
        public void CreateInitialPath(double InitialHeading, double Lat, double Lon)
        {
            isInitialPath = true;
            isTurn180 = false;
            pathGenerator.InitialHeading = InitialHeading;
            pathGenerator.InitialPosition = new PointLatLng(Lat, Lon);
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


        public bool ProximityCheck(double L1, double Lat, double Lon, double psi)
        {
            if(proximityCheck(L1,Lat,Lon))
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
                        pathGenerator.InitialPosition = new PointLatLng(pointListObjectives[pointListObjectives.Count - 1].Lat, pointListObjectives[pointListObjectives.Count - 1].Lng);
                        pathGenerator.InitialHeading = psi;
                        pathGenerator.InitialEnuPosition = initialEnuPosition;
                        pathGenerator.newPath();
                        pathGenerator.TurnRadius = turnRadius;
                        pathGenerator.Turn180(new PointLatLng(pointListObjectives[pointListObjectives.Count - 1].Lat, pointListObjectives[pointListObjectives.Count - 1].Lng), psi);
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

        private bool proximityCheck(double L1, double lat, double lon)
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
                dist = mapTools.GetDistanceMeters(new PointLatLng(lat, lon), pointListObjectives[currentObjective]);
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
                    //    pathGenerator.InitialHeading = imu.psi;
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
                            pathGenerator.InitialPosition = new PointLatLng(pointListObjectives[pointListObjectives.Count - 1].Lat, pointListObjectives[pointListObjectives.Count - 1].Lng);
                            pathGenerator.InitialHeading = psi;
                            pathGenerator.InitialEnuPosition = initialEnuPosition;
                            pathGenerator.newPath();
                            pathGenerator.TurnRadius = turnRadius;
                            pathGenerator.Turn180(new PointLatLng(pointListObjectives[pointListObjectives.Count - 1].Lat, pointListObjectives[pointListObjectives.Count - 1].Lng), psi);
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
                            PointLatLng endCircle = pointListObjectives[currentObjective];
                            currentObjective = currentObjectiveOld;
                            pointListObjectives = pointListObjectivesAuxCircle;
                            cCurvature = cCurvatureAuxCircle;
                            psi_f = Psi_fAuxCircle;
                            //arcLength = new List<double>(); //
                            arcLength = arcLengthAuxCircle;
                            CreateInitialPath(psi, endCircle.Lat,endCircle.Lng);
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
            double arcl1 = 0;
            double arcl2 = 0;
            try
            {
                arcl1 = Convert.ToDouble(getArcLength(currentObjective));
                arcl2 = Convert.ToDouble(getArcLength(currentObjective + cont)); 

                while (arcl1 + d > arcl2)
                {
                    if (currentObjective + ++cont >= pointListObjectives.Count)
                        return true;
                    arcl1 = Convert.ToDouble(getArcLength(currentObjective));
                    arcl2 = Convert.ToDouble(getArcLength(currentObjective + cont));
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

        public bool CheckForPointsOfInterests(double psi, double lat, double lon)
        {
            if (checkForPointsOfInterests(lat, lon) && !isCirclePath && !ignorePointsOfInterest)
            {
                currentObjectiveOld = currentObjective;
                double angle = mapTools.GetAngleMeassuredFromNord(new PointLatLng(lat, lon), pointsOfInterests[currentPointOfInterest]);
                pathGenerator.InitialPosition = pointsOfInterests[currentPointOfInterest];
                pathGenerator.InitialHeading = angle;
                pathGenerator.newPath();
                pathGenerator.StraightLine(pathGenerator.TurnRadius);
                List<PointLatLng> line = pathGenerator.RoutePoints;


                PointLatLng center = line[line.Count - 1];
                MAV3DSim.Navigation.PathGenerator.Direction direction = MAV3DSim.Navigation.PathGenerator.Direction.left;
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
                        direction = MAV3DSim.Navigation.PathGenerator.Direction.right;
                }
                else
                {
                    if (initialHeading < -Math.PI / 2)
                        direction = MAV3DSim.Navigation.PathGenerator.Direction.right;
                }

                pathGenerator.InitialPosition = center;
                pathGenerator.InitialHeading = initialHeading;
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
                pathGenerator.InitialPosition = new PointLatLng(lat, lon);
                pathGenerator.InitialHeading = psi;
                pathGenerator.newPath();
                double turnAngle = initialHeading > 0 ? initialHeading - Math.PI : initialHeading + Math.PI;
                pathGenerator.TurnUntil(direction == MAV3DSim.Navigation.PathGenerator.Direction.right ? MAV3DSim.Navigation.PathGenerator.Direction.left : MAV3DSim.Navigation.PathGenerator.Direction.right, turnAngle);

                List<PointLatLng> pointListObjectivesTurn = pathGenerator.RoutePoints;
                List<double> cCurvatureTurn = pathGenerator.cCurvature;
                List<double> Psi_fTurn = pathGenerator.Psi_f;
                //List<double> arcLengthTurn = new List<double>(); // pathGenerator.ArcLength;
                currentObjective = 0;
                //currentArcLength = 0;
                CreateInitialPath(Psi_fTurn[Psi_fTurn.Count - 1], pointListObjectivesTurn[pointListObjectivesTurn.Count - 1].Lat, pointListObjectivesTurn[pointListObjectivesTurn.Count - 1].Lng);
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

        private bool checkForPointsOfInterests(double lat, double lon)
        {
            
            for(int i=0;i<pointsOfInterests.Count;i++)
            {
                if(mapTools.IsInsideCircle(pointsOfInterests[i], new PointLatLng(lat, lon), pathGenerator.TurnRadius))
                {
                    currentPointOfInterest = i;
                    return true;
                }
            }
            currentPointOfInterest = -1;
            return false;
            

        }

        public void GenerateSearchPath(GMapPolygon Rectangle, PointLatLng PosInitial)
        {
            double d = 10000000;
            PointLatLng startingPoint = new PointLatLng();

            

            foreach (PointLatLng p in Rectangle.Points)
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
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, 1, -turnRadius);
                    pathGenerator.InitialHeading = Math.PI / 2; ;
                    right = true;
                }

                if (startingPoint.Equals(Rectangle.Points[1]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -1, -turnRadius);
                    pathGenerator.InitialHeading = -Math.PI / 2; ;
                    right = false;
                }

                if (startingPoint.Equals(Rectangle.Points[2]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -1, turnRadius);
                    pathGenerator.InitialHeading = -Math.PI / 2; ;
                    right = true;
                }

                if (startingPoint.Equals(Rectangle.Points[3]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, 1, turnRadius);
                    pathGenerator.InitialHeading = Math.PI / 2; ;
                    right = false;
                }
            }
            else
            {
                distance = distance2;
                if (startingPoint.Equals(Rectangle.Points[0]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, turnRadius, 1); ;
                    pathGenerator.InitialHeading = Math.PI; ;
                    right = false;
                }

                if (startingPoint.Equals(Rectangle.Points[1]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -turnRadius, 1); ;
                    pathGenerator.InitialHeading = Math.PI;
                    right = true;

                }

                if (startingPoint.Equals(Rectangle.Points[2]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -turnRadius, -1); ;
                    pathGenerator.InitialHeading = 0;
                    right = false;

                }

                if (startingPoint.Equals(Rectangle.Points[3]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, turnRadius, -1); ;
                    pathGenerator.InitialHeading = 0;
                    right = true;

                }
            }

            pathGenerator.newPath();
            pathGenerator.TurnRadius = turnRadius;
            pathGenerator.StraightLine(pathGenerator.TurnRadius);
            while (mapTools.IsInside(pathGenerator.InitialPosition, Rectangle.Points))
            {
                pathGenerator.StraightLine(distance * 1000 - 2 * pathGenerator.TurnRadius);
                pathGenerator.Turn(right ? Navigation.PathGenerator.Direction.right : Navigation.PathGenerator.Direction.left, Math.PI);
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

        public void GenerateSearchPathPlygon(GMapPolygon Polygon, PointLatLng PosInitial)
        {
            double d = 10000000;
            PointLatLng startingPoint = new PointLatLng();

            rectangle = new List<PointLatLng>();
            double maxLat = -1000;
            double minLat = 1000;
            double maxLon = -1000;
            double minLon = 1000;
            for (int i = 0; i < Polygon.Points.Count;i++ )
            {
                maxLat = Polygon.Points[i].Lat > maxLat ? Polygon.Points[i].Lat : maxLat;
                maxLon = Polygon.Points[i].Lng > maxLon ? Polygon.Points[i].Lng : maxLon;
                minLat = Polygon.Points[i].Lat < minLat ? Polygon.Points[i].Lat : minLat;
                minLon = Polygon.Points[i].Lng < minLon ? Polygon.Points[i].Lng : minLon;
                
            }

            rectangle.Add(new PointLatLng(maxLat, maxLon));
            rectangle.Add(new PointLatLng(maxLat, minLon));
            rectangle.Add(new PointLatLng(minLat, minLon));
            rectangle.Add(new PointLatLng(minLat, maxLon));

            rectangle.Sort((x, y) => x.Lng.CompareTo(y.Lng));
            rectangle.Sort((x, y) => x.Lat.CompareTo(y.Lat));

            PointLatLng p_aux = Rectangle[2];
            rectangle[2] = Rectangle[3];
            rectangle[3] = p_aux;

            rectangle.Reverse();

            foreach (PointLatLng p in Rectangle)
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
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, 1, -turnRadius);
                    pathGenerator.InitialHeading = Math.PI / 2; ;
                    right = true;
                }

                if (startingPoint.Equals(Rectangle[1]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -1, -turnRadius);
                    pathGenerator.InitialHeading = -Math.PI / 2; ;
                    right = false;
                }

                if (startingPoint.Equals(Rectangle[2]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -1, turnRadius);
                    pathGenerator.InitialHeading = -Math.PI / 2; ;
                    right = true;
                }

                if (startingPoint.Equals(Rectangle[3]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, 1, turnRadius);
                    pathGenerator.InitialHeading = Math.PI / 2; ;
                    right = false;
                }
            }
            else
            {
                distance = distance2;
                if (startingPoint.Equals(Rectangle[0]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, turnRadius, 1); ;
                    pathGenerator.InitialHeading = Math.PI; ;
                    right = false;
                }

                if (startingPoint.Equals(Rectangle[1]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -turnRadius, 1); ;
                    pathGenerator.InitialHeading = Math.PI;
                    right = true;

                }

                if (startingPoint.Equals(Rectangle[2]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, -turnRadius, -1); ;
                    pathGenerator.InitialHeading = 0;
                    right = false;

                }

                if (startingPoint.Equals(Rectangle[3]))
                {
                    pathGenerator.InitialPosition = mapTools.OffsetInMeters(startingPoint, turnRadius, -1); ;
                    pathGenerator.InitialHeading = 0;
                    right = true;

                }
            }




            pathGenerator.newPath();
            pathGenerator.TurnRadius = turnRadius;
            pathGenerator.StraightLine(pathGenerator.TurnRadius);


            while (!mapTools.IsInside(pathGenerator.InitialPosition, Polygon.Points))
            {
                pathGenerator.StraightLine(pathGenerator.TurnRadius);
            }

            pathGenerator.newPath();

            while (mapTools.IsInside(pathGenerator.InitialPosition, Rectangle))
            {
                while (mapTools.IsInside(pathGenerator.InitialPosition, Polygon.Points))
                {
                    pathGenerator.StraightLine(pathGenerator.TurnRadius);
                }
                
                //pathGenerator.StraightLine(distance * 1000 - 2 * pathGenerator.TurnRadius);
                pathGenerator.Turn(right ? Navigation.PathGenerator.Direction.right : Navigation.PathGenerator.Direction.left, Math.PI);
                pathGenerator.StraightLine(pathGenerator.TurnRadius);
                right = !right;
                while (!mapTools.IsInside(pathGenerator.InitialPosition, Polygon.Points) && mapTools.IsInside(pathGenerator.InitialPosition, Rectangle))
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

        public void GenerateLandingPath(double Heading, double Lat, double Lon)
        {
            pathGenerator.newPath();
            pathGenerator.InitialPosition = pointListObjectivesLanding[0];
            pathGenerator.InitialHeading = mapTools.GetSlopeFromNord(pointListObjectivesLanding[0], pointListObjectivesLanding[1]);
            pathGenerator.TurnRadius = turnRadius;
            pathGenerator.StraightLine(mapTools.GetDistanceMeters(pointListObjectivesLanding[0], pointListObjectivesLanding[1]));
            pointListObjectives = pathGenerator.RoutePoints;
            cCurvature = pathGenerator.cCurvature;
            psi_f = pathGenerator.Psi_f;
            ResetArc();
            currentObjective = 0;
            CreateInitialPath(Heading, Lat, Lon);
        }

        public void ClearWaypoints()
        {
            waypoints = new List<PointLatLng>();
        }

        public void NewWaypoint(PointLatLng waypoint)
        {
            waypoints.Add(waypoint);
        }
        public void GenerateWaypointsPath()
        {
            pathGenerator.newPath();
            pathGenerator.InitialPosition = waypoints[0];
            pathGenerator.InitialHeading = mapTools.GetSlopeFromNord(waypoints[0], waypoints[1]);
            pathGenerator.TurnRadius = turnRadius;
            pathGenerator.StraightLine(mapTools.GetDistanceMeters(waypoints[0], waypoints[1]));
            for (int i = 1; i <  waypoints.Count-1; i++)
                pathGenerator.GeneratePath(waypoints[i + 1], mapTools.GetSlopeFromNord(waypoints[i], waypoints[i + 1]));
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

        public List<PointLatLng> PointListObjectives
        {
            get { return pointListObjectives; }
            set { pointListObjectives = value; pointListObjectives_o = value; }
        }
        public List<PointLatLng> PointsOfInterests
        {
            get { return pointsOfInterests; }
            set { pointsOfInterests = value; }
        }
        public List<PointLatLng> PointListObjectivesLanding
        {
            get { return pointListObjectivesLanding; }
            set { pointListObjectivesLanding = value; }
        }
        public List<PointLatLng> CirclePath
        {
            get { return pointListObjectivesCircle; }
            set { pointListObjectivesCircle = value; }
        }
        public List<PointLatLng> Rectangle
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

        public PointLatLng InitialEnuPosition
        {
            set { initialEnuPosition = value; }
        }

        public PointLatLng CurrentObjective
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
