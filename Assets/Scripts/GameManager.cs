using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private bool isTimeStopped = false;

    public static int currentLevel = 1;

    private void OnEnable()
    {
        EventManager.OnLevelStart += StopMusic;
    }

    private void OnDisable()
    {
        EventManager.OnLevelStart -= StopMusic;
    }

    private void StopMusic()
    {
        SoundManager.Instance.StopMusic();
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {

    }

    public void PauseGame()
    {
        if (!isTimeStopped)
        {
            Time.timeScale = 0;
            isTimeStopped = true;
        }
    }

    public void LaunchApp()
    {
        bool fail = false;
        string bundleId = "com.google.MidSummerNightsDream"; // your target bundle id
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject launchIntent = null;
        try
        {
            launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", bundleId);
        }
        catch (System.Exception e)
        {
            fail = true;
        }

        if (fail)
        { //open app in store
            Application.OpenURL("https://google.com");
        }
        else //open the app
            ca.Call("startActivity", launchIntent);

        up.Dispose();
        ca.Dispose();
        packageManager.Dispose();
        launchIntent.Dispose();
    }
}