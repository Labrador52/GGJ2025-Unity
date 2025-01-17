using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building/New Buildable Item", fileName = "New Buildable Item")]
public class BuildableItem : ScriptableObject
{
    public int buildingId;
    public string buildingName;
    public List<Sprite> previewSprites;
    public GameObject buildingPrefab;

    [TextArea]
    public string buildingDescription;
}
