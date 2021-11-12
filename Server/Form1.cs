using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public partial class Form1 : Form
    {
        //initilize socket
        Socket serverSocket;
        IPEndPoint serverAddress;
        Thread thread;
        void startServer()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            
            serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6767);
            //bind : make ip address contained ip and port number
            serverSocket.Bind(serverAddress);
            //ThreadStart(Listning) run lisning method in that thread
            thread = new Thread(new ThreadStart(Listening));
            //make server start to listen
            thread.Start();

        }
        bool isRunning = false;
        void Listening()
        {
            while (isRunning)
            {
                byte[] buffer = new byte[4096];
                //ip address any to recieve from any ip address and port 0 to recieve from any port number
                EndPoint clientAddress = new IPEndPoint(IPAddress.Any, 0);
                //receive from client address and put data on buffer
                int byteCount = serverSocket.ReceiveFrom(buffer, ref clientAddress);
                String msg = Encoding.ASCII.GetString(buffer, 0, byteCount);
                
              
                showMesseges.Invoke(new Action(delegate
                {
                    showMesseges.AppendText(clientAddress.ToString() + " : " + msg + "\n");
                }));
            }

        }
        void stopServer()
        {

            isRunning = false;
            //stop thread
            thread.Abort();
            //close socket
            serverSocket.Close();

        }
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "Dissconnected";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void stop_Click(object sender, EventArgs e)
        {

            start.Enabled = true;
            stop.Enabled = false;
            textBox1.Text = "dissconnected";
            textBox1.ForeColor = Color.Red;
            stopServer();
            
        }

        private void start_Click(object sender, EventArgs e)
        {
            start.Enabled = false;
            stop.Enabled = true;
            textBox1.Text = "Connected";
            textBox1.ForeColor = Color.Green;
            startServer();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
