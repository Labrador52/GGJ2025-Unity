using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ConstructionLayer : TileMapLayer
{
    private Dictionary<Vector3Int, Buildable> buildables = new Dictionary<Vector3Int, Buildable>();
    [SerializeField] private Transform buildParent;
    [SerializeField] private List<BuildableItem> allBuildable;

    public void Build(Vector3 _worldPosition, BuildableItem _item, int _direction, TileBase _currentTile,Tilemap _rowTilemap)
    {
        Vector3Int coordinates = tilemap.WorldToCell(_worldPosition) - new Vector3Int(1, 1);

        Vector3 zOffset = new Vector3(0, 0, coordinates.x + coordinates.y);

        GameObject itemObject = Instantiate(_item.buildingPrefab, tilemap.CellToWorld(coordinates + new Vector3Int(1, 1)) + BuildingManager.instance.offset + zOffset + new Vector3(0, 0, 20), Quaternion.identity, buildParent);

        Buildable buildable = new Buildable(_item, coordinates + new Vector3Int(1, 1), BuildingManager.instance.direction, itemObject);
        BuildingBase itemScript = itemObject.GetComponent<BuildingBase>();

        itemScript.SetBuildable(buildable);
        itemScript.currentConstructionLayer = this;
        itemScript.tile = _currentTile;
        itemScript.rowTilemap = _rowTilemap;

        buildables.Add(coordinates, buildable);
        BuildingManager.instance.isBuildingMode = false;

        AudioManager.instance.PlaySFX(1);
        //Inventory.instance.RemoveItem(Inventory.instance.allMaterials[_item.buildingId]);
    }


    public bool IsEmpty(Vector3 _worldPosition)
    {
        if (buildables.ContainsKey(tilemap.WorldToCell(_worldPosition) - new Vector3Int(1, 1))) 
            return false;

        return true;
    }

    public Buildable GetBuildable(Vector3Int _coordinate)
    {
        if (buildables.ContainsKey(_coordinate)) 
            return buildables[_coordinate];
        return null;
    }
    
    public void DeleteBuildable(Vector3Int _coordinate)
    {
        if (buildables.ContainsKey(_coordinate))
        {
            buildables[_coordinate].gameObject.GetComponent<BuildingBase>().DestroySelfOnConstructionLayer();
            Destroy(buildables[_coordinate].gameObject);
            buildables.Remove(_coordinate);
        }
    }

    public void DeleteAllBuildable() => buildables.Clear();
}
