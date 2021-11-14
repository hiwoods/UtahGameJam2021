using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OnlineMode
{
    Relay, IpHost, UnityRelay
}

public class MainMenuManager : MonoBehaviour
{
    private string k_DefaultIP = "127.0.0.1";
    private string k_ConnectPort = "9998";

    private PopupPanel PopupPanel;
    // Start is called before the first frame update
    void Start()
    {
        PopupPanel = FindObjectOfType<PopupPanel>(true);
        PopupPanel.gameObject.SetActive(false);
    }

    public void OnHostClicked()
    {
        PopupPanel.SetupEnterGameDisplay(true, "Host Game",
            (string connectInput, int connectPort, string playerName, OnlineMode onlineMode) =>
            {
                switch (onlineMode)
                {
                    case OnlineMode.Relay:
                        //m_GameNetPortal.StartPhotonRelayHost(connectInput);
                        break;

                    case OnlineMode.IpHost:
                        //m_GameNetPortal.StartHost(PostProcessIpInput(connectInput), connectPort);
                        break;

                    case OnlineMode.UnityRelay:
                        //Debug.Log("Unity Relay Host clicked");
                        //m_GameNetPortal.StartUnityRelayHost();
                        break;
                }
            }, k_DefaultIP, k_ConnectPort);
    }

    public void OnConnectClicked()
    {
        PopupPanel.SetupEnterGameDisplay(false, "Join Game",
            (string connectInput, int connectPort, string playerName, OnlineMode onlineMode) =>
            {
                //m_GameNetPortal.PlayerName = playerName;

                switch (onlineMode)
                {
                    case OnlineMode.Relay:
                        //if (ClientGameNetPortal.StartClientRelayMode(m_GameNetPortal, connectInput, out string failMessage) == false)
                        //{
                        //    m_ResponsePopup.SetupNotifierDisplay("Connection Failed", failMessage, false, true);
                        //    return;
                        //}
                        break;

                    case OnlineMode.IpHost:
                        //ClientGameNetPortal.StartClient(m_GameNetPortal, connectInput, connectPort);
                        break;

                    case OnlineMode.UnityRelay:
                        //Debug.Log($"Unity Relay Client, join code {connectInput}");
                        //m_ClientNetPortal.StartClientUnityRelayModeAsync(m_GameNetPortal, connectInput);
                        break;
                }
                //m_ResponsePopup.SetupNotifierDisplay("Connecting", "Attempting to Join...", true, false);
            }, k_DefaultIP, k_ConnectPort);
    }

    private void SetupEnterGameDisplay(bool v1, string v2, string v3, string v4, string v5, string v6, string v7, Action<string, int, string, OnlineMode> p, string k_DefaultIP, string k_ConnectPort)
    {
        throw new NotImplementedException();
    }

    private string PostProcessIpInput(string ipInput)
    {
        string ipAddress = ipInput;
        if (string.IsNullOrEmpty(ipInput))
        {
            ipAddress = k_DefaultIP;
        }

        return ipAddress;
    }
}
