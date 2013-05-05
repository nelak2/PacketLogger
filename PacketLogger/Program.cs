using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace PacketLogger
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Starting server...");
			var Proxy = new Proxy();
			Thread client = new Thread(Proxy.ListenClient);
			Thread server = new Thread(Proxy.ListenServer);

			client.Start ();
			server.Start ();


			while(true)
			{
			}
		}

	}

	class Proxy
	{
		public static UdpClient client;
		public static UdpClient server;

		public Proxy()
		{
			// Client to proxy
			client = new UdpClient(5121);
			
			// server to proxy
			server = new UdpClient(5120);
		}

		public void ListenClient()
		{
			while(true)
			{
			byte[] data;
			var srcAdd = new IPEndPoint(IPAddress.Any, 0);
			data = client.Receive (ref srcAdd);

			Console.Write ("Client @" + srcAdd.Address.ToString() + ":" + srcAdd.Port.ToString() + " ");
			foreach(byte b in data)
			{
				Console.Write(b + " ");
			}
			Console.WriteLine (" ");

			server.Send(data, data.Length, "127.0.0.1", 5122);
			}
		}

		public void ListenServer()
		{
			while(true)
			{
			byte[] data;
			var srcAdd = new IPEndPoint(IPAddress.Any, 0);
			data = server.Receive (ref srcAdd);
			
			Console.Write ("Server @" + srcAdd.Address.ToString() + ":" + srcAdd.Port.ToString() + " ");
			foreach(byte b in data)
			{
				Console.Write(b + " ");
			}
			Console.WriteLine (" ");
			
			client.Send(data, data.Length, "127.0.0.1", 5119);
			}
		}
	}
}
