using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    private Dictionary<Vector3Int, Fan> fanList;

    public Tilemap ground;
    public Tilemap mountain;
    public Tilemap water;
    public Tilemap contaminate;
    [Space]
    public int maxWidth;
    public int maxLength;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        fanList = new Dictionary<Vector3Int, Fan>();
    }

    public void RegisteredFan(Vector3Int _effectCoordinate,Fan _fan) => fanList.Add(_effectCoordinate, _fan);

    public bool IsOverlapFan(Vector3Int _effectCoordinate) => fanList.ContainsKey(_effectCoordinate);

    public Vector3Int CheckFanEffectRange(Vector3 _position)
    {
        Vector3Int bubbleCoordinate = BuildingManager.instance.constructionLayer.tilemap.WorldToCell(_position);

        foreach(var coor in fanList.Keys)
        {
            if(coor == bubbleCoordinate)
                return fanList[coor].FanLogic();
            
        }

        return new Vector3Int(0, 0);
    }
}
