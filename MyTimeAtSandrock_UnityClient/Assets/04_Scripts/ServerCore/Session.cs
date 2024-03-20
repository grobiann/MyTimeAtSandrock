using System;
using System.Net.Sockets;
using System.Text;

namespace ServerCore
{
    public abstract class PacketSession : Session
    {
        sealed protected override void OnReceived(ArraySegment<byte> message)
        {
            int count = 0;
            while (count < message.Count)
            {
                int dataLength = BitConverter.ToUInt16(message.Array, message.Offset);
                OnReceivedPacket(new ArraySegment<byte>(message.Array, message.Offset, dataLength));
                count += dataLength;
            }
        }

        protected abstract void OnReceivedPacket(ArraySegment<byte> packet);
    }

    public abstract class Session
    {
        private Socket _socket;

        private ReceiveBuff _receiveBuff = new ReceiveBuff(65535);
        SocketAsyncEventArgs _sendArgs = new SocketAsyncEventArgs();
        SocketAsyncEventArgs _recvArgs = new SocketAsyncEventArgs();

        private bool _disconnected;

        public void Start(Socket socket)
        {
            _socket = socket;

            _recvArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnReceiveCompleted);
            _sendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnSendCompleted);
            RegisterReceive(_recvArgs);
        }

        private void RegisterReceive(SocketAsyncEventArgs args)
        {
            var writeSegment = _receiveBuff.GetWriteSegment();
            _recvArgs.SetBuffer(writeSegment.Array, writeSegment.Offset, writeSegment.Count);
            bool pending = _socket.ReceiveAsync(_recvArgs);
            if (pending == false)
            {
                OnReceiveCompleted(null, _recvArgs);
            }
        }

        private void OnReceiveCompleted(object sender, SocketAsyncEventArgs args)
        {
            if (_disconnected)
                return;

            if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
            {
                Console.WriteLine("Received: " + args.BytesTransferred);
                _receiveBuff.OnWrite(args.BytesTransferred);

                ArraySegment<byte> data = _receiveBuff.GetDataSegment();
                _receiveBuff.OnRead(data.Count);

                OnReceived(data);

                RegisterReceive(args);
            }
            else
            {
                Console.WriteLine("[Receive Failed] " + args.SocketError.ToString());
                Disconnect();
            }
        }

        public void Send(ArraySegment<byte> data)
        {
            if (_disconnected)
                return;

            _sendArgs.SetBuffer(data.Array, data.Offset, data.Count);
            bool pending = _socket.SendAsync(_sendArgs);
            if (pending == false)
            {
                OnSendCompleted(null, _sendArgs);
            }
        }

        private void OnSendCompleted(object sender, SocketAsyncEventArgs args)
        {
            if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
            {
                Console.WriteLine("Send: " + args.BytesTransferred);
            }
            else
            {
                Console.WriteLine("[Send Failed] " + args.SocketError.ToString());
                Disconnect();
            }
        }

        public void Disconnect()
        {
            _disconnected = true;
            Console.WriteLine("Disconnected");
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        protected abstract void OnReceived(ArraySegment<byte> message);
    }
}
