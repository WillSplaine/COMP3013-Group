using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float origMoveSpeed;
    public float currentSpeed;
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Player Stair Climb")]

    [SerializeField] GameObject RayCastKnee;

    [SerializeField] GameObject RayCastToes;

    //[SerializeField] float stairHeight = 0.4f; //Value is assigned but never used
    //[SerializeField] float smoothMotion = 0.02f; //Value is assigned but never used

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Movement")]
    public float maxSloapAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

   

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
       // RayCastKnee.transform.position = new Vector3(RayCastKnee.transform.position.x, stairHeight, RayCastKnee.transform.position.z);

        origMoveSpeed = moveSpeed;
        currentSpeed = origMoveSpeed;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        //stairClimb();
        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;


        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDir() * moveSpeed * 20f, ForceMode.Force);
            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);

        }
        else if (grounded)
        {
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            rb.AddForce(moveDirection.normalized * moveSpeed * 4f, ForceMode.Force);

        }
        rb.useGravity = !OnSlope();
    }

    public void SpeedControl()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = origMoveSpeed * 1.7f; 
        }
        else
        {
            currentSpeed = origMoveSpeed;
        }

        moveSpeed = currentSpeed;

        if (OnSlope())
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedv = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedv.x, rb.velocity.y, limitedv.z);
            }
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

   // void stairClimb()
   // {
    //    RaycastHit hitLow;
    //    if (Physics.Raycast(RayCastToes.transform.position, transform.TransformDirection(Vector3.forward), out hitLow, 0.1f))
    //    {
     //       RaycastHit hitHigh;
     //       if (!Physics.Raycast(RayCastKnee.transform.position, transform.TransformDirection(Vector3.forward), out hitHigh, 0.2f)) 
     //       {
     //           rb.position -= new Vector3(0f, -smoothMotion * Time.deltaTime, 0f);
     //       }
     //   }
   // }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSloapAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDir()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}


        

               
                
        
    
