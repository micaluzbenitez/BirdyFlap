using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger
{
    const string PACK_NAME = "com.mobile.unity";

    const string LOGGER_CLASS_NAME = "MyPlugin";

    static AndroidJavaClass LoggerClass = null;

    static AndroidJavaObject LoggerInstance = null;

    static AndroidJavaClass unityPlayer = null;
    static AndroidJavaObject activity = null;
    static AndroidJavaObject context = null;

    static void init()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        LoggerClass = new AndroidJavaClass(PACK_NAME + "." + LOGGER_CLASS_NAME);
        LoggerInstance = LoggerClass.CallStatic<AndroidJavaObject>("GetInstance");
#endif
    }
    static void initContext()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        context = activity.Call<AndroidJavaObject>("getApplicationContext");
#endif
    }
    public static void SendLog(string msg)
    {
        if (LoggerInstance == null)
        {
            init();
        }

        //LoggerInstance.Call("MyPlugin", msg);
        LoggerInstance.Call("ShowMessage", msg);
    }

    public static void SendCurrency(int points, int coins, string from)
    {
        if (LoggerInstance == null)
        {
            init();
        }
        LoggerInstance?.Call("GetCurrency", points, coins, from);
    }


    public static void WriteInContextFile(string content)
    {

        if (LoggerInstance == null)
        {
            init();
        }

        if(context == null)
        {
            initContext();
        }

        LoggerInstance?.Call("addToFile", content, context);

        Debug.Log("Creado un nuevo Archivo. O se actualizo uno viejo.\n");

    }

    public static void CleanFile()
    {
        if (LoggerInstance == null)
        {
            init();
        }

        if (context == null)
        {
            initContext();
        }

        LoggerInstance?.Call("cleanFile", context);
        LoggerInstance?.Call("cleanSaveFile", context);

        Debug.Log("Archivos Reseteados.\n");
    }

    public static string DebugReadedFile()
    {
        if (LoggerInstance == null)
        {
            init();
        }

        if (context == null)
        {
            initContext();
        }

        string fileRead = LoggerInstance.Call<string>("readFromSaveFile", context);

        //Debug.Log("Mostrando el valor del archivo.");

        return fileRead;
    }

    public static void SaveCurrencyInFile(int points, int coins, int maxPoints, int maxCoins, List<Cosmetic> cos)
    {
        if (LoggerInstance == null)
        {
            init();
        }

        if (context == null)
        {
            initContext();
        }

        string data;

        data = points.ToString() + "_" + coins.ToString() + "_" + maxPoints.ToString() + "_" + maxCoins.ToString() + "-";

        for (int i = 0; i < cos.Count; i++)
        {
            data += cos[i].IsEquipped() ? "t" : "f";
            if(i<cos.Count-1)
            {
                data += "_";
            }
        }

        data += "-";

        for (int i = 0; i < cos.Count; i++)
        {
            data += cos[i].IsBought() ? "t" : "f";
            if (i < cos.Count - 1)
            {
                data += "_";
            }
        }

        Debug.Log(data);
        #if UNITY_ANDROID && !UNITY_EDITOR
        LoggerInstance?.Call("saveCurrency", data, context);
#endif
    }

}