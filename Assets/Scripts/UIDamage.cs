using UnityEngine;
using UnityEngine.UI;

public class UIDamage : MonoBehaviour
{
    public static UIDamage Instance { get; private set; }

    [Header("UI Images (assign in inspector)")]
    [SerializeField] private Image[] damageImages;

    [Header("Optional: assign a parent to auto-fill images")]
    [SerializeField] private Transform imagesParent;

    private int maxLives = 3;
    private int currentLives = 3;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        if ((damageImages == null || damageImages.Length == 0) && imagesParent != null)
        {
            damageImages = imagesParent.GetComponentsInChildren<Image>(true);
        }

        maxLives = damageImages != null ? damageImages.Length : 0;
        currentLives = maxLives;
        UpdateUI();
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void ApplyDamage(int amount = 1)
    {
        if (maxLives == 0) return;
        currentLives = Mathf.Clamp(currentLives - amount, 0, maxLives);
        UpdateUI();
    }

    public void Heal(int amount = 1)
    {
        if (maxLives == 0) return;
        currentLives = Mathf.Clamp(currentLives + amount, 0, maxLives);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (damageImages == null) return;
        for (int i = 0; i < damageImages.Length; i++)
        {
            bool enabledState = i < currentLives;
            // Use GameObject.SetActive to ensure the UI element is hidden
            damageImages[i].gameObject.SetActive(enabledState);
        }
    }
}
