using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform startTarget;
    [SerializeField]
    private Transform endTarget;
    [SerializeField]
    private float speed = 3.0f;

    private Vector3 currentTarget;
    private Vector3 nextTarget;

    void Start()
    {
        transform.position = startTarget.position;
        currentTarget = endTarget.position;
        nextTarget = startTarget.position;
    }

    private void FixedUpdate()
    {
        float distance = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, distance);

        // Swap targets if we approximately reach the current target
        if (Vector3.Distance(transform.position, currentTarget) < 0.001f)
        {
            var temp = currentTarget;
            currentTarget = nextTarget;
            nextTarget = temp;
        }
    }
}
