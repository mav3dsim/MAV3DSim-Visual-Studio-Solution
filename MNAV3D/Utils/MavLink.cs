using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAV3DSim.Utils
{
    class MavLink
    {

        int packetid = 0;
        byte MAVLINK_MSG_ID_REQUEST_DATA_STREAM = 66;
        IMU imu = new IMU();

        public static readonly byte[] MAVLINK_MESSAGE_CRCS = new byte[] { 50, 124, 137, 0, 237, 217, 104, 119, 0, 0, 0, 89, 0, 0, 0, 0, 0, 0, 0, 0, 214, 159, 220, 168, 24, 23, 170, 144, 67, 115, 39, 246, 185, 104, 237, 244, 222, 212, 9, 254, 230, 28, 28, 132, 221, 232, 11, 153, 41, 39, 0, 0, 0, 0, 15, 3, 0, 0, 0, 0, 0, 153, 183, 51, 82, 118, 148, 21, 0, 243, 124, 0, 0, 38, 20, 158, 152, 143, 0, 0, 0, 106, 49, 22, 143, 140, 5, 150, 0, 231, 183, 63, 54, 0, 0, 0, 0, 0, 0, 0, 175, 102, 158, 208, 56, 93, 138, 108, 32, 185, 84, 34, 0, 124, 237, 4, 76, 128, 56, 116, 134, 237, 203, 250, 87, 203, 220, 25, 226, 0, 29, 223, 85, 6, 229, 203, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 154, 49, 0, 134, 219, 208, 188, 84, 22, 19, 21, 134, 0, 78, 68, 189, 127, 154, 21, 21, 144, 1, 234, 73, 181, 22, 83, 167, 138, 234, 240, 47, 189, 52, 174, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 204, 49, 170, 44, 83, 46, 0 };

        public MavLink()
        {

        }

        public void decode(byte[] data)
        {
            
            
      
            

        }

        public byte[] requestDatastream(byte id, byte rate)
        {
            MavLinkNet.UasRequestDataStream rqstDataStream = new MavLinkNet.UasRequestDataStream();
            rqstDataStream.ReqMessageRate = rate;
            rqstDataStream.StartStop = 1;
            rqstDataStream.TargetSystem = 1;
            rqstDataStream.TargetComponent = 0;
            rqstDataStream.ReqStreamId = id;

            return GeneratePacket(rqstDataStream, MAVLINK_MSG_ID_REQUEST_DATA_STREAM);

        }

        public byte[] GeneratePacket(byte[] packetData, byte msgID)
        {
            byte[] Packet = new byte[packetData.Length + 6 + 2];

            Packet[0] = (byte)254;
            Packet[1] = (byte)packetData.Length;
            Packet[2] = (byte)packetid;
            Packet[3] = (byte)255;
            Packet[4] = (byte)1;
            Packet[5] = msgID;

            for (int n = 0; n < packetData.Length; n++)
                Packet[6 + n] = packetData[n];

            ushort checksum = MavlinkCRC.crc_calculate(Packet, Packet[1] + 6);

            checksum = MavlinkCRC.crc_accumulate(MAVLINK_MESSAGE_CRCS[msgID], checksum);

            byte ck_a = (byte)(checksum & 0xFF); ///< High byte
            byte ck_b = (byte)(checksum >> 8); ///< Low byte

            Packet[Packet.Length - 2] = ck_a;
            Packet[Packet.Length - 1] = ck_b;

            return Packet;
        }

        //Overloaded function
        public byte[] GeneratePacket(object packetData, byte msgID)
        {
            int len = System.Runtime.InteropServices.Marshal.SizeOf(packetData);
            byte[] arr = new byte[len];
            IntPtr ptr = System.Runtime.InteropServices.Marshal.AllocHGlobal(len);
            System.Runtime.InteropServices.Marshal.StructureToPtr(packetData, ptr, true);
            System.Runtime.InteropServices.Marshal.Copy(ptr, arr, 0, len);
            System.Runtime.InteropServices.Marshal.FreeHGlobal(ptr);

            return GeneratePacket(arr, msgID);

        }

        public IMU Imu
        {
            get { return imu; }
            set { imu = value; }
        }


    }
}
