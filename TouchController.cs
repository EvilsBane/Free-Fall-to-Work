using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    #region Objects, Variables
    private Touch touch;
    private Rigidbody rb;
    private Vector3 touchPos, moveVector;
    private Vector3 rVelocity = Vector3.one;

    private float horizontalMove = 0f;
    private float deltaX = 0f;
    private float movementSmooth = 0.15f;
    private bool isStop;

    public float moveSpeed = 40f;
    public float fallSpeed = 107f;
    public float slowFall = 64f;
    public float slowTime = 1.5f;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameManager.gameIsPaused == false)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                touchPos = GetWorldPositionOnPlane(touch.position, transform.position.y);
                print("touch: " + touchPos);

                if (touch.phase == TouchPhase.Began)
                {
                    print("Start");
                    deltaX = (touchPos.x - transform.position.x);
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    print("Moving");
                    isStop = false;
                    horizontalMove = (touchPos.x - deltaX - transform.position.x) * moveSpeed * Time.deltaTime;
                    print("move: " + horizontalMove);
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    print("End");
                    isStop = true;
                    horizontalMove = 0f;
                }
                else if (touch.phase == TouchPhase.Stationary)
                {
                    print("Hold");
                    isStop = true;
                    horizontalMove = 0f;
                }
            }
        }
        print("h move " + horizontalMove);
        
    }

    private void FixedUpdate()
    {
        if (GameManager.gameIsPaused == false)
        {
            if (horizontalMove != 0)
            {
                moveVector = new Vector3(horizontalMove * 100f, rb.velocity.y, rb.velocity.z);
            }
            if (isStop == true)
            {
                moveVector = Vector3.SmoothDamp(moveVector, new Vector3(0f, rb.velocity.y, rb.velocity.z), ref rVelocity, movementSmooth);
            }
            rb.velocity = new Vector3(moveVector.x, rb.velocity.y + Vector3.down.y * fallSpeed, rb.velocity.z) * Time.fixedDeltaTime * 10f;
        }
        print("x velocity" + rb.velocity.x);
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.up, new Vector3(0, z, 0));
        float distance;
        xy.Raycast(ray, out distance);
        //print("distance: " + distance);
        return ray.GetPoint(distance);
    }

    IEnumerator Slowdown()
    {
        float tempSpeed = fallSpeed;
        fallSpeed = slowFall;
        yield return new WaitForSeconds(slowTime);
        fallSpeed = tempSpeed;
    }

    #region Player Collisions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            GameManager.endLevel = true;
            GameManager.finishOnTime = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Net"))
        {
            StartCoroutine(Slowdown());
        }
    }
    #endregion

    #region Old Code
    /*
    public Joystick joystick;
    private float moveSpeed = 36f;
    private Vector3 rVelocity = Vector3.zero;
    private float movementSmooth = 0f;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = joystick.Horizontal * moveSpeed;
        //rb.MovePosition(new Vector3(touchPos.x - deltaX, rb.position.y, rb.position.z));
        //rb.AddForce(Vector3.right * (touchPos.x - deltaX), ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        // Calculating the desired velocity
        Vector3 targetVelocity = new Vector3(horizontalMove, rb.velocity.y, rb.velocity.z) * Time.fixedDeltaTime;
        // And then smoothing it out and applying it to the character
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref rVelocity, movementSmooth);
    } 
    */
    #endregion
}
