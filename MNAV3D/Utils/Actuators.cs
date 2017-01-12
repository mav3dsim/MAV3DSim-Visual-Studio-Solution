using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAV3DSim
{
    
    class Actuators
    {
        
        
        private byte[] encode;
        public Actuators()
        {
            encode = new byte[0];
        }

        public byte[] Encode()
        {
            try
            {
                long[] cnt_cmd = new long[4];
                byte[] data = new byte[] { 0x55, 0x55, 0x73, 0x73, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x00, 0x00 };
                short i = 0;
                long sum = 0;

                double ch0, ch1, ch2, ch3;
                double a = (this.Elevator + this.Aileron) ;
                ch0 = (this.Elevator + this.Aileron) ;
                ch1 = (-this.Elevator + this.Aileron) ;
                ch2 = this.Throttle;
                ch3 = -this.Rudder;

                cnt_cmd[0] = Convert.ToInt32(0xffff * ch0 + 0x7fff);
                cnt_cmd[1] = Convert.ToInt32(0xffff * ch1 + 0x7fff);
                cnt_cmd[2] = Convert.ToInt32(0xffff * ch2);
                cnt_cmd[3] = Convert.ToInt32(0xffff * ch3 + 0x7fff);

                data[0] = 0x55;
                data[1] = 0x55;
                data[2] = 0x53;
                data[3] = 0x53;

                //aileron ch#0,elevator ch#1,throttle ch#2
                //aileron
                data[4] = (byte)(cnt_cmd[0] >> 8);
                data[5] = (byte)cnt_cmd[0];
                //elevator
                data[6] = (byte)(cnt_cmd[1] >> 8);
                data[7] = (byte)cnt_cmd[1];
                //throttle
                data[8] = (byte)(cnt_cmd[2] >> 8);
                data[9] = (byte)cnt_cmd[2];

                //throttle
                data[10] = (byte)(cnt_cmd[3] >> 8);
                data[11] = (byte)cnt_cmd[3];

                //checksum
                sum = 0xa6; //0x53+0x53
                for (i = 4; i < 22; i++) sum += data[i];

                data[22] = (byte)(sum >> 8);
                data[23] = (byte)sum;
                encode = data;
                return encode;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in Actuaros->Encode(): " + e.ToString());
                return new byte[0];
            }
        }



        public byte[] EncodeMavlink()
        {
            return new byte[]{ };
        }



        public double Aileron
        {
            get;
            set;
        }

        public double Elevator
        {
            get;
            set;
        }

        public double Throttle
        {
            get;
            set;
        }

        public double Rudder
        {
            get;
            set;
        }

        


    }
}
