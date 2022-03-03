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

    // It appears as though CharacterController.isGrounded can be a bit unreliable when using a
    // y-axis velocity of zero. This constant is being used in place of zero to move the player down
    // slightly to re-collide with a platform after every observed grounding. In addition to this
    // value, the CharacterController on the player has had its "Min Move Distance" set to zero.
    private const float ZERO_VELOCITY = -0.001f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterController.minMoveDistance = 0.0f;
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
            yVelocity = ZERO_VELOCITY;
        }
        else
        {
            yVelocity -= gravity;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            jumpsRemaining--;
            yVelocity = jumpHeight;
        }

        return yVelocity;
    }

    private void ResetJumps()
    {
        jumpsRemaining = consecutiveJumps;
    }
}
