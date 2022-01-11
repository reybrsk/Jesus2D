using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounterUI : MonoBehaviour
{

    [SerializeField] private TMP_Text text;
    private int _coinNumber;

    public void CountCoin(int amount)
    {
        _coinNumber += amount;
        text.text = _coinNumber.ToString();
    }
    
}
