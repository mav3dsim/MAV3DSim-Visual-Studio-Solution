using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace MAV3DSim.Utils
{
    class MathTools
    {
        public MathTools()
        {
        }

        public double[] Quaternion2Euler(Quaternion q)
        {
            double[] euler = new double[3];

            double r11 = 2 * (q.X * q.Y + q.W * q.Z);
            double r12 = Math.Pow(q.W, 2) + Math.Pow(q.X, 2) - Math.Pow(q.Y, 2) - Math.Pow(q.Z, 2);
            double r21 = -2 * (q.X * q.Z - q.W * q.Y);
            double r31 = 2 * (q.Y * q.Z + q.W * q.X);
            double r32 = Math.Pow(q.W, 2) - Math.Pow(q.X, 2) - Math.Pow(q.Y, 2) + Math.Pow(q.Z, 2);

            euler[0] = Math.Atan2(r11, r12);
            euler[1] = Math.Asin(r21);
            euler[2] = Math.Atan2(r31, r32);

            return euler;
        }

        public Quaternion Euler2Quaternion(double roll, double pitch, double yaw)
        {
            double c1 = Math.Cos(roll / 2);
            double c2 = Math.Cos(pitch / 2);
            double c3 = Math.Cos(yaw / 2);
            double s1 = Math.Sin(roll / 2);
            double s2 = Math.Sin(pitch / 2);
            double s3 = Math.Sin(yaw / 2);

            double w = c1 * c2 * c3 + s1 * s2 * s3;
            double x = s1 * c2 * c3 - c1 * s2 * s3;
            double y = c1 * s2 * c3 + s1 * c2 * s3;
            double z = c1 * c2 * s3 - s1 * s2 * c3;



            return new Quaternion(w, x, y, z);
        }

        // This function is in radians.
        public static float AngleDiff(float Angle1, float Angle2)
        {
            return Math.Abs(MathHelper.WrapAngle(Angle1 - Angle2));
        }
        public static double AngleDiff(double Angle1, double Angle2)
        {
            return (double)Math.Abs(MathHelper.WrapAngle((float)(Angle1 - Angle2)));
        }
    }
}
