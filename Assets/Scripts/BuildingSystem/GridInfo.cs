using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInfo : MonoBehaviour
{
    [SerializeField] private Vector3Int bubbleBegin;
    [SerializeField] private Vector3Int bubbleEnd;
    [SerializeField] private Vector3Int bubbleMiddle;

    public GameObject tip;
    [HideInInspector] public Vector3 beginWorldCoor;
    [HideInInspector] public Vector3 endWorldCoor;
    [HideInInspector] public Vector3 middleWorldCoor;

    private void Start()
    {
        beginWorldCoor = BuildingManager.instance.constructionLayer.tilemap.CellToWorld(bubbleBegin);
        endWorldCoor = BuildingManager.instance.constructionLayer.tilemap.CellToWorld(bubbleEnd);
        middleWorldCoor = BuildingManager.instance.constructionLayer.tilemap.CellToWorld(bubbleMiddle);
    }
}
