using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking.Server
{
    public enum WinState
    { 
        NotStarted
    }

    public class GlacierLevel_1State : GameStateBehaviour
    {
        public override GameState ActiveState => GameState.Glacier;

        public Transform[] PlayerSpawnLocations_Walrus;

        private GameNetPortal NetPortal;
        private ServerGameNetPortal ServerNetPortal;
        private WinState WinState;

        public override void OnNetworkSpawn()
        {
            if (!IsServer)
            {
                enabled = false;
            }
            else
            {
                // reset win state
                SetWinState(WinState.NotStarted);

                NetPortal = GameObject.FindGameObjectWithTag("GameNetPortal").GetComponent<GameNetPortal>();
                ServerNetPortal = NetPortal.GetComponent<ServerGameNetPortal>();

                NetworkManager.SceneManager.OnSceneEvent += OnClientSceneChanged;

                SpawnPlayer();
            }
        }

        private void SetWinState(WinState notStarted)
        {
            WinState = notStarted;
        }

        private bool SpawnPlayer()
        {
            foreach (var kvp in NetworkManager.ConnectedClients)
            {
                SpawnPlayer(kvp.Key, false);
            }

            return true;
        }

        private void SpawnPlayer(ulong clientId, bool lateJoin)
        {

        }

        public void OnClientSceneChanged(SceneEvent sceneEvent)
        {
        }

    }
}
