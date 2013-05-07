using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacketLoggerGUI
{
    public partial class Form1 : Form
    {
        Proxy proxy;
        Thread client;
        Thread server;
        public List<byte[]> serverLog;
        public List<byte[]> clientLog;

        public Form1()
        {
            InitializeComponent();

            serverLog = new List<byte[]>();
            clientLog = new List<byte[]>();

            proxy = new Proxy(lstbx_Server, lstbx_Client, ref serverLog, ref clientLog);
            client = new Thread(proxy.ListenClient);
            server = new Thread(proxy.ListenServer);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            client.Start();
            server.Start();
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            client.Abort();
            server.Abort();
        }

        private void lstbx_Server_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstbx_Server.SelectedIndex;
            byte[] data = serverLog[index];
            string line = "";

            foreach (byte b in data)
            {
                line = line + " " + b;
            }
            lblServerPacket.Text = line;
            lblServerASCII.Text = Encoding.ASCII.GetString(data);
        }

        private void lstbx_Client_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstbx_Client.SelectedIndex;
            byte[] data = clientLog[index];
            string line = "";

            foreach (byte b in data)
            {
                line = line + " " + b;
            }
            lblClientPacket.Text = line;
            lblClientASCII.Text = Encoding.ASCII.GetString(data);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
