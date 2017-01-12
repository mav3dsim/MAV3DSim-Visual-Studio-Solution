using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAV3DSim.Utils
{
    public class Quaternion
    {
        public Quaternion(double w, double x, double y, double z)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }


        //
        // Summary:
        //     Transforms this Quaternion into its conjugate.
        public Quaternion Conjugate()
        {
            return new Quaternion(W, -X, -Y, -Z);
        }

        public static Quaternion Conjugate(Quaternion q)
        {
            return new Quaternion(q.W, -q.X, -q.Y, -q.Z);
        }

        public double Norm()
        {
            return Math.Sqrt(Math.Pow(W, 2) + Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
        }

        public double Norm(Quaternion q)
        {
            return Math.Sqrt(Math.Pow(q.W, 2) + Math.Pow(q.X, 2) + Math.Pow(q.Y, 2) + Math.Pow(q.Z, 2));
        }

        public static Quaternion Multiply(Quaternion q1, Quaternion q2)
        {

            return new Quaternion(
                q1.W * q2.W - q1.X * q2.X - q1.Y * q2.Y - q1.Z * q2.Z,
                q1.W * q2.X + q1.X * q2.W + q1.Y * q2.Z - q1.Z * q2.Y,
                q1.W * q2.Y - q1.X * q2.Y + q1.Y * q2.W + q1.Z * q2.X,
                q1.W * q2.Z + q1.X * q2.Y - q1.Y * q2.X + q1.Z * q2.W);
  
        }

        public Quaternion Multiply(Quaternion q2)
        {

            return new Quaternion(
                W * q2.W - X * q2.X - Y * q2.Y - Z * q2.Z,
                W * q2.X + X * q2.W + Y * q2.Z - Z * q2.Y,
                W * q2.Y - X * q2.Y + Y * q2.W + Z * q2.X,
                W * q2.Z + X * q2.Y - Y * q2.X + Z * q2.W);

        }

      

        public Quaternion Inverse()
        {
            double w = this.Conjugate().W / this.Norm();
            double x = this.Conjugate().X / this.Norm();
            double y = this.Conjugate().Y / this.Norm();
            double z = this.Conjugate().Z / this.Norm();
            return new Quaternion(w, x, y, z);
        }

        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
            return Multiply(q1, q2);
        }

        public float[] ToFloat()
        {
            return new float[] { (float)W, (float)X, (float)Y, (float)Z };
        }

        



        public double W { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
