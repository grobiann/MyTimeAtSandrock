using System;
using System.Net;
using System.Net.Sockets;

namespace ServerCore
{
    public class Listener
    {
        private Socket _listenSocket;
        private Action<Socket> _onAcceptHandler;

        public void Init(IPEndPoint endPoint, Action<Socket> onAcceptHandler)
        {
            _onAcceptHandler = onAcceptHandler;

            _listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _listenSocket.Bind(endPoint);
            _listenSocket.Listen(10);

            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAccept);
            RegisterAccept(args);
        }

        private void RegisterAccept(SocketAsyncEventArgs args)
        {
            args.AcceptSocket = null;
            bool pending = _listenSocket.AcceptAsync(args);
            if (pending == false)
            {
                OnAccept(null, args);
            }
        }

        private void OnAccept(object sender, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                _onAcceptHandler.Invoke(args.AcceptSocket);
         
                RegisterAccept(args);
            }
            else
            {
                Console.WriteLine("[Accept Failed]" + args.SocketError.ToString());
            }
        }
    }
}
