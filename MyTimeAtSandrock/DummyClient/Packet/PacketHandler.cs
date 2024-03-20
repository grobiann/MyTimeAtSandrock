using DummyClient;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System.Diagnostics;

public class PacketHandler
{
    public static void S_EnterGameHandler(PacketSession session, IMessage packet)
    {
        S_EnterGame enterGamePacket = packet as S_EnterGame;

        var player = Managers.ObjectManager.CreatePlayer(enterGamePacket.PlayerInfo);
    }

    public static void S_MoveHandler(PacketSession session, IMessage packet)
    {

    }

    public static void S_LoginHandler(PacketSession session, IMessage packet)
    {
        var serverSession = session as ServerSession;
        S_Login loginPacket = packet as S_Login;

        Debug.Assert(loginPacket.PlayerInfo != null, "계정 생성시 항상 캐릭터를 자동생성하여 내려준다");
        C_EnterGame enterGamePacket = new C_EnterGame();
        serverSession.Send(enterGamePacket);
    }

    //public static void S_ConnectedHandler(ClientSession session, IMessage packet)
    //{
    //    C_Login loginPacket = new C_Login();
    //    loginPacket.Id = new Random().Next();
    //    session.Send(loginPacket);
    //}
}
