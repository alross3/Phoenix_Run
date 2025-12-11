using UnityEngine;

public class SlowdownPickup : MonoBehaviour
{
    public float slowAmount = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMove player = collision.GetComponent<PlayerMove>();

        if (player != null)
        {
            player.currentSpeed -= slowAmount;

            player.currentSpeed = Mathf.Max(player.currentSpeed, player.minSpeed);

            Destroy(gameObject);
        }
    }
}
