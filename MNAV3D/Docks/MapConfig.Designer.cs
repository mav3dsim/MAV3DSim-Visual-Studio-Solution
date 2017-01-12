namespace MAV3DSim.Docks
{
    partial class MapConfig
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
            this.groupBox4 = new MAV3DSim.Utils.WinFormControls.CustomGroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInitAlt = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.cbGPSDifferential = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtInitLat = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtInitLon = new MAV3DSim.Utils.WinFormControls.CustomTextBox();
            this.btnLoad = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.btnSave = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.btnNewPointsSet = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Checked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnRefresh = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.btnSet = new MAV3DSim.Utils.WinFormControls.CustomButton();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.BorderColor = System.Drawing.Color.Black;
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.txtInitAlt);
            this.groupBox4.Controls.Add(this.cbGPSDifferential);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.txtInitLat);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.txtInitLon);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.RoundCorners = 0;
            this.groupBox4.Size = new System.Drawing.Size(182, 124);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Initial Lat/Lon Value";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Alt";
            // 
            // txtInitAlt
            // 
            this.txtInitAlt.BackColor = System.Drawing.Color.White;
            this.txtInitAlt.BorderColor = System.Drawing.Color.Empty;
            this.txtInitAlt.BorderColorHover = System.Drawing.Color.Empty;
            this.txtInitAlt.ForeColorHover = System.Drawing.Color.Empty;
            this.txtInitAlt.Location = new System.Drawing.Point(45, 74);
            this.txtInitAlt.Multiline = false;
            this.txtInitAlt.Name = "txtInitAlt";
            this.txtInitAlt.Size = new System.Drawing.Size(113, 20);
            this.txtInitAlt.TabIndex = 15;
            this.txtInitAlt.TextChanged += this.txtInitAlt_TextChanged;
            // 
            // cbGPSDifferential
            // 
            this.cbGPSDifferential.AutoSize = true;
            this.cbGPSDifferential.Location = new System.Drawing.Point(16, 101);
            this.cbGPSDifferential.Name = "cbGPSDifferential";
            this.cbGPSDifferential.Size = new System.Drawing.Size(134, 17);
            this.cbGPSDifferential.TabIndex = 13;
            this.cbGPSDifferential.Text = "Use diferential Lat/Lon";
            this.cbGPSDifferential.UseVisualStyleBackColor = true;
            this.cbGPSDifferential.CheckedChanged += new System.EventHandler(this.cbGPSDifferential_CheckedChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(13, 25);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(22, 13);
            this.label22.TabIndex = 2;
            this.label22.Text = "Lat";
            // 
            // txtInitLat
            // 
            this.txtInitLat.BackColor = System.Drawing.Color.White;
            this.txtInitLat.BorderColor = System.Drawing.Color.Empty;
            this.txtInitLat.BorderColorHover = System.Drawing.Color.Empty;
            this.txtInitLat.ForeColorHover = System.Drawing.Color.Empty;
            this.txtInitLat.Location = new System.Drawing.Point(45, 22);
            this.txtInitLat.Multiline = false;
            this.txtInitLat.Name = "txtInitLat";
            this.txtInitLat.Size = new System.Drawing.Size(113, 20);
            this.txtInitLat.TabIndex = 3;
            this.txtInitLat.TextChanged += txtInitLat_TextChanged;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(13, 51);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(25, 13);
            this.label24.TabIndex = 4;
            this.label24.Text = "Lon";
            // 
            // txtInitLon
            // 
            this.txtInitLon.BackColor = System.Drawing.Color.White;
            this.txtInitLon.BorderColor = System.Drawing.Color.Empty;
            this.txtInitLon.BorderColorHover = System.Drawing.Color.Empty;
            this.txtInitLon.ForeColorHover = System.Drawing.Color.Empty;
            this.txtInitLon.Location = new System.Drawing.Point(45, 48);
            this.txtInitLon.Multiline = false;
            this.txtInitLon.Name = "txtInitLon";
            this.txtInitLon.Size = new System.Drawing.Size(113, 20);
            this.txtInitLon.TabIndex = 5;
            this.txtInitLon.TextChanged += txtInitLon_TextChanged;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(208, 86);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(70, 32);
            this.btnLoad.TabIndex = 21;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.VerticalText = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(208, 49);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 32);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.VerticalText = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNewPointsSet
            // 
            this.btnNewPointsSet.Location = new System.Drawing.Point(208, 12);
            this.btnNewPointsSet.Name = "btnNewPointsSet";
            this.btnNewPointsSet.Size = new System.Drawing.Size(70, 32);
            this.btnNewPointsSet.TabIndex = 19;
            this.btnNewPointsSet.Text = "New Points";
            this.btnNewPointsSet.UseVisualStyleBackColor = true;
            this.btnNewPointsSet.VerticalText = false;
            this.btnNewPointsSet.Click += new System.EventHandler(this.btnNewPointsSet_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Lat,
            this.Lon,
            this.Checked});
            this.dataGridView1.Location = new System.Drawing.Point(360, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(375, 106);
            this.dataGridView1.TabIndex = 18;
            // 
            // Id
            // 
            this.Id.HeaderText = "ID";
            this.Id.Name = "Id";
            // 
            // Lat
            // 
            this.Lat.HeaderText = "LAT";
            this.Lat.Name = "Lat";
            // 
            // Lon
            // 
            this.Lon.HeaderText = "LON";
            this.Lon.Name = "Lon";
            // 
            // Checked
            // 
            this.Checked.HeaderText = "CHECKED";
            this.Checked.Name = "Checked";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(284, 85);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(70, 32);
            this.btnRefresh.TabIndex = 23;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.VerticalText = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(284, 48);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(70, 32);
            this.btnSet.TabIndex = 22;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.VerticalText = false;
            // 
            // MapConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 202);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNewPointsSet);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox4);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "MapConfig";
            this.Text = "MapConfig";
            this.Shown += new System.EventHandler(this.MapConfig_Shown);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

       

        #endregion

        private System.Windows.Forms.CheckBox cbGPSDifferential;
        private System.Windows.Forms.Label label22;
        private Utils.WinFormControls.CustomTextBox txtInitLat;
        private System.Windows.Forms.Label label24;
        private Utils.WinFormControls.CustomTextBox txtInitLon;
        private Utils.WinFormControls.CustomButton btnLoad;
        private Utils.WinFormControls.CustomButton btnSave;
        private Utils.WinFormControls.CustomButton btnNewPointsSet;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lon;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Checked;
        private Utils.WinFormControls.CustomGroupBox groupBox4;
        private Utils.WinFormControls.CustomButton btnRefresh;
        private Utils.WinFormControls.CustomButton btnSet;
        private System.Windows.Forms.Label label1;
        private Utils.WinFormControls.CustomTextBox txtInitAlt;
    }
}