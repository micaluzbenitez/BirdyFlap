using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkinHatColor
{
    Red,
    LightBlue,
    Purple,
    Blue,
    Green
}
public enum SkinBeakColor
{
    Gray,
    Black
}
public enum SkinEyeColor
{
    Black,
    LightBlue
}
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
public struct Bird
{
    public int hat_color;
    public int eyes;
    public int beak;
}
public struct Skin
{
    public Bird bird;
    public int tube;
}
public class Manager : MonoBehaviour
{
    private static Manager instance = null;

    public static int SkinHatColorCount = 5;
    public static int SkinBeakColorCount = 2;
    public static int SkinEyeColorCount = 2;

    public List<Cosmetic> cosmetics = new List<Cosmetic>(SkinBeakColorCount + SkinEyeColorCount + SkinHatColorCount);

    private Currency currency;
    public Skin skin;

    private Currency maxCurrencyEarned;

    public static Skin GetDefaultSkin()
    { 
        Skin def = new Skin();

        def.bird.beak = 5;
        def.bird.hat_color = 0;
        def.bird.eyes = 7;

        return def;
    }

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

            skin.bird.eyes = 7;
            skin.bird.hat_color = 0;
            skin.bird.beak = 5;

            skin.tube = 0;



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
    public Skin GetSkins()
    {
        return skin;
    }
    public void SelectBirdSkin(int index)
    {
        //skin.bird = index;
    }
    public void SelectTubeSkin(int index)
    {
        skin.tube = index;
    }
    public List<Cosmetic> GetCosmeticList()
    {
        return cosmetics;
    }
}
