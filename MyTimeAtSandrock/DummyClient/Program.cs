using Google.Protobuf.Protocol;
using ServerCore;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DummyClient
{
    public static class Program
    {
        private static ServerSession _session;

        private static void OnAccept(Socket socket)
        {
            _session = new ServerSession();
            _session.Start(socket);
            Console.WriteLine("Connected.");

            while (true)
            {
                string cmd = string.Empty;
                byte[] receiveBuff = new byte[8192];

                for(int i = 0; i < 5; i++)
                {
                    byte[] buff = Encoding.UTF8.GetBytes("test");
                    _session.Send(buff);
                    Thread.Sleep(1000);
                }
                while ((cmd = Console.ReadLine()) != "Q")
                {
                    //int n2 = socket.Receive(receiveBuff);
                    //string data2 = Encoding.UTF8.GetString(receiveBuff, 0, n2);
                    //Console.WriteLine("Received2 - " + data2);

                    C_Login msg = new C_Login();
                    msg.Id = 5;
                    //byte[] buff = Encoding.UTF8.GetBytes(cmd);
                    _session.Send(msg);
                    //socket.Send(buff, SocketFlags.None);

                    //int n = socket.Receive(receiveBuff);
                    //string data = Encoding.UTF8.GetString(receiveBuff, 0, n);
                    //Console.WriteLine("Received - " + data);
                }

                Thread.Sleep(100);
            }
        }

        public static void Main()
        {
            Thread.Sleep(1000);

            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            Console.WriteLine("Connecting.");
            Connector connector = new Connector();
            connector.Connect(endPoint, OnAccept, 1);
            
            while(true)
            {

            }

            //Console.WriteLine("Closed");
            //socket.Close();
        }
    }
}
