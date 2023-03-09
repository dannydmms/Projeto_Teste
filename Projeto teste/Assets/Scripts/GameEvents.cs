using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public event Action<int> UpdateMoney;
    public void UpdateCurrentMoney(int money)
    {
        if(UpdateMoney != null)
        {
            UpdateMoney(money); 
        }
    }
    public event Action<int> UpdateCarryAmount;
    public void UpdateCurrentCarryAmount(int carryAmount)
    {
        if (UpdateCarryAmount != null)
        {
            UpdateCarryAmount(carryAmount);
        }
    }
    public event Action<int> UpdateUpgradeCost;
    public void UpdateCurrentCarryUpgradeCost(int upgradeCost)
    {
        if (UpdateUpgradeCost != null)
        {
            UpdateUpgradeCost(upgradeCost);
        }
    }
    public event Action<int> UpdateColorCost;
    public void UpdateCurrentColorText(int colorCost)
    {
        if (UpdateColorCost != null)
        {
            UpdateColorCost(colorCost);
        }
    }

}
