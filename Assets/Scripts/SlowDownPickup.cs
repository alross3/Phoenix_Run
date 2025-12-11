using UnityEngine;

public class SlowdownPickup : MonoBehaviour
{
    public float slowAmount = 3f; // how much to slow the player

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMove player = collision.GetComponent<PlayerMove>();

        if (player != null)
        {
            // Reduce speed directly
            player.currentSpeed -= slowAmount;

            // Clamp so the player doesn't go below min speed
            player.currentSpeed = Mathf.Max(player.currentSpeed, player.minSpeed);

            Destroy(gameObject);
        }
    }
}
