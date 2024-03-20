using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;

namespace DummyClient
{
    public class ServerSession : PacketSession
    {
        public void Send(IMessage packet)
        {
            ushort size = (ushort)packet.CalculateSize();
            ushort protocolld = (ushort)System.Enum.Parse<MessageId>(packet.Descriptor.Name.Replace("_", string.Empty), true);
            byte[] sendBuffer = new byte[size + 4];
            Array.Copy(BitConverter.GetBytes(size + 4), 0, sendBuffer, 0, sizeof(ushort));
            Array.Copy(BitConverter.GetBytes(protocolld), 0, sendBuffer, 2, sizeof(ushort));
            Array.Copy(packet.ToByteArray(), 0, sendBuffer, 4, size);

            base.Send(sendBuffer);
        }

        protected override void OnReceivedPacket(ArraySegment<byte> packet)
        {
            //PacketManager.Instance.OnReceivedPacket(packet);

            //var array = msg.ToArray();
            //ushort size = BitConverter.ToUInt16(array, 0);
            //ushort protocolId = BitConverter.ToUInt16(array, 2);



            //OnReceivedPacket(new ArraySegment<byte>(array, 4, size - 4));
            //Person person2 = new Person();
            //person2.MergeFrom(sendBuffer);//버퍼를 객체로 변경
        }

        public T MakePacket<T>(ArraySegment<byte> message) where T : IMessage, new()
        {
            T packet = new T();
            packet.MergeFrom(message.Array, message.Offset, message.Count);
            return packet;
        }
    }
}



