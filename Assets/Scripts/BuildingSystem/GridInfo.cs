using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInfo : MonoBehaviour
{
    [SerializeField] public Vector3Int bubbleBegin;
    [SerializeField] public Vector3Int bubbleEnd;
    [SerializeField] public Vector3Int bubbleMiddle;

    public Transform begin;
    public Transform end;
    public Transform middle;

    public GameObject tip;
    public Vector3 beginWorldCoor;
    public Vector3 endWorldCoor;
    public Vector3 middleWorldCoor;

    private void Start()
    {
        bubbleBegin = MapManager.instance.mountain.WorldToCell(begin.position) - new Vector3Int(1, 1);
        bubbleMiddle = MapManager.instance.mountain.WorldToCell(middle.position);
        bubbleEnd = MapManager.instance.mountain.WorldToCell(end.position) - new Vector3Int(1, 1);

        beginWorldCoor = MapManager.instance.mountain.CellToWorld(bubbleBegin);
        endWorldCoor = MapManager.instance.mountain.CellToWorld(bubbleEnd);
        middleWorldCoor = MapManager.instance.mountain.CellToWorld(bubbleMiddle);

        begin.position = new Vector3(begin.position.x, begin.position.y, -(beginWorldCoor.x + beginWorldCoor.y));
        end.position = new Vector3(end.position.x, end.position.y, -(endWorldCoor.x + endWorldCoor.y));
        middle.position = new Vector3(middle.position.x, middle.position.y, -(middleWorldCoor.x + middleWorldCoor.y));

        begin.position = BuildingManager.PositionZScale(begin.position);
        end.position = BuildingManager.PositionZScale(end.position);
        middle.position = BuildingManager.PositionZScale(middle.position);
    }
}
