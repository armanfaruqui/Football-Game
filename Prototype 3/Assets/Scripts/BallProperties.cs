using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProperties : MonoBehaviour
{
    public bool charge = false;
    public Rigidbody BallRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        BallRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Charge") && BallRigidbody.angularVelocity.magnitude < 6.0f)
        {
            charge = true;
            Debug.Log("Ball entered box");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Charge"))
        {
            charge = false;
            Debug.Log("Ball exited box");
        }
    }
}
