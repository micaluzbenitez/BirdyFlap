using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Manager : MonoBehaviour
{
    private static Manager instance = null;
    private int points = 0;
    private int maxPoints = 0;

    private static bool achievementPoints20 = false;
    private static bool achievementPoints35 = false;
    private static bool achievementPoints50 = false;
    private static bool achievementPoints75 = false;

    private static bool achievementAccumulate100 = false;
    private static bool achievementAccumulate500 = false;

    void Awake()
    {
        points = 0;
        maxPoints = 0;
    }

    public int GetPoints()
    {
        return points;
    }

    public int GetMaxPoints()
    {
        return maxPoints;
    }

    public void SetMaxPoints(int points)
    {
        maxPoints = points;
    }

    public void SetPoints(int points)
    {
        this.points = points;
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
