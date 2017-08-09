using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NativeWifi;

namespace WiFIMsg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ColumnHeader ch = new ColumnHeader();  //先创建列表头
            listView1.GridLines = true;//显示网格
            listView1.Scrollable = true;//显示所有项时是否显示滚动条
            listView1.AllowColumnReorder = true;
            listView1.FullRowSelect = true;
            listView1.CheckBoxes = true;
            this.listView1.Columns.Add("wifi名称", 160, HorizontalAlignment.Left); //一步添加 
            this.listView1.Columns.Add("wifiSSID", 120, HorizontalAlignment.Left); //一步添加 
            this.listView1.Columns.Add("加密方式", 100, HorizontalAlignment.Left); //一步添加
            this.listView1.Columns.Add("信号强度", 90, HorizontalAlignment.Left); //一步添加 
         

            ScanSSID();
        }

        private void ScanSSID()
        {
            WlanClient client = new WlanClient();
            foreach(WlanClient.WlanInterface wlanIface in client.Interfaces)
            {
                Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList(0);
                foreach(Wlan.WlanAvailableNetwork network in networks)
                {
                    WIFISSID targetSSID = new WIFISSID();
                    targetSSID.wlanInterface = wlanIface;
                    targetSSID.wlanSignalQuality = (int)network.wlanSignalQuality;
                    targetSSID.SSID = GetStringForSSID(network.dot11Ssid);
                    targetSSID.dot11DefaultAuthAlgorithm = network.dot11DefaultAuthAlgorithm.ToString();
                    targetSSID.dot11DefaultCipherAlgorithm = network.dot11DefaultCipherAlgorithm.ToString();

                    wifiListOKADDitem(targetSSID.SSID,
                        targetSSID.dot11DefaultAuthAlgorithm,
                        targetSSID.dot11DefaultCipherAlgorithm,
                        targetSSID.wlanSignalQuality);
                    //checkedListBox1.Items.Add(targetSSID.SSID);
                    //if (targetSSID.SSID.ToLower().Equals("cmcc"))
                    //{
                    //    cmccWifiSSID = targetSSID;
                    //    return;
                    //}
                }
            }
        }

        //添加数据
        private void wifiListOKADDitem(String wifiname, String pass, String dot11DefaultAuthAlgorithm, int i)
        {
            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度  
            //this.wifiListOK.Items.Add(wifiname,0);
            ListViewItem wifiitem = listView1.Items.Add(wifiname);

            wifiitem.SubItems.Add(pass);
            wifiitem.SubItems.Add(dot11DefaultAuthAlgorithm);
            wifiitem.SubItems.Add(i + "");

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
            this.listView1.View = System.Windows.Forms.View.Details;
        }

        private string GetStringForSSID(Wlan.Dot11Ssid ssid)
        {
            return Encoding.UTF8.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //if(checkedListBox1.IndexFromPoint(  
            //    checkedListBox1.PointToClient(Cursor.Position).X, 
            //    checkedListBox1.PointToClient(Cursor.Position).Y) == -1)
            //{
            //    e.NewValue = e.CurrentValue;
            //}
        }

        private void checkedListBox1_DoubleClick(object sender, EventArgs e)
        {
            //String WlanSSID = checkedListBox1.SelectedItem.ToString();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {

        }
    }

    class WIFISSID
    {
        public string SSID = "NONE";
        public string dot11DefaultAuthAlgorithm = "";
        public string dot11DefaultCipherAlgorithm = "";
        public bool networkConnectable = true;
        public string wlanNotConnectableReason = "";
        public int wlanSignalQuality = 0;
        public WlanClient.WlanInterface wlanInterface = null;
    }
}
