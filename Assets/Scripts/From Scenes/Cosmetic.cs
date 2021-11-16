using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CosmeticType
{
    Hat,
    Beak,
    Eyes
}
public struct Price
{
    public int quantity;
    public CurrencyType currencyType;
}

[System.Serializable]
public class Cosmetic
{
    public CosmeticType cosmetic;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Price price;

    private bool bought = false;

    public void Buy() => bought = true;
    public bool IsBought() { return bought; }    
    public Price GetPrice() { return price; }    
    
}
