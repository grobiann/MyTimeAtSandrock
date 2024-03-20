using System;
using System.Net;
using System.Net.Sockets;
using Google.Protobuf.Protocol;
using ServerCore;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneController : MonoBehaviour
{
    private void Start()
    {
        Managers.TableManager.LoadTextAssets();

        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

        Debug.Log("Connecting.");
        Connector connector = new Connector();
        connector.Connect(endPoint, OnAccept, 1);
    }

    private void OnAccept(Socket socket)
    {
        var session = new ServerSession();
        NetworkManager.PlayerSession = session;

        Debug.Log("Connected.");
        session.Start(socket);

        C_Login loginPacket = new C_Login();
        loginPacket.Id = 9999;  // TODO:
        Managers.NetworkManager.Send(loginPacket);
    }
}
