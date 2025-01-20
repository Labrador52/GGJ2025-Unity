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

    [SerializeField] public bool isPlaying;
    [SerializeField] private int _bubbleSpawnInterval;
    [SerializeField] private int _bubbleSpawnWaiting;

    [SerializeField] private GameObject gameplayCanvas;
    [SerializeField] private GameObject winPage;
    [SerializeField] private int currentLevel;

    [SerializeField] private GameObject bubbles;
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
        // FogOfWarManager.Instance.CreateFog();
        StartMenu.Instance.gameObject.SetActive(false);

        // Enable Gameplay UI
        gameplayCanvas.SetActive(true);
        FogOfWarManager.Instance.CreateFog();
        
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
        // destory current level
        DeleteLevel();
        // load start menu
        StartMenu.Instance.gameObject.SetActive(true);
        // Disable Gameplay UI
        gameplayCanvas.SetActive(false);
    }

    [ContextMenu("Win")]
    public void Win()
    {
        Debug.Log("Game Win");
        isPlaying = false;

        // Show Win UI
        winPage.SetActive(true); 

        // destory current level
        // DeleteLevel();
        // load next level
        // LoadLevel(1);
    }
#endregion

#region Level Controls
    [ContextMenu("Load Level")]
    public void LoadLevel(int levelNumber)
    {
        currentLevel = levelNumber;
        Debug.Log("Level Loaded");
        InitializeLevel(levelNumber);
        // reset camera position
        CameraController.Instance.ResetPosition(GetLevelStartPosition(levelNumber));


        _bubbleSpawnWaiting = 80;

        
    }

    public void LoadNextLevel()
    {
        currentLevel += 1;
        if (currentLevel >= 3)
        {
            Debug.LogError("No more levels");
            return;
            // show introduce page

        }
        DeleteLevel();
        LoadLevel(currentLevel);
        FogOfWarManager.Instance.CreateFogWithDelay(1f);
        // disable win page
        winPage.SetActive(false);
        isPlaying = true;
    }

    private Vector3 GetLevelStartPosition(int levelNumber)
    {
        switch (levelNumber)
        {
            case 0:
                return new Vector3(-12.5f, -4.5f, -10.0f);
            case 1:
                return new Vector3(-12.5f, -4.5f, -10.0f);
            case 2:
                return new Vector3(-12.5f, -4.5f, -10.0f);
            case 3:
                return new Vector3(-12.5f, -4.5f, -10.0f);
            case 4:
                return new Vector3(-12.5f, -4.5f, -10.0f);
        }
        return new Vector3(0.0f, 0.0f, -10.0f);
    }
    

    [ContextMenu("Delete Level")]
    public void DeleteLevel()
    {
        // DeleteFan();
        MapManager.instance.DeleteAllBuild();
        Destroy(bubbles);
        bubbles = new GameObject("Bubbles");

        bubbles.transform.SetParent(gameObject.transform);
        
        Debug.Log("Level Deleted");
        // destory current level
        GameObject levelGameObject = GameObject.Find("Level");
        if (levelGameObject != null)
        {
            Destroy(levelGameObject);
        }
        

    }
#endregion

    // Delete Fan (clone) Gameobjects
    // public void DeleteFan()
    // {

    // }

    // reload Scene
    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void InitializeLevel(int level)
    {

        GameObject levelGameObject = Instantiate(PrefabManager.Instance.LevelPrefabs[level], gameObject.transform);
        // set name as Level
        levelGameObject.name = "Level";
        levelGameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        GameObject gridGameObject = levelGameObject.transform.Find("Grid").gameObject;
        // Debug.Log(gridGameObject);
        List<BuildableItem> allBuildale;
        allBuildale = PrefabManager.AllBuildable;

        // Initialize the managers by fyns
        BubblePool.instance.ReleaseAllBubble();
        //BubblePool.instance.pool.Clear();

        BuildingManager.instance.Initial(gridGameObject, allBuildale);

        MapManager.instance.Initial(gridGameObject, PrefabManager.TileInstantiat, PrefabManager.Tile);

        Inventory.instance.Initial(PrefabManager.AllMaterials);  
    }

    public void SetInterval(int internvl)
    {
        _bubbleSpawnInterval = internvl;
    }
}
