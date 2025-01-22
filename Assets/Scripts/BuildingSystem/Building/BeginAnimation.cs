using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginAnimation : MonoBehaviour
{
    private Begin begin;

    private void Start()
    {
        begin = GetComponentInParent<Begin>();
    }

    public void SpawnBubble()
    {
        Bubble.Spawn(MapManager.instance.GetBeginWorldCoordinate());
    }
}
