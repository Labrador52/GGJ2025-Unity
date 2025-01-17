using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{

#region Gameplay Controls
    [ContextMenu("Start Game")]
    public void StartGame()
    {
        Debug.Log("Game Started");
    }

    [ContextMenu("Pause Game")]
    public void PauseGame()
    {
        Debug.Log("Game Paused");
    }

    [ContextMenu("Resume Game")]
    public void ResumeGame()
    {
        Debug.Log("Game Resumed");
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

}
