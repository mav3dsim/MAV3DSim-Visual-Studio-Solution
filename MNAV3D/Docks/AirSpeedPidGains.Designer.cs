namespace MAV3DSim.Docks
{
    partial class AirSpeedPidGains
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
            this.groupBox16 = new MAV3DSim.Utils.WinFormControls.CustomGroupBox();
            this.txtSpeedFF = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.txtMaxSpeedFF = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMinSpeedFF = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSpeedFF = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaxSpeedKd = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.txtMinSpeedKd = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.txtMaxSpeedKi = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.txtMinSpeedKi = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.txtMaxSpeedKp = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label61 = new System.Windows.Forms.Label();
            this.txtMinSpeedKp = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label62 = new System.Windows.Forms.Label();
            this.tbSpeedKd = new System.Windows.Forms.TrackBar();
            this.tbSpeedKi = new System.Windows.Forms.TrackBar();
            this.tbSpeedKp = new System.Windows.Forms.TrackBar();
            this.txtSpeedKd = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label63 = new System.Windows.Forms.Label();
            this.txtSpeedKi = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.txtSpeedKp = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label65 = new System.Windows.Forms.Label();
            this.btnUpdateSpeedGains = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.groupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeedFF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeedKd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeedKi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeedKp)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox16
            // 
            this.groupBox16.BorderColor = System.Drawing.Color.Black;
            this.groupBox16.Controls.Add(this.txtSpeedFF);
            this.groupBox16.Controls.Add(this.txtMaxSpeedFF);
            this.groupBox16.Controls.Add(this.label1);
            this.groupBox16.Controls.Add(this.txtMinSpeedFF);
            this.groupBox16.Controls.Add(this.label2);
            this.groupBox16.Controls.Add(this.tbSpeedFF);
            this.groupBox16.Controls.Add(this.label3);
            this.groupBox16.Controls.Add(this.txtMaxSpeedKd);
            this.groupBox16.Controls.Add(this.label57);
            this.groupBox16.Controls.Add(this.txtMinSpeedKd);
            this.groupBox16.Controls.Add(this.label58);
            this.groupBox16.Controls.Add(this.txtMaxSpeedKi);
            this.groupBox16.Controls.Add(this.label59);
            this.groupBox16.Controls.Add(this.txtMinSpeedKi);
            this.groupBox16.Controls.Add(this.label60);
            this.groupBox16.Controls.Add(this.txtMaxSpeedKp);
            this.groupBox16.Controls.Add(this.label61);
            this.groupBox16.Controls.Add(this.txtMinSpeedKp);
            this.groupBox16.Controls.Add(this.label62);
            this.groupBox16.Controls.Add(this.tbSpeedKd);
            this.groupBox16.Controls.Add(this.tbSpeedKi);
            this.groupBox16.Controls.Add(this.tbSpeedKp);
            this.groupBox16.Controls.Add(this.txtSpeedKd);
            this.groupBox16.Controls.Add(this.label63);
            this.groupBox16.Controls.Add(this.txtSpeedKi);
            this.groupBox16.Controls.Add(this.label64);
            this.groupBox16.Controls.Add(this.txtSpeedKp);
            this.groupBox16.Controls.Add(this.label65);
            this.groupBox16.Location = new System.Drawing.Point(12, 12);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.RoundCorners = 0;
            this.groupBox16.Size = new System.Drawing.Size(255, 306);
            this.groupBox16.TabIndex = 24;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Speed Gain";
            // 
            // txtSpeedFF
            // 
            this.txtSpeedFF.BackColor = System.Drawing.Color.White;
            this.txtSpeedFF.BorderColor = System.Drawing.Color.Empty;
            this.txtSpeedFF.BorderColorHover = System.Drawing.Color.Empty;
            this.txtSpeedFF.ForeColorHover = System.Drawing.Color.Empty;
            this.txtSpeedFF.Location = new System.Drawing.Point(25, 245);
            this.txtSpeedFF.Multiline = false;
            this.txtSpeedFF.Name = "txtSpeedFF";
            this.txtSpeedFF.Size = new System.Drawing.Size(40, 20);
            this.txtSpeedFF.TabIndex = 27;
            this.txtSpeedFF.TextChanged += new MAV3DSim.Utils.WinFormControls.TextChangedEventHandler(this.txtSpeedFF_TextChanged);
            // 
            // txtMaxSpeedFF
            // 
            this.txtMaxSpeedFF.BackColor = System.Drawing.Color.White;
            this.txtMaxSpeedFF.BorderColor = System.Drawing.Color.Empty;
            this.txtMaxSpeedFF.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMaxSpeedFF.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMaxSpeedFF.Location = new System.Drawing.Point(211, 245);
            this.txtMaxSpeedFF.Multiline = false;
            this.txtMaxSpeedFF.Name = "txtMaxSpeedFF";
            this.txtMaxSpeedFF.Size = new System.Drawing.Size(31, 20);
            this.txtMaxSpeedFF.TabIndex = 26;
            this.txtMaxSpeedFF.Load += new System.EventHandler(this.txtMaxSpeedFF_Load);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 248);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Max";
            // 
            // txtMinSpeedFF
            // 
            this.txtMinSpeedFF.BackColor = System.Drawing.Color.White;
            this.txtMinSpeedFF.BorderColor = System.Drawing.Color.Empty;
            this.txtMinSpeedFF.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMinSpeedFF.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMinSpeedFF.Location = new System.Drawing.Point(144, 245);
            this.txtMinSpeedFF.Multiline = false;
            this.txtMinSpeedFF.Name = "txtMinSpeedFF";
            this.txtMinSpeedFF.Size = new System.Drawing.Size(31, 20);
            this.txtMinSpeedFF.TabIndex = 24;
            this.txtMinSpeedFF.Load += new System.EventHandler(this.txtMinSpeedFF_Load);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 248);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Min";
            // 
            // tbSpeedFF
            // 
            this.tbSpeedFF.Location = new System.Drawing.Point(6, 268);
            this.tbSpeedFF.Maximum = 2000;
            this.tbSpeedFF.Name = "tbSpeedFF";
            this.tbSpeedFF.Size = new System.Drawing.Size(237, 45);
            this.tbSpeedFF.TabIndex = 22;
            this.tbSpeedFF.ValueChanged += new System.EventHandler(this.tbSpeedFF_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 248);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "FF";
            // 
            // txtMaxSpeedKd
            // 
            this.txtMaxSpeedKd.BackColor = System.Drawing.Color.White;
            this.txtMaxSpeedKd.BorderColor = System.Drawing.Color.Empty;
            this.txtMaxSpeedKd.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMaxSpeedKd.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMaxSpeedKd.Location = new System.Drawing.Point(211, 177);
            this.txtMaxSpeedKd.Multiline = false;
            this.txtMaxSpeedKd.Name = "txtMaxSpeedKd";
            this.txtMaxSpeedKd.Size = new System.Drawing.Size(31, 20);
            this.txtMaxSpeedKd.TabIndex = 20;
            this.txtMaxSpeedKd.TextChanged += new MAV3DSim.Utils.WinFormControls.TextChangedEventHandler(this.txtMaxSpeedKd_TextChanged);
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(182, 180);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(27, 13);
            this.label57.TabIndex = 19;
            this.label57.Text = "Max";
            // 
            // txtMinSpeedKd
            // 
            this.txtMinSpeedKd.BackColor = System.Drawing.Color.White;
            this.txtMinSpeedKd.BorderColor = System.Drawing.Color.Empty;
            this.txtMinSpeedKd.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMinSpeedKd.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMinSpeedKd.Location = new System.Drawing.Point(144, 177);
            this.txtMinSpeedKd.Multiline = false;
            this.txtMinSpeedKd.Name = "txtMinSpeedKd";
            this.txtMinSpeedKd.Size = new System.Drawing.Size(31, 20);
            this.txtMinSpeedKd.TabIndex = 18;
            this.txtMinSpeedKd.TextChanged += new MAV3DSim.Utils.WinFormControls.TextChangedEventHandler(this.txtMinSpeedKd_TextChanged);
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(119, 180);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(24, 13);
            this.label58.TabIndex = 17;
            this.label58.Text = "Min";
            // 
            // txtMaxSpeedKi
            // 
            this.txtMaxSpeedKi.BackColor = System.Drawing.Color.White;
            this.txtMaxSpeedKi.BorderColor = System.Drawing.Color.Empty;
            this.txtMaxSpeedKi.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMaxSpeedKi.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMaxSpeedKi.Location = new System.Drawing.Point(211, 100);
            this.txtMaxSpeedKi.Multiline = false;
            this.txtMaxSpeedKi.Name = "txtMaxSpeedKi";
            this.txtMaxSpeedKi.Size = new System.Drawing.Size(31, 20);
            this.txtMaxSpeedKi.TabIndex = 16;
            this.txtMaxSpeedKi.TextChanged += new MAV3DSim.Utils.WinFormControls.TextChangedEventHandler(this.txtMaxSpeedKi_TextChanged);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(182, 103);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(27, 13);
            this.label59.TabIndex = 15;
            this.label59.Text = "Max";
            // 
            // txtMinSpeedKi
            // 
            this.txtMinSpeedKi.BackColor = System.Drawing.Color.White;
            this.txtMinSpeedKi.BorderColor = System.Drawing.Color.Empty;
            this.txtMinSpeedKi.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMinSpeedKi.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMinSpeedKi.Location = new System.Drawing.Point(144, 100);
            this.txtMinSpeedKi.Multiline = false;
            this.txtMinSpeedKi.Name = "txtMinSpeedKi";
            this.txtMinSpeedKi.Size = new System.Drawing.Size(31, 20);
            this.txtMinSpeedKi.TabIndex = 14;
            this.txtMinSpeedKi.TextChanged += new MAV3DSim.Utils.WinFormControls.TextChangedEventHandler(this.txtMinSpeedKi_TextChanged);
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(119, 103);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(24, 13);
            this.label60.TabIndex = 13;
            this.label60.Text = "Min";
            // 
            // txtMaxSpeedKp
            // 
            this.txtMaxSpeedKp.BackColor = System.Drawing.Color.White;
            this.txtMaxSpeedKp.BorderColor = System.Drawing.Color.Empty;
            this.txtMaxSpeedKp.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMaxSpeedKp.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMaxSpeedKp.Location = new System.Drawing.Point(211, 26);
            this.txtMaxSpeedKp.Multiline = false;
            this.txtMaxSpeedKp.Name = "txtMaxSpeedKp";
            this.txtMaxSpeedKp.Size = new System.Drawing.Size(31, 20);
            this.txtMaxSpeedKp.TabIndex = 12;
            this.txtMaxSpeedKp.TextChanged += new MAV3DSim.Utils.WinFormControls.TextChangedEventHandler(this.txtMaxSpeedKp_TextChanged);
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(182, 29);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(27, 13);
            this.label61.TabIndex = 11;
            this.label61.Text = "Max";
            // 
            // txtMinSpeedKp
            // 
            this.txtMinSpeedKp.BackColor = System.Drawing.Color.White;
            this.txtMinSpeedKp.BorderColor = System.Drawing.Color.Empty;
            this.txtMinSpeedKp.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMinSpeedKp.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMinSpeedKp.Location = new System.Drawing.Point(144, 26);
            this.txtMinSpeedKp.Multiline = false;
            this.txtMinSpeedKp.Name = "txtMinSpeedKp";
            this.txtMinSpeedKp.Size = new System.Drawing.Size(31, 20);
            this.txtMinSpeedKp.TabIndex = 10;
            this.txtMinSpeedKp.TextChanged += new MAV3DSim.Utils.WinFormControls.TextChangedEventHandler(this.txtMinSpeedKp_TextChanged);
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(119, 29);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(24, 13);
            this.label62.TabIndex = 9;
            this.label62.Text = "Min";
            // 
            // tbSpeedKd
            // 
            this.tbSpeedKd.Location = new System.Drawing.Point(6, 200);
            this.tbSpeedKd.Maximum = 2000;
            this.tbSpeedKd.Name = "tbSpeedKd";
            this.tbSpeedKd.Size = new System.Drawing.Size(237, 45);
            this.tbSpeedKd.TabIndex = 8;
            this.tbSpeedKd.ValueChanged += new System.EventHandler(this.tbSpeedKd_ValueChanged);
            // 
            // tbSpeedKi
            // 
            this.tbSpeedKi.Location = new System.Drawing.Point(6, 124);
            this.tbSpeedKi.Maximum = 2000;
            this.tbSpeedKi.Name = "tbSpeedKi";
            this.tbSpeedKi.Size = new System.Drawing.Size(237, 45);
            this.tbSpeedKi.TabIndex = 7;
            this.tbSpeedKi.ValueChanged += new System.EventHandler(this.tbSpeedKi_ValueChanged);
            // 
            // tbSpeedKp
            // 
            this.tbSpeedKp.Location = new System.Drawing.Point(6, 48);
            this.tbSpeedKp.Maximum = 2000;
            this.tbSpeedKp.Name = "tbSpeedKp";
            this.tbSpeedKp.Size = new System.Drawing.Size(237, 45);
            this.tbSpeedKp.TabIndex = 6;
            this.tbSpeedKp.ValueChanged += new System.EventHandler(this.tbSpeedKp_ValueChanged);
            // 
            // txtSpeedKd
            // 
            this.txtSpeedKd.BackColor = System.Drawing.Color.White;
            this.txtSpeedKd.BorderColor = System.Drawing.Color.Empty;
            this.txtSpeedKd.BorderColorHover = System.Drawing.Color.Empty;
            this.txtSpeedKd.ForeColorHover = System.Drawing.Color.Empty;
            this.txtSpeedKd.Location = new System.Drawing.Point(25, 177);
            this.txtSpeedKd.Multiline = false;
            this.txtSpeedKd.Name = "txtSpeedKd";
            this.txtSpeedKd.Size = new System.Drawing.Size(40, 20);
            this.txtSpeedKd.TabIndex = 5;
            this.txtSpeedKd.TextChanged += new MAV3DSim.Utils.WinFormControls.TextChangedEventHandler(this.txtSpeedKd_TextChanged);
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(6, 180);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(20, 13);
            this.label63.TabIndex = 4;
            this.label63.Text = "Kd";
            // 
            // txtSpeedKi
            // 
            this.txtSpeedKi.BackColor = System.Drawing.Color.White;
            this.txtSpeedKi.BorderColor = System.Drawing.Color.Empty;
            this.txtSpeedKi.BorderColorHover = System.Drawing.Color.Empty;
            this.txtSpeedKi.ForeColorHover = System.Drawing.Color.Empty;
            this.txtSpeedKi.Location = new System.Drawing.Point(25, 100);
            this.txtSpeedKi.Multiline = false;
            this.txtSpeedKi.Name = "txtSpeedKi";
            this.txtSpeedKi.Size = new System.Drawing.Size(40, 20);
            this.txtSpeedKi.TabIndex = 3;
            this.txtSpeedKi.TextChanged += new MAV3DSim.Utils.WinFormControls.TextChangedEventHandler(this.txtSpeedKi_TextChanged);
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(6, 103);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(16, 13);
            this.label64.TabIndex = 2;
            this.label64.Text = "Ki";
            // 
            // txtSpeedKp
            // 
            this.txtSpeedKp.BackColor = System.Drawing.Color.White;
            this.txtSpeedKp.BorderColor = System.Drawing.Color.Empty;
            this.txtSpeedKp.BorderColorHover = System.Drawing.Color.Empty;
            this.txtSpeedKp.ForeColorHover = System.Drawing.Color.Empty;
            this.txtSpeedKp.Location = new System.Drawing.Point(25, 23);
            this.txtSpeedKp.Multiline = false;
            this.txtSpeedKp.Name = "txtSpeedKp";
            this.txtSpeedKp.Size = new System.Drawing.Size(40, 20);
            this.txtSpeedKp.TabIndex = 1;
            this.txtSpeedKp.Enter += new System.EventHandler(this.txtSpeedKp_TextChanged);
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(4, 26);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(20, 13);
            this.label65.TabIndex = 1;
            this.label65.Text = "Kp";
            // 
            // btnUpdateSpeedGains
            // 
            this.btnUpdateSpeedGains.Location = new System.Drawing.Point(179, 362);
            this.btnUpdateSpeedGains.Name = "btnUpdateSpeedGains";
            this.btnUpdateSpeedGains.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateSpeedGains.TabIndex = 1;
            this.btnUpdateSpeedGains.Text = "Update";
            this.btnUpdateSpeedGains.UseVisualStyleBackColor = true;
            this.btnUpdateSpeedGains.VerticalText = false;
            this.btnUpdateSpeedGains.Click += new System.EventHandler(this.btnUpdateSpeedGains_Click);
            // 
            // AirSpeedPidGains
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 397);
            this.Controls.Add(this.groupBox16);
            this.Controls.Add(this.btnUpdateSpeedGains);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "AirSpeedPidGains";
            this.Text = "SpeedPidGains";
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeedFF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeedKd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeedKi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeedKp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Utils.WinFormControls.CustomTextBox txtMaxSpeedKd;
        private System.Windows.Forms.Label label57;
        private Utils.WinFormControls.CustomTextBox txtMinSpeedKd;
        private System.Windows.Forms.Label label58;
        private Utils.WinFormControls.CustomTextBox txtMaxSpeedKi;
        private System.Windows.Forms.Label label59;
        private Utils.WinFormControls.CustomTextBox txtMinSpeedKi;
        private System.Windows.Forms.Label label60;
        private Utils.WinFormControls.CustomTextBox txtMaxSpeedKp;
        private System.Windows.Forms.Label label61;
        private Utils.WinFormControls.CustomTextBox txtMinSpeedKp;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.TrackBar tbSpeedKd;
        private System.Windows.Forms.TrackBar tbSpeedKi;
        private System.Windows.Forms.TrackBar tbSpeedKp;
        private Utils.WinFormControls.CustomTextBox txtSpeedKd;
        private System.Windows.Forms.Label label63;
        private Utils.WinFormControls.CustomButton btnUpdateSpeedGains;
        private Utils.WinFormControls.CustomTextBox txtSpeedKi;
        private System.Windows.Forms.Label label64;
        private Utils.WinFormControls.CustomTextBox txtSpeedKp;
        private System.Windows.Forms.Label label65;
        private Utils.WinFormControls.CustomGroupBox groupBox16;
        private Utils.WinFormControls.CustomTextBox txtMaxSpeedFF;
        private System.Windows.Forms.Label label1;
        private Utils.WinFormControls.CustomTextBox txtMinSpeedFF;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbSpeedFF;
        private System.Windows.Forms.Label label3;
        private Utils.WinFormControls.CustomTextBox txtSpeedFF;
    }
}