using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    private Dictionary<Vector3Int, Fan> fanList;
    [SerializeField] private GameObject tileInstantiate;
    [SerializeField] private Tile defaultTile;
    private GridInfo gridInfo;

    public Tilemap ground;
    public Tilemap mountain;
    public Tilemap water;
    public Tilemap contaminate;
    [Space]
    public int maxWidth;
    public int maxLength;

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

    //private void Update()
    //{
    //    foreach(var coor in fanList.Keys)
    //    {
    //        Debug.Log(coor);
    //    }
    //    Debug.Log("end");
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="level prefab下的Grid对象"></param>
    /// <param name="预制体tileInstantiat"></param>
    /// <param name="任一草地瓦片"></param>
    public void Initial(GameObject _Grid,GameObject _tileInstantiat, Tile _defaultTile)
    {
        tileInstantiate = _tileInstantiat;
        defaultTile = _defaultTile;
        gridInfo = _Grid.GetComponentInChildren<GridInfo>();

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

        maxWidth = 48;
        maxLength = 48;

        gameObject.SetActive(true);
    }

    public Vector3 GetEndWorldCoordianate() => gridInfo.endWorldCoor;
    public Vector3 GetBeginWorldCoordinate() => gridInfo.beginWorldCoor;
    public Vector3 GetMiddleWorldCoordinate() => gridInfo.middleWorldCoor;

    public bool IsGetMail(Vector3 _position)
    {
        Vector3Int bubbleCoordinate = BuildingManager.instance.constructionLayer.tilemap.WorldToCell(_position);

        if (Vector3Int.Distance(bubbleCoordinate, gridInfo.bubbleMiddle) <= 1)
        {
            CloseTip();
            return true;    
        }
        return false;
    }

    public bool IsArrive(Vector3 _position)
    {
        Vector3Int bubbleCoordinate = BuildingManager.instance.constructionLayer.tilemap.WorldToCell(_position);

        if (Vector3Int.Distance(bubbleCoordinate, gridInfo.bubbleEnd) <= 1)
            return true;
        return false;
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
                    tileItem.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    tileItem.transform.SetParent(mountain.gameObject.transform.parent);
                    tileItem.transform.position -= new Vector3(0, -0.25f, tileItem.transform.position.x + tileItem.transform.position.y);
                }
            }
        }
    }

    public void CloseTip() => gridInfo.tip.SetActive(false);

    public void RegisteredFan(Vector3Int _effectCoordinate,Fan _fan) => fanList.Add(_effectCoordinate, _fan);

    public bool IsOverlapFan(Vector3Int _effectCoordinate) => fanList.ContainsKey(_effectCoordinate);

    public Vector3Int CheckFanEffectRange(Vector3 _position)
    {
        Vector3Int bubbleCoordinate = BuildingManager.instance.constructionLayer.tilemap.WorldToCell(_position);

        //Debug.Log("bubbleCoordinate:" + bubbleCoordinate);

        foreach(var coor in fanList.Keys)
        {
            //Debug.Log("coordinate:" + coor);
            if (coor == bubbleCoordinate)
                return fanList[coor].FanLogic();
        }

        return new Vector3Int(0, 0);
    }
}
