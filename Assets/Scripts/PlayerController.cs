using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 9f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.8f;
    public float roatationSmoothTime = 0.1f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    [Header("References")]
    public Transform cameraTransform;

    //Private Variables
    CharacterController _controller;
    Vector2 _moveInput;
    bool _isJumping;
    bool _jumpPressed;
    bool _isGrounded;
    float _jumpCooldown = 0f;
    Vector3 _velocity;
    float _rotationVelocity;



    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

        if(cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    //Input System Callbacks
    public void OnMove(InputValue value)
    {
        Debug.Log(value.Get<Vector2>());
        _moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            _jumpPressed = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        HandleGroundCheck();
        HandleMovement();
        HandleJump();
        ApplyGravity();
    }

    void HandleMovement()
    {
        //This is a check
        if (_moveInput.sqrMagnitude < 0.01f) return;

        float speed = moveSpeed;

        //Get Camera Relative Direction
        float targetAngle = Mathf.Atan2(_moveInput.x, _moveInput.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

        //Smooth rotation of player in direction of camera
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _rotationVelocity, roatationSmoothTime);

        transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f,targetAngle, 0f)  * Vector3.forward;//new Vector3(_moveInput.x, 0, _moveInput.y);

        _controller.Move(moveDir.normalized * speed * Time.deltaTime);
    }

    void HandleGroundCheck()
    {
        //Setup a cooldown for the jump
        if (_jumpCooldown > 0f)
        {
            //Don't allow player to jump
            _jumpCooldown -= Time.deltaTime;
            _isGrounded = false;
            return;
        }

        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Reset downward velocity when grounded;
        if(_isGrounded && _velocity.y < 0f)
        {
            _velocity.y = -2;
            _isJumping = false;
        }

    }
    void HandleJump()
    {
        if (_jumpPressed && _isGrounded && !_isJumping)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            _isJumping = true;
            _jumpCooldown = 0.5f;
        }

        _jumpPressed = false;
    }

    void ApplyGravity()
    {
        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(new Vector3(0f,_velocity.y, 0f)  * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck == null) return;
        Gizmos.color = _isGrounded? Color.green:Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
