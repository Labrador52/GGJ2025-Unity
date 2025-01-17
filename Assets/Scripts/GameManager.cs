using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
#region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
#endregion

    public event EventHandler OnGameStart;
    // public event EventHandler OnGameStop;

    public void Awake()
    {
# region Singleton
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
# endregion

    }

    public void StartGame()
    {
        OnGameStart?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// End the game
    /// </summary>
    public static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
