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
    public bool FogMode;
    public float timeScale;
    [SerializeField] public bool isPlaying;
    [SerializeField] public bool isWin;
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
                //Bubble.Spawn(MapManager.instance.GetBeginWorldCoordinate());
                _bubbleSpawnWaiting = 0;
            }
        }

        Time.timeScale = timeScale;
    }

    #region Gameplay Controls
    [ContextMenu("Start Game")]
    public void StartGame()
    {
        LoadAnimation.instance.PlayLoadAnimation();
        Debug.Log("Game Started");
        LoadLevel(0);
        // FogOfWarManager.Instance.CreateFog();
        //StartMenu.Instance.gameObject.SetActive(false);
        StartCoroutine(CloseStartMenuWithDelay());
        
        // Enable Gameplay UI
        gameplayCanvas.SetActive(true);

        if (!FogMode)
            FogOfWarManager.Instance.CreateFogWithDelay(1f);
        
        isPlaying = true;
    }

    private IEnumerator CloseStartMenuWithDelay()
    {
        yield return new WaitForSeconds(1f);
        StartMenu.Instance.gameObject.SetActive(false);
    }

    private IEnumerator CloseWinPageWithDelay()
    {
        yield return new WaitForSeconds(1f);
        winPage.SetActive(false);
        DeleteLevel();
        LoadLevel(currentLevel);
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
        if (isWin) return;

        Debug.Log("Game Win");
        isPlaying = false;
        isWin = true;
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
        //CameraController.Instance.ResetPosition(GetLevelStartPosition(levelNumber));
        StartCoroutine(ResetCameraWithDelay(0.2f));

        _bubbleSpawnWaiting = 80;

        
    }

    private IEnumerator ResetCameraWithDelay(float _time)
    {
        yield return new WaitForSeconds(_time);
        CameraController.Instance.ResetPosition(GetLevelStartPosition());
    }

    public void LoadNextLevel()
    {
        LoadAnimation.instance.PlayLoadAnimation();
        
        currentLevel += 1;
        if (currentLevel >= 4)
        {
            Debug.LogError("No more levels");
            return;
            // show introduce page

        }
        StartCoroutine(CloseWinPageWithDelay());

        if (!FogMode)
            FogOfWarManager.Instance.CreateFogWithDelay(1f);

        // disable win page
        //winPage.SetActive(false);
        isPlaying = true;
    }

    private Vector3 GetLevelStartPosition()
    {
        Vector3 ret = MapManager.instance.GetBeginWorldCoordinate();
        return new Vector3(ret.x, ret.y, -10);

        //switch (levelNumber)
        //{
        //    case 0:
        //        return new Vector3(-12.5f, -4.5f, -50.0f);
        //    case 1:
        //        return new Vector3(-12.5f, -4.5f, -50.0f);
        //    case 2:
        //        return new Vector3(-12.5f, -4.5f, -50.0f);
        //    case 3:
        //        return new Vector3(-12.5f, -4.5f, -50.0f);
        //    case 4:
        //        return new Vector3(-12.5f, -4.5f, -50.0f);
        //}
        //return new Vector3(0.0f, 0.0f, -50.0f);
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
        isWin = false;

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
        CloudPool.instance.ReleaseAllCloud();
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
