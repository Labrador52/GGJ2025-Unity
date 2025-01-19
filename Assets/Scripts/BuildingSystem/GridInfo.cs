using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInfo : MonoBehaviour
{
    [SerializeField] private Vector3Int bubbleBegin;
    [SerializeField] public Vector3Int bubbleEnd;
    [SerializeField] public Vector3Int bubbleMiddle;

    public Transform begin;
    public Transform end;
    public Transform middle;

    public GameObject tip;
    [HideInInspector] public Vector3 beginWorldCoor;
    [HideInInspector] public Vector3 endWorldCoor;
    [HideInInspector] public Vector3 middleWorldCoor;

    private void Start()
    {
        bubbleBegin = BuildingManager.instance.constructionLayer.tilemap.WorldToCell(begin.position);
        bubbleMiddle = BuildingManager.instance.constructionLayer.tilemap.WorldToCell(middle.position);
        bubbleEnd = BuildingManager.instance.constructionLayer.tilemap.WorldToCell(end.position);

        beginWorldCoor = BuildingManager.instance.constructionLayer.tilemap.CellToWorld(bubbleBegin);
        endWorldCoor = BuildingManager.instance.constructionLayer.tilemap.CellToWorld(bubbleEnd);
        middleWorldCoor = BuildingManager.instance.constructionLayer.tilemap.CellToWorld(bubbleMiddle);
    }
}
