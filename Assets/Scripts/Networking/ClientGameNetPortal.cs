using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking
{
    public class ClientGameNetPortal : MonoBehaviour
    {
        private GameNetPortal Portal;

        public static ClientGameNetPortal Instance;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            Portal = GetComponent<GameNetPortal>();

            //Portal.NetworkManager.OnClientDisconnectCallback += OnDisconnectOrTimeout;
        }

        public event Action<ConnectStatus> ConnectFinished;

        public void OnConnectFinished(ConnectStatus status)
        {
            Debug.Log($"Client connected: Status {status}");

            ConnectFinished?.Invoke(status);
        }

        public void OnNetworkReady()
        {
            enabled = Portal.NetworkManager.IsClient;
        }

        public static void StartClient(GameNetPortal gameNetPortal, string ipAddress, int connectPort)
        {
            var chosenTransport = NetworkManager.Singleton.NetworkConfig.NetworkTransport as UNetTransport;

            chosenTransport.ConnectAddress = ipAddress;
            chosenTransport.ServerListenPort = connectPort;

            ConnectClient(gameNetPortal);
        }

        private static string GetClientGuid()
        {
            if (PlayerPrefs.HasKey("client_guid"))
            {
                return PlayerPrefs.GetString("client_guid");
            }

            var guidString = Guid.NewGuid().ToString();

            PlayerPrefs.SetString("client_guid", guidString);
            return guidString;
        }

        private static void ConnectClient(GameNetPortal portal)
        {
            var clientGuid = GetClientGuid();

            var payload = JsonUtility.ToJson(new
            {
                clientGUID = clientGuid,
                clientScene = SceneManager.GetActiveScene().buildIndex,
                playerName = portal.PlayerName
            });

            portal.NetworkManager.NetworkConfig.ConnectionData = Encoding.UTF8.GetBytes(payload);
            portal.NetworkManager.NetworkConfig.ClientConnectionBufferTimeout = 10;

            //and...we're off! Netcode will establish a socket connection to the host.
            //  If the socket connection fails, we'll hear back by getting an OnClientDisconnect callback for ourselves and get a message telling us the reason
            //  If the socket connection succeeds, we'll get our RecvConnectFinished invoked. This is where game-layer failures will be reported.
            portal.NetworkManager.StartClient();

            // should only do this once StartClient has been called (start client will initialize CustomMessagingManager
            NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler(nameof(ReceiveServerToClientConnectResult), ReceiveServerToClientConnectResult);
            NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler(nameof(ReceiveServerToClientSetDisconnectReason), ReceiveServerToClientSetDisconnectReason);
        }

        public static void ReceiveServerToClientConnectResult(ulong clientID, FastBufferReader reader)
        {
            reader.ReadValueSafe(out ConnectStatus status);
            Instance.OnConnectFinished(status);
        }

        public static void ReceiveServerToClientSetDisconnectReason(ulong clientID, FastBufferReader reader)
        {
            reader.ReadValueSafe(out ConnectStatus status);
            Instance.OnDisconnectReasonReceived(status);
        }

        private void OnDisconnectReasonReceived(ConnectStatus status)
        {
            Debug.Log($"Client Disconnected. Status {status}");
        }
    }
}