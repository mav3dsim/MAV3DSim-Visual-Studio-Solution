namespace MAV3DSim.Docks
{
    partial class Verticalspeed
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
            this.verticalSpeedIndicatorInstrumentControl1 = new MAV3DSim.VerticalSpeedIndicatorInstrumentControl();
            this.SuspendLayout();
            // 
            // verticalSpeedIndicatorInstrumentControl1
            // 
            this.verticalSpeedIndicatorInstrumentControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalSpeedIndicatorInstrumentControl1.Location = new System.Drawing.Point(0, 0);
            this.verticalSpeedIndicatorInstrumentControl1.Name = "verticalSpeedIndicatorInstrumentControl1";
            this.verticalSpeedIndicatorInstrumentControl1.Size = new System.Drawing.Size(215, 215);
            this.verticalSpeedIndicatorInstrumentControl1.TabIndex = 0;
            this.verticalSpeedIndicatorInstrumentControl1.Text = "verticalSpeedIndicatorInstrumentControl1";
            // 
            // VerticalSpeed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 215);
            this.Controls.Add(this.verticalSpeedIndicatorInstrumentControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "VerticalSpeed";
            this.Text = "VerticalSpeed";
            this.ResumeLayout(false);

        }

        #endregion

        private VerticalSpeedIndicatorInstrumentControl verticalSpeedIndicatorInstrumentControl1;
    }
}