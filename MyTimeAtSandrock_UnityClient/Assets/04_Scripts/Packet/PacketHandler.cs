using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PacketHandler
{
    public static void S_LoginHandler(PacketSession session, IMessage message)
    {
        ServerSession serverSession = (ServerSession)session;
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);

        S_Login loginPacket = message as S_Login;

        Debug.Assert(loginPacket.PlayerInfo != null, "계정 생성시 항상 캐릭터를 자동생성하여 내려준다");
        C_EnterGame enterGamePacket = new C_EnterGame();
        enterGamePacket.Key = 1;

        serverSession.Send(enterGamePacket);
    }

    public static void S_EnterGameHandler(PacketSession session, IMessage message)
    {
        ServerSession serverSession = (ServerSession)session;
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);

        S_EnterGame enterGamePacket = message as S_EnterGame;

        // 월드(지형) 로드
        SceneManager.LoadScene("Ingame");
    }

    public static void S_MoveHandler(PacketSession session, IMessage message)
    {
        ServerSession serverSession = (ServerSession)session;
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public static void S_LeaveGameHandler(PacketSession session, IMessage message)
    {
        ServerSession serverSession = (ServerSession)session;
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public static void S_ConnectedHandler(PacketSession session, IMessage message)
    {
        ServerSession serverSession = (ServerSession)session;
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);

        C_Login loginPacket = new C_Login();

        loginPacket.Id = new System.Random().Next();
        serverSession.Send(loginPacket);
    }
}
