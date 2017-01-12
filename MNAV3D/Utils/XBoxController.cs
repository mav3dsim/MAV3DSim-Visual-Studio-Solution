using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.DirectX.DirectInput;
using System.Threading;

namespace MAV3DSim
{
    // A delegate type for hooking up change notifications.
    public delegate void StateChangedHandler(object sender, EventArgs e);

    class XBoxController : IDisposable
    {
        private Device xboxController;
        private AutoResetEvent JOYSTICK_EVENT = new AutoResetEvent(true);
        private WaitHandle[] WAIT_FOR = new WaitHandle[1];
        private bool CloseRequest = true;
        
        // elements of the list change.
        public event StateChangedHandler StateChanged;

        public XBoxController()
        {
            DeviceList devices;
            DeviceInstance dev;
            try
            {
                devices = Manager.GetDevices(DeviceType.Gamepad, EnumDevicesFlags.AllDevices);
                devices.MoveNext();
                dev = (Microsoft.DirectX.DirectInput.DeviceInstance)devices.Current;



                try
                {
                    xboxController = new Device(dev.ProductGuid);
                }
                catch (Exception ex)
                {
                    xboxController = new Device(dev.InstanceGuid);
                }

                //gamepadDevice.SetCooperativeLevel(, CooperativeLevelFlags.NonExclusive | CooperativeLevelFlags.Background);

                Thread t = new Thread(new ThreadStart(DoWork));
                t.Start();
                WAIT_FOR[0] = JOYSTICK_EVENT;
                xboxController.SetEventNotification(JOYSTICK_EVENT);
                xboxController.Acquire();
            }
            catch (Exception e) { }

        }

        private void DoWork()
        {
            //JoystickEvent je = null;


            while (CloseRequest)
            {
                try
                {
                    WaitHandle.WaitAll(WAIT_FOR);
                    JoystickState xboxState = xboxController.CurrentJoystickState;
                    byte[] buttons = xboxState.GetButtons();

                    this.Y = (buttons[0] == 128);
                    this.B = (buttons[1] == 128);
                    this.A = (buttons[2] == 128);
                    this.X = (buttons[3] == 128);
                    this.WhiteButton = (buttons[4] == 128);
                    this.BlackButton = (buttons[5] == 128);
                    this.LeftTrigger = (buttons[6] == 128);
                    this.RightTrigger = (buttons[7] == 128);
                    this.Back = (buttons[8] == 128);
                    this.Start = (buttons[9] == 128);
                    this.LeftStick = (buttons[10] == 128);
                    this.RightStick = (buttons[11] == 128);

                    int[] dpad = xboxState.GetPointOfView();

                    this.Up = (dpad[0] == 0 || dpad[0] == 31500 || dpad[0] == 4500);
                    this.Down = (dpad[0] == 18000 || dpad[0] == 22500 || dpad[0] == 13500);
                    this.Left = (dpad[0] == 27000 || dpad[0] == 31500 || dpad[0] == 22500);
                    this.Right = (dpad[0] == 9000 || dpad[0] == 13500 || dpad[0] == 4500);

                    this.X1 = -(((xboxState.X - 1) * 1.0f / 32767.0f) - 1);
                    this.Y1 = -(((xboxState.Y - 1) * 1.0f / 32767.0f) - 1);
                    this.X2 = -(((xboxState.Z - 1) * 1.0f / 32767.0f) - 1);
                    this.Y2 = -(((xboxState.Rz - 1) * 1.0f / 32767.0f) - 1);

                    /* Joystick Gera
                    this.X1 = -(((xboxState.X - 1) * 1.0f / 32707.0f) - 1);
                    this.Y1 = -(((xboxState.Y - 1) * 1.0f / 32707.0f) - 1);
                    this.X2 = -(((xboxState.Z - 1) * 1.0f / 32707.0f) - 1);
                    this.Y2 = -(((xboxState.Rz - 1) * 1.0f / 32707.0f) - 1);
                    */

                    OnChanged(EventArgs.Empty);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error XBoxController" + e.ToString());
                }

            }
        }

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnChanged(EventArgs e)
        {
            if (StateChanged != null)
                StateChanged(this, e);
        }


        public bool A { get; set; }
        public bool B { get; set; }
        public bool X { get; set; }
        public bool Y { get; set; }

        public bool Start { get; set; }
        public bool Back { get; set; }

        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool Left { get; set; }
        public bool Right { get; set; }

        public bool LeftTrigger { get; set; }
        public bool RightTrigger { get; set; }

        public bool LeftStick { get; set; }
        public bool RightStick { get; set; }

        public bool BlackButton { get; set; }
        public bool WhiteButton { get; set; }

        public float X1 { get; set; }
        public float Y1 { get; set; }
        public float X2 { get; set; }
        public float Y2 { get; set; }


        public void Dispose()
        {
            CloseRequest = false;
            

        }


    }
}
