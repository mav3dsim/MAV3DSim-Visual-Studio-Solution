namespace MAV3DSim.Docks
{
    partial class Altimeter
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.altimeterInstrumentControl1 = new MAV3DSim.AltimeterInstrumentControl();
            this.SuspendLayout();
            // 
            // altimeterInstrumentControl1
            // 
            this.altimeterInstrumentControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.altimeterInstrumentControl1.Location = new System.Drawing.Point(0, 0);
            this.altimeterInstrumentControl1.Name = "altimeterInstrumentControl1";
            this.altimeterInstrumentControl1.Size = new System.Drawing.Size(215, 215);
            this.altimeterInstrumentControl1.TabIndex = 0;
            this.altimeterInstrumentControl1.Text = "altimeterInstrumentControl1";
            // 
            // Altimeter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 215);
            this.Controls.Add(this.altimeterInstrumentControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "Altimeter";
            this.Text = "Altimeter";
            this.ResumeLayout(false);

        }

        #endregion

        private AltimeterInstrumentControl altimeterInstrumentControl1;
    }
}