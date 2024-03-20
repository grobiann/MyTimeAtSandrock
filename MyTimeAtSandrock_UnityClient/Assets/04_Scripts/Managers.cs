using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static NetworkManager NetworkManager = new NetworkManager();
    public static ObjectManager ObjectManager = new ObjectManager();
    public static TableManager TableManager = new TableManager();
    public static PoolManager PoolManager = new PoolManager();
    public static PacketManager PacketManager = new PacketManager();
}
