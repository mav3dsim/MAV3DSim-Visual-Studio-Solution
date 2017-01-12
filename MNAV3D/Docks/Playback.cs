using MAV3DSim.Utils;
using MAV3DSim.Utils.GenericParsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MAV3DSim.Docks
{
    public partial class Playback : DockContent
    {
        ThreadSafe threadSafe = new ThreadSafe();
        Mav3DSim parent;
        bool playback;
        DataTable table;
        Dictionary<string, double> imu = new Dictionary<string,double>();
        double startTime = 0;
        

        public Playback()
        {
            imu.Add("IMU_AccX", 0);
            imu.Add("IMU_AccY", 0);
            imu.Add("IMU_AccZ", 0);

            imu.Add("IMU_GyroX", 0);
            imu.Add("IMU_GyroY", 0);
            imu.Add("IMU_GyroZ", 0);

            imu.Add("IMU_MagX", 0);
            imu.Add("IMU_MagY", 0);
            imu.Add("IMU_MagZ", 0);

            imu.Add("GPOS_Lat", 0);
            imu.Add("GPOS_Lon", 0);
            imu.Add("GPOS_Alt", 0);

            imu.Add("ATT_Roll", 0);
            imu.Add("ATT_Pitch", 0);
            imu.Add("ATT_Yaw", 0);

            imu.Add("AIRS_TrueSpeed", 0);
            imu.Add("SENS_DiffPresFilt", 0);
            
            

            

            
            
            InitializeComponent();
        }

        private void Playback_Shown(object sender, EventArgs e)
        {
            parent = (Mav3DSim)this.ParentForm;
        }

        private void btnLoad_Click(object sender, EventArgs e)
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
                GenericParserAdapter parser = new GenericParserAdapter(fileDialog.FileName);
                parser.FirstRowHasHeader=true;
                parser.ColumnDelimiter = ",".ToCharArray()[0];
                parser.SkipStartingDataRows = 2;
                
                table = parser.GetDataTable();
                DataTable dtCloned = table.Clone();
                dtCloned.Columns["TIME_StartTime"].DataType = typeof(Int32);
                foreach (DataRow row in table.Rows)
                {
                    dtCloned.ImportRow(row);
                }
                table = dtCloned;

                startTime = Convert.ToDouble(table.Rows[0]["TIME_StartTime"]);
                
            }
        }

        private void chkEnable_CheckedChanged(object sender, EventArgs e)
        {
            playback = chkEnable.Checked;
        }

        public bool PlaybackEnabled
        {
            get { return playback; }
            set{ playback= value;}
        }

        public Dictionary<string,double> GetImu(double microseconds)
        {
            int rowIndex = table.Rows.IndexOf(table.Select("TIME_StartTime - "+startTime.ToString()+" > "+microseconds.ToString())[0]);

            Dictionary<string, double>tmp = new Dictionary<string,double>();

            foreach (string item in imu.Keys)
            {
                tmp.Add(item,Convert.ToDouble(table.Rows[rowIndex][item]));
            }

            imu = tmp;
            
            return imu;
        }

  
    }
}
