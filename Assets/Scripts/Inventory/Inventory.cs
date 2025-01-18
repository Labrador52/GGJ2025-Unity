using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private Dictionary<MaterialsItem, int> materialsInventory;
    public List<MaterialsItem> allMaterials;

    private void Awake()
    {
        gameObject.SetActive(false);

        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        materialsInventory = new Dictionary<MaterialsItem, int>();
    }

    //_allMaterials存放所有的MaterialsItem //MaterialsItem为ScriptableObject //顺序需要与MaterialsItem中id字段顺序保持一致
    public void Initial(List<MaterialsItem> _allMaterials)
    {
        allMaterials = _allMaterials;   

        gameObject.SetActive(true);
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
