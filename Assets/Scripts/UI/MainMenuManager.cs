using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void SelectPlayerCount(int count)
    {
        GlobalBlackboard.Instance.playerCount = count;
        Debug.Log($"Starting with {count} players");
    }
}
