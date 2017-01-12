namespace MAV3DSim.Docks
{
    partial class ControlSelection
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
            this.groupBox6 = new MAV3DSim.Utils.WinFormControls.CustomGroupBox();
            this.chkHilSimulator = new System.Windows.Forms.CheckBox();
            this.btnEndMission = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.btnStartMission = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.rbLyapController = new System.Windows.Forms.RadioButton();
            this.rbL1Controller = new System.Windows.Forms.RadioButton();
            this.rbAltitud = new System.Windows.Forms.RadioButton();
            this.rbPitchRoll = new System.Windows.Forms.RadioButton();
            this.rbManual = new System.Windows.Forms.RadioButton();
            this.btnStop = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.btnStart = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.rbLyapController3D = new System.Windows.Forms.RadioButton();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.BorderColor = System.Drawing.Color.Black;
            this.groupBox6.Controls.Add(this.rbLyapController3D);
            this.groupBox6.Controls.Add(this.chkHilSimulator);
            this.groupBox6.Controls.Add(this.btnEndMission);
            this.groupBox6.Controls.Add(this.btnStartMission);
            this.groupBox6.Controls.Add(this.rbLyapController);
            this.groupBox6.Controls.Add(this.rbL1Controller);
            this.groupBox6.Controls.Add(this.rbAltitud);
            this.groupBox6.Controls.Add(this.rbPitchRoll);
            this.groupBox6.Controls.Add(this.rbManual);
            this.groupBox6.Controls.Add(this.btnStop);
            this.groupBox6.Controls.Add(this.btnStart);
            this.groupBox6.Location = new System.Drawing.Point(12, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.RoundCorners = 2;
            this.groupBox6.Size = new System.Drawing.Size(164, 284);
            this.groupBox6.TabIndex = 15;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Control Selection";
            // 
            // chkHilSimulator
            // 
            this.chkHilSimulator.AutoSize = true;
            this.chkHilSimulator.Location = new System.Drawing.Point(12, 106);
            this.chkHilSimulator.Name = "chkHilSimulator";
            this.chkHilSimulator.Size = new System.Drawing.Size(84, 17);
            this.chkHilSimulator.TabIndex = 13;
            this.chkHilSimulator.Text = "Hil Simulator";
            this.chkHilSimulator.UseVisualStyleBackColor = true;
            // 
            // btnEndMission
            // 
            this.btnEndMission.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnEndMission.Location = new System.Drawing.Point(12, 77);
            this.btnEndMission.Name = "btnEndMission";
            this.btnEndMission.Size = new System.Drawing.Size(142, 23);
            this.btnEndMission.TabIndex = 12;
            this.btnEndMission.Text = "End Mission";
            this.btnEndMission.UseVisualStyleBackColor = true;
            this.btnEndMission.VerticalText = false;
            this.btnEndMission.Click += new System.EventHandler(this.btnEndMission_Click);
            // 
            // btnStartMission
            // 
            this.btnStartMission.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStartMission.Location = new System.Drawing.Point(13, 48);
            this.btnStartMission.Name = "btnStartMission";
            this.btnStartMission.Size = new System.Drawing.Size(142, 23);
            this.btnStartMission.TabIndex = 11;
            this.btnStartMission.Text = "Start Mission";
            this.btnStartMission.UseVisualStyleBackColor = true;
            this.btnStartMission.VerticalText = false;
            this.btnStartMission.Click += new System.EventHandler(this.btnStartMission_Click);
            // 
            // rbLyapController
            // 
            this.rbLyapController.AutoSize = true;
            this.rbLyapController.Location = new System.Drawing.Point(12, 222);
            this.rbLyapController.Name = "rbLyapController";
            this.rbLyapController.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rbLyapController.Size = new System.Drawing.Size(92, 17);
            this.rbLyapController.TabIndex = 10;
            this.rbLyapController.Text = "LyapController";
            this.rbLyapController.UseVisualStyleBackColor = true;
            this.rbLyapController.CheckedChanged += new System.EventHandler(this.rbLyapController_CheckedChanged);
            // 
            // rbL1Controller
            // 
            this.rbL1Controller.AutoSize = true;
            this.rbL1Controller.Location = new System.Drawing.Point(12, 199);
            this.rbL1Controller.Name = "rbL1Controller";
            this.rbL1Controller.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rbL1Controller.Size = new System.Drawing.Size(84, 17);
            this.rbL1Controller.TabIndex = 9;
            this.rbL1Controller.Text = "L1 Controller";
            this.rbL1Controller.UseVisualStyleBackColor = true;
            this.rbL1Controller.CheckedChanged += new System.EventHandler(this.rbL1Controller_CheckedChanged);
            // 
            // rbAltitud
            // 
            this.rbAltitud.AutoSize = true;
            this.rbAltitud.Location = new System.Drawing.Point(11, 176);
            this.rbAltitud.Name = "rbAltitud";
            this.rbAltitud.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rbAltitud.Size = new System.Drawing.Size(113, 17);
            this.rbAltitud.TabIndex = 8;
            this.rbAltitud.Text = "Altitud Stabilization";
            this.rbAltitud.UseVisualStyleBackColor = true;
            this.rbAltitud.CheckedChanged += new System.EventHandler(this.rbAltitud_CheckedChanged);
            // 
            // rbPitchRoll
            // 
            this.rbPitchRoll.AutoSize = true;
            this.rbPitchRoll.Location = new System.Drawing.Point(12, 152);
            this.rbPitchRoll.Name = "rbPitchRoll";
            this.rbPitchRoll.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rbPitchRoll.Size = new System.Drawing.Size(138, 17);
            this.rbPitchRoll.TabIndex = 7;
            this.rbPitchRoll.Text = "Pitch && Roll Stabilization";
            this.rbPitchRoll.UseVisualStyleBackColor = true;
            this.rbPitchRoll.CheckedChanged += new System.EventHandler(this.rbPitchRoll_CheckedChanged);
            // 
            // rbManual
            // 
            this.rbManual.AutoSize = true;
            this.rbManual.Checked = true;
            this.rbManual.Location = new System.Drawing.Point(12, 129);
            this.rbManual.Name = "rbManual";
            this.rbManual.Size = new System.Drawing.Size(60, 17);
            this.rbManual.TabIndex = 6;
            this.rbManual.TabStop = true;
            this.rbManual.Text = "Manual";
            this.rbManual.UseVisualStyleBackColor = true;
            this.rbManual.CheckedChanged += new System.EventHandler(this.rbManual_CheckedChanged);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(90, 19);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(65, 23);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.VerticalText = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStart.Location = new System.Drawing.Point(13, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(65, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.VerticalText = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // rbLyapController3D
            // 
            this.rbLyapController3D.AutoSize = true;
            this.rbLyapController3D.Location = new System.Drawing.Point(12, 245);
            this.rbLyapController3D.Name = "rbLyapController3D";
            this.rbLyapController3D.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rbLyapController3D.Size = new System.Drawing.Size(106, 17);
            this.rbLyapController3D.TabIndex = 14;
            this.rbLyapController3D.Text = "LyapController3D";
            this.rbLyapController3D.UseVisualStyleBackColor = true;
            this.rbLyapController3D.CheckedChanged += new System.EventHandler(this.rbLyapController3D_CheckedChanged);
            // 
            // ControlSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(186, 308);
            this.Controls.Add(this.groupBox6);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ControlSelection";
            this.Text = "ControlSelection";
            this.Shown += new System.EventHandler(this.ControlSelection_Shown);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbLyapController;
        private System.Windows.Forms.RadioButton rbL1Controller;
        private System.Windows.Forms.RadioButton rbAltitud;
        private System.Windows.Forms.RadioButton rbPitchRoll;
        private System.Windows.Forms.RadioButton rbManual;
        private Utils.WinFormControls.CustomButton btnStop;
        private Utils.WinFormControls.CustomButton btnStart;
        private Utils.WinFormControls.CustomGroupBox groupBox6;
        private Utils.WinFormControls.CustomButton btnStartMission;
        private Utils.WinFormControls.CustomButton btnEndMission;
        private System.Windows.Forms.CheckBox chkHilSimulator;
        private System.Windows.Forms.RadioButton rbLyapController3D;
    }
}