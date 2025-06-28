using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent(typeof(InteractionController))]
[RequireComponent(typeof(GroundChecker))]


public class PlayerController : MonoBehaviour
{
    private GroundChecker grChecker;
    private InteractionController intController;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float moveSpeedMult = 1.8f;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float sensitivity = 1.5f;
    [SerializeField] float smooth = 10;
    [SerializeField] new Transform camera;
    private PlayerInput _playerInput;
    private Vector3 movementVector;
    private Vector2 inputVector;
    private Rigidbody rb;
    private float yRotation;
    private float xRotation;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Jump.performed += context => Jump();
        _playerInput.Player.Interaction.performed += context => InteractionActive();
        intController = GetComponent<InteractionController>();
        grChecker = GetComponent<GroundChecker>();
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        yRotation += _playerInput.Player.MouseDelta.ReadValue<Vector2>().x * sensitivity;
        xRotation -= _playerInput.Player.MouseDelta.ReadValue<Vector2>().y * sensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        RotateCharacter();
    }

    private void FixedUpdate()
    {
        ReadMovement();
        movementVector = (inputVector.x * transform.right + inputVector.y * transform.forward).normalized;
        if (_playerInput.Player.Run.IsPressed())
        {
            rb.MovePosition(transform.position + movementVector * moveSpeed * moveSpeedMult * Time.fixedDeltaTime);
        }
        else 
        {
            rb.MovePosition(transform.position + movementVector * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void ReadMovement()
    {
        inputVector = _playerInput.Player.Movement.ReadValue<Vector2>();
    }

    private void RotateCharacter()
    {
        camera.rotation = Quaternion.Lerp(camera.rotation, Quaternion.Euler(xRotation, yRotation, 0), Time.deltaTime * smooth);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, yRotation, 0), Time.deltaTime * smooth);
    }

    private void Jump()
    { 
        if (grChecker.isOnGround())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void InteractionActive()
    {
        intController.InteractActivation();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}
