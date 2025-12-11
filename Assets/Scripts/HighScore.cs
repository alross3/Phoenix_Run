using UnityEngine;
using TMPro;

public class HighScore: MonoBehaviour
{
    public TMP_Text highScoreText;
    public TMP_Text lastRunText;

    void Start()
    {
        float highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        float lastRun = PlayerPrefs.GetFloat("LastRunDistance", 0f);

        highScoreText.text = "High Score: " + highScore.ToString("F1");
        lastRunText.text = "Last Run: " + lastRun.ToString("F1");
    }
}
