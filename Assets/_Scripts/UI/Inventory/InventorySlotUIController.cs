using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUIController : MonoBehaviour
{
    [SerializeField]
    private Image outlineImage_;

    [SerializeField]
    private Image iconImage_;

    private bool isSelected_;
    private Color original_;

    private void Awake()
    {
        original_ = iconImage_.color;
    }

    private void Start() { }

    public void SetOutlineImage(Sprite outline)
    {
        outlineImage_.sprite = outline;
    }

    public void SetIconImage(Sprite icon)
    {
        if (icon == null)
        {
            Color temp = iconImage_.color;
            temp.a = 0;
            iconImage_.color = temp;
        }
        else
        {
            iconImage_.color = original_;
            iconImage_.sprite = icon;
        }
    }
}
