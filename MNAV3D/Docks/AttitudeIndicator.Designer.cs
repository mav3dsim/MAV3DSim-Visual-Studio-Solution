namespace MAV3DSim.Docks
{
    partial class AttitudeIndicator
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
            this.attitudeIndicatorInstrumentControl1 = new MAV3DSim.AttitudeIndicatorInstrumentControl();
            this.SuspendLayout();
            // 
            // attitudeIndicatorInstrumentControl1
            // 
            this.attitudeIndicatorInstrumentControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attitudeIndicatorInstrumentControl1.Location = new System.Drawing.Point(0, 0);
            this.attitudeIndicatorInstrumentControl1.Name = "attitudeIndicatorInstrumentControl1";
            this.attitudeIndicatorInstrumentControl1.Size = new System.Drawing.Size(215, 215);
            this.attitudeIndicatorInstrumentControl1.TabIndex = 0;
            this.attitudeIndicatorInstrumentControl1.Text = "attitudeIndicatorInstrumentControl1";
            // 
            // ArtificialHorizon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 215);
            this.Controls.Add(this.attitudeIndicatorInstrumentControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "ArtificialHorizon";
            this.Text = "ArtificialHorizon";
            this.ResumeLayout(false);

        }

        #endregion

        private AttitudeIndicatorInstrumentControl attitudeIndicatorInstrumentControl1;
    }
}