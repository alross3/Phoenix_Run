using UnityEngine;

namespace WinterBackgroundsPixelArt2
{
    public class ParallaxEffect : MonoBehaviour
    {
        private Transform mainCamera;
        private Transform player;

        [Header("Parallax Settings")]
        public float parallaxIntensityX = 1f;  // Horizontal parallax factor
        public float parallaxIntensityY = 0f;  // Vertical parallax factor
        public float independentSpeed = 0f;    // Optional constant horizontal movement

        private float cameraSize;
        private float spriteWidth;
        private Vector2 initialPos;
        private float translationOffset = 0f;

        private void Start()
        {
            mainCamera = Camera.main.transform;
            cameraSize = Camera.main.orthographicSize;

            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
                spriteWidth = sr.bounds.size.x / 3f;  // Keep horizontal tiling
            else
                Debug.LogError("ParallaxEffect requires a SpriteRenderer on the same object.");

            // Keep prefab's original position — no jump
            initialPos = transform.position;
        }

        private void LateUpdate()
        {
            if (player == null || mainCamera == null) return;

            // Optional independent movement
            translationOffset += independentSpeed * Time.deltaTime * parallaxIntensityX;

            // Horizontal parallax based on camera position
            float parallaxOffsetX = (mainCamera.position.x * (1 - (parallaxIntensityX / 2f))) + translationOffset;

            // Vertical parallax based on camera and player
            float parallaxOffsetY = 0f;
            if (parallaxIntensityY > 0f)
            {
                parallaxOffsetY = ((player.position.y - initialPos.y) / cameraSize) * (1 - parallaxIntensityY);
            }

            transform.position = new Vector2(initialPos.x + parallaxOffsetX, initialPos.y + parallaxOffsetY);

            // Handle horizontal tiling
            float cameraOffsetX = mainCamera.position.x - transform.position.x;
            if (cameraOffsetX > spriteWidth / 2f)
                initialPos.x += spriteWidth;
            else if (cameraOffsetX < -spriteWidth / 2f)
                initialPos.x -= spriteWidth;
        }
    }
}
