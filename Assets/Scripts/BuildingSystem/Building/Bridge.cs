public class Bridge : BuildingBase
{
    protected override void Start()
    {
        base.Start();

        recoveryTilemap = BuildingManager.instance.water;

        RecoveryTile();
    }
}
