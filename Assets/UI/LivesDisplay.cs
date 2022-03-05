using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField]
    private Text livesText;

    public void OnLivesUpdate(int lives)
    {
        livesText.text = $"Lives: {lives}";
    }
}
