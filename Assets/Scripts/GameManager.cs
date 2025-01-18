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

    [SerializeField] private GlobalCfgScriptObject _globalCfg;

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
#region Singleton
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

        ApplyGlobalConfig();
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
    
    private void ApplyGlobalConfig()
    {
        if (_globalCfg == null)
        {
            Debug.LogError("GlobalCfg is not assigned in the inspector");
        }
        else
        {
            // Set Trigger Range
            Bubble.SetTriggerRange(TriggerType.Body, _globalCfg.TriggerBody);
            Bubble.SetTriggerRange(TriggerType.Pickup, _globalCfg.TriggerPickup);
            Bubble.SetTriggerRange(TriggerType.Fog, _globalCfg.TriggerFog);

            // Set Buggle Height
            Bubble.SetSolidHeight(_globalCfg.solidHeight);

            Gameplay.Instance.SetInterval(_globalCfg.bubbleSpawnInterval);
        }
    }

}

public enum GameState
{
    StartMenu,
    Gameplay
}