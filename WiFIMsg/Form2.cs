using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WiFIMsg
{
    public partial class Form2 : Form
    {
        IntPtr h;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //serialPort1.Open();
            //serialPort1.WriteLine(textBox1.Text.ToString());
            //serialPort1.Close();
            string str = textBox1.Text;
            IntPtr i = Marshal.StringToHGlobalAuto(str);
            WIn32Api.PostMessage(h, WIn32Api.UM_1, 0, i);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //serialPort1.Close();
            //serialPort1.PortName = "COM3";
            //serialPort1.BaudRate = 9600;
            var f = new NewWindow();
            h = f.Handle;
            f.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
