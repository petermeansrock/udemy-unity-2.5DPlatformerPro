using UnityEngine;
using UnityEngine.Events;

public class PlayerCollection : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<int> coinsUpdatedEvent; 

    private int coins = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.Coin))
        {
            coins++;
            coinsUpdatedEvent.Invoke(coins);
        }
    }
}
