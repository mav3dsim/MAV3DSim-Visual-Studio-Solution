using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace MAV3DSim.Utils
{
    public class Log
    {
        private ArrayList logText;
        String saveFile;
        public Log()
        {
            logText = new ArrayList();

        }

        public void Append(String text)
        {
            logText.Add(text);

        }

        public void ResetLog()
        {
            logText = new ArrayList();
        }

        public void SaveLog(String toFile)
        {
            using (StreamWriter streamWriter = new StreamWriter(@toFile))
            {
                streamWriter.WriteLine("Count,TotalMilliseconds,p,q,r,ax,ay,az,mx,my,mz,phi,the,psi,u,v,w,ve,vn,vd,lat,lon,alt,lat differential, lon differential,roll setpoint, roll error, roll control, pitch setpoint, pitch error pitch control");
                streamWriter.Flush();
                for (int i = 0; i < logText.Count;i++ )
                {
                    
                    streamWriter.WriteLine(logText[i]);
                    streamWriter.Flush(); //force line to be written to disk
                }
            }
        }
    }
}
