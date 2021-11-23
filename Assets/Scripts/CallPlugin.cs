using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPlugin : MonoBehaviour
{
    // Start is called before the first frame update
    public void CallThePlugin()
    {
        Logger.SendFilePath();
    }
}
