using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger
{
    const string PACK_NAME = "com.mobile.unity";

    const string LOGGER_CLASS_NAME = "MyPlugin";

    static AndroidJavaClass LoggerClass = null;

    static AndroidJavaObject LoggerInstance = null;

    static AndroidJavaClass playerClass = null;
    static AndroidJavaObject activity = null;
    static AndroidJavaObject context = null;

    static void init()
    {
        LoggerClass = new AndroidJavaClass(PACK_NAME + "." + LOGGER_CLASS_NAME);
        LoggerInstance = LoggerClass.CallStatic<AndroidJavaObject>("GetInstance");

    }
    public static void SendLog(string msg)
    {
        if (LoggerInstance == null)
        {
            init();
        }

        //LoggerInstance.Call("MyPlugin", msg);
        LoggerInstance.Call("ShowMessage",msg);
    }

    public static void SendCurrency(int points, int coins, string from)
    {
        if (LoggerInstance == null)
        {
            init();
        }
        LoggerInstance?.Call("GetCurrency", points, coins, from);
    }

    public static void SendFilePath()
    {
        if (LoggerInstance == null)
        {
            SendLog("SendFile antes de crear nada");
            init();
        }
        LoggerInstance?.Call("ShowMessage", "SendFile Antes de crear la activity");

        if (activity == null)
        {
            playerClass = new AndroidJavaClass(PACK_NAME + "." + LOGGER_CLASS_NAME);
            LoggerInstance?.Call("ShowMessage", "Sendfile despues de crear el playerClass");
            activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            LoggerInstance?.Call("ShowMessage", "Despues de crear la activity");
        }
        if(activity!=null)
            activity?.Call("CreateDirectory", activity);
        else        
            LoggerInstance?.Call("ShowMessage", "LA ACTIVITY NO HACE UN PITO");
    }
}