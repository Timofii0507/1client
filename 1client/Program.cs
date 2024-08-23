using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main(string[] args)
    {
        IPAddress ipAddress = IPAddress.Parse("192.168.0.1");
        int port = 11000;
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

        Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            sender.Connect(remoteEP);

            Console.WriteLine($"Connected to {sender.RemoteEndPoint.ToString()}");

            byte[] msg = Encoding.UTF8.GetBytes("Hello, server!");
            int bytesSent = sender.Send(msg);
            byte[] bytes = new byte[1024];
            int bytesRec = sender.Receive(bytes);
            string data = Encoding.UTF8.GetString(bytes, 0, bytesRec);

            Console.WriteLine($"At {DateTime.Now.ToShortTimeString()} received from {((IPEndPoint)sender.RemoteEndPoint).Address}: {data}");

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}