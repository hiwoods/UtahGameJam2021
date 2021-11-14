///
///
///
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class MainLogic : GenericSingletonClass<MainLogic>
{
    //function to check playercount on ice
    //if playercount is 1 or less then end the game.
    //tell which player won/ have a win and loose screen.

    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject looseUI;
    [SerializeField] private GameObject endUILocal;
    [SerializeField] private TMP_Text endUIMessage;

    [SerializeField] private bool ranOuttaTime = false;

    public void AddPlayerOnIce()
    {
        GlobalBlackboard.Instance.playersOnIce++;
    }

    public void RemovePlayerFromIce()
    {
        GlobalBlackboard.Instance.playersOnIce--;

        if(GlobalBlackboard.Instance.playersOnIce <= 1)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        //disable all characters
        foreach(CharacterControllerSumo sumo in Object.FindObjectsOfType<CharacterControllerSumo>())
        {
            sumo.gameObject.SetActive(false);
        }

        int winningPlayer = 0;

        foreach (LocalBlackboard player in Object.FindObjectsOfType<LocalBlackboard>())
        {
            if (player.currentReincarnation != 2)
            {
                winningPlayer = player.controllerSet;
                break;
            }
        }


        if (ranOuttaTime)
        {
            endUILocal.SetActive(true);
            endUIMessage.text = "Player " + winningPlayer + " wins!\nThe rest of you can shove off.";
        }
        else
        {
            bool won = false;
            won = (winningPlayer == GlobalBlackboard.Instance.myPlsyerID);


            if (won)
            {
                winUI.SetActive(true);
            }
            else
            {
                looseUI.SetActive(true);
            }
        }
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }
}
