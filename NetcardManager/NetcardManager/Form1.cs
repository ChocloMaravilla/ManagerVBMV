using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetcardManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Function();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void Function()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            label6.Text = Environment.UserName.ToString();
            label10.Text = Dns.GetHostName().ToString();
            NetworkInterface[] Ifaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface item in Ifaces)
            { 
                if (item.NetworkInterfaceType.Equals(NetworkInterfaceType.Ethernet) && item.OperationalStatus.Equals(OperationalStatus.Up) && item.Name.Equals("Ethernet") || item.NetworkInterfaceType.Equals(NetworkInterfaceType.Wireless80211) && item.OperationalStatus.Equals(OperationalStatus.Up) && item.Name.Equals("Wi-Fi"))
                { 
                    //Console.WriteLine("Funciona");
                    IPInterfaceProperties IPIProp = item.GetIPProperties();
                    GatewayIPAddressInformationCollection GIPAICollection = IPIProp.GatewayAddresses;
                    if (GIPAICollection != null || GIPAICollection.Count > 0)
                    {
                        foreach (GatewayIPAddressInformation GIPAInformation in GIPAICollection)
                        {
                            label11.Text = GIPAInformation.Address.ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error con la GateWay", "Error", MessageBoxButtons.OK);
                    }
                    foreach (UnicastIPAddressInformation UIPAdres in IPIProp.UnicastAddresses)
                    {
                        if (UIPAdres.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            PingReply pings = new Ping().Send("www.Google.com", 20);
                            if (pings.Status == IPStatus.Success) { label15.Text = "Conection"; label15.BackColor = Color.GreenYellow; }
                            label1.Text = UIPAdres.Address.ToString();
                            label21.Text = item.GetPhysicalAddress().ToString();
                        }
                    }
                }
            }
        }
    }
}
    
