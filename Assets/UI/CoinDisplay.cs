using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField]
    private Text coinText;

    public void OnCoinsUpdate(int coins)
    {
        coinText.text = $"Coins: {coins}";
    }
}
