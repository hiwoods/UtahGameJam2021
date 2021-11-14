using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using UnityEngine;

namespace Networking
{
    public class GameNetPortal : MonoBehaviour
    {
        public string PlayerName = "Test Player Name";
        public static GameNetPortal Instance;

        public NetworkManager NetworkManager;
        private ClientGameNetPortal ClientPortal;
        private ServerGameNetPortal ServerPortal;

        private void Awake()
        {
            Instance = this;
            NetworkManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>();
            ClientPortal = GetComponent<ClientGameNetPortal>();
            ServerPortal = GetComponent<ServerGameNetPortal>();
        }

        void Start()
        {
            DontDestroyOnLoad(gameObject);

            //we synthesize a "OnNetworkSpawn" event for the NetworkManager out of existing events. At some point
            //we expect NetworkManager will expose an event like this itself.
            NetworkManager.OnServerStarted += OnNetworkReady;
            NetworkManager.OnClientConnectedCallback += ClientNetworkReadyWrapper;
        }

        private void OnDestroy()
        {
            if (NetworkManager != null)
            {
                NetworkManager.OnServerStarted -= OnNetworkReady;
                NetworkManager.OnClientConnectedCallback -= ClientNetworkReadyWrapper;
            }
        }

        private void OnNetworkReady()
        {
            if (NetworkManager.IsHost)
            {
                //special host code. This is what kicks off the flow that happens on a regular client
                //when it has finished connecting successfully. A dedicated server would remove this.
                ClientPortal.OnConnectFinished(ConnectStatus.Success);
            }

            ClientPortal.OnNetworkReady();
            ServerPortal.OnNetworkReady();

        }

        private void ClientNetworkReadyWrapper(ulong clientId)
        {
            if (clientId == NetworkManager.LocalClientId)
            {
                OnNetworkReady();
                //NetworkManager.SceneManager.OnSceneEvent += OnSceneEvent;
            }
        }

        public void StartHost(string ipaddress, int port)
        {
            var transport = NetworkManager.Singleton.NetworkConfig.NetworkTransport as UnityTransport;

            transport.SetConnectionData(ipaddress, (ushort)port);

            NetworkManager.StartHost();
        }
    }
}