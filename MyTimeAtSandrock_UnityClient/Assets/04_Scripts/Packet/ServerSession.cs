using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Diagnostics;
using UnityEditor.VersionControl;

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

    protected override void OnReceivedPacket(ArraySegment<byte> message)
    {
        MessageId messageId;
        IMessage packet;
        if (PacketManager.Instance.TryMakePacket(message, out messageId, out packet))
        {
            NetworkManager.Instance.Enqueue(messageId, packet);
            return;
        }

        UnityEngine.Debug.LogError("Packet is null");
    }

    public T MakePacket<T>(ArraySegment<byte> message) where T : IMessage, new()
    {
        T packet = new T();
        packet.MergeFrom(message.Array, message.Offset, message.Count);
        return packet;
    }
}