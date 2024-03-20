using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;
    public static ServerSession PlayerSession;

    public Queue<(MessageId messageId, IMessage packet)> PacketQueue
        = new Queue<(MessageId messageId, IMessage packet)>();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        while(PacketQueue.Count > 0)
        {
            var item = PacketQueue.Dequeue();
            PacketManager.Instance.HandlePacket(PlayerSession, item.messageId, item.packet);
        }
    }

    public void Enqueue(MessageId id, IMessage message)
    {
        PacketQueue.Enqueue((id, message));
    }

    public void Send(IMessage packet)
    {
        PlayerSession.Send(packet);
    }
}
