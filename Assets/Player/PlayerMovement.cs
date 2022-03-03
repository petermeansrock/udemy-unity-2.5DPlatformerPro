using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float gravity = 1.0f;
    [SerializeField]
    private float jumpHeight = 15.0f;

    private float yVelocity = 0.0f;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var xVelocity = speed * horizontalInput;
        if (characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpHeight;
            }
        }
        else
        {
            yVelocity -= gravity;
        }
        var velocity = new Vector3(xVelocity, yVelocity, 0);
        characterController.Move(Time.deltaTime * velocity);
    }
}
