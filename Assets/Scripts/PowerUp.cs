using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speedBoost = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMove player = other.GetComponent<PlayerMove>();

        if (player != null)
        {
            player.AddSpeed(speedBoost);
            Destroy(gameObject);
        }
    }
}
