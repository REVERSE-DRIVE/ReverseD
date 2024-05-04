using System;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private int _resourceAmount;

    public int ResourceAmount => _resourceAmount;

    private void Awake()
    {
        
    }

    public void ApplyResource(int amount)
    {
        _resourceAmount += amount;
    }

    // 뭔가 함수명 맘에 안드는데
    public bool LoseResource(int amount)
    {
        if (amount > _resourceAmount)
            return false;
        _resourceAmount -= amount;
        return true;
    }

    private void RefreshAmount()
    {
        _text.text = _resourceAmount.ToString();
    }
}