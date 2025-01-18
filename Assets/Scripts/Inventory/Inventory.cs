using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private Dictionary<MaterialsItem, int> materialsInventory;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        materialsInventory = new Dictionary<MaterialsItem, int>();
    }

    public void AddItem(MaterialsItem _materials, int _num = 1)
    {
        foreach (var item in materialsInventory.Keys)
        {
            if (_materials == item)
            {
                materialsInventory[item] += _num;
                return;
            }
        }

        materialsInventory.Add(_materials, _num);
    }

    public bool CanRemoveItem(MaterialsItem _materials, int _num = 1)
    {
        foreach (var item in materialsInventory.Keys)
        {
            if (_materials == item && materialsInventory[item] >= _num)
                return true;
        }

        return false;
    }

    public void RemoveItem(MaterialsItem _materials, int _num = 1)
    {
        materialsInventory[_materials] -= _num;

        if (materialsInventory[_materials] == 0)
            materialsInventory.Remove(_materials);
    }
}
