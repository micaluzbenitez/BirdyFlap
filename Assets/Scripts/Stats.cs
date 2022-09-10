using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stats : MonoBehaviour
{
    private int points = 0;

    void Awake()
    {
        points = 0;
    }

    public int GetPoints()
    {
        return points;
    }

    public void SetPoints(int points)
    {
        this.points = points;
    }
}
