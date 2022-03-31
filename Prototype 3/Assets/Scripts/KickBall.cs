using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickBall : MonoBehaviour
{
    public Rigidbody BallRigidbody;
    private GameObject Feet;
    public bool Kickable = false;
    public Camera cam;
    public Animator playerAnim;

    public float shootHeight = 400;
    Vector3 addHeight;
    public float shootMultiplier = 640;
    public float passMultiplier = 400;

    Vector3 shootVector;

    public bool castRay = false;

    public bool shouldDiveLeft = false;
    public bool shouldDiveRight = false;


    // Start is called before the first frame update
    void Start()
    {
        BallRigidbody = GetComponent<Rigidbody>();
        Feet = GameObject.FindWithTag("Feet");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Kickable == true)
            {
                Shoot();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Kickable == true)
            {
                Pass();
            }
        }

        shotRayCast();
    }

    private void FixedUpdate()
    {
        // Debug.Log(cam.transform.rotation.x);
   
    }

    void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.CompareTag("Feet"))
        {
            Kickable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Feet"))
        {
            Kickable = false;
        }
    }


    private void Shoot()
    {
        if (cam.transform.rotation.x > 0.07)
        {
            shootHeight = 400;
            Debug.Log("WEEE");
        }
        else
        {
            shootHeight = 400;
        }
        playerAnim.SetTrigger("shoot");
        addHeight = new Vector3(0, shootHeight, 0);
        shootVector = (Feet.transform.forward * shootMultiplier) + addHeight;
        castRay = true;
        BallRigidbody.AddForce(shootVector);     
    }

    private void Pass()
    {
        BallRigidbody.AddForce(Feet.transform.forward * passMultiplier);
        playerAnim.SetTrigger("pass");
    }

    private void shotRayCast()
    {
        if (castRay == true)
        {
            Ray shotVar = new Ray(transform.position, shootVector - addHeight); // Creates the ray
            RaycastHit shotInfo; //Information from raycast

            if (Physics.Raycast(shotVar, out shotInfo, 1000)) // If ray is hitting an object:
            {
                Debug.DrawLine(shotVar.origin, shotInfo.point, Color.red);
                if (shotInfo.collider.tag == "DiveLeft")
                {
                    shouldDiveLeft = true;
                    Debug.Log("OKAY");
                }     
                else if (shotInfo.collider.tag == "DiveRight")
                {
                    shouldDiveRight = true;
                }
            }
        }
    }

}
