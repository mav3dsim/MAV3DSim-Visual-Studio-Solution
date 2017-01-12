using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;



namespace MAV3DSim.Controller
{
    class PIDQuaternion
    {
        PID roll;
        PID pitch;
        PID yaw;
        Utils.Quaternion SetPoint;
        Utils.Quaternion Value;
        Utils.MathTools mathTools = new Utils.MathTools();
        double[] pqr;
        


        public PIDQuaternion()
        {
            roll = new PID();
            pitch = new PID();
            yaw = new PID();
        }

        public void Init()
        {
            roll.Init();
            roll.useExternDerivative = true;
            roll.useExternError = true;
            pitch.Init();
            pitch.useExternDerivative = true;
            pitch.useExternError = true;
            yaw.Init();
            yaw.useExternDerivative = true;
            yaw.useExternError = true;

            roll.HighSatValue = 0.5;
            roll.LowSatValue = -0.5;
            pitch.HighSatValue = 0.5;
            pitch.LowSatValue = -0.5;
            yaw.HighSatValue = 0.5;
            yaw.LowSatValue = -0.5;


        }

        public void ControlStep(double Dt)
        {
            Utils.Quaternion error = SetPoint.Multiply(Value.Conjugate());
            double[] euler = mathTools.Quaternion2Euler(error);
            roll.Error=euler[2];
            pitch.Error = euler[1];
            yaw.Error = euler[0];

            roll.Derivative = pqr[0];
            pitch.Derivative = pqr[1];
            yaw.Derivative = pqr[2];

            roll.ControlStep(Dt);
            pitch.ControlStep(Dt);
            yaw.ControlStep(Dt);

            RollControl = roll.u;
            PitchControl = pitch.u;
            YawControl = yaw.u;
        }


        public void updateQuaternionSetPoint(Utils.Quaternion SetPoint)
        {
            this.SetPoint = SetPoint;
        }

        public void updateRollPitchYawSetPoint(double rollSetPoint, double pitchSetPoint, double yawSetPoint)
        {
            this.roll.SetPoint= rollSetPoint;
            this.pitch.SetPoint = pitchSetPoint;
            this.yaw.SetPoint = yawSetPoint;
        }

        public Utils.Quaternion getQuaternionSetPoint()
        {
            return SetPoint;
        }

        public void updateQuaternionValue(Utils.Quaternion Value)
        {
            this.Value = Value;
        }

        public void updateRollPitchYawValue(double roll, double pitch, double yaw)
        {
            this.roll.Value = roll;
            this.pitch.Value = pitch;
            this.yaw.Value = yaw;

        }

        public void updateRollGains(double Kp, double Ki, double Kd)
        {
            roll.Kp = Kp;
            roll.Ki = Ki;
            roll.Kd = Kd;
        }

        public void updatePitchGains(double Kp, double Ki, double Kd)
        {
            pitch.Kp = Kp;
            pitch.Ki = Ki;
            pitch.Kd = Kd;
        }

        public void updateYawGains(double Kp, double Ki, double Kd)
        {
            yaw.Kp = Kp;
            yaw.Ki = Ki;
            yaw.Kd = Kd;
        }

        public void updateDerivatives(double p, double q, double r)
        {
            this.pqr = new double[] { p, q, r };
        }

        public double RollControl { get; set; }
        public double PitchControl { get; set; }
        public double YawControl { get; set; }

        public double RollError { get { return roll.Error; } set { roll.Error = value; } }
        public double PitchError { get { return pitch.Error; } set { pitch.Error = value; } }
        public double YawError { get { return yaw.Error; } set { yaw.Error = value; } }
       




    }
}
