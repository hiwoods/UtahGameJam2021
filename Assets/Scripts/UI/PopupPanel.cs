using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupPanel : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public Button ConfirmationButton;

    public TMP_InputField IpInput;
    public TMP_InputField PortInput;

    public delegate void ConfirmDelegate(string ip, int port, string playerName);

    public ConfirmDelegate ConfirmCallback;

    private void Start()
    {
        ConfirmationButton.onClick.AddListener(OnConfirmClicked);
    }

    private void OnDestroy()
    {
        ConfirmationButton?.onClick.RemoveListener(OnConfirmClicked);
    }

    private void OnConfirmClicked()
    {
        ConfirmCallback?.Invoke(GetIp(), GetPort(), "Test Player");
    }

    private string GetIp()
    {
        if (!string.IsNullOrWhiteSpace(IpInput.text))
            return IpInput.text;

        var ip = IpInput.placeholder as TextMeshProUGUI;

        return ip.text;
    }

    private int GetPort()
    {
        string portStr; 
        if (!string.IsNullOrWhiteSpace(PortInput.text))
        {
            portStr = PortInput.text;
        }
        else
        {
            var port = PortInput.placeholder as TextMeshProUGUI;

            portStr = port.text;
        }

        return int.Parse(portStr);
    }

    public void SetupEnterGameDisplay(string title, Action<string, int, string> p)
    {
        Title.text = title;

        ConfirmCallback = new ConfirmDelegate(p);

        gameObject.SetActive(true);
    }
}
