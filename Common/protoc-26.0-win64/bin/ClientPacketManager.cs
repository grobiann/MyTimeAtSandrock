using System;
using System.Collections.Generic;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System.Diagnostics;

public class PacketManager
{
    public static PacketManager Instance = new PacketManager();
    public PacketManager()
    {
        Init();
    }

    private Dictionary<MessageId, Func<ArraySegment<byte>, IMessage>> _packetFactory = new Dictionary<MessageId, Func<ArraySegment<byte>, IMessage>>();
    private Dictionary<MessageId, System.Action<PacketSession, IMessage>> _packetHandler = new Dictionary<MessageId, Action<PacketSession, IMessage>>();
    
    public void Init()
    {
        _packetFactory.Add(MessageId.SLogin, MakePacket<S_Login>);
        _packetHandler.Add(MessageId.SLogin, PacketHandler.S_LoginHandler);
        _packetFactory.Add(MessageId.SEnterGame, MakePacket<S_EnterGame>);
        _packetHandler.Add(MessageId.SEnterGame, PacketHandler.S_EnterGameHandler);
        _packetFactory.Add(MessageId.SMove, MakePacket<S_Move>);
        _packetHandler.Add(MessageId.SMove, PacketHandler.S_MoveHandler);         
    }

    public void HandlePacket(PacketSession session, MessageId messageId, IMessage packet)
    {
        Debug.Assert(_packetHandler.ContainsKey(messageId));
        _packetHandler[messageId].Invoke(session, packet);
    }

    public bool TryMakePacket(ArraySegment<byte> message, out MessageId messageId, out IMessage packet)
    {
        ushort size = BitConverter.ToUInt16(message.Array, message.Offset);
        ushort protocolId = BitConverter.ToUInt16(message.Array, message.Offset + 2);
        messageId = (MessageId)protocolId;
        if (_packetFactory.TryGetValue(messageId, out var factory))
        {
            packet = factory.Invoke(message);
            return true;
        }

        packet = null;
        return false;
    }

    private IMessage MakePacket<T>(ArraySegment<byte> message) where T : IMessage, new()
    {
        ushort size = BitConverter.ToUInt16(message.Array, message.Offset);

        T packet = new T();
        packet.MergeFrom(message.Array, message.Offset + 4, size - 4);
        return packet;
    }
}