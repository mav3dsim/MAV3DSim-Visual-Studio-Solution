namespace MAV3DSim.Docks
{
    partial class AltitudePidGains
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
            this.groupBox15 = new Utils.WinFormControls.CustomGroupBox();
            this.txtMaxAltitudeKd = new Utils.WinFormControls.CustomTextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.txtMinAltitudeKd = new Utils.WinFormControls.CustomTextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.txtMaxAltitudeKi = new Utils.WinFormControls.CustomTextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.txtMinAltitudeKi = new Utils.WinFormControls.CustomTextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.txtMaxAltitudeKp = new Utils.WinFormControls.CustomTextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.txtMinAltitudeKp = new Utils.WinFormControls.CustomTextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.tbAltitudeKd = new System.Windows.Forms.TrackBar();
            this.tbAltitudeKi = new System.Windows.Forms.TrackBar();
            this.tbAltitudeKp = new System.Windows.Forms.TrackBar();
            this.txtAltitudeKd = new Utils.WinFormControls.CustomTextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.btnUpdateAltitudeGains = new Utils.WinFormControls.CustomButton();
            this.txtAltitudeKi = new Utils.WinFormControls.CustomTextBox();
            this.label55 = new System.Windows.Forms.Label();
            this.txtAltitudeKp = new Utils.WinFormControls.CustomTextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbAltitudeKd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAltitudeKi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAltitudeKp)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.txtMaxAltitudeKd);
            this.groupBox15.Controls.Add(this.label48);
            this.groupBox15.Controls.Add(this.txtMinAltitudeKd);
            this.groupBox15.Controls.Add(this.label49);
            this.groupBox15.Controls.Add(this.txtMaxAltitudeKi);
            this.groupBox15.Controls.Add(this.label50);
            this.groupBox15.Controls.Add(this.txtMinAltitudeKi);
            this.groupBox15.Controls.Add(this.label51);
            this.groupBox15.Controls.Add(this.txtMaxAltitudeKp);
            this.groupBox15.Controls.Add(this.label52);
            this.groupBox15.Controls.Add(this.txtMinAltitudeKp);
            this.groupBox15.Controls.Add(this.label53);
            this.groupBox15.Controls.Add(this.tbAltitudeKd);
            this.groupBox15.Controls.Add(this.tbAltitudeKi);
            this.groupBox15.Controls.Add(this.tbAltitudeKp);
            this.groupBox15.Controls.Add(this.txtAltitudeKd);
            this.groupBox15.Controls.Add(this.label54);
            this.groupBox15.Controls.Add(this.btnUpdateAltitudeGains);
            this.groupBox15.Controls.Add(this.txtAltitudeKi);
            this.groupBox15.Controls.Add(this.label55);
            this.groupBox15.Controls.Add(this.txtAltitudeKp);
            this.groupBox15.Controls.Add(this.label56);
            this.groupBox15.Location = new System.Drawing.Point(12, 12);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(255, 278);
            this.groupBox15.TabIndex = 23;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Altitud Gain";
            // 
            // txtMaxAltitudKd
            // 
            this.txtMaxAltitudeKd.Location = new System.Drawing.Point(211, 177);
            this.txtMaxAltitudeKd.Name = "txtMaxAltitudKd";
            this.txtMaxAltitudeKd.Size = new System.Drawing.Size(31, 20);
            this.txtMaxAltitudeKd.TabIndex = 20;
            this.txtMaxAltitudeKd.Text = "2";
            this.txtMaxAltitudeKd.TextChanged += this.txtMaxAltitudKd_TextChanged;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(182, 180);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(27, 13);
            this.label48.TabIndex = 19;
            this.label48.Text = "Max";
            // 
            // txtMinAltitudKd
            // 
            this.txtMinAltitudeKd.Location = new System.Drawing.Point(144, 177);
            this.txtMinAltitudeKd.Name = "txtMinAltitudKd";
            this.txtMinAltitudeKd.Size = new System.Drawing.Size(31, 20);
            this.txtMinAltitudeKd.TabIndex = 18;
            this.txtMinAltitudeKd.Text = "0";
            this.txtMinAltitudeKd.TextChanged += this.txtMinAltitudKd_TextChanged;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(119, 180);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(24, 13);
            this.label49.TabIndex = 17;
            this.label49.Text = "Min";
            // 
            // txtMaxAltitudKi
            // 
            this.txtMaxAltitudeKi.Location = new System.Drawing.Point(211, 100);
            this.txtMaxAltitudeKi.Name = "txtMaxAltitudKi";
            this.txtMaxAltitudeKi.Size = new System.Drawing.Size(31, 20);
            this.txtMaxAltitudeKi.TabIndex = 16;
            this.txtMaxAltitudeKi.Text = "2";
            this.txtMaxAltitudeKi.TextChanged += this.txtMaxAltitudKi_TextChanged;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(182, 103);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(27, 13);
            this.label50.TabIndex = 15;
            this.label50.Text = "Max";
            // 
            // txtMinAltitudKi
            // 
            this.txtMinAltitudeKi.Location = new System.Drawing.Point(144, 100);
            this.txtMinAltitudeKi.Name = "txtMinAltitudKi";
            this.txtMinAltitudeKi.Size = new System.Drawing.Size(31, 20);
            this.txtMinAltitudeKi.TabIndex = 14;
            this.txtMinAltitudeKi.Text = "0";
            this.txtMinAltitudeKi.TextChanged += this.txtMinAltitudKi_TextChanged;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(119, 103);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(24, 13);
            this.label51.TabIndex = 13;
            this.label51.Text = "Min";
            // 
            // txtMaxAltitudKp
            // 
            this.txtMaxAltitudeKp.Location = new System.Drawing.Point(211, 26);
            this.txtMaxAltitudeKp.Name = "txtMaxAltitudKp";
            this.txtMaxAltitudeKp.Size = new System.Drawing.Size(31, 20);
            this.txtMaxAltitudeKp.TabIndex = 12;
            this.txtMaxAltitudeKp.Text = "2";
            this.txtMaxAltitudeKp.TextChanged += this.txtMaxAltitudKp_TextChanged;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(182, 29);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(27, 13);
            this.label52.TabIndex = 11;
            this.label52.Text = "Max";
            // 
            // txtMinAltitudKp
            // 
            this.txtMinAltitudeKp.Location = new System.Drawing.Point(144, 26);
            this.txtMinAltitudeKp.Name = "txtMinAltitudKp";
            this.txtMinAltitudeKp.Size = new System.Drawing.Size(31, 20);
            this.txtMinAltitudeKp.TabIndex = 10;
            this.txtMinAltitudeKp.Text = "0";
            this.txtMinAltitudeKp.TextChanged += this.txtMinAltitudKp_TextChanged;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(119, 29);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(24, 13);
            this.label53.TabIndex = 9;
            this.label53.Text = "Min";
            // 
            // tbAltitudKd
            // 
            this.tbAltitudeKd.Location = new System.Drawing.Point(6, 200);
            this.tbAltitudeKd.Maximum = 2000;
            this.tbAltitudeKd.Name = "tbAltitudKd";
            this.tbAltitudeKd.Size = new System.Drawing.Size(237, 45);
            this.tbAltitudeKd.TabIndex = 8;
            this.tbAltitudeKd.ValueChanged += new System.EventHandler(this.tbAltitudKd_ValueChanged);
            // 
            // tbAltitudKi
            // 
            this.tbAltitudeKi.Location = new System.Drawing.Point(6, 124);
            this.tbAltitudeKi.Maximum = 2000;
            this.tbAltitudeKi.Name = "tbAltitudKi";
            this.tbAltitudeKi.Size = new System.Drawing.Size(237, 45);
            this.tbAltitudeKi.TabIndex = 7;
            this.tbAltitudeKi.ValueChanged += new System.EventHandler(this.tbAltitudKi_ValueChanged);
            // 
            // tbAltitudKp
            // 
            this.tbAltitudeKp.Location = new System.Drawing.Point(6, 48);
            this.tbAltitudeKp.Maximum = 2000;
            this.tbAltitudeKp.Name = "tbAltitudKp";
            this.tbAltitudeKp.Size = new System.Drawing.Size(237, 45);
            this.tbAltitudeKp.TabIndex = 6;
            this.tbAltitudeKp.ValueChanged += new System.EventHandler(this.tbAltitudKp_ValueChanged);
            // 
            // txtAltitudKd
            // 
            this.txtAltitudeKd.Location = new System.Drawing.Point(25, 177);
            this.txtAltitudeKd.Name = "txtAltitudKd";
            this.txtAltitudeKd.Size = new System.Drawing.Size(40, 20);
            this.txtAltitudeKd.TabIndex = 5;
            this.txtAltitudeKd.Text = "0";
            this.txtAltitudeKd.TextChanged += this.txtAltitudKd_TextChanged;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(6, 180);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(20, 13);
            this.label54.TabIndex = 4;
            this.label54.Text = "Kd";
            // 
            // btnUpdateAltitudGains
            // 
            this.btnUpdateAltitudeGains.Location = new System.Drawing.Point(160, 249);
            this.btnUpdateAltitudeGains.Name = "btnUpdateAltitudGains";
            this.btnUpdateAltitudeGains.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateAltitudeGains.TabIndex = 1;
            this.btnUpdateAltitudeGains.Text = "Update";
            this.btnUpdateAltitudeGains.UseVisualStyleBackColor = true;
            this.btnUpdateAltitudeGains.Click += new System.EventHandler(this.btnUpdateAltitudGains_Click);
            // 
            // txtAltitudKi
            // 
            this.txtAltitudeKi.Location = new System.Drawing.Point(25, 100);
            this.txtAltitudeKi.Name = "txtAltitudKi";
            this.txtAltitudeKi.Size = new System.Drawing.Size(40, 20);
            this.txtAltitudeKi.TabIndex = 3;
            this.txtAltitudeKi.Text = "0";
            this.txtAltitudeKi.TextChanged += this.txtAltitudKi_TextChanged;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(6, 103);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(16, 13);
            this.label55.TabIndex = 2;
            this.label55.Text = "Ki";
            // 
            // txtAltitudKp
            // 
            this.txtAltitudeKp.Location = new System.Drawing.Point(25, 23);
            this.txtAltitudeKp.Name = "txtAltitudKp";
            this.txtAltitudeKp.Size = new System.Drawing.Size(40, 20);
            this.txtAltitudeKp.TabIndex = 1;
            this.txtAltitudeKp.Text = "0";
            this.txtAltitudeKp.TextChanged += this.txtAltitudKp_TextChanged;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(4, 26);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(20, 13);
            this.label56.TabIndex = 1;
            this.label56.Text = "Kp";
            // 
            // AltitudPidGains
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 298);
            this.Controls.Add(this.groupBox15);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "AltitudPidGains";
            this.Text = "AltitudPidGains";
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbAltitudeKd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAltitudeKi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAltitudeKp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox15;
        private Utils.WinFormControls.CustomTextBox txtMaxAltitudeKd;
        private System.Windows.Forms.Label label48;
        private Utils.WinFormControls.CustomTextBox txtMinAltitudeKd;
        private System.Windows.Forms.Label label49;
        private Utils.WinFormControls.CustomTextBox txtMaxAltitudeKi;
        private System.Windows.Forms.Label label50;
        private Utils.WinFormControls.CustomTextBox txtMinAltitudeKi;
        private System.Windows.Forms.Label label51;
        private Utils.WinFormControls.CustomTextBox txtMaxAltitudeKp;
        private System.Windows.Forms.Label label52;
        private Utils.WinFormControls.CustomTextBox txtMinAltitudeKp;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.TrackBar tbAltitudeKd;
        private System.Windows.Forms.TrackBar tbAltitudeKi;
        private System.Windows.Forms.TrackBar tbAltitudeKp;
        private Utils.WinFormControls.CustomTextBox txtAltitudeKd;
        private System.Windows.Forms.Label label54;
        private Utils.WinFormControls.CustomButton btnUpdateAltitudeGains;
        private Utils.WinFormControls.CustomTextBox txtAltitudeKi;
        private System.Windows.Forms.Label label55;
        private Utils.WinFormControls.CustomTextBox txtAltitudeKp;
        private System.Windows.Forms.Label label56;
    }
}