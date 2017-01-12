using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAV3DSim.Utils.WinFormControls
{
    class CustomButton : Button
    {
        private enum MouseState { None = 1, Down = 2, Up = 3, Enter = 4, Leave = 5}
        private MouseState mouseState = MouseState.None;
        private bool verticalText;
        public CustomButton()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor |
                      ControlStyles.Opaque |
                      ControlStyles.ResizeRedraw |
                      ControlStyles.OptimizedDoubleBuffer |
                      ControlStyles.CacheText, // We gain about 2% in painting by avoiding extra GetWindowText calls
                      true);

            //this.colorTable = new Colortable();

            this.MouseLeave += new EventHandler(MouseLeaveHandler);
            this.MouseDown += new MouseEventHandler(MouseDownHandler);
            this.MouseUp += new MouseEventHandler(MouseUpHandler);
            this.MouseEnter += new EventHandler(MouseEnterHandler);
            //this.MouseMove += new MouseEventHandler(MouseMoveHandler);
            this.verticalText = false;
        }

        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            mouseState = MouseState.Down;
            Invalidate();
        }

        private void MouseUpHandler(object sender, MouseEventArgs e)
        {
            mouseState = MouseState.Up;
            Invalidate();
        }

        private void MouseLeaveHandler(object sender, EventArgs e)
        {
            mouseState = MouseState.Leave;
            Invalidate();
        }
        private void MouseEnterHandler(object sender, EventArgs e)
        {
            mouseState = MouseState.Enter;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Color BackGround = Color.FromArgb(45,45,48);
            Color Border = Color.FromArgb(63,63,70);
            Color StringColor = Color.White;
            StringFormat sf = new StringFormat();
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            Rectangle r = new Rectangle(this.ClientRectangle.Left, this.ClientRectangle.Top, this.ClientRectangle.Width, this.ClientRectangle.Height);


            if (!this.Enabled)
            {
                StringColor = Color.FromArgb(78, 78, 80);
                BackGround = Color.FromArgb(45, 45, 48);
                Border = Color.FromArgb(63, 63, 70);
            }
            else
            {
                if (mouseState == MouseState.None || mouseState == MouseState.Leave)
                {
                    StringColor = Color.White;
                    BackGround = Color.FromArgb(45, 45, 48);
                }
                else if (mouseState == MouseState.Down)
                {
                    BackGround = Color.FromArgb(45, 45, 48);
                    Border = Color.FromArgb(0, 122, 204);
                }
                else if ( mouseState == MouseState.Up || mouseState == MouseState.Enter)
                {
                    //StringColor = this.ForeColor;
                    BackGround = Color.FromArgb(63, 63, 70);
                    Border = Color.FromArgb(0,122,204);

                }
            }
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
            e.Graphics.FillRectangle(new SolidBrush(BackGround), r);
            ControlPaint.DrawBorder(e.Graphics,r,Border,ButtonBorderStyle.Solid);
            if(!verticalText)
                e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(StringColor), this.ClientRectangle, sf);
            else
            {
                e.Graphics.TranslateTransform(this.ClientRectangle.Width, 0);
                e.Graphics.RotateTransform(90);
                e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(StringColor), this.ClientRectangle, sf);
            }
        }

        public bool VerticalText
        {
            set { verticalText = value; }
            get { return verticalText; }
        }
    }
}
