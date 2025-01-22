using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    private Dictionary<Vector3Int, Fan> fanList;
    private Dictionary<Vector3Int, Bridge> bridgeList;
    [SerializeField] private GameObject tileInstantiate;
    [SerializeField] private Tile defaultTile;
    public GridInfo gridInfo;

    private bool[,] ClearFogArr;

    public Tilemap ground;
    public Tilemap mountain;
    public Tilemap water;
    public Tilemap contaminate;
    public Tilemap fog;
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
        bridgeList = new Dictionary<Vector3Int, Bridge>();

        ClearFogArr = new bool[9, 9]
        {
            { false,true,true,true,false,false,false,false,false },
            { true,true,true,true,true,true,false,false,false },
            { true,true,true,true,true,true,true,false, false },
            { true,true,true,true,true,true,true,true,false },
            { true,true,true,true,true,true,true,true,true },
            { false,true,true,true,true,true,true,true,true },
            { false,false,true,true,true,true,true,true,true },
            { false,false,false,true,true,true,true,true,true },
            { false,false,false,false,true,true,true,true,false },
        };

        //InitialMap();
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
    /// <param name="level prefab�µ�Grid����"></param>
    /// <param name="Ԥ����tileInstantiat"></param>
    /// <param name="��һ�ݵ���Ƭ"></param>
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
                case "Fog":
                    fog = _Grid.transform.GetChild(i).GetComponent<Tilemap>();
                    break;
                default: 
                    break;
            }
        }

        maxWidth = 100;
        maxLength = 100;

        gameObject.SetActive(true);
        InitialMap();
    }

    public Vector3 GetEndWorldCoordianate() => gridInfo.endWorldCoor;
    public Vector3 GetBeginWorldCoordinate() => gridInfo.beginWorldCoor;
    public Vector3 GetMiddleWorldCoordinate() => gridInfo.middleWorldCoor;

    public bool IsGetMail(Vector3 _position)
    {
        if(gridInfo == null) return false;

        Vector3Int bubbleCoordinate = BuildingManager.instance.constructionLayer.tilemap.WorldToCell(_position);

        if (Vector3Int.Distance(bubbleCoordinate, gridInfo.bubbleMiddle) <= 1)
        {
            CloseTip();
            return true;    
        }
        return false;
    }

    public void ClearSurroundingFog(Vector3 _position,bool _isRight,bool _isFront)
    {
        Vector3Int bubbleCoordinate = BuildingManager.instance.constructionLayer.tilemap.WorldToCell(_position);

        for(int i = 4; i > -5; i--)
        {
            for(int j = -4; j < 5; j++)
            {
                if (!ClearFogArr[i + 4, j + 4]) continue;

                if (fog.GetTile(bubbleCoordinate + new Vector3Int(i, j)) != null)
                {
                    int index;
                    if (_isRight && _isFront) index = 2;
                    else if(!_isFront && !_isRight) index = 1;
                    else
                    {
                        if (j == 0) index = Mathf.FloorToInt(Random.value) + 1;
                        else index = j < 0 ? 2 : 1;
                    }


                    fog.SetTile(bubbleCoordinate + new Vector3Int(i, j), null);

                    GameObject cloud = CloudPool.instance.pool.Get();
                    //cloud.transform.parent = fog.transform;
                    cloud.transform.position = mountain.CellToWorld(bubbleCoordinate + new Vector3Int(i, j)) + new Vector3(0, 0.25f);

                    cloud.GetComponent<Cloud>().DestroySelfAnimation(index);
                }
            }
        }
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
                var tileBase = mountain.GetTile(new Vector3Int(i, j));
                if (tileBase != null)
                {
                    mountain.SetTile(new Vector3Int(i, j), defaultTile);
                    Tile tile = tileBase as Tile;
                    GameObject tileItem = Instantiate(tileInstantiate, mountain.CellToWorld(new Vector3Int(i, j)), Quaternion.identity);
                    tileItem.GetComponent<SpriteRenderer>().sprite = tile.sprite;
                    tileItem.GetComponent<SpriteRenderer>().sortingOrder = 3;
                    tileItem.transform.SetParent(mountain.gameObject.transform.parent);
                    tileItem.transform.position = new Vector3(tileItem.transform.position.x, tileItem.transform.position.y + 0.25f, -(tileItem.transform.position.x + tileItem.transform.position.y));
                    //tileItem.transform.position -= new Vector3(0, -0.25f, tileItem.transform.position.x + tileItem.transform.position.y);
                    tileItem.transform.position = BuildingManager.PositionZScale(tileItem.transform.position);
                }
            }
        }
    }

    public void CloseTip() => gridInfo.tip.SetActive(false);

    public void DeleteAllBuild()
    {
        foreach(var coor in fanList.Keys)
            fanList[coor].DestroySelf();

        foreach(var coor in bridgeList.Keys)
            bridgeList[coor].DestroySelf();

        fanList.Clear();
        bridgeList.Clear();
        BuildingManager.instance.constructionLayer.DeleteAllBuildable();
    }

    public void RegisteredFan(Vector3Int _effectCoordinate,Fan _fan) => fanList.Add(_effectCoordinate, _fan);

    public void RegisteredBridge(Vector3Int _coordinate,Bridge _bridge) => bridgeList.Add(_coordinate, _bridge);

    public void LogoutFan(Vector3Int _effectCoordinate) => fanList.Remove(_effectCoordinate);

    public void LogoutBridge(Vector3Int _coordinate) => bridgeList.Remove(_coordinate);

    public bool IsOverlapFan(Vector3Int _effectCoordinate) => fanList.ContainsKey(_effectCoordinate);

    public Vector3Int CheckFanEffectRange(Vector3 _position)
    {
        if (gridInfo == null)
            return new Vector3Int(0, 0);

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
