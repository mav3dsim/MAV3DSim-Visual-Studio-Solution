using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MAV3DSim.Controller
{
    class PIDFF
    {
        
        private double prevError;
        private double prevu;
        private double integral;
        private double integralLimit;
        
        private bool stop;
        private Thread control;
        private DateTime then;
        


        public PIDFF()
        {
        }

        public PIDFF(double Kp, double Ki, double Kd,double FF)
        {
            this.Kp = Kp;
            this.Kd = Kd;
            this.Ki = Ki;
            this.FF=FF;
        }

        public void Init()
        {
            Error = 0;
            integral = 0;
            prevError = 0;
            Derivative = 0;
            then = DateTime.Now;
            useExternDerivative = false;
            useExternError = false;
            integralLimit = 10;
        }

        public void ControlStep(double Dt)
        {
                /*
                 * Pseudocode from Wikipedia
                 * 
                    previous_error = 0
                    integral = 0 
                start:
                    error = setpoint - PV(actual_position)
                    integral = integral + error*dt
                    derivative = (error - previous_error)/dt
                    output = Kp*error + Ki*integral + Kd*derivative
                    previous_error = error
                    wait(dt)
                    goto start
                 */
            try
            {
                
                // calculate the difference between the desired value and the actual value
                if (!useExternError)
                    Error = SetPoint - Value;
                

                // track error over time, scaled to the timer interval
                integral = integral + (Error * Dt);
                // determin the amount of change from the last time checked
                if(!useExternDerivative)
                    Derivative = (Error - prevError) / Dt;

                if (integral > integralLimit)
                    integral = integralLimit;
                else
                    if (integral < -integralLimit)
                        integral = -integralLimit;

                // calculate how much drive the output in order to get to the 
                // desired setpoint. 
                u = Sat((Kp * Error) + (Ki * integral) + (Kd * Derivative) + FF);

                if (Double.IsNaN(u))
                {
                    integral = 0;
                    u = prevu;
                    //throw new NotFiniteNumberException();
                }

                

                // remember the error for the next time around.
                prevError = Error;
                prevu = u;
            }
            catch (Exception e) 
            { 
                
                Console.WriteLine(e.ToString()); 
            }
            
        }

       

        private double Sat(double value)
        {
            if (value >= HighSatValue)
                return HighSatValue;
            if (value <= LowSatValue)
                return LowSatValue;
            return value;
        }

        public double Kp { get; set; }
        public double Kd { get; set; }
        public double Ki { get; set; }
        public double FF { get; set; }
        public double SetPoint { get; set; }
        public double Value { get; set; }
        public double HighSatValue { get; set; }
        public double LowSatValue { get; set; }
        public double Error { get; set; }
        public double Derivative { get; set; }
        public double u;

        

        public bool useExternDerivative { get; set; }
        public bool useExternError { get; set; }

    }
}
