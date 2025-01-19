using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PreviewLayer : TileMapLayer
{
    [SerializeField] private SpriteRenderer previewRenderer;
    [SerializeField] private TilemapRenderer tilemapRenderer;

    public void ShowPreview(BuildableItem _item, Vector3 _worldCoordinates, bool _isValid, int _direction)
    {
        Vector3Int coordinates = tilemap.WorldToCell(_worldCoordinates);
        previewRenderer.enabled = true;
        previewRenderer.transform.position = tilemap.CellToWorld(coordinates) + BuildingManager.instance.offset;

        if (_item.buildingId == 0)
            previewRenderer.transform.position += new Vector3(0, 0.25f);

        previewRenderer.transform.position = new Vector3(previewRenderer.transform.position.x, previewRenderer.transform.position.y,
            -(tilemap.CellToWorld(coordinates).x + tilemap.CellToWorld(coordinates).y));

        previewRenderer.sprite = _item.previewSprites[_direction];
        previewRenderer.color = _isValid ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);
        tilemapRenderer.enabled = true;
    }

    public void ClearPreview()
    {
        previewRenderer.enabled = false;
        tilemapRenderer.enabled = false;
    }
}
