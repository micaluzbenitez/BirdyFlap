using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
public class Auth : MonoBehaviour
{

    private static PlayGamesPlatform platform;

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Init();
#endif
    }
    private void Init()
    {


        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("\nLogged in successfully\n");
                Logger.SendLog("\nLogged in successfully\n");
            }
            else
            {
                Debug.Log("\nLogin Failed\n");
                Logger.SendLog("\nLogin Failed\n");
            }
        });

    }

    static public void ShowAchievements()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (Social.Active.localUser.authenticated)
        {

            platform.ShowAchievementsUI();
            Logger.SendLog("\nMostrando logros\n");
    }
#endif
}

    static public void UnlockAchievement(string a)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportProgress(a, 100f, success => { });
            Logger.SendLog("\nLogro desbloqueado!\n");
        }
#endif
    }

}