using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using GMap.NET.MapProviders;
using GMap.NET;
using System.Net;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;

namespace MAV3DSim.Docks
{
    public delegate void RectangleDrawnEventHandler(object sender, DrawnPolygonEventArgs e);
    public delegate void PolygonDrawnEventHandler(object sender, DrawnPolygonEventArgs e);
    public delegate void GeofenceDrawnEventHandler(object sender, DrawnGeofenceEventArgs e);
    public delegate void PoiModifiedEventHandler(object sender, POIEventArgs e);
    public partial class Map : DockContent
    {
        Mav3DSim parent;
        GMapMarkerImage marker;
        GMapRoute route;
        GMapOverlay overlay;

        GMapRoute waypointsRoute;
        GMapRoute geofenceRoute;

        bool drawRectangle = false;
        bool drawPolygon = false;
        bool newPointOfInterest = false;
        bool newLandingTrack= false;
        bool newGeofence = false;
        List<PointLatLng> pointListRectangle;
        List<PointLatLng> pointListGeofence;
        List<PointLatLng> pointListLanding;
        List<PointLatLng> pointsOfInterests;
        List<GMapMarkerLabel> pointsOfInterestsMarkers;
        PointLatLng initialRectanglePoint;
        List<GMapRoute> gmapRoutes;

        GMapMarkerImage markerCircle;
        List<PointLatLng> pointList;
        List<PointLatLng> waypoints;
        //List<PointLatLng> waypoints_1;
        List<PointLatLng> geofence;
        List<GMapMarkerLabel> waypointsMarkers;
        List<GMapMarkerLabel> geofenceMarkers;

        PointLatLng rectangleP1;
        public event RectangleDrawnEventHandler RectangleDrawn;
        public event PolygonDrawnEventHandler PolygonDrawn;
        public event GeofenceDrawnEventHandler GeofenceDrawn;
        public event PoiModifiedEventHandler PoiModified;

        bool enableUpdate;

        ThreadSafe threadSafe;

        public enum MapRoute
        {
            Plane = 0,
            Path,
            InitialPath,
            Circle,
            Waypoints,
            Geofence,
        }

        public Map()
        {
            InitializeComponent();

            

            // gMap
            gMapControl1.MapProvider = GMapProviders.BingHybridMap;
            PointLatLng point = new PointLatLng(49.394285, 2.711907);
            gMapControl1.Position = point;
            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 21;
            gMapControl1.Zoom = 16;
            
            // Use UTC Proxy
            WebProxy proxy = new WebProxy("proxyweb.utc.fr", 3128);
            proxy.UseDefaultCredentials = true;
            GMapProvider.WebProxy = proxy;

            InitMapRoutes();
            
            
            // Rectangle variables;
            pointListRectangle = new List<PointLatLng>();
            pointListGeofence = new List<PointLatLng>();
            initialRectanglePoint = new PointLatLng();
            enableUpdate = true;
            waypointsMarkers = new List<GMapMarkerLabel>();
            threadSafe = new ThreadSafe();
        }
        private void InitMapRoutes()
        {
            PointLatLng point = new PointLatLng(49.394285, 2.711907);
            overlay = new GMapOverlay("overlay");
            marker = new GMap.NET.WindowsForms.Markers.GMapMarkerImage(point, Properties.Resources.Bixler_Final4);
            markerCircle = new GMap.NET.WindowsForms.Markers.GMapMarkerImage(point, Properties.Resources.loading_circle);
            overlay.Markers.Add(marker);
            pointList = new List<PointLatLng>();
            pointList.Add(point);
            route = new GMapRoute(pointList, MapRoute.Plane.ToString());
            route.Stroke = (Pen)route.Stroke.Clone();
            route.Stroke.Color = System.Drawing.Color.FromArgb(125, System.Drawing.Color.Red);
            overlay.Routes.Add(route);
            overlay.Routes.Add(new GMapRoute(MapRoute.Plane.ToString()));
            //overlay.Polygons.Add(polygon);
            gMapControl1.Overlays.Add(overlay);

            AddRoute(route, Color.FromArgb(125, Color.Red));
            AddRoute(new GMapRoute(MapRoute.Path.ToString()), Color.FromArgb(125, Color.Red));
            AddRoute(new GMapRoute(MapRoute.InitialPath.ToString()), Color.FromArgb(125, Color.Red));
            AddRoute(new GMapRoute(MapRoute.Circle.ToString()), Color.FromArgb(125, Color.Red));
            AddRoute(new GMapRoute(MapRoute.Waypoints.ToString()), Color.FromArgb(125, Color.Blue));
            AddRoute(new GMapRoute(MapRoute.Geofence.ToString()), Color.FromArgb(125, Color.Black));

        }
        private void Map_Shown(object sender, EventArgs e)
        {
            parent = (Mav3DSim)this.ParentForm;
        }

        protected virtual void OnRectangleDrawn(DrawnPolygonEventArgs e)
        {
            if (RectangleDrawn != null)
                RectangleDrawn(this, e);
        }

        protected virtual void OnPolygonDrawn(DrawnPolygonEventArgs e)
        {
            if (PolygonDrawn != null)
                PolygonDrawn(this, e);
        }

        protected virtual void OnGeofenceDrawn(DrawnGeofenceEventArgs e)
        {
            if (GeofenceDrawn != null)
                GeofenceDrawn(this, e);
        }

        public PointLatLng Position
        {
            get { return gMapControl1.Position; }
            set { gMapControl1.Position = value; }
        }

        public Double Lat
        {
            get { return gMapControl1.Position.Lat; }
            set
            {
                PointLatLng point = gMapControl1.Position;
                point.Lat = value;
                gMapControl1.Position = point;
            }
        }

        public Double Lon
        {
            get { return gMapControl1.Position.Lat; }
            set
            {
                PointLatLng point = gMapControl1.Position;
                point.Lng = value;
                gMapControl1.Position = point;
            }
        }

        public void UpdateMarkerPosition(PointLatLng position)
        {

            
            marker.Position = position;
            pointList.Add(position);
            route = new GMapRoute(pointList, "Plane Route");
            route.Stroke = (Pen)route.Stroke.Clone();
            route.Stroke.Color = System.Drawing.Color.FromArgb(125, System.Drawing.Color.Red);
            gMapControl1.Overlays[0].Routes[0] = route;
            
        }

        public void UpdateMarkerRotation(float rotation)
        {
            marker.setRotation(rotation);
        }

        public void UpdateMarker()
        {
            overlay.Markers[0] = marker;
        }

        public void UpdateMarker(PointLatLng position, float rotation)
        {

            //marker = new GMapMarkerImage(position, rotation, Properties.Resources.Bixler_Final4);
            //marker.Position = position;
            //marker.setRotation(rotation);
                
            pointList.Add(position);
            
            
            //route = new GMapRoute(pointList, "Plane Route");
            //route.Stroke = (Pen)route.Stroke.Clone();
            //route.Stroke.Color = System.Drawing.Color.FromArgb(125, System.Drawing.Color.Red);
            
            if (enableUpdate)
            {
                //threadSafe.SetControlPropertyThreadSafe(gMapControl1, "Overlays[0].Markers[0]", marker);
                //threadSafe.SetGMapControlOverlayPropertyThreadSafe(gMapControl1, "Routes[0]",0, route);
                gMapControl1.Overlays[0].Markers[0] = new GMapMarkerImage(position, rotation, Properties.Resources.Bixler_Final4); ;
                gMapControl1.Overlays[0].Routes[0] = new GMapRoute(pointList, "Plane Route");
                gMapControl1.Overlays[0].Routes[0].Stroke = (Pen)route.Stroke.Clone();
                gMapControl1.Overlays[0].Routes[0].Stroke.Color = System.Drawing.Color.FromArgb(125, System.Drawing.Color.Red);

            }
        }

        public void UpdateMap()
        {
            gMapControl1.Update();
        }

        public void UpdateMarkerCircle(PointLatLng position)
        {
            markerCircle.Position = position;
            int markerCirclePosition = gMapControl1.Overlays[0].Markers.IndexOf(markerCircle);
            if (markerCirclePosition != -1)
                gMapControl1.Overlays[0].Markers[markerCirclePosition] = markerCircle;
            else
                gMapControl1.Overlays[0].Markers.Add(markerCircle);
        }

        public void ClearVehicleRoute(PointLatLng point)
        {
            pointList = new List<PointLatLng>();
            pointList.Add(point);
            route = new GMapRoute(pointList, "My plaine route");
            gMapControl1.Overlays[0].Routes[0] = route;
        }

        public void AddRoute(GMapRoute Route, Color RouteColor)
        {
            Route.Stroke = (Pen)route.Stroke.Clone();
            Route.Stroke.Color = RouteColor;
            gMapControl1.Overlays[0].Routes.Add(Route);
        }

        public void ClearRoute(MapRoute MapRoute)
        {
            gMapControl1.Overlays[0].Routes[(int)MapRoute] = new GMapRoute(MapRoute.ToString());
        }

        public void AddRoute(MapRoute MapRoute, GMapRoute Route, Color RouteColor)
        {
            AddRoute((int)MapRoute, Route, RouteColor);
        }
        public void AddRoute(int RouteIndex, GMapRoute Route, Color RouteColor)
        {
            if ((gMapControl1.Overlays[0].Routes.Count - 1) >= RouteIndex)
            {
                gMapControl1.Overlays[0].Routes[RouteIndex] = Route;
                gMapControl1.Overlays[0].Routes[RouteIndex].Stroke = (Pen)route.Stroke.Clone();
                gMapControl1.Overlays[0].Routes[RouteIndex].Stroke.Color = RouteColor;

            }
            else
                AddRoute(Route, RouteColor);
        }

        public void DrawRectangle()
        {
            drawRectangle = true;
            pointListRectangle = new List<PointLatLng>();
        }

        public void DrawPolygon()
        {
            drawRectangle = false;
            drawPolygon = true;
            while (gMapControl1.Overlays[0].Markers.Count != 1)
            {
                gMapControl1.Overlays[0].Markers.Remove(gMapControl1.Overlays[0].Markers[gMapControl1.Overlays[0].Markers.Count-1]);
            }
            pointListRectangle = new List<PointLatLng>();
            gMapControl1.Overlays[0].Polygons.Clear();
        }

        public void NewGeofence()
        {
            newGeofence = true;
            ClearGeofence();
        }

        public void NewPointOfInterest()
        {
            if(pointsOfInterests==null)
            {
                pointsOfInterests = new List<PointLatLng>();
                pointsOfInterestsMarkers = new List<GMapMarkerLabel>();
            }
                
            newPointOfInterest = true;
        }

        public void NewWaypoint(PointLatLngAlt waypoint)
        {
            NewWaypoint(waypoint.GetPointLatLng());
        }
        public void NewWaypoint(PointLatLng waypoint)
        {
            if(waypoints == null)
            {
                waypoints = new List<PointLatLng>();
            }
            waypoints.Add(waypoint);
            

            
            if(gMapControl1.Overlays[0].Routes.Contains(waypointsRoute))
            {
                int index = gMapControl1.Overlays[0].Routes.IndexOf(waypointsRoute);
                waypointsRoute = new GMapRoute(waypoints, "Waypoints");
                waypointsRoute.Stroke.Color = Color.FromArgb(90,Color.Blue);
                gMapControl1.Overlays[0].Routes[index] = waypointsRoute;
                waypointsMarkers.Add(new GMapMarkerLabel(waypoint, Properties.Resources.Google_Maps_Marker, waypoints.Count.ToString()));
                gMapControl1.Overlays[0].Markers.Add(waypointsMarkers[waypointsMarkers.Count - 1]);
                
            }
            else
            {
                waypointsRoute = new GMapRoute(waypoints, "Waypoints");
                waypointsMarkers.Add(new GMapMarkerLabel(waypoint, Properties.Resources.Google_Maps_Marker, waypoints.Count.ToString()));
                gMapControl1.Overlays[0].Routes.Add(waypointsRoute);
                gMapControl1.Overlays[0].Markers.Add(waypointsMarkers[waypointsMarkers.Count-1]);
            }
        }

        public void ClearWaypoints()
        {
            if (gMapControl1.Overlays[0].Routes.Contains(waypointsRoute))
            {
                waypoints.Clear();
                
                int index = gMapControl1.Overlays[0].Routes.IndexOf(waypointsRoute);
                waypointsRoute = new GMapRoute(waypoints, "Waypoints");
                gMapControl1.Overlays[0].Routes[index] = waypointsRoute;
                for(int i=0;i<waypointsMarkers.Count;i++)
                {
                    gMapControl1.Overlays[0].Markers.Remove(waypointsMarkers[i]);
                }
                waypointsMarkers.Clear();
            }
            
        }

        public void NewGeofence(PointLatLngAlt GeofencePoint)
        {
            NewGeofence(GeofencePoint.GetPointLatLng());
        }
        public void NewGeofence(PointLatLng GeofencePoint)
        {
            if (geofence == null || geofenceMarkers==null)
            {
                geofence = new List<PointLatLng>();
                geofenceMarkers = new List<GMapMarkerLabel>();
            }
            geofence.Add(GeofencePoint);

            //AddRoute(MapRoute.Geofence, new GMapRoute(geofence, "Geofence"), Color.FromArgb(127, Color.Magenta));
            //gMapControl1.Overlays[0].Polygons[0] = new GMapPolygon(geofence, "Geofence");
            addPolygon(geofence, "Geofence");

            geofenceMarkers.Add(new GMapMarkerLabel(GeofencePoint, Properties.Resources.Google_Maps_Marker, geofence.Count.ToString()));
            gMapControl1.Overlays[0].Markers.Add(geofenceMarkers[geofenceMarkers.Count - 1]);

        }

        private void addPolygon( List<PointLatLng> poligonList, string name)
        {
            if (gMapControl1.Overlays[0].Polygons.Count == 0)
                gMapControl1.Overlays[0].Polygons.Add(new GMapPolygon(poligonList, name));
            else
                gMapControl1.Overlays[0].Polygons[0] = new GMapPolygon(poligonList, name);
        }
        private void clearPolygon()
        {
            if (gMapControl1.Overlays[0].Polygons.Count != 0)
                gMapControl1.Overlays[0].Polygons[0].Clear();
        }

        public void ClearGeofence()
        {

            //ClearRoute(MapRoute.Geofence);
            clearPolygon();
            geofence = new List<PointLatLng>();
            for (int i = 1; i < gMapControl1.Overlays[0].Markers.Count - 1; i++)
                gMapControl1.Overlays[0].Markers.Remove(gMapControl1.Overlays[0].Markers[i]);

            geofenceMarkers = new List<GMapMarkerLabel>();

        }

        public void NewLandingTrack()
        {
            
            pointListLanding = new List<PointLatLng>();
            newLandingTrack = true;
            
        }

        public void RemovePointOfInterest(int remove)
        {
            pointsOfInterests.Remove(pointsOfInterests[remove-1]);
            gMapControl1.Overlays[0].Markers.Remove(pointsOfInterestsMarkers[remove-1]);
            pointsOfInterestsMarkers.Remove(pointsOfInterestsMarkers[remove-1]);
        }

        protected virtual void OnPoiModified(POIEventArgs e)
        {
            if (PoiModified != null)
                PoiModified(this, new POIEventArgs(pointsOfInterests));
        }

        

        private void gMapControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawRectangle && pointListRectangle.Count > 0)
            {
                PointLatLng point = gMapControl1.FromLocalToLatLng(e.X, e.Y);
                pointListRectangle[0] = initialRectanglePoint;
                if(pointListRectangle.Count==1)
                {
                    pointListRectangle.Add(new PointLatLng(pointListRectangle[0].Lat, point.Lng));
                    pointListRectangle.Add(new PointLatLng(point.Lat, point.Lng));
                    pointListRectangle.Add(new PointLatLng(point.Lat, pointListRectangle[0].Lng));

                }
                else {

                    pointListRectangle[1] = new PointLatLng(pointListRectangle[0].Lat, point.Lng);
                    pointListRectangle[2] = new PointLatLng(point.Lat, point.Lng);
                    pointListRectangle[3] = new PointLatLng(point.Lat, pointListRectangle[0].Lng);
                }

                pointListRectangle.Sort((x, y) => x.Lng.CompareTo(y.Lng));
                pointListRectangle.Sort((x, y) => x.Lat.CompareTo(y.Lat));

                PointLatLng p = pointListRectangle[2];
                pointListRectangle[2] = pointListRectangle[3];
                pointListRectangle[3] = p;

                pointListRectangle.Reverse();

                if (gMapControl1.Overlays[0].Polygons.Count == 0)
                {
                    gMapControl1.Overlays[0].Polygons.Add(new GMapPolygon(pointListRectangle, "Rectangle"));
                    //gMapControl1.Overlays[0].Markers.Add(new GMapMarkerLabel(pointListRectangle[0], Properties.Resources.Google_Maps_Marker, "0"));
                    //gMapControl1.Overlays[0].Markers.Add(new GMapMarkerLabel(pointListRectangle[1], Properties.Resources.Google_Maps_Marker, "1"));
                    //gMapControl1.Overlays[0].Markers.Add(new GMapMarkerLabel(pointListRectangle[2], Properties.Resources.Google_Maps_Marker, "2"));
                    //gMapControl1.Overlays[0].Markers.Add(new GMapMarkerLabel(pointListRectangle[3], Properties.Resources.Google_Maps_Marker, "3"));
                }
                else
                {
                    gMapControl1.Overlays[0].Polygons[0] = (new GMapPolygon(pointListRectangle, "Rectangle"));
                    //gMapControl1.Overlays[0].Markers[1] = (new GMapMarkerLabel(pointListRectangle[0], Properties.Resources.Google_Maps_Marker, "0"));
                    //gMapControl1.Overlays[0].Markers[2] = (new GMapMarkerLabel(pointListRectangle[1], Properties.Resources.Google_Maps_Marker, "1"));
                    //gMapControl1.Overlays[0].Markers[3] = (new GMapMarkerLabel(pointListRectangle[2], Properties.Resources.Google_Maps_Marker, "2"));
                    //gMapControl1.Overlays[0].Markers[4] = (new GMapMarkerLabel(pointListRectangle[3], Properties.Resources.Google_Maps_Marker, "3"));
                }
                gMapControl1.Overlays[0].Polygons[0].Fill = Brushes.Transparent;
            } 
            else if(newLandingTrack && pointListLanding.Count > 0)
            {
                if (pointListLanding.Count > 1)
                    pointListLanding[1]=gMapControl1.FromLocalToLatLng(e.X, e.Y);
                else
                    pointListLanding.Add(gMapControl1.FromLocalToLatLng(e.X, e.Y));

                if (gMapControl1.Overlays[0].Polygons.Count == 0)
                {
                    gMapControl1.Overlays[0].Polygons.Add(new GMapPolygon(pointListLanding, "Landing"));
                    
                }
                else
                {
                    gMapControl1.Overlays[0].Polygons[0] = (new GMapPolygon(pointListLanding, "Landing"));
                }
                gMapControl1.Overlays[0].Polygons[0].Fill = Brushes.Transparent;
            }
        }

        private void gMapControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (drawRectangle)
            {
                
                drawRectangle = false;
                OnRectangleDrawn(new DrawnPolygonEventArgs(gMapControl1.Overlays[0].Polygons[0]));
            }else 

            if(drawPolygon)
            {
                pointListRectangle.Add(gMapControl1.FromLocalToLatLng(e.X, e.Y));
                gMapControl1.Overlays[0].Markers.Add(new GMapMarkerLabel(pointListRectangle[pointListRectangle.Count-1], Properties.Resources.Google_Maps_Marker, (pointListRectangle.Count-1).ToString()));
                if (gMapControl1.Overlays[0].Polygons.Count == 0)
                {
                    gMapControl1.Overlays[0].Polygons.Add(new GMapPolygon(pointListRectangle, "Polygon"));
                }
                else
                {
                    gMapControl1.Overlays[0].Polygons[0] = (new GMapPolygon(pointListRectangle, "Polygon"));
                }
            }else if(newGeofence)
            {
                pointListGeofence.Add(gMapControl1.FromLocalToLatLng(e.X, e.Y));
                gMapControl1.Overlays[0].Markers.Add(new GMapMarkerLabel(pointListGeofence[pointListGeofence.Count - 1], Properties.Resources.Google_Maps_Marker, (pointListGeofence.Count - 1).ToString()));
                if (gMapControl1.Overlays[0].Polygons.Count == 0)
                {
                    gMapControl1.Overlays[0].Polygons.Add(new GMapPolygon(pointListGeofence, "Polygon"));
                    gMapControl1.Overlays[0].Polygons[0].Fill = Brushes.Transparent;
                    gMapControl1.Overlays[0].Polygons[0].Stroke.Color = Color.Red;
                }
                else
                {
                    gMapControl1.Overlays[0].Polygons[0] = (new GMapPolygon(pointListGeofence, "Polygon"));
                    gMapControl1.Overlays[0].Polygons[0].Stroke.Color = Color.Red;
                    gMapControl1.Overlays[0].Polygons[0].Fill = Brushes.Transparent;

                }

            }else
                

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                enableUpdate = true;
            }
            else if (newPointOfInterest)
            {
                pointsOfInterests.Add(gMapControl1.FromLocalToLatLng(e.X, e.Y));
                gMapControl1.Overlays[0].Markers.Add(new GMapMarkerLabel(pointsOfInterests[pointsOfInterests.Count - 1], Properties.Resources.Google_Maps_Marker, pointsOfInterests.Count.ToString()));
                pointsOfInterestsMarkers.Add((GMapMarkerLabel)gMapControl1.Overlays[0].Markers[gMapControl1.Overlays[0].Markers.Count - 1]);
                newPointOfInterest = false;
                OnPoiModified(new POIEventArgs(pointsOfInterests));

            }
        }

        private void gMapControl1_MouseDown(object sender, MouseEventArgs e)
        {
            

            if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                enableUpdate = false;
            }
            else if (drawRectangle)
            {
                pointListRectangle.Add(gMapControl1.FromLocalToLatLng(e.X, e.Y));
                initialRectanglePoint = gMapControl1.FromLocalToLatLng(e.X, e.Y);
            }
            else if (newLandingTrack)
            {
                if (pointListLanding.Count == 0)
                    pointListLanding.Add(gMapControl1.FromLocalToLatLng(e.X, e.Y));
                if (pointListLanding.Count == 2)
                {
                    newLandingTrack = false;
                    List<PointLatLngAlt> landingPoints = new List<PointLatLngAlt>();
                    landingPoints.Add(new PointLatLngAlt(pointListLanding[0]));
                    landingPoints.Add(new PointLatLngAlt(pointListLanding[1]));

                    ((Navigation.Navigator4D)parent.GetNavigator4D).PointListObjectivesLanding = landingPoints;
                }
            }
        }

        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (drawPolygon)
            {
                drawPolygon = false;
                OnPolygonDrawn(new DrawnPolygonEventArgs(gMapControl1.Overlays[0].Polygons[0]));
            }
            if(newGeofence)
            {
                newGeofence = false;
                OnGeofenceDrawn(new DrawnGeofenceEventArgs(gMapControl1.Overlays[0].Polygons[0]));

                
            }


        }


    }
}


public class DrawnPolygonEventArgs : EventArgs
{
    public DrawnPolygonEventArgs(GMapPolygon gMapPolygon)
    {
        this.gMapPolygon = gMapPolygon;
    }

    public GMapPolygon gMapPolygon { get; private set; }
}
public class DrawnGeofenceEventArgs : EventArgs
{
    public DrawnGeofenceEventArgs(GMapPolygon gMapPolygon)
    {
        this.GeofencePolygon = gMapPolygon;
    }

    public GMapPolygon GeofencePolygon { get; private set; }
}

public class POIEventArgs : EventArgs
{
    public POIEventArgs(List<PointLatLng> pointsOfInterests)
    {
        this.pointsOfInterests = pointsOfInterests;
    }

    public List<PointLatLng> pointsOfInterests { get; private set; }
}

public class SortByLat : IComparer<PointLatLng>
{
    public int Compare(PointLatLng p1, PointLatLng p2)
    {
        return p1.Lat.CompareTo(p2.Lat);
    }
}

public class SortByLon : IComparer<PointLatLng>
{
    public int Compare(PointLatLng p1, PointLatLng p2)
    {
        return p1.Lng.CompareTo(p2.Lng);
    }
}
