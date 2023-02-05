using UnityEngine;
using UnityEngine.UI;

public class HeartsBarUIController : MonoBehaviour
{
    [SerializeField]
    private PotionDrinker potionDrinker;

    [SerializeField]
    private HorizontalLayoutGroup heartsHolder;

    [SerializeField]
    private Sprite heartEmpty;

    [SerializeField]
    private Sprite heartFull;

    [SerializeField]
    private GameObject heartPrefab;

    private int heartsCount = 10; // just fixed now

    private void Awake() { }

    private void Start()
    {
        potionDrinker.OnHealthChanged += PotionDrinker_OnHealthChanged;

        for (int i = 0; i < heartsCount; ++i)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartsHolder.transform);
        }
        SetHeartPercentage(potionDrinker.health / potionDrinker.maxHealth);
    }

    private void OnDestroy()
    {
        potionDrinker.OnHealthChanged -= PotionDrinker_OnHealthChanged;
    }

    private void PotionDrinker_OnHealthChanged(float current, float max)
    {
        SetHeartPercentage(current / max);
    }

    private void SetHeartPercentage(float percentage)
    {
        int lastIdx = (int)Mathf.Floor(heartsCount * percentage);
        for (int i = 0; i < heartsCount; ++i)
        {
            heartsHolder.transform.GetChild(i).GetComponent<Image>().sprite = heartEmpty;
        }
        for (int i = 0; i < lastIdx; ++i)
        {
            heartsHolder.transform.GetChild(i).GetComponent<Image>().sprite = heartFull;
        }
    }
}
