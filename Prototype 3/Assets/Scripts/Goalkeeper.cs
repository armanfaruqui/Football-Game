using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper : MonoBehaviour
{
    public GameObject Ball;
    public GameObject BallContainer;
    public GameObject player;
    public BallProperties ballPropScript;
    Rigidbody GKRigidbody;
    public Animator gkAnim;
    public GameObject startingPos;

    public KickBall ballScript;

    public float speedModifierSlow = 3.0f;
    public float speedModifierFast = 5.0f;

    private bool lookAt = true;

    Collider ballCollider;

    Vector3 DiveLeft;
    Vector3 DiveRight;

    Vector3 clearVector;
    Vector3 clearHeight = new Vector3(0, 500, 0);

    bool kickable = true;
    bool reset = false;


    // Start is called before the first frame update
    void Start()
    {
        GKRigidbody = GetComponent<Rigidbody>();
        gkAnim = GetComponent<Animator>();
        ballCollider = Ball.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {  
        Dive();
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(Ball.transform.position.x);
        }
    }

    private void FixedUpdate()
    {
        AdjustPosition();
        if (lookAt == true)
        {
            transform.LookAt(Ball.transform.position);
            
        }
        charge();

        resetPosition();
        Debug.Log(ballScript.BallRigidbody.angularVelocity.magnitude);
    }


    private void AdjustPosition()
    {
        if (reset == false)
        {
            if (Ball.transform.position.z < -0.05)
            {
                if (transform.position.z > -0.3)
                {
                    transform.position -= transform.right * speedModifierSlow * Time.deltaTime;
                    gkAnim.SetBool("MoveLeft", true);
                }
                else
                {
                    gkAnim.SetBool("MoveLeft", false);
                }
            }
            else if (Ball.transform.position.z > -0.05)
            {
                if (transform.position.z < 0.3)
                {
                    transform.position += transform.right * speedModifierSlow * Time.deltaTime;
                    gkAnim.SetBool("MoveRight", true);
                }
                else
                {
                    gkAnim.SetBool("MoveRight", false);
                }
            }

            if (ballPropScript.charge == true)
            {
                lookAt = false;
            }
            else
            {
                lookAt = true;
            }
        }
        
    }

    private void Dive()
    {
        if (Ball.transform.position.x > 40.0f && ballScript.shouldDiveRight == true)
        {
            lookAt = false;
            gkAnim.SetTrigger("DiveRight");
            Invoke("resetHeight", 2.2f);
            ballScript.shouldDiveRight = false;
            ballScript.castRay = false;
        }

        if (ballScript.shouldDiveLeft == true)
        {
            lookAt = false;
            gkAnim.SetTrigger("DiveLeft");
            Invoke("resetHeight", 2.2f);
            ballScript.shouldDiveLeft = false;
            ballScript.castRay = false;
        }
    }

   void resetHeight()
    {
        transform.position = new Vector3(transform.position.x, -0.01f, transform.position.z);
    }

    void resetPosition()
    {
        if (reset == true)
        {
            gkAnim.SetBool("reset", true);
            transform.position = Vector3.MoveTowards(transform.position, startingPos.transform.position, 6.0f * Time.deltaTime);
            if (transform.position == startingPos.transform.position)
            {
                gkAnim.SetBool("reset", false);
                resetHeight();
                reset = false;
                lookAt = true;
            }
        }
    }

    void charge()
    {
        if (ballPropScript.charge == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Ball.transform.position, 6.0f * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, -0.01f, transform.position.z);
            gkAnim.SetBool("Charge", true);
        }
        else
        {
            gkAnim.SetBool("Charge", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Ball")
        {
            gkAnim.SetTrigger("Kick");
            Invoke("KickBall", 0.2f);
        }
    }

    void KickBall()
    {
        if (kickable == true)
        {
            clearVector = new Vector3(-20, 2, -3);
            Debug.Log(clearVector);
            ballScript.BallRigidbody.AddForce(clearVector * 2, ForceMode.Impulse);
            Invoke("resetHeight", 1.0f);
            kickable = false;
            Invoke("resetTrue", 1.0f);
        }
    }

    void resetTrue()
    {
        reset = true;
    }
}
