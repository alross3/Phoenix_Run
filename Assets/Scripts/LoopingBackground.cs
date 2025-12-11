using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public Transform player;          // The bird/player
    public float scrollSpeed = 1f;    // Background moves relative to player
    private float spriteWidth;        // Width of first child sprite

    private Vector3 startPosition;    
    private float lastPlayerX;        // Track last frame's player X

    void Start()
    {
        startPosition = transform.position;

        if (transform.childCount > 0)
        {
            SpriteRenderer sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
            if (sr != null)
                spriteWidth = sr.bounds.size.x;
            else
                Debug.LogError("First child must have a SpriteRenderer.");
        }
        else
        {
            Debug.LogError("Background parent has no children.");
        }

        if (player != null)
            lastPlayerX = player.position.x;
    }

    void Update()
    {
        if (player == null) return;

        // Calculate how much the player moved since last frame
        float deltaX = player.position.x - lastPlayerX;
        lastPlayerX = player.position.x;

        // Move background left based on delta
        transform.position += Vector3.left * deltaX * scrollSpeed;

        // Loop background horizontally
        float xOffset = (transform.position.x - startPosition.x) % spriteWidth;
        transform.position = new Vector3(startPosition.x + xOffset, startPosition.y, startPosition.z);
    }
}
