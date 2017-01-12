using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MAV3DSim.Utils.WinFormControls
{

    public delegate void TextChangedEventHandler(object sender, EventArgs e);
    public partial class CustomTextBox :  UserControl
    {
        [Browsable(true)]
        public event TextChangedEventHandler TextChanged;
        TextBox textBox1 = new TextBox();
        private Color oldBorderColor;
        public CustomTextBox()
        {
            InitializeComponent();
            textBox1.Multiline = false;
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox1.Font = this.Font;
            textBox1.BackColor = this.BackColor;
            this.Controls.Add(textBox1);
            textBox1.TextChanged += textBox1_TextChanged;
        }

        void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(this.TextChanged!=null)
                TextChanged(this, e);
        }
        [Browsable(true)]
        public override string Text
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public void AppendText(string text)
        {
            textBox1.AppendText(text);
        }

        private Color borderColor;
        private Color borderColorHover;
        private Color DrawBorderColor;
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; DrawBorderColor=value; }
        }

        public Color BorderColorHover
        {
            get { return borderColorHover; }
            set { borderColorHover = value; }
        }
        
        
        
        private void CustomTextBox_SizeChanged(object sender, EventArgs e)
        {
            textBox1.Size = new Size(this.Width - 3, this.Height - 2);
            textBox1.Location = new Point(2, 1);
            textBox1.Font = this.Font;
            textBox1.BackColor = this.BackColor;
        }
        public enum BorderSides{Left,Right,Top,Bottom,All}        
        public BorderSides SetBorderSide= BorderSides.All;
        int BorderSize =1;
        private void CustomTextBox_Paint(object sender, PaintEventArgs e)
        {
            Rectangle txtRect = this.ClientRectangle;
            //txtRect.Width = txtRect.Width - 3;
            //txtRect.X += 2;
            switch (SetBorderSide)
            {
                case BorderSides.All:
                    ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, DrawBorderColor, BorderSize, ButtonBorderStyle.Solid, DrawBorderColor, BorderSize, ButtonBorderStyle.Solid, DrawBorderColor, BorderSize, ButtonBorderStyle.Solid, DrawBorderColor, BorderSize, ButtonBorderStyle.Solid);
                    break;
                case BorderSides.Left:
                    ControlPaint.DrawBorder(e.Graphics, txtRect, DrawBorderColor, BorderSize, ButtonBorderStyle.Solid, DrawBorderColor, 0, ButtonBorderStyle.Solid, DrawBorderColor, 0, ButtonBorderStyle.Solid, DrawBorderColor, 0, ButtonBorderStyle.Solid);
                    break;
                case BorderSides.Top:
                    ControlPaint.DrawBorder(e.Graphics, txtRect, DrawBorderColor, 0, ButtonBorderStyle.Solid, DrawBorderColor, BorderSize, ButtonBorderStyle.Solid, DrawBorderColor, 0, ButtonBorderStyle.Solid, DrawBorderColor, 0, ButtonBorderStyle.Solid);
                    break;
                case BorderSides.Right:
                    ControlPaint.DrawBorder(e.Graphics, txtRect, DrawBorderColor, 0, ButtonBorderStyle.Solid, DrawBorderColor, 0, ButtonBorderStyle.Solid, DrawBorderColor, BorderSize, ButtonBorderStyle.Solid, DrawBorderColor, 0, ButtonBorderStyle.Solid);
                    break;
                case BorderSides.Bottom:
                    ControlPaint.DrawBorder(e.Graphics, txtRect, DrawBorderColor, 0, ButtonBorderStyle.Solid, DrawBorderColor, 0, ButtonBorderStyle.Solid, DrawBorderColor, 0, ButtonBorderStyle.Solid, DrawBorderColor, BorderSize, ButtonBorderStyle.Solid);
                    break;
                default:
                    break;
            }
            textBox1.BackColor = this.BackColor;
        }

        void CustomTextBox_MouseEnter(object sender, System.EventArgs e)
        {
            this.DrawBorderColor = borderColorHover;
            this.textBox1.ForeColor = foreColorHover;
            CustomTextBox_Paint(sender, new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle));
        }
        private void CustomTextBox_MouseLeave(object sender, System.EventArgs e)
        {
            this.DrawBorderColor = borderColor;
            this.textBox1.ForeColor = foreColor;
            CustomTextBox_Paint(sender, new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle));
        }



        internal void ScrollToCaret()
        {
            textBox1.ScrollToCaret();
        }

        public bool Multiline { 
            get {return textBox1.Multiline;}
            set { textBox1.Multiline = value; }
        }
        Color foreColor;
        public override Color ForeColor
        {
            get { return foreColor; }
            set { this.textBox1.ForeColor = value; foreColor = value; }
        }
        Color foreColorHover;
        public Color ForeColorHover
        {
            get { return foreColorHover; }
            set { foreColorHover = value; }
        }


	
    }
}
