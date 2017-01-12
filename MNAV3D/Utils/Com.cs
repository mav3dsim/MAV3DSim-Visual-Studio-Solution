using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO.Ports;
namespace MAV3DSim
{
    public delegate void DataReceivedHandler(object sender, EventArgs e);
    class Com
    {
        
        private SerialPort serialPort;
        private UdpClient udpClient = new UdpClient();
        private Int32 port;
        private String ipaddress;
        private IPAddress ip;
        private IPEndPoint ipEndPoint;
        private Byte[] buf;
        private Byte[] buf_aux;

        Socket socket;

        private int rxOffset;
        public event DataReceivedHandler DataReceive;

        /// <summary>
        /// This is a class for UDP comunications.</summary>
        /// <remarks>
        /// In this class you can open the socket, send data through UDP
        /// and receive data from a local host.</remarks>
        public Com()
        {
            serialPort = new SerialPort();
        }

        public void CreatConnectionUDP()
        {
            try
            {
                ip = IPAddress.Parse(ipaddress);
                ipEndPoint = new IPEndPoint(ip, port);
            }
            catch (Exception e) { }
        }

        public void CreatConnectionTCP()
        {
            
			//create a new client socket ...
			socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			
			ip = IPAddress.Parse(ipaddress);
            ipEndPoint = new IPEndPoint(ip, port);
			socket.Connect(ipEndPoint);	
        }

        public void CreateConnectionSerialPort()
        {
            DataReceived = new byte[102];
            buf = new Byte[5000];
            buf_aux = new Byte[5000];
            serialPort.BaudRate = Convert.ToInt32(this.BaudRate);
            serialPort.DataBits = Convert.ToInt32(this.DataBits);
            serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), this.StopBits);
            serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), this.Parity);
            serialPort.PortName = this.PortName;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
            serialPort.Open();
        }

        public void CloseSerialPort()
        {
            serialPort.Close();
        }

        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort spL = (SerialPort)sender;
            int messageLength= 102;
            int bufSize = spL.BytesToRead;
            Console.WriteLine("DATA RECEIVED!");
            Console.WriteLine(spL.Read(buf, rxOffset, bufSize));
            //DataReceived = buf;
            rxOffset += bufSize;
            for (int i = 0; i < buf.Length - 3;i++ )
            {
                if (buf[i] == 'U' && buf[i + 1] == 'U' && buf[i + 2] == 'N')
                    if (rxOffset - i >= messageLength)
                    {
                        Array.Copy(buf, i, DataReceived, 0, (buf.Length - i) > messageLength ? messageLength : buf.Length - i);
                        buf_aux = new byte[5000];
                        Array.Copy(buf, i + messageLength, buf_aux, 0, buf.Length - i - messageLength);
                        buf = new byte[5000];
                        Array.Copy(buf_aux, 0, buf, 0, buf.Length);
                        rxOffset = rxOffset - i - messageLength;
                        OnChanged(EventArgs.Empty);

                    }
                    
            }
                
            
                
            

                
            
            
        }

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnChanged(EventArgs e)
        {
            if (DataReceive != null)
                DataReceive(this, e);
        }

        public void Send(byte[] data)
        {
            if (!UseSerialCom)
            {
                if (socket != null)
                {
                    if (socket.IsBound)
                        socket.Send(data);

                }
                else
                    udpClient.Send(data, data.Length, ipEndPoint);
            }
            else
            {
                serialPort.Write(data, 0, data.Length);
            }
                
        }

        public byte[] Receive()
        {
            byte[] receive = new byte[0];
            if (!UseSerialCom)
            {
                
                if (socket != null)
                {
                    if (!socket.IsBound)
                        socket.Receive(receive);
                }
                else
                {
                    return udpClient.Receive(ref ipEndPoint);
                }
            }
            else
            {
                
            }
            return receive;
        }

        public Int32 Port
        {
            get { return port; }
            set { port = value; }
        }

        public String IpAddress
        {
            get { return ipaddress; }
            set { ipaddress = value; }
        }
		
		public bool isConnectedTCP{
			get{return socket.IsBound;}
		}

        public bool UseSerialCom
        {
            get;
            set;
        }
        public String BaudRate { get; set; }
        public String Parity { get; set; }
        public String StopBits { get; set; }
        public String DataBits { get; set; }
        public String PortName  { get; set; }
        public byte[] DataReceived;
		
        
    }
}
