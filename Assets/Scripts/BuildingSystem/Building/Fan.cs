public class Fan : BuildingBase
{
    protected override void Start()
    {
        base.Start();

        recoveryTilemap = BuildingManager.instance.mountain;

        RecoveryTile();
    }

}
