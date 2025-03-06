using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimplePlayerMovement : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float sprintSpeed = 12f;
    public float lookSpeed = 2f;
    public float jumpForce = 8f;
    private float jumpCD = 0.2f;
    private float lastJumpTime;
    public float gravity = 10f;
    public Camera playerCamera;
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    public gameOver gameOverHandler;

    // Audio
    public AudioSource walkAudioSource;
    public AudioSource jumpAudioSource;
    public AudioClip walkSound;
    public AudioClip jumpSound;

    private float rotationX = 0;
    private bool isMoving = false; // Track if the player is moving
    private bool isSprinting = false; // Track if the player is sprinting

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();

        // Initialize audio sources
        if (walkAudioSource == null)
            walkAudioSource = gameObject.AddComponent<AudioSource>();
        if (jumpAudioSource == null)
            jumpAudioSource = gameObject.AddComponent<AudioSource>();

        walkAudioSource.clip = walkSound;
        walkAudioSource.loop = true; // Walking sound should loop
        jumpAudioSource.clip = jumpSound;
        jumpAudioSource.loop = false; // Jumping sound should not loop
    }

    void Update()
    {
        if (!PauseMenu.instance.GameIsPaused)
        {
            float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
            isSprinting = Input.GetKey(KeyCode.LeftShift); // Check if the player is sprinting

            // Player movement
            float moveX = Input.GetAxis("Horizontal") * speed;
            float moveZ = Input.GetAxis("Vertical") * speed;
            Vector3 move = transform.TransformDirection(new Vector3(moveX, 0, moveZ));

            // Check if the player is moving
            isMoving = moveX != 0 || moveZ != 0;

            // Handle walking sound
            if (isMoving && characterController.isGrounded)
            {
                if (!walkAudioSource.isPlaying)
                {
                    walkAudioSource.Play(); // Play walking sound if not already playing
                }

                // Adjust pitch based on sprinting
                if (isSprinting)
                {
                    walkAudioSource.pitch = 1.5f; // Increase pitch when sprinting
                }
                else
                {
                    walkAudioSource.pitch = 1.0f; // Normal pitch when walking
                }
            }
            else
            {
                if (walkAudioSource.isPlaying)
                {
                    walkAudioSource.Stop(); // Stop walking sound if player stops moving or is in the air
                }
            }

            if (characterController.isGrounded)
            {
                moveDirection.y = -0.1f;

                // Handle jumping
                if (Input.GetButton("Jump") && Time.time > lastJumpTime + jumpCD)
                {
                    moveDirection.y = jumpForce;
                    lastJumpTime = Time.time;

                    // Play jumping sound
                    if (jumpAudioSource != null && jumpSound != null)
                    {
                        jumpAudioSource.PlayOneShot(jumpSound); // Play jump sound once
                    }
                }
            }

            moveDirection.y -= gravity * Time.deltaTime;
            characterController.Move((move + moveDirection) * Time.deltaTime);

            // Camera rotation
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -45f, 45f);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.Rotate(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        else
        {
            // Stop the walking sound when the game is paused
            if (walkAudioSource.isPlaying)
            {
                walkAudioSource.Stop();
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("trap"))
        {
            gameOverHandler.gg(gameOver.GameOverType.TrapFall);
        }
        if (hit.gameObject.CompareTag("uWon"))
        {
            gameOverHandler.gg(gameOver.GameOverType.WinGame);
        }
    }
}