using Networking;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
    }

    private void OnDestroy()
    {
    }

    public void SelectPlayerCount(int count)
    {
        Debug.Log($"Starting with {count} players");
    }
}
