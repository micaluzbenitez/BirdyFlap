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
            float timer = 1;
            while (timer > 0)
            {
                panel.alpha = Mathf.Lerp(0, 1, timer);
                timer -= Time.deltaTime * showSpeed;
                yield return null;
            }
            panel.alpha = 0;
            panel.interactable = false;
            panel.blocksRaycasts = false;
        }
    }

    private IEnumerator LoadPanelCoroutine(CanvasGroup panel)
    {
        float timer = 0;
        while (timer < 1)
        {
            panel.alpha = Mathf.Lerp(0, 1, timer);
            timer += Time.deltaTime * showSpeed;
            yield return null;
        }
        panel.alpha = 1;
        panel.interactable = true;
        panel.blocksRaycasts = true;
    }
}
