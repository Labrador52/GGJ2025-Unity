using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class BuildingBase : MonoBehaviour
{
    public ConstructionLayer currentConstructionLayer;

    public Buildable buildable;
    protected SpriteRenderer sr;
    public TileBase tile;
    protected Tilemap recoveryTilemap;


    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        sr.sprite = buildable.buildableType.previewSprites[buildable.direction];
    }

    protected virtual void Update()
    {

    }

    public void SetBuildable(Buildable _buildable) => buildable = new Buildable(_buildable.buildableType, _buildable.coordinates, _buildable.direction, _buildable.gameObject);

    protected void RecoveryTile()
    {
        recoveryTilemap.SetTile(buildable.coordinates, tile);
    }
}
