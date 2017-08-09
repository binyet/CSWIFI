using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WiFIMsg
{
    public partial class PassWord : Form
    {
        public PassWord()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Open();
            String text = textBox1.Text.ToString();
            serialPort1.WriteLine(text);
            serialPort1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            serialPort1.PortName = "COM3";
            serialPort1.BaudRate = 9600; 
        }

        private void PassWord_Load(object sender, EventArgs e)
        {
            new Thread(method).Start();
        }

        private void method()
        {
            MethodInvoker MethInvo = new MethodInvoker(show);
            BeginInvoke(MethInvo);
        }

        private void show()
        {
            Form2 f = new Form2();
            f.Visible = true;
            f.Activate();
        }
    }
}
