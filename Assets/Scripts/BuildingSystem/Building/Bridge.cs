public class Bridge : BuildingBase
{
    protected override void Start()
    {
        base.Start();

        recoveryTilemap = MapManager.instance.water;

        RecoveryTile();
    }
}
