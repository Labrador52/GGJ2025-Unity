using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
#region Singleton
    private static PrefabManager _instance;
    public static PrefabManager Instance
    {
        get
        {
            return _instance;
        }
    }
#endregion

    // prefab bubble
    [SerializeField] private GameObject _bubblePrefab;

    public GameObject BubblePrefab
    {
        get
        {
            return _instance._bubblePrefab;
        }
    }

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

        if (_bubblePrefab == null)
        {
            Debug.LogError("Bubble Prefab is not assigned in the inspector");
        }
    }
}
