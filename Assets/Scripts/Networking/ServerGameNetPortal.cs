using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking
{
    public class ServerGameNetPortal : MonoBehaviour
    {
        private GameNetPortal Portal;

        public Dictionary<ulong, int> ClientSceneMap = new Dictionary<ulong, int>();

        // Start is called before the first frame update
        void Start()
        {
            Portal = GetComponent<GameNetPortal>();
        }

        public void OnNetworkReady()
        {
            if (!Portal.NetworkManager.IsServer)
            {
                enabled = false;
            }
            else
            {
                //O__O if adding any event registrations here, please add an unregistration in OnClientDisconnect.
                //Portal.NetworkManager.OnClientDisconnectCallback += OnClientDisconnect;

                NetworkManager.Singleton.SceneManager.LoadScene("GlacierLevel_1", LoadSceneMode.Single);

                if (Portal.NetworkManager.IsHost)
                {
                    //ClientSceneMap[Portal.NetworkManager.LocalClientId] = SceneManager.GetActiveScene().buildIndex;
                }
            }
        }
    }
}