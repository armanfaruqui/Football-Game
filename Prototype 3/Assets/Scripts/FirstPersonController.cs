using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float mouseSensitivityX = 250f; // Horizontal sens
    public float mouseSensitivityY = 250f; // Vertical sens

    public float speed; 
    public float jumpForce = 220;

    public bool isJogging = false;
    public float joggingSpeed = 4.0f;

    public bool isSprinting = false;
    public float sprintingSpeed = 9.0f;

    public LayerMask groundedMask;
    Transform cameraT;

    public Animator anim;

    float verticalLookRotation; // Stores the amount the player is rotated horizontally

    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    bool grounded; // True is player is grounded

    public float smoothAmount = 0.15f;
    public float verticalConstraint = 60f; // Stores the degrees at which the player is able to look up or down

    // Start is called before the first frame update
    void Start()
    {
        cameraT = Camera.main.transform; // Finds the main camera
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Jump();

        SpeedAdjuster();

    }

    void FixedUpdate(){
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime); // Updates the player's position
    }

    private void Move()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivityX); // Takes vertical mouse movement. Rotates the camera
        verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivityY; // Takes horizontal mouse movement
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -verticalConstraint, verticalConstraint); // Prevents the camera from rotating higher or lower than the contrains
        cameraT.localEulerAngles = Vector3.left * verticalLookRotation; //  Rotates the player capsule

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized; // Stores vector for direction the player should be moving in
        Vector3 targetMoveAmount = moveDir * speed; // Controlls the speed in that direction
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, smoothAmount); // Smoothes the movement between axises

        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        this.anim.SetFloat("vertical", verticalAxis);
        this.anim.SetFloat("horizontal", horizontalAxis);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        { // Checks if user presses space
            if (grounded)
            {
                GetComponent<Rigidbody>().AddForce(transform.up * jumpForce); // Adds force for jump
            }
        }
        grounded = false; // Grounded is set to false by default
        Ray ray = new Ray(transform.position, -transform.up); // Creates ray from player to ground
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1 + 0.1f, groundedMask))
        { // If ray hits surface with 'ground' layer
            grounded = true; // Grounded set to true
        }
    }

    private void SpeedAdjuster()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        if (isSprinting == true)
        {
            speed = sprintingSpeed;
        }
        else
        {
            speed = joggingSpeed;
        }
    }

}
