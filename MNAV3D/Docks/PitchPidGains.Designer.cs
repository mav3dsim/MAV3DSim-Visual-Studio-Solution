namespace MAV3DSim.Docks
{
    partial class PitchPidGains
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
            this.groupBox14 = new MAV3DSim.Utils.WinFormControls.CustomGroupBox();
            this.txtMaxPitchKd = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.txtMinPitchKd = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.txtMaxPitchKi = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.txtMinPitchKi = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.txtMaxPitchKp = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.txtMinPitchKp = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.tbPitchKd = new System.Windows.Forms.TrackBar();
            this.tbPitchKi = new System.Windows.Forms.TrackBar();
            this.tbPitchKp = new System.Windows.Forms.TrackBar();
            this.txtPitchKd = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.btnUpdatePitchGains = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.txtPitchKi = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.txtPitchKp = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPitchKd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPitchKi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPitchKp)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox14
            // 
            this.groupBox14.BorderColor = System.Drawing.Color.Black;
            this.groupBox14.Controls.Add(this.txtMaxPitchKd);
            this.groupBox14.Controls.Add(this.label39);
            this.groupBox14.Controls.Add(this.txtMinPitchKd);
            this.groupBox14.Controls.Add(this.label40);
            this.groupBox14.Controls.Add(this.txtMaxPitchKi);
            this.groupBox14.Controls.Add(this.label41);
            this.groupBox14.Controls.Add(this.txtMinPitchKi);
            this.groupBox14.Controls.Add(this.label42);
            this.groupBox14.Controls.Add(this.txtMaxPitchKp);
            this.groupBox14.Controls.Add(this.label43);
            this.groupBox14.Controls.Add(this.txtMinPitchKp);
            this.groupBox14.Controls.Add(this.label44);
            this.groupBox14.Controls.Add(this.tbPitchKd);
            this.groupBox14.Controls.Add(this.tbPitchKi);
            this.groupBox14.Controls.Add(this.tbPitchKp);
            this.groupBox14.Controls.Add(this.txtPitchKd);
            this.groupBox14.Controls.Add(this.label45);
            this.groupBox14.Controls.Add(this.btnUpdatePitchGains);
            this.groupBox14.Controls.Add(this.txtPitchKi);
            this.groupBox14.Controls.Add(this.label46);
            this.groupBox14.Controls.Add(this.txtPitchKp);
            this.groupBox14.Controls.Add(this.label47);
            this.groupBox14.Location = new System.Drawing.Point(12, 12);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.RoundCorners = 0;
            this.groupBox14.Size = new System.Drawing.Size(255, 278);
            this.groupBox14.TabIndex = 22;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Pitch Gain";
            // 
            // txtMaxPitchKd
            // 
            this.txtMaxPitchKd.BackColor = System.Drawing.Color.White;
            this.txtMaxPitchKd.BorderColor = System.Drawing.Color.Empty;
            this.txtMaxPitchKd.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMaxPitchKd.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMaxPitchKd.Location = new System.Drawing.Point(213, 177);
            this.txtMaxPitchKd.Multiline = false;
            this.txtMaxPitchKd.Name = "txtMaxPitchKd";
            this.txtMaxPitchKd.Size = new System.Drawing.Size(31, 20);
            this.txtMaxPitchKd.TabIndex = 20;
            this.txtMaxPitchKd.TextChanged += new Utils.WinFormControls.TextChangedEventHandler(this.txtMaxPitchKd_TextChanged);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(184, 180);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(27, 13);
            this.label39.TabIndex = 19;
            this.label39.Text = "Max";
            // 
            // txtMinPitchKd
            // 
            this.txtMinPitchKd.BackColor = System.Drawing.Color.White;
            this.txtMinPitchKd.BorderColor = System.Drawing.Color.Empty;
            this.txtMinPitchKd.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMinPitchKd.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMinPitchKd.Location = new System.Drawing.Point(146, 177);
            this.txtMinPitchKd.Multiline = false;
            this.txtMinPitchKd.Name = "txtMinPitchKd";
            this.txtMinPitchKd.Size = new System.Drawing.Size(31, 20);
            this.txtMinPitchKd.TabIndex = 18;
            this.txtMinPitchKd.TextChanged += new Utils.WinFormControls.TextChangedEventHandler(this.txtMinPitchKd_TextChanged);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(121, 180);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(24, 13);
            this.label40.TabIndex = 17;
            this.label40.Text = "Min";
            // 
            // txtMaxPitchKi
            // 
            this.txtMaxPitchKi.BackColor = System.Drawing.Color.White;
            this.txtMaxPitchKi.BorderColor = System.Drawing.Color.Empty;
            this.txtMaxPitchKi.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMaxPitchKi.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMaxPitchKi.Location = new System.Drawing.Point(213, 100);
            this.txtMaxPitchKi.Multiline = false;
            this.txtMaxPitchKi.Name = "txtMaxPitchKi";
            this.txtMaxPitchKi.Size = new System.Drawing.Size(31, 20);
            this.txtMaxPitchKi.TabIndex = 16;
            this.txtMaxPitchKi.TextChanged += new Utils.WinFormControls.TextChangedEventHandler(this.txtMaxPitchKi_TextChanged);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(184, 103);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(27, 13);
            this.label41.TabIndex = 15;
            this.label41.Text = "Max";
            // 
            // txtMinPitchKi
            // 
            this.txtMinPitchKi.BackColor = System.Drawing.Color.White;
            this.txtMinPitchKi.BorderColor = System.Drawing.Color.Empty;
            this.txtMinPitchKi.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMinPitchKi.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMinPitchKi.Location = new System.Drawing.Point(146, 100);
            this.txtMinPitchKi.Multiline = false;
            this.txtMinPitchKi.Name = "txtMinPitchKi";
            this.txtMinPitchKi.Size = new System.Drawing.Size(31, 20);
            this.txtMinPitchKi.TabIndex = 14;
            this.txtMinPitchKi.TextChanged += new Utils.WinFormControls.TextChangedEventHandler(this.txtMinPitchKi_TextChanged);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(121, 103);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(24, 13);
            this.label42.TabIndex = 13;
            this.label42.Text = "Min";
            // 
            // txtMaxPitchKp
            // 
            this.txtMaxPitchKp.BackColor = System.Drawing.Color.White;
            this.txtMaxPitchKp.BorderColor = System.Drawing.Color.Empty;
            this.txtMaxPitchKp.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMaxPitchKp.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMaxPitchKp.Location = new System.Drawing.Point(213, 26);
            this.txtMaxPitchKp.Multiline = false;
            this.txtMaxPitchKp.Name = "txtMaxPitchKp";
            this.txtMaxPitchKp.Size = new System.Drawing.Size(31, 20);
            this.txtMaxPitchKp.TabIndex = 12;
            this.txtMaxPitchKp.TextChanged += new Utils.WinFormControls.TextChangedEventHandler(this.txtMaxPitchKp_TextChanged);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(184, 29);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(27, 13);
            this.label43.TabIndex = 11;
            this.label43.Text = "Max";
            // 
            // txtMinPitchKp
            // 
            this.txtMinPitchKp.BackColor = System.Drawing.Color.White;
            this.txtMinPitchKp.BorderColor = System.Drawing.Color.Empty;
            this.txtMinPitchKp.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMinPitchKp.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMinPitchKp.Location = new System.Drawing.Point(146, 26);
            this.txtMinPitchKp.Multiline = false;
            this.txtMinPitchKp.Name = "txtMinPitchKp";
            this.txtMinPitchKp.Size = new System.Drawing.Size(31, 20);
            this.txtMinPitchKp.TabIndex = 10;
            this.txtMinPitchKp.TextChanged += new Utils.WinFormControls.TextChangedEventHandler(this.txtMinPitchKp_TextChanged);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(121, 29);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(24, 13);
            this.label44.TabIndex = 9;
            this.label44.Text = "Min";
            // 
            // tbPitchKd
            // 
            this.tbPitchKd.Location = new System.Drawing.Point(6, 200);
            this.tbPitchKd.Maximum = 2000;
            this.tbPitchKd.Name = "tbPitchKd";
            this.tbPitchKd.Size = new System.Drawing.Size(237, 45);
            this.tbPitchKd.TabIndex = 8;
            // 
            // tbPitchKi
            // 
            this.tbPitchKi.Location = new System.Drawing.Point(6, 124);
            this.tbPitchKi.Maximum = 2000;
            this.tbPitchKi.Name = "tbPitchKi";
            this.tbPitchKi.Size = new System.Drawing.Size(237, 45);
            this.tbPitchKi.TabIndex = 7;
            // 
            // tbPitchKp
            // 
            this.tbPitchKp.Location = new System.Drawing.Point(6, 48);
            this.tbPitchKp.Maximum = 2000;
            this.tbPitchKp.Name = "tbPitchKp";
            this.tbPitchKp.Size = new System.Drawing.Size(237, 45);
            this.tbPitchKp.TabIndex = 6;
            // 
            // txtPitchKd
            // 
            this.txtPitchKd.BackColor = System.Drawing.Color.White;
            this.txtPitchKd.BorderColor = System.Drawing.Color.Empty;
            this.txtPitchKd.BorderColorHover = System.Drawing.Color.Empty;
            this.txtPitchKd.ForeColorHover = System.Drawing.Color.Empty;
            this.txtPitchKd.Location = new System.Drawing.Point(25, 177);
            this.txtPitchKd.Multiline = false;
            this.txtPitchKd.Name = "txtPitchKd";
            this.txtPitchKd.Size = new System.Drawing.Size(40, 20);
            this.txtPitchKd.TabIndex = 5;
            this.txtPitchKd.TextChanged += new Utils.WinFormControls.TextChangedEventHandler(this.txtPitchKd_TextChanged);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(6, 180);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(20, 13);
            this.label45.TabIndex = 4;
            this.label45.Text = "Kd";
            // 
            // btnUpdatePitchGains
            // 
            this.btnUpdatePitchGains.Location = new System.Drawing.Point(160, 249);
            this.btnUpdatePitchGains.Name = "btnUpdatePitchGains";
            this.btnUpdatePitchGains.Size = new System.Drawing.Size(75, 23);
            this.btnUpdatePitchGains.TabIndex = 1;
            this.btnUpdatePitchGains.Text = "Update";
            this.btnUpdatePitchGains.UseVisualStyleBackColor = true;
            this.btnUpdatePitchGains.VerticalText = false;
            this.btnUpdatePitchGains.Click += new System.EventHandler(this.btnUpdatePitchGains_Click);
            // 
            // txtPitchKi
            // 
            this.txtPitchKi.BackColor = System.Drawing.Color.White;
            this.txtPitchKi.BorderColor = System.Drawing.Color.Empty;
            this.txtPitchKi.BorderColorHover = System.Drawing.Color.Empty;
            this.txtPitchKi.ForeColorHover = System.Drawing.Color.Empty;
            this.txtPitchKi.Location = new System.Drawing.Point(25, 100);
            this.txtPitchKi.Multiline = false;
            this.txtPitchKi.Name = "txtPitchKi";
            this.txtPitchKi.Size = new System.Drawing.Size(40, 20);
            this.txtPitchKi.TabIndex = 3;
            this.txtPitchKi.TextChanged += new Utils.WinFormControls.TextChangedEventHandler(this.txtPitchKi_TextChanged);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(6, 103);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(16, 13);
            this.label46.TabIndex = 2;
            this.label46.Text = "Ki";
            // 
            // txtPitchKp
            // 
            this.txtPitchKp.BackColor = System.Drawing.Color.White;
            this.txtPitchKp.BorderColor = System.Drawing.Color.Empty;
            this.txtPitchKp.BorderColorHover = System.Drawing.Color.Empty;
            this.txtPitchKp.ForeColorHover = System.Drawing.Color.Empty;
            this.txtPitchKp.Location = new System.Drawing.Point(25, 23);
            this.txtPitchKp.Multiline = false;
            this.txtPitchKp.Name = "txtPitchKp";
            this.txtPitchKp.Size = new System.Drawing.Size(40, 20);
            this.txtPitchKp.TabIndex = 1;
            this.txtPitchKp.TextChanged += new Utils.WinFormControls.TextChangedEventHandler(this.txtPitchKp_TextChanged);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(4, 26);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(20, 13);
            this.label47.TabIndex = 1;
            this.label47.Text = "Kp";
            // 
            // PitchPidGains
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 297);
            this.Controls.Add(this.groupBox14);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PitchPidGains";
            this.Text = "PitchPidGains";
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPitchKd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPitchKi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPitchKp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Utils.WinFormControls.CustomTextBox txtMaxPitchKd;
        private System.Windows.Forms.Label label39;
        private Utils.WinFormControls.CustomTextBox txtMinPitchKd;
        private System.Windows.Forms.Label label40;
        private Utils.WinFormControls.CustomTextBox txtMaxPitchKi;
        private System.Windows.Forms.Label label41;
        private Utils.WinFormControls.CustomTextBox txtMinPitchKi;
        private System.Windows.Forms.Label label42;
        private Utils.WinFormControls.CustomTextBox txtMaxPitchKp;
        private System.Windows.Forms.Label label43;
        private Utils.WinFormControls.CustomTextBox txtMinPitchKp;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TrackBar tbPitchKd;
        private System.Windows.Forms.TrackBar tbPitchKi;
        private System.Windows.Forms.TrackBar tbPitchKp;
        private Utils.WinFormControls.CustomTextBox txtPitchKd;
        private System.Windows.Forms.Label label45;
        private Utils.WinFormControls.CustomButton btnUpdatePitchGains;
        private Utils.WinFormControls.CustomTextBox txtPitchKi;
        private System.Windows.Forms.Label label46;
        private Utils.WinFormControls.CustomTextBox txtPitchKp;
        private System.Windows.Forms.Label label47;
        private Utils.WinFormControls.CustomGroupBox groupBox14;
    }
}