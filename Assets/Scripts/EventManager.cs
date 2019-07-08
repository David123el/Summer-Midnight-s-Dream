using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action OnLevelStart = delegate { };
    public static event Action OnLevelComplete = delegate { };
    public static event Action OnLevelFailed = delegate { };
    public static event Action OnSwitchBabyTeddy = delegate { };
    public static event Action OnBlockLand = delegate { };

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
}
