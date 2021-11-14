using Networking;
using System;
using UnityEngine;

public enum OnlineMode
{
    Relay, IpHost, UnityRelay
}

public class MainMenuManager : MonoBehaviour
{
    private PopupPanel PopupPanel;
    private GameNetPortal GameNetPortal;

    // Start is called before the first frame update
    void Start()
    {
        PopupPanel = FindObjectOfType<PopupPanel>(true);
        PopupPanel.gameObject.SetActive(false);

        GameNetPortal = GameObject.FindGameObjectWithTag("GameNetPortal").GetComponent<GameNetPortal>();
    }

    public void OnHostClicked()
    {
        Action<string, int, string> startHostAction = (string ip, int port, string playerName)
        =>
        {
            Debug.Log($"Begin Host at {ip}:{port}");
            //GameNetPortal.StartHost(ip, port);
        };

        PopupPanel.SetupEnterGameDisplay("Host Game", startHostAction);

    }

    public void OnConnectClicked()
    {
        Action<string, int, string> startClientAction = (string ip, int port, string playerName)
            =>
        {
            Debug.Log($"Begin Client at at {ip}:{port}");
            //ClientGameNetPortal.StartClient(GameNetPortal, connectInput, connectPort);
        };

        PopupPanel.SetupEnterGameDisplay("Join Game", startClientAction);
    }
}
