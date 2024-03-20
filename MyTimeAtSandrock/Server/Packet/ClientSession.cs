using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;

namespace Server
{
    public class ClientSession : PacketSession
    {
        public int Id { get; set; }

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

        protected override void OnReceivedPacket(ArraySegment<byte> message)
        {
            MessageId messageId;
            IMessage packet;
            if (PacketManager.Instance.TryMakePacket(message, out messageId, out packet))
            {
                PacketManager.Instance.HandlePacket(this, messageId, packet);
                return;
            }

            Console.WriteLine("Packet is null");
        }
    }
}



