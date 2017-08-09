using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WiFIMsg
{
    public partial class Form2 : Form
    {
        IntPtr h;
        private static Encoding encode = Encoding.Default;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            IntPtr i = Marshal.StringToHGlobalAuto(str);
            WIn32Api.PostMessage(h, WIn32Api.UM_1, 0, i);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            var f = new NewWindow();
            h = f.Handle;
            f.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            new Thread(connSocket).Start();
        }

        private void connSocket()
        {
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(new IPEndPoint(IPAddress.Any, 8888));
            listenSocket.Listen(100);
            while (true)
            {
                Socket acceptSocket = listenSocket.Accept();
                string receiceData = Receive(acceptSocket, 1000);
                acceptSocket.Send(encode.GetBytes("ok"));
                DestroySocket(acceptSocket); //import 
            }
        }
        /// <summary>  
        /// 发送数据  
        /// </summary>  
        /// <param name="host"></param>  
        /// <param name="port"></param>  
        /// <param name="data"></param>  
        /// <returns></returns>  
        public static string Send(string host, int port, string data)
        {
            string result = string.Empty;
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(host, port);
            clientSocket.Send(encode.GetBytes(data));
            Console.WriteLine("Send：" + data);
            result = Receive(clientSocket, 5000 * 2); //5*2 seconds timeout.  
            Console.WriteLine("Receive：" + result);
            DestroySocket(clientSocket);
            return result;
        }
        /// <summary>  
        /// 接收数据  
        /// </summary>  
        /// <param name="socket"></param>  
        /// <param name="timeout"></param>  
        /// <returns></returns>  
        private static string Receive(Socket socket, int timeout)
        {
            string result = string.Empty;
            socket.ReceiveTimeout = timeout;
            List<byte> data = new List<byte>();
            byte[] buffer = new byte[1024];
            int length = 0;
            try
            {
                while ((length = socket.Receive(buffer)) > 0)
                {
                    for (int j = 0; j < length; j++)
                    {
                        data.Add(buffer[j]);
                    }
                    if (length < buffer.Length)
                    {
                        break;
                    }
                }
            }
            catch { }
            if (data.Count > 0)
            {
                result = encode.GetString(data.ToArray(), 0, data.Count);
            }
            MessageBox.Show(result);
            return result;
        }

        /// <summary>  
        /// 销毁Socket对象  
        /// </summary>  
        /// <param name="socket"></param>  
        private static void DestroySocket(Socket socket)
        {
            if (socket.Connected)
            {
                socket.Shutdown(SocketShutdown.Both);
            }
            socket.Close();
        }
    }
}
