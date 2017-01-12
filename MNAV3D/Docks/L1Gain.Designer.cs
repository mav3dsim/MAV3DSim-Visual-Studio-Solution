namespace MAV3DSim.Docks
{
    partial class L1Gain
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
            this.groupBox20 = new MAV3DSim.Utils.WinFormControls.CustomGroupBox();
            this.txtL1Max = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label88 = new System.Windows.Forms.Label();
            this.txtL1Min = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label89 = new System.Windows.Forms.Label();
            this.tbL1 = new System.Windows.Forms.TrackBar();
            this.btnL1Update = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.txtL1 = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label92 = new System.Windows.Forms.Label();
            this.txtTurnRadius = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbL1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox20
            // 
            this.groupBox20.BorderColor = System.Drawing.Color.Black;
            this.groupBox20.Controls.Add(this.txtL1Max);
            this.groupBox20.Controls.Add(this.label88);
            this.groupBox20.Controls.Add(this.txtL1Min);
            this.groupBox20.Controls.Add(this.label89);
            this.groupBox20.Controls.Add(this.tbL1);
            this.groupBox20.Controls.Add(this.btnL1Update);
            this.groupBox20.Controls.Add(this.txtL1);
            this.groupBox20.Controls.Add(this.label92);
            this.groupBox20.Location = new System.Drawing.Point(12, 12);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.RoundCorners = 0;
            this.groupBox20.Size = new System.Drawing.Size(255, 137);
            this.groupBox20.TabIndex = 26;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "L1 Gain";
            // 
            // txtL1Max
            // 
            this.txtL1Max.BackColor = System.Drawing.Color.White;
            this.txtL1Max.BorderColor = System.Drawing.Color.Empty;
            this.txtL1Max.BorderColorHover = System.Drawing.Color.Empty;
            this.txtL1Max.ForeColorHover = System.Drawing.Color.Empty;
            this.txtL1Max.Location = new System.Drawing.Point(211, 26);
            this.txtL1Max.Multiline = false;
            this.txtL1Max.Name = "txtL1Max";
            this.txtL1Max.Size = new System.Drawing.Size(31, 20);
            this.txtL1Max.TabIndex = 12;
            this.txtL1Max.TextChanged += new Utils.WinFormControls.TextChangedEventHandler(this.txtL1Max_TextChanged);
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(182, 29);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(27, 13);
            this.label88.TabIndex = 11;
            this.label88.Text = "Max";
            // 
            // txtL1Min
            // 
            this.txtL1Min.BackColor = System.Drawing.Color.White;
            this.txtL1Min.BorderColor = System.Drawing.Color.Empty;
            this.txtL1Min.BorderColorHover = System.Drawing.Color.Empty;
            this.txtL1Min.ForeColorHover = System.Drawing.Color.Empty;
            this.txtL1Min.Location = new System.Drawing.Point(144, 26);
            this.txtL1Min.Multiline = false;
            this.txtL1Min.Name = "txtL1Min";
            this.txtL1Min.Size = new System.Drawing.Size(31, 20);
            this.txtL1Min.TabIndex = 10;
            this.txtL1Min.TextChanged += new Utils.WinFormControls.TextChangedEventHandler(this.txtL1Min_TextChanged);
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(119, 29);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(24, 13);
            this.label89.TabIndex = 9;
            this.label89.Text = "Min";
            // 
            // tbL1
            // 
            this.tbL1.Location = new System.Drawing.Point(6, 48);
            this.tbL1.Maximum = 500;
            this.tbL1.Name = "tbL1";
            this.tbL1.Size = new System.Drawing.Size(237, 45);
            this.tbL1.TabIndex = 6;
            this.tbL1.ValueChanged += new System.EventHandler(this.tbL1_ValueChanged);
            // 
            // btnL1Update
            // 
            this.btnL1Update.Location = new System.Drawing.Point(166, 103);
            this.btnL1Update.Name = "btnL1Update";
            this.btnL1Update.Size = new System.Drawing.Size(75, 23);
            this.btnL1Update.TabIndex = 1;
            this.btnL1Update.Text = "Update";
            this.btnL1Update.UseVisualStyleBackColor = true;
            this.btnL1Update.VerticalText = false;
            this.btnL1Update.Click += new System.EventHandler(this.btnL1Update_Click);
            // 
            // txtL1
            // 
            this.txtL1.BackColor = System.Drawing.Color.White;
            this.txtL1.BorderColor = System.Drawing.Color.Empty;
            this.txtL1.BorderColorHover = System.Drawing.Color.Empty;
            this.txtL1.ForeColorHover = System.Drawing.Color.Empty;
            this.txtL1.Location = new System.Drawing.Point(25, 23);
            this.txtL1.Multiline = false;
            this.txtL1.Name = "txtL1";
            this.txtL1.Size = new System.Drawing.Size(40, 20);
            this.txtL1.TabIndex = 1;
            this.txtL1.TextChanged += new Utils.WinFormControls.TextChangedEventHandler(this.txtL1_TextChanged);
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(4, 26);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(19, 13);
            this.label92.TabIndex = 1;
            this.label92.Text = "L1";
            // 
            // txtTurnRadius
            // 
            this.txtTurnRadius.BackColor = System.Drawing.Color.White;
            this.txtTurnRadius.BorderColor = System.Drawing.Color.Empty;
            this.txtTurnRadius.BorderColorHover = System.Drawing.Color.Empty;
            this.txtTurnRadius.ForeColorHover = System.Drawing.Color.Empty;
            this.txtTurnRadius.Location = new System.Drawing.Point(78, 156);
            this.txtTurnRadius.Multiline = false;
            this.txtTurnRadius.Name = "txtTurnRadius";
            this.txtTurnRadius.Size = new System.Drawing.Size(79, 23);
            this.txtTurnRadius.TabIndex = 27;
            this.txtTurnRadius.TextChanged += new Utils.WinFormControls.TextChangedEventHandler(this.txtTurnRadius_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Turn radius";
            // 
            // L1Gain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 197);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTurnRadius);
            this.Controls.Add(this.groupBox20);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "L1Gain";
            this.Text = "L1Gain";
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbL1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Utils.WinFormControls.CustomTextBox txtL1Max;
        private System.Windows.Forms.Label label88;
        private Utils.WinFormControls.CustomTextBox txtL1Min;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.TrackBar tbL1;
        private Utils.WinFormControls.CustomButton btnL1Update;
        private Utils.WinFormControls.CustomTextBox txtL1;
        private System.Windows.Forms.Label label92;
        private Utils.WinFormControls.CustomGroupBox groupBox20;
        private Utils.WinFormControls.CustomTextBox txtTurnRadius;
        private System.Windows.Forms.Label label1;
    }
}