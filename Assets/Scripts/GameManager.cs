using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private bool isTimeStopped = false;

    public static int currentLevel;

    private void OnEnable()
    {
        EventManager.OnLevelStart += StopMusic;
        EventManager.LevelBegin += Guide;
        EventManager.OnPauseGame += PauseGame;
        EventManager.OnExitLevel += ExitToMainMenu;
    }

    private void OnDisable()
    {
        EventManager.OnLevelStart -= StopMusic;
        EventManager.LevelBegin -= Guide;
        EventManager.OnPauseGame -= PauseGame;
        EventManager.OnExitLevel -= ExitToMainMenu;
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

        Resources.UnloadUnusedAssets();
        //GC.Collect();

        //PlayerPrefs.SetInt("currentLevel", currentLevel);
        /*if (PlayerPrefs.GetInt("currentLevel") != 0)
            currentLevel = PlayerPrefs.GetInt("currentLevel");*/
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

        currentLevel = 1;
    }

    public void PauseGame()
    {
        if (!isTimeStopped)
        {
            Time.timeScale = 0;
            isTimeStopped = true;
        }
        else
        {
            Time.timeScale = 1;
            isTimeStopped = false;
        }
    }

    public void PauseGame(GameObject[] objectsToFreeze)
    {
        if (!isTimeStopped)
        {
            Time.timeScale = 0;
            isTimeStopped = true;

            for (int i = 0; i < objectsToFreeze.Length; i++)
            {
                //objectsToFreeze[i].
            }
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
        {
            ca.Call("startActivity", launchIntent);
        }

        up.Dispose();
        ca.Dispose();
        packageManager.Dispose();
        launchIntent.Dispose();
    }

    public IEnumerator Guide(GameObject greyScreen, GameObject guideText)
    {
        greyScreen.SetActive(true);
        guideText.SetActive(true);
        yield return new WaitForSeconds(3f);
        greyScreen.SetActive(false);
        guideText.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("Main_Stage_Scene");
    }
}