using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Store : MonoBehaviour
{

    [SerializeField] private CanvasGroup panel;

    [SerializeField] private TMP_Text coinCurr;
    [SerializeField] private TMP_Text pointCurr;

    [SerializeField] private TMP_Text price;
    [SerializeField] private GameObject priceCoinCurr;
    [SerializeField] private GameObject pricePointsCurr;

    [SerializeField] private GameObject hat;
    [SerializeField] private GameObject eyes;
    [SerializeField] private GameObject beak;

    private Image hatSprite;
    private Image eyesSprite;
    private Image beakSprite;

    private List<Cosmetic> cosmetics;

    private int totalCoins;
    private int totalPoints;

    private int actualIndex = 0; 
    private int nextIndex = 0;

    Manager manager;

    void Awake()
    {
        manager = Manager.GetInstance();
        totalCoins = manager.GetCurrency().coins;
        //totalCoins = 9999;
        totalPoints = manager.GetCurrency().points;
        //totalPoints = 9999;
        coinCurr.text = totalCoins.ToString();
        pointCurr.text = totalPoints.ToString();
        cosmetics = manager.GetCosmeticList();

        hatSprite = hat.GetComponent<Image>();
        beakSprite = beak.GetComponent<Image>();
        eyesSprite = eyes.GetComponent<Image>();


        price.text = cosmetics[actualIndex].IsEquipped() ? "EQUIPPED" : "BOUGHT";

    }

    void Start()
    {
        LoadPanelCoroutine(panel);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator LoadPanelCoroutine(CanvasGroup panel)
    {
        float t = 0;
        while (t < 1)
        {
            panel.alpha = Mathf.Lerp(0, 1, t);
            t += Time.deltaTime;
            yield return null;
        }
        panel.alpha = 1;
        panel.interactable = true;
        panel.blocksRaycasts = true;
    }
    int GetACorrectIndex(int i)
    {
        if (i >= cosmetics.Count) 
        { 
            i -= cosmetics.Count; 
        }
        if (i < 0) 
        { 
            i += cosmetics.Count; 
        }
        return i;
    }
    public void GetRightIndex()
    {
        ResetSkin(actualIndex);
        int aux = actualIndex+1;
        nextIndex = GetACorrectIndex(aux);
        ShowSkin(nextIndex);
    }
    public void GetLeftIndex()
    {
        ResetSkin(actualIndex);
        int aux = actualIndex-1;
        nextIndex = GetACorrectIndex(aux);
        ShowSkin(nextIndex);
    }
    void ShowSkin(int index)
    {
        actualIndex = index;

        Debug.Log("Mostrando Skin i="+index);

        switch (cosmetics[index].cosmetic)
        {
            case CosmeticType.Hat:
                hatSprite.sprite = cosmetics[index].GetSprite();
                break;
            case CosmeticType.Beak:
                beakSprite.sprite = cosmetics[index].GetSprite();
                break;
            case CosmeticType.Eyes:
                eyesSprite.sprite = cosmetics[index].GetSprite();
                break;
            default:
                break;
        }
        if (!cosmetics[index].IsBought())
        {
            if (cosmetics[index].GetPrice().currencyType.Equals(CurrencyType.Points))
            {
                pricePointsCurr.SetActive(true);
            }
            else
            { 
                priceCoinCurr.SetActive(true);
            }
            price.text = cosmetics[index].GetPrice().quantity.ToString();
        }
        else
        {
            price.text = !cosmetics[index].IsEquipped() ? "BOUGHT" : "EQUIPPED";
        }
    }
    void ResetSkin(int i)
    {
        pricePointsCurr.SetActive(false);
        priceCoinCurr.SetActive(false);
        price.text = "";
        hatSprite.sprite = cosmetics[Manager.GetDefaultSkin().bird.hat_color].GetSprite();
        beakSprite.sprite = cosmetics[Manager.GetDefaultSkin().bird.beak].GetSprite();
        eyesSprite.sprite = cosmetics[Manager.GetDefaultSkin().bird.eyes].GetSprite();
    }

    public void BuySkin()
    {
        if(!cosmetics[actualIndex].IsBought())
        {
            if(cosmetics[actualIndex].GetPrice().currencyType.Equals(CurrencyType.Points))
            {
                if (totalPoints >= cosmetics[actualIndex].GetPrice().quantity)
                {
                    totalPoints -= cosmetics[actualIndex].GetPrice().quantity;
                    cosmetics[actualIndex].Buy();
                    manager.SetPoints(totalPoints);
                    pointCurr.text = totalPoints.ToString();
                    Debug.Log("Comprado item " + actualIndex + " a " + cosmetics[actualIndex].GetPrice().quantity + " puntos.");
#if UNITY_ANDROID && !UNITY_EDITOR
                    Logger.SaveCurrencyInFile(totalPoints, totalCoins, manager.GetMaxPoints().points, manager.GetMaxPoints().coins,manager.GetCosmeticList());
#endif

                }
            }
            else
            {
                if (totalCoins >= cosmetics[actualIndex].GetPrice().quantity)
                {
                    totalCoins -= cosmetics[actualIndex].GetPrice().quantity;
                    cosmetics[actualIndex].Buy();
                    manager.SetCoins(totalCoins);
                    coinCurr.text = totalCoins.ToString();
                    Debug.Log("Comprado item " + actualIndex + " a " + cosmetics[actualIndex].GetPrice().quantity + " coins.");
#if UNITY_ANDROID && !UNITY_EDITOR
                    Logger.SaveCurrencyInFile(totalPoints, totalCoins, manager.GetMaxPoints().points, manager.GetMaxPoints().coins,manager.GetCosmeticList());
#endif
                }
            }
            if (cosmetics[actualIndex].IsBought())
            {
                pricePointsCurr.SetActive(false);
                priceCoinCurr.SetActive(false);
                price.text = "BOUGHT";
                Debug.Log("Item " + actualIndex + " cambia su estado a 'COMPRADO'");
            }
        } 
    }
    public void EquipSkin()
    {
        if (cosmetics[actualIndex].IsBought())
        {
            if (!cosmetics[actualIndex].IsEquipped())
            {
                for (int i = 0; i < cosmetics.Count; i++)
                {
                    if (cosmetics[i].cosmetic.Equals(cosmetics[actualIndex].cosmetic))
                    {
                        cosmetics[i].UnEquip();
                    }
                }
                cosmetics[actualIndex].Equip();
                if(cosmetics[actualIndex].IsEquipped())
                {
                    price.text = "EQUIPPED";
                    Debug.Log("Equipado el item " + actualIndex);
                }
            }
        }
    }
}
