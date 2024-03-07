using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    int currentMoney = 10;
    public int CurrentMoney
    {
        get => currentMoney;
        set => SetFunction(value);
    }
    int SetFunction(int value)
    {
        if (value <= -1) return currentMoney;
        Debug.Log("SETTING MONEY!!! CURRENT = " + currentMoney);
        return currentMoney = value;
    }
    //throw money function
}
