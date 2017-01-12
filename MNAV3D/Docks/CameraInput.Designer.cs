namespace MAV3DSim.Docks
{
    partial class CameraInput
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
            this.components = new System.ComponentModel.Container();
            this.customGroupBox1 = new MAV3DSim.Utils.WinFormControls.CustomGroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btStartCapture = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.cbCameraList = new System.Windows.Forms.ComboBox();
            this.pbImage = new Emgu.CV.UI.ImageBox();
            this.customGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // customGroupBox1
            // 
            this.customGroupBox1.BorderColor = System.Drawing.Color.Black;
            this.customGroupBox1.Controls.Add(this.label1);
            this.customGroupBox1.Controls.Add(this.btStartCapture);
            this.customGroupBox1.Controls.Add(this.cbCameraList);
            this.customGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.customGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.customGroupBox1.Name = "customGroupBox1";
            this.customGroupBox1.RoundCorners = 0;
            this.customGroupBox1.Size = new System.Drawing.Size(866, 52);
            this.customGroupBox1.TabIndex = 4;
            this.customGroupBox1.TabStop = false;
            this.customGroupBox1.Text = "customGroupBox1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select camera";
            // 
            // btStartCapture
            // 
            this.btStartCapture.Location = new System.Drawing.Point(303, 18);
            this.btStartCapture.Name = "btStartCapture";
            this.btStartCapture.Size = new System.Drawing.Size(115, 23);
            this.btStartCapture.TabIndex = 1;
            this.btStartCapture.Text = "Start Capture";
            this.btStartCapture.UseVisualStyleBackColor = true;
            this.btStartCapture.VerticalText = false;
            this.btStartCapture.Click += new System.EventHandler(this.btStartCapture_Click);
            // 
            // cbCameraList
            // 
            this.cbCameraList.FormattingEnabled = true;
            this.cbCameraList.Location = new System.Drawing.Point(96, 20);
            this.cbCameraList.Name = "cbCameraList";
            this.cbCameraList.Size = new System.Drawing.Size(201, 21);
            this.cbCameraList.TabIndex = 2;
            // 
            // pbImage
            // 
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Location = new System.Drawing.Point(0, 52);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(866, 229);
            this.pbImage.TabIndex = 2;
            this.pbImage.TabStop = false;
            // 
            // CameraInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 281);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.customGroupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CameraInput";
            this.Text = "CameraInput";
            this.customGroupBox1.ResumeLayout(false);
            this.customGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Utils.WinFormControls.CustomButton btStartCapture;
        private System.Windows.Forms.ComboBox cbCameraList;
        private System.Windows.Forms.Label label1;
        private Utils.WinFormControls.CustomGroupBox customGroupBox1;
        private Emgu.CV.UI.ImageBox pbImage;
    }
}