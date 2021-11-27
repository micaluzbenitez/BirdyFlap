using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{

    [SerializeField] private CanvasGroup mainMenuPanel;
    [SerializeField] private CanvasGroup creditPanel;

    [SerializeField] private Image hatSkin;
    [SerializeField] private Image beakSkin;
    [SerializeField] private Image eyesSkin;

    Manager manager;

    void Awake()
    {
        manager = Manager.GetInstance();
        GetBirdSkins();
        //Application.logMessageReceived += HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        Logger.WriteInContextFile(logString);
#endif
    }

    public float showSpeed = 1;
    public void Start()
    {
        LoadMainMenuPanel();
    }
    public void LoadMainMenuPanel()
    {
        StopAllCoroutines();
        UnloadPanel(creditPanel);
        LoadPanel(mainMenuPanel);
        Debug.Log("Inicia el panel de menu");
    }
    public void LoadCreditsPanel()
    {
        StopAllCoroutines();
        UnloadPanel(mainMenuPanel);
        LoadPanel(creditPanel);
        Debug.Log("Inicia el panel de creditos");
    }
    public void LoadStoreScene()
    {
        Debug.Log("Deja el menu, y se va a la tienda");
        SceneManager.LoadScene("Store");
    }
    private void UnloadPanel(CanvasGroup panel)
    {
        StartCoroutine(UnloadPanelCoroutine(panel));
    }
    private void LoadPanel(CanvasGroup panel)
    {
        StartCoroutine(LoadPanelCoroutine(panel));
    }
    IEnumerator UnloadPanelCoroutine(CanvasGroup panel)
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
    IEnumerator LoadPanelCoroutine(CanvasGroup panel)
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
    public void LoadGame(string scene)
    {
        Debug.Log("Deja el menu, y se va al juego"); 
        SceneManager.LoadScene(scene);
    }
    public void ExitGame()
    {
        Debug.Log("Saliendo del juego.");
        Application.Quit();
    }
    void GetBirdSkins()
    {
        for (int i = 0; i < manager.GetCosmeticList().Count; i++)
        {
            if(manager.GetCosmeticList()[i].IsEquipped())
            {
                switch (manager.GetCosmeticList()[i].cosmetic)
                {
                    case CosmeticType.Hat:
                        //Debug.Log("Skin equipado de sombrero: " + i);
                        hatSkin.sprite = manager.GetCosmeticList()[i].GetSprite();
                        break;
                    case CosmeticType.Beak:
                        //Debug.Log("Skin equipado de pico: " + i);
                        beakSkin.sprite = manager.GetCosmeticList()[i].GetSprite();
                        break;
                    case CosmeticType.Eyes:
                        //Debug.Log("Skin equipado de ojos: " + i);
                        eyesSkin.sprite = manager.GetCosmeticList()[i].GetSprite();
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public void ShowAchievements()
    {
        Auth.ShowAchievements();
    }

}
