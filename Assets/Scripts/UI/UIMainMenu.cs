using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainMenuPanel = null;
    [SerializeField] private CanvasGroup creditPanel = null;

    [SerializeField] private float showSpeed = 1;

    //[SerializeField] private Image hatSkin = null;
    //[SerializeField] private Image beakSkin= null;
    //[SerializeField] private Image eyesSkin = null;
    //[SerializeField] private Image eyesSkin = null;

    //private Manager manager = null;

    private void Awake()
    {
        //manager = Manager.GetInstance();
        //GetBirdSkins();
    }

    public void Start()
    {
        LoadMainMenuPanel();
    }

    public void LoadMainMenuPanel()
    {
        StopAllCoroutines();
        StartCoroutine(UnloadPanelCoroutine(creditPanel));
        StartCoroutine(LoadPanelCoroutine(mainMenuPanel));
    }

    public void LoadCreditsPanel()
    {
        StopAllCoroutines();
        StartCoroutine(UnloadPanelCoroutine(mainMenuPanel));
        StartCoroutine(LoadPanelCoroutine(creditPanel));
    }

    public void LoadGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadStore()
    {
        SceneManager.LoadScene("Store");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator UnloadPanelCoroutine(CanvasGroup panel)
    {
        if (panel.alpha > 0)
        {
            float t = 1;
            while (t > 0)
            {
                panel.alpha = Mathf.Lerp(0, 1, t);
                t -= Time.deltaTime * showSpeed;
                yield return null;
            }
            panel.alpha = 0;
            panel.interactable = false;
            panel.blocksRaycasts = false;
        }
    }

    private IEnumerator LoadPanelCoroutine(CanvasGroup panel)
    {
        float t = 0;
        while (t < 1)
        {
            panel.alpha = Mathf.Lerp(0, 1, t);
            t += Time.deltaTime * showSpeed;
            yield return null;
        }
        panel.alpha = 1;
        panel.interactable = true;
        panel.blocksRaycasts = true;
    }

    //private void GetBirdSkins()
    //{
    //    for (int i = 0; i < manager.GetCosmeticList().Count; i++)
    //    {
    //        if(manager.GetCosmeticList()[i].IsEquipped())
    //        {
    //            switch (manager.GetCosmeticList()[i].cosmetic)
    //            {
    //                case CosmeticType.Hat:
    //                    //Debug.Log("Skin equipado de sombrero: " + i);
    //                    hatSkin.sprite = manager.GetCosmeticList()[i].GetSprite();
    //                    break;
    //                case CosmeticType.Beak:
    //                    //Debug.Log("Skin equipado de pico: " + i);
    //                    beakSkin.sprite = manager.GetCosmeticList()[i].GetSprite();
    //                    break;
    //                case CosmeticType.Eyes:
    //                    //Debug.Log("Skin equipado de ojos: " + i);
    //                    eyesSkin.sprite = manager.GetCosmeticList()[i].GetSprite();
    //                    break;
    //                default:
    //                    break;
    //            }
    //        }
    //    }
    //}
}
