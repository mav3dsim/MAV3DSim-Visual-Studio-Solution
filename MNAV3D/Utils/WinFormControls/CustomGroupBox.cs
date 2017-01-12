using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAV3DSim.Utils.WinFormControls
{
    class CustomGroupBox : GroupBox
    {
        private Color borderColor;

        public Color BorderColor
        {
            get { return this.borderColor; }
            set { this.borderColor = value; }
        }

        private int roundCorners;
        public int RoundCorners
        {
            get { return this.roundCorners; }
            set { this.roundCorners = value; }
        }
 
        public CustomGroupBox()
        {
            this.borderColor = Color.Black;
        }
        
 
        protected override void OnPaint(PaintEventArgs e)
        {
            
            float BorderThickness = 2;
            Size tSize = e.Graphics.MeasureString(this.Text, this.Font).ToSize();
            int ArcWidth = this.RoundCorners * 2;
            int ArcHeight = this.RoundCorners * 2;
            int ArcX1 = 0;
            int ArcX2 = this.Width - (ArcWidth + 1);
            int ArcY1 = tSize.Height/2;
            int ArcY2 = this.Height - (ArcHeight + 1);
            //Set Graphics smoothing mode to Anit-Alias-- 
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Drawing2D.GraphicsPath pathString = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Brush BackgroundBrush = new SolidBrush(this.BackColor);
            System.Drawing.Brush BorderBrush = new SolidBrush(this.BorderColor);
            System.Drawing.Pen BorderPen = new Pen(BorderBrush, BorderThickness);



            Rectangle r = new Rectangle(this.ClientRectangle.Left, this.ClientRectangle.Top, this.ClientRectangle.Width, this.ClientRectangle.Height);
            try
            {
                //Create Rounded Rectangle Path------
                path.AddArc(ArcX1, ArcY1, ArcWidth, ArcHeight, 180, 90); // Top Left
                path.AddArc(ArcX2, ArcY1, ArcWidth, ArcHeight, 270, 90); //Top Right
                path.AddArc(ArcX2, ArcY2, ArcWidth, ArcHeight, 360, 90); //Bottom Right
                path.AddArc(ArcX1, ArcY2, ArcWidth, ArcHeight, 90, 90); //Bottom Left
                path.CloseAllFigures();
            }
            catch (Exception ex) { }



            int offset = 1;
            pathString.AddLine(RoundCorners + offset, 0, RoundCorners + tSize.Width, 0);
            pathString.AddLine(RoundCorners + offset + tSize.Width, 0, RoundCorners + offset + tSize.Width , tSize.Height);
            pathString.AddLine(tSize.Width, tSize.Height, RoundCorners + offset, tSize.Height);
            pathString.AddLine(RoundCorners + offset, tSize.Height, RoundCorners + offset, 0);
            
            e.Graphics.FillPath(BackgroundBrush, path);            
            e.Graphics.DrawPath(BorderPen, path);
            e.Graphics.FillPath(new SolidBrush(this.BackColor), pathString);
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), RoundCorners + offset, 0);
            


        }

        public static GraphicsPath GetRoundedRect(RectangleF r, float radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.StartFigure();
            r = new RectangleF(r.Left, r.Top, r.Width, r.Height);
            if (radius <= 0.0F || radius <= 0.0F)
            {
                gp.AddRectangle(r);
            }
            else
            {
                gp.AddArc((float)r.X, (float)r.Y, radius, radius, 180, 90);
                gp.AddArc((float)r.Right - radius, (float)r.Y, radius - 1, radius, 270, 90);
                gp.AddArc((float)r.Right - radius, ((float)r.Bottom) - radius - 1, radius - 1, radius, 0, 90);
                gp.AddArc((float)r.X, ((float)r.Bottom) - radius - 1, radius - 1, radius, 90, 90);
            }
            gp.CloseFigure();
            return gp;
        }
    }
}

