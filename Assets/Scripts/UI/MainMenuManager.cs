using Networking;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public enum OnlineMode
{
    Relay, IpHost, UnityRelay
}

public class MainMenuManager : MonoBehaviour
{
    private PopupPanel PopupPanel;
    private ConnectionStatus ConnectionStatusPanel;

    private GameNetPortal GameNetPortal;
    private ClientGameNetPortal ClientNetPortal;

    // Start is called before the first frame update
    void Start()
    {
        PopupPanel = FindObjectOfType<PopupPanel>(true);
        PopupPanel.gameObject.SetActive(false);

        ConnectionStatusPanel = FindObjectOfType<ConnectionStatus>(true);

        GameNetPortal = GameObject.FindGameObjectWithTag("GameNetPortal").GetComponent<GameNetPortal>();
        ClientNetPortal = GameNetPortal.GetComponent<ClientGameNetPortal>();

        ClientNetPortal.ConnectFinished += OnConnectFinished;

    }

    private void OnDestroy()
    {
        ClientNetPortal.ConnectFinished -= OnConnectFinished;
    }

    public void OnHostClicked()
    {
        Action<string, int, string> startHostAction = (string ip, int port, string playerName)
        =>
        {
            Debug.Log($"Begin Host at {ip}:{port}");

            ConnectionStatusPanel.Title.text = $"Hosting at {ip}:{port}";
            ConnectionStatusPanel.gameObject.SetActive(true);

            GameNetPortal.StartHost(ip, port);
        };

        PopupPanel.SetupEnterGameDisplay("Host Game", startHostAction);

    }

    public void OnConnectClicked()
    {
        Action<string, int, string> startClientAction = (string ip, int port, string playerName)
            =>
        {
            Debug.Log($"Begin Client at at {ip}:{port}");

            ConnectionStatusPanel.Title.text = $"Connecting to {ip}:{port}";
            ConnectionStatusPanel.gameObject.SetActive(true);

            ClientGameNetPortal.StartClient(GameNetPortal, ip, port);
        };

        PopupPanel.SetupEnterGameDisplay("Join Game", startClientAction);
    }

    private void OnConnectFinished(ConnectStatus status)
    {
        ConnectStatusToMessage(status, true);
    }

    private void ConnectStatusToMessage(ConnectStatus status, bool connecting)
    {
        switch (status)
        {
            case ConnectStatus.Undefined:
            case ConnectStatus.UserRequestedDisconnect:
                break;
            case ConnectStatus.ServerFull:
                ConnectionStatusPanel.Title.text = "Connection Failed";
                break;
            case ConnectStatus.Success:
                ConnectionStatusPanel.Title.text = "Success! Joining Now...";
                break;
            case ConnectStatus.LoggedInAgain:
                ConnectionStatusPanel.Title.text = "Connection Failed. You have logged in elsewhere using the same account";
                break;
            case ConnectStatus.GenericDisconnect:
                ConnectionStatusPanel.Title.text = "Something went wrong";
                break;
        }

        ConnectionStatusPanel.ConfirmationButton.gameObject.SetActive(true);
    }

}
