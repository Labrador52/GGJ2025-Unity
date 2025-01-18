using UnityEngine;

public class Fan : BuildingBase
{
    private Vector3Int effectZone;
    [SerializeField] private int effectLength;

    protected override void Start()
    {
        base.Start();

        recoveryTilemap = MapManager.instance.mountain;

        RecoveryTile();
        effectZone = buildable.coordinates + GetDirectionVector(buildable.direction);

        MapManager.instance.RegisteredFan(effectZone, this);
    }

    public static Vector3Int GetDirectionVector(int _direction)
    {

        switch (_direction)
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

    public Vector3Int FanLogic() => GetDirectionVector(buildable.direction) * effectLength;

}
