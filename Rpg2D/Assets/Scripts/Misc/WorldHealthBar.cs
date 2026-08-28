using UnityEngine;
using UnityEngine.UI;

public class WorldHealthBar : MonoBehaviour
{
    private Image _fillImage;

    private void Awake()
    {
        _fillImage = transform.Find("Fill").GetComponent<Image>();
    }

    public void UpdateHealthBar(float percentage)
    {
        _fillImage.fillAmount = percentage;
    }
}