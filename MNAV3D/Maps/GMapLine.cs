using System.Drawing;
using GMap.NET.WindowsForms;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace GMap.NET.WindowsForms
{
    public class GMapLine : GMapPolygon
    {
        public GMapLine(List<PointLatLng> points, string name)
            : base(points, name)
        {
            LocalPoints.Capacity = Points.Count;

            Stroke.LineJoin = LineJoin.Round;

        }

       /* protected override void OnRender(Graphics g)
        {

            if (IsVisible)
            {
                using (GraphicsPath rp = new GraphicsPath())
                {
                    for (int i = 0; i < LocalPoints.Count; i++)
                    {
                        GPoint p2 = LocalPoints[i];

                        if (i == 0)
                        {
                            rp.AddLine(p2.X, p2.Y, p2.X, p2.Y);
                        }
                        else
                        {
                            System.Drawing.PointF p = rp.GetLastPoint();
                            rp.AddLine(p.X, p.Y, p2.X, p2.Y);
                        }
                    }

                    if (rp.PointCount > 0)
                    {
                        rp.CloseFigure();

                        g.FillPath(Fill, rp);

                        g.DrawPath(Stroke, rp);
                    }
                }
            }


        }
        */
        


        
    }
}
