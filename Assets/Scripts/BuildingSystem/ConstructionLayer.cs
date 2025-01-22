using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ConstructionLayer : TileMapLayer
{
    private Dictionary<Vector3Int, Buildable> buildables = new Dictionary<Vector3Int, Buildable>();
    
    [SerializeField] private List<BuildableItem> allBuildable;

    public void Build(Vector3 _worldPosition, BuildableItem _item, int _direction, TileBase _currentTile,Tilemap _rowTilemap)
    {
        Vector3Int coordinates = tilemap.WorldToCell(_worldPosition) - new Vector3Int(1, 1);;

        GameObject itemObject = Instantiate(_item.buildingPrefab, tilemap.CellToWorld(coordinates + new Vector3Int(1, 1)) + BuildingManager.instance.offset, Quaternion.identity, transform);

        float zOffset = tilemap.CellToWorld (coordinates + new Vector3Int(1, 1)).x + tilemap.CellToWorld(coordinates + new Vector3Int(1, 1)).y;

        itemObject.transform.position = new Vector3(itemObject.transform.position.x, itemObject.transform.position.y, -zOffset);

        itemObject.transform.position = BuildingManager.PositionZScale(itemObject.transform.position);
        //Debug.Log(itemObject.transform.position);

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
