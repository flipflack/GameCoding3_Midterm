using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    bool isGrounded;

    public Transform orientation;

    //Floats for horizontal & vertical keyboard input
    float horizontalInput;
    float verticalInput;

    //Vector3 for movement direction
    Vector3 moveDirection;

    //Player rigidbody
    Rigidbody rb;
    public Transform playerCamera;

    private void Start()
    {   
        //Obtain player rigidbody component & freeze rotation
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        //Check if player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, ground);

        MyInput();
        SpeedControl();

        //Handle drag
        if (isGrounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //Calculate movement direction
        moveDirection = playerCamera.forward * verticalInput + playerCamera.right * horizontalInput;
        moveDirection.y = 0f;

        //Add force to player to actually move player in calculated direction
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //Limit velcity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
    public void setPlayerMovement(bool b)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement pm = player.GetComponent<PlayerMovement>();

        if (b)
        {
            pm.enabled = false;
        }
        else
        {
            pm.enabled = true;
        }
    }

    public void moveForDialog(Transform movePos)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = movePos.position;
    }
}
