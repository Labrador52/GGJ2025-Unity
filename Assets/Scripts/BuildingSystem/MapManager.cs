using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    private Dictionary<Vector3Int, Fan> fanList;
    [SerializeField] private GameObject tileInstantiate;
    [SerializeField] private Tile defaultTile;

    public Tilemap ground;
    public Tilemap mountain;
    public Tilemap water;
    public Tilemap contaminate;
    [Space]
    public int maxWidth = 48;
    public int maxLength = 48;

    private void Awake()
    {
        gameObject.SetActive(false);

        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        fanList = new Dictionary<Vector3Int, Fan>();

        InitialMap();
    }

    //_Grid为level prefab下的Grid对象，_tileInstantiat为预制体tileInstantiat，_defaultTile为任一草地瓦片
    public void Initial(GameObject _Grid,GameObject _tileInstantiat, Tile _defaultTile)
    {
        tileInstantiate = _tileInstantiat;
        defaultTile = _defaultTile;

        int childCount = _Grid.transform.childCount;
        for(int i = 0;i<childCount;i++)
        {
            string childTag = _Grid.transform.GetChild(i).tag;
            switch(childTag)
            {
                case "Ground":
                    ground = _Grid.transform.GetChild(i).GetComponent<Tilemap>();
                    break;
                case "Water":
                    water = _Grid.transform.GetChild(i).GetComponent<Tilemap>();
                    break;
                case "Mountain":
                    mountain = _Grid.transform.GetChild(i).GetComponent<Tilemap>();
                    break;
                case "Contaminate":
                    contaminate = _Grid.transform.GetChild(i).GetComponent<Tilemap>();
                    break;
                default: 
                    break;
            }
        }

        gameObject.SetActive(true);
    }

    private void InitialMap()
    {
        for (int i = -maxLength / 2; i < maxLength / 2; i++) 
        {
            for (int j = -maxWidth / 2; j < maxWidth / 2; j++)
            {
                var tileBase =mountain.GetTile(new Vector3Int(i, j));
                if (tileBase != null)
                {
                    mountain.SetTile(new Vector3Int(i, j), defaultTile);
                    Tile tile = tileBase as Tile;
                    GameObject tileItem = Instantiate(tileInstantiate, mountain.CellToWorld(new Vector3Int(i, j)), Quaternion.identity);
                    tileItem.GetComponent<SpriteRenderer>().sprite = tile.sprite;
                    tileItem.transform.position -= new Vector3(0, -0.25f, tileItem.transform.position.x + tileItem.transform.position.y);
                }
            }
        }
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
