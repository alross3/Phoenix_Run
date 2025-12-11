using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject speedPrefab;
    public GameObject slowPrefab;

    public float spawnInterval = 2f;
    public float forwardDistance = 10f;
    public float verticalRange = 5f;
    public float minY = -4f;
    public float maxY = 4f;

    private float timer = 0f;

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPowerUp();
            timer = 0f;
        }
    }

    void SpawnPowerUp()
    {
        GameObject prefabToSpawn = (Random.value < 0.7f) ? speedPrefab : slowPrefab;

        Vector2 spawnPos = (Vector2)player.position + Vector2.right * forwardDistance;

        float randomY = Random.Range(-verticalRange, verticalRange);
        spawnPos.y += randomY;

        spawnPos.y = Mathf.Clamp(spawnPos.y, minY, maxY);

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}
