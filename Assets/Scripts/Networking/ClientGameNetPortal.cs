using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using UnityEngine;

namespace Networking
{
    public class ClientGameNetPortal : MonoBehaviour
    {
        private GameNetPortal Portal;

        void Start()
        {
            Portal = GetComponent<GameNetPortal>();

            //Portal.NetworkManager.OnClientDisconnectCallback += OnDisconnectOrTimeout;
        }


        public void OnConnectFinished(ConnectStatus status)
        {
            //throw new NotImplementedException();
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
        }
    }
}