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

    private float rotationX = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(!PauseMenu.instance.GameIsPaused)
        {

        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        // Player movement
        float moveX = Input.GetAxis("Horizontal") * speed;
        float moveZ = Input.GetAxis("Vertical") * speed;
        Vector3 move = transform.TransformDirection(new Vector3(moveX, 0, moveZ));

         if (characterController.isGrounded)
        {
            moveDirection.y = -0.1f; 

            if (Input.GetButton("Jump") && Time.time > lastJumpTime + jumpCD)
            {
                moveDirection.y = jumpForce;
                lastJumpTime = Time.time;
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
    }
   
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Zombie"))
        {
            gameOverHandler.gg(gameOver.GameOverType.ZombieTouch);
        }  
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
