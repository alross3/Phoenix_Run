using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float forwardForce = 5f;
    public float rotateSpeed = 90f;
    public float liftStrength = 5f;
    public float baseSpeed = 5f;
    public float climbPenalty = 2f;
    public float diveBoost = 3f;
    public float minSpeed = 2f;
    public float maxSpeed = 12f;

    public float currentSpeed;

    public float airResistance = 0.1f;

    //Stall Variables
    public float stallSpeed = 1.0f;
    public float stallAngle = 30f;
    private bool isStalled = false;
    public float stallFallSpeed = -5f;

    //Launch Variables
    public bool isLaunched = false;
    public float launchUpForce = 5f;
    public float launchForwardSpeed = 5f;

    private Rigidbody2D rb2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentSpeed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLaunched && Input.GetKeyDown(KeyCode.Space))
        {
            isLaunched = true;
            rb2d.linearVelocity = new Vector2(launchForwardSpeed, launchUpForce);
            currentSpeed = launchForwardSpeed;
        }
        float input = Input.GetAxisRaw("Vertical");

        float angle = transform.eulerAngles.z;
        if (angle > 180) angle -= 360;

        angle += input * rotateSpeed * Time.deltaTime;
        angle = Mathf.Clamp(angle, -45f, 45f);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void FixedUpdate()
    {
        if (!isLaunched) return;
        float angle = transform.eulerAngles.z;
        if (angle > 180) angle -= 360;

        float normalized = angle / 45f;
        
        if (normalized < 0)
        {
            currentSpeed -= normalized * climbPenalty * Time.fixedDeltaTime;
        }
        else if (normalized > 0)
        {
            currentSpeed -= normalized * diveBoost * Time.fixedDeltaTime;
        }
        
        currentSpeed -= currentSpeed * airResistance * Time.fixedDeltaTime;

        if(!isStalled)
        {
            if (angle > stallAngle && currentSpeed <= stallSpeed)
            {
                isStalled = true;
            }
        }
        else
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, stallFallSpeed);

            if (currentSpeed > stallSpeed + 0.5f && angle < stallAngle )
            {
                isStalled = false;

                return;
            }
        }
        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);

        rb2d.linearVelocity = new Vector2 (currentSpeed, rb2d.linearVelocity.y);

        float angleRad = angle * Mathf.Deg2Rad;
        float lift = Mathf.Sin(angleRad) * liftStrength;
        rb2d.AddForce(Vector2.up * lift);
    }
}
