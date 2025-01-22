using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance;
    public bool isBuildingMode;
    public bool isDeleteBuildingMode;
    [SerializeField] private BuildableItem activeBuildable;
    public ConstructionLayer constructionLayer;
    [SerializeField] private PreviewLayer previewLayer;
    [HideInInspector] public int direction = 0;
    public Vector3 offset = new Vector3(0,0.25f);
    [SerializeField] private List<BuildableItem> allBuildable;

    private void Awake()
    {
        gameObject.SetActive(false);

        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

        //Debug.Log("鼠标坐标：" + constructionLayer.tilemap.WorldToCell(mousePosition));

        if (constructionLayer == null)
            return;

        if (Mathf.Abs(constructionLayer.tilemap.WorldToCell(mousePosition).x) > MapManager.instance.maxWidth / 2 ||
            Mathf.Abs(constructionLayer.tilemap.WorldToCell(mousePosition).y) > MapManager.instance.maxLength / 2)
            return;

        if (isBuildingMode)
        {
            if (Input.GetKeyDown(KeyCode.R))
                direction = direction < activeBuildable.previewSprites.Count - 1 ? direction + 1 : 0;

            BuildingLogic(mousePosition);
        }
        else if (isDeleteBuildingMode)
            DeleteBuildingLogic(mousePosition);
        else previewLayer.ClearPreview();

    }

    private void DeleteBuildingLogic(Vector3 _mousePosition)
    {
        bool isValid = !constructionLayer.IsEmpty(_mousePosition);

        if (isValid)
        {
            Vector3Int mouseCoordiantes = constructionLayer.tilemap.WorldToCell(_mousePosition) - new Vector3Int(1, 1);
            Buildable buildable = constructionLayer.GetBuildable(mouseCoordiantes);
            if (buildable == null) return;

            previewLayer.ShowPreview(buildable.buildableType, _mousePosition, isValid, buildable.direction, false);
            
            if(Input.GetMouseButtonDown(0))
            {
                constructionLayer.DeleteBuildable(mouseCoordiantes);
            }
        }
        else
            previewLayer.ClearPreview();

        
    }

    /// <summary>
    /// Initial
    /// </summary>
    /// <param name="_Grid"></param>
    /// <param name="所有的建筑"></param>
    public void Initial(GameObject _Grid,List<BuildableItem> _allBuildable)
    {
        constructionLayer = _Grid.GetComponentInChildren<ConstructionLayer>();
        previewLayer = _Grid.GetComponentInChildren<PreviewLayer>();

        allBuildable = _allBuildable;

        gameObject.SetActive(true);
    }

    public void SetActiveBuilding(int _id)
    {
        isBuildingMode = true;

        activeBuildable = allBuildable[_id];
    }

    private void BuildingLogic(Vector3 _mousePosition)
    {
        if (activeBuildable == null)
        {
            previewLayer.ClearPreview();
            return;
        }

        bool isValid;
        Collider2D hit;
        CanBuilding(_mousePosition, out isValid, out hit);

        if (isValid && Input.GetMouseButtonDown(0))
        {
            Tilemap rowTilemap = hit.GetComponent<Tilemap>();
            TileBase currentTile = rowTilemap.GetTile(constructionLayer.tilemap.WorldToCell(_mousePosition));

            constructionLayer.Build(_mousePosition, activeBuildable, direction, currentTile, rowTilemap);

            hit.GetComponent<Tilemap>().SetTile(constructionLayer.tilemap.WorldToCell(_mousePosition), null);
            //Debug.Log(constructionLayer.tilemap.WorldToCell(_mousePosition));
            

        }
    }

    private void CanBuilding(Vector3 _mousePosition, out bool isValid, out Collider2D hit)
    {
        isValid = true;
        Vector3 checkPoint = constructionLayer.tilemap.CellToWorld(constructionLayer.tilemap.WorldToCell(_mousePosition));

        hit = Physics2D.OverlapPoint(checkPoint);
        if (hit != null && hit.GetComponent<Tilemap>() != null)
        {
            if (hit.gameObject.tag == "Water" || hit.gameObject.tag == "Mountain")
            {
                isValid = false;
                //Debug.Log("瓦片检测");
            }
            //else
            //{
            //    isValid = constructionLayer.IsEmpty(_mousePosition, activeBuildable);
            //    Debug.Log("建筑物检测");
            //}
        }
        else
        {
            isValid = false;
            //Debug.Log("未检测到地面");
        }

        if (isValid && activeBuildable.buildingId == 0)
        {
            Vector3Int effectCoordinate = Fan.GetDirectionVector(direction) + constructionLayer.tilemap.WorldToCell(_mousePosition);
            isValid = !MapManager.instance.IsOverlapFan(effectCoordinate);
        }

        if (isValid)
        {
            if (constructionLayer.tilemap.WorldToCell(_mousePosition) == MapManager.instance.gridInfo.bubbleEnd ||
                constructionLayer.tilemap.WorldToCell(_mousePosition) == MapManager.instance.gridInfo.bubbleMiddle ||
                constructionLayer.tilemap.WorldToCell(_mousePosition) == MapManager.instance.gridInfo.bubbleBegin)
                isValid = false;
        }

        //if (isValid)
        //    isValid = Inventory.instance.CanRemoveItem(Inventory.instance.allMaterials[activeBuildable.buildingId]);

        previewLayer.ShowPreview(activeBuildable, _mousePosition, isValid, direction, true);
    }

    public void EnterBuildingMode(int _buildId)
    {
        direction = 0;
        activeBuildable = allBuildable[_buildId];
        isBuildingMode = true;
        isDeleteBuildingMode = false;
    }

    public void EnterDeleteBuildingMode()
    {
        isDeleteBuildingMode = true;
        isBuildingMode = false;
    }

    public static Vector3 PositionZScale(Vector3 _position, float _scale = 0.2f) => _position = new Vector3(_position.x, _position.y, _position.z * _scale);
}