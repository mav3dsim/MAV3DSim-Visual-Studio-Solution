using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MAV3DSim;

namespace MAV3DSim.Controller
{
    class UAVControlQuaternion
    {

        PID altitud;
        //PID heading;
        PIDFF speed;
        PIDQuaternion pidQuaternion;
        PIDQuaternion pidYawQuaternion;
        Utils.MathTools mathTools;
        double roll;
        double pitch;
        double yaw;

        double rollSetPoint;
        double pitchSetPoint;
        double yawSetPoint;



        public UAVControlQuaternion()
        {
            pidQuaternion = new PIDQuaternion();
            pidYawQuaternion = new PIDQuaternion();
            mathTools = new Utils.MathTools();
            altitud = new PID();
            //heading = new PID();
            speed = new PIDFF();
            altitud.HighSatValue = 0.5;
            altitud.LowSatValue = -0.5;
            //heading.HighSatValue = 0.5;
            //heading.LowSatValue = -0.5;
            speed.HighSatValue = 1;
            speed.LowSatValue = 0;

        }

        public void StartRollPitchControl()
        {
            pidQuaternion.Init();   
        }

        public void startYawControl()
        {
            pidYawQuaternion.Init();
        }

        public void startAltitudControl()
        {
            //altitud.SetPoint = 0;
            altitud.Init();
        }

        public void startSpeedControl()
        {
            //altitud.SetPoint = 0;
            //heading.Init();
            
            speed.Init();
        }

        public void stopControl()
        {
            
        }

        public void updateRollValue(double value){ roll = value;}
        public void updatePitchValue(double value){pitch = value;}
        public void updateAltitudValue(double value) { altitud.Value = value; }
        //public void updateHeadingValue(double value) { heading.Value = value; }
        public void updateYawValue(double value) { yaw = value; }
        public void updateSpeedValue(double value) { speed.Value = value; }

        public void updateRollSetPoint(double value) { rollSetPoint = value; }
        public double getRollSetPoint() { return rollSetPoint; }
        public double getRollError() { return pidQuaternion.RollError; }
        public void setRollSetPoint(double rollSetPoint) { this.rollSetPoint = rollSetPoint; }
        public void setRollError(double RollError) { pidQuaternion.RollError = RollError; }


        public void updatePitchSetPoint(double value) { pitchSetPoint = value; }
        public double getPitchSetPoint() { return pitchSetPoint; }
        public double getPitchError() { return pidQuaternion.PitchError; }
        public void setPitchSetPoint(double pitchSetPoint) { this.pitchSetPoint = pitchSetPoint; }
        public void setPitchError(double pitchError) { pidQuaternion.PitchError = pitchError; }

        public void updateAltitudSetPoint(double AltitudeValue) { altitud.SetPoint = AltitudeValue; }
        public double getAltitudSetPoint() { return altitud.SetPoint; }
        public double getYawError() { return pidYawQuaternion.YawError; }

        public void updateYawSetPoint(double value) { yawSetPoint = value; }
        public double getYawSetPoint() { return yawSetPoint; }

        //public void updateHeadingSetPoint(double value) { heading.SetPoint = value; }
        //public double getHeadingSetPoint() { return heading.SetPoint; }

        public void updateSpeedSetPoint(double SpeedValue) { speed.SetPoint = SpeedValue; }
        public double getSpeedSetPoint() { return speed.SetPoint; }

        public void updateRollGains(double Kp, double Ki, double Kd)
        {
            pidQuaternion.updateRollGains(Kp, Ki, Kd);
            pidYawQuaternion.updateRollGains(Kp, Ki, Kd);
        }

        public void updatePitchGains(double Kp, double Ki, double Kd)
        {
            pidQuaternion.updatePitchGains(Kp, Ki, Kd);
            pidYawQuaternion.updatePitchGains(Kp, Ki, Kd);
        }

        public void updateYawGains(double Kp, double Ki, double Kd)
        {
            pidQuaternion.updateYawGains(Kp, Ki, Kd);
            pidYawQuaternion.updateYawGains(Kp, Ki, Kd);
        }

        public void updateDerivatives(double p, double q, double r)
        {
            pidQuaternion.updateDerivatives(p, q, r);
            pidYawQuaternion.updateDerivatives(p, q, r);
        }

        public void updateAltitudeGains(double Kp, double Ki, double Kd)
        {
            altitud.Kp = Kp;
            altitud.Ki = Ki;
            altitud.Kd = Kd;
        }

        /*public void updateHeadingGains(double Kp, double Ki, double Kd)
        {
            heading.Kp = Kp;
            heading.Ki = Ki;
            heading.Kd = Kd;
        }*/

        public void updateSpeedGains(double Kp, double Ki, double Kd,double FF)
        {
            speed.Kp = Kp;
            speed.Ki = Ki;
            speed.Kd = Kd;
            speed.FF = FF;
        }

        public void ControlStep(double Dt)
        {
            if (AltitudControl )
            {
                altitud.ControlStep(Dt);
                pitchSetPoint =altitud.u;// *Math.PI / 2;  
                speed.ControlStep(Dt);
            }

           

            

            if (HeadingControl)
            {
                pidYawQuaternion.updateQuaternionValue(mathTools.Euler2Quaternion(0, 0, yaw));
                pidYawQuaternion.updateRollPitchYawValue(0, 0, yaw);
                pidYawQuaternion.updateQuaternionSetPoint(mathTools.Euler2Quaternion(0, 0, yawSetPoint));
                pidYawQuaternion.updateRollPitchYawSetPoint(0, 0, yawSetPoint);
                pidYawQuaternion.ControlStep(Dt);

                //rollSetPoint = pidYawQuaternion.YawControl;
                speed.ControlStep(Dt);
            }

            
            pidQuaternion.updateQuaternionValue(mathTools.Euler2Quaternion(roll, pitch, 0));
            pidQuaternion.updateRollPitchYawValue(roll, pitch, 0);
            pidQuaternion.updateQuaternionSetPoint(mathTools.Euler2Quaternion(rollSetPoint, pitchSetPoint, 0));
            pidQuaternion.updateRollPitchYawSetPoint(rollSetPoint, pitchSetPoint, 0);
            pidQuaternion.ControlStep(Dt);

            
        }

        public double getRollControl()
        {
            return pidQuaternion.RollControl;
        }

        public double getPitchControl()
        {
            return pidQuaternion.PitchControl;
        }

        public double getYawControl()
        {
            return pidYawQuaternion.YawControl;
        }

        public double getAltitudControl()
        {
            return altitud.u;
        }
        public double getHeadingControl()
        {
            return pidYawQuaternion.YawControl;
        }

        public double getSpeedControl()
        {
            return speed.u;
        }

        public bool AltitudControl { get; set; }
        public bool HeadingControl { get; set; }

        public Utils.Quaternion GetQuaternionSetPoint()
        {
            return mathTools.Euler2Quaternion(rollSetPoint, pitchSetPoint, yawSetPoint);
        }

    }

}
