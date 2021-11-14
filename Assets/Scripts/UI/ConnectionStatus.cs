using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionStatus : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public Button ConfirmationButton;

    // Start is called before the first frame update
    void Start()
    {
        ConfirmationButton.gameObject.SetActive(false);
    }
}
