using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void SelectPlayerCount(int count)
    {
        GlobalBlackboard.Instance.playerCount = count;

        SceneManager.LoadScene("GlacierLevel_1");

        Debug.Log($"Starting with {count} players");
    }
}
