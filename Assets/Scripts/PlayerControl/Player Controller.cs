using UnityEngine;

namespace PlayerControl
{
    [RequireComponent(typeof(Rigidbody))]

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GroundChecker grChecker;
        [SerializeField] private InteractionController intController;
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private float moveSpeedMult = 1.8f;
        [SerializeField] private float jumpForce = 5;
        [SerializeField] private new Transform camera;
        [SerializeField] private float sensitivity = 1.5f;
        [SerializeField] private float smooth = 10;
        private PlayerInput playerInput;
        private Vector3 movementVector;
        private Vector2 inputVector;
        private Rigidbody rb;
        private float yRotation;
        private float xRotation;

        private void Awake()
        {
            playerInput = new PlayerInput();
            playerInput.Player.Jump.performed += _ => Jump();
            playerInput.Player.Interaction.performed += _ => InteractionActive();
            playerInput.Player.DropHandInteraction.performed += _ => Drop();
            playerInput.Player.InHandInteraction.performed += _ => InHandInteractionActive();
            playerInput.Player.Replace.performed += _ => Replace();
            playerInput.Player.ReplaceRotate.performed += _ => ReplaceRotate();
            rb = GetComponent<Rigidbody>();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            yRotation += playerInput.Player.MouseDelta.ReadValue<Vector2>().x * sensitivity;
            xRotation -= playerInput.Player.MouseDelta.ReadValue<Vector2>().y * sensitivity;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            RotateCharacter();
        }

        private void FixedUpdate()
        {
            ReadMovement();
            movementVector = (inputVector.x * transform.right + inputVector.y * transform.forward).normalized;
            if (playerInput.Player.Run.IsPressed())
            {
                rb.MovePosition(transform.position + movementVector * (moveSpeed * moveSpeedMult * Time.fixedDeltaTime));
            }
            else
            {
                rb.MovePosition(transform.position + movementVector * (moveSpeed * Time.fixedDeltaTime));
            }
        }

        private void ReadMovement()
        {
            inputVector = playerInput.Player.Movement.ReadValue<Vector2>();
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

        private void Drop()
        {
            intController.Drop();
        }

        private void InHandInteractionActive()
        {
            intController.InHandInteractActivation();
        }

        private void Replace()
        {
            intController.ReplaceActivation();
        }

        private void ReplaceRotate()
        {
            intController.ReplaceRotateActivation();
        }

        public void OnEnable()
        {
            playerInput.Enable();
        }

        public void OnDisable()
        {
            playerInput.Disable();
        }
    }
}
