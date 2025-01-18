using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
#region Singleton
    private static Gameplay _instance;
    public static Gameplay Instance
    {
        get
        {
            return _instance;
        }
    }
#endregion

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

#region Gameplay Controls
    [ContextMenu("Start Game")]
    public void StartGame()
    {
        Debug.Log("Game Started");
        InitializeLevel(0);
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        Debug.Log("Game Restarted");
    }

    [ContextMenu("End Game")]
    public void EndGame()
    {
        Debug.Log("Game Ended");
    }
#endregion

#region Level Controls
    [ContextMenu("Load Level")]
    public void LoadLevel()
    {
        Debug.Log("Level Loaded");
    }
#endregion

    private void InitializeLevel(int level)
    {
        GameObject levelGameObject = Instantiate(PrefabManager.Instance.LevelPrefabs[level], gameObject.transform);
        levelGameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        GameObject gridGameObject = levelGameObject.transform.Find("Grid").gameObject;
        Debug.Log(gridGameObject);
    }
}
