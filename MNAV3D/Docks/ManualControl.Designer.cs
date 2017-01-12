namespace MAV3DSim.Docks
{
    partial class ManualControl
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
            this.groupBox5 = new Utils.WinFormControls.CustomGroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtThrottle1 = new Utils.WinFormControls.CustomTextBox();
            this.txtRudder1 = new Utils.WinFormControls.CustomTextBox();
            this.txtElevator1 = new Utils.WinFormControls.CustomTextBox();
            this.vsRudder = new System.Windows.Forms.HScrollBar();
            this.txtRudder = new Utils.WinFormControls.CustomTextBox();
            this.vsElevator = new System.Windows.Forms.VScrollBar();
            this.vsAileron = new System.Windows.Forms.HScrollBar();
            this.txtAileron = new Utils.WinFormControls.CustomTextBox();
            this.txtThrottle = new Utils.WinFormControls.CustomTextBox();
            this.txtElevator = new Utils.WinFormControls.CustomTextBox();
            this.vsThrottle = new System.Windows.Forms.VScrollBar();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.txtThrottle1);
            this.groupBox5.Controls.Add(this.txtRudder1);
            this.groupBox5.Controls.Add(this.txtElevator1);
            this.groupBox5.Controls.Add(this.vsRudder);
            this.groupBox5.Controls.Add(this.txtRudder);
            this.groupBox5.Controls.Add(this.vsElevator);
            this.groupBox5.Controls.Add(this.vsAileron);
            this.groupBox5.Controls.Add(this.txtAileron);
            this.groupBox5.Controls.Add(this.txtThrottle);
            this.groupBox5.Controls.Add(this.txtElevator);
            this.groupBox5.Controls.Add(this.vsThrottle);
            this.groupBox5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox5.Location = new System.Drawing.Point(12, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(165, 268);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Manual control";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Ail";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Rud";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(9, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Ele";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Thr";
            // 
            // txtThrottle1
            // 
            this.txtThrottle1.Location = new System.Drawing.Point(81, 131);
            this.txtThrottle1.Name = "txtThrottle1";
            this.txtThrottle1.Size = new System.Drawing.Size(47, 20);
            this.txtThrottle1.TabIndex = 32;
            this.txtThrottle1.Text = "0";
            this.txtThrottle1.Visible = false;
            // 
            // txtRudder1
            // 
            this.txtRudder1.Location = new System.Drawing.Point(55, 182);
            this.txtRudder1.Name = "txtRudder1";
            this.txtRudder1.Size = new System.Drawing.Size(47, 20);
            this.txtRudder1.TabIndex = 31;
            this.txtRudder1.Text = "0";
            this.txtRudder1.Visible = false;
            // 
            // txtElevator1
            // 
            this.txtElevator1.Location = new System.Drawing.Point(28, 131);
            this.txtElevator1.Name = "txtElevator1";
            this.txtElevator1.Size = new System.Drawing.Size(47, 20);
            this.txtElevator1.TabIndex = 15;
            this.txtElevator1.Text = "0";
            this.txtElevator1.Visible = false;
            // 
            // vsRudder
            // 
            this.vsRudder.Location = new System.Drawing.Point(12, 159);
            this.vsRudder.Maximum = 59;
            this.vsRudder.Minimum = -50;
            this.vsRudder.Name = "vsRudder";
            this.vsRudder.Size = new System.Drawing.Size(140, 20);
            this.vsRudder.TabIndex = 14;
            this.vsRudder.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsRudder_Scroll);
            this.vsRudder.ValueChanged += new System.EventHandler(this.vsRudder_ValueChanged);
            // 
            // txtRudder
            // 
            this.txtRudder.Location = new System.Drawing.Point(55, 182);
            this.txtRudder.Name = "txtRudder";
            this.txtRudder.Size = new System.Drawing.Size(47, 20);
            this.txtRudder.TabIndex = 13;
            this.txtRudder.Text = "0";
            this.txtRudder.TextChanged += this.txtRudder_TextChanged;
            // 
            // vsElevator
            // 
            this.vsElevator.Location = new System.Drawing.Point(39, 15);
            this.vsElevator.Maximum = 59;
            this.vsElevator.Minimum = -50;
            this.vsElevator.Name = "vsElevator";
            this.vsElevator.Size = new System.Drawing.Size(21, 109);
            this.vsElevator.TabIndex = 7;
            this.vsElevator.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsElevator_Scroll);
            this.vsElevator.ValueChanged += new System.EventHandler(this.vsElevator_ValueChanged);
            // 
            // vsAileron
            // 
            this.vsAileron.Location = new System.Drawing.Point(13, 209);
            this.vsAileron.Maximum = 59;
            this.vsAileron.Minimum = -50;
            this.vsAileron.Name = "vsAileron";
            this.vsAileron.Size = new System.Drawing.Size(140, 20);
            this.vsAileron.TabIndex = 12;
            this.vsAileron.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsAileron_Scroll);
            this.vsAileron.ValueChanged += new System.EventHandler(this.vsAileron_ValueChanged);
            // 
            // txtAileron
            // 
            this.txtAileron.Location = new System.Drawing.Point(56, 232);
            this.txtAileron.Name = "txtAileron";
            this.txtAileron.Size = new System.Drawing.Size(47, 20);
            this.txtAileron.TabIndex = 6;
            this.txtAileron.Text = "0";
            this.txtAileron.TextChanged += this.txtAileron_TextChanged;
            // 
            // txtThrottle
            // 
            this.txtThrottle.Location = new System.Drawing.Point(81, 131);
            this.txtThrottle.Name = "txtThrottle";
            this.txtThrottle.Size = new System.Drawing.Size(47, 20);
            this.txtThrottle.TabIndex = 10;
            this.txtThrottle.Text = "0";
            this.txtThrottle.TextChanged += this.txtThrottle_TextChanged;
            // 
            // txtElevator
            // 
            this.txtElevator.Location = new System.Drawing.Point(28, 131);
            this.txtElevator.Name = "txtElevator";
            this.txtElevator.Size = new System.Drawing.Size(47, 20);
            this.txtElevator.TabIndex = 8;
            this.txtElevator.Text = "0";
            this.txtElevator.TextChanged += this.txtElevator_TextChanged;
            // 
            // vsThrottle
            // 
            this.vsThrottle.Location = new System.Drawing.Point(99, 15);
            this.vsThrottle.Maximum = 0;
            this.vsThrottle.Minimum = -100;
            this.vsThrottle.Name = "vsThrottle";
            this.vsThrottle.Size = new System.Drawing.Size(21, 109);
            this.vsThrottle.TabIndex = 9;
            this.vsThrottle.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsThrottle_Scroll);
            this.vsThrottle.ValueChanged += new System.EventHandler(this.vsThrottle_ValueChanged);
            // 
            // ManualControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 285);
            this.Controls.Add(this.groupBox5);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "ManualControl";
            this.Text = "ManualControl";
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Utils.WinFormControls.CustomGroupBox groupBox5;
        private Utils.WinFormControls.CustomTextBox txtThrottle1;
        private Utils.WinFormControls.CustomTextBox txtRudder1;
        private Utils.WinFormControls.CustomTextBox txtElevator1;
        private System.Windows.Forms.HScrollBar vsRudder;
        private Utils.WinFormControls.CustomTextBox txtRudder;
        private System.Windows.Forms.VScrollBar vsElevator;
        private System.Windows.Forms.HScrollBar vsAileron;
        private Utils.WinFormControls.CustomTextBox txtAileron;
        private Utils.WinFormControls.CustomTextBox txtThrottle;
        private Utils.WinFormControls.CustomTextBox txtElevator;
        private System.Windows.Forms.VScrollBar vsThrottle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}