using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace PacketLogger
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			// Client to proxy (listen)
			var clientToProxy = new UdpClient(5121);
			var clientToProxyEP = new IPEndPoint(IPAddress.Any, 5121);

			// server to proxy (listen)
			var serverToProxy = new UdpClient(5119);
			var serverToProxyEP = new IPEndPoint(IPAddress.Any, 5119);

			// Proxy to server (send)
			var proxyToServer = new UdpClient(5119);
			var proxyToServerEP = new IPEndPoint(IPAddress.Parse ("127.0.0.1"), 5119);

			var proxyToClient = new UdpClient(5121);
			var proxyToClientEP = new IPEndPoint(IPAddress.Parse("192.168.0.198"), 5121);


			Console.WriteLine ("Starting server...");

			byte[] clientMessage;
			string ascii;


			clientToProxy.BeginReceive (
			while(true)
			{
				// receive from client
				clientMessage = clientToProxy.Receive(ref clientToProxyEP);

				if(clientMessage != null)
				{
					Console.WriteLine ("Client to server");
					ascii = Encoding.ASCII.GetString (clientMessage);
					
					foreach(byte b in clientMessage)
					{
						Console.Write (b);
						Console.Write (" ");
					}
					Console.WriteLine (" ");
					Console.WriteLine (ascii);
				}

			}




		}
	}
}
