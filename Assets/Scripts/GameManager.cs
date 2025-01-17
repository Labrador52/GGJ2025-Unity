using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [SerializeField] private GameState _gameState;
    public static GameState GameState
    {
        get
        {
            return Instance._gameState;
        }
    }

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

public enum GameState
{
    StartMenu,
    Gameplay
}