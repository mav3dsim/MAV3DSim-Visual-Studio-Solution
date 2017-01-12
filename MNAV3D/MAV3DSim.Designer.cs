namespace MAV3DSim
{
    partial class Mav3DSim
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

            closeRequested = true;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mav3DSim));
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mbtSaveWorkspace = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtLoadWorkspace = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mbtSaveLogFile = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtShowMap = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtMapInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtMapConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtRawData = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtAddPlot = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtSavePlotWorkspace = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtLoadPlotWorkspace = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mbtMessages = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtWaypoints = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtGeofence = new System.Windows.Forms.ToolStripMenuItem();
            this.instrumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtAirspeed = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtAttitudInstrument = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtAltimeter = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtCompass = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtVerticalSpeed = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtBattery = new System.Windows.Forms.ToolStripMenuItem();
            this.mbt3DView = new System.Windows.Forms.ToolStripMenuItem();
            this.gainsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtRollPIDGains = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtPitchPIDGains = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtAirspeedGains = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtAltitudPIDGains = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtL1PIDGains = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtLyapunovGains = new System.Windows.Forms.ToolStripMenuItem();
            this.controlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtManualControl = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtControlSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtPathGeneration = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtMissionControl = new System.Windows.Forms.ToolStripMenuItem();
            this.inputCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtUDP = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtSerialPort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.loadSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.PlaybackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.slControlerFreq = new System.Windows.Forms.ToolStripStatusLabel();
            this.slIMUFreq = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new BSE.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCRRCSim = new System.Windows.Forms.ToolStripButton();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.mbtLyapunov3DGains = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.configurationToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(709, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtOpen,
            this.mbtExit,
            this.toolStripSeparator1,
            this.mbtSaveWorkspace,
            this.mbtLoadWorkspace,
            this.toolStripSeparator3,
            this.mbtSaveLogFile});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.fileToolStripMenuItem.Text = "FILE";
            // 
            // mbtOpen
            // 
            this.mbtOpen.Name = "mbtOpen";
            this.mbtOpen.Size = new System.Drawing.Size(159, 22);
            this.mbtOpen.Text = "Open";
            // 
            // mbtExit
            // 
            this.mbtExit.Name = "mbtExit";
            this.mbtExit.Size = new System.Drawing.Size(159, 22);
            this.mbtExit.Text = "Exit";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(156, 6);
            // 
            // mbtSaveWorkspace
            // 
            this.mbtSaveWorkspace.Name = "mbtSaveWorkspace";
            this.mbtSaveWorkspace.Size = new System.Drawing.Size(159, 22);
            this.mbtSaveWorkspace.Text = "Save workspace";
            this.mbtSaveWorkspace.Click += new System.EventHandler(this.mbtSaveWorkspace_Click);
            // 
            // mbtLoadWorkspace
            // 
            this.mbtLoadWorkspace.Name = "mbtLoadWorkspace";
            this.mbtLoadWorkspace.Size = new System.Drawing.Size(159, 22);
            this.mbtLoadWorkspace.Text = "Load workspace";
            this.mbtLoadWorkspace.Click += new System.EventHandler(this.mbtLoadWorkspace_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(156, 6);
            // 
            // mbtSaveLogFile
            // 
            this.mbtSaveLogFile.Name = "mbtSaveLogFile";
            this.mbtSaveLogFile.Size = new System.Drawing.Size(159, 22);
            this.mbtSaveLogFile.Text = "Save log file";
            this.mbtSaveLogFile.Click += new System.EventHandler(this.mbtSaveLogFile_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.instrumentsToolStripMenuItem,
            this.gainsToolStripMenuItem,
            this.controlToolStripMenuItem,
            this.inputCameraToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.viewToolStripMenuItem.Text = "VIEW";
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtShowMap,
            this.mbtMapInfo,
            this.mbtMapConfig});
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mapToolStripMenuItem.Text = "Map";
            // 
            // mbtShowMap
            // 
            this.mbtShowMap.CheckOnClick = true;
            this.mbtShowMap.Name = "mbtShowMap";
            this.mbtShowMap.Size = new System.Drawing.Size(137, 22);
            this.mbtShowMap.Text = "Show Map";
            this.mbtShowMap.Click += new System.EventHandler(this.mbtShowMap_Click);
            // 
            // mbtMapInfo
            // 
            this.mbtMapInfo.CheckOnClick = true;
            this.mbtMapInfo.Name = "mbtMapInfo";
            this.mbtMapInfo.Size = new System.Drawing.Size(137, 22);
            this.mbtMapInfo.Text = "Map Info";
            this.mbtMapInfo.Click += new System.EventHandler(this.mbtMapInfo_Click);
            // 
            // mbtMapConfig
            // 
            this.mbtMapConfig.CheckOnClick = true;
            this.mbtMapConfig.Name = "mbtMapConfig";
            this.mbtMapConfig.Size = new System.Drawing.Size(137, 22);
            this.mbtMapConfig.Text = "Map Config";
            this.mbtMapConfig.Click += new System.EventHandler(this.mbtMapConfig_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtRawData,
            this.mbtAddPlot,
            this.mbtSavePlotWorkspace,
            this.mbtLoadPlotWorkspace,
            this.toolStripSeparator2,
            this.mbtMessages,
            this.mbtWaypoints,
            this.mbtGeofence});
            this.dataToolStripMenuItem.Image = global::MAV3DSim.Properties.Resources.loading_circle;
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // mbtRawData
            // 
            this.mbtRawData.Name = "mbtRawData";
            this.mbtRawData.Size = new System.Drawing.Size(185, 22);
            this.mbtRawData.Text = "Raw Data";
            this.mbtRawData.Click += new System.EventHandler(this.mbtRawData_Click);
            // 
            // mbtAddPlot
            // 
            this.mbtAddPlot.Name = "mbtAddPlot";
            this.mbtAddPlot.Size = new System.Drawing.Size(185, 22);
            this.mbtAddPlot.Text = "Add Plot";
            this.mbtAddPlot.Click += new System.EventHandler(this.mbtAddPlot_Click);
            // 
            // mbtSavePlotWorkspace
            // 
            this.mbtSavePlotWorkspace.Name = "mbtSavePlotWorkspace";
            this.mbtSavePlotWorkspace.Size = new System.Drawing.Size(185, 22);
            this.mbtSavePlotWorkspace.Text = "Save Plot Workspace";
            this.mbtSavePlotWorkspace.Click += new System.EventHandler(this.mbtPlotPitchQAy_Click);
            // 
            // mbtLoadPlotWorkspace
            // 
            this.mbtLoadPlotWorkspace.Name = "mbtLoadPlotWorkspace";
            this.mbtLoadPlotWorkspace.Size = new System.Drawing.Size(185, 22);
            this.mbtLoadPlotWorkspace.Text = "Load Plot Workspace";
            this.mbtLoadPlotWorkspace.Click += new System.EventHandler(this.mbtPlotYawRAz_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(182, 6);
            // 
            // mbtMessages
            // 
            this.mbtMessages.Name = "mbtMessages";
            this.mbtMessages.Size = new System.Drawing.Size(185, 22);
            this.mbtMessages.Text = "Messages";
            this.mbtMessages.Click += new System.EventHandler(this.messagesToolStripMenuItem_Click);
            // 
            // mbtWaypoints
            // 
            this.mbtWaypoints.Name = "mbtWaypoints";
            this.mbtWaypoints.Size = new System.Drawing.Size(185, 22);
            this.mbtWaypoints.Text = "Waypoints";
            this.mbtWaypoints.Click += new System.EventHandler(this.mbtWaypoints_Click);
            // 
            // mbtGeofence
            // 
            this.mbtGeofence.Name = "mbtGeofence";
            this.mbtGeofence.Size = new System.Drawing.Size(185, 22);
            this.mbtGeofence.Text = "Geofence";
            this.mbtGeofence.Click += new System.EventHandler(this.mbtGeofence_Click);
            // 
            // instrumentsToolStripMenuItem
            // 
            this.instrumentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtAirspeed,
            this.mbtAttitudInstrument,
            this.mbtAltimeter,
            this.mbtCompass,
            this.mbtVerticalSpeed,
            this.mbtBattery,
            this.mbt3DView});
            this.instrumentsToolStripMenuItem.Name = "instrumentsToolStripMenuItem";
            this.instrumentsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.instrumentsToolStripMenuItem.Text = "Instruments";
            // 
            // mbtAirspeed
            // 
            this.mbtAirspeed.Name = "mbtAirspeed";
            this.mbtAirspeed.Size = new System.Drawing.Size(161, 22);
            this.mbtAirspeed.Text = "Airspeed";
            this.mbtAirspeed.Click += new System.EventHandler(this.mbtAirspeed_Click);
            // 
            // mbtAttitudInstrument
            // 
            this.mbtAttitudInstrument.Name = "mbtAttitudInstrument";
            this.mbtAttitudInstrument.Size = new System.Drawing.Size(161, 22);
            this.mbtAttitudInstrument.Text = "Attitud Indicator";
            this.mbtAttitudInstrument.Click += new System.EventHandler(this.mbtAttitudIndicator_Click);
            // 
            // mbtAltimeter
            // 
            this.mbtAltimeter.Name = "mbtAltimeter";
            this.mbtAltimeter.Size = new System.Drawing.Size(161, 22);
            this.mbtAltimeter.Text = "Altimeter";
            this.mbtAltimeter.Click += new System.EventHandler(this.mbtAltimeter_Click);
            // 
            // mbtCompass
            // 
            this.mbtCompass.Name = "mbtCompass";
            this.mbtCompass.Size = new System.Drawing.Size(161, 22);
            this.mbtCompass.Text = "Compass";
            this.mbtCompass.Click += new System.EventHandler(this.mbtCompass_Click);
            // 
            // mbtVerticalSpeed
            // 
            this.mbtVerticalSpeed.Name = "mbtVerticalSpeed";
            this.mbtVerticalSpeed.Size = new System.Drawing.Size(161, 22);
            this.mbtVerticalSpeed.Text = "Vertical Speed";
            this.mbtVerticalSpeed.Click += new System.EventHandler(this.mbtVerticalSpeed_Click);
            // 
            // mbtBattery
            // 
            this.mbtBattery.Name = "mbtBattery";
            this.mbtBattery.Size = new System.Drawing.Size(161, 22);
            this.mbtBattery.Text = "Battery";
            // 
            // mbt3DView
            // 
            this.mbt3DView.Name = "mbt3DView";
            this.mbt3DView.Size = new System.Drawing.Size(161, 22);
            this.mbt3DView.Text = "3D View";
            // 
            // gainsToolStripMenuItem
            // 
            this.gainsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtRollPIDGains,
            this.mbtPitchPIDGains,
            this.mbtAirspeedGains,
            this.mbtAltitudPIDGains,
            this.mbtL1PIDGains,
            this.mbtLyapunovGains,
            this.mbtLyapunov3DGains});
            this.gainsToolStripMenuItem.Name = "gainsToolStripMenuItem";
            this.gainsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.gainsToolStripMenuItem.Text = "Gains";
            // 
            // mbtRollPIDGains
            // 
            this.mbtRollPIDGains.Name = "mbtRollPIDGains";
            this.mbtRollPIDGains.Size = new System.Drawing.Size(173, 22);
            this.mbtRollPIDGains.Text = "Roll PID Gains";
            this.mbtRollPIDGains.Click += new System.EventHandler(this.mbtRollPIDGains_Click);
            // 
            // mbtPitchPIDGains
            // 
            this.mbtPitchPIDGains.Name = "mbtPitchPIDGains";
            this.mbtPitchPIDGains.Size = new System.Drawing.Size(173, 22);
            this.mbtPitchPIDGains.Text = "Pitch PID Gains";
            this.mbtPitchPIDGains.Click += new System.EventHandler(this.mbtPitchPIDGains_Click);
            // 
            // mbtAirspeedGains
            // 
            this.mbtAirspeedGains.Name = "mbtAirspeedGains";
            this.mbtAirspeedGains.Size = new System.Drawing.Size(173, 22);
            this.mbtAirspeedGains.Text = "Airspeed PID Gains";
            this.mbtAirspeedGains.Click += new System.EventHandler(this.mbtAirspeed_Click);
            // 
            // mbtAltitudPIDGains
            // 
            this.mbtAltitudPIDGains.Name = "mbtAltitudPIDGains";
            this.mbtAltitudPIDGains.Size = new System.Drawing.Size(173, 22);
            this.mbtAltitudPIDGains.Text = "Altitud PID Gains";
            this.mbtAltitudPIDGains.Click += new System.EventHandler(this.mbtAltitudPIDGains_Click);
            // 
            // mbtL1PIDGains
            // 
            this.mbtL1PIDGains.Name = "mbtL1PIDGains";
            this.mbtL1PIDGains.Size = new System.Drawing.Size(173, 22);
            this.mbtL1PIDGains.Text = "L1 PID Gains";
            this.mbtL1PIDGains.Click += new System.EventHandler(this.mbtL1PIDGains_Click);
            // 
            // mbtLyapunovGains
            // 
            this.mbtLyapunovGains.Name = "mbtLyapunovGains";
            this.mbtLyapunovGains.Size = new System.Drawing.Size(173, 22);
            this.mbtLyapunovGains.Text = "Lyapunov Gains";
            this.mbtLyapunovGains.Click += new System.EventHandler(this.mbtLyapunovGains_Click);
            // 
            // controlToolStripMenuItem
            // 
            this.controlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtManualControl,
            this.mbtControlSelection,
            this.mbtPathGeneration,
            this.mbtMissionControl});
            this.controlToolStripMenuItem.Name = "controlToolStripMenuItem";
            this.controlToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.controlToolStripMenuItem.Text = "Control";
            // 
            // mbtManualControl
            // 
            this.mbtManualControl.Name = "mbtManualControl";
            this.mbtManualControl.Size = new System.Drawing.Size(165, 22);
            this.mbtManualControl.Text = "Manual Control";
            this.mbtManualControl.Click += new System.EventHandler(this.mbtManualControl_Click);
            // 
            // mbtControlSelection
            // 
            this.mbtControlSelection.Name = "mbtControlSelection";
            this.mbtControlSelection.Size = new System.Drawing.Size(165, 22);
            this.mbtControlSelection.Text = "Control Selection";
            this.mbtControlSelection.Click += new System.EventHandler(this.mbtControlSelection_Click);
            // 
            // mbtPathGeneration
            // 
            this.mbtPathGeneration.Name = "mbtPathGeneration";
            this.mbtPathGeneration.Size = new System.Drawing.Size(165, 22);
            this.mbtPathGeneration.Text = "Path Generation";
            this.mbtPathGeneration.Click += new System.EventHandler(this.mbtPathGeneration_Click);
            // 
            // mbtMissionControl
            // 
            this.mbtMissionControl.Name = "mbtMissionControl";
            this.mbtMissionControl.Size = new System.Drawing.Size(165, 22);
            this.mbtMissionControl.Text = "Mission Control";
            this.mbtMissionControl.Click += new System.EventHandler(this.mbtMissionControl_Click);
            // 
            // inputCameraToolStripMenuItem
            // 
            this.inputCameraToolStripMenuItem.Name = "inputCameraToolStripMenuItem";
            this.inputCameraToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.inputCameraToolStripMenuItem.Text = "Input Camera";
            this.inputCameraToolStripMenuItem.Click += new System.EventHandler(this.inputCameraToolStripMenuItem_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtUDP,
            this.mbtSerialPort,
            this.toolStripSeparator4,
            this.loadSettingsToolStripMenuItem,
            this.saveSettingsToolStripMenuItem,
            this.toolStripSeparator5,
            this.PlaybackToolStripMenuItem});
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(113, 20);
            this.configurationToolStripMenuItem.Text = "CONFIGURATION";
            // 
            // mbtUDP
            // 
            this.mbtUDP.Name = "mbtUDP";
            this.mbtUDP.Size = new System.Drawing.Size(145, 22);
            this.mbtUDP.Text = "UDP";
            this.mbtUDP.Click += new System.EventHandler(this.mbtUDP_Click);
            // 
            // mbtSerialPort
            // 
            this.mbtSerialPort.Name = "mbtSerialPort";
            this.mbtSerialPort.Size = new System.Drawing.Size(145, 22);
            this.mbtSerialPort.Text = "Serial Port";
            this.mbtSerialPort.Click += new System.EventHandler(this.mbtSerialPort_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(142, 6);
            // 
            // loadSettingsToolStripMenuItem
            // 
            this.loadSettingsToolStripMenuItem.Name = "loadSettingsToolStripMenuItem";
            this.loadSettingsToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.loadSettingsToolStripMenuItem.Text = "Load Settings";
            this.loadSettingsToolStripMenuItem.Click += new System.EventHandler(this.loadSettingsToolStripMenuItem_Click);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.saveSettingsToolStripMenuItem.Text = "Save Settings";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(142, 6);
            // 
            // PlaybackToolStripMenuItem
            // 
            this.PlaybackToolStripMenuItem.Name = "PlaybackToolStripMenuItem";
            this.PlaybackToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.PlaybackToolStripMenuItem.Text = "Playback";
            this.PlaybackToolStripMenuItem.Click += new System.EventHandler(this.PlaybackToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.helpToolStripMenuItem.Text = "HELP";
            // 
            // mbtAbout
            // 
            this.mbtAbout.Name = "mbtAbout";
            this.mbtAbout.Size = new System.Drawing.Size(107, 22);
            this.mbtAbout.Text = "About";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slControlerFreq,
            this.slIMUFreq,
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 240);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(709, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // slControlerFreq
            // 
            this.slControlerFreq.Name = "slControlerFreq";
            this.slControlerFreq.Size = new System.Drawing.Size(118, 17);
            this.slControlerFreq.Text = "toolStripStatusLabel1";
            // 
            // slIMUFreq
            // 
            this.slIMUFreq.Name = "slIMUFreq";
            this.slIMUFreq.Size = new System.Drawing.Size(118, 17);
            this.slIMUFreq.Text = "toolStripStatusLabel3";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripProgressBar1.BackgroundColor = System.Drawing.Color.Lime;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(63, 17);
            this.toolStripStatusLabel1.Text = "HIL Enable";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(83, 17);
            this.toolStripStatusLabel2.Text = "Guided Enable";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCRRCSim});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(709, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbCRRCSim
            // 
            this.tsbCRRCSim.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCRRCSim.Image = ((System.Drawing.Image)(resources.GetObject("tsbCRRCSim.Image")));
            this.tsbCRRCSim.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCRRCSim.Name = "tsbCRRCSim";
            this.tsbCRRCSim.Size = new System.Drawing.Size(23, 22);
            this.tsbCRRCSim.Text = "CRRCSim";
            this.tsbCRRCSim.Click += new System.EventHandler(this.tsbCRRCSim_Click);
            // 
            // dockPanel
            // 
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.Location = new System.Drawing.Point(0, 49);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(709, 191);
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            autoHideStripSkin1.TextFont = new System.Drawing.Font("Segoe UI", 9F);
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            dockPaneStripSkin1.TextFont = new System.Drawing.Font("Segoe UI", 9F);
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.InactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.InactiveCaptionText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.dockPanel.Skin = dockPanelSkin1;
            this.dockPanel.TabIndex = 3;
            // 
            // mbtLyapunov3DGains
            // 
            this.mbtLyapunov3DGains.Name = "mbtLyapunov3DGains";
            this.mbtLyapunov3DGains.Size = new System.Drawing.Size(173, 22);
            this.mbtLyapunov3DGains.Text = "Lyapunov3D Gains";
            this.mbtLyapunov3DGains.Click += new System.EventHandler(this.mbtLyapunov3DGains_Click);
            // 
            // Mav3DSim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 262);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Mav3DSim";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MAV3D Sim V2.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MNAV3DSim_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mbtOpen;
        private System.Windows.Forms.ToolStripMenuItem mbtExit;
        private System.Windows.Forms.ToolStripMenuItem mbtSaveWorkspace;
        private System.Windows.Forms.ToolStripMenuItem mbtLoadWorkspace;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mbtShowMap;
        private System.Windows.Forms.ToolStripMenuItem mbtMapInfo;
        private System.Windows.Forms.ToolStripMenuItem mbtMapConfig;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mbtRawData;
        private System.Windows.Forms.ToolStripMenuItem mbtAddPlot;
        private System.Windows.Forms.ToolStripMenuItem mbtSavePlotWorkspace;
        private System.Windows.Forms.ToolStripMenuItem mbtLoadPlotWorkspace;
        private System.Windows.Forms.ToolStripMenuItem instrumentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mbtAirspeed;
        private System.Windows.Forms.ToolStripMenuItem mbtAttitudInstrument;
        private System.Windows.Forms.ToolStripMenuItem mbtAltimeter;
        private System.Windows.Forms.ToolStripMenuItem mbtCompass;
        private System.Windows.Forms.ToolStripMenuItem mbtVerticalSpeed;
        private System.Windows.Forms.ToolStripMenuItem mbtBattery;
        private System.Windows.Forms.ToolStripMenuItem mbt3DView;
        private System.Windows.Forms.ToolStripMenuItem gainsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mbtUDP;
        private System.Windows.Forms.ToolStripMenuItem mbtSerialPort;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mbtAbout;
        private System.Windows.Forms.ToolStripMenuItem mbtRollPIDGains;
        private System.Windows.Forms.ToolStripMenuItem mbtPitchPIDGains;
        private System.Windows.Forms.ToolStripMenuItem mbtAirspeedGains;
        private System.Windows.Forms.ToolStripMenuItem mbtAltitudPIDGains;
        private System.Windows.Forms.ToolStripMenuItem mbtL1PIDGains;
        private System.Windows.Forms.ToolStripMenuItem mbtLyapunovGains;
        private System.Windows.Forms.ToolStripMenuItem mbtManualControl;
        private System.Windows.Forms.ToolStripMenuItem mbtControlSelection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbCRRCSim;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mbtPathGeneration;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mbtSaveLogFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem loadSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inputCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel slControlerFreq;
        private BSE.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem mbtMissionControl;
        private System.Windows.Forms.ToolStripMenuItem mbtMessages;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel slIMUFreq;
        private System.Windows.Forms.ToolStripMenuItem mbtWaypoints;
        private System.Windows.Forms.ToolStripMenuItem mbtGeofence;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem PlaybackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mbtLyapunov3DGains;

    }
}

