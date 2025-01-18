using System;

[Serializable]
public class InventoryItem
{
    public MaterialsItem materials;
    public int number;

    public InventoryItem(MaterialsItem _materials, int _number)
    {
        materials = _materials;
        number = _number;
    }
}
