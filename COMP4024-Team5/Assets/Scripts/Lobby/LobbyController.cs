using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite[] levelBackgrounds;

    private void Start()
    {
        PlayerController player = Object.FindFirstObjectByType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("Player object not found!");
            return;
        }
        
        int level = player.level;
        int index = level - 1;

        Debug.Log(index);

        if (index >= 0 && index < levelBackgrounds.Length)
        {
            backgroundImage.sprite = levelBackgrounds[index];
            Debug.Log($"Loaded background for level {level}");
        }
        else
        {
            Debug.LogWarning($"No background defined for level {level}");
        }
    }
}
