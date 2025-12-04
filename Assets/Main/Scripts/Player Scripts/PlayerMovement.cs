using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float runSpeed = 15;
    public float crouchSpeed = 5;
    public float jumpForce = 5;
    public float doubleJumpForce = 3;
    public float rotationSpeed = 3;
    public float NormalHeight = 1;
    public float crouchHeight = 0.65f;
    public bool canMove = true;
    public Transform cameraTransform;

    private Rigidbody _theRigidBody;
    private Quaternion _targetRotation;
    private float _currentSpeed;

    [SerializeField] private float _groundCheckerOffset = -0.9f;
    [SerializeField] private float _groundCheckerRadius = 0.3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioSource[] SFXSourceList;
    [SerializeField] private AudioClip[] SFXClipList;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isCrouched = false;
    [SerializeField] private bool _isSprinting = false;
    [SerializeField] private bool _canDoubleJump;
    [SerializeField] private bool _canSprint;
    [SerializeField] private bool _canUncrouch = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created.
    void Start()
    {
        _theRigidBody = GetComponent<Rigidbody>(); //Getting Rigidbody from Player Object.
        _targetRotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        _theRigidBody.freezeRotation = true; //This is to stop other game objects from affecting the player's rotation
        _currentSpeed = speed;
    }

    private void Update()
    {
        if (canMove)
        {
            jump();
            sprint();
            crouch();
        }




    }

    // Update is called once per frame.
    void FixedUpdate()
    {
        if (canMove)
        {
            moveAndRotate();
        }



    }

    public void toggleUncroucher(bool toggle)
    {
        _canUncrouch = toggle;
    }

    private void moveAndRotate()
    {
        _isGrounded = Physics.CheckSphere(transform.position + Vector3.up * _groundCheckerOffset, _groundCheckerRadius, groundLayer); //Checking if player is on ground.
        float Horizontal = Input.GetAxis("Horizontal"); //Defining Char X Axis.
        float Vertical = Input.GetAxis("Vertical"); //Defining Char Z Axis.

        bool isWalking = ((Horizontal != 0 || Vertical != 0) && _isGrounded); //Check if player is walking to play walkingSFX

        if (isWalking && !SFXSourceList[0].isPlaying) //if player is walking and the walking audio source is not playing, play it.
        {
            SFXSourceList[0].Play();
        }
        else if (!isWalking && SFXSourceList[0].isPlaying) //if player STOPPED walking and the walking audio source is playing, stop it.
        {
            SFXSourceList[0].Stop();
        }

        // Camera Controls (for Realtive Movement)
        // Taking the Camera Forward and Right
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        //freezing the camera's y axis as we don't want it to be affected for the direction
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        //Realtive Cam Direction
        Vector3 forwardRealtive = Vertical * cameraForward;
        Vector3 rightRealtive = Horizontal * cameraRight;

        Vector3 movementDir = (forwardRealtive + rightRealtive).normalized * _currentSpeed; //assigning movement with camera direction in mind, also using normalized to make movement in corner dierctions the same as normal directions (not faster)

        //Movement
        _theRigidBody.linearVelocity = new Vector3(movementDir.x, _theRigidBody.linearVelocity.y, movementDir.z); // Changing the velocity based on Horizontal and Vertical Movements alongside camera direction.

        //Rotation
        Vector3 lookDirection = cameraForward; //Taking the direction the camera is currently facing

        if (lookDirection != Vector3.zero) //on player movement
        {
            _targetRotation = Quaternion.LookRotation(lookDirection); // makes the target rotation that we want the player to move to
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime); //Using lerp to smooth the player rotation using current rotation, target rotaion and rotation speed.
    }

    private void jump()
    {
        // Allow Player to jump if on ground and jump button pressed.
        if (_isGrounded && !_isCrouched && Input.GetButtonDown("Jump"))
        {
            _theRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _canDoubleJump = true;
            SFXSourceList[1].PlayOneShot(SFXClipList[3]);
        }
        // Allow Player to double jump if NOT on ground and jump button pressed.
        if (!_isGrounded && !_isCrouched && _canDoubleJump && Input.GetButtonDown("Jump"))
        {
            _theRigidBody.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            _canDoubleJump = false;
            SFXSourceList[1].PlayOneShot(SFXClipList[4]);
        }
    }

    private void sprint()
    {
        //Sprint Code
        if (_isGrounded && !_isCrouched && Input.GetKey(KeyCode.LeftShift))
        {
            SFXSourceList[0].clip = SFXClipList[2];
            _currentSpeed = runSpeed;
            _isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SFXSourceList[0].clip = SFXClipList[0];
            _currentSpeed = speed;
            _isSprinting = false;
        }
    }

    private void crouch()
    {
        //Crouch Code
        if (_isGrounded && !_isCrouched && !_isSprinting && Input.GetKeyDown(KeyCode.Tab))
        {
            transform.localScale = new Vector3(1f, crouchHeight, 1f);
            _isCrouched = true;
            _currentSpeed = crouchSpeed;
            SFXSourceList[0].clip = SFXClipList[1];
            SFXSourceList[2].PlayOneShot(SFXClipList[5]);
        }
        else if (_isCrouched && _canUncrouch && Input.GetKeyDown(KeyCode.Tab))
        {
            transform.localScale = new Vector3(1f, NormalHeight, 1f);
            _isCrouched = false;
            _currentSpeed = speed;
            SFXSourceList[0].clip = SFXClipList[0];
            SFXSourceList[2].PlayOneShot(SFXClipList[6]);
        }
    }

    private void OnDrawGizmos() //Gizmo to draw the ground checker sphere.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * _groundCheckerOffset, _groundCheckerRadius);
    }
}
