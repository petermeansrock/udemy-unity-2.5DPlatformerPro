using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float gravity = 40.0f;
    [SerializeField]
    private float jumpHeight = 15.0f;
    [SerializeField]
    private int consecutiveJumps = 2;

    private float yVelocity = 0.0f;
    private int jumpsRemaining;
    private bool isJumpButtonPressed = false;
    private Vector3 startingPosition;

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
        ResetRemainingJumps();
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpButtonPressed = true;
        }
    }

    private void FixedUpdate()
    {
        var xVelocity = DetermineHorizontalVelocity();
        var yVelocity = DetermineVerticalVelocity();
        var velocity = new Vector3(xVelocity, yVelocity, 0);
        characterController.Move(Time.deltaTime * velocity);

        // Reset state of jump button
        isJumpButtonPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Snap the player from moving platforms
        if (other.CompareTag(Tag.MovingPlatform))
        {
            transform.parent = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Unsnap the player from moving platforms
        if (other.CompareTag(Tag.MovingPlatform))
        {
            transform.parent = null;
        }
    }

    private float DetermineHorizontalVelocity()
    {
        return speed * Input.GetAxis("Horizontal");
    }

    private float DetermineVerticalVelocity()
    {
        if (characterController.isGrounded)
        {
            ResetRemainingJumps();
            yVelocity = ZERO_VELOCITY;
        }
        else
        {
            yVelocity -= Time.deltaTime * gravity;
        }

        if (isJumpButtonPressed && jumpsRemaining > 0)
        {
            jumpsRemaining--;
            yVelocity = jumpHeight;
        }

        return yVelocity;
    }

    private void ResetRemainingJumps()
    {
        jumpsRemaining = consecutiveJumps;
    }

    public void TeleportToStartingPosition()
    {
        // Disable then re-enable the character controller to avoid consistency issues between the
        // transform and the character controller
        characterController.enabled = false;
        transform.position = startingPosition;
        characterController.enabled = true;

        // Reset other states related to velocity and input
        ResetRemainingJumps();
        yVelocity = 0.0f;
        isJumpButtonPressed = false;
    }
}
