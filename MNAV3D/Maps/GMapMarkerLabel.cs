using System.Drawing;
using GMap.NET.WindowsForms;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System;


    namespace GMap.NET.WindowsForms.Markers
    {


        public class GMapMarkerLabel : GMapMarker
        {
            public Pen Pen;
            float angle;
            float prevAngle;
            Bitmap image;
            String label;
            public GMapMarkerLabel(PointLatLng p,Bitmap pImage,String label)
                : base(p)
            {
                Pen = new Pen(Brushes.AliceBlue, 2);

                // do not forget set Size of the marker
                // if so, you shall have no event on it ;}
                Size = new Size(55, 55);
                angle = 0;
                
                
                image = pImage;
                this.label = label;
               
                    
            }

            public override void OnRender(Graphics g)
            {
                StringFormat format1 = new StringFormat(StringFormatFlags.NoClip);
                format1.Alignment = StringAlignment.Center;
                
               g.DrawImage(image, new Point(LocalPosition.X - image.Width/3 , LocalPosition.Y - image.Height-10));
               g.DrawString(label, SystemFonts.DefaultFont, Brushes.Black, new PointF(LocalPosition.X , LocalPosition.Y - image.Height-5),format1);
                
            }

            public void setRotation(float Angle)
            {
                angle = Angle;
            }

            
        


        }
    }

