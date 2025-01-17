using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ConstructionLayer : TileMapLayer
{
    private Dictionary<Vector3Int, Buildable> buildables = new Dictionary<Vector3Int, Buildable>();
    [SerializeField] private Transform buildParent;
    [SerializeField] private List<BuildableItem> allBuildable;

    public void Build(Vector3 _worldPosition, BuildableItem _item, int _direction, TileBase _currentTile)
    {
        Vector3Int coordinates = tilemap.WorldToCell(_worldPosition) - new Vector3Int(1, 1);

        GameObject itemObject = Instantiate(_item.buildingPrefab, tilemap.CellToWorld(coordinates + new Vector3Int(1, 1)) + BuildingManager.instance.offset, Quaternion.identity, buildParent);

        Buildable buildable = new Buildable(_item, coordinates, BuildingManager.instance.direction, itemObject);
        BuildingBase itemScript = itemObject.GetComponent<BuildingBase>();

        itemScript.SetBuildable(buildable);
        itemScript.currentConstructionLayer = this;
        itemScript.tile = _currentTile;

        buildables.Add(coordinates, buildable);
        BuildingManager.instance.isBuildingMode = false;


    }


    public bool IsEmpty(Vector3 _worldPosition, BuildableItem _item)
    {
        if (buildables.ContainsKey(tilemap.WorldToCell(_worldPosition)))
            return false;

        return true;
    }

}
