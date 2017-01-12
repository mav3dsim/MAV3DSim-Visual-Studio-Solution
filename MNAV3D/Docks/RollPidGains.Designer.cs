namespace MAV3DSim.Docks
{
    partial class RollPidGains
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
            this.groupBox13 = new MAV3DSim.Utils.WinFormControls.CustomGroupBox();
            this.txtMaxRollKd = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.txtMinRollKd = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.txtMaxRollKi = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.txtMinRollKi = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txtMaxRollKp = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.txtMinRollKp = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.tbRollKd = new System.Windows.Forms.TrackBar();
            this.tbRollKi = new System.Windows.Forms.TrackBar();
            this.tbRollKp = new System.Windows.Forms.TrackBar();
            this.txtRollKd = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.btnUpdateRollGains = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.txtRollKi = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtRollKp = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbRollKd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRollKi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRollKp)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox13
            // 
            this.groupBox13.BorderColor = System.Drawing.Color.Black;
            this.groupBox13.Controls.Add(this.txtMaxRollKd);
            this.groupBox13.Controls.Add(this.label37);
            this.groupBox13.Controls.Add(this.txtMinRollKd);
            this.groupBox13.Controls.Add(this.label38);
            this.groupBox13.Controls.Add(this.txtMaxRollKi);
            this.groupBox13.Controls.Add(this.label35);
            this.groupBox13.Controls.Add(this.txtMinRollKi);
            this.groupBox13.Controls.Add(this.label36);
            this.groupBox13.Controls.Add(this.txtMaxRollKp);
            this.groupBox13.Controls.Add(this.label34);
            this.groupBox13.Controls.Add(this.txtMinRollKp);
            this.groupBox13.Controls.Add(this.label28);
            this.groupBox13.Controls.Add(this.tbRollKd);
            this.groupBox13.Controls.Add(this.tbRollKi);
            this.groupBox13.Controls.Add(this.tbRollKp);
            this.groupBox13.Controls.Add(this.txtRollKd);
            this.groupBox13.Controls.Add(this.label26);
            this.groupBox13.Controls.Add(this.btnUpdateRollGains);
            this.groupBox13.Controls.Add(this.txtRollKi);
            this.groupBox13.Controls.Add(this.label30);
            this.groupBox13.Controls.Add(this.txtRollKp);
            this.groupBox13.Controls.Add(this.label31);
            this.groupBox13.Location = new System.Drawing.Point(12, 12);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.RoundCorners = 0;
            this.groupBox13.Size = new System.Drawing.Size(255, 278);
            this.groupBox13.TabIndex = 5;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Roll Gain";
            // 
            // txtMaxRollKd
            // 
            this.txtMaxRollKd.BackColor = System.Drawing.Color.White;
            this.txtMaxRollKd.BorderColor = System.Drawing.Color.Empty;
            this.txtMaxRollKd.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMaxRollKd.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMaxRollKd.Location = new System.Drawing.Point(211, 177);
            this.txtMaxRollKd.Multiline = false;
            this.txtMaxRollKd.Name = "txtMaxRollKd";
            this.txtMaxRollKd.Size = new System.Drawing.Size(31, 20);
            this.txtMaxRollKd.TabIndex = 20;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(182, 180);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(27, 13);
            this.label37.TabIndex = 19;
            this.label37.Text = "Max";
            // 
            // txtMinRollKd
            // 
            this.txtMinRollKd.BackColor = System.Drawing.Color.White;
            this.txtMinRollKd.BorderColor = System.Drawing.Color.Empty;
            this.txtMinRollKd.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMinRollKd.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMinRollKd.Location = new System.Drawing.Point(144, 177);
            this.txtMinRollKd.Multiline = false;
            this.txtMinRollKd.Name = "txtMinRollKd";
            this.txtMinRollKd.Size = new System.Drawing.Size(31, 20);
            this.txtMinRollKd.TabIndex = 18;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(119, 180);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(24, 13);
            this.label38.TabIndex = 17;
            this.label38.Text = "Min";
            // 
            // txtMaxRollKi
            // 
            this.txtMaxRollKi.BackColor = System.Drawing.Color.White;
            this.txtMaxRollKi.BorderColor = System.Drawing.Color.Empty;
            this.txtMaxRollKi.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMaxRollKi.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMaxRollKi.Location = new System.Drawing.Point(211, 100);
            this.txtMaxRollKi.Multiline = false;
            this.txtMaxRollKi.Name = "txtMaxRollKi";
            this.txtMaxRollKi.Size = new System.Drawing.Size(31, 20);
            this.txtMaxRollKi.TabIndex = 16;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(182, 103);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(27, 13);
            this.label35.TabIndex = 15;
            this.label35.Text = "Max";
            // 
            // txtMinRollKi
            // 
            this.txtMinRollKi.BackColor = System.Drawing.Color.White;
            this.txtMinRollKi.BorderColor = System.Drawing.Color.Empty;
            this.txtMinRollKi.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMinRollKi.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMinRollKi.Location = new System.Drawing.Point(144, 100);
            this.txtMinRollKi.Multiline = false;
            this.txtMinRollKi.Name = "txtMinRollKi";
            this.txtMinRollKi.Size = new System.Drawing.Size(31, 20);
            this.txtMinRollKi.TabIndex = 14;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(119, 103);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(24, 13);
            this.label36.TabIndex = 13;
            this.label36.Text = "Min";
            // 
            // txtMaxRollKp
            // 
            this.txtMaxRollKp.BackColor = System.Drawing.Color.White;
            this.txtMaxRollKp.BorderColor = System.Drawing.Color.Empty;
            this.txtMaxRollKp.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMaxRollKp.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMaxRollKp.Location = new System.Drawing.Point(211, 26);
            this.txtMaxRollKp.Multiline = false;
            this.txtMaxRollKp.Name = "txtMaxRollKp";
            this.txtMaxRollKp.Size = new System.Drawing.Size(31, 20);
            this.txtMaxRollKp.TabIndex = 12;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(182, 29);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(27, 13);
            this.label34.TabIndex = 11;
            this.label34.Text = "Max";
            // 
            // txtMinRollKp
            // 
            this.txtMinRollKp.BackColor = System.Drawing.Color.White;
            this.txtMinRollKp.BorderColor = System.Drawing.Color.Empty;
            this.txtMinRollKp.BorderColorHover = System.Drawing.Color.Empty;
            this.txtMinRollKp.ForeColorHover = System.Drawing.Color.Empty;
            this.txtMinRollKp.Location = new System.Drawing.Point(144, 26);
            this.txtMinRollKp.Multiline = false;
            this.txtMinRollKp.Name = "txtMinRollKp";
            this.txtMinRollKp.Size = new System.Drawing.Size(31, 20);
            this.txtMinRollKp.TabIndex = 10;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(119, 29);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(24, 13);
            this.label28.TabIndex = 9;
            this.label28.Text = "Min";
            // 
            // tbRollKd
            // 
            this.tbRollKd.Location = new System.Drawing.Point(6, 200);
            this.tbRollKd.Maximum = 2000;
            this.tbRollKd.Name = "tbRollKd";
            this.tbRollKd.Size = new System.Drawing.Size(237, 45);
            this.tbRollKd.TabIndex = 8;
            this.tbRollKd.ValueChanged += new System.EventHandler(this.tbRollKd_ValueChanged);
            // 
            // tbRollKi
            // 
            this.tbRollKi.Location = new System.Drawing.Point(6, 124);
            this.tbRollKi.Maximum = 2000;
            this.tbRollKi.Name = "tbRollKi";
            this.tbRollKi.Size = new System.Drawing.Size(237, 45);
            this.tbRollKi.TabIndex = 7;
            this.tbRollKi.ValueChanged += new System.EventHandler(this.tbRollKi_ValueChanged);
            // 
            // tbRollKp
            // 
            this.tbRollKp.Location = new System.Drawing.Point(6, 48);
            this.tbRollKp.Maximum = 2000;
            this.tbRollKp.Name = "tbRollKp";
            this.tbRollKp.Size = new System.Drawing.Size(237, 45);
            this.tbRollKp.TabIndex = 6;
            this.tbRollKp.ValueChanged += new System.EventHandler(this.tbRollKp_ValueChanged);
            // 
            // txtRollKd
            // 
            this.txtRollKd.BackColor = System.Drawing.Color.White;
            this.txtRollKd.BorderColor = System.Drawing.Color.Empty;
            this.txtRollKd.BorderColorHover = System.Drawing.Color.Empty;
            this.txtRollKd.ForeColorHover = System.Drawing.Color.Empty;
            this.txtRollKd.Location = new System.Drawing.Point(25, 177);
            this.txtRollKd.Multiline = false;
            this.txtRollKd.Name = "txtRollKd";
            this.txtRollKd.Size = new System.Drawing.Size(40, 20);
            this.txtRollKd.TabIndex = 5;
            this.txtRollKd.TextChanged += txtRollKd_TextChanged;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 180);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(20, 13);
            this.label26.TabIndex = 4;
            this.label26.Text = "Kd";
            // 
            // btnUpdateRollGains
            // 
            this.btnUpdateRollGains.Location = new System.Drawing.Point(160, 249);
            this.btnUpdateRollGains.Name = "btnUpdateRollGains";
            this.btnUpdateRollGains.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateRollGains.TabIndex = 1;
            this.btnUpdateRollGains.Text = "Update";
            this.btnUpdateRollGains.UseVisualStyleBackColor = true;
            this.btnUpdateRollGains.VerticalText = false;
            this.btnUpdateRollGains.Click += new System.EventHandler(this.btnUpdateRollGains_Click);
            // 
            // txtRollKi
            // 
            this.txtRollKi.BackColor = System.Drawing.Color.White;
            this.txtRollKi.BorderColor = System.Drawing.Color.Empty;
            this.txtRollKi.BorderColorHover = System.Drawing.Color.Empty;
            this.txtRollKi.ForeColorHover = System.Drawing.Color.Empty;
            this.txtRollKi.Location = new System.Drawing.Point(25, 100);
            this.txtRollKi.Multiline = false;
            this.txtRollKi.Name = "txtRollKi";
            this.txtRollKi.Size = new System.Drawing.Size(40, 20);
            this.txtRollKi.TabIndex = 3;
            this.txtRollKi.TextChanged += txtRollKi_TextChanged;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 103);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(16, 13);
            this.label30.TabIndex = 2;
            this.label30.Text = "Ki";
            // 
            // txtRollKp
            // 
            this.txtRollKp.BackColor = System.Drawing.Color.White;
            this.txtRollKp.BorderColor = System.Drawing.Color.Empty;
            this.txtRollKp.BorderColorHover = System.Drawing.Color.Empty;
            this.txtRollKp.ForeColorHover = System.Drawing.Color.Empty;
            this.txtRollKp.Location = new System.Drawing.Point(25, 23);
            this.txtRollKp.Multiline = false;
            this.txtRollKp.Name = "txtRollKp";
            this.txtRollKp.Size = new System.Drawing.Size(40, 20);
            this.txtRollKp.TabIndex = 1;
            this.txtRollKp.TextChanged += txtRollKp_TextChanged;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(4, 26);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(20, 13);
            this.label31.TabIndex = 1;
            this.label31.Text = "Kp";
            // 
            // RollPidGains
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 296);
            this.Controls.Add(this.groupBox13);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "RollPidGains";
            this.Text = "RollPidGains";
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbRollKd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRollKi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRollKp)).EndInit();
            this.ResumeLayout(false);

        }

       

        #endregion

        private Utils.WinFormControls.CustomTextBox txtMaxRollKd;
        private System.Windows.Forms.Label label37;
        private Utils.WinFormControls.CustomTextBox txtMinRollKd;
        private System.Windows.Forms.Label label38;
        private Utils.WinFormControls.CustomTextBox txtMaxRollKi;
        private System.Windows.Forms.Label label35;
        private Utils.WinFormControls.CustomTextBox txtMinRollKi;
        private System.Windows.Forms.Label label36;
        private Utils.WinFormControls.CustomTextBox txtMaxRollKp;
        private System.Windows.Forms.Label label34;
        private Utils.WinFormControls.CustomTextBox txtMinRollKp;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TrackBar tbRollKd;
        private System.Windows.Forms.TrackBar tbRollKi;
        private System.Windows.Forms.TrackBar tbRollKp;
        private Utils.WinFormControls.CustomTextBox txtRollKd;
        private System.Windows.Forms.Label label26;
        private Utils.WinFormControls.CustomButton btnUpdateRollGains;
        private Utils.WinFormControls.CustomTextBox txtRollKi;
        private System.Windows.Forms.Label label30;
        private Utils.WinFormControls.CustomTextBox txtRollKp;
        private System.Windows.Forms.Label label31;
        private Utils.WinFormControls.CustomGroupBox groupBox13;
    }
}