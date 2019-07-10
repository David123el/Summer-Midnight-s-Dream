using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public new string name;
    public string levelToLoad;

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadExternalLevel(string name)
    {
        string bundleId = name; // your target bundle id
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");
    
        AndroidJavaObject launchIntent = null;
        try
        {
            launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage",bundleId);
            //launchIntent.Call("setFlags", /* 0x00010000 |*/ 0x10000000);
            ca.Call("startActivity",launchIntent);
        }
        finally
        {
            up.Dispose();
            ca.Dispose();
            packageManager.Dispose();
            launchIntent.Dispose();
        }

    }

    public void LoadLevel(string n)
    {
        if (n == "Level_11_Scene") {
            LoadExternalLevel("com.bbs.wedding11");
            return;
        } else if (n == "Level_06_Scene") {
            LoadExternalLevel("com.bbs.patterns06");
            return;
        } else if (n == "Level_07_Scene") {
            LoadExternalLevel("com.bbs.pong");
            return;
        }
        SceneManager.LoadScene(n);

        //SoundManager.Instance.StopMusic();
    }

    public void LoadLevel()
    {
        if (levelToLoad != string.Empty)
            LoadLevel(levelToLoad);
    }

    public void LoadLevelAsync(string name)
    {
        StartCoroutine(LoadYourAsyncScene(name));
    }

    public void LoadLevelAsync()
    {
        if (levelToLoad != string.Empty)
            StartCoroutine(LoadYourAsyncScene(levelToLoad));
    }

    public IEnumerator LoadYourAsyncScene(string name)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
