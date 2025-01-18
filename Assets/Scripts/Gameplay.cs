using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
#region Singleton
    private static Gameplay _instance;
    public static Gameplay Instance
    {
        get
        {
            if (_instance == null)
            {
                return GameObject.Find("Gameplay").GetComponent<Gameplay>();
            }
            return _instance;
        }
    }
#endregion

    [SerializeField] private bool isPlaying;
    [SerializeField] private int _bubbleSpawnInterval;
    [SerializeField] private int _bubbleSpawnWaiting;

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

    private void FixedUpdate()
    {
        if (isPlaying)
        {
            _bubbleSpawnWaiting += 1;
            if (_bubbleSpawnWaiting == _bubbleSpawnInterval)
            {
                Bubble.Spawn(MapManager.instance.GetBeginWorldCoordinate());
                _bubbleSpawnWaiting = 0;
            }
        }
    }

#region Gameplay Controls
    [ContextMenu("Start Game")]
    public void StartGame()
    {
        Debug.Log("Game Started");
        LoadLevel(0);
        FogOfWarManager.Instance.CreateFog();
        StartMenu.Instance.gameObject.SetActive(false);

        isPlaying = true;
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        Debug.Log("Game Restarted");
        // destory current level
        // load level
    }

    [ContextMenu("End Game")]
    public void EndGame()
    {
        Debug.Log("Game Ended");
        isPlaying = false;
    }
#endregion

#region Level Controls
    [ContextMenu("Load Level")]
    public void LoadLevel(int levelNumber)
    {
        Debug.Log("Level Loaded");
        InitializeLevel(levelNumber);
    }
#endregion

    private void InitializeLevel(int level)
    {

        GameObject levelGameObject = Instantiate(PrefabManager.Instance.LevelPrefabs[level], gameObject.transform);
        levelGameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        GameObject gridGameObject = levelGameObject.transform.Find("Grid").gameObject;
        // Debug.Log(gridGameObject);
        List<BuildableItem> allBuildale;
        allBuildale = PrefabManager.AllBuildable;
        BuildingManager.instance.Initial(gridGameObject, allBuildale);

        MapManager.instance.Initial(gridGameObject, PrefabManager.TileInstantiat, PrefabManager.Tile);

        Inventory.instance.Initial(PrefabManager.AllMaterials);
    }

    public void SetInterval(int internvl)
    {
        _bubbleSpawnInterval = internvl;
    }
}
