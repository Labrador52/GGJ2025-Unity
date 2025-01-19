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

        Vector3 zOffset = new Vector3(0, 0, coordinates.x + coordinates.y);

        GameObject itemObject = Instantiate(_item.buildingPrefab, tilemap.CellToWorld(coordinates + new Vector3Int(1, 1)) + BuildingManager.instance.offset + zOffset + new Vector3(0, 0, 20), Quaternion.identity, buildParent);

        Buildable buildable = new Buildable(_item, coordinates + new Vector3Int(1, 1), BuildingManager.instance.direction, itemObject);
        BuildingBase itemScript = itemObject.GetComponent<BuildingBase>();

        itemScript.SetBuildable(buildable);
        itemScript.currentConstructionLayer = this;
        itemScript.tile = _currentTile;

        buildables.Add(coordinates, buildable);
        BuildingManager.instance.isBuildingMode = false;

        AudioManager.instance.PlaySFX(1);
        //Inventory.instance.RemoveItem(Inventory.instance.allMaterials[_item.buildingId]);
    }


    public bool IsEmpty(Vector3 _worldPosition, BuildableItem _item)
    {
        if (buildables.ContainsKey(tilemap.WorldToCell(_worldPosition)))
            return false;

        return true;
    }

}
