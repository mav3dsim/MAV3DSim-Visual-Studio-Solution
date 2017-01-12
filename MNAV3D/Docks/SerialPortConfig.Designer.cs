namespace MAV3DSim.Docks
{
    partial class SerialPortConfig
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
            this.cbProtocol = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.label66 = new System.Windows.Forms.Label();
            this.cbFlowControl = new System.Windows.Forms.ComboBox();
            this.label71 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.label70 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.label69 = new System.Windows.Forms.Label();
            this.btnClosePort = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.btnOpenSerialPort = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.SuspendLayout();
            // 
            // cbProtocol
            // 
            this.cbProtocol.FormattingEnabled = true;
            this.cbProtocol.Items.AddRange(new object[] {
            ""});
            this.cbProtocol.Location = new System.Drawing.Point(86, 191);
            this.cbProtocol.Name = "cbProtocol";
            this.cbProtocol.Size = new System.Drawing.Size(100, 21);
            this.cbProtocol.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Protocol";
            // 
            // cbPort
            // 
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Location = new System.Drawing.Point(86, 25);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(101, 21);
            this.cbPort.TabIndex = 20;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(15, 28);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(26, 13);
            this.label66.TabIndex = 19;
            this.label66.Text = "Port";
            // 
            // cbFlowControl
            // 
            this.cbFlowControl.FormattingEnabled = true;
            this.cbFlowControl.Location = new System.Drawing.Point(87, 164);
            this.cbFlowControl.Name = "cbFlowControl";
            this.cbFlowControl.Size = new System.Drawing.Size(101, 21);
            this.cbFlowControl.TabIndex = 30;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(15, 167);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(65, 13);
            this.label71.TabIndex = 29;
            this.label71.Text = "Flow Control";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(14, 59);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(50, 13);
            this.label67.TabIndex = 21;
            this.label67.Text = "Baudrate";
            // 
            // cbStopBits
            // 
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Location = new System.Drawing.Point(87, 137);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(101, 21);
            this.cbStopBits.TabIndex = 28;
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Location = new System.Drawing.Point(86, 56);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(101, 21);
            this.cbBaudRate.TabIndex = 22;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(15, 140);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(49, 13);
            this.label70.TabIndex = 27;
            this.label70.Text = "Stop Bits";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(15, 87);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(50, 13);
            this.label68.TabIndex = 23;
            this.label68.Text = "Data Bits";
            // 
            // cbParity
            // 
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Location = new System.Drawing.Point(86, 110);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(101, 21);
            this.cbParity.TabIndex = 26;
            // 
            // cbDataBits
            // 
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Location = new System.Drawing.Point(87, 84);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(101, 21);
            this.cbDataBits.TabIndex = 24;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(14, 113);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(33, 13);
            this.label69.TabIndex = 25;
            this.label69.Text = "Parity";
            // 
            // btnClosePort
            // 
            this.btnClosePort.Location = new System.Drawing.Point(113, 224);
            this.btnClosePort.Name = "btnClosePort";
            this.btnClosePort.Size = new System.Drawing.Size(75, 23);
            this.btnClosePort.TabIndex = 31;
            this.btnClosePort.Text = "Close Port";
            this.btnClosePort.UseVisualStyleBackColor = true;
            this.btnClosePort.VerticalText = false;
            this.btnClosePort.Click += new System.EventHandler(this.btnClosePort_Click);
            // 
            // btnOpenSerialPort
            // 
            this.btnOpenSerialPort.Location = new System.Drawing.Point(18, 224);
            this.btnOpenSerialPort.Name = "btnOpenSerialPort";
            this.btnOpenSerialPort.Size = new System.Drawing.Size(75, 23);
            this.btnOpenSerialPort.TabIndex = 18;
            this.btnOpenSerialPort.Text = "Open Port";
            this.btnOpenSerialPort.UseVisualStyleBackColor = true;
            this.btnOpenSerialPort.VerticalText = false;
            this.btnOpenSerialPort.Click += new System.EventHandler(this.btnOpenSerialPort_Click);
            // 
            // SerialPortConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 273);
            this.Controls.Add(this.cbProtocol);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbPort);
            this.Controls.Add(this.btnClosePort);
            this.Controls.Add(this.label66);
            this.Controls.Add(this.cbFlowControl);
            this.Controls.Add(this.btnOpenSerialPort);
            this.Controls.Add(this.label71);
            this.Controls.Add(this.label67);
            this.Controls.Add(this.cbStopBits);
            this.Controls.Add(this.cbBaudRate);
            this.Controls.Add(this.label70);
            this.Controls.Add(this.label68);
            this.Controls.Add(this.cbParity);
            this.Controls.Add(this.cbDataBits);
            this.Controls.Add(this.label69);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "SerialPortConfig";
            this.Text = "SerialPortConfig";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbProtocol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPort;
        private Utils.WinFormControls.CustomButton btnClosePort;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.ComboBox cbFlowControl;
        private Utils.WinFormControls.CustomButton btnOpenSerialPort;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.ComboBox cbStopBits;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.ComboBox cbDataBits;
        private System.Windows.Forms.Label label69;

    }
}