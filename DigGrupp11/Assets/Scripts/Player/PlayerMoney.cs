using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    int currentMoney = 10;
    public int CurrentMoney
    {
        get => currentMoney;
        set => SetFunction(value);
    }
    int SetFunction(int value)
    {
        if (value <= -1) return currentMoney;
        text.text = /*"Money in wallet: " +*/ value.ToString();
        Debug.Log("SETTING MONEY!!! CURRENT = " + currentMoney);
        return currentMoney = value;
    }
    public void MoneyTheft()
    {
        currentMoney--;
    }
    //throw money function
}
