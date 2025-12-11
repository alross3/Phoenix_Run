using UnityEngine;

public class PowerUpSpawner2D : MonoBehaviour
{
    public Transform player;             // your bird
    public GameObject powerUpPrefab;

    public float spawnInterval = 2f;     // seconds between spawns
    public float forwardDistance = 10f;  // in front of the player
    public float verticalRange = 5f;     // up/down randomness

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnPowerUp();
            timer = 0f;
        }
    }

    void SpawnPowerUp()
    {
        // Spawn in front of the player based on its facing direction
        Vector2 spawnPos = (Vector2)player.position + (Vector2)player.right * forwardDistance;

        // Random vertical offset
        spawnPos += new Vector2(0f, Random.Range(-verticalRange, verticalRange));

        // Instantiate the powerup
        Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);
    }
}
