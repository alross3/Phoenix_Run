using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GroundLoop: MonoBehaviour
{
    public Transform player;         // Your player
    public GameObject groundPrefab;  // Ground prefab
    public float groundY = -3f;      // Y position for ground
    public float extraPieces = 2f;   // Extra pieces to cover camera edges

    private Transform[] pieces;
    private float groundWidth;
    private int totalPieces;

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;

        SpriteRenderer sr = groundPrefab.GetComponent<SpriteRenderer>();
        if (sr != null)
            groundWidth = sr.bounds.size.x;
        else
            groundWidth = 20f;

        float camWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;
        totalPieces = Mathf.CeilToInt(camWidth / groundWidth) + (int)extraPieces;

        pieces = new Transform[totalPieces];

        for (int i = 0; i < totalPieces; i++)
        {
            Vector3 pos = new Vector3(i * groundWidth, groundY, 0);
            GameObject g = Instantiate(groundPrefab, pos, Quaternion.identity);

            if (g.GetComponent<BoxCollider2D>() == null)
                g.AddComponent<BoxCollider2D>();

            if (g.GetComponent<Rigidbody2D>() == null)
            {
                Rigidbody2D rb = g.AddComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Static;
            }

            if (g.GetComponent<GroundCollision>() == null)
                g.AddComponent<GroundCollision>();

            pieces[i] = g.transform;
        }
    }

    void Update()
    {
        Transform rightmost = pieces[0];

        foreach (Transform t in pieces)
        {
            if (t.position.x > rightmost.position.x)
                rightmost = t;
        }

        float leftEdge = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;

        foreach (Transform t in pieces)
        {
            if (t.position.x + groundWidth < leftEdge)
            {
                t.position = new Vector3(rightmost.position.x + groundWidth, groundY, 0);
                rightmost = t; 
            }
        }
    }
}


public class GroundCollision : MonoBehaviour
{
    public float delayBeforeEnd = .5f;
    private bool triggered = false;   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (triggered) return;
        if (collision.collider.CompareTag("Player"))
        {
            triggered = true;

            UIManager uiManager = Object.FindFirstObjectByType<UIManager>();
            if (uiManager != null)
            {
                uiManager.SaveLastRun();
            }

            StartCoroutine(LoadEndGameScene());
        }
    }

    private IEnumerator LoadEndGameScene()
    {
        yield return new WaitForSeconds(delayBeforeEnd);
        SceneManager.LoadScene("End_Game");
    }
}
