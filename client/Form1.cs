using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace client
{
    public partial class Form1 : Form
    {
        Socket clientSocket;
        IPEndPoint serverIpAddress;
        public Form1()
        {
            InitializeComponent();
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            serverIpAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"),6767);
        }

        private void send_Click(object sender, EventArgs e)
        {
            if (messege.Text.Trim()==null)
            {
                MessageBox.Show("empty");
                messege.Focus();
                return;
            }
            byte[] buffer = Encoding.ASCII.GetBytes(messege.Text.Trim());
            clientSocket.SendTo(buffer, serverIpAddress);
            messege.Text = "";
        }
    }
}
