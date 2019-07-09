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

    public void LoadLevel(string n)
    {
        SceneManager.LoadScene(n);
    }

    public void LoadLevel()
    {
        if (levelToLoad != string.Empty)
            SceneManager.LoadScene(levelToLoad);
    }

    public void LoadLevelAsync(string name)
    {
        StartCoroutine(LoadYourAsyncScene(name));
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
