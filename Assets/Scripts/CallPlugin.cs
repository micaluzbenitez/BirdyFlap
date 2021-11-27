using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CallPlugin : MonoBehaviour
{
    [SerializeField] private TMP_Text test; 

    public void CallThePlugin()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            test.text = Logger.DebugReadedFile();
#endif
        //Debug.Log("Se han modificado cosas en el archivo de logs");
    }
    public void EraseFile()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            Logger.CleanFile();
            test.text = Logger.DebugReadedFile();
#endif
        Debug.Log("Se ha limpiado el archivo de logs");
    }
}
