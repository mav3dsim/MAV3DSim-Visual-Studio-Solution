namespace MAV3DSim.Docks
{
    partial class MapInfo
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtAlt = new Utils.WinFormControls.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLon = new Utils.WinFormControls.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLat = new Utils.WinFormControls.CustomTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFix = new Utils.WinFormControls.CustomTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNumSV = new Utils.WinFormControls.CustomTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPosAcc = new Utils.WinFormControls.CustomTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtVelAcc = new Utils.WinFormControls.CustomTextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Alt";
            // 
            // txtAlt
            // 
            this.txtAlt.Location = new System.Drawing.Point(50, 64);
            this.txtAlt.Name = "txtAlt";
            this.txtAlt.Size = new System.Drawing.Size(100, 20);
            this.txtAlt.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Lon";
            // 
            // txtLon
            // 
            this.txtLon.Location = new System.Drawing.Point(50, 38);
            this.txtLon.Name = "txtLon";
            this.txtLon.Size = new System.Drawing.Size(100, 20);
            this.txtLon.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Lat";
            // 
            // txtLat
            // 
            this.txtLat.Location = new System.Drawing.Point(50, 12);
            this.txtLat.Name = "txtLat";
            this.txtLat.Size = new System.Drawing.Size(100, 20);
            this.txtLat.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(156, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Fix";
            // 
            // txtFix
            // 
            this.txtFix.Location = new System.Drawing.Point(206, 12);
            this.txtFix.Name = "txtFix";
            this.txtFix.Size = new System.Drawing.Size(100, 20);
            this.txtFix.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "# SV";
            // 
            // txtNumSV
            // 
            this.txtNumSV.Location = new System.Drawing.Point(50, 90);
            this.txtNumSV.Name = "txtNumSV";
            this.txtNumSV.Size = new System.Drawing.Size(100, 20);
            this.txtNumSV.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(156, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Pos Acc.";
            // 
            // txtPosAcc
            // 
            this.txtPosAcc.Location = new System.Drawing.Point(206, 38);
            this.txtPosAcc.Name = "txtPosAcc";
            this.txtPosAcc.Size = new System.Drawing.Size(100, 20);
            this.txtPosAcc.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(156, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Vel Acc.";
            // 
            // txtVelAcc
            // 
            this.txtVelAcc.Location = new System.Drawing.Point(206, 64);
            this.txtVelAcc.Name = "txtVelAcc";
            this.txtVelAcc.Size = new System.Drawing.Size(100, 20);
            this.txtVelAcc.TabIndex = 19;
            // 
            // MapInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 122);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtVelAcc);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPosAcc);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNumSV);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFix);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAlt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLon);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLat);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "MapInfo";
            this.Text = "MapInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private Utils.WinFormControls.CustomTextBox txtAlt;
        private System.Windows.Forms.Label label2;
        private Utils.WinFormControls.CustomTextBox txtLon;
        private System.Windows.Forms.Label label1;
        private Utils.WinFormControls.CustomTextBox txtLat;
        private System.Windows.Forms.Label label4;
        private Utils.WinFormControls.CustomTextBox txtFix;
        private System.Windows.Forms.Label label5;
        private Utils.WinFormControls.CustomTextBox txtNumSV;
        private System.Windows.Forms.Label label6;
        private Utils.WinFormControls.CustomTextBox txtPosAcc;
        private System.Windows.Forms.Label label7;
        private Utils.WinFormControls.CustomTextBox txtVelAcc;
    }
}