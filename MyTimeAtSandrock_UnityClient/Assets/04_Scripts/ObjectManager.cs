using Google.Protobuf.Protocol;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectManager
{
    public PlayerController MyPlayer;

    public GameObjectController CreatePlayer()
    {
        int key = 10001;
        CharacterInfo playerInfo = Managers.TableManager.Characters[key];

        var res = Managers.PoolManager.GetPoolable(playerInfo.resourcePath);
        PlayerController player = res.GetComponent<PlayerController>();
        player.ObjectData = new GameObjectData();
        player.transform.position = player.ObjectData.GetPosition();
        MyPlayer = player;
        return player;
    }
}


public static class ProtocolExtentionMethods
{
    public static Vector3 GetPosition(this GameObjectData data)
    {
        return new Vector3(data.PosX, data.PosY, data.PosZ);
    }
}