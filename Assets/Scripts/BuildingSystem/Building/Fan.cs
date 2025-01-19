using UnityEngine;

public class Fan : BuildingBase
{
    private Vector3Int effectZone;
    [SerializeField] private int effectLength;
    [SerializeField] private Animator animator;
    protected override void Start()
    {
        base.Start();

        float zOffset = currentConstructionLayer.tilemap.CellToWorld(buildable.coordinates).x + currentConstructionLayer.tilemap.CellToWorld(buildable.coordinates).y;

        transform.position = new Vector3(transform.position.x, transform.position.y, -zOffset);

        recoveryTilemap = MapManager.instance.mountain;
        animator = GetComponentInChildren<Animator>();

        animator.SetInteger("direction", buildable.direction);

        RecoveryTile();
        effectZone = buildable.coordinates + GetDirectionVector(buildable.direction);

        //Debug.Log("风扇坐标"+buildable.coordinates);
        //Debug.Log("风扇作用坐标" + effectZone);
        //Debug.LogError("");

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

    public void DestroySelf() => Destroy(gameObject);

}
