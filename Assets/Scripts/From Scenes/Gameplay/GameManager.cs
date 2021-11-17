using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    Manager manager;

    bool paused = false;

    [SerializeField] private TMP_Text txtPointsOnGame;
    [SerializeField] private TMP_Text txtPointsOnUI;
    [SerializeField] private TMP_Text txtCoinsOnUI;
    [SerializeField] private TMP_Text txtCoinsTotal;
    [SerializeField] private TMP_Text txtPointsTotal;

    [SerializeField] private TMP_Text txtHighPoints;
    [SerializeField] private TMP_Text txtHighCoins;

    [SerializeField] private CanvasGroup endScreen;
    public float UIshowSpeed = 1;
    private bool shownEndScreen = false;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float distanceBetweenObstacles = 1;
    [SerializeField] private float initialPosX = 3.5f;
    
    private const float MaxHeight = 3.7f;
    private const float MinHeight = -2.6f;
    private const float LimitLeft = -3.5f;
    private const float LimitRight = 3.5f;

    public float maxHeight = MaxHeight;
    public float minHeight = MinHeight;

    private float distanceToReset = 0;
    private bool[] justPassed = null;
    private bool[] justChecked = null;

    private int pointsInGame = 0;
    private int coinsInGame = 0;
    private int pointsTotal = 0;
    private int coinsTotal = 0;

    private bool playerAlive = false;

    private Currency higher;

    [SerializeField] private float tubeSpeed = 1;
    [SerializeField] private float tubeAugmentCoef = 1.0000001f;

    private float speedMultiplier = 1;
    private float timer = 0;

    private void Awake()
    {
        manager = Manager.GetInstance();
        playerAlive = player.GetComponent<Player>().alive;
        Player.onPlayerCollision += StopMovement;

        pointsTotal = manager.GetCurrency().points;
        coinsTotal = manager.GetCurrency().coins;

        higher = manager.GetMaxPoints();

        txtHighPoints.text = higher.points.ToString();
        txtHighCoins.text = higher.coins.ToString();
    }

    // Start is called before the first frame update
    

    public void Start()
    {
        UnloadEndScreen(endScreen);
        justPassed = new bool[obstacles.Length];
        justChecked = new bool[obstacles.Length];
        SetTubesPosition();

        paused = false;
        playerAlive = true;
        shownEndScreen = false;

        higher = manager.GetMaxPoints();

        pointsInGame = 0;
        coinsInGame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAlive)
        {
            MoveTubes();
            CheckTubes();
            CheckScore();
        }
        else
        {
            if(!shownEndScreen)
            {
                txtPointsOnGame.text = "0";
                txtPointsOnUI.text = pointsInGame.ToString();
                txtCoinsOnUI.text = coinsInGame.ToString();

                pointsTotal += pointsInGame;
                coinsTotal += coinsInGame;

                txtPointsTotal.text = GetTotalCurrency(pointsTotal);
                txtCoinsTotal.text = GetTotalCurrency(coinsTotal);

                if(HigherThanPrev(pointsInGame,higher.points))
                {
                    txtHighPoints.text = pointsInGame.ToString();
                    manager.SetMaxPoints(pointsInGame);
                }

                if (HigherThanPrev(coinsInGame, higher.coins))
                {
                    txtHighCoins.text = coinsInGame.ToString();
                    manager.SetMaxCoins(coinsInGame);
                }

                LoadEndScreen(endScreen);
                shownEndScreen = true;
            }
        }

    }
    void StopMovement() => playerAlive = false;
    void SetTubesPosition()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].transform.position = new Vector3(initialPosX + distanceBetweenObstacles * i, Random.Range(minHeight, maxHeight));
            justPassed[i] = false;
            justChecked[i] = false;
        }
        distanceToReset = obstacles[obstacles.Length - 1].transform.position.x + distanceBetweenObstacles;
    }
    void SetNewObstaclePos(ref GameObject o, int actualPos)
    {
        int lastObstacle = 0;
        switch (actualPos)
        {
            case 0:
                lastObstacle = 2;
                break;
            case 1:
                lastObstacle = 0;
                break;
            case 2:
                lastObstacle = 1;
                break;
            default:
                break;
        }
        o.transform.position = new Vector3(obstacles[lastObstacle].transform.position.x + distanceBetweenObstacles, Random.Range(minHeight, maxHeight));
        justPassed[actualPos] = false;
        justChecked[actualPos] = false;
    }
    void CheckTubes()//Se fija cual ha llegado al final y lo resetea a una Y distinta
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (player.transform.position.x > obstacles[i].transform.position.x && !justPassed[i])
            {
                justPassed[i] = true;
            }
            if (obstacles[i].transform.position.x < LimitLeft)
            {
                SetNewObstaclePos(ref obstacles[i], i);
            }
        }
    }
    void CheckScore()
    {
        for (int i = 0; i < justPassed.Length; i++)
        {
            if (justPassed[i])
            {
                if (!justChecked[i])
                {
                    pointsInGame++;
                    txtPointsOnGame.text = pointsInGame.ToString();
                    Debug.Log(pointsInGame);
                    justChecked[i] = true;
                }
            }
        }
    }
    void MoveTubes()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].transform.position = new Vector3(obstacles[i].transform.position.x - tubeSpeed * Time.deltaTime * speedMultiplier, obstacles[i].transform.position.y);
        }
        if (timer > 1)
        {
            timer = 0;
            speedMultiplier *= tubeAugmentCoef;
        }
        timer += Time.deltaTime;
    }
    bool HigherThanPrev(int actual, int prev)
    {
        return actual > prev;
    }
    void LoadEndScreen(CanvasGroup panel)
    {
        StartCoroutine(LoadPanelCoroutine(panel));
    }
    void UnloadEndScreen(CanvasGroup panel)
    {
        panel.alpha = 0;
        panel.interactable = false;
        panel.blocksRaycasts = false;
    }
    IEnumerator LoadPanelCoroutine(CanvasGroup panel)
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
    string GetTotalCurrency(int c)
    {
        string txt = "(";
        txt += c.ToString();
        txt += ")";
        return txt;
    }
    public void BackToMenu()
    {
        SendCurrency();
        SceneManager.LoadScene("MainMenu");
    }
    public void EnterStore()
    {
        SendCurrency();
        SceneManager.LoadScene("Store");
    }
    private void SendCurrency()
    {
        manager.SetCoins(coinsTotal);
        manager.SetPoints(pointsTotal);
    }
}
