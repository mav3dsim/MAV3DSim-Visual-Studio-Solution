using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using MAV3DSim.Utils;

namespace MAV3DSim.Controller
{
    public class ControllerLyap3D
    {
        MapTools mapTools = new MapTools();
        MathTools mathTools = new MathTools();
        Point3D pPoint;
        Point3D qPoint;
        double psi_s;
        double psi;
        double theta_s;
        double k_s;
        double V;

        double phi;
        double theta;
        double s_dot;

        double ex;
        double ey;
        double ez;
        double e_psi;

        double kx; // = 1;
        double ky; // =.1;
        double kz; // =1.5;
        double psi_a;
        double kdelta; // = .1;

        // Gravity
        double g;

        public ControllerLyap3D()
        {
            kx = 1.6; // = 1;
            ky = .1; // =.1;
            kz = 0.75; // =1.5;
            psi_a = 0.5; 
            kdelta = 0.1; // = .1;
            g = 9.81;

        }
        public void ControlStep(double dt)
        {
            psi = Math.PI / 2 - psi;
            psi_s = Math.PI / 2 - psi_s;

            if (psi > Math.PI)
                psi = psi - 2 * Math.PI;
            if (psi_s > Math.PI)
                psi_s = psi_s - 2 * Math.PI;


            
            double sint_s = Math.Sin(theta_s);
            
            double cost_s = Math.Sin(theta_s);

            // Rotation matrix from Inertial to Frenet
            Matrix R = new Matrix(3, 3);

            R[0, 0] = Math.Cos(theta_s)*Math.Cos(psi_s);
            R[0, 1] = Math.Cos(theta_s)*Math.Sin(psi_s);
            R[0, 2] = -Math.Sin(theta_s);
            R[1, 0] = -Math.Sin(psi_s);
            R[1, 1] = Math.Cos(psi_s);
            R[1, 2] = 0;
            R[2, 0] = Math.Sin(theta_s) * Math.Cos(psi_s);
            R[2, 1] = Math.Sin(theta_s) * Math.Sin(psi_s);
            R[2, 2] = Math.Cos(theta_s);

            Matrix p = new Matrix(3, 1);
            p[0, 0] = pPoint.X;
            p[1, 0] = pPoint.Y;
            p[2, 0] = pPoint.Z;

            Matrix q = new Matrix(3, 1);
            q[0, 0] = qPoint.X;
            q[1, 0] = qPoint.Y;
            q[2, 0] = qPoint.Z;



            Matrix d = R * (p - q);

            // Calculate position error and angular error
            ex = d[0,0];
            ey = d[1, 0];
            ez = d[2, 0];
            // calculate angular error e_psi =  psi-psi_s;
            double psi_tilde = psi - psi_s;
            if (psi_tilde > Math.PI)
                psi_tilde -= 2 * Math.PI;
            else if (psi_tilde < -Math.PI)
                psi_tilde += 2 * Math.PI;
            e_psi = MathTools.AngleDiff(psi,psi_s);
            e_psi = -psi_tilde;


           
           
            //Kx = 1;
            //Ky=.1;
            //Kz=1.5;
            //double psi_a=1/2;
            //%Kdelta = .1;
            
            



            double theta_1 = kz*(ez)/(V*Math.Cos(theta_s));
            if (theta_1>1)
                theta_1=1;
            else if (theta_1<-1)
                theta_1=-1;
                
            

            theta=Math.Asin(theta_1);

            double sint = Math.Sin(theta);
            double cost = Math.Sin(theta);
            
            
            //double alpha = V*Math.Sin(theta)*Math.Cos(theta_s)+V*Math.Cos(theta)*Math.Cos(theta_s)*Math.Cos(e_psi) + V*ey/ex*Math.Cos(theta)*Math.Sin(e_psi)+V*ez/ex*Math.Cos(theta)*Math.Sin(theta_s)*Math.Sin(e_psi);
            double alpha = V * sint * sint_s + V * cost * cost_s * Math.Cos(e_psi);
            double delta_ey = -psi_a*(Math.Exp(2*kdelta*ey)-1)/(Math.Exp(2*kdelta*ey)+1);
            double delta_ey_dot = -(4*psi_a*kdelta*Math.Exp(2*kdelta*ey))/Math.Pow(Math.Exp(2*kdelta*ey)+1,2);

            s_dot = (kx * ex + alpha);
            s_dot = 0;

            //double beta = -k_s*s_dot+delta_ey_dot*V*Math.Cos(theta)*Math.Sin(e_psi)-(ey*delta_ey)/(ey-delta_ey);
            double pBeta = (ey * V * cost * Math.Sin(e_psi) + V * ez * cost * sint_s * Math.Cos(e_psi) - V * ey * Math.Sin(delta_ey)) / (ey - delta_ey);
            double beta = -k_s * s_dot - delta_ey_dot * (k_s * s_dot * ex * cost_s + k_s * s_dot * ez * sint_s + V * cost * Math.Sin(e_psi)) + pBeta;

            

            phi=Math.Atan(V/g*(-beta-ky*(e_psi-delta_ey)));
            if (phi>0.8)
                phi=0.8;
            else if(phi<-0.8)
                    phi=-0.8;
                
            

            if (theta>0.8)
                theta=0.8;
            else if (theta<-0.8)
                    theta=-0.8;
            
            if(s_dot>20)
                s_dot=20;
            

        }

        
        public Point3D ArcraftPoint
        {
            get { return pPoint; }
            set { pPoint = value; }
        }
        public Point3D CurvePoint
        {
            get { return qPoint; }
            set { qPoint = value; }
        }
        
        public double Psi_s
        {
            get{return psi_s;}
            set{psi_s=value;}
        }
        public double Psi
        {
            get { return psi; }
            set { psi = value; }
        }
        public double Theta_s
        {
            get { return theta_s; }
            set { theta_s = value; }
        }
        public double Curvature
        {
            get { return k_s; }
            set { k_s = value; }
        }
        public double Vel
        {
            get { return V; }
            set { V = value; }
        }
        public double S_dot
        {
            get { return s_dot; }
        }
        public double Phi
        {
            get { return phi; }
        }
        public double Theta
        {
            get { return theta; }
        }
        public double Ex
        {
            get { return ex; }
        }
        public double Ey
        {
            get { return ey; }
        }
        public double Ez
        {
            get { return ez; }
        }
        public double E_psi
        {
            get { return e_psi; }
        }


        public void UpdateGains(double Kx, double Ky, double Kz, double Kdelta, double Psi_a)
        {
            kx = Kx;
            ky = Ky;
            kz = Kz;
            kdelta = Kdelta;
            psi_a = Psi_a;
        }

    }


	
}
