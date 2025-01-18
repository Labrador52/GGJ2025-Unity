using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    [SerializeField] private GameObject[] _levelPrefabs;
    public GameObject[] LevelPrefabs
    {
        get
        {
            return _levelPrefabs;
        }
    }

    [SerializeField] private GameObject _tileInstantiat;
    public static GameObject TileInstantiat
    {
        get
        {
            return Instance._tileInstantiat;
        }
    }

    [SerializeField] private List<BuildableItem> _allBuildale;
    public static List<BuildableItem> AllBuildable
    {
        get
        {
            return Instance._allBuildale;
        }
    }

    [SerializeField] private Tile _tile;
    public static Tile Tile
    {
        get
        {
            return Instance._tile;
        }
    }

    [SerializeField] private List<MaterialsItem> _allMaterials;
    public static List<MaterialsItem> AllMaterials
    {
        get
        {
            return Instance._allMaterials;
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
