using UnityEngine;
using UnityEngine.SceneManagement;

public class StartsScreenManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Main_Game");
    }
    public void OpenHowToPlay()
    {
        SceneManager.LoadScene("How_To_Play");
    }
}
