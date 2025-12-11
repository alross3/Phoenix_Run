using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameButtons: MonoBehaviour
{
    public void PlayAgain()
    {
        string lastScene = PlayerPrefs.GetString("LastSceneName", "Main_Game"); 
        SceneManager.LoadScene(lastScene);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
