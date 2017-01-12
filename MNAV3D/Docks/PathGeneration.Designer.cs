namespace MAV3DSim.Docks
{
    partial class PathGeneration
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
            this.btnRight = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.btnLeft = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.btnUp = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.txtUp = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.txtRight = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.txtLeft = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.btnNew = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTurnRadius = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.btnSaveData = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.btnPolygon = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.txtPitchAngle = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAltitudeOffset = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRight
            // 
            this.btnRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRight.Location = new System.Drawing.Point(92, 74);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(40, 40);
            this.btnRight.TabIndex = 2;
            this.btnRight.Text = ">";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.VerticalText = false;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLeft.Location = new System.Drawing.Point(13, 74);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(40, 40);
            this.btnLeft.TabIndex = 1;
            this.btnLeft.Text = "<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.VerticalText = false;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnUp
            // 
            this.btnUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUp.Location = new System.Drawing.Point(52, 28);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(40, 40);
            this.btnUp.TabIndex = 0;
            this.btnUp.Text = "<";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.VerticalText = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // txtUp
            // 
            this.txtUp.BackColor = System.Drawing.Color.White;
            this.txtUp.BorderColor = System.Drawing.Color.Black;
            this.txtUp.BorderColorHover = System.Drawing.Color.Empty;
            this.txtUp.ForeColorHover = System.Drawing.Color.Empty;
            this.txtUp.Location = new System.Drawing.Point(52, 4);
            this.txtUp.Multiline = false;
            this.txtUp.Name = "txtUp";
            this.txtUp.Size = new System.Drawing.Size(40, 18);
            this.txtUp.TabIndex = 3;
            // 
            // txtRight
            // 
            this.txtRight.BackColor = System.Drawing.Color.White;
            this.txtRight.BorderColor = System.Drawing.Color.Black;
            this.txtRight.BorderColorHover = System.Drawing.Color.Empty;
            this.txtRight.ForeColorHover = System.Drawing.Color.Empty;
            this.txtRight.Location = new System.Drawing.Point(92, 120);
            this.txtRight.Multiline = false;
            this.txtRight.Name = "txtRight";
            this.txtRight.Size = new System.Drawing.Size(40, 18);
            this.txtRight.TabIndex = 4;
            // 
            // txtLeft
            // 
            this.txtLeft.BackColor = System.Drawing.Color.White;
            this.txtLeft.BorderColor = System.Drawing.Color.Black;
            this.txtLeft.BorderColorHover = System.Drawing.Color.Empty;
            this.txtLeft.ForeColorHover = System.Drawing.Color.Empty;
            this.txtLeft.Location = new System.Drawing.Point(13, 120);
            this.txtLeft.Multiline = false;
            this.txtLeft.Name = "txtLeft";
            this.txtLeft.Size = new System.Drawing.Size(40, 18);
            this.txtLeft.TabIndex = 5;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(15, 259);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(119, 31);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.VerticalText = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Turn radius";
            // 
            // txtTurnRadius
            // 
            this.txtTurnRadius.BackColor = System.Drawing.Color.White;
            this.txtTurnRadius.BorderColor = System.Drawing.Color.Black;
            this.txtTurnRadius.BorderColorHover = System.Drawing.Color.Empty;
            this.txtTurnRadius.ForeColorHover = System.Drawing.Color.Empty;
            this.txtTurnRadius.Location = new System.Drawing.Point(88, 164);
            this.txtTurnRadius.Multiline = false;
            this.txtTurnRadius.Name = "txtTurnRadius";
            this.txtTurnRadius.Size = new System.Drawing.Size(40, 18);
            this.txtTurnRadius.TabIndex = 9;
            // 
            // btnSaveData
            // 
            this.btnSaveData.Location = new System.Drawing.Point(13, 335);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(119, 31);
            this.btnSaveData.TabIndex = 10;
            this.btnSaveData.Text = "Save data";
            this.btnSaveData.UseVisualStyleBackColor = true;
            this.btnSaveData.VerticalText = false;
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            // 
            // btnPolygon
            // 
            this.btnPolygon.Location = new System.Drawing.Point(14, 297);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(119, 31);
            this.btnPolygon.TabIndex = 11;
            this.btnPolygon.Text = "Polygon";
            this.btnPolygon.UseVisualStyleBackColor = true;
            this.btnPolygon.VerticalText = false;
            this.btnPolygon.Click += new System.EventHandler(this.btnPolygon_Click);
            // 
            // txtPitchAngle
            // 
            this.txtPitchAngle.BackColor = System.Drawing.Color.White;
            this.txtPitchAngle.BorderColor = System.Drawing.Color.Black;
            this.txtPitchAngle.BorderColorHover = System.Drawing.Color.Empty;
            this.txtPitchAngle.ForeColorHover = System.Drawing.Color.Empty;
            this.txtPitchAngle.Location = new System.Drawing.Point(88, 188);
            this.txtPitchAngle.Multiline = false;
            this.txtPitchAngle.Name = "txtPitchAngle";
            this.txtPitchAngle.Size = new System.Drawing.Size(40, 18);
            this.txtPitchAngle.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Pitch angle";
            // 
            // txtAltitudeOffset
            // 
            this.txtAltitudeOffset.BackColor = System.Drawing.Color.White;
            this.txtAltitudeOffset.BorderColor = System.Drawing.Color.Black;
            this.txtAltitudeOffset.BorderColorHover = System.Drawing.Color.Empty;
            this.txtAltitudeOffset.ForeColorHover = System.Drawing.Color.Empty;
            this.txtAltitudeOffset.Location = new System.Drawing.Point(88, 212);
            this.txtAltitudeOffset.Multiline = false;
            this.txtAltitudeOffset.Name = "txtAltitudeOffset";
            this.txtAltitudeOffset.Size = new System.Drawing.Size(40, 18);
            this.txtAltitudeOffset.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Altitude Offset";
            // 
            // PathGeneration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(144, 385);
            this.Controls.Add(this.txtAltitudeOffset);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPitchAngle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPolygon);
            this.Controls.Add(this.btnSaveData);
            this.Controls.Add(this.txtTurnRadius);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.txtLeft);
            this.Controls.Add(this.txtRight);
            this.Controls.Add(this.txtUp);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnUp);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PathGeneration";
            this.Text = "PathGenerator";
            this.Shown += new System.EventHandler(this.PathGeneration_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Utils.WinFormControls.CustomButton btnUp;
        private Utils.WinFormControls.CustomButton btnLeft;
        private Utils.WinFormControls.CustomButton btnRight;
        private Utils.WinFormControls.CustomTextBox txtUp;
        private Utils.WinFormControls.CustomTextBox txtRight;
        private Utils.WinFormControls.CustomTextBox txtLeft;
        private Utils.WinFormControls.CustomButton btnNew;
        private System.Windows.Forms.Label label2;
        private Utils.WinFormControls.CustomTextBox txtTurnRadius;
        private Utils.WinFormControls.CustomButton btnSaveData;
        private Utils.WinFormControls.CustomButton btnPolygon;
        private Utils.WinFormControls.CustomTextBox txtPitchAngle;
        private System.Windows.Forms.Label label1;
        private Utils.WinFormControls.CustomTextBox txtAltitudeOffset;
        private System.Windows.Forms.Label label3;
    }
}