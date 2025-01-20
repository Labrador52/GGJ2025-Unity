using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCanvas : MonoBehaviour
{
    public Button buttonSpawnBubble;
    public Button buildMode;
    public Button deleteBuildMode;

    private void Awake()
    {
        buttonSpawnBubble.onClick.AddListener(SpawnBubble);
        buildMode.onClick.AddListener(BuildMode);
        deleteBuildMode.onClick.AddListener(DeleteBuildMode);
    }

    private void SpawnBubble()
    {
        Bubble.Spawn(new Vector2(0.0f, 0.0f), new Vector2(0.0f, 0.0f));
    }

    private void BuildMode()
    {
        // start build mode
        BuildingManager.instance.EnterBuildingMode(0);
    }

    private void DeleteBuildMode()
    {
        BuildingManager.instance.EnterDeleteBuildingMode();
    }
}
