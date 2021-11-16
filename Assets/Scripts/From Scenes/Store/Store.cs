using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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

    private int totalCoins;
    private int totalPoints;

    Manager manager;

    void Awake()
    {
        manager = Manager.GetInstance();
        totalCoins = manager.GetCurrency().coins;
        totalPoints = manager.GetCurrency().points;
        coinCurr.text = totalCoins.ToString();
        pointCurr.text = totalPoints.ToString();
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

}
