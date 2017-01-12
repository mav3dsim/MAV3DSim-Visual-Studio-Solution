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

namespace MAV3DSim.Docks
{
    // A delegate type for radio buttons.
    public delegate void RadioButtonEventHandler(object sender, EventArgs e);
    /*public delegate void StopButtonEventHandler(object sender, EventArgs e);
    public delegate void ManualRButtonEventHandler(object sender, EventArgs e);
    public delegate void RollPitchRButtonEventHandler(object sender, EventArgs e);
    public delegate void AltitudeRButtonEventHandler(object sender, EventArgs e);
    public delegate void L1ControllerRButtonEventHandler(object sender, EventArgs e);
    public delegate void LyapControllerRButtonEventHandler(object sender, EventArgs e);
    public delegate void LyapController3DRButtonEventHandler(object sender, EventArgs e);
    public delegate void StartMissionButtonEventHandler(object sender, EventArgs e);
    public delegate void EndMissionButtonEventHandler(object sender, EventArgs e);
     * */
    public partial class ControlSelection : DockContent
    {
        Mav3DSim parent;
        // An event that clients can use to be notified
        public event RadioButtonEventHandler StartButton;
        public event RadioButtonEventHandler StopButton;
        public event RadioButtonEventHandler ManualRButton;
        public event RadioButtonEventHandler RollPitchRButton;
        public event RadioButtonEventHandler AltitudeRButton;
        public event RadioButtonEventHandler L1ControllerRButton;
        public event RadioButtonEventHandler LyapControllerRButton;
        public event RadioButtonEventHandler LyapController3DRButton;
        public event RadioButtonEventHandler StartMissionButton;
        public event RadioButtonEventHandler EndMissionButton;

        ThreadSafe threadSafe;
        
        public ControlSelection()
        {
            InitializeComponent();
            threadSafe = new ThreadSafe();
        }

        public bool Manual
        {
            set { rbManual.Checked = value; }
            get { return rbManual.Checked; }
        }

        public bool PitchRoll
        {
            set { rbPitchRoll.Checked = value; }
            get { return rbPitchRoll.Checked; }
        }

        public bool Altitud
        {
            set { rbAltitud.Checked = value; }
            get { return rbAltitud.Checked; }
        }

        public bool L1Controller
        {
            set { rbL1Controller.Checked = value; }
            get { return rbL1Controller.Checked; }
        }

        public bool Guidance
        {
            set { threadSafe.SetControlPropertyThreadSafe(rbLyapController, "Checked", value); }
            get { return rbLyapController.Checked; }
        }

        public bool Lyap3D
        {
            set { threadSafe.SetControlPropertyThreadSafe(rbLyapController3D, "Checked", value); }
            get { return rbLyapController3D.Checked; }
        }
        public bool HilSimulator
        {
            set { threadSafe.SetControlPropertyThreadSafe(chkHilSimulator, "Checked", value); }
            get { return (bool)threadSafe.GetControlPropertyThreadSafe(chkHilSimulator, "Checked"); }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            OnStartButton(EventArgs.Empty);
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        protected virtual void OnStartButton(EventArgs e)
        {
            if (StartButton != null)
                StartButton(this, e);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopButtonClick();
        }

        public void StopButtonClick()
        {
            OnStopButton(EventArgs.Empty);
            threadSafe.SetControlPropertyThreadSafe( btnStart,"Enabled",true);
            threadSafe.SetControlPropertyThreadSafe( btnStop, "Enabled", false);
            rbManual.Checked = true;
        }

        protected virtual void OnStopButton(EventArgs e)
        {
            if (StopButton != null)
                StopButton(this, e);
        }

        private void rbManual_CheckedChanged(object sender, EventArgs e)
        {
            if(rbManual.Checked)
                if (ManualRButton != null)
                    ManualRButton(this, e);
        }

        private void rbPitchRoll_CheckedChanged(object sender, EventArgs e)
        {
            if(rbPitchRoll.Checked)
                if (RollPitchRButton != null)
                    RollPitchRButton(this, e);
        }

        private void rbAltitud_CheckedChanged(object sender, EventArgs e)
        {
            if(rbAltitud.Checked)
                if (AltitudeRButton != null)
                    AltitudeRButton(this, e);
        }

        private void rbL1Controller_CheckedChanged(object sender, EventArgs e)
        {
            if (rbL1Controller.Checked)
                if (L1ControllerRButton != null)
                    L1ControllerRButton(this, e);
        }
        private void rbLyapController_CheckedChanged(object sender, EventArgs e)
        {
            
            if (rbLyapController.Checked)
                if (LyapControllerRButton != null)
                    LyapControllerRButton(this, e);
        }

        private void btnStartMission_Click(object sender, EventArgs e)
        {
            if (StartMissionButton != null)
                StartMissionButton(this, e);
        }

        private void btnEndMission_Click(object sender, EventArgs e)
        {
            // FIX ME
            //((Controler.Navigator4D)parent.GetNavigator4D).GenerateLandingPath(((Utils.IMU)parent.GetImu).psi, ((Docks.MapInfo)parent.GetDocks["MAV3DSim.Docks.MapInfo"]).Lat, ((Docks.MapInfo)parent.GetDocks["MAV3DSim.Docks.MapInfo"]).Lon);
            //((Docks.Map)parent.GetDocks["MAV3DSim.Docks.Map"]).AddRoute(MAV3DSim.Docks.Map.MapRoute.InitialPath, new GMap.NET.WindowsForms.GMapRoute(((Controler.Navigator4D)parent.GetNavigator4D).PointListObjectives, "EndPath"), Color.FromArgb(125, Color.Magenta));
        }

        private void ControlSelection_Shown(object sender, EventArgs e)
        {
            parent = (Mav3DSim)this.ParentForm;
        }

        private void rbLyapController3D_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLyapController3D.Checked)
                if (LyapController3DRButton != null)
                    LyapController3DRButton(this, e);
        }


    }
}
