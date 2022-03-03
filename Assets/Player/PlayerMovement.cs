using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float gravity = 1.0f;
    [SerializeField]
    private float jumpHeight = 15.0f;
    [SerializeField]
    private int consecutiveJumps = 2;

    private float yVelocity = 0.0f;
    private int jumpsRemaining;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        ResetJumps();
    }

    private void Update()
    {
        var xVelocity = DetermineHorizontalVelocity();
        var yVelocity = DetermineVerticalVelocity();
        var velocity = new Vector3(xVelocity, yVelocity, 0);
        characterController.Move(Time.deltaTime * velocity);
    }

    private float DetermineHorizontalVelocity()
    {
        return speed * Input.GetAxis("Horizontal");
    }

    private float DetermineVerticalVelocity()
    {
        if (characterController.isGrounded)
        {
            ResetJumps();
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            jumpsRemaining--;
            yVelocity = jumpHeight;
        }
        else
        {
            yVelocity -= gravity;
        }

        return yVelocity;
    }

    private void ResetJumps()
    {
        jumpsRemaining = consecutiveJumps;
    }
}
