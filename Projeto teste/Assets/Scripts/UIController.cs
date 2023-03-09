using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI carryAmountText;
    [SerializeField] private TextMeshProUGUI carryCostText;
    [SerializeField] private TextMeshProUGUI colorCostText;

    private void Start()
    {
        GameEvents.instance.UpdateCarryAmount += UpdateCurrentCarryAmount;
        GameEvents.instance.UpdateColorCost += UpdateColorCostText;
        GameEvents.instance.UpdateMoney += UpdateCurrentMoney;
        GameEvents.instance.UpdateUpgradeCost += UpdateCurrentCarryUpgradeCost;
    }

    private void UpdateCurrentMoney(int money)
    {
        moneyText.text = money.ToString();
    }
    private void UpdateCurrentCarryAmount(int carryAmount)
    {
        carryAmountText.text = carryAmount.ToString();
    }
    private void UpdateCurrentCarryUpgradeCost(int upgradeCost)
    {
        carryCostText.text = upgradeCost.ToString();
    }
    private void UpdateColorCostText(int upgradCost)
    {
        colorCostText.text = upgradCost.ToString();
    }
}
