using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    public void Start()
    {
        text.text = $"Coins {0}";
    }

    public void UpdateText(int value)
    {
        text.text = $"Coins: {value}";
    }
}
