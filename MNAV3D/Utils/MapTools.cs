using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;


namespace MAV3DSim.Utils
{
    class MapTools
    {
        public MapTools()
        {

        }

        public PointLatLng OffsetInMeters(PointLatLng InitialPosition, double OffsetE, double OffsetN)
        {
            //offsets in meters

            //Earth’s radius, sphere
            double R = 6378137;
            //Coordinate offsets in radians
            double dLat = OffsetN/R;
            double dLon = OffsetE / (R * Math.Cos(Math.PI * InitialPosition.Lat / 180));
            //OffsetPosition, decimal degrees
            double latO = InitialPosition.Lat + dLat * 180 / Math.PI;
            double lonO = InitialPosition.Lng + dLon * 180 / Math.PI;

            return new PointLatLng(latO, lonO);
 
        }

        public PointLatLngAlt OffsetInMeters(PointLatLngAlt InitialPosition, double OffsetE, double OffsetN, double OffsetZ)
        {
            //offsets in meters

            //Earth’s radius, sphere
            double R = 6378137;
            //Coordinate offsets in radians
            double dLat = OffsetN / R;
            double dLon = OffsetE / (R * Math.Cos(Math.PI * InitialPosition.Lat / 180));
            //OffsetPosition, decimal degrees
            double latO = InitialPosition.Lat + dLat * 180 / Math.PI;
            double lonO = InitialPosition.Lng + dLon * 180 / Math.PI;

            double z = InitialPosition.Alt + OffsetZ;
            return new PointLatLngAlt(latO, lonO, z);

        }

        public double GetDistance(PointLatLng InitialPosition, PointLatLng FinalPosition)
        {
            List<PointLatLng> list = new List<PointLatLng>();
            list.Add(InitialPosition);
            list.Add(FinalPosition);
            GMapRoute route = new GMapRoute(list, "Distance");

            double R = 6371; // km
            double phi_1 = InitialPosition.Lat * Math.PI / 180;// lat1.toRadians();
            double phi_2 = FinalPosition.Lat * Math.PI /180; // lat2.toRadians();
            double delta_phi = (FinalPosition.Lat - InitialPosition.Lat) * Math.PI / 180;
            double delta_lambda = (FinalPosition.Lng - InitialPosition.Lng)* Math.PI /180;

            double a = Math.Sin(delta_phi / 2) * Math.Sin(delta_phi/ 2) + Math.Cos(phi_1) * Math.Cos(phi_2) * Math.Sin(delta_lambda / 2) * Math.Sin(delta_lambda / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double d = R * c;

            double distance = route.Distance;
            return d;
        }

        public double GetDistanceMeters(PointLatLng InitialPosition, PointLatLng FinalPosition)
        {
            List<PointLatLng> list = new List<PointLatLng>();
            list.Add(InitialPosition);
            list.Add(FinalPosition);
            GMapRoute route = new GMapRoute(list, "Distance");



            double r = route.Distance*1000;
            return GetDistance(InitialPosition, FinalPosition)*1000;
        }

        public double GetDistanceMeters(PointLatLngAlt InitialPosition, PointLatLngAlt FinalPosition)
        {
            return GetDistance(InitialPosition.GetPointLatLng(), FinalPosition.GetPointLatLng()) * 1000;
        }

        public double GetDistance(PointLatLngAlt InitialPosition, PointLatLngAlt FinalPosition)
        {
            return GetDistance(InitialPosition.GetPointLatLng(), FinalPosition.GetPointLatLng());
        }
        public double GetSlopeFromNord(PointLatLngAlt InitialPosition, PointLatLngAlt FinalPosition)
        {
            return GetSlopeFromNord(InitialPosition.GetPointLatLng(), FinalPosition.GetPointLatLng());
        }

        public double GetSlopeFromNord(PointLatLng InitialPosition, PointLatLng FinalPosition)
        {
            double slope = GetSlope( InitialPosition,  FinalPosition);
            slope = Math.PI / 2 - slope;
            if (slope > Math.PI)
                slope -= 2 * Math.PI;
            else if (slope < -Math.PI)
                slope += 2 * Math.PI;

            double dLon = (FinalPosition.Lng - InitialPosition.Lng);

            double y = Math.Sin(dLon) * Math.Cos(FinalPosition.Lat);
            double x = Math.Cos(InitialPosition.Lat) * Math.Sin(FinalPosition.Lat) - Math.Sin(InitialPosition.Lat)
                    * Math.Cos(FinalPosition.Lat) * Math.Cos(dLon);

            double brng = Math.Atan2(y, x);
            return brng;
        }
        public double GetAngleMeassuredFromNord(PointLatLngAlt InitialPosition, PointLatLngAlt FinalPosition)
        {
            return GetAngleMeassuredFromNord(InitialPosition.GetPointLatLng(), FinalPosition.GetPointLatLng());
        }
        public double GetAngleMeassuredFromNord(PointLatLng InitialPosition, PointLatLng FinalPosition)
        {


            Point3D point = Geodetic2ENU(FinalPosition, InitialPosition);
            double slope = Math.Atan2((point.Y), (point.X)); 
            double angle = Math.PI / 2 - Math.Atan2((point.Y), (point.X));
            
            //double slope3 =  Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) + Math.PI/2;

            if (angle > Math.PI)
                angle -= 2 * Math.PI;
            else if (angle < -Math.PI)
                angle += 2 * Math.PI;
            return angle;

            
        }

        public double GetSlope(PointLatLng InitialPosition, PointLatLng FinalPosition)
        {
            GMapControl control = new GMapControl();
            GPoint p1 = control.FromLatLngToLocal(InitialPosition);
            GPoint p2 = control.FromLatLngToLocal(FinalPosition);


            Point3D point = Geodetic2ENU(FinalPosition, InitialPosition);
            double slope1 = Math.PI / 2 - Math.Atan2((point.Y), (point.X));
            double slope2 = -Math.PI / 2 - Math.Atan2((InitialPosition.Lat - FinalPosition.Lat), (InitialPosition.Lng - FinalPosition.Lng));
            double slope3 =  Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);

            if (slope2 > Math.PI)
                slope2 -= 2 * Math.PI;
            else if (slope2 < -Math.PI)
                slope2 += 2 * Math.PI;
            return slope2;// -Math.PI / 2 - Math.Atan2((InitialPosition.Lat - FinalPosition.Lat), (InitialPosition.Lng - FinalPosition.Lng));

            //return Math.PI / 2 - Math.Atan2((point.Y) ,(point.X));
        }

        public double angleBetweenVectors(PointLatLng initialPointV1, PointLatLng finalPointV1, PointLatLng initialPointV2, PointLatLng finalPointV2, GMapControl gmapControl)
        {
            double angle=0;


            return angle;
        }


        public bool IsInside(PointLatLng Point, List<PointLatLng> PontosPolig)
        {//                             X               y               
            int sides = PontosPolig.Count();
            int j = sides - 1;
            bool pointStatus = false;

            for (int i = 0; i < sides; i++)
            {
                if (PontosPolig[i].Lng < Point.Lng && PontosPolig[j].Lng >= Point.Lng ||
                    PontosPolig[j].Lng < Point.Lng && PontosPolig[i].Lng >= Point.Lng)
                {
                    if (PontosPolig[i].Lat + (Point.Lng - PontosPolig[i].Lng) /
                        (PontosPolig[j].Lng - PontosPolig[i].Lng) * (PontosPolig[j].Lat - PontosPolig[i].Lat) < Point.Lat)
                    {
                        pointStatus = !pointStatus;
                    }
                }
                j = i;
            }
            return pointStatus;
        }

        public bool IsInside(PointLatLngAlt Point, List<PointLatLngAlt> PontosPolig)
        {
            int sides = PontosPolig.Count();
            int j = sides - 1;
            bool pointStatus = false;

            for (int i = 0; i < sides; i++)
            {
                if (PontosPolig[i].Lng < Point.Lng && PontosPolig[j].Lng >= Point.Lng ||
                    PontosPolig[j].Lng < Point.Lng && PontosPolig[i].Lng >= Point.Lng)
                {
                    if (PontosPolig[i].Lat + (Point.Lng - PontosPolig[i].Lng) /
                        (PontosPolig[j].Lng - PontosPolig[i].Lng) * (PontosPolig[j].Lat - PontosPolig[i].Lat) < Point.Lat)
                    {
                        pointStatus = !pointStatus;
                    }
                }
                j = i;
            }
            return pointStatus;
        }

        public bool IsInsideCircle(PointLatLng Point, PointLatLng Center, double Radius)
        {

            double distance = GetDistanceMeters(Center, Point);
            distance *=distance;
            double radiusSquared = Radius * Radius;
            return distance <= radiusSquared;
        }

        public bool IsInsideCircle(PointLatLngAlt Point, PointLatLngAlt Center, double Radius)
        {
            return IsInsideCircle(Point.GetPointLatLng(), Center.GetPointLatLng(), Radius);
        }

        public Point3D Geodetic2ENU(PointLatLng Point, PointLatLng InitialPoint)
        {
            double a = 6378137;     // Semi-major axis
            double ex = 0.08181919; // Excentricity
            double h = 2241;        // Height of Mexico City
            double N_phi = 0;
            double x_ecef = 0;
            double y_ecef = 0;
            double z_ecef = 0;

            double a_h = 48.032537299811120;
            double a_v = 57.210092131377180;

            double phi_r = InitialPoint.Lat;//Convert.ToDouble(txtInitX.Text);
            double lambda_r = InitialPoint.Lng;//Convert.ToDouble(txtInitY.Text);

            double N_phi_r = a / Math.Sqrt(1 - Math.Pow(ex, 2) * Math.Pow(Math.Sin(phi_r), 2));

            double x_ecef_r = (N_phi_r / a_v + h) * Math.Cos(phi_r) * Math.Cos(lambda_r);
            double y_ecef_r = (N_phi_r / a_v + h) * Math.Cos(phi_r) * Math.Sin(lambda_r);
            double z_ecef_r = ((N_phi_r / a_v) * (1 - Math.Pow(ex, 2)) + h) * Math.Sin(phi_r);

            /*double e2 = Math.Pow(ex, 2);
            double sinx = Math.Sin(lat);
            double sinx2 = Math.Pow(sinx, 2);
            double e2sinx = e2 * sinx2;
            double sqrte2sinx = Math.Sqrt(1 - e2sinx);
            double N_phi1 = a / (sqrte2sinx);
            */
            N_phi = a / (Math.Sqrt(1 - Math.Pow(ex, 2) * Math.Pow(Math.Sin(Point.Lat), 2)));

            double cosx = Math.Cos(Point.Lat);
            double cosy = Math.Cos(Point.Lng);

            x_ecef = (N_phi / a_v + h) * Math.Cos(Point.Lat) * Math.Cos(Point.Lng);
            y_ecef = (N_phi / a_v + h) * Math.Cos(Point.Lat) * Math.Sin(Point.Lng);
            z_ecef = (N_phi / a_v * (1 - Math.Pow(ex, 2)) + h) * Math.Sin(Point.Lat);

            Matrix R = new Matrix(3, 3);
            R[0, 0] = -Math.Sin(lambda_r);
            R[0, 1] = Math.Cos(lambda_r);
            R[0, 2] = 0;

            R[1, 0] = -Math.Sin(phi_r) * Math.Cos(lambda_r);
            R[1, 1] = -Math.Sin(phi_r) * Math.Sin(lambda_r);
            R[1, 2] = Math.Cos(phi_r);

            R[2, 0] = Math.Cos(phi_r) * Math.Cos(lambda_r);
            R[2, 1] = Math.Cos(phi_r) * Math.Sin(lambda_r);
            R[2, 2] = Math.Sin(phi_r);

            Matrix P = new Matrix(3, 1);
            P[0, 0] = x_ecef - x_ecef_r;
            P[1, 0] = y_ecef - y_ecef_r;
            P[2, 0] = z_ecef - z_ecef_r;

            Matrix enu = new Matrix(3, 1);

            enu = R * P;

            PointF _p = new PointF((float)enu[0, 0], (float)enu[1, 0]);


            double InitialPointLat = InitialPoint.Lat * Math.PI / 180;
            double InitialPointLng = InitialPoint.Lng * Math.PI / 180;
            double PointLat = Point.Lat * Math.PI / 180;
            double PointLng = Point.Lng * Math.PI / 180;

            // Matlab code
            double e2 = Math.Pow(ex, 2); //self.Eccentricity^2;

            double s1 = Math.Sin(InitialPointLat); //sinfun(phi1); phi1 lat0
            double c1 = Math.Cos(InitialPointLat); // cosfun(phi1);

            double s2 = Math.Sin(PointLat); // sinfun(phi2);
            double c2 = Math.Cos(PointLat); //cosfun(phi2);

            double p1 = c1 * Math.Cos(InitialPointLng); //c1 .* cosfun(lambda1);
            double p2 = c2 * Math.Cos(PointLng); //c2 .* cosfun(lambda2);

            double q1 = c1 * Math.Sin(InitialPointLng); // c1 .* sinfun(lambda1);
            double q2 = c2 * Math.Sin(PointLng); //c2 .* sinfun(lambda2);

            double w1 = 1 / Math.Sqrt(1 - e2 * Math.Pow(s1, 2)); // 1 ./ sqrt(1 - e2 * s1.^2);
            double w2 = 1 / Math.Sqrt(1 - e2 * Math.Pow(s2, 2)); // 1 / sqrt(1 - e2 * s2.^2);

            double deltaX = a * (p2 * w2 - p1 * w1) + (h * p2 - h * p1); //self.a * (p2 .* w2 - p1 .* w1) + (h2 .* p2 - h1 .* p1);
            double deltaY = a * (q2 * w2 - q1 * w1) + (h * q2 - h * q1); //self.a * (q2 .* w2 - q1 .* w1) + (h2 .* q2 - h1 .* q1);
            double deltaZ = (1 - e2) * a * (s2 * w2 - s1 * w1) + (h * s2 - h * s1); //(1 - e2) * self.a * (s2 .* w2 - s1 .* w1) + (h2 .* s2 - h1 .* s1);

            double cosPhi = Math.Cos(InitialPointLat); // cosfun(lat0); lat0 initialpoint.lat
            double sinPhi = Math.Sin(InitialPointLat); //sinfun(lat0);
            double cosLambda = Math.Cos(InitialPointLng); //cosfun(lon0);
            double sinLambda = Math.Sin(InitialPointLng); //sinfun(lon0);

            double t = cosLambda * deltaX + sinLambda * deltaY; // cosLambda .* u + sinLambda .* v; u = deltaX
            double uEast = -sinLambda * deltaX + cosLambda * deltaY; //-sinLambda .* u + cosLambda .* v;

            double wUp = cosPhi * t + sinPhi * deltaZ; //cosPhi .* t + sinPhi .* w;
            double vNorth = -sinPhi * t + cosPhi * deltaZ; // -sinPhi .* t + cosPhi .* w;

            Point3D p = new Point3D((float)uEast, (float)vNorth, (float)wUp);



            return p;
        }

        public Point3D Geodetic2ENU(PointLatLngAlt Point, PointLatLngAlt InitialPoint)
        {
            double a = 6378137;     // Semi-major axis
            double ex = 0.08181919; // Excentricity
            
            double N_phi = 0;
            double x_ecef = 0;
            double y_ecef = 0;
            double z_ecef = 0;

            double a_h = 48.032537299811120;
            double a_v = 57.210092131377180;

            double phi_r = InitialPoint.Lat;//Convert.ToDouble(txtInitX.Text);
            double lambda_r = InitialPoint.Lng;//Convert.ToDouble(txtInitY.Text);
            double h_r = InitialPoint.Alt;        // Height of Mexico City

            double N_phi_r = a / Math.Sqrt(1 - Math.Pow(ex, 2) * Math.Pow(Math.Sin(phi_r), 2));

            double x_ecef_r = (N_phi_r / a_v + h_r) * Math.Cos(phi_r) * Math.Cos(lambda_r);
            double y_ecef_r = (N_phi_r / a_v + h_r) * Math.Cos(phi_r) * Math.Sin(lambda_r);
            double z_ecef_r = ((N_phi_r / a_v) * (1 - Math.Pow(ex, 2)) + h_r) * Math.Sin(phi_r);

            /*double e2 = Math.Pow(ex, 2);
            double sinx = Math.Sin(lat);
            double sinx2 = Math.Pow(sinx, 2);
            double e2sinx = e2 * sinx2;
            double sqrte2sinx = Math.Sqrt(1 - e2sinx);
            double N_phi1 = a / (sqrte2sinx);
            */
            N_phi = a / (Math.Sqrt(1 - Math.Pow(ex, 2) * Math.Pow(Math.Sin(Point.Lat), 2)));

            double cosx = Math.Cos(Point.Lat);
            double cosy = Math.Cos(Point.Lng);

            x_ecef = (N_phi / a_v + Point.Alt) * Math.Cos(Point.Lat) * Math.Cos(Point.Lng);
            y_ecef = (N_phi / a_v + Point.Alt) * Math.Cos(Point.Lat) * Math.Sin(Point.Lng);
            z_ecef = (N_phi / a_v * (1 - Math.Pow(ex, 2)) + Point.Alt) * Math.Sin(Point.Lat);

            Matrix R = new Matrix(3, 3);
            R[0, 0] = -Math.Sin(lambda_r);
            R[0, 1] = Math.Cos(lambda_r);
            R[0, 2] = 0;

            R[1, 0] = -Math.Sin(phi_r) * Math.Cos(lambda_r);
            R[1, 1] = -Math.Sin(phi_r) * Math.Sin(lambda_r);
            R[1, 2] = Math.Cos(phi_r);

            R[2, 0] = Math.Cos(phi_r) * Math.Cos(lambda_r);
            R[2, 1] = Math.Cos(phi_r) * Math.Sin(lambda_r);
            R[2, 2] = Math.Sin(phi_r);

            Matrix P = new Matrix(3, 1);
            P[0, 0] = x_ecef - x_ecef_r;
            P[1, 0] = y_ecef - y_ecef_r;
            P[2, 0] = z_ecef - z_ecef_r;

            Matrix enu = new Matrix(3, 1);

            enu = R * P;

            PointF _p = new PointF((float)enu[0, 0], (float)enu[1, 0]);


            double InitialPointLat = InitialPoint.Lat * Math.PI / 180;
            double InitialPointLng = InitialPoint.Lng * Math.PI / 180;
            double PointLat = Point.Lat * Math.PI / 180;
            double PointLng = Point.Lng * Math.PI / 180;

            // Matab code
            double e2 = Math.Pow(ex, 2); //self.Eccentricity^2;

            double s1 = Math.Sin(InitialPointLat); //sinfun(phi1); phi1 lat0
            double c1 = Math.Cos(InitialPointLat); // cosfun(phi1);

            double s2 = Math.Sin(PointLat); // sinfun(phi2);
            double c2 = Math.Cos(PointLat); //cosfun(phi2);

            double p1 = c1 * Math.Cos(InitialPointLng); //c1 .* cosfun(lambda1);
            double p2 = c2 * Math.Cos(PointLng); //c2 .* cosfun(lambda2);

            double q1 = c1 * Math.Sin(InitialPointLng); // c1 .* sinfun(lambda1);
            double q2 = c2 * Math.Sin(PointLng); //c2 .* sinfun(lambda2);

            double w1 = 1 / Math.Sqrt(1 - e2 * Math.Pow(s1, 2)); // 1 ./ sqrt(1 - e2 * s1.^2);
            double w2 = 1 / Math.Sqrt(1 - e2 * Math.Pow(s2, 2)); // 1 / sqrt(1 - e2 * s2.^2);

            double h1 = InitialPoint.Alt;
            double h2 = Point.Alt;

            double deltaX = a * (p2 * w2 - p1 * w1) + (h2 * p2 - h1 * p1); //self.a * (p2 .* w2 - p1 .* w1) + (h2 .* p2 - h1 .* p1);
            double deltaY = a * (q2 * w2 - q1 * w1) + (h2 * q2 - h1 * q1); //self.a * (q2 .* w2 - q1 .* w1) + (h2 .* q2 - h1 .* q1);
            double deltaZ = (1 - e2) * a * (s2 * w2 - s1 * w1) + (h2 * s2 - h1 * s1); //(1 - e2) * self.a * (s2 .* w2 - s1 .* w1) + (h2 .* s2 - h1 .* s1);

            double cosPhi = Math.Cos(InitialPointLat); // cosfun(lat0); lat0 initialpoint.lat
            double sinPhi = Math.Sin(InitialPointLat); //sinfun(lat0);
            double cosLambda = Math.Cos(InitialPointLng); //cosfun(lon0);
            double sinLambda = Math.Sin(InitialPointLng); //sinfun(lon0);

            double t = cosLambda * deltaX + sinLambda * deltaY; // cosLambda .* u + sinLambda .* v; u = deltaX
            double uEast = -sinLambda * deltaX + cosLambda * deltaY; //-sinLambda .* u + cosLambda .* v;

            double wUp = cosPhi * t + sinPhi * deltaZ; //cosPhi .* t + sinPhi .* w;
            double vNorth = -sinPhi * t + cosPhi * deltaZ; // -sinPhi .* t + cosPhi .* w;

            Point3D p = new Point3D((float)uEast, (float)vNorth, (float)wUp);



            return p;
        }

        private PointF Geodetic2ECEF(PointLatLng Point)
        {
            double a = 6378137;     // Semi-major axis
            double ex = 0.08181919; // Excentricity
            double h = 2241;        // Height of Mexico City
            double N_phi = 0;
            double x_ecef = 0;
            double y_ecef = 0;
            double z_ecef = 0;

            double a_h = 48.032537299811120;
            double a_v = 57.210092131377180;

            /*double e2 = Math.Pow(ex, 2);
            double sinx = Math.Sin(lat);
            double sinx2 = Math.Pow(sinx, 2);
            double e2sinx = e2 * sinx2;
            double sqrte2sinx = Math.Sqrt(1 - e2sinx);
            double N_phi1 = a / (sqrte2sinx);
            */
            N_phi = a / (Math.Sqrt(1 - Math.Pow(ex, 2) * Math.Pow(Math.Sin(Point.Lat), 2)));

            double cosx = Math.Cos(Point.Lat);
            double cosy = Math.Cos(Point.Lng);

            x_ecef = (N_phi / a_v + h) * Math.Cos(Point.Lat) * Math.Cos(Point.Lng);
            y_ecef = (N_phi / a_v + h) * Math.Cos(Point.Lat) * Math.Sin(Point.Lng);
            z_ecef = (N_phi / a_v * (1 - Math.Pow(ex, 2)) + h) * Math.Sin(Point.Lat);

            PointF p = new PointF((float)x_ecef, (float)y_ecef);
            return p;
        }
    }
}
