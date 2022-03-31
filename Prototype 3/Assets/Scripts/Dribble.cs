using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dribble : MonoBehaviour
{
    private GameObject Front_Foot;
    Rigidbody BallRigidbody;
    public float dribbleMultiplier = 2;

    // Start is called before the first frame update
    void Start()
    {
        BallRigidbody = GetComponent<Rigidbody>();
        Front_Foot = GameObject.FindWithTag("Dribble");

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void FixedUpdate()
    {
        dribbleMultiplier = Random.Range(2, 5);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name == "Front_Foot")
        {
            BallRigidbody.AddForce(Front_Foot.transform.forward * dribbleMultiplier, ForceMode.Impulse);
            Debug.Log("Dribbled");
        }
    }
}
