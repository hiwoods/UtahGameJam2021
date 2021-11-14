using System;
using System.Text;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking
{
    public enum GameState
    {
        MainMenu,
        Glacier,
        PostGame
    }

    public abstract class GameStateBehaviour : NetworkBehaviour
    {
        /// <summary>
        /// Does this GameState persist across multiple scenes?
        /// </summary>
        public virtual bool Persists

        {
            get { return false; }
        }

        /// <summary>
        /// What GameState this represents. Server and client specializations of a state should always return the same enum.
        /// </summary>
        public abstract GameState ActiveState { get; }

        /// <summary>
        /// This is the single active GameState object. There can be only one.
        /// </summary>
        private static GameObject ActiveStateGO;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            if (ActiveStateGO != null)
            {
                if (ActiveStateGO == gameObject)
                {
                    //nothing to do here, if we're already the active state object.
                    return;
                }

                //on the host, this might return either the client or server version, but it doesn't matter which;
                //we are only curious about its type, and its persist state.
                var previousState = ActiveStateGO.GetComponent<GameStateBehaviour>();

                if (previousState.Persists && previousState.ActiveState == ActiveState)
                {
                    //we need to make way for the DontDestroyOnLoad state that already exists.
                    Destroy(gameObject);
                    return;
                }

                //otherwise, the old state is going away. Either it wasn't a Persisting state, or it was,
                //but we're a different kind of state. In either case, we're going to be replacing it.
                Destroy(ActiveStateGO);
            }

            ActiveStateGO = gameObject;
            if (Persists)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            if (!Persists)
            {
                ActiveStateGO = null;
            }
        }
    }
}