using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType
{
    Points,
    Coins
}

public struct Currency
{
    public int points;
    public int coins;
}

public class Manager : MonoBehaviour
{
    private static Manager instance = null;

    private Currency currency;

    private Currency maxCurrencyEarned;

    private static bool achievementPoints20 = false;
    private static bool achievementPoints35 = false;
    private static bool achievementPoints50 = false;
    private static bool achievementPoints75 = false;

    private static bool achievementAccumulate100 = false;
    private static bool achievementAccumulate500 = false;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            currency.coins = 0;
            currency.points = 0;
            maxCurrencyEarned.coins = 0;
            maxCurrencyEarned.points = 0;

            DontDestroyOnLoad(gameObject);
        }
    }

    public static Manager GetInstance()
    {
        return instance;
    }

    public Currency GetCurrency()
    {
        return currency;
    }

    public Currency GetMaxPoints()
    {
        return maxCurrencyEarned;
    }

    public void SetMaxPoints(int points)
    {
        maxCurrencyEarned.points = points;
    }

    public void SetMaxCoins(int coins)
    {
        maxCurrencyEarned.coins = coins;
    }

    public void SetPoints(int points)
    {
        currency.points = points;
    }
    
    public void SetCoins(int coins)
    {
        currency.coins = coins;
    }

    public static void CheckPointAchievement(int realizedPoints)
    {
        if (realizedPoints >= 20 && !achievementPoints20)
        {
            achievementPoints20 = true;
        }
        if (realizedPoints >= 35 && !achievementPoints35)
        {
            achievementPoints35 = true;
        }
        if (realizedPoints >= 50 && !achievementPoints50)
        {
            achievementPoints50 = true;
        }
        if (realizedPoints >= 75 && !achievementPoints75)
        {
            achievementPoints75 = true;
        }
    }

    public static void CheckAccumultarionAchievement(int totalAccumulated)
    {
        if (totalAccumulated >= 100 && !achievementAccumulate100)
        {
            achievementAccumulate100 = true;
        }
        if (totalAccumulated >= 500 && !achievementAccumulate500)
        {
            achievementAccumulate500 = true;
        }
    }
}
