using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace PacketLogger
{
	class MainClass
	{
		public static UdpClient client;
		public static UdpClient server;


		public static void Main (string[] args)
		{
			// Client to proxy
			client = new UdpClient(5121);


			// server to proxy
			server = new UdpClient(5120);


			Console.WriteLine ("Starting server...");

			client.BeginReceive (new AsyncCallback(clientRecv), null);
			server.BeginReceive (new AsyncCallback(serverRecv), null);

			while(true)
			{
				// Infinitely loop
			}
		}
		
		private static void clientRecv(IAsyncResult result)
		{
			byte[] recv;
			// Receive from client
			IPEndPoint RemoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
			recv = client.EndReceive(result, ref RemoteIPEndPoint);
			// Immediately begin listening for incoming packets again
			client.BeginReceive (new AsyncCallback(clientRecv), null);
			// Forward onto server
			Console.Write ("Client @" + RemoteIPEndPoint.Address.ToString() + ":" + RemoteIPEndPoint.Port.ToString() + " ");

			foreach(byte b in recv)
			{
				Console.Write(b + " ");
			}
			Console.WriteLine (" ");

			server.Send (recv, recv.Length, "127.0.0.1", 5122);

		}

		private static void serverRecv(IAsyncResult result)
		{
			byte[] recv;
			// Receive from client
			IPEndPoint RemoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
			recv = server.EndReceive (result, ref RemoteIPEndPoint);
			// Immediately begin listening for incoming packets again
			server.BeginReceive (new AsyncCallback(clientRecv), null);
			// Forward onto server
			Console.Write ("Server @" + RemoteIPEndPoint.Address.ToString() + ":" + RemoteIPEndPoint.Port.ToString() + " ");
			foreach(byte b in recv)
			{
				Console.Write(b + " ");
			}
			Console.WriteLine (" ");

			client.Send (recv, recv.Length, "127.0.0.1", 5119);

		}
	}
}
