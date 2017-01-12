using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using DirectShowLib;

namespace MAV3DSim.Docks
{
    
    public partial class CameraInput : DockContent
    {
        private int _CameraIndex;
        private Capture capture;
        public CameraInput()
        {
            InitializeComponent();
           

            List<KeyValuePair<int, string>> ListCamerasData = new List<KeyValuePair<int, string>>();

            //-> Find systems cameras with DirectShow.Net dll
            DsDevice[] _SystemCamereas = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            int _DeviceIndex = 0;
            foreach (DirectShowLib.DsDevice _Camera in _SystemCamereas)
            {
                ListCamerasData.Add(new KeyValuePair<int, string>(_DeviceIndex, _Camera.Name));
                _DeviceIndex++;
            }

            //-> clear the combobox
            
            cbCameraList.DataSource = null;
            cbCameraList.Items.Clear();

            //-> bind the combobox
            cbCameraList.DataSource = new BindingSource(ListCamerasData, null);
            cbCameraList.DisplayMember = "Value";
            cbCameraList.ValueMember = "Key";
            
        }

        private void btStartCapture_Click(object sender, EventArgs e)
        {
            //-> Get the selected item in the combobox
            KeyValuePair<int, string> SelectedItem = (KeyValuePair<int, string>)cbCameraList.SelectedItem;
            //-> Assign selected cam index to defined var
            _CameraIndex = SelectedItem.Key;

            try
            {
                capture = new Capture(_CameraIndex);
                capture.ImageGrabbed += ProcessFrame;
                capture.Start();
                //_capture.ImageGrabbed += ProcessFrame;
            }
            catch (Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }
           
            
        }

        private void ProcessFrame(object sender, EventArgs arg)
        {
            Image<Bgr, Byte> frame = capture.RetrieveBgrFrame();
            pbImage.Image = frame;
        }
    }
}
