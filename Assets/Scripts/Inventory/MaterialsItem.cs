using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Materials/New Materials Item", fileName = "New Materials Item")]
public class MaterialsItem : ScriptableObject
{
    public int materialsId;
    public string materialsName;
    public Sprite materialsSprite;
    [TextArea]
    public string materialsDescription;
}
