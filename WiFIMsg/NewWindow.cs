using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace WiFIMsg
{
    public partial class NewWindow : Form
    {

        private static Encoding encode = Encoding.Default;

        public NewWindow()
        {
            InitializeComponent();
        }

        private void NewWindow_Load(object sender, EventArgs e)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect("127.0.0.1", 8888);
            Send("127.0.0.1", 8888, "test");
        }
        /// <summary>  
        /// 发送数据  
        /// </summary>  
        /// <param name="host"></param>  
        /// <param name="port"></param>  
        /// <param name="data"></param>  
        /// <returns></returns>  
        public string Send(string host, int port, string data)
        {
            string result = string.Empty;
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(host, port);
            clientSocket.Send(encode.GetBytes(data));
            richTextBox1.AppendText("send: "+data+"\r\n");
            return result;
        }


        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WIn32Api.UM_1:
                    string str = Marshal.PtrToStringAuto(m.LParam);
                    richTextBox1.AppendText(str);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }

        }
    }
}
