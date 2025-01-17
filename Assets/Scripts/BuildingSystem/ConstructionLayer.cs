using System.Collections.Generic;
using UnityEngine;

public class ConstructionLayer : TileMapLayer
{
    private Dictionary<Vector3Int, Buildable> buildables = new Dictionary<Vector3Int, Buildable>();
    [SerializeField] private Transform buildParent;
    [SerializeField] private List<BuildableItem> allBuildable;

    public void Build(Vector3 _worldPosition, BuildableItem _item, int _direction)
    {
        if (!IsEmpty(_worldPosition, _item))
            return;

        if (!BuildingManager.instance.isBuildingMode)
            return;

        Vector3Int coordinates = tilemap.WorldToCell(_worldPosition);

        GameObject itemObject = Instantiate(_item.buildingPrefab, tilemap.CellToWorld(coordinates) + BuildingManager.instance.offset, Quaternion.identity, buildParent);

        Buildable buildable = new Buildable(_item, coordinates, BuildingManager.instance.direction, itemObject);
        BuildingBase itemScript = itemObject.GetComponent<BuildingBase>();

        itemScript.SetBuildable(buildable);
        itemScript.currentConstructionLayer = this;

        buildables.Add(coordinates, buildable);
        //BuildingManager.instance.isBuildingMode = false;
    }


    public bool IsEmpty(Vector3 _worldPosition, BuildableItem _item)
    {
        if (buildables.ContainsKey(tilemap.WorldToCell(_worldPosition)))
            return false;

        return true;
    }

}
