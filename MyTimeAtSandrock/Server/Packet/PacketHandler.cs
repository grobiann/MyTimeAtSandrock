using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server;
using ServerCore;

public class PacketHandler
{
    public static void C_LoginHandler(PacketSession session, IMessage message)
    {
        ClientSession clientSession = (ClientSession)session;
        Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name, clientSession.Id);

        S_Login login = new S_Login();
        var playerData = new PlayerPacketData();
        playerData.Key = 1;
        playerData.ObjectData = new GameObjectData()
        {
            PosX = 0, PosY = 0, PosZ = 0,
            RotX = 0, RotY = 0, RotZ = 0,
        };
        login.PlayerInfo = playerData;

        clientSession.Send(login);
    }

    public static void C_EnterGameHandler(PacketSession session, IMessage message)
    {
        ClientSession clientSession = (ClientSession)session;
        Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name, clientSession.Id);

        S_EnterGame enterGame = new S_EnterGame();
        clientSession.Send(enterGame);
    }

    public static void C_MoveHandler(PacketSession session, IMessage message)
    {
        ClientSession clientSession = (ClientSession)session;
        Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name, clientSession.Id);
    }
}
