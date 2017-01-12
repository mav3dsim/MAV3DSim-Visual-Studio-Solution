namespace MAV3DSim.Utils.WinFormControls
{
    partial class CustomTextBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CustomTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Name = "CustomTextBox";
            this.Size = new System.Drawing.Size(153, 22);
            this.SizeChanged += new System.EventHandler(this.CustomTextBox_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CustomTextBox_Paint);
            this.textBox1.MouseEnter += new System.EventHandler(this.CustomTextBox_MouseEnter);
            this.textBox1.MouseLeave += new System.EventHandler(this.CustomTextBox_MouseLeave);
            
            this.ResumeLayout(false);
            

        }

        

        

        #endregion

    }
}
