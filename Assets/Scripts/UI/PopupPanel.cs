using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupPanel : MonoBehaviour
{
    public TextMeshProUGUI Title;

    public void SetupEnterGameDisplay(bool isHost, string title, Action<string, int, string, OnlineMode> p, string k_DefaultIP, string k_ConnectPort)
    {
        Title.text = title;
        gameObject.SetActive(true);
    }
}
