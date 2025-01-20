using UnityEngine;

public class Bridge : BuildingBase
{
    protected override void Start()
    {
        base.Start();

        recoveryTilemap = MapManager.instance.water;

        transform.position += new Vector3(0.0f, 0.25f, 0.0f);

        RecoveryTile();

        MapManager.instance.RegisteredBridge(buildable.coordinates, this);
    }
}
