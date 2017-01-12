namespace MAV3DSim.Docks
{
    partial class UDPConfig
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
            this.groupBox2 = new MAV3DSim.Utils.WinFormControls.CustomGroupBox();
            this.cbProtocol = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUDPPort = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtUDPIPAddress = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.btnUDPConnect = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BorderColor = System.Drawing.Color.Black;
            this.groupBox2.Controls.Add(this.cbProtocol);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtUDPPort);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.txtUDPIPAddress);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RoundCorners = 0;
            this.groupBox2.Size = new System.Drawing.Size(184, 117);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "UDP Configuration";
            // 
            // cbProtocol
            // 
            this.cbProtocol.FormattingEnabled = true;
            this.cbProtocol.Location = new System.Drawing.Point(78, 79);
            this.cbProtocol.Name = "cbProtocol";
            this.cbProtocol.Size = new System.Drawing.Size(100, 21);
            this.cbProtocol.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Protocol";
            // 
            // txtUDPPort
            // 
            this.txtUDPPort.BackColor = System.Drawing.Color.White;
            this.txtUDPPort.BorderColor = System.Drawing.Color.Empty;
            this.txtUDPPort.BorderColorHover = System.Drawing.Color.Empty;
            this.txtUDPPort.ForeColorHover = System.Drawing.Color.Empty;
            this.txtUDPPort.Location = new System.Drawing.Point(78, 53);
            this.txtUDPPort.Multiline = false;
            this.txtUDPPort.Name = "txtUDPPort";
            this.txtUDPPort.Size = new System.Drawing.Size(100, 20);
            this.txtUDPPort.TabIndex = 3;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 56);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(26, 13);
            this.label21.TabIndex = 2;
            this.label21.Text = "Port";
            // 
            // txtUDPIPAddress
            // 
            this.txtUDPIPAddress.BackColor = System.Drawing.Color.White;
            this.txtUDPIPAddress.BorderColor = System.Drawing.Color.Empty;
            this.txtUDPIPAddress.BorderColorHover = System.Drawing.Color.Empty;
            this.txtUDPIPAddress.ForeColorHover = System.Drawing.Color.Empty;
            this.txtUDPIPAddress.Location = new System.Drawing.Point(78, 23);
            this.txtUDPIPAddress.Multiline = false;
            this.txtUDPIPAddress.Name = "txtUDPIPAddress";
            this.txtUDPIPAddress.Size = new System.Drawing.Size(100, 20);
            this.txtUDPIPAddress.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 26);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(58, 13);
            this.label20.TabIndex = 1;
            this.label20.Text = "IP Address";
            // 
            // btnUDPConnect
            // 
            this.btnUDPConnect.Location = new System.Drawing.Point(115, 135);
            this.btnUDPConnect.Name = "btnUDPConnect";
            this.btnUDPConnect.Size = new System.Drawing.Size(75, 23);
            this.btnUDPConnect.TabIndex = 1;
            this.btnUDPConnect.Text = "Connect";
            this.btnUDPConnect.UseVisualStyleBackColor = true;
            this.btnUDPConnect.VerticalText = false;
            this.btnUDPConnect.Click += new System.EventHandler(this.btnUDPConnect_Click);
            // 
            // UDPConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(211, 172);
            this.Controls.Add(this.btnUDPConnect);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "UDPConfig";
            this.Text = "UDPConfig";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Utils.WinFormControls.CustomButton btnUDPConnect;
        private Utils.WinFormControls.CustomTextBox txtUDPPort;
        private System.Windows.Forms.Label label21;
        private Utils.WinFormControls.CustomTextBox txtUDPIPAddress;
        private System.Windows.Forms.Label label20;
        private Utils.WinFormControls.CustomGroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbProtocol;
        private System.Windows.Forms.Label label1;
    }
}