using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAV3DSim.Utils
{
    public class IMU
    {

        private double _ax,_ay,_az;
        private double _p, _q, _r;
        private double _mx, _my,_mz;

        private double _vn, _ve, _vd;
        private double _lon, _lat, _alt, _LatInit, _LonInit, _AltInit;
        private double _ps, _pt;
        private double _itow;
        private double _phi,_the,_psi;

        private double _lidar;

        private int time;

        private Dictionary<string, double> plotVariables = new Dictionary<string, double>();

        MavLinkNet.UasHilSensor hilSensor = new MavLinkNet.UasHilSensor();
        MavLinkNet.UasHilStateQuaternion attitude = new MavLinkNet.UasHilStateQuaternion();
        MavLinkNet.UasHilGps gps = new MavLinkNet.UasHilGps();

        MathTools mathTool = new MathTools();
        private bool updated;
        public IMU()
        {
            plotVariables.Add("Ax", _ax);
            plotVariables.Add("Ay", _ay);
            plotVariables.Add("Az", _az);
            plotVariables.Add("P", _p);
            plotVariables.Add("Q", _q);
            plotVariables.Add("R", _r);
            plotVariables.Add("Mx", _mx);
            plotVariables.Add("My", _my);
            plotVariables.Add("Mz", _mz);
            plotVariables.Add("Pitot", _pt);
            plotVariables.Add("Vn", _vn);
            plotVariables.Add("Ve", _ve);
            plotVariables.Add("Vd", _vd);
            plotVariables.Add("Lat", _lat);
            plotVariables.Add("Lon", _lon);
            plotVariables.Add("Alt", _alt);
            plotVariables.Add("Phi", _phi);
            plotVariables.Add("The", _the);
            plotVariables.Add("Psi", _psi);
            plotVariables.Add("Lidar", _lidar);


            updated = true;
        }

        public void decode(MavLinkNet.UasMessage msg)
        {

            
            if(msg.MessageId == MavLinkNet.UasHighresImu.MId)
            {
                _ax = ((MavLinkNet.UasHighresImu)msg).Xacc;
                _ay = ((MavLinkNet.UasHighresImu)msg).Yacc;
                _az = ((MavLinkNet.UasHighresImu)msg).Zacc;

                _p = ((MavLinkNet.UasHighresImu)msg).Xgyro;
                _q = ((MavLinkNet.UasHighresImu)msg).Ygyro;
                _r = ((MavLinkNet.UasHighresImu)msg).Zgyro;

                _mx = ((MavLinkNet.UasHighresImu)msg).Xmag;
                _my = ((MavLinkNet.UasHighresImu)msg).Ymag;
                _mz = ((MavLinkNet.UasHighresImu)msg).Zmag;

                _pt = ((MavLinkNet.UasHighresImu)msg).DiffPressure*100;
                _ps = ((MavLinkNet.UasHighresImu)msg).AbsPressure;
            } else if(msg.MessageId == MavLinkNet.UasAttitude.MId)
            {
                _phi = ((MavLinkNet.UasAttitude)msg).Roll;
                _the = ((MavLinkNet.UasAttitude)msg).Pitch;
                _psi = ((MavLinkNet.UasAttitude)msg).Yaw;

            } else if (msg.MessageId == MavLinkNet.UasGlobalPositionInt.MId)
            {
                _lat = ((MavLinkNet.UasGlobalPositionInt)msg).Lat*1e-7;
                _lon = ((MavLinkNet.UasGlobalPositionInt)msg).Lon*1e-7;
                _alt = ((MavLinkNet.UasGlobalPositionInt)msg).Alt*1e-3;
                    

                _vn = ((MavLinkNet.UasGlobalPositionInt)msg).Vx*1e-6;
                _ve = ((MavLinkNet.UasGlobalPositionInt)msg).Vy * 1e-6;
                _vd = ((MavLinkNet.UasGlobalPositionInt)msg).Vz * 1e-6;
            }
            else if (msg.MessageId == MavLinkNet.UasDistanceSensor.MId)
            {
                _lidar = ((MavLinkNet.UasDistanceSensor)msg).CurrentDistance;
 
            }

            time = DateTime.Now.Millisecond;

            updated = true;
        }


        public void decode(byte[] data)
        {
            
            if (data[0] != 'U' || data[1] != 'U' || data[2] != 'N')
            {
                Console.WriteLine("Header Nok");
                return;
            }

            
            // acceleration in m/s^2
		    // -2,2
		    //_ax=(double)((0xFFFFFF00 & (data[3]<<8)) | (0x00FF & data[4]));
		    //_ay=(double)((0xFFFFFF00 & (data[5]<<8)) | (0x00FF & data[6]));
		    //_az=(double)((0xFFFFFF00 & (data[7]<<8)) | (0x00FF & data[8]));
		    //_ax=_ax/16383.0;_ay=_ay/16383.0;_az=_az/16383.0;

            _ax = (double)(short)((data[3] << 8) | data[4]) * 5.98755e-04;
            _ay = (double)(short)((data[5] << 8) | data[6]) * 5.98755e-04;
            _az = (double)(short)((data[7] << 8) | data[8]) * 5.98755e-04;



            //_ax = (double)(short)(((data[3] << 8) | data[4])) * 1.52590219e-5 * 9.81;
            //_ay = (double)(short)((data[5] << 8) | data[6]) * 1.52590219e-5 * 9.81;
            //_az = (double)(short)((data[7] << 8) | data[8]) * 1.52590219e-5 * 9.81;
            
            
		
            // angular rate in rad/s
		    // -200,200
		    //_p=(double)((0xFFFFFF00 & (data[9]<<8)) | (0x00FF & data[10]));
		    //_q=(double)((0xFFFFFF00 & (data[11]<<8)) | (0x00FF & data[12]));
		    //_r=(double)((0xFFFFFF00 & (data[13]<<8)) | (0x00FF & data[14]));
		    //_p=_p/163.830;
            //_q=_q/163.84;
            //_r=_r/163.84;

            _p = (double)(short)(((data[9] << 8)) | (data[10])) * 1.06526e-04;
            _q = (double)(short)(((data[11] << 8)) | (data[12])) * 1.06526e-04;
            _r = (double)(short)(((data[13] << 8)) | (data[14])) * 1.06526e-04;

            
            /*_p = (double)(short)(((data[9] << 8)) | (data[10])) * 1.52590219e-7;
            _q = (double)(short)(((data[11] << 8)) | (data[12])) * 1.52590219e-7;
            _r = (double)(short)(((data[13] << 8)) | (data[14])) * 1.52590219e-7;
             * */

            // magnetic field in Gauss
		    // -1,1
            //_mx = (double)((0xFFFFFF00 & (data[15] << 8)) | (0x00FF & data[16]));
            //_my = (double)((0xFFFFFF00 & (data[17] << 8)) | (0x00FF & data[18]));
            //_mz = (double)((0xFFFFFF00 & (data[19] << 8)) | (0x00FF & data[20]));
            //_mx = _mx / 32767.0;
            //_my = _my / 32767.0;
            //_mz = _mz / 32767.0;

            _mx = (double)(short)(((data[15]) << 8) | data[16]) * 6.10352e-05;
            _my = (double)(short)(((data[17]) << 8) | data[18]) * 6.10352e-05;
            _mz = (double)(short)(((data[19]) << 8) | data[20]) * 6.10352e-05;

            

            /*_mx = (double)(((data[15] << 8)) | (data[16])) * 3.051804379e-5;
            _my = (double)(((data[17] << 8)) | (data[18])) * 3.051804379e-5;
            _mz = (double)(((data[19] << 8)) | (data[20])) * 3.051804379e-5;
             * */
            
		
            // pressure in m and m/s
		    // -100,10000 Absolute
            _ps = (double)((0xFFFFFF00 & (data[27] << 8)) | (0x00FF & data[28]));
            _ps = _ps / 3.27670;

            //_ps = (double)(((data[27] << 8)) | (data[28])) * 3.05176e-01;
            
		    // 0,80 Pitot
            _pt = (double)((0xFFFFFF00 & (data[29] << 8)) | (0x00FF & data[30]));
            _pt = _pt / 409.6;

            

            //_pt = (double)(((data[29] << 8)) | (data[30])) * 2.44141e-03;
            
		    //			System.out.println("data   "+(byte)data[3]+" "+(byte)data[4]);
		    // gps
		    int b=30;
		    if(data[b+1] !='U' || data[b+2]!='U' || data[b+3] !='G')
                Console.WriteLine("gps header Nok");
		
             // gps velocity in m/s 
		    
            _vn = (double)(((((((char)data[37] << 8) | data[36]) << 8) | data[35]) << 8) | data[34]) * 1.0e-6;
            _ve = (double)(((((((char)data[41] << 8) | data[40]) << 8) | data[39]) << 8) | data[38]) * 1.0e-6;
            _vd = (double)(((((((char)data[45] << 8) | data[44]) << 8) | data[43]) << 8) | data[42]) * 1.0e-6;

            
		
            // gps position 
            //_lon=(double)((0xff000000 & (data[b+19]<< 24) | (0x00FF0000 & data[b+18]<<16)) | (0x0000FF00 & (data[b+17]<<8)) | (0x000000ff & data[b+16]));
            //_lon=_lon / Math.Pow(10,7);
            //_lat=(double)((0xff000000 & (data[b+23]<< 24) | (0x00FF0000 & data[b+22]<<16)) | (0x0000FF00 & (data[b+21]<<8)) | (0x000000ff & data[b+20]));
            //_lat=_lat / Math.Pow(10,7);
            //_alt=(double)((0xff000000 & (data[b+27]<< 24) | (0x00FF0000 & data[b+26]<<16)) | (0x0000FF00 & (data[b+25]<<8)) | (0x000000ff & data[b+24]));
            //_alt=_alt/1000.0;

            _lon=(double)(((((((char)data[49]<<8)|data[48])<<8)|data[47])<<8)|data[46])*1.0e-7;
            _lat = (double)(((((((char)data[53] << 8) | data[52]) << 8) | data[51]) << 8) | data[50]) * 1.0e-7; 
            _alt = (double)(((((((char)data[57] << 8) | data[56]) << 8) | data[55]) << 8) | data[54]) * 1.0e-3;
				
            // gps time 
		    _itow = (double)((0x0000FF00 & (data[b+29]<<8)) | (0x00FF & data[b+28]));

            // Ahrs data
            _phi = (double)(short)(((data[67] << 8)) | (data[68])) * 1.06526e-04;
            _the = (double)(short)(((data[69] << 8)) | (data[70])) * 1.06526e-04;
            _psi = (double)(short)(((data[71] << 8)) | (data[72])) * 1.06526e-04;

            time = DateTime.Now.Millisecond;
            updated = true;
        }

        public void decode(Dictionary<string,double> imu)
        {
            _ax = imu["IMU_AccX"];
            _ay = imu["IMU_AccY"];
            _az = imu["IMU_AccZ"];


            _p = imu["IMU_GyroX"];
            _q = imu["IMU_GyroX"];
            _r = imu["IMU_GyroX"];



            // magnetic field in Gauss
            // -1,1

            _mx = imu["IMU_MagX"];
            _my = imu["IMU_MagY"];
            _mz = imu["IMU_MagZ"];

            _lon = imu["GPOS_Lon"];
            _lat = imu["GPOS_Lat"];
            _alt = imu["GPOS_Alt"];

            _phi = imu["ATT_Roll"];
            _the = imu["ATT_Pitch"];
            _psi = imu["ATT_Yaw"];


            // pressure in m and m/s
            // -100,10000 Absolute

            _ps = imu["SENS_DiffPresFilt"];

            
            // 0,80 Pitot
            _pt = imu["AIRS_TrueSpeed"]; 

            time = DateTime.Now.Millisecond;
            
        }   

        public void MavlinkEncode()
        {
            hilSensor.Xacc=(float)_ax;
            hilSensor.Yacc=(float)_ay;
            hilSensor.Zacc=(float)_az;

            hilSensor.Xgyro=(float)_p;
            hilSensor.Ygyro=(float)_q;
            hilSensor.Zgyro=(float)_r;

            hilSensor.Xmag=(float)_mx;
            hilSensor.Ymag=(float)_my;
            hilSensor.Zmag=(float)_mz;

            const float CONSTANTS_AIR_DENSITY_SEA_LEVEL_15C = 1.225f;
            double pt = (Math.Pow(_pt, 2) * CONSTANTS_AIR_DENSITY_SEA_LEVEL_15C) / 2;
            hilSensor.DiffPressure = (float)pt / 100;
            hilSensor.AbsPressure=(float)_ps;

            hilSensor.TimeUsec = (ulong)DateTime.Now.Ticks;

            attitude.Lat = (int)((_lat + _LatInit) * 1e7);
            attitude.Lon = (int)((_lon + _LonInit) * 1e7);
            attitude.Alt = (int)(_AltInit + _alt * 1000);

            Quaternion q = mathTool.Euler2Quaternion(_phi, _the, _psi);

            attitude.AttitudeQuaternion[0] = (float)q.W;
            attitude.AttitudeQuaternion[1] = (float)q.X;
            attitude.AttitudeQuaternion[2] = (float)q.Y;
            attitude.AttitudeQuaternion[3] = (float)q.Z;

            double calculate_IAS;
            
           
            if (_pt > 0.0f)
            {
                calculate_IAS = (Math.Sqrt((2.0f * _pt ) / CONSTANTS_AIR_DENSITY_SEA_LEVEL_15C));
            }
            else
            {
                calculate_IAS = (-Math.Sqrt((2.0f * Math.Abs(_pt )) / CONSTANTS_AIR_DENSITY_SEA_LEVEL_15C));
            }

            attitude.IndAirspeed = (ushort)((_pt) * 100);
            attitude.TrueAirspeed = (ushort)((_pt) * 100);

            attitude.Rollspeed = (float)_p;
            attitude.Pitchspeed = (float)_q;
            attitude.Pitchspeed = (float)_r;

            attitude.Vx = (short)(_vn * 100);
            attitude.Vy = (short)(_ve* 100);
            attitude.Vz = (short)(_vd* 100);

            attitude.Xacc = (short)_ax;
            attitude.Yacc = (short)_ay;
            attitude.Zacc = (short)_az;

            attitude.TimeUsec = (ulong)DateTime.Now.Ticks;

            gps.Lat = (int)((_lat + _LatInit) * 1e7);
            gps.Lon = (int)((_lon + _LonInit) * 1e7);
            gps.Alt = (int)(_alt * 1000 + _AltInit);

            gps.Vn = (short)(_vn * 100);
            gps.Ve = (short)(_ve * 100);
            gps.Vd = (short)(_vd * 100);

            gps.TimeUsec = (ulong)DateTime.Now.Ticks;



            
            

            time = DateTime.Now.Millisecond;
        }

        public Dictionary<string, double> PopulateVariables()
        {
            //plotVariables.Clear(;
            plotVariables["Ax"] = _ax;
            plotVariables["Ay"] = _ay;
            plotVariables["Az"] = _az;
            plotVariables["P"] = _p;
            plotVariables["Q"] = _q;
            plotVariables["R"] = _r;
            plotVariables["Mx"] = _mx;
            plotVariables["My"] = _my;
            plotVariables["Mz"] = _mz;
            plotVariables["Pitot"] = _pt;
            plotVariables["Vn"] = _vn;
            plotVariables["Ve"] = _ve;
            plotVariables["Vd"] = _vd;
            plotVariables["Lat"] = _lat;
            plotVariables["Lon"] = _lon;
            plotVariables["Alt"] = _alt;
            plotVariables["Phi"] = _phi;
            plotVariables["The"] = _the;
            plotVariables["Psi"] = _psi;
            plotVariables["Lidar"] = _lidar;
            return plotVariables;


        }

        public double ax
        {
            get { return _ax; }
        }
        public double ay
        {
            get { return _ay; }
        }
        public double az
        {
            get { return _az; }
        }
        public double p
        {
            get { return _p; }
        }
        public double q
        {
            get { return _q; }
        }   
        public double r
        {
            get { return _r; }
        }
        public double mx
        {
            get { return _mx; }
        }
        public double my
        {
            get { return _my; }
        }
        public double mz
        {
            get { return _mz; }
        }

        public double vn
        {
            get { return _vn; }
        }
        public double ve
        {
            get { return _ve; }
        }
        public double vd
        {
            get { return _vd; }
        }
        public double lon
        {
            get { return _lon; }
        }
        public double lat
        {
            get { 
                if (_lat > 49) 
                    _lat = _lat;
                return _lat; }
        }
        public double LonInit
        {
            get { return _LonInit; }
            set { _LonInit = value; }
        }
        public double LatInit
        {
            get { return _LatInit; }
            set { _LatInit = value; }
        }
        public double AltInit
        {
            get { return _AltInit; }
            set { _AltInit = value; }
        }
        public double alt
        {
            get { return _alt; }
        }
        public double ps
        {
            get { return _ps; }
        }
        public double pt
        {
            get { return _pt; }
        }
        public double itow
        {
            get { return _itow; }
        }

        public double phi
        {
            get { return _phi; }
        }

        public double the
        {
            get { return _the; }
        }

        public double psi
        {
            get { return _psi; }
        }

        public double Lidar
        {
            get { return _lidar; }
        }
        public int Time
        {
            get { return time; }
        }
        public Dictionary<string, double> PlotVariables
        {
            get { return plotVariables; }
            set { plotVariables = value; }
        }
        public MavLinkNet.UasHilSensor HilSensor
        {
            get { return hilSensor; }
        }
        public MavLinkNet.UasHilStateQuaternion HilStateQuaternion
        {
            get { return attitude; }
        }
        public MavLinkNet.UasHilGps HilGps
        {
            get { return gps; }
        }

        public bool Updated
        {
            get { return updated; }
            set { updated = value;}
        }

        

        
    }
}
