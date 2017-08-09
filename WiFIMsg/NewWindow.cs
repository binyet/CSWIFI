using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace WiFIMsg
{
    public partial class NewWindow : Form
    {
        public NewWindow()
        {
            InitializeComponent();
        }

        private void NewWindow_Load(object sender, EventArgs e)
        {

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
