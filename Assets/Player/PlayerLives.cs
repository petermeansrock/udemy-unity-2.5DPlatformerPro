using UnityEngine;
using UnityEngine.Events;

public class PlayerLives : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;
    [SerializeField]
    private UnityEvent<int> livesUpdatedEvent;
    [SerializeField]
    private UnityEvent respawnEvent;

    private void Start()
    {
        livesUpdatedEvent.Invoke(lives);
    }

    private void Update()
    {
        if (transform.position.y < -9.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        if (lives > 0)
        {
            lives--;
            livesUpdatedEvent.Invoke(lives);

            if (lives > 0)
            {
                respawnEvent.Invoke();
            }
            else
            {
                DestroyPlayerWhileRetainingCamera();
            }
        }
    }

    private void DestroyPlayerWhileRetainingCamera()
    {
        Camera.main.transform.parent = null;
        Destroy(gameObject);
    }
}
