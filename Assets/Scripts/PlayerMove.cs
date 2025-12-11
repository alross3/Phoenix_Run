using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float rotateSpeed = 90f;
    public float liftStrength = 5f;

    //Speed
    public float baseSpeed = 5f;
    public float minSpeed = 2f;
    public float maxSpeed = 12f;
    public float airResistance = 0.1f;

    public float currentSpeed;

    //Launch
    public bool isLaunched = false;
    public float launchUpForce = 5f;
    public float launchForwardSpeed = 5f;

    private float launchTimer = 0f;
    public float launchDuration = 0.2f; 

    public float stallThreshold = 1f; 
    public float stallDownForce = -5f;    
    //Area Clamp
    public float minY = -5f; 
    public float maxY = 5f;  



    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        //Launch
        if (!isLaunched && Input.GetKeyDown(KeyCode.Space))
        {
            isLaunched = true;
            launchTimer = launchDuration;

            
            rb2d.linearVelocity = new Vector2(launchForwardSpeed, launchUpForce);
            currentSpeed = launchForwardSpeed;
        }

        if (!isLaunched) return;

        
        float input = Input.GetAxisRaw("Vertical");

        float angle = transform.eulerAngles.z;
        if (angle > 180) angle -= 360;

        angle += input * rotateSpeed * Time.deltaTime;
        angle = Mathf.Clamp(angle, -45f, 45f);

        transform.rotation = Quaternion.Euler(0, 0, angle);
        //Clamp
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

    void FixedUpdate()
    {
    if (!isLaunched) return;

    float angle = transform.eulerAngles.z;
    if (angle > 180) angle -= 360;

    currentSpeed -= currentSpeed * airResistance * Time.fixedDeltaTime;
    currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);

    if (currentSpeed < stallThreshold)
    {
    rb2d.AddForce(new Vector2(0f, stallDownForce), ForceMode2D.Force);
    }

    rb2d.linearVelocity = transform.right * currentSpeed;
    }
    public void AddSpeed(float amount)
    {
        currentSpeed += amount;
    }


}
