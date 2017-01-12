using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using GMap.NET;
using System.Threading;
using System.Reflection;
using System.IO;
using MAV3DSim.Utils.WinFormControls;
using MAV3DSim.Utils;
using System.Xml;
using GMap.NET.WindowsForms;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna;
using System.Windows.Forms.DataVisualization.Charting;


namespace MAV3DSim

{
    public partial class Mav3DSim : Form
    {

        #region Definitions

        XBoxController xboxController;
        Com com = new Com();
        
        IMU imu = new Utils.IMU();
        MapTools mapTools = new Utils.MapTools();
        //List<PointLatLng> pointListObjectives;
        //int currentObjective = 0;
        Actuators actuators = new Actuators();
        bool closeRequested = false;
        DateTime lastTime = DateTime.Now;
        Thread updateGUI;
        Thread updateIMU;
        Thread updateAvionicsInstruments;
        Thread updateRawData;
        Thread updateMap;
        Thread controller;
        Thread updateImuPlayback;
        ThreadSafe threadSafe = new ThreadSafe();
        bool sendUpdate = false;

        Log log = new Log();
        DateTime startTime;
        Dictionary<string, Docks.Plot> PlotDocks = new Dictionary<string, Docks.Plot>();
        private Dictionary<string, double> plotVariables = new Dictionary<string, double>();
        bool startMission = false;


        Dictionary<String, IDockContent> docks = new Dictionary<string, IDockContent>();
        Docks.Map mapDock = new Docks.Map();
        Docks.MapInfo mapInfoDock = new Docks.MapInfo();
        Docks.MapConfig mapConfigDock = new Docks.MapConfig();
        Docks.RawData rawDataDock = new Docks.RawData();
        //Docks.PlotRollPAx plotRollPAxDock = new Docks.PlotRollPAx();
        //Docks.PlotPitchQAy plotPitchQAyDock = new Docks.PlotPitchQAy();
        //Docks.PlotYawRAz plotYawRAzDock = new Docks.PlotYawRAz();
        Docks.Airspeed airSpeedDock = new Docks.Airspeed();
        Docks.Altimeter altimeterDock = new Docks.Altimeter();
        Docks.AttitudeIndicator AttitudeIndicatorDock = new Docks.AttitudeIndicator();
        Docks.Compass compassDock = new Docks.Compass();
        Docks.Verticalspeed vertiaclSpeedDock = new Docks.Verticalspeed();
        Docks.RollPidGains rollPidGainsDock = new Docks.RollPidGains();
        Docks.PitchPidGains pitchPidGainsDock = new Docks.PitchPidGains();
        Docks.AirSpeedPidGains airSpeedPidGainsDock = new Docks.AirSpeedPidGains();
        Docks.AltitudePidGains altitudePidGainsDock = new Docks.AltitudePidGains();
        Docks.L1Gain l1GainDock = new Docks.L1Gain();
        Docks.LyapunovGains lyapunovGainsDock = new Docks.LyapunovGains();
        Docks.Lyapunov3DGains lyapunov3DGainsDock = new Docks.Lyapunov3DGains();
        Docks.ManualControl manualControlDock = new Docks.ManualControl();
        Docks.MissionControl missionControlDock = new Docks.MissionControl();
        Docks.ControlSelection controlSelectionDock = new Docks.ControlSelection();
        Docks.UDPConfig udpConfigDock = new Docks.UDPConfig();
        Docks.SerialPortConfig serialPortConfigDock = new Docks.SerialPortConfig();
        Docks.PlotScale plotScaleDock = new Docks.PlotScale();
        Docks.CRRCSim CRRCSimDock = new Docks.CRRCSim();
        //Docks.Plot PlotDock = new Docks.Plot();
        Docks.PathGeneration pathGenerationDock = new Docks.PathGeneration();
        Docks.CameraInput cameraInputDock = new Docks.CameraInput();
        Docks.Messages messagesDock = new Docks.Messages();
        Docks.Waypoints waypointsDock = new Docks.Waypoints();
        Docks.Geofence geofenceDock = new Docks.Geofence();
        Docks.Playback playbackDock = new Docks.Playback();

        WeifenLuo.WinFormsUI.Docking.VS2013DarkTheme vS2013DarkTheme1;
        WeifenLuo.WinFormsUI.Docking.VS2005Theme vS2005Theme1;
        VS2003Theme vS2003Theme1;
        private readonly ToolStripRenderer _custom = new Docks.VS2012ToolStripRenderer();

        MavLink mavlink = new MavLink();

        MavLinkNet.MavLinkUdpTransport mavLinkUdpTransport;
        MavLinkNet.MavLinkSerialPortTransport mavLinkSerialTransport;
        MavLinkNet.UasHeartbeat mavHeartBeat = new MavLinkNet.UasHeartbeat();
        MavLinkNet.UasManualControl manualControl = new MavLinkNet.UasManualControl();
        MavLinkNet.UasMissionCount mCount = new MavLinkNet.UasMissionCount();
        Controller.UAVControlQuaternion uavControlQuaternion;

        

        public Navigation.Navigator navigator = new Navigation.Navigator();
        //public Controler.Navigator3D navigator3D = new Controler.Navigator3D();
        public Navigation.Navigator4D navigator4D = new Navigation.Navigator4D();

        //Utils.FilterButterworth FilterL1Output = new FilterButterworth(25, 3, 1.5, 1.0)

        static readonly object lockVariables = new object();

        MathTools mtools = new MathTools();

        #endregion

        public Mav3DSim()
        {
            InitializeComponent();

            // Initialice XBox Controller Class
            xboxController = new XBoxController();
            xboxController.StateChanged += new StateChangedHandler(xboxController_StateChanged);
            

            // Initialize events handlers
            controlSelectionDock.StartButton += new Docks.RadioButtonEventHandler(controlSelectionDock_StartButton);
            controlSelectionDock.StartMissionButton += new Docks.RadioButtonEventHandler(controlSelectionDock_StartMissionButton);
            controlSelectionDock.StopButton += new Docks.RadioButtonEventHandler(controlSelectionDock_StopButton);
            controlSelectionDock.RollPitchRButton += new Docks.RadioButtonEventHandler(controlSelectionDock_RollPitchRButton);
            controlSelectionDock.AltitudeRButton += new Docks.RadioButtonEventHandler(controlSelectionDock_AltitudeRButton);
            controlSelectionDock.ManualRButton += new Docks.RadioButtonEventHandler(controlSelectionDock_ManualRButton);
            controlSelectionDock.L1ControllerRButton += new Docks.RadioButtonEventHandler(controlSelectionDock_L1ControllerRButton);
            controlSelectionDock.LyapControllerRButton += new Docks.RadioButtonEventHandler(controlSelectionDock_LyapControllerRButton);
            controlSelectionDock.LyapController3DRButton += new Docks.RadioButtonEventHandler(controlSelectionDock_LyapController3DRButton);

            udpConfigDock.UDPConnect += new Docks.UDPConnectEventHandler(udpConfigDock_UDPConnect);
            serialPortConfigDock.SerialConnect += new Docks.SerialConnectEventHandler(serialPortConfigDock_SerialConnect);
            serialPortConfigDock.SerialDisconnect += new Docks.SerialDisconnectEventHandler(serialPortConfigDock_SerialDisconnect);
            mapDock.RectangleDrawn += new Docks.RectangleDrawnEventHandler(mapDock_RectangleDrawn);
            mapDock.PolygonDrawn += new Docks.PolygonDrawnEventHandler(mapDock_PolygonDrawn);
            mapDock.GeofenceDrawn += new Docks.GeofenceDrawnEventHandler(mapDock_GeofenceDrawn);
            mapDock.PoiModified += new Docks.PoiModifiedEventHandler(mapDock_PoiModified);
            mapConfigDock.LoadButton += new Docks.LoadEventHandler(mapConfigDock_Load);
            mapConfigDock.RefreshButton += new Docks.RefreshEventHandler(mapConfigDock_Refresh);
            rollPidGainsDock.UpdateButton += new Docks.UpdatePidEventHandler(RollPidGainsDock_UpdateButton);
            pitchPidGainsDock.UpdateButton += new Docks.UpdatePidEventHandler(PitchPidGainsDock_UpdateButton);
            airSpeedPidGainsDock.UpdateButton += new Docks.UpdatePidFFEventHandler(SpeedPidGainsDock_UpdateButton);
            altitudePidGainsDock.UpdateButton += new Docks.UpdatePidEventHandler(AltitudePidGainsDock_UpdateButton);
            l1GainDock.UpdateButton += new Docks.UpdateL1GainsEventHandler(L1GainsDock_UpdateButton);
            lyapunovGainsDock.UpdateButton += new Docks.UpdateLyapEventHandler(LyapGainsDock_UpdateButton);
            //lyapunov3DGainsDock.UpdateButton += new Docks.UpdateLyap3DEventHandler(Lyap3DGainsDock_UpdateButton);
            missionControlDock.ReturnFromPOI += new Docks.ReturnFromPOIEventHandler(misionControlDock_ReturnFromPOI);
            


            // Init docks list

            docks.Add(typeof(Docks.Map).ToString(), mapDock);
            docks.Add(typeof(Docks.MapInfo).ToString(), mapInfoDock);
            docks.Add(typeof(Docks.MapConfig).ToString(), mapConfigDock);
            docks.Add(typeof(Docks.RawData).ToString(), rawDataDock);
            //docks.Add(typeof(Docks.PlotRollPAx).ToString(), plotRollPAxDock);
            //docks.Add(typeof(Docks.PlotPitchQAy).ToString(), plotPitchQAyDock);
            //docks.Add(typeof(Docks.PlotYawRAz).ToString(), plotYawRAzDock);
            docks.Add(typeof(Docks.Airspeed).ToString(), airSpeedDock);
            docks.Add(typeof(Docks.Altimeter).ToString(), altimeterDock);
            docks.Add(typeof(Docks.AttitudeIndicator).ToString(), AttitudeIndicatorDock);
            docks.Add(typeof(Docks.Compass).ToString(), compassDock);
            docks.Add(typeof(Docks.Verticalspeed).ToString(), vertiaclSpeedDock);
            docks.Add(typeof(Docks.RollPidGains).ToString(), rollPidGainsDock);
            docks.Add(typeof(Docks.PitchPidGains).ToString(), pitchPidGainsDock);
            docks.Add(typeof(Docks.AirSpeedPidGains).ToString(), airSpeedPidGainsDock);
            docks.Add(typeof(Docks.AltitudePidGains).ToString(), altitudePidGainsDock);
            docks.Add(typeof(Docks.L1Gain).ToString(), l1GainDock);
            docks.Add(typeof(Docks.LyapunovGains).ToString(), lyapunovGainsDock);
            docks.Add(typeof(Docks.Lyapunov3DGains).ToString(), lyapunov3DGainsDock);
            docks.Add(typeof(Docks.ManualControl).ToString(), manualControlDock);
            docks.Add(typeof(Docks.MissionControl).ToString(), missionControlDock);
            docks.Add(typeof(Docks.ControlSelection).ToString(), controlSelectionDock);
            docks.Add(typeof(Docks.UDPConfig).ToString(), udpConfigDock);
            docks.Add(typeof(Docks.SerialPortConfig).ToString(), serialPortConfigDock);
            docks.Add(typeof(Docks.PlotScale).ToString(), plotScaleDock);
            docks.Add(typeof(Docks.CRRCSim).ToString(), CRRCSimDock);
            docks.Add(typeof(Docks.PathGeneration).ToString(), pathGenerationDock);
            docks.Add(typeof(Docks.CameraInput).ToString(), cameraInputDock);
            docks.Add(typeof(Docks.Messages).ToString(), messagesDock);
            docks.Add(typeof(Docks.Waypoints).ToString(), waypointsDock);
            docks.Add(typeof(Docks.Geofence).ToString(), geofenceDock);
            docks.Add(typeof(Docks.Playback).ToString(), playbackDock);

            

            // Change Look and Feel
            vS2013DarkTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2013DarkTheme();
            vS2005Theme1 = new VS2005Theme();
            vS2003Theme1 = new VS2003Theme();
            dockPanel.Theme = vS2013DarkTheme1;
            //ToolStripManager.Renderer = _custom;
            
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new Docks.VS2013ColorTable());
            toolStrip1.Renderer = new ToolStripProfessionalRenderer(new Docks.VS2013ColorTable());
            

            //menuStrip1.BackgroundImage = Properties.Resources.dot;

            fileToolStripMenuItem.ForeColor = Color.White;
            viewToolStripMenuItem.ForeColor = Color.White;
            configurationToolStripMenuItem.ForeColor = Color.White;
            helpToolStripMenuItem.ForeColor = Color.White;

            

            List<ToolStripMenuItem> allItems = new List<ToolStripMenuItem>();
            foreach (ToolStripMenuItem toolItem in menuStrip1.Items) {
                //allItems.Add(toolItem);
                //add sub items
                allItems.AddRange(GetItems(toolItem));
            }

            for (int i = 0; i < allItems.Count;i++ )
            {
                allItems[i].ForeColor = Color.White;
                allItems[i].BackColor = Color.FromArgb(27,27,28);

            }
            Dictionary<String, IDockContent>.ValueCollection values = docks.Values;
            foreach (DockContent d in values)
            {
                d.BackColor = Color.FromArgb(45, 45, 48);
                skinGUI(d.Controls);
            }

            /*


            AutoHideStripSkin autoHideSkin = new AutoHideStripSkin();
            autoHideSkin.DockStripGradient.StartColor = Color.FromArgb(45, 45, 48);
            autoHideSkin.DockStripGradient.EndColor = Color.FromArgb(45, 45, 48);
            autoHideSkin.DockStripGradient.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            autoHideSkin.TabGradient.StartColor = Color.FromArgb(0, 122, 204);
            autoHideSkin.TabGradient.EndColor = Color.FromArgb(0, 122, 204);
            autoHideSkin.TabGradient.TextColor = Color.White;
            //autoHideSkin.TextFont = new Font("Showcard Gothic", 10);

            dockPanel.Skin.AutoHideStripSkin = autoHideSkin;
             * */
            dockPanel.BackColor = Color.FromArgb(45, 45, 48);
            dockPanel.DockBackColor = Color.FromArgb(45, 45, 48);
           
            toolStrip1.BackColor = Color.FromArgb(45, 45, 48);
            toolStrip1.ForeColor = Color.White;
            statusStrip1.BackColor = Color.FromArgb(45, 45, 48);
            statusStrip1.ForeColor = Color.White;

            //PlotDock.PlotVariables = imu.PopulateVariable();
            //PlotDock.PopulateMenu();

            // Init Controller
            uavControlQuaternion = new Controller.UAVControlQuaternion();


        }

        

        private IEnumerable<ToolStripMenuItem> GetItems(ToolStripMenuItem item)
        {
            
            
            foreach (ToolStripMenuItem dropDownItem in item.DropDownItems.OfType<ToolStripMenuItem>())
            {
                if (dropDownItem.HasDropDownItems)
                {
                    foreach (ToolStripMenuItem subItem in GetItems(dropDownItem))
                        yield return subItem;
                }
                yield return dropDownItem;
            }

           
       
        
        }
        private void skinGUI(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c is CustomGroupBox)
                {
                    CustomGroupBox c1 = (CustomGroupBox)c;
                    c1.ForeColor = Color.FromArgb(0, 122, 204);
                    c1.BackColor = Color.FromArgb(45,45,48);
                    c1.BorderColor = Color.FromArgb(63,63,70);
                    c1.RoundCorners = 10;
                    skinGUI(c.Controls);

                }
                else if (c is Button)
                {
                    c.ForeColor = Color.FromArgb(0, 122, 204);

                }
                else if (c is CustomTextBox)
                {
                    CustomTextBox c1 = (CustomTextBox)c;
                    
                    c1.BackColor = Color.FromArgb(51, 51, 55);
                    //c1.BorderStyle = BorderStyle.FixedSingle;
                    c1.BorderColor = Color.FromArgb(63,63,70);
                    c1.BorderColorHover = Color.FromArgb(0,122,204);
                    c1.ForeColor = Color.FromArgb(153, 153, 153);
                    c1.ForeColorHover = Color.White;

                }
                else if (c is ComboBox)
                {
                    ComboBox c1 = (ComboBox)c;

                    c1.BackColor = Color.FromArgb(51, 51, 55);
                    c1.ForeColor = Color.White;
                }
                else
                    c.ForeColor = Color.White;
            }
        }
        #region EventHandlers
        void RollPidGainsDock_UpdateButton(object sender, Docks.PidGainsEventArgs e)
        {
            uavControlQuaternion.updateRollGains(e.Kp, e.Ki, e.Kd);
        }
        void PitchPidGainsDock_UpdateButton(object sender, Docks.PidGainsEventArgs e)
        {
            uavControlQuaternion.updatePitchGains(e.Kp, e.Ki, e.Kd);
        }
        void SpeedPidGainsDock_UpdateButton(object sender, Docks.PidFFGainsEventArgs e)
        {
            uavControlQuaternion.updateSpeedGains(e.Kp, e.Ki, e.Kd,e.FF);
        }
        void AltitudePidGainsDock_UpdateButton(object sender, Docks.PidGainsEventArgs e)
        {
            uavControlQuaternion.updateAltitudeGains(e.Kp, e.Ki, e.Kd);
        }
        void L1GainsDock_UpdateButton(object sender, Docks.L1GainsEventArgs e)
        {
            navigator.TurnRadius = e.TurnRadius;
            navigator4D.TurnRadius = e.TurnRadius;
            L1 = e.L1;
        }
        

        void LyapGainsDock_UpdateButton(object sender, Docks.LyapGainsEventArgs e)
        {
            
            Kdelta = e.Kdelta;
            Ks = e.Ks;
            Kw1 = e.Kw1;
            Kphi = e.Kphi;
            Psi_a = e.Psi_a;

        }

        void misionControlDock_ReturnFromPOI(object sender, EventArgs e)
        {
            navigator.IgnorePointsOfInterest = true;
            navigator.IsCirclePath = true;
            navigator.IsReturnFromCircle = true;

            navigator4D.IgnorePointsOfInterest = true;
            navigator4D.IsCirclePath = true;
            navigator4D.IsReturnFromCircle = true;
        }

        void serialPortConfigDock_SerialConnect(object sender, EventArgs e)
        {
            //com.BaudRate = (string)cbBaudRate.SelectedItem;
            //com.DataBits = (string)cbDataBits.SelectedItem;
            //com.Parity = (string)cbParity.SelectedItem;
            //com.PortName = (string)cbPort.SelectedItem;
            //com.StopBits = (string)cbStopBits.SelectedItem;
            //com.UseSerialCom = true;
            //com.CreateConnectionSerialPort();
            //com.DataReceive += new DataReceivedHandler(com_DataReceive);

            if (serialPortConfigDock.Protocol == "MNAV")
            {

            }
            else 
            {
                mavLinkSerialTransport = new MavLinkNet.MavLinkSerialPortTransport();
                mavLinkSerialTransport.BaudRate = Convert.ToInt32(serialPortConfigDock.BaudRate);
                mavLinkSerialTransport.MavlinkSystemId = 1;
                mavLinkSerialTransport.MavlinkComponentId = 0;
                mavLinkSerialTransport.SerialPortName = serialPortConfigDock.Port;
                mavLinkSerialTransport.OnPacketReceived += mavLinkTransport_OnPacketReceived;
                mavLinkSerialTransport.Initialize();
                
            }

            

            sendUpdate = true;
        }

        void serialPortConfigDock_SerialDisconnect(object sender, EventArgs e)
        {
            if (serialPortConfigDock.Protocol == "MNAV")
            {

            }
            else
            {
                mavLinkSerialTransport.Dispose();
            }
            sendUpdate = true;
        }

        void udpConfigDock_UDPConnect(object sender, EventArgs e)
        {
            if(udpConfigDock.Protocol == "MNAV")
            { 
                com.UseSerialCom = false;
                com.Port = udpConfigDock.Port;
                com.IpAddress = udpConfigDock.IpAddress;
                com.CreatConnectionUDP();
            }else
            {
                mavLinkUdpTransport = new MavLinkNet.MavLinkUdpTransport();
                mavLinkUdpTransport.TargetIpAddress = udpConfigDock.IpAddress;
                mavLinkUdpTransport.UdpListeningPort = udpConfigDock.Port;
                mavLinkUdpTransport.UdpTargetPort = udpConfigDock.Port;
                mavLinkUdpTransport.MavlinkSystemId = 1;
                mavLinkUdpTransport.InitializeMavLink();
                mavLinkUdpTransport.InitializeUdpSync();
                mavLinkUdpTransport.OnPacketReceived += mavLinkTransport_OnPacketReceived;
                //mavLinkUdpTransport.Initialize();
                //mavLinkUdpTransport.HeartBeatUpdateRateMs = 100;
             
                //mavLinkUdpTransport.BeginHeartBeatLoop();
                
            }
                


        }

        void mavLinkTransport_OnPacketReceived(object sender, MavLinkNet.MavLinkPacket packet)
        {
            if (packet.MessageId == MavLinkNet.UasHeartbeat.MId)
            {
                if (((MavLinkNet.UasHeartbeat)packet.Message).BaseMode.HasFlag(MavLinkNet.MavModeFlag.HilEnabled))
                    threadSafe.SetStatusStripPropertyThreadSafe(statusStrip1, toolStripStatusLabel1, "BackColor", Color.Green);
                else
                    threadSafe.SetStatusStripPropertyThreadSafe(statusStrip1, toolStripStatusLabel1, "BackColor", Color.Red);

                if(((MavLinkNet.UasHeartbeat)packet.Message).BaseMode.HasFlag(MavLinkNet.MavModeFlag.GuidedEnabled))
                    threadSafe.SetStatusStripPropertyThreadSafe(statusStrip1, toolStripStatusLabel2, "BackColor",Color.Green);
                else
                    threadSafe.SetStatusStripPropertyThreadSafe(statusStrip1, toolStripStatusLabel2, "BackColor", Color.Red);
                mavHeartBeat = ((MavLinkNet.UasHeartbeat)packet.Message);
            
            }
            else
                if (packet.MessageId == MavLinkNet.UasStatustext.MId)
                {
                    messagesDock.AddMessage("(MAV " + packet.SystemId.ToString() + ":" + packet.ComponentId.ToString() + ") " + ((MavLinkNet.UasStatustext)packet.Message).Severity.ToString() + ": " + new string(((MavLinkNet.UasStatustext)packet.Message).Text).Replace("\0", string.Empty));
                }
                else if (packet.MessageId == MavLinkNet.UasHilControls.MId)
                {
                    actuators.Elevator = -(double)(((MavLinkNet.UasHilControls)packet.Message).PitchElevator) / 2;
                    actuators.Rudder = (double)(((MavLinkNet.UasHilControls)packet.Message).YawRudder) / 2;
                    actuators.Throttle = (double)(((MavLinkNet.UasHilControls)packet.Message).Throttle) / 1;
                    actuators.Aileron = -(double)(((MavLinkNet.UasHilControls)packet.Message).RollAilerons) / 2;
                }
                else if (packet.MessageId == MavLinkNet.UasMissionItem.MId)
                {
                    MavLinkNet.UasMissionItem mItem = (MavLinkNet.UasMissionItem)packet.Message;
                    mapDock.NewWaypoint(new PointLatLngAlt(mItem.X, mItem.Y, mItem.Z));
                    waypointsDock.NewWaypoint(new PointLatLngAlt(mItem.X, mItem.Y, mItem.Z), mItem.Seq);
                    navigator4D.NewWaypoint(new PointLatLngAlt(mItem.X, mItem.Y, mItem.Z));

                    if(mItem.Seq < mCount.Count-1 )
                    {
                        MavLinkNet.UasMissionRequest mRequest = new MavLinkNet.UasMissionRequest();
                        mRequest.Seq = ++mItem.Seq;
                        mRequest.TargetSystem = 1;
                        mRequest.TargetComponent = 190;
                        mavLinkSerialTransport.SendMessage(mRequest);
                    }else
                    {
                        MavLinkNet.UasMissionAck mRequest = new MavLinkNet.UasMissionAck();
                        mRequest.TargetSystem = 1;
                        mRequest.TargetComponent = 190;
                        mavLinkSerialTransport.SendMessage(mRequest);

                        navigator4D.GenerateWaypointsPath(175);
                        mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.Waypoints, new GMapRoute(navigator4D.PointListObjectivesLatLng, "Route"), Color.FromArgb(127, Color.Black));
                    }
                }
                else if(packet.MessageId == MavLinkNet.UasMissionCount.MId)
                {
                    mCount = (MavLinkNet.UasMissionCount)packet.Message;
                    mapDock.ClearWaypoints();
                    waypointsDock.ClearWaypoints();
                    navigator4D.ClearWaypoints();
                    //navigator4D.TurnRadius = l1GainDock.TurnRadius;
                    MavLinkNet.UasMissionRequest mRequest = new MavLinkNet.UasMissionRequest();
                    mRequest.Seq = 0;
                    mRequest.TargetSystem = 1;
                    mRequest.TargetComponent = 190;
                    mavLinkSerialTransport.SendMessage(mRequest);

                }
                else if (packet.MessageId == MavLinkNet.UasAttitude.MId)
                {
                    if (!controlSelectionDock.HilSimulator)
                    imu.decode(packet.Message);
                }
                else if (packet.MessageId == MavLinkNet.UasHighresImu.MId)
                {
                    if (!controlSelectionDock.HilSimulator)
                    imu.decode(packet.Message);
                }
                else if (packet.MessageId == MavLinkNet.UasVfrHud.MId)
                {

                }
                else if (packet.MessageId == MavLinkNet.UasVfrHud.MId)
                {

                }
                else if (packet.MessageId == MavLinkNet.UasRcChannels.MId)
                {

                }
                else if (packet.MessageId == MavLinkNet.UasCommandLong.MId)
                {

                }
                else if (packet.MessageId == MavLinkNet.UasGlobalPositionInt.MId)
                {
                    if (!controlSelectionDock.HilSimulator)
                    imu.decode(packet.Message);
                }
                else if (packet.MessageId == MavLinkNet.UasLocalPositionNed.MId)
                {

                }
                else if (packet.MessageId == MavLinkNet.UasPositionTargetGlobalInt.MId)
                {

                }
                else if (packet.MessageId == MavLinkNet.UasAttitudeTarget.MId)
                {

                }
                else if (packet.MessageId == MavLinkNet.UasSysStatus.MId)
                {

                }
                else if (packet.MessageId == MavLinkNet.UasBatteryStatus.MId)
                {

                }
                else if (packet.MessageId == MavLinkNet.UasGpsRawInt.MId)
                {

                }
                else if (packet.MessageId == MavLinkNet.UasMissionCurrent.MId)
                {

                }
                else if (packet.MessageId == MavLinkNet.UasManualControl.MId)
                {
                    //actuators.Elevator = (double)(((MavLinkNet.UasManualControl)packet.Message).X)/2000;
                    //actuators.Rudder = (double)(((MavLinkNet.UasManualControl)packet.Message).R)/2000 ;
                    //actuators.Throttle = (double)(((MavLinkNet.UasManualControl)packet.Message).Z)/1000;
                    //actuators.Aileron = (double)(((MavLinkNet.UasManualControl)packet.Message).Y)/2000;
                } else if(packet.MessageId == MavLinkNet.UasDistanceSensor.MId)
                {
                    imu.decode(packet.Message);
                }

  
                    /*if(packet.MessageId == MavLinkNet.UasVfrHud.MId)
            {
                actuators.Aileron = (double)(((MavLinkNet.UasVfrHud)packet.Message).Airspeed * .5) * 0.02;
                actuators.Elevator = (double)(((MavLinkNet.UasVfrHud)packet.Message).Groundspeed * .5) * 0.02;
                actuators.Rudder = (double)(((MavLinkNet.UasVfrHud)packet.Message).Alt * .5) * 0.02;
                actuators.Throttle = (double)(((MavLinkNet.UasVfrHud)packet.Message).Climb);
            }*/
                else if (!controlSelectionDock.HilSimulator)
                    imu.decode(packet.Message);
            
            
        }

        void controlSelectionDock_StartButton(object sender, EventArgs e)
        {

            // Init StartTime
            startTime = DateTime.Now;
            // init scope counter 

            //plotRollPAxDock.ClearAll();
            //plotPitchQAyDock.ClearAll();
            //plotYawRAzDock.ClearAll();

            // Init Log File
            log.ResetLog();

            // Reset vehicle route
            mapDock.ClearVehicleRoute(new PointLatLng(mapConfigDock.InitLat, mapConfigDock.InitLon));
            

            if (!com.UseSerialCom && mavLinkSerialTransport==null && !playbackDock.PlaybackEnabled || controlSelectionDock.HilSimulator)
            {
                if (controlSelectionDock.HilSimulator)
                {
                    MavLinkNet.UasCommandLong cmd = new MavLinkNet.UasCommandLong();
                    cmd.Command = MavLinkNet.MavCmd.DoSetMode;
                    cmd.Confirmation = 1;
                    cmd.Param1 = (float)MavLinkNet.MavModeFlag.HilEnabled;
                    cmd.TargetSystem = 1;
                    cmd.TargetComponent = 0;
                    mavLinkSerialTransport.SendMessage(cmd);
                    
                }
                closeRequested = false;
                controller = new Thread(new ThreadStart(this.Controller));
                controller.Name = "Controller";
                controller.Start();

                updateIMU = new Thread(new ThreadStart(this.UpdateIMU));
                updateIMU.Name = "UpdateIMU";
                updateIMU.Start();

                updateGUI = new Thread(new ThreadStart(this.UpdateGUI));
                updateGUI.Name = "UpdateGUI";
                updateGUI.Start();

                updateAvionicsInstruments = new Thread(new ThreadStart(this.UpdateAvionicsInstruments));
                updateAvionicsInstruments.Name = "UpdateAvionicsInstruments";
                updateAvionicsInstruments.Start();

                updateRawData = new Thread(new ThreadStart(this.UpdateRawData));
                updateRawData.Name = "UpdateRawData";
                updateRawData.Start();

                updateMap = new Thread(new ThreadStart(this.UpdateMap));
                updateMap.Name = "UpdateMap";
                updateMap.Start();

            }
            else
            {
                closeRequested = false;

                if (playbackDock.PlaybackEnabled)
                {
                    
                    updateImuPlayback = new Thread(new ThreadStart(this.UpdateIMUPlayback));
                    updateImuPlayback.Name = "UpdatImuPlayback";
                    updateImuPlayback.Start();
                }

                updateGUI = new Thread(new ThreadStart(this.UpdateGUI));
                updateGUI.Name = "UpdateGUI";
                updateGUI.Start();
                sendUpdate = true;

                updateRawData = new Thread(new ThreadStart(this.UpdateRawData));
                updateRawData.Name = "UpdateRawData";
                updateRawData.Start();

                updateMap = new Thread(new ThreadStart(this.UpdateMap));
                updateMap.Name = "UpdateMap";
                updateMap.Start();

                updateAvionicsInstruments = new Thread(new ThreadStart(this.UpdateAvionicsInstruments));
                updateAvionicsInstruments.Name = "UpdateAvionicsInstruments";
                updateAvionicsInstruments.Start();
            }


            InitUavControlQuaternion();
            navigator.IsTurn180 = false;
            navigator.TurnRadius = l1GainDock.TurnRadius;
            navigator.RestoreOriginalPath();

            navigator4D.IsTurn180 = false;
            navigator4D.TurnRadius = l1GainDock.TurnRadius;
            navigator4D.RestoreOriginalPath();

            /*
            testCount = 0;

            
            uavControl = new UAVControl();
            uavControlQuaternion = new UAVControlQuaternion();
            uavControl.updateRollGains(Convert.ToDouble(txtRollKp.Text), Convert.ToDouble(txtRollKi.Text), Convert.ToDouble(txtRollKd.Text));
            uavControl.updatePitchGains(Convert.ToDouble(txtPitchKp.Text), Convert.ToDouble(txtPitchKi.Text), Convert.ToDouble(txtPitchKd.Text));
            uavControl.updateAltitudGains(Convert.ToDouble(txtAltitudKp.Text), Convert.ToDouble(txtAltitudKi.Text), Convert.ToDouble(txtAltitudKd.Text));
            //uavControl.updateHeadingGains(Convert.ToDouble(txtHeadingKp.Text), Convert.ToDouble(txtHeadingKi.Text), Convert.ToDouble(txtHeadingKd.Text));

            uavControlQuaternion.updateRollGains(Convert.ToDouble(txtRollKp.Text), Convert.ToDouble(txtRollKi.Text), Convert.ToDouble(txtRollKd.Text));
            uavControlQuaternion.updatePitchGains(Convert.ToDouble(txtPitchKp.Text), Convert.ToDouble(txtPitchKi.Text), Convert.ToDouble(txtPitchKd.Text));
            uavControlQuaternion.updateYawGains(Convert.ToDouble(txtYawKp.Text), Convert.ToDouble(txtYawKi.Text), Convert.ToDouble(txtYawKd.Text));
            uavControlQuaternion.updateAltitudGains(Convert.ToDouble(txtAltitudKp.Text), Convert.ToDouble(txtAltitudKi.Text), Convert.ToDouble(txtAltitudKd.Text));
            uavControlQuaternion.updateSpeedGains(Convert.ToDouble(txtSpeedKp.Text), Convert.ToDouble(txtSpeedKi.Text), Convert.ToDouble(txtSpeedKd.Text));
            Kdelta = Convert.ToDouble(tbLyapKd.Value) / 1000;
            Kw1 = Convert.ToDouble(tbLyapKw.Value) / 1000;
            Ks = Convert.ToDouble(tbLyapKs.Value) / 1000;
            Kphi = Convert.ToDouble(tbLyapKphi.Value) / 1000;
            Psi_a = Convert.ToDouble(tbLyapPsia.Value) / 1000;
            */

            //uavControlQuaternion.updateHeadingGains(Convert.ToDouble(txtHeadingKp.Text), Convert.ToDouble(txtHeadingKi.Text), Convert.ToDouble(txtHeadingKd.Text));

            // If serial used ask for update
            if (com.UseSerialCom)
            {
                Byte[] data = new Byte[3];
                // Header
                data[0] = 0x55;
                data[1] = 0x55;
                data[2] = 0x55; // Send update

                com.Send(data);
                sendUpdate = false;
            }
        }

        void controlSelectionDock_StartMissionButton(object sender, EventArgs e)
        {
            navigator4D.GenerateTakeoffPath(new PointLatLngAlt(imu.lat + mapConfigDock.GPSLat, imu.lon + mapConfigDock.GPSLon, mapConfigDock.GPSAlt+ imu.alt), imu.psi);
            mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.InitialPath, new GMapRoute(navigator4D.PointListObjectivesLatLng, MAV3DSim.Docks.Map.MapRoute.Path.ToString()), Color.Orange);
            startMission = true;
            controlSelectionDock.Guidance = true;
            
            
        }

        void controlSelectionDock_StopButton(object sender, EventArgs e)
        {
            closeRequested = true;
            startMission = false;
            //threadSafe.SetControlPropertyThreadSafe(rbManual, "Checked", true);
            //currentObjective = 0;
            //overlay.Markers.Remove(markerCircle);

            navigator.RestoreOriginalPath();
            navigator4D.RestoreOriginalPath();

           

            foreach (var plot in PlotDocks)
            {
                plot.Value.Clear();

            }
            
        }

        void controlSelectionDock_ManualRButton(object sender, EventArgs e)
        {
            uavControlQuaternion.AltitudControl = false;
            uavControlQuaternion.HeadingControl = false;
            manualControlDock.Elevator1Visible = false;
            manualControlDock.Throttle1Visible = false;

            if (controlSelectionDock.HilSimulator)
            {
                MavLinkNet.UasCommandLong cmd = new MavLinkNet.UasCommandLong();
                cmd.Command = MavLinkNet.MavCmd.NavGuidedEnable;
                cmd.Confirmation = 1;
                cmd.Param1 = 0;
                cmd.TargetSystem = 1;
                cmd.TargetComponent = 50;

                mavLinkSerialTransport.SendMavlinkMessage(cmd);
            }
            
        }

        void controlSelectionDock_RollPitchRButton(object sender, EventArgs e)
        {
            uavControlQuaternion.AltitudControl = false;
            uavControlQuaternion.StartRollPitchControl();
            manualControlDock.Elevator1Visible = false;
            manualControlDock.Throttle1Visible = false;
            
        }
        void controlSelectionDock_AltitudeRButton(object sender, EventArgs e)
        {
            manualControlDock.Elevator1Visible = true;
            manualControlDock.sElevator1 = imu.alt.ToString();
            manualControlDock.Throttle1Visible = true;

            uavControlQuaternion.AltitudControl = true;
            uavControlQuaternion.HeadingControl = false;
            uavControlQuaternion.startAltitudControl();
            uavControlQuaternion.startSpeedControl();

            
        }
        void controlSelectionDock_L1ControllerRButton(object sender, EventArgs e)
        {
            // --------------Review this is deprecated

            navigator.RestoreOriginalPath();
            //navigator3D.RestoreOriginalPath();
            //pointListObjectives.InsertRange(0, pathGenerator.RoutePoints);
            navigator.InitialEnuPosition = new PointLatLng(mapConfigDock.GPSLat, mapConfigDock.GPSLon);
            navigator.CreateInitialPath(imu.psi,imu.lat + mapConfigDock.GPSLat, imu.lon + mapConfigDock.GPSLon );
            mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.InitialPath, new GMapRoute(navigator.PointListObjectives, "Route"), Color.FromArgb(127, Color.Black));

            /*navigator3D.InitialEnuPosition = new PointLatLngAlt(mapConfigDock.GPSLat, mapConfigDock.GPSLat,0);
            navigator3D.CreateInitialPath(imu.psi, imu.lat + mapConfigDock.GPSLat, imu.lon + mapConfigDock.GPSLon,imu.alt);
            mapDock.AddRoute(2, new GMapRoute(navigator3D.PointListObjectivesLatLng, "Route"), Color.FromArgb(127, Color.Black));*/

            //if (overlay.Routes.Count < 3)
            ////    overlay.Routes.Add(new GMapRoute(pointListObjectives, "Route"));
            //else
            //    overlay.Routes[2] = new GMapRoute(pointListObjectives, "Route");

            //overlay.Routes[2].Stroke.Color = System.Drawing.Color.Black;

            manualControlDock.Elevator1Visible = true; // txtElevator1.Visible = true;
            manualControlDock.Throttle1Visible = true; // txtThrottle1.Visible = true;
            manualControlDock.sElevator1 = imu.alt.ToString(); // txtElevator1.Text = imu.alt.ToString();
            //uavControl.HeadingControl = true;
            //uavControl.startHeadingControl();

            mapDock.UpdateMarkerCircle(navigator.CurrentObjective);
            //mapDock.UpdateMarkerCircle(navigator3D.CurrentObjective.GetPointLatLng());
            //markerCircle = new GMap.NET.WindowsForms.Markers.GMapMarkerImage(pointListObjectives[currentObjective], Properties.Resources.loading_circle);
            //overlay.Markers.Add(markerCircle);

            uavControlQuaternion.AltitudControl = true;
            uavControlQuaternion.HeadingControl = true;
            uavControlQuaternion.startYawControl();
            uavControlQuaternion.startSpeedControl();
        }

        void controlSelectionDock_LyapControllerRButton(object sender, EventArgs e)
        {
            /*navigator.RestoreOriginalPath();
            //pointListObjectives.InsertRange(0, pathGenerator.RoutePoints);
            navigator.InitialEnuPosition = new PointLatLng(mapConfigDock.GPSLat, mapConfigDock.GPSLat);
            navigator.CreateInitialPath(imu.psi, imu.lat + mapConfigDock.GPSLat, imu.lon + mapConfigDock.GPSLon);
            mapDock.AddRoute(2, new GMapRoute(navigator.PointListObjectives, "Route"), Color.Blue);
            */

            if (!startMission)
            {
                navigator4D.RestoreOriginalPath();
                //pointListObjectives.InsertRange(0, pathGenerator.RoutePoints);
                navigator4D.InitialEnuPosition = new PointLatLngAlt(mapConfigDock.GPSLat, mapConfigDock.GPSLon, mapConfigDock.GPSAlt);
                if (missionControlDock.GenerateInitialPath) {
                    navigator4D.CreateInitialPath(imu.psi, imu.lat + mapConfigDock.GPSLat, imu.lon + mapConfigDock.GPSLon, mapConfigDock.GPSAlt + imu.alt, 215);
                    mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.InitialPath, new GMapRoute(navigator4D.PointListObjectivesLatLng, "Route"), Color.Blue);
                
                }
                
            }
            manualControlDock.Elevator1Visible = true; // txtElevator1.Visible = true;
            manualControlDock.Throttle1Visible = true; // txtThrottle1.Visible = true;
            manualControlDock.sElevator1 = imu.alt.ToString(); // txtElevator1.Text = imu.alt.ToString();

            uavControlQuaternion.AltitudControl = true;
            uavControlQuaternion.HeadingControl = true;
            uavControlQuaternion.startYawControl();
            uavControlQuaternion.startSpeedControl();

            if (controlSelectionDock.HilSimulator)
            {
                MavLinkNet.UasCommandLong cmd = new MavLinkNet.UasCommandLong();
                cmd.Command = MavLinkNet.MavCmd.NavGuidedEnable;
                cmd.Confirmation = 1;
                cmd.Param1 = 1;
                cmd.TargetSystem = 1;
                cmd.TargetComponent = 50;

                //mavLinkSerialTransport.SendMessage(cmd);
                mavLinkSerialTransport.SendMavlinkMessage(cmd);


                messagesDock.AddMessage("UasCommandLong: " + MavLinkNet.MavCmd.NavGuidedEnable.ToString());
            }


        }

        void controlSelectionDock_LyapController3DRButton(object sender, EventArgs e)
        {
        
            if (!startMission)
            {
                navigator4D.RestoreOriginalPath();
                //pointListObjectives.InsertRange(0, pathGenerator.RoutePoints);
                navigator4D.InitialEnuPosition = new PointLatLngAlt(mapConfigDock.GPSLat, mapConfigDock.GPSLon, mapConfigDock.GPSAlt);
                navigator4D.CreateInitialPath(imu.psi, imu.lat + mapConfigDock.GPSLat, imu.lon + mapConfigDock.GPSLon, mapConfigDock.GPSAlt + imu.alt, 215);
                mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.InitialPath, new GMapRoute(navigator4D.PointListObjectivesLatLng, "Route"), Color.Blue);
            }
            manualControlDock.Elevator1Visible = true; // txtElevator1.Visible = true;
            manualControlDock.Throttle1Visible = true; // txtThrottle1.Visible = true;
            manualControlDock.sElevator1 = imu.alt.ToString(); // txtElevator1.Text = imu.alt.ToString();

            uavControlQuaternion.AltitudControl = false;
            uavControlQuaternion.HeadingControl = true;
            uavControlQuaternion.startYawControl();
            uavControlQuaternion.startSpeedControl();

            if (controlSelectionDock.HilSimulator)
            {
                MavLinkNet.UasCommandLong cmd = new MavLinkNet.UasCommandLong();
                cmd.Command = MavLinkNet.MavCmd.NavGuidedEnable;
                cmd.Confirmation = 1;
                cmd.Param1 = 1;
                cmd.TargetSystem = 1;
                cmd.TargetComponent = 50;

                //mavLinkSerialTransport.SendMessage(cmd);
                mavLinkSerialTransport.SendMavlinkMessage(cmd);


                messagesDock.AddMessage("UasCommandLong: " + MavLinkNet.MavCmd.NavGuidedEnable.ToString());
            }


        }

        void mapDock_RectangleDrawn(object sender, DrawnPolygonEventArgs e)
        {
            //navigator.GenerateSearchPath(e.gMapPolygon, new PointLatLng(imu.lat + mapConfigDock.GPSLat, imu.lon + mapConfigDock.GPSLon));
            //mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.Path, new GMapRoute(navigator.PointListObjectives, "SearchPath"), Color.Black);
            //pathGenerationDock.RectangleDrawn(e.DrawnRectangleEvent);
            navigator4D.GenerateSearchPath(e.gMapPolygon, new PointLatLngAlt(imu.lat + mapConfigDock.GPSLat, imu.lon + mapConfigDock.GPSLon, mapConfigDock.GPSLon + imu.alt),215);
            mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.Path, new GMapRoute(navigator4D.PointListObjectivesLatLng, "SearchPath"), Color.Black);
        }

        void mapDock_PolygonDrawn(object sender, DrawnPolygonEventArgs e)
        {
            //navigator.GenerateSearchPathPlygon(e.gMapPolygon, new PointLatLng(imu.lat + mapConfigDock.GPSLat, imu.lon + mapConfigDock.GPSLon));
            //mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.Path, new GMapRoute(navigator.PointListObjectives, "SearchPath"), Color.Black);
            //mapDock.AddRoute(2, new GMapRoute(Navigator.Rectangle, "SearchPath"), Color.Blue);
            //pathGenerationDock.RectangleDrawn(e.DrawnRectangleEvent);
            navigator4D.GenerateSearchPathPolygon(e.gMapPolygon, new PointLatLngAlt(imu.lat + mapConfigDock.GPSLat, imu.lon + mapConfigDock.GPSLon,imu.alt),215);
            mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.Path, new GMapRoute(navigator4D.PointListObjectivesLatLng, "SearchPath"), Color.Black);
        }
        void mapDock_GeofenceDrawn(object sender, DrawnGeofenceEventArgs e)
        {
            geofenceDock.NewGeofencePoints(e.GeofencePolygon);
        }
        void mapDock_PoiModified(object sender, POIEventArgs e)
        {
            navigator.PointsOfInterests = e.pointsOfInterests;
            //navigator3D.PointsOfInterests = e.pointsOfInterests;

        }
        void mapConfigDock_Load(object sender, EventArgs e)
        {
            navigator.PointListObjectives = mapConfigDock.PointListObjectives;
            navigator.CCurvature = mapConfigDock.CCurvature;
            //Navigator.ArcLength = mapConfigDock.ArcLength;
            navigator.Psi_F = mapConfigDock.Psi_F;
            mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.Path, new GMapRoute(navigator.PointListObjectives, "Route"), Color.FromArgb(127, Color.Blue));

            //navigator3D.PointListObjectives = mapConfigDock.PointListObjectives;
            //navigator3D.CCurvature = mapConfigDock.CCurvature;
            //Navigator.ArcLength = mapConfigDock.ArcLength;
            //navigator3D.Psi_F = mapConfigDock.Psi_F;
            //mapDock.AddRoute(1, new GMapRoute(navigator3D.PointListObjectives, "Route"), Color.FromArgb(127, Color.Blue));
        }

        void mapConfigDock_Refresh(object sender, EventArgs e)
        {
            MavLinkNet.UasMissionRequestList missionRequest = new MavLinkNet.UasMissionRequestList();
            missionRequest.TargetSystem = 1;
            missionRequest.TargetComponent =190;
            //mavLinkSerialTransport.MavlinkSystemId = 1;
            //mavLinkSerialTransport.MavlinkComponentId = 190;
            mavLinkSerialTransport.SendMessage(missionRequest);
            
        }
        #endregion

        #region Gui Handlers
        private void mbtSaveWorkspace_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Default to the directory which contains our content files.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation);
            string contentPath = Path.GetFullPath(relativePath);

            saveFileDialog.InitialDirectory = contentPath;

            saveFileDialog.Title = "Save Workspace";



            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                dockPanel.SaveAsXml(saveFileDialog.FileName);
            }
        }

        private void mbtLoadWorkspace_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            // Default to the directory which contains our content files.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../../../../Content");
            string contentPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = contentPath;

            fileDialog.Title = "Load workspace";

            fileDialog.Filter = "XML Files (*.xml)" +
                                "All Files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {

                for (int index = dockPanel.Contents.Count - 1; index >= 0; index--)
                {
                    if (dockPanel.Contents[index] is IDockContent)
                    {
                        IDockContent content = (IDockContent)dockPanel.Contents[index];
                        content.DockHandler.Close();
                    }
                }

                DeserializeDockContent deserializeDockContent = new DeserializeDockContent(FindDocument);
                dockPanel.LoadFromXml(fileDialog.FileName, deserializeDockContent);
                dockPanel.Update();
                //PlotDock.Show(dockPanel);
            }
        }

        private void MNAV3DSim_FormClosing(object sender, FormClosingEventArgs e)
        {
            xboxController.Dispose();
        }
        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            // Default to the directory which contains our content files.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../../../../Config");
            string contentPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = contentPath;

            fileDialog.Title = "Save Points";

            fileDialog.Filter = "XML Files (*.xml) |*.xml|" +
                                "All Files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlTextReader reader = new XmlTextReader(fileDialog.FileName);
                while (reader.Read())
                {
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element: // El nodo es un elemento. 
                                if (reader.Name == "ROLL")
                                {
                                    rollPidGainsDock.Kp = Convert.ToDouble(reader.GetAttribute("KP"));
                                    rollPidGainsDock.Ki = Convert.ToDouble(reader.GetAttribute("KI"));
                                    rollPidGainsDock.Kd = Convert.ToDouble(reader.GetAttribute("KD"));
                                    uavControlQuaternion.updateRollGains(rollPidGainsDock.Kp, rollPidGainsDock.Ki, rollPidGainsDock.Kd);

                                }
                                else if (reader.Name == "PITCH")
                                {

                                    pitchPidGainsDock.Kp = Convert.ToDouble(reader.GetAttribute("KP"));
                                    pitchPidGainsDock.Ki = Convert.ToDouble(reader.GetAttribute("KI"));
                                    pitchPidGainsDock.Kd = Convert.ToDouble(reader.GetAttribute("KD"));
                                    uavControlQuaternion.updatePitchGains(pitchPidGainsDock.Kp, pitchPidGainsDock.Ki, pitchPidGainsDock.Kd);
                                }
                                /*else if (reader.Name == "YAW")
                                {
                                    txtYawKp.Text = reader.GetAttribute("KP");
                                    txtYawKi.Text = reader.GetAttribute("KI");
                                    txtYawKd.Text = reader.GetAttribute("KD");
                                }*/
                                else if (reader.Name == "ALTITUD")
                                {

                                    altitudePidGainsDock.Kp = Convert.ToDouble(reader.GetAttribute("KP"));
                                    altitudePidGainsDock.Ki = Convert.ToDouble(reader.GetAttribute("KI"));
                                    altitudePidGainsDock.Kd = Convert.ToDouble(reader.GetAttribute("KD"));
                                    uavControlQuaternion.updateAltitudeGains(altitudePidGainsDock.Kp, altitudePidGainsDock.Ki, altitudePidGainsDock.Kd);

                                }
                                else if (reader.Name == "SPEED")
                                {
                                    airSpeedPidGainsDock.Kp = Convert.ToDouble(reader.GetAttribute("KP"));
                                    airSpeedPidGainsDock.Ki = Convert.ToDouble(reader.GetAttribute("KI"));
                                    airSpeedPidGainsDock.Kd = Convert.ToDouble(reader.GetAttribute("KD"));
                                    airSpeedPidGainsDock.FF = Convert.ToDouble(reader.GetAttribute("FF"));
                                    uavControlQuaternion.updateSpeedGains(airSpeedPidGainsDock.Kp, airSpeedPidGainsDock.Ki, airSpeedPidGainsDock.Kd, airSpeedPidGainsDock.FF);

                                }
                                else if (reader.Name == "L1")
                                {
                                    l1GainDock.sL1 = reader.GetAttribute("L1");
                                    L1 = l1GainDock.L1;
                                    l1GainDock.sTurnRadius = reader.GetAttribute("TURN_RADIUS");
                                    navigator.TurnRadius = l1GainDock.TurnRadius;
                                    navigator4D.TurnRadius = l1GainDock.TurnRadius;

                                    uavControlQuaternion.updateSpeedGains(airSpeedPidGainsDock.Kp, airSpeedPidGainsDock.Ki, airSpeedPidGainsDock.Kd, airSpeedPidGainsDock.FF);

                                }
                                /*
                                else if (reader.Name == "UDP")
                                {
                                    txtUDPIPAddress.Text = reader.GetAttribute("IP");
                                    txtUDPPort.Text = reader.GetAttribute("PORT");
                                }
                                else if (reader.Name == "TCP")
                                {
                                    txtTCPIPAddress.Text = reader.GetAttribute("IP");
                                    txtTCPPort.Text = reader.GetAttribute("PORT");
                                }*/
                                else if (reader.Name == "GPS")
                                {
                                    mapConfigDock.InitLat = Convert.ToDouble(reader.GetAttribute("LAT"));
                                    mapConfigDock.InitLon = Convert.ToDouble(reader.GetAttribute("LON"));
                                    mapConfigDock.InitAlt = Convert.ToDouble(reader.GetAttribute("ALT"));
                                    mapConfigDock.DiffLatLon = Convert.ToBoolean(reader.GetAttribute("DIFFERENTIAL"));
                                }
                                else if (reader.Name == "LYAPGAINS")
                                {
                                    lyapunovGainsDock.Kdelta = Convert.ToDouble(reader.GetAttribute("KDELTA"));
                                    lyapunovGainsDock.Ks = Convert.ToDouble(reader.GetAttribute("KS"));
                                    lyapunovGainsDock.Kw1 = Convert.ToDouble(reader.GetAttribute("KW1"));
                                    lyapunovGainsDock.Kphi = Convert.ToDouble(reader.GetAttribute("KPHI"));
                                    lyapunovGainsDock.Psi_a = Convert.ToDouble(reader.GetAttribute("PSI_A"));

                                    Kdelta = lyapunovGainsDock.Kdelta;
                                    Ks = lyapunovGainsDock.Ks;
                                    Kw1 = lyapunovGainsDock.Kw1;
                                    Kphi = lyapunovGainsDock.Kphi;
                                    Psi_a = lyapunovGainsDock.Psi_a;

                                }
                                else if (reader.Name == "LYAP3DGAINS")
                                {
                                    lyapunov3DGainsDock.Kx = Convert.ToDouble(reader.GetAttribute("KX"));
                                    lyapunov3DGainsDock.Ky = Convert.ToDouble(reader.GetAttribute("KY"));
                                    lyapunov3DGainsDock.Kz = Convert.ToDouble(reader.GetAttribute("KZ"));
                                    lyapunov3DGainsDock.Kdelta = Convert.ToDouble(reader.GetAttribute("KDELTA"));
                                    lyapunov3DGainsDock.Psi_a = Convert.ToDouble(reader.GetAttribute("PSI_A"));

                                    lyap3D.UpdateGains(lyapunov3DGainsDock.Kx, lyapunov3DGainsDock.Ky, lyapunov3DGainsDock.Kz, lyapunov3DGainsDock.Kdelta, lyapunov3DGainsDock.Psi_a);

                                }

                                break;
                        }
                    }
                }
            }
        }
        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            // Default to the directory which contains our content files.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../../../../Config");
            string contentPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = contentPath;

            fileDialog.Title = "Save Config";

            fileDialog.Filter = "XML Files (*.xml) |*.xml|" +
                                "All Files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlTextWriter writer = new XmlTextWriter(fileDialog.FileName, Encoding.UTF8);

                writer.WriteStartDocument();
                writer.WriteStartElement("CONFIG"); // CONFIG

                writer.WriteStartElement("GAINS");  // GAINS
                writer.WriteStartElement("ROLL");
                writer.WriteAttributeString("KP", rollPidGainsDock.Kp.ToString());
                writer.WriteAttributeString("KI", rollPidGainsDock.Ki.ToString());
                writer.WriteAttributeString("KD", rollPidGainsDock.Kd.ToString());
                writer.WriteEndElement();
                writer.WriteStartElement("PITCH");
                writer.WriteAttributeString("KP", pitchPidGainsDock.Kp.ToString());
                writer.WriteAttributeString("KI", pitchPidGainsDock.Ki.ToString());
                writer.WriteAttributeString("KD", pitchPidGainsDock.Kd.ToString());
                //writer.WriteEndElement();
                //writer.WriteStartElement("YAW");
                //writer.WriteAttributeString("KP", txtYawKp.Text);
                //writer.WriteAttributeString("KI", txtYawKi.Text);
                //writer.WriteAttributeString("KD", txtYawKd.Text);
                writer.WriteEndElement();
                writer.WriteStartElement("ALTITUD");
                writer.WriteAttributeString("KP", altitudePidGainsDock.Kp.ToString());
                writer.WriteAttributeString("KI", altitudePidGainsDock.Ki.ToString());
                writer.WriteAttributeString("KD", altitudePidGainsDock.Kd.ToString());
                writer.WriteStartElement("SPEED");
                writer.WriteAttributeString("KP", airSpeedPidGainsDock.Kp.ToString());
                writer.WriteAttributeString("KI", airSpeedPidGainsDock.Ki.ToString());
                writer.WriteAttributeString("KD", airSpeedPidGainsDock.Kd.ToString());
                writer.WriteEndElement();
                writer.WriteStartElement("LYAPGAINS");
                writer.WriteAttributeString("KDELTA", lyapunovGainsDock.Kdelta.ToString());
                writer.WriteAttributeString("KS", lyapunovGainsDock.Ks.ToString());
                writer.WriteAttributeString("KW1", lyapunovGainsDock.Kw1.ToString());
                writer.WriteAttributeString("KPHI", lyapunovGainsDock.Kphi.ToString());
                writer.WriteAttributeString("PSI_A", lyapunovGainsDock.Psi_a.ToString());
                writer.WriteEndElement();
                writer.WriteStartElement("L1");
                writer.WriteAttributeString("L1", l1GainDock.L1.ToString());
                writer.WriteAttributeString("TURN_RADIUS", l1GainDock.TurnRadius.ToString());
                writer.WriteEndElement();
                writer.WriteEndElement();// END GAINS

                //writer.WriteStartElement("UDP");  // UDP
                //writer.WriteAttributeString("IP", txtUDPIPAddress.Text);
                //writer.WriteAttributeString("PORT", txtUDPPort.Text);
                //writer.WriteEndElement(); // END UDP

                //writer.WriteStartElement("TCP");  // TCP
                //writer.WriteAttributeString("IP", txtTCPIPAddress.Text);
                //writer.WriteAttributeString("PORT", txtTCPPort.Text);
                //writer.WriteEndElement(); // END TCP

                writer.WriteStartElement("GPS");  // GPS
                writer.WriteAttributeString("LAT", mapConfigDock.InitLat.ToString());
                writer.WriteAttributeString("LON", mapConfigDock.InitLon.ToString());
                writer.WriteAttributeString("ALT", mapConfigDock.InitAlt.ToString());
                writer.WriteAttributeString("DIFFERENTIAL", mapConfigDock.UseGpsDifferential.ToString());
                writer.WriteEndElement(); // END GPS

                writer.WriteEndElement(); // END CONFIG
                writer.WriteEndDocument();

                writer.Close();

            }
        }


        #endregion


        #region Show panels


        private void mbtMapInfo_Click(object sender, EventArgs e)
        {
            mapInfoDock.Show(dockPanel);
        }

        private void mbtShowMap_Click(object sender, EventArgs e)
        {
            mapDock.Show(dockPanel);
        }

        private void mbtMapConfig_Click(object sender, EventArgs e)
        {
            mapConfigDock.Show(dockPanel);
        }

        private void mbtRawData_Click(object sender, EventArgs e)
        {
            rawDataDock.Show(dockPanel);
        }

        private void mbtAddPlot_Click(object sender, EventArgs e)
        {
            
            string outString = "";
            if (InputBox.Show("Add New Plot", "Name of the new plot", ref outString) == DialogResult.OK)
            { 
                Docks.Plot newPlot = new Docks.Plot();
                newPlot.PlotVariables = PopulateVariables();
                
                //PopulateVariable(newPlot);
                newPlot.PopulateMenu();
                newPlot.Text = outString;
                newPlot.Show(dockPanel);
                PlotDocks.Add(outString, newPlot);

                // Add item to menu
                foreach(ToolStripMenuItem toolStripMenuItem in menuStrip1.Items)
                {
                    if(toolStripMenuItem.Text == "VIEW")
                    {
                        foreach (ToolStripMenuItem toolStripSubMenuItem in toolStripMenuItem.DropDownItems)
                        {
                            if(toolStripSubMenuItem.Text == "Data")
                            {
                                ToolStripItem subItem = new ToolStripMenuItem();
                                subItem.Text = outString;
                                subItem.ForeColor = Color.White;
                                subItem.Click += subItem_Click;
                                toolStripSubMenuItem.DropDownItems.Add(subItem);
                            }
                        }
                        
                    }

                }
               
               
            }
        }

        private Dictionary<string, double> PopulateVariables()
        {
            Dictionary<string, double> pVariables = new Dictionary<string,double>();
            pVariables = imu.PopulateVariables();
            lock(lockVariables)
            {
                foreach (KeyValuePair<string, double> variable in plotVariables)
                {
                    if (!pVariables.ContainsKey(variable.Key))
                        pVariables.Add(variable.Key, variable.Value);
                    else
                        pVariables[variable.Key] = variable.Value;
                }
            }
            
            return pVariables;
        }

        private void AddPlotVariable(String Key, double Value)
        {
            lock (lockVariables)
            {
                if (plotVariables.ContainsKey(Key))
                    plotVariables[Key] = Value;
                else
                    plotVariables.Add(Key, Value);
            }
            

        }

        void subItem_Click(object sender, EventArgs e)
        {
            ToolStripItem toolStrip = sender as ToolStripItem;
            PlotDocks[toolStrip.Text].Show(dockPanel);
            
        }

        private void mbtPlotPitchQAy_Click(object sender, EventArgs e)
        {
            //plotPitchQAyDock.Show(dockPanel);
        }

        private void mbtPlotYawRAz_Click(object sender, EventArgs e)
        {
            //plotYawRAzDock.Show(dockPanel);
        }

        private void mbtAirspeed_Click(object sender, EventArgs e)
        {
            airSpeedPidGainsDock.Show(dockPanel);
        }

        private void mbtAttitudIndicator_Click(object sender, EventArgs e)
        {
            AttitudeIndicatorDock.Show(dockPanel);
        }

        private void mbtAltimeter_Click(object sender, EventArgs e)
        {
            altimeterDock.Show(dockPanel);
        }

        private void mbtCompass_Click(object sender, EventArgs e)
        {
            compassDock.Show(dockPanel);
        }

        private void mbtVerticalSpeed_Click(object sender, EventArgs e)
        {
            vertiaclSpeedDock.Show(dockPanel);
        }

        private void mbtRollPIDGains_Click(object sender, EventArgs e)
        {
            rollPidGainsDock.Show(dockPanel);
        }

        private void mbtPitchPIDGains_Click(object sender, EventArgs e)
        {
            pitchPidGainsDock.Show(dockPanel);
        }

        private void mbtAirSpeedPIDGains_Click(object sender, EventArgs e)
        {
            airSpeedPidGainsDock.Show(dockPanel);
        }

        private void mbtAltitudPIDGains_Click(object sender, EventArgs e)
        {
            altitudePidGainsDock.Show(dockPanel);
        }

        private void mbtL1PIDGains_Click(object sender, EventArgs e)
        {
            l1GainDock.Show(dockPanel);
        }

        private void mbtLyapunovGains_Click(object sender, EventArgs e)
        {
            lyapunovGainsDock.Show(dockPanel);
        }
        private void mbtLyapunov3DGains_Click(object sender, EventArgs e)
        {
            lyapunov3DGainsDock.Show(dockPanel);
        }

        private void mbtManualControl_Click(object sender, EventArgs e)
        {
            manualControlDock.Show(dockPanel);
        }

        private void mbtControlSelection_Click(object sender, EventArgs e)
        {
            controlSelectionDock.Show(dockPanel);
        }

        private void mbtUDP_Click(object sender, EventArgs e)
        {
            udpConfigDock.Show(dockPanel);
        }

        private void mbtSerialPort_Click(object sender, EventArgs e)
        {
            serialPortConfigDock.Show(dockPanel);
        }

        private void mbtPlotScale_Click(object sender, EventArgs e)
        {


        }

        private void tsbCRRCSim_Click(object sender, EventArgs e)
        {
            CRRCSimDock.Show(dockPanel);
        }

        private void mbtPathGeneration_Click(object sender, EventArgs e)
        {
            pathGenerationDock.Show(dockPanel);
        }
        private void mbtMissionControl_Click(object sender, EventArgs e)
        {
            missionControlDock.Show(dockPanel);
        }

        private void PlaybackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playbackDock.Show(dockPanel);
        }

        #endregion

        #region XBox Controller
        void xboxController_StateChanged(object sender, EventArgs e)
        {
            UpdateControllerState();
        }
        private void UpdateControllerState()
        {
            //Get the new gamepad state and save the old state.
            //JoystickState state = 

            /*threadSafe.SetControlPropertyThreadSafe(buttonA, "Checked", xboxController.A);
            threadSafe.SetControlPropertyThreadSafe(buttonB, "Checked", xboxController.B);
            threadSafe.SetControlPropertyThreadSafe(buttonX, "Checked", xboxController.X);
            threadSafe.SetControlPropertyThreadSafe(buttonY, "Checked", xboxController.Y);
            threadSafe.SetControlPropertyThreadSafe(buttonLeftShoulder, "Checked", xboxController.LeftTrigger);
            threadSafe.SetControlPropertyThreadSafe(buttonRightShoulder, "Checked", xboxController.RightTrigger);
            threadSafe.SetControlPropertyThreadSafe(buttonStart, "Checked", xboxController.Start);
            threadSafe.SetControlPropertyThreadSafe(buttonBack, "Checked", xboxController.Back);
            threadSafe.SetControlPropertyThreadSafe(buttonLeftStick, "Checked", xboxController.LeftStick);
            threadSafe.SetControlPropertyThreadSafe(buttonRightStick, "Checked", xboxController.RightStick);

            threadSafe.SetControlPropertyThreadSafe(buttonUp, "Checked", xboxController.Up);
            threadSafe.SetControlPropertyThreadSafe(buttonDown, "Checked", xboxController.Down);
            threadSafe.SetControlPropertyThreadSafe(buttonLeft, "Checked", xboxController.Left);
            threadSafe.SetControlPropertyThreadSafe(buttonRight, "Checked", xboxController.Right);
            */



            //Update the position of the thumb sticks
            //since the thumbsticks can return a number between -1.0 and +1.0 I had to shift (add 1.0)
            //and scale (mutiplication by 100/2, or 50) to get the numbers to be in the range of 0-100
            //for the progress bar
            double offset = 50;

            // Xbox controller
            //threadSafe.SetControlPropertyThreadSafe(this.vsAileron, "Value", -(int)(xboxController.X2 * offset));
            //threadSafe.SetControlPropertyThreadSafe(this.vsElevator, "Value", -(int)(xboxController.Y1 * offset));
            //threadSafe.SetControlPropertyThreadSafe(this.vsThrottle, "Value", - (int)((xboxController.Y2+1) * 100/2));
            //threadSafe.SetControlPropertyThreadSafe(this.vsRudder, "Value", -(int)(xboxController.X1 * offset));

            // Joystick
            // Only update if no serial comunication is used
            if (!com.UseSerialCom)
            {


                manualControlDock.Rudder = -(int)(xboxController.X1 * offset);
                manualControlDock.Elevator = -(int)(xboxController.Y1 * offset);
                manualControlDock.Throttle = -(int)((xboxController.Y2 + 1) * 100 / 2);
                manualControlDock.Aileron = -(int)(xboxController.X2 * offset);

                if (xboxController.Start)
                    //btnStart_Click(new object(), new EventArgs());
                    if (xboxController.Back)
                        //btnStop_Click(new object(), new EventArgs());
                        if (xboxController.LeftTrigger)
                        {
                            /*int index = (int)threadSafe.GetControlPropertyThreadSafe(tabControl1, "SelectedIndex");
                            if (index > 0)
                                threadSafe.SetControlPropertyThreadSafe(tabControl1, "SelectedIndex", index - 1);
                             */
                        }
                if (xboxController.RightTrigger)
                {
                    /*int index = (int)threadSafe.GetControlPropertyThreadSafe(tabControl1, "SelectedIndex");
                    int tabCount = (int)threadSafe.GetControlPropertyThreadSafe(tabControl1, "TabCount");

                    if (index < tabCount - 1)
                        threadSafe.SetControlPropertyThreadSafe(tabControl1, "SelectedIndex", index + 1);
                     * */
                }

                if (xboxController.Y)
                    controlSelectionDock.Manual = true;
                if (xboxController.X)
                    controlSelectionDock.PitchRoll = true;
                if (xboxController.A)
                    controlSelectionDock.Altitud = true;
                if (xboxController.B)
                    controlSelectionDock.Guidance = true;
                //controlSelectionDock.Heading = true;

            }
        }

        #endregion



        private void UpdateGUI()
        {
            int count = 0;
            //plotRollPAxDock.Value = 0;
            //plotRollPAxDock.DataValue = 0;

            //plotPitchQAyDock.Value = 0;
            //plotPitchQAyDock.DataValue = 0;

            //plotYawRAzDock.Value = 0;
            //plotYawRAzDock.DataValue = 0;
            try
            {
                do
                {


                    // Update AHRS
                    //Vector3 angles = QuaternionToEuclidean(ahrs.Quaternion);
                    //threadSafe.SetControlPropertyThreadSafe(txtPhi, "Text", angles.X.ToString());
                    //threadSafe.SetControlPropertyThreadSafe(txtTheta, "Text", ahrs.Quaternion.Y.ToString());
                    //threadSafe.SetControlPropertyThreadSafe(txtPsi, "Text", ahrs.Quaternion.Z.ToString());

                    

                    //rawDataDock.U = ahrs.u.ToString();            Estas variables no las manda el simulador
                    //rawDataDock.V = ahrs.v.ToString();
                    //rawDataDock.W = ahrs.w.ToString();

                    // Update 3D Model                              Hay que agregar el modelo en 3d.
                    // modelViewerControl.Rotation = ahrs.Rotation;

                    // Update communication
                    //double freq = 1 / (periodo.TotalSeconds);
                    //threadSafe.SetControlPropertyThreadSafe(txtCommFreq, "Text", "COMM: " + (freq).ToString());
                    //ahrs.SamplePeriod = (float)duration.TotalSeconds;

                   


                    //horizonInstrumentControl1.SetAttitudeIndicatorParameters(trackPitchAngle.Value, trackBarRollAngle.Value);

                    // add a point to scope

                    int scale = plotScaleDock.Scale;
                    if (count > scale)
                    {
                        //plotRollPAxDock.Value = count - scale;
                        //plotRollPAxDock.DataValue = count - scale;

                        //plotPitchQAyDock.Value = count - scale;
                        //plotPitchQAyDock.DataValue = count - scale;

                        //plotYawRAzDock.Value = count - scale;
                        //plotYawRAzDock.DataValue = count - scale;

                    }
                    count++;

                    foreach(var plot in PlotDocks )
                    {
                        
                        plot.Value.PlotVariables = PopulateVariables();
                        plot.Value.addPoint();
                    }
                    //PlotDock.PlotVariables = imu.PopulateVariable();
                    //PlotDock.addPoint();

                    /*plotRollPAxDock.AddYPoint(0, imu.phi);
                    plotRollPAxDock.AddYPoint(1, imu.p);
                    plotRollPAxDock.AddYPoint(2, imu.ax);
                    plotRollPAxDock.AddYPoint(3, imu.mx);
                    plotRollPAxDock.AddYPoint(4, actuators.Aileron);
                    */

                    // Es necesario encapsular los controladores en una sola clase y agregar los controladores en esa clase.
                    // Y asi obtener el setpoint y el error del mismo lugar. no importando el controlador utilizado
                    /*if ((bool)threadSafe.GetControlPropertyThreadSafe(rbQuadrotor, "Checked"))
                    {
                        scPhi.Channels[5].Data.AddYPoint(quadrotorControl.getRollSetPoint());
                        scPhi.Channels[6].Data.AddYPoint(quadrotorControl.getRollError());
                    }
                    else
                    {
                        scPhi.Channels[5].Data.AddYPoint(uavControlQuaternion.getRollSetPoint());
                        scPhi.Channels[6].Data.AddYPoint(uavControlQuaternion.getRollError());
                    }
                     * **/

                    /*plotPitchQAyDock.AddYPoint(0, imu.the);
                    plotPitchQAyDock.AddYPoint(1, imu.q);
                    plotPitchQAyDock.AddYPoint(2, imu.ay);
                    plotPitchQAyDock.AddYPoint(3, imu.my);
                    plotPitchQAyDock.AddYPoint(4, actuators.Elevator);
                    */
 
                    /* lo mismo de arriba CORREGIR
                    if ((bool)threadSafe.GetControlPropertyThreadSafe(rbQuadrotor, "Checked"))
                    {
                        scThe.Channels[5].Data.AddYPoint(quadrotorControl.getPitchSetPoint());
                        scThe.Channels[6].Data.AddYPoint(quadrotorControl.getPitchError());
                    }
                    else
                    {
                        scThe.Channels[5].Data.AddYPoint(uavControlQuaternion.getPitchSetPoint());
                        scThe.Channels[6].Data.AddYPoint(uavControlQuaternion.getPitchError());
                    }*/

                    /*plotYawRAzDock.AddYPoint(0, imu.psi);
                    plotPitchQAyDock.AddYPoint(1, imu.r);
                    plotPitchQAyDock.AddYPoint(2, imu.az);
                    plotPitchQAyDock.AddYPoint(3, imu.mz);
                    plotPitchQAyDock.AddYPoint(4, actuators.Rudder);
                    */
 

                    /*
                    //scPsi.Channels[5].Data.AddYPoint(uavControlQuaternion.getYawSetPoint());
                    scPsi.Channels[5].Data.AddYPoint(Convert.ToDouble(Psi_f[currentObjective]));
                    scPsi.Channels[6].Data.AddYPoint(uavControlQuaternion.getYawError());
                    //scPsi.Channels[7].Data.AddYPoint(uavControlQuaternion.getYawSetPoint()-ahrs.psi);

                     * */
                    if (controlSelectionDock.Altitud)
                    {

                        double dElevator1 = Convert.ToDouble(manualControlDock.sElevator1);
                        double dElevator = Convert.ToDouble(manualControlDock.sElevator);
                        manualControlDock.sElevator1 = (dElevator1 + dElevator).ToString();

                        /*
                        double dRudder1 = Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtRudder1, "Text"));
                        double dRudder = Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtRudder, "Text"));
                        double dRudder2 = dRudder1 + dRudder;
                        if (dRudder2 > 2 * Math.PI)
                            dRudder2 = dRudder2 - 2 * Math.PI;
                        if (dRudder2 < -2 * Math.PI)
                            dRudder2 = dRudder2 + 2 * Math.PI;
                        threadSafe.SetControlPropertyThreadSafe(txtRudder1, "Text", (dRudder2).ToString());
                         * */

                    }

                    if (controlSelectionDock.L1Controller || controlSelectionDock.Guidance || controlSelectionDock.Altitud)
                    {

                      /*  double dThrottle1 = Convert.ToDouble(manualControlDock.sThrottle);
                        manualControlDock.sThrottle1 = (dThrottle1 * 350).ToString();
                        if(controlSelectionDock.Guidance)
                        {
                            manualControlDock.sElevator1 = navigator3D.CurrentObjective.Alt.ToString();
                        }*/
                    }

                    

                    if (com.UseSerialCom)
                    {
                        DateTime nowTime1 = DateTime.Now;
                        if ((nowTime1 - lastTime).Seconds > 1)
                        {
                            Byte[] data = new Byte[3];
                            // Header
                            data[0] = 0x55;
                            data[1] = 0x55;
                            data[2] = 0x55; // Send update

                            com.Send(data);

                        }
                    }

                    Thread.Sleep(10);



                } while (!closeRequested);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception e: " + e.ToString());
            }
            Console.WriteLine("Salio del thread UpdateGui");

        }
        private void UpdateMap()
        {
            int time = 0;
            try
            {
                do
                {
                    if (time != imu.Time)
                    {
                        // Update GPS TextBox
                        mapInfoDock.Lat = imu.lat + mapConfigDock.GPSLat;
                        mapInfoDock.Lon = imu.lon + mapConfigDock.GPSLon;
                        mapInfoDock.Alt = imu.alt + mapConfigDock.GPSAlt;

                        // Update Map
                            mapDock.UpdateMarker(new PointLatLng(mapInfoDock.Lat, mapInfoDock.Lon), ((float)(imu.psi * 180 / Math.PI)));
                        if (controlSelectionDock.L1Controller || controlSelectionDock.Guidance || controlSelectionDock.Lyap3D)
                        {
                            //mapDock.UpdateMarkerCircle(navigator.CurrentObjective);
                            mapDock.UpdateMarkerCircle(navigator4D.CurrentObjective.GetPointLatLng());
                        }
                        time = imu.Time;
                        
                    }
                    Thread.Sleep(200);

                } while (!closeRequested);
            }
            catch (Exception e)
            {
                Console.WriteLine("UpdateAvionicsInstruments: Exception e: " + e.ToString());
            }
            Console.WriteLine("Salio del thread UpdateMap");

        }
        private void UpdateRawData()
        {
            int time = 0;
            try
            {
                do
                {
                    if (time != imu.Time)
                    {
                        // Update IMU TextBox
                        rawDataDock.Ax = imu.ax;
                        rawDataDock.Ay = imu.ay;
                        rawDataDock.Az = imu.az;

                        rawDataDock.P = imu.p;
                        rawDataDock.Q = imu.q;
                        rawDataDock.R = imu.r;

                        rawDataDock.Mx = imu.mx;
                        rawDataDock.My = imu.my;
                        rawDataDock.Mz = imu.mz;

                        rawDataDock.Phi = imu.phi;
                        rawDataDock.Theta = imu.the;
                        rawDataDock.Psi = imu.psi;

                        rawDataDock.Lidar = imu.Lidar;
                        time = imu.Time;

                    }
                    Thread.Sleep(250);

                } while (!closeRequested);
                    }
            catch (Exception e)
            {
                Console.WriteLine("UpdateAvionicsInstruments: Exception e: " + e.ToString());
            }
            Console.WriteLine("Salio del thread UpdateRawData");

        }
        private void UpdateAvionicsInstruments()
        {
            int time = 0;
            try
            {
                do
                {
                    if (time != imu.Time)
                    {
                        // Update Avionics Controls
                        AttitudeIndicatorDock.Pitch = imu.the * 180 / Math.PI;
                        AttitudeIndicatorDock.Roll = -imu.phi * 180 / Math.PI;
                        altimeterDock.Altitud = (int)(imu.alt * 3.2808399); // Convertido a pies, cambiar y poner en metros

                        int iPsi = (int)(imu.psi * 180 / Math.PI);
                        if (imu.psi < 0)
                            iPsi = (int)(imu.psi * 180 / Math.PI) + 360;

                        compassDock.Heading = iPsi;
                        airSpeedDock.AirSpeed = (int)(imu.pt * 10);

                        time = imu.Time;

                        if(controlSelectionDock.HilSimulator)
                        {
                            manualControlDock.sAileron = actuators.Aileron.ToString();
                            manualControlDock.sThrottle = actuators.Throttle.ToString();
                            manualControlDock.sRudder = actuators.Rudder.ToString();
                            manualControlDock.sElevator = actuators.Elevator.ToString();
                        }

                    }

                    Thread.Sleep(100);

                } while (!closeRequested);
            }
            catch (Exception e)
            {
                Console.WriteLine("UpdateAvionicsInstruments: Exception e: " + e.ToString());
            }
            Console.WriteLine("Salio del thread UpdateAvionicsInstruments");

        }
        private void UpdateIMU()
        {
            bool firstRun = true;
            int cont = 0;
            DateTime period = DateTime.Now;
            DateTime lastPeriod = DateTime.Now;
            double dt = 0;
            do
            {
                try
                {
                    cont++;
                    TimeSpan d = DateTime.Now - period;
                    int mil = d.Milliseconds + d.Seconds * 1000;
                    dt = ((double)mil) / 1000;
                    period = DateTime.Now;
                    if (period.Second != lastPeriod.Second)
                    {
                        threadSafe.SetStatusStripPropertyThreadSafe(statusStrip1, slIMUFreq, "Text", "Freq: " + cont.ToString());
                        lastPeriod = period;
                        cont = 0;
                    }

                    if (!firstRun)
                    {
                        if (controlSelectionDock.HilSimulator)
                        {
                            if (udpConfigDock.Protocol == "MNAV")
                            {
                                byte[] received_data = com.Receive();
                                imu.decode(received_data);
                            }
                            else
                            {
                                mavLinkUdpTransport.ReceiveMavlinkMsg();
                            }
                            imu.MavlinkEncode();
                            mavLinkSerialTransport.SendMessage(imu.HilSensor);
                            mavLinkSerialTransport.SendMessage(imu.HilStateQuaternion);
                            mavLinkSerialTransport.SendMessage(imu.HilGps);





                        }
                        else
                        {
                            if (!com.UseSerialCom)
                            {

                                if (udpConfigDock.Protocol == "MNAV")
                                {
                                    byte[] received_data = com.Receive();
                                    imu.decode(received_data);

                                }

                                else
                                {
                                    mavLinkUdpTransport.ReceiveMavlinkMsg();
                                }


                            }
                        }

                    }
                    else firstRun = false;
                }
                catch (Exception e)
                {

                }
            } while (!closeRequested);
        }

        private void UpdateIMUPlayback()
        {
            bool firstRun = true;
            int cont = 0;
            DateTime Start = DateTime.Now;
            DateTime lastPeriod = DateTime.Now;
            double dt = 0;
            do
            {
                try
                {
                    TimeSpan d = DateTime.Now - Start;
                    int mil = d.Milliseconds + d.Seconds * 1000 ;
                    double microseconds = d.TotalMilliseconds*1000;
                    

                    dt = ((double)mil) / 1000;

                    imu.decode(playbackDock.GetImu(microseconds));

                    //lastPeriod = DateTime.Now;
                    Thread.Sleep(50);
                    
                }
                catch (Exception e)
                {
                    controlSelectionDock.StopButtonClick();
                    //Console.WriteLine("UpdateIMUPlayback: Exception e: " + e.ToString());
                }
            } while (!closeRequested);
            Console.WriteLine("Salio del thread UpdateIMUPlayback");
        }

        // L1 Controller
        double L1 = 0;
        double Ks;
        double Kw1;
        double Kdelta;
        double Kphi;

        // Lyapunov 3D Controller
        Controller.ControllerLyap3D lyap3D = new Controller.ControllerLyap3D();
        double Psi_a;
        private void Controller()
        {
            try
            {
                int cont = 0;
                DateTime period = DateTime.Now;
                DateTime lastPeriod = DateTime.Now;
                double dt = 0;
                com.CreatConnectionUDP();
                
                byte[] actuatorEncode;

                // LyapController
                Microsoft.Xna.Framework.Vector3 v1;
                Microsoft.Xna.Framework.Vector3 v2;
                Microsoft.Xna.Framework.Vector3 v2_aux;
                Microsoft.Xna.Framework.Vector3 v3;
                float v1v2;
                double acoseta;
                double eta;
                double us;
                double lat;
                double lon;

                // Lyap Guidance variables
                double es = 0;
                double ed = 0;
                double s_dot = 0;
                double V = 17.5;  // Constant Velocicty
                double psi_tilde = 0;
                double omega_d = 0;
                double prevOmega_d = 0;
                double omega_tilde = 0;
                double old_control_u = 0;
                double control_u = 0;
                double x_enu = 0;
                double y_enu = 0;
                double x_enu_aircraft = 0;
                double y_enu_aircraft = 0;

                

                do
                {
                    if (imu.Updated)
                    {
                        cont++;
                        TimeSpan d = DateTime.Now - period;
                        int mil = d.Milliseconds + d.Seconds * 1000;
                        dt = ((double)mil) / 1000;
                        period = DateTime.Now;
                        if (period.Second != lastPeriod.Second)
                        {
                            threadSafe.SetStatusStripPropertyThreadSafe(statusStrip1, slControlerFreq, "Text", "Freq: " + cont.ToString());
                            lastPeriod = period;
                            cont = 0;
                        }





                        if (!controlSelectionDock.HilSimulator)
                        {
                            actuators.Aileron = Convert.ToDouble(manualControlDock.sAileron);
                            actuators.Elevator = Convert.ToDouble(manualControlDock.sElevator);
                            actuators.Rudder = Convert.ToDouble(manualControlDock.sRudder);
                            actuators.Throttle = Convert.ToDouble(manualControlDock.sThrottle);
                        }


                        if (controlSelectionDock.PitchRoll)
                        {

                            uavControlQuaternion.updateRollSetPoint(Convert.ToDouble(manualControlDock.sAileron) * Math.PI / 2);
                            uavControlQuaternion.updatePitchSetPoint(-Convert.ToDouble(manualControlDock.sElevator) * Math.PI / 2);
                            uavControlQuaternion.updateRollValue(imu.phi);
                            uavControlQuaternion.updatePitchValue(imu.the);
                            uavControlQuaternion.updateDerivatives(imu.p, imu.q, imu.r);
                            uavControlQuaternion.ControlStep(dt);
                            actuators.Aileron = uavControlQuaternion.getRollControl();
                            actuators.Elevator = -uavControlQuaternion.getPitchControl();
                        }
                        else if (controlSelectionDock.Altitud)
                        {
                            //uavControlQuaternion.updateYawSetPoint(Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtRudder1, "Text")));
                            //uavControlQuaternion.updateYawValue(ahrs.psi);
                            uavControlQuaternion.updateRollSetPoint(Convert.ToDouble(manualControlDock.sAileron) * Math.PI / 2);
                            uavControlQuaternion.updateAltitudSetPoint(Convert.ToDouble(manualControlDock.sElevator1));
                            uavControlQuaternion.updateRollValue(imu.phi);
                            uavControlQuaternion.updateAltitudValue(imu.alt);
                            uavControlQuaternion.updatePitchValue(imu.the);
                            uavControlQuaternion.updateSpeedValue(imu.pt * 10);
                            uavControlQuaternion.updateSpeedSetPoint(Convert.ToDouble(manualControlDock.sThrottle1));
                            uavControlQuaternion.updateDerivatives(imu.p, imu.q, imu.r);
                            uavControlQuaternion.ControlStep(dt);
                            actuators.Aileron = uavControlQuaternion.getRollControl();
                            actuators.Elevator = -uavControlQuaternion.getPitchControl();
                            //actuators.Rudder = uavControlQuaternion.getYawControl();
                            //if(testCount%100==0)
                            actuators.Throttle = uavControlQuaternion.getSpeedControl();
                            AddPlotVariable("SpeedControl", uavControlQuaternion.getSpeedControl());
                        }
                        else if (controlSelectionDock.L1Controller)
                        {
                            lat = imu.lat + mapConfigDock.GPSLat;
                            lon = imu.lon + mapConfigDock.GPSLon;


                            if (navigator.ProximityCheck(L1, imu.lat + mapConfigDock.GPSLat, imu.lon + mapConfigDock.GPSLon, imu.psi))
                            {
                                if (navigator.IsTurn180)
                                {
                                    mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.InitialPath, new GMapRoute(navigator.PointListObjectives, "Route"), Color.FromArgb(127, Color.Green));
                                }
                            }

                            v1 = new Microsoft.Xna.Framework.Vector3((float)imu.ve, (float)imu.vn, 0);
                            v2 = new Microsoft.Xna.Framework.Vector3((float)(navigator.CurrentObjective.Lng - lon), (float)(navigator.CurrentObjective.Lat - lat), 0);
                            v2_aux = new Microsoft.Xna.Framework.Vector3((float)(navigator.CurrentObjective.Lng - lon), (float)(navigator.CurrentObjective.Lat - lat), 0);


                            double v1xv2x = imu.vn * (navigator.CurrentObjective.Lng - lon);
                            double v1yv2y = imu.ve * (navigator.CurrentObjective.Lat - lat);

                            v1.Normalize();
                            v2.Normalize();

                            v3 = Microsoft.Xna.Framework.Vector3.Cross(v1, v2);


                            v1v2 = v1.X * v2.X + v1.Y * v2.Y;
                            acoseta = Math.Acos(v1v2);
                            eta = acoseta * Math.Sign(v3.Z) - Math.PI;

                            //theta = Math.Atan2(pointListObjectives[currentObjective].Lng - lon, pointListObjectives[currentObjective].Lat - lat);
                            //eta =  theta + ahrs.psi;

                            us = Math.Atan2(2 * Math.Pow(17, 2) * Math.Sin(eta), 9.81 * L1);


                            //us *= -1;
                            double sat = .78;
                            if (us > sat)
                                us = sat;
                            if (us < -sat)
                                us = -sat;



                            //uavControlQuaternion.updateYawValue(ahrs.psi);
                            //uavControlQuaternion.updateHeadingValue(ahrs.psi);
                            //uavControlQuaternion.updateYawSetPoint(us);
                            //uavControlQuaternion.updateRollSetPoint(Convert.ToDouble(threadSafe.GetControlPropertyThreadSafe(txtAileron, "Text")) * Math.PI / 2);
                            uavControlQuaternion.updateRollSetPoint(us);
                            uavControlQuaternion.updateAltitudSetPoint(Convert.ToDouble(manualControlDock.sElevator1));
                            uavControlQuaternion.updateRollValue(imu.phi);
                            uavControlQuaternion.updateAltitudValue(imu.alt);
                            uavControlQuaternion.updatePitchValue(imu.the);
                            uavControlQuaternion.updateSpeedValue(imu.pt * 10);
                            uavControlQuaternion.updateSpeedSetPoint(Convert.ToDouble(manualControlDock.sThrottle1));
                            uavControlQuaternion.updateDerivatives(imu.p, imu.q, imu.r);
                            uavControlQuaternion.ControlStep(dt);
                            actuators.Aileron = uavControlQuaternion.getRollControl();
                            actuators.Elevator = -uavControlQuaternion.getPitchControl();
                            //actuators.Rudder = uavControlQuaternion.getHeadingControl() * .0;
                            actuators.Throttle = uavControlQuaternion.getSpeedControl();
                            




                        }
                        else if (controlSelectionDock.Guidance)
                        {
                            lat = imu.lat + mapConfigDock.GPSLat;
                            lon = imu.lon + mapConfigDock.GPSLon;
                            //V = imu.pt;
                            // Get error
                            //Vector2 vel = new Vector2((float)imu.ve, (float)imu.vn);

                            //Point3D point1 = mapTools.Geodetic2ENU(new PointLatLng(lat, lon), new PointLatLng(mapConfigDock.GPSLat, mapConfigDock.GPSLon));
                            //Point3D qoint1 = mapTools.Geodetic2ENU(navigator.CurrentObjective, new PointLatLng(mapConfigDock.GPSLat, mapConfigDock.GPSLon));
                            Point3D point = mapTools.Geodetic2ENU(new PointLatLngAlt(lat, lon, imu.alt), new PointLatLngAlt(mapConfigDock.GPSLat, mapConfigDock.GPSLon, 0));
                            Point3D qoint = mapTools.Geodetic2ENU(navigator4D.CurrentObjective, new PointLatLngAlt(mapConfigDock.GPSLat, mapConfigDock.GPSLon, 0));



                            //dPointListObjectivesLng = Navigator.CurrentObjective.Lng;   Se utiliza para el log
                            //dPointListObjectivesLat = pointListObjectives[currentObjective].Lat;     Se utiliza para el log

                            x_enu = qoint.X;
                            y_enu = qoint.Y;

                            x_enu_aircraft = point.X;
                            y_enu_aircraft = point.Y;

                            //Vector2 p = new Vector2(point.X, point.Y);
                            Matrix p = new Matrix(2, 1);
                            p[0, 0] = point.X;
                            p[1, 0] = point.Y;


                            //Vector2 q = new Vector2((float)Convert.ToDouble(x_enu[currentObjective]), (float)Convert.ToDouble(y_enu[currentObjective]));
                            Matrix q = new Matrix(2, 1);
                            //q[0, 0] = Convert.ToDouble(x_enu[currentObjective]);
                            q[0, 0] = Convert.ToDouble(qoint.X);
                            q[1, 0] = Convert.ToDouble(qoint.Y);

                            Matrix _d = p - q;
                            Matrix R = new Matrix(2, 2);
                            /*R[0, 0] = Math.Cos(Math.PI / 2 - navigator.CurrentPsi_F);
                            R[0, 1] = -Math.Sin(Math.PI / 2 - navigator.CurrentPsi_F);
                            R[1, 0] = Math.Sin(Math.PI / 2 - navigator.CurrentPsi_F);
                            R[1, 1] = Math.Cos(Math.PI / 2 - navigator.CurrentPsi_F);*/

                            R[0, 0] = Math.Cos(Math.PI / 2 - navigator4D.CurrentPsi_F);
                            R[0, 1] = -Math.Sin(Math.PI / 2 - navigator4D.CurrentPsi_F);
                            R[1, 0] = Math.Sin(Math.PI / 2 - navigator4D.CurrentPsi_F);
                            R[1, 1] = Math.Cos(Math.PI / 2 - navigator4D.CurrentPsi_F);

                            Matrix d_SF = R.Transpose() * _d;




                            //psi_tilde = Convert.ToDouble(imu.psi) - Convert.ToDouble(navigator.CurrentPsi_F);
                            psi_tilde = Convert.ToDouble(imu.psi) - Convert.ToDouble(navigator4D.CurrentPsi_F);
                            if (psi_tilde > Math.PI)
                                psi_tilde -= 2 * Math.PI;
                            else if (psi_tilde < -Math.PI)
                                psi_tilde += 2 * Math.PI;

                            es = d_SF[0, 0]; // Error es componente x de d
                            ed = d_SF[1, 0]; // Error ed componente y de d


                            s_dot = V * Math.Cos(psi_tilde) + Ks * es;
                            if (s_dot > 17.5)
                                s_dot = 17.5;


                            double delta_ed_dot = (4 * Psi_a * Kdelta * Math.Pow(Math.E, 2 * Kdelta * ed)) / Math.Pow(Math.Pow(Math.E, 2 * Kdelta * ed) + 1, 2);
                            double delta_ed = -Psi_a * (Math.Pow(Math.E, 2 * Kdelta * ed) - 1) / (Math.Pow(Math.E, 2 * Kdelta * ed) + 1);

                            //double beta = -Convert.ToDouble(navigator.CurrentCurvature) * s_dot - delta_ed_dot * (V * Math.Sin(psi_tilde) - Convert.ToDouble(navigator.CurrentCurvature) * es * s_dot) + (V * ed) * 0.001 * ((Math.Sin(psi_tilde) - Math.Sin(delta_ed)) / (psi_tilde - delta_ed));
                            double beta = -Convert.ToDouble(navigator4D.CurrentCurvature) * s_dot - delta_ed_dot * (V * Math.Sin(psi_tilde) - Convert.ToDouble(navigator4D.CurrentCurvature) * es * s_dot) + (V * ed) * 0.003 * ((Math.Sin(psi_tilde) - Math.Sin(delta_ed)) / (psi_tilde - delta_ed));
                            beta = -beta;


                            omega_d = -beta - Kw1 * (psi_tilde - delta_ed);
                            double g = 9.81;
                            double omega = g / V * Math.Tan(imu.phi);
                            old_control_u = control_u;
                            control_u = Math.Atan(V / g * omega_d);
                            omega_tilde = omega - omega_d;



                            //DateTime now = DateTime.Now;
                            //double Dt = (now - then).TotalSeconds;
                            //then = now;

                            // Apply lowpass filter

                            double RC = 0.5;
                            double alpha = dt / (RC + dt);

                            //if (currentObjective != 0)
                            //control_u = alpha * control_u + (1 - alpha) * old_control_u;

                            double omega_d_dot = (omega_d - prevOmega_d) / dt;
                            prevOmega_d = omega_d;


                            double omega_tilde_dot = g / V * imu.p * Math.Pow(1 / Math.Cos(imu.phi), 2) - omega_d_dot;

                            // Old controler
                            //lyap_u = Convert.ToDouble(txtYawKp.Text) * (-Kphi * omega_tilde + Convert.ToDouble(txtYawKp.Text) * omega_tilde_dot - (psi_tilde - delta_ed)) * (V / (g * Math.Pow(1 / Math.Cos(ahrs.phi), 2)));


                            double sat = 0.78;
                            if (control_u > sat)
                                control_u = sat;
                            if (control_u < -sat)
                                control_u = -sat;

                            /*if (navigator.VelocityCheck(s_dot, dt, imu.psi))
                            {
                                if (navigator.IsTurn180)
                                {
                                    mapDock.AddRoute(2, new GMapRoute(navigator.PointListObjectives, "Route"), Color.FromArgb(127, Color.Green));
                                } else if(navigator.IsCirclePath)
                                {
                                    mapDock.AddRoute(3, new GMapRoute(navigator.PointListObjectives, "Route"), Color.FromArgb(127, Color.Yellow));
                                }
                            }*/

                            if (navigator4D.VelocityCheck(s_dot, dt, imu.psi))
                            {
                                if(navigator4D.IsLanding)
                                {

                                }else
                                if (navigator4D.IsTurn180)
                                {
                                    mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.InitialPath, new GMapRoute(navigator4D.PointListObjectivesLatLng, "Route"), Color.FromArgb(127, Color.Green));
                                }
                                else if (navigator4D.IsCirclePath)
                                {
                                    mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.Circle, new GMapRoute(navigator4D.PointListObjectivesLatLng, "Route"), Color.FromArgb(127, Color.Yellow));
                                } else if(startMission)
                                {
                                    startMission = false;
                                    mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.InitialPath, new GMapRoute(navigator4D.PointListObjectivesLatLng, "Route"), Color.FromArgb(127, Color.Blue));
                                }
                            }


                            /*if(navigator.CheckForPointsOfInterests(imu.psi,lat,lon))
                            {
                                mapDock.AddRoute(3,new GMapRoute(navigator.CirclePath, "Circle"),System.Drawing.Color.ForestGreen); //overlay.Routes.Add(new GMapRoute(pointListObjectives, "Circulo"));
                                mapDock.AddRoute(2, new GMapRoute(navigator.PointListObjectives, "Route"), System.Drawing.Color.Black);
                           
                            }*/

                            if (navigator4D.CheckForPointsOfInterests(imu.psi, lat, lon, imu.lat))
                            {
                                mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.Circle, new GMapRoute(navigator4D.CirclePathatLng, "Circle"), System.Drawing.Color.ForestGreen); //overlay.Routes.Add(new GMapRoute(pointListObjectives, "Circulo"));
                                mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.InitialPath, new GMapRoute(navigator4D.PointListObjectivesLatLng, "Route"), System.Drawing.Color.Black);

                            }

                            //log.Append(navigator4D.CurrentObjective.ToStringCSV() + ", " + lat.ToString() + ", " + lon.ToString() + ", " + imu.alt.ToString() + ", " + navigator4D.CurrentVelocity + ", " + imu.pt * 10);
                            log.Append(navigator4D.CurrentObjective.ToStringCSV() + ", " + lat.ToString() + ", " + lon.ToString() + ", " + imu.alt.ToString() + ", " + navigator4D.CurrentVelocity / 10 + ", " + imu.pt + ", " + navigator4D.CurrentPsi_F.ToString() + ", " + imu.psi.ToString() + ", " + es.ToString() + ", " + ed.ToString() + ", " + imu.the.ToString() + ", " + s_dot + ", " + psi_tilde);

                            uavControlQuaternion.updateRollSetPoint(control_u);
                            //uavControlQuaternion.updateAltitudSetPoint(Convert.ToDouble(manualControlDock.sElevator1));
                            uavControlQuaternion.updateAltitudSetPoint(navigator4D.CurrentObjective.Alt);
                            uavControlQuaternion.updateRollValue(imu.phi);
                            uavControlQuaternion.updateAltitudValue(imu.alt);
                            uavControlQuaternion.updatePitchValue(imu.the);
                            uavControlQuaternion.updateSpeedValue(imu.pt * 10);
                            uavControlQuaternion.updateSpeedSetPoint(navigator4D.CurrentVelocity); 
                            uavControlQuaternion.updateDerivatives(imu.p, imu.q, imu.r);
                            uavControlQuaternion.ControlStep(dt);

                            AddPlotVariable("SpeedValue", imu.pt * 10);
                            AddPlotVariable("CurrentVelocity", navigator4D.CurrentVelocity);
                            AddPlotVariable("s_dot", s_dot);
                            AddPlotVariable("psi_tilde", psi_tilde);
                            AddPlotVariable("V * Math.Cos(psi_tilde)", V * Math.Cos(psi_tilde));
                            AddPlotVariable("es", es);
                            AddPlotVariable("Ks*es", Ks * es);
                            AddPlotVariable("dt", dt);

                           



                            
                            

                            if (controlSelectionDock.HilSimulator)
                            {
                                MavLinkNet.UasSetAttitudeTarget cmd = new MavLinkNet.UasSetAttitudeTarget();
                                Quaternion Q = uavControlQuaternion.GetQuaternionSetPoint();
                                if(imu.alt>5)
                                    cmd.Q = mtools.Euler2Quaternion(control_u, uavControlQuaternion.getPitchSetPoint(), 0).ToFloat();
                                else
                                    cmd.Q = mtools.Euler2Quaternion(0, uavControlQuaternion.getPitchSetPoint(), 0).ToFloat();
                                cmd.TargetSystem = 1;
                                cmd.TargetComponent = 0;
                                if(navigator4D.IsLanding && imu.alt<3)
                                    cmd.Thrust = 0;
                                else
                                    cmd.Thrust = (float)uavControlQuaternion.getSpeedControl();
                                cmd.TypeMask = 0;
                                if (!mavHeartBeat.BaseMode.HasFlag(MavLinkNet.MavModeFlag.GuidedEnabled))
                                {
                                    MavLinkNet.UasCommandLong cmd1 = new MavLinkNet.UasCommandLong();
                                    cmd1.Command = MavLinkNet.MavCmd.NavGuidedEnable;
                                    cmd1.Confirmation = 1;
                                    cmd1.Param1 = 1;
                                    cmd1.TargetSystem = 1;
                                    cmd1.TargetComponent = 0;

                                    //mavLinkSerialTransport.SendMavlinkMessage(cmd1);

                                    //messagesDock.AddMessage("UasCommandLong: " + MavLinkNet.MavCmd.NavGuidedEnable.ToString());
                                    //Thread.Sleep(10);
                                }

                                mavLinkSerialTransport.SendMavlinkMessage(cmd);
                                //Thread.Sleep(10);

                            }
                            else
                            {
                                actuators.Aileron = uavControlQuaternion.getRollControl();
                                actuators.Elevator = -uavControlQuaternion.getPitchControl();
                                actuators.Rudder = uavControlQuaternion.getHeadingControl() * .0;
                                actuators.Throttle = uavControlQuaternion.getSpeedControl();
                            }


                        }

                        else if (controlSelectionDock.Lyap3D)
                        {
                            lat = imu.lat + mapConfigDock.GPSLat;
                            lon = imu.lon + mapConfigDock.GPSLon;
                            
                            Point3D point = mapTools.Geodetic2ENU(new PointLatLngAlt(lat, lon, imu.alt), new PointLatLngAlt(mapConfigDock.GPSLat, mapConfigDock.GPSLon, 0));
                            Point3D qoint = mapTools.Geodetic2ENU(navigator4D.CurrentObjective, new PointLatLngAlt(mapConfigDock.GPSLat, mapConfigDock.GPSLon, 0));

                            lyap3D.ArcraftPoint = point;
                            lyap3D.CurvePoint = qoint;
                            lyap3D.Psi = imu.psi;
                            lyap3D.Psi_s = navigator4D.CurrentPsi_F;
                            lyap3D.Theta_s = navigator4D.CurrentTheta_F;

                            lyap3D.Vel = imu.pt;
                            lyap3D.Curvature = navigator4D.CurrentCurvature;

                            lyap3D.ControlStep(dt);

                            control_u = lyap3D.Phi;
                            s_dot = lyap3D.S_dot;

                            AddPlotVariable("S_dot", lyap3D.S_dot);
                            AddPlotVariable("Phi_Control", lyap3D.Phi);
                            AddPlotVariable("Theta_Control", lyap3D.Theta);

                            AddPlotVariable("Ex", lyap3D.Ex);
                            AddPlotVariable("Ey", lyap3D.Ey);
                            AddPlotVariable("Ez", lyap3D.Ez);
                            AddPlotVariable("E_psi", lyap3D.E_psi);

                            AddPlotVariable("psi_s", navigator4D.CurrentPsi_F);

                            log.Append(navigator4D.CurrentObjective.ToStringCSV() + ", " + lat.ToString() + ", " + lon.ToString() + ", " + imu.alt.ToString() + ", " + navigator4D.CurrentVelocity + ", " + imu.pt * 10 + ", " + navigator4D.CurrentPsi_F + ", " + imu.psi + ", " + navigator4D.CurrentTheta_F + ", " + imu.the + ", " + lyap3D.Ex + ", " + lyap3D.Ey + ", " + lyap3D.Ez + ", " + lyap3D.E_psi + ", " + point.X + ", " + point.Y + ", " + point.Z + ", " + qoint.X + ", " + qoint.Y + ", " + qoint.Z);

                            

                            double sat = 0.78;
                            if (control_u > sat)
                                control_u = sat;
                            if (control_u < -sat)
                                control_u = -sat;

                            if (navigator4D.VelocityCheck(s_dot, dt, imu.psi))
                            {
                                if (navigator4D.IsLanding)
                                {

                                }
                                else
                                    if (navigator4D.IsTurn180)
                                    {
                                        mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.InitialPath, new GMapRoute(navigator4D.PointListObjectivesLatLng, "Route"), Color.FromArgb(127, Color.Green));
                                    }
                                    else if (navigator4D.IsCirclePath)
                                    {
                                        mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.Circle, new GMapRoute(navigator4D.PointListObjectivesLatLng, "Route"), Color.FromArgb(127, Color.Yellow));
                                    }
                                    else if (startMission)
                                    {
                                        startMission = false;
                                        mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.InitialPath, new GMapRoute(navigator4D.PointListObjectivesLatLng, "Route"), Color.FromArgb(127, Color.Blue));
                                    }
                            }

                            if (navigator4D.CheckForPointsOfInterests(imu.psi, lat, lon, imu.lat))
                            {
                                mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.Circle, new GMapRoute(navigator4D.CirclePathatLng, "Circle"), System.Drawing.Color.ForestGreen); //overlay.Routes.Add(new GMapRoute(pointListObjectives, "Circulo"));
                                mapDock.AddRoute(MAV3DSim.Docks.Map.MapRoute.InitialPath, new GMapRoute(navigator4D.PointListObjectivesLatLng, "Route"), System.Drawing.Color.Black);

                            }

                            //log.Append(navigator3D.CurrentObjective.ToStringCSV() + ", " + lat.ToString() + ", " + lon.ToString() + ", " + imu.alt.ToString());

                            uavControlQuaternion.updateRollSetPoint(0);//control_u);
                            uavControlQuaternion.updateRollValue(imu.phi);
                            uavControlQuaternion.updatePitchSetPoint(0);//-lyap3D.Theta);
                            uavControlQuaternion.updatePitchValue(imu.the);
                            uavControlQuaternion.updateSpeedValue(imu.pt * 10);
                            uavControlQuaternion.updateSpeedSetPoint(navigator4D.CurrentVelocity); // FIX ME
                            uavControlQuaternion.updateDerivatives(imu.p, imu.q, imu.r);
                            uavControlQuaternion.ControlStep(dt);



                            if (controlSelectionDock.HilSimulator)
                            {
                                MavLinkNet.UasSetAttitudeTarget cmd = new MavLinkNet.UasSetAttitudeTarget();
                                Quaternion Q = uavControlQuaternion.GetQuaternionSetPoint();
                                if (imu.alt > 5)
                                    cmd.Q = mtools.Euler2Quaternion(control_u, lyap3D.Theta, 0).ToFloat();
                                else
                                    cmd.Q = mtools.Euler2Quaternion(0, uavControlQuaternion.getPitchSetPoint(), 0).ToFloat();
                                cmd.TargetSystem = 1;
                                cmd.TargetComponent = 0;
                                if (navigator4D.IsLanding && imu.alt < 3)
                                    cmd.Thrust = 0;
                                else
                                    cmd.Thrust = (float)uavControlQuaternion.getSpeedControl();
                                cmd.TypeMask = 0;
                                if (!mavHeartBeat.BaseMode.HasFlag(MavLinkNet.MavModeFlag.GuidedEnabled))
                                {
                                    MavLinkNet.UasCommandLong cmd1 = new MavLinkNet.UasCommandLong();
                                    cmd1.Command = MavLinkNet.MavCmd.NavGuidedEnable;
                                    cmd1.Confirmation = 1;
                                    cmd1.Param1 = 1;
                                    cmd1.TargetSystem = 1;
                                    cmd1.TargetComponent = 0;

                                    //mavLinkSerialTransport.SendMavlinkMessage(cmd1);

                                    //messagesDock.AddMessage("UasCommandLong: " + MavLinkNet.MavCmd.NavGuidedEnable.ToString());
                                    //Thread.Sleep(10);
                                }

                                mavLinkSerialTransport.SendMavlinkMessage(cmd);
                                //Thread.Sleep(10);

                            }
                            else
                            {
                                actuators.Aileron = uavControlQuaternion.getRollControl();
                                actuators.Elevator = -uavControlQuaternion.getPitchControl();
                                actuators.Rudder = uavControlQuaternion.getHeadingControl() * .0;
                                actuators.Throttle = uavControlQuaternion.getSpeedControl();
                            }



                        }

                        /*if (startMission)
                        {
                            if (imu.alt >= Convert.ToDouble(manualControlDock.sElevator1) - 5 && Convert.ToDouble(manualControlDock.sElevator1) < 100)
                                manualControlDock.sElevator1 = (Convert.ToDouble(manualControlDock.sElevator1) + 10).ToString();
                            if (imu.alt >= 100)
                            {
                                startMission = false;
                                manualControlDock.sThrottle = ".5";
                                controlSelectionDock.Guidance = true;
                            }

                        }*/



                        if (udpConfigDock.Protocol == "MNAV")
                        {
                            actuatorEncode = actuators.Encode();
                            com.Send(actuatorEncode);
                        }
                        else
                        {
                            actuators.Elevator = -actuators.Elevator;
                            actuatorEncode = actuators.Encode();
                            //MavLinkNet.MavLinkState mavState = new MavLinkNet.MavLinkState();
                            manualControl.X = (short)((actuatorEncode[4] << 8) | actuatorEncode[5]); //Convert.ToInt16(actuators.Elevator * 2000);
                            manualControl.Y = (short)((actuatorEncode[6] << 8) | actuatorEncode[7]);//Convert.ToInt16(actuators.Aileron * 2000);
                            manualControl.Z = (short)((actuatorEncode[8] << 8) | actuatorEncode[9]);//Convert.ToInt16(actuators.Throttle * 2000 - 1000);
                            manualControl.R = (short)((actuatorEncode[10] << 8) | actuatorEncode[11]);//Convert.ToInt16(actuators.Rudder * 2000);
                            mavLinkUdpTransport.Send(manualControl);

                            //Thread.Sleep(100);
                        }

                        imu.Updated = false;
                        //threadSafe.SetStatusStripPropertyThreadSafe(statusStrip1,lblFreqStatus,"Text",(1/(period - lastPeriod).Seconds).ToString());

                    }//end if(imu.updated)

                } while (!closeRequested);


                //Console.WriteLine("Salio del thread Controller");
            }
            catch (Exception e)
            {
                Console.WriteLine("-------------- Controller Exception-------------");
                Console.WriteLine("CONTROLLER: " + e.Message);
                Console.WriteLine("-------------- Controller Exception-------------");
            }
        }

        private IDockContent FindDocument(string persistString)
        {
            return docks[persistString];
        }

        public Dictionary<string, IDockContent> GetDocks { get { return this.docks; } }
        public Utils.IMU GetImu { get { return imu; } }
        public Utils.Log GetLog { get { return log; } }
        //public Controler.Navigator GetNavigator { get { return navigator; } }
        public Navigation.Navigator4D GetNavigator4D { get { return navigator4D; } }
        //public Controler.Navigator3D GetNavigator3D { get { return navigator3D; } }
        public Controller.ControllerLyap3D GetControllerLyap3D { get { return lyap3D; } }

        private void mbtSaveLogFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Default to the directory which contains our content files.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation);
            string contentPath = Path.GetFullPath(relativePath);

            saveFileDialog.InitialDirectory = contentPath;

            saveFileDialog.Title = "Save Log File";



            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                log.SaveLog(saveFileDialog.FileName);
            }
        }

        

        private void InitUavControlQuaternion()
        {
            uavControlQuaternion = new Controller.UAVControlQuaternion();
            uavControlQuaternion.updateAltitudeGains(altitudePidGainsDock.Kp, altitudePidGainsDock.Ki, altitudePidGainsDock.Kd);
            uavControlQuaternion.updatePitchGains(pitchPidGainsDock.Kp, pitchPidGainsDock.Ki, pitchPidGainsDock.Kd);
            uavControlQuaternion.updateRollGains(rollPidGainsDock.Kp, rollPidGainsDock.Ki, rollPidGainsDock.Kd);
            uavControlQuaternion.updateSpeedGains(airSpeedPidGainsDock.Kp, airSpeedPidGainsDock.Ki, airSpeedPidGainsDock.Kd, airSpeedPidGainsDock.FF);
        }

        private void inputCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cameraInputDock.Show(dockPanel);
        }

        private void messagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            messagesDock.Show(dockPanel);
        }

        private void mbtWaypoints_Click(object sender, EventArgs e)
        {
            waypointsDock.Show(dockPanel);
        }

        private void mbtGeofence_Click(object sender, EventArgs e)
        {
            geofenceDock.Show(dockPanel);
        }



        

        

        

    }
}
