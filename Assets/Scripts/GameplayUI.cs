using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
#region Singleton
    private static GameplayUI _instance;
    public static GameplayUI Instance
    {
        get
        {
            return _instance;
        }
    }
#endregion

    private void Awake()
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
#endregion

        if (GameManager.GameState != GameState.Gameplay)
        {
            gameObject.SetActive(false);
        }
    }
}
