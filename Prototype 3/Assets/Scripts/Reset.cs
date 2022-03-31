using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public Vector3 BallOrigin = new Vector3(0, 0, 0);
    public Vector3 PlayerOrigin = new Vector3(-8, 1, 0);

    public GameObject Ball;
    public GameObject Player;

    Rigidbody BallRb;


    // Start is called before the first frame update
    void Start()
    {
       BallRb = Ball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Ball.transform.position.y < -2)
        {
            ResetBallAndPlayer();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ResetBallAndPlayer();
        }
    }

    private void ResetBallAndPlayer()
    {
        Ball.transform.position = BallOrigin;
        BallRb.velocity = Vector3.zero;
        BallRb.angularVelocity = Vector3.zero;
    }
}
