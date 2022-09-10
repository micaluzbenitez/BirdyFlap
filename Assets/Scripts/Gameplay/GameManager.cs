using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    bool paused = false;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject[] obstaclesPrefabs;
    [SerializeField] private float distanceBetweenObstacles = 1;
    [SerializeField] private float initialPosX = 3.5f;

    [SerializeField] private float maxHeight = 3.7f;
    [SerializeField] private float minHeight = -2.6f;
    [SerializeField] private float limitLeft = -3.5f;
    [SerializeField] private float limitRight = 3.5f;

    [SerializeField] private UIGame uiGame = null;

    private float distanceToReset = 0;
    private bool[] justPassed = null;
    private bool[] justChecked = null;

    private bool playerAlive = false;

    [SerializeField] private float tubeSpeed = 1;
    [SerializeField] private float tubeAugmentCoef = 1.000001f;

    private float speedMultiplier = 1;
    private float timer = 0;

    private void Awake()
    {
        playerAlive = player.GetComponent<Player>().alive;
        Player.onPlayerCollision += StopMovement;
    }

    private void Start()
    {
        justPassed = new bool[obstacles.Length];
        justChecked = new bool[obstacles.Length];
        SetTubesPosition();

        paused = false;
        playerAlive = true;

    }

    private void Update()
    {
        if (playerAlive)
        {
            MoveTubes();
            CheckTubes();
            CheckScore();
        }
        else
        {
            uiGame.ShowEndScreen();
        }
    }

    private void StopMovement() => playerAlive = false;

    private void SetTubesPosition()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].transform.position = new Vector3(initialPosX + distanceBetweenObstacles * i, Random.Range(minHeight, maxHeight));
            justPassed[i] = false;
            justChecked[i] = false;
            CreateRandomObstacle(obstacles[i]);
        }

        distanceToReset = obstacles[obstacles.Length - 1].transform.position.x + distanceBetweenObstacles;
    }

    private void SetNewObstaclePos(ref GameObject obstacle, int actualPos)
    {
        Destroy(obstacle.transform.GetChild(0).gameObject);

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
        obstacle.transform.position = new Vector3(obstacles[lastObstacle].transform.position.x + distanceBetweenObstacles, Random.Range(minHeight, maxHeight));

        justPassed[actualPos] = false;
        justChecked[actualPos] = false;

        CreateRandomObstacle(obstacle);
    }

    private void CheckTubes() //Se fija cual ha llegado al final y lo resetea a una Y distinta
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (player.transform.position.x > obstacles[i].transform.position.x && !justPassed[i])
            {
                justPassed[i] = true;
            }
            if (obstacles[i].transform.position.x < limitLeft)
            {
                SetNewObstaclePos(ref obstacles[i], i);
            }
        }
    }

    private void CreateRandomObstacle(GameObject obstacle)
    {
        int index = Random.Range(0, obstaclesPrefabs.Length);
        GameObject go = Instantiate(obstaclesPrefabs[index], obstacle.transform.position, obstaclesPrefabs[index].transform.rotation);
        go.transform.SetParent(obstacle.transform);

        if (index == 3 || index == 4)
            obstacle.transform.position = new Vector3(obstacle.transform.position.x, maxHeight, obstacle.transform.position.z);
    }

    private void CheckScore()
    {
        for (int i = 0; i < justPassed.Length; i++)
        {
            if (justPassed[i])
            {
                if (!justChecked[i])
                {
                    uiGame.AddScore();
                    justChecked[i] = true;
                }
            }
        }
    }

    private void MoveTubes()
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
}
