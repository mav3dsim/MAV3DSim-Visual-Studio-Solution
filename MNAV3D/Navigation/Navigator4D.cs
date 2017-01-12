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
    
    public class Navigator4D
    {
        PathGenerator4D pathGenerator;
        
        Path4D currentPath;
        Path4D originalPath;
        Path4D auxPath;
        Path4D auxCirclePath;

        List<PointLatLng> pointsOfInterests;
        List<PointLatLngAlt> rectangle;
        List<PointLatLngAlt> pointListObjectivesLanding;

        List<PointLatLngAlt> waypoints;
        List<double> arcLength = new List<double>();      // Arc-length

        int currentObjective = 0;
        int currentObjectiveOld = 0;
        int currentPointOfInterest = 0;
        int landingPoint = 0;
        //int currentArcLength = 0;

        Utils.MapTools mapTools = new Utils.MapTools();

        double turnRadius = 0;
        PointLatLngAlt initialEnuPosition = new PointLatLngAlt();

        bool isInitialPath = false;
        bool isTurn180 = false;
        bool isReturnFromCircle = false;
        bool isCirclePath = false;
        bool ignorePointsOfInterest = false;
        bool isTakeoff = false;
        bool isLanding = false;
        

        Log log = new Log();

        public Navigator4D()
        {
            pathGenerator = new PathGenerator4D();
            pointsOfInterests = new List<PointLatLng>();
        }

        public void RestoreOriginalPath()
        {
            currentPath = originalPath;

            currentObjective = 0;
            isTurn180 = false;
            isInitialPath = false;
            isReturnFromCircle = false;
            isCirclePath = false;
            ignorePointsOfInterest = false;
            
            

        }

        public void setPath(Path4D path)
        {
            currentPath = path;
            originalPath = path;

            currentObjective = 0;
            isTurn180 = false;
            isInitialPath = false;
            isReturnFromCircle = false;
            isCirclePath = false;
            ignorePointsOfInterest = false;
            ResetArc();
        }

        private void ResetArc()
        {
            arcLength = new List<double>(); // arcLength_o;
            arcLength.Add(0);
            //currentArcLength = 0;
        }

        public void CreateInitialPath(PointLatLngAlt InitialPosition, double InitialYaw, double Velocity)
        {
            CreateInitialPath(InitialYaw, InitialPosition.Lat, InitialPosition.Lng, InitialPosition.Alt, Velocity);
        }
        public void CreateInitialPath(double InitialYaw, PointLatLngAlt Point, double Velocity)
        {
            CreateInitialPath(InitialYaw, Point.Lat, Point.Lng, Point.Alt, Velocity);
        }
        public void CreateInitialPath(double InitialYaw, double Lat, double Lon, double Alt, double Velocity)
        {
            isInitialPath = true;
            isTurn180 = false;
            pathGenerator.InitialYaw = InitialYaw;
            pathGenerator.InitialPosition = new PointLatLngAlt(Lat, Lon,Alt);
            pathGenerator.InitialEnuPosition = initialEnuPosition;
            pathGenerator.InitialPitch = 0;
            pathGenerator.newPath();
            pathGenerator.CurrentVelocity = Velocity;
            pathGenerator.StraightLine(pathGenerator.TurnRadius * 1.5);

            // Generate path to initial point of the rectangle generated path
            
            pathGenerator.GeneratePath(currentPath.PointList[currentObjective], currentPath.Psi_f[currentObjective]);

            // Initial path
            auxPath = currentPath;

            currentPath = pathGenerator.Path;
            ResetArc();

        }
        public bool ProximityCheck(double L1, double Lat, double Lon, double Alt, double psi)
        {
            if(proximityCheck(L1,Lat,Lon,Alt))
            {
                if (isInitialPath)
                {
                    isInitialPath = false;
                    currentObjective = 0;
                    currentPath = auxPath;
                    ResetArc();
                    
                }
                else
                {


                    isTurn180 = !isTurn180;

                    if (isTurn180)
                    {
                        currentObjective = 0;
                        pathGenerator.InitialPosition = currentPath.LastPoint;
                        pathGenerator.InitialYaw = psi;
                        pathGenerator.InitialPitch = 0; // just turn, do not change altitude
                        pathGenerator.InitialEnuPosition = initialEnuPosition;
                        pathGenerator.newPath();
                        pathGenerator.TurnRadius = turnRadius;
                        pathGenerator.Turn180(currentPath.LastPoint, psi);

                        auxPath = currentPath;

                        currentPath = pathGenerator.Path;
                        ResetArc();
                        
                    }
                    else
                    {
                        currentObjective = 0;
                        currentPath = auxPath;
                        ResetArc();
                        currentPath.Reverse();
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
                    if (currentObjective >= currentPath.Count)
                        return true;  //currentObjective = 0;  
                }
                dist = mapTools.GetDistanceMeters(new PointLatLngAlt(lat, lon,alt), currentPath.PointList[currentObjective]);
                first = false;
            };
            return false;
        }
        public bool VelocityCheck(double s_dot, double dt, double psi)
        {
            if (currentObjective > landingPoint && landingPoint !=0)
                isLanding = true;
            if (velocityCheck(s_dot, dt)) // if true means that it reach the last point.
            {
                if (isLanding)
                    return true;
                if(isInitialPath)
                {
                    isInitialPath = false;
                    currentPath = auxPath;
                    if (!isReturnFromCircle)
                        currentObjective = 0;
                    else
                    {
                        currentObjective = currentObjectiveOld;
                        isReturnFromCircle = false;
                        isCirclePath = false;
                    }
                }
                else if (isCirclePath)
                {
                    if (!isReturnFromCircle)
                        currentObjective = 0;
                    else
                    {
                        PointLatLngAlt endCircle = currentPath.PointList[currentObjective];
                        currentObjective = currentObjectiveOld;
                        currentPath = auxCirclePath;
                        CreateInitialPath(psi, endCircle.Lat, endCircle.Lng, endCircle.Alt, 215); // FIX ME hardcode velocity
                        currentObjective = 0;
                    }

                }
                else if(isTakeoff)
                {
                    isTakeoff = false;
                    PointLatLngAlt endTakeoff = currentPath.PointList[currentObjective];
                    currentObjective = 0;
                    currentPath = auxPath;
                    ResetArc();

                    CreateInitialPath(endTakeoff, psi, 215); // FIX ME hardcode velocity

                }
                else // End of waypoints turn 180 and continue with the waypoints.
                {
                    isTurn180 = !isTurn180;

                    if (isTurn180)
                    {
                        currentObjective = 0;
                        pathGenerator.InitialPosition = currentPath.LastPoint; // pointListObjectives[pointListObjectives.Count - 1];
                        pathGenerator.InitialYaw = psi;
                        pathGenerator.InitialPitch = 0;
                        pathGenerator.CurrentVelocity = 215; // FIX ME
                        pathGenerator.InitialEnuPosition = initialEnuPosition;

                        pathGenerator.newPath();
                        pathGenerator.TurnRadius = turnRadius;
                        pathGenerator.Turn180(currentPath.LastPoint, psi);

                        auxPath = currentPath;

                        currentPath = pathGenerator.Path;
                        ResetArc();
                    }
                    else
                    {
                        currentObjective = 0;
                        auxPath = currentPath;

                        currentPath.Reverse();
                        ResetArc();
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
                    if (currentObjective + ++cont >= currentPath.Count)
                        return true;
                }
                currentObjective += cont;
                if (currentObjective < 0)
                    currentObjective = 0;
                if (currentObjective >= currentPath.Count)
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
                arcLength.Add(arcLength[CurrentObjective - 1] + mapTools.GetDistanceMeters(currentPath.PointList[CurrentObjective - 1], currentPath.PointList[CurrentObjective]));
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
                MAV3DSim.Navigation.PathGenerator4D.Direction direction = MAV3DSim.Navigation.PathGenerator4D.Direction.left;
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
                        direction = MAV3DSim.Navigation.PathGenerator4D.Direction.right;
                }
                else
                {
                    if (initialHeading < -Math.PI / 2)
                        direction = MAV3DSim.Navigation.PathGenerator4D.Direction.right;
                }

                pathGenerator.InitialPosition = center;
                pathGenerator.InitialYaw = initialHeading;
                pathGenerator.newPath();
                pathGenerator.Turn(direction, 2 * Math.PI);

                auxCirclePath = currentPath;

                currentPath = pathGenerator.Path;
                ResetArc();
                // Turn
                pathGenerator.InitialPosition = new PointLatLngAlt(lat, lon,alt);
                pathGenerator.InitialYaw = psi;
                pathGenerator.InitialPitch = 0;
                pathGenerator.newPath();
                double turnAngle = initialHeading > 0 ? initialHeading - Math.PI : initialHeading + Math.PI;
                pathGenerator.TurnUntil(direction == MAV3DSim.Navigation.PathGenerator4D.Direction.right ? MAV3DSim.Navigation.PathGenerator4D.Direction.left : MAV3DSim.Navigation.PathGenerator4D.Direction.right, turnAngle);

                Path4D turnPath = pathGenerator.Path;

                currentObjective = 0;
                CreateInitialPath(turnPath.LastPsi_f, turnPath.LastPoint, 215); //FIX ME hardcode velocity

                currentPath.InsertRange(0,turnPath);
                
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
        public void GenerateSearchPath(GMapPolygon Rectangle, PointLatLngAlt PosInitial, double Velocity)
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
            pathGenerator.CurrentVelocity = Velocity;
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
                pathGenerator.Turn(right ? Navigation.PathGenerator4D.Direction.right : Navigation.PathGenerator4D.Direction.left, Math.PI);
                right = !right;
            }

            currentPath = pathGenerator.Path;
            ResetArc();
            originalPath = pathGenerator.Path;
        }
        public void GenerateSearchPathPolygon(GMapPolygon Polygon, PointLatLngAlt PosInitial, double Velocity)
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
            pathGenerator.CurrentVelocity = Velocity;
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
                
                pathGenerator.Turn(right ? Navigation.PathGenerator4D.Direction.right : Navigation.PathGenerator4D.Direction.left, Math.PI);

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

            currentPath = pathGenerator.Path;
            ResetArc();

            originalPath = currentPath;
        }
        public void ClearWaypoints()
        {
            waypoints = new List<PointLatLngAlt>();
        }
        public void NewWaypoint(PointLatLngAlt waypoint)
        {
            waypoints.Add(waypoint);
        }
        public void GenerateWaypointsPath(double Velocity)
        {
            pathGenerator.newPath();
            pathGenerator.InitialPosition = waypoints[0];
            pathGenerator.InitialYaw = mapTools.GetSlopeFromNord(waypoints[0].GetPointLatLng(), waypoints[1].GetPointLatLng());
            pathGenerator.TurnRadius = turnRadius;
            pathGenerator.CurrentVelocity = Velocity;
            pathGenerator.InitialPitch = pathGenerator.FindPitch(waypoints[0], waypoints[1], pathGenerator.InitialYaw, pathGenerator.InitialYaw, pathGenerator.InitialYaw,mapTools.GetDistanceMeters(waypoints[0], waypoints[1]));
            pathGenerator.StraightLine(mapTools.GetDistanceMeters(waypoints[0], waypoints[1]));
            for (int i = 1; i < waypoints.Count-2;i++ )
            {
                if(waypoints[i+1].Type == PointLatLngAlt.PointType.Land)
                    GenerateLandingPath(waypoints[i],waypoints[i+1], false);
                else
                    pathGenerator.GeneratePath(waypoints[i + 1], mapTools.GetSlopeFromNord(waypoints[i].GetPointLatLng(), waypoints[i + 1].GetPointLatLng()));
            }

            GenerateLandingPath(waypoints[waypoints.Count - 2], waypoints[waypoints.Count - 1],false);

            currentPath = pathGenerator.Path;
            ResetArc();
            originalPath = currentPath;

        }
        public void GenerateTakeoffPath(PointLatLngAlt Position, double Heading)
        {
            isTakeoff = true;
            pathGenerator.newPath();
            pathGenerator.InitialPosition= Position;
            pathGenerator.InitialYaw = Heading;
            pathGenerator.InitialPitch = 0;
            pathGenerator.TurnRadius = turnRadius;
            pathGenerator.StraightLine(30, 150, 200); // Ground maneuver
            pathGenerator.InitialPitch = 15 * Math.PI / 180;
            pathGenerator.StraightLine(50, 200, 300); // Lift Off
            pathGenerator.InitialPitch = 25 * Math.PI / 180;
            pathGenerator.StraightLine(100);// Lift Off
            pathGenerator.InitialPitch = 0;
            pathGenerator.StraightLine(150);// Low Altitud flight
            // Initial path
            auxPath = currentPath;

            currentPath = pathGenerator.Path;
            ResetArc();

        }
        public void GenerateLandingPath(PointLatLngAlt WP1, PointLatLngAlt WP2, bool NewPath)
        {
            if(NewPath)
                pathGenerator.newPath();

            if(pointListObjectivesLanding== null)
            {
                pointListObjectivesLanding = new List<PointLatLngAlt>();
                double waypointAngle = mapTools.GetSlopeFromNord(WP1, WP2);
                pointListObjectivesLanding.Add(mapTools.OffsetInMeters(WP2, Math.Cos(waypointAngle) * 30, Math.Sin(waypointAngle) * 30,0));
                pointListObjectivesLanding.Add(WP2);
            }

            PathGenerator4D pathGeneratorLanding = new PathGenerator4D();
            isLanding = false;

            double d1 = mapTools.GetDistance(WP1, pointListObjectivesLanding[0]);
            double d2 = mapTools.GetDistance(WP1, pointListObjectivesLanding[1]);
            double landSlope=0;
            PointLatLngAlt initialPosition;
            if(d1>d2){
                landSlope = mapTools.GetSlopeFromNord(pointListObjectivesLanding[0], pointListObjectivesLanding[1]);
                initialPosition = pointListObjectivesLanding[0];
            }

            else
            {
                landSlope = mapTools.GetSlopeFromNord(pointListObjectivesLanding[1], pointListObjectivesLanding[0]);
                initialPosition = pointListObjectivesLanding[1];
            }
                

            pathGeneratorLanding.newPath();
            pathGeneratorLanding.InitialPosition = initialPosition;
            pathGeneratorLanding.InitialYaw = landSlope;
            double flareAlt = 2;
            double flareDistance = 20;
            double descentDistance = 100;
            double descentAlt = 30;
            double landTrackDistance = mapTools.GetDistanceMeters(pointListObjectivesLanding[0], pointListObjectivesLanding[1]);

            pathGeneratorLanding.InitialPitch = 0;
            pathGeneratorLanding.StraightLine(landTrackDistance / 3, 0, 0);
            pathGeneratorLanding.StraightLine(landTrackDistance / 3, 0, 75);
            pathGeneratorLanding.StraightLine(landTrackDistance/3, 0, 140);
            pathGeneratorLanding.InitialPitch = Math.Atan2(flareAlt , flareDistance);
            pathGeneratorLanding.StraightLine(flareDistance/2, 150, 160);
            pathGeneratorLanding.StraightLine(flareDistance/2, 160, 170);
            pathGeneratorLanding.InitialPitch = Math.Atan2(descentAlt, descentDistance);
            pathGeneratorLanding.StraightLine(descentDistance, 170, 175);

            double d3 = mapTools.GetDistanceMeters(pathGeneratorLanding.InitialPosition, pathGenerator.InitialPosition);
            double alt1 = pathGenerator.InitialPosition.Alt - pathGeneratorLanding.InitialPosition.Alt;
            //pathGeneratorLanding.InitialPitch = Math.Atan2(alt1, d3);
            //double initialYaw_1 = pathGenerator.
            

            //pathGeneratorLanding.GeneratePath(pathGenerator.InitialPosition, initialYaw_1);
            pathGeneratorLanding.RoutePoints.Reverse();
            pathGeneratorLanding.Velocity.Reverse();
            pathGeneratorLanding.cCurvature.Reverse();
            pathGeneratorLanding.Psi_f.Reverse();
            for (int i = 0; i < pathGeneratorLanding.Psi_f.Count; i++)
            {
               if (pathGeneratorLanding.Psi_f[i] <= 0)
                    pathGeneratorLanding.Psi_f[i] += Math.PI;
                else
                    pathGeneratorLanding.Psi_f[i] -= Math.PI;
            }
            pathGeneratorLanding.Theta_f.Reverse();

            pathGenerator.GeneratePath(pathGeneratorLanding.RoutePoints[0], pathGeneratorLanding.Psi_f[0]);

            landingPoint = pathGenerator.RoutePoints.Count-1;

            pathGenerator.AddPath(pathGeneratorLanding.RoutePoints, pathGeneratorLanding.Velocity, pathGeneratorLanding.cCurvature, pathGeneratorLanding.Psi_f, pathGeneratorLanding.Theta_f);

        } 
  
        public List<PointLatLngAlt> PointListObjectives
        {
            get { return currentPath.PointList; }
            //set { pointListObjectives = value; pointListObjectives_o = value; }
        }
        public List<PointLatLng> PointListObjectivesLatLng
        {
            get { return currentPath._PointList; }
            //set { _pointListObjectives = value; }
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
            get { return auxCirclePath.PointList; }
            //set { pointListObjectivesCircle = value; }
        }
        public List<PointLatLng> CirclePathatLng
        {
            get { return auxCirclePath._PointList; }
            //set { _pointListObjectivesCircle = value; }
        }
        public List<PointLatLngAlt> Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public List<double> CCurvature
        {
            get { return currentPath.Curvature; }
            //set { cCurvature = value; cCurvature_o = value; }
        }
        public List<double> Velocity
        {
            get { return currentPath.Velocity; }
            //set { velocity = value; velocity_o = value; }
        }

        //public List<double> ArcLength
        //{
        //    get { return arcLength; }
        //    set { arcLength = value; arcLength_o = value; }
        //}

        public List<double> Psi_F
        {
            get { return currentPath.Psi_f; }
            //set { psi_f = value; psi_f_o = value; }
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
            get { return currentPath.PointList[currentObjective]; }
        }
        public double CurrentVelocity
        {
            get { return currentPath.Velocity[currentObjective]; }
        }
        public double CurrentPsi_F
        {
            get { return currentPath.Psi_f[currentObjective]; }
        }
        public double CurrentTheta_F
        {
            get { return currentPath.Theta_f[currentObjective]; }
        }
        public double CurrentCurvature
        {
            get { return currentPath.Curvature[currentObjective]; }
        }
        public bool IsTurn180
        {
            get { return isTurn180; }
            set { isTurn180 = value; }
        }
        public bool IsLanding
        {
            get { return isLanding; }
            set { isLanding = value; }
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
