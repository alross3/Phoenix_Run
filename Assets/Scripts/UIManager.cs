using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public PlayerMove player;           // Reference to your player
    public TMP_Text speedText;          // Assign SpeedText
    public TMP_Text distanceText;       // Assign DistanceText

    private float startX;
    private float distanceTraveled;
    private float highScore;

    private void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();

        startX = player.transform.position.x;

        // Load high score from PlayerPrefs
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
    }

    private void Update()
    {
        UpdateSpeed();
        UpdateDistance();
    }

    private void UpdateSpeed()
    {
        speedText.text = "Speed: " + player.currentSpeed.ToString("F1");
    }

    private void UpdateDistance()
    {
        distanceTraveled = player.transform.position.x - startX;
        distanceText.text = "Distance: " + distanceTraveled.ToString("F1");

        if (distanceTraveled > highScore)
        {
            highScore = distanceTraveled;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
    }

    public void SaveLastRun()
    {
        PlayerPrefs.SetFloat("LastRunDistance", distanceTraveled);
        PlayerPrefs.Save();
    }

    public float GetHighScore()
    {
        return highScore;
    }
}
