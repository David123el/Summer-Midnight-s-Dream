using System;
using System.Collections;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action OnLevelStart = delegate { };
    public static event Action OnLevelComplete = delegate { };
    public static event Action OnLevelFailed = delegate { };
    public static event Action OnSwitchBabyTeddy = delegate { };
    public static event Action OnBlockLand = delegate { };
    public static event Action<GameObject[]> OnPauseGame = delegate { };

    public delegate IEnumerator LevelBeginEventHandler(GameObject a, GameObject b);
    public static event LevelBeginEventHandler LevelBegin;

    public void OnLevelBegin(GameObject a, GameObject b)
    {
        //LevelBeginEventHandler levelBegin = LevelBegin;
        if (LevelBegin != null)
            StartCoroutine(LevelBegin(a, b));
    }

    public static void LevelStart()
    {
        OnLevelStart();
    }

    public static void LevelComplete()
    {
        OnLevelComplete();
    }

    public static void LevelFailed()
    {
        OnLevelFailed();
    }

    public static void SwitchBabyTeddy()
    {
        OnSwitchBabyTeddy();
    }

    public static void BlockLand()
    {
        OnBlockLand();
    }

    public static void PauseGame(GameObject[] objects)
    {
        OnPauseGame(objects);
    }
}
