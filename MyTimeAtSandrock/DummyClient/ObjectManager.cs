using Google.Protobuf.Protocol;

namespace DummyClient
{
    public class ObjectManager
    {
        public List<PlayerController> MyPlayers = new List<PlayerController>();

        public GameObjectController CreatePlayer(PlayerPacketData info)
        {
            PlayerController player = new PlayerController();
            player.PlayerInfo = info;
            MyPlayers.Add(player);
            return player;
        }
    }
}
