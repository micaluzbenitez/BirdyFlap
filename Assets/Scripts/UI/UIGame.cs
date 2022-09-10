using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIGame : MonoBehaviour
{
    [Header("Points UI")]
    [SerializeField] private TMP_Text txtPointsOnGame = null;
    [SerializeField] private TMP_Text txtPointsOnUI = null;

    [Header("End panel")]
    [SerializeField] private CanvasGroup endScreen = null;
    [SerializeField] private float UIshowSpeed = 1;

    private bool showEndScreen = false;
    private int pointsInGame = 0;

    [SerializeField] private Stats stats;

    private void Start()
    {
        pointsInGame = 0;
        UnloadEndScreen(endScreen);
        showEndScreen = false;
    }

    public void ShowEndScreen()
    {
        if (!showEndScreen)
        {
            txtPointsOnGame.text = "";
            txtPointsOnUI.text = pointsInGame.ToString();

            StartCoroutine(LoadPanelCoroutine(endScreen));
            showEndScreen = true;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Replay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void AddScore()
    {
        pointsInGame++;
        txtPointsOnGame.text = pointsInGame.ToString();
    }

    private bool HigherThanPrev(int actual, int prev)
    {
        return actual > prev;
    }

    private void UnloadEndScreen(CanvasGroup panel)
    {
        panel.alpha = 0;
        panel.interactable = false;
        panel.blocksRaycasts = false;
    }

    private IEnumerator LoadPanelCoroutine(CanvasGroup panel)
    {
        float t = 0;
        while (t < 1)
        {
            panel.alpha = Mathf.Lerp(0, 1, t);
            t += Time.deltaTime * UIshowSpeed;
            yield return null;
        }
        panel.alpha = 1;
        panel.interactable = true;
        panel.blocksRaycasts = true;
    }
}
