using UnityEngine;

public class Fan : BuildingBase
{
    private Vector3Int effectZone;
    [SerializeField] private int effectLength;

    protected override void Start()
    {
        base.Start();

        recoveryTilemap = BuildingManager.instance.mountain;

        RecoveryTile();
        effectZone = buildable.coordinates + GetDirectionVector();
    }

    private Vector3Int GetDirectionVector()
    {

        switch (buildable.direction)
        {
            case 0:
                return new Vector3Int(1, 0);
            case 1:
                return new Vector3Int(0, -1);
            case 2:
                return new Vector3Int(-1, 0);
            case 3:
                return new Vector3Int(0, 1);
            default:
                return new Vector3Int(0, 0);
        }
    }

    public Vector3Int FanLogic(Transform _bubble)
    {
        Vector3Int bubbleCoordinate = currentConstructionLayer.tilemap.WorldToCell(_bubble.position);
        if (bubbleCoordinate == effectZone)
        {
            return GetDirectionVector() * effectLength;
        }
        else
            return new Vector3Int(0, 0);
    }
}
