using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinVerticalMovement : MonoBehaviour
{
    [SerializeField] private float vel = 1;

    private float moddifier;

    const float maxMod = 1.5f;
    const float minMod = 0.5f;
    const float maxX = 1.5f;
    const float minX = 0.5f;

    public static float maxY = 0.6f;
    public static float minY = -1.4f;

    private bool goingUp = true;

    private float parentPos;

    private void Awake()
    {
        moddifier = Random.Range(minMod, maxMod);
    }

    private void Start()
    {
        parentPos = GetComponentInParent<Transform>().position.y;
        Vector3 pos = transform.position;
        float newPos = pos.x + Random.Range(minX, maxX);
        transform.position = new Vector3 ( pos.x, pos.y + parentPos, pos.z);
    }

    void Update()
    {
        Vector3 pos = transform.position;
        if(isActiveAndEnabled)
        {
            

            if(goingUp)
            {
                transform.position = new Vector3(pos.x, pos.y + vel * Time.deltaTime * moddifier, pos.z);
            }
            else
            {
                transform.position = new Vector3(pos.x, pos.y - vel * Time.deltaTime * moddifier, pos.z);
            }

        }
         
    }
}
