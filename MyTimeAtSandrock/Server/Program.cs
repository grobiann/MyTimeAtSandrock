using ServerCore;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public static class Program
    {
        public static GameRoom GameRoom;

        public static void NetworkLogic()
        {   
        }

        public static void GameLogic()
        {
            GameRoom = new GameRoom();
            
            while (true)
            {
                GameRoom.Update();
            }
        }

        private static Listener _listener = new Listener();

        private static Random sessionIdGenerator = new Random();
        private static void OnAccept(Socket socket)
        {
            ClientSession session = new ClientSession();
            session.Start(socket);
            session.Id = sessionIdGenerator.Next();


            //byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to MyTimeAtSandrock!");
            //session.Send(sendBuff);
        }

        static void Main()
        {
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            _listener.Init(endPoint, OnAccept);
            Console.WriteLine("Listening...");

            NetworkLogic();
            GameLogic();
            
            //while(true)
            //{
            //    Thread.Sleep(0);
            //}
        }
    }

    public class GameRoom
    {
        int i = 0;

        public void Update()
        {
            i++;
            //Console.WriteLine("hello " + i);
        }
    }
}

