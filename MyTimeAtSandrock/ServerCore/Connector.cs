using System;
using System.Net;
using System.Net.Sockets;

namespace ServerCore
{
    public class Connector
    {
        private Action<Socket> _socketHandler;

        public void Connect(IPEndPoint endPoint, Action<Socket> socketHandler, int count = 1)
        {
            _socketHandler = socketHandler;

            for (int i = 0; i < count; i++)
            {
                Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += new EventHandler<SocketAsyncEventArgs>(OnConnect);
                args.RemoteEndPoint = endPoint;
                args.UserToken = socket;
                RegisterConnect(args);
            }
        }

        private void RegisterConnect(SocketAsyncEventArgs args)
        {
            var socket = args.UserToken as Socket;
            bool pending = socket.ConnectAsync(args);
            if (pending == false)
            {
                OnConnect(null, args);
            }
        }

        private void OnConnect(object sender, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                _socketHandler.Invoke(args.ConnectSocket);
            }
            else
            {
                Console.WriteLine("[Connect Failed]" + args.SocketError.ToString());
            }
        }
    }
}
